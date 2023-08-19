import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetBook from "../interfaces/books/GetBook";
import { deleteBook, selectCurrentBook } from "../redux/reducers/booksReducer";

function BookList() {
  const dispatch = useAppDispatch();
  const books = useAppSelector((state) => state.books.books);
  let token = useAppSelector((state) => state.users.currentToken);
  const currentUser = useAppSelector((state) => state.users.currentUser);

  useEffect(() => {
    dispatch(getAllBooks());
  }, []);

  function deleteBookHandler(bookId: string) {
    dispatch(deleteBook({ bookId: bookId, jwt_token: token }));
  }

  return (
    <div>
      {books.map((book: GetBook) => {
        return (
          <div key={book.bookId}>
            <div>
              <h1>Book Name: {book.bookName}</h1>
              <h2>Author Name: {book.authorName}</h2>
            </div>
            {currentUser?.role === "Librarian" ? (
              <button onClick={() => deleteBookHandler(book.bookId)}>
                Delete
              </button>
            ) : null}
            <Link to={`/books/${book.bookId}`}>
              <button onClick={() => dispatch(selectCurrentBook(book))}>
                Details
              </button>
            </Link>
          </div>
        );
      })}
    </div>
  );
}

export default BookList;
