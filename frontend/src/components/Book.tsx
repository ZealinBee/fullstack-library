import React from "react";
import { Link } from "react-router-dom";

import GetBook from "../interfaces/books/GetBook";
import { selectCurrentBook } from "../redux/reducers/booksReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";

interface BookProps {
  book: GetBook;
}

function Book({ book }: BookProps) {
  const dispatch = useAppDispatch();

  return (
    <div className="book" onClick={() => dispatch(selectCurrentBook(book))}>
      <Link to={`/books/${book.bookId}`}>
        <img src={book.bookImage} alt="book" />
      </Link>
    </div>
  );
}

export default Book;
