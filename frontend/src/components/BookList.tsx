import React, { useEffect, useState } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import SimpleBook from "../interfaces/books/SimpleBook"

function BookList() {
  const dispatch = useAppDispatch();
  const books = useAppSelector((state) => state.books.books);

  useEffect(() => {
    dispatch(getAllBooks());
  }, []);
  return <div>
    {books.map((book : SimpleBook)=> {
      return (
        <div key={book.bookName}>
          <h1>Book Name: {book.bookName}</h1>
          <h2>Author Name: {book.authorName}</h2>
          <h3>ISBN: {book.ISBN}</h3>
        </div>
      )
    })}

  </div>;
}

export default BookList;
