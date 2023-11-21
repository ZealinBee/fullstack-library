import React from "react";
import { Link } from "react-router-dom";

import GetBook from "../interfaces/books/GetBook";
import { selectCurrentBook } from "../redux/reducers/booksReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import useAppSelector from "../redux/hooks/useAppSelector";

interface BookProps {
  book: GetBook;
}

function Book({ book }: BookProps) {
  const dispatch = useAppDispatch();
  // Prop does not automatically update when book state changes, so we need to get the latest book
  const updatedBook = useAppSelector((state) =>
    state.books.books.find((b) => b.bookId === book.bookId)
  );

  const clickHandler = () => {
    dispatch(selectCurrentBook(updatedBook))
  }

  return (
    <div className="book" onClick={clickHandler}>
      <Link to={`/books/${book.bookId}`}>
        <img src={book.bookImage} alt="book" />
      </Link>
    </div>
  );
}

export default Book;
