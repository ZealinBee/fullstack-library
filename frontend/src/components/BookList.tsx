import React, { useEffect, useState } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetBook from "../interfaces/books/GetBook";
import { deleteBook } from "../redux/reducers/booksReducer";

function BookList() {
  const dispatch = useAppDispatch();
  const books = useAppSelector((state) => state.books.books);
  let token = useAppSelector((state) => state.users.currentToken);

  useEffect(() => {
    dispatch(getAllBooks());
  }, []);

  function deleteBookHandler(bookId : string) {
    dispatch(deleteBook({ bookId: bookId, jwt_token: token }));
  }
  return (
    <div>
      {books.map((book: GetBook) => {
        return (
          <>
            <div key={book.bookId}>
              <h1>Book Name: {book.bookName}</h1>
              <h2>Author Name: {book.authorName}</h2>
              <h3>ISBN: {book.ISBN}</h3>
            </div>
            <button onClick={() => deleteBookHandler(book.bookId)}>Delete</button>
          </>
        );
      })}
    </div>
  );
}

export default BookList;
