import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetBook from "../interfaces/books/GetBook";
import { deleteBook, selectCurrentBook } from "../redux/reducers/booksReducer";
import { addToCart } from "../redux/reducers/cartReducer";

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

  function addToCartHandler(book: GetBook) {
    dispatch(addToCart(book));
  }

  return (
    <div className="bookList">
      {books.map((book: GetBook) => {
        return (
          <div
            key={book.bookId}
            className="book"
            onClick={() => dispatch(selectCurrentBook(book))}
          >
            {/* {currentUser?.role === "Librarian" ? (
              <button onClick={() => deleteBookHandler(book.bookId)}>
                Delete
              </button>
            ) : null} */}
            <Link to={`/books/${book.bookId}`}>
              <img
                src="https://m.media-amazon.com/images/I/81zlbsnFiYL._AC_UF1000,1000_QL80_.jpg"
                alt="an image for the book"
              />
            </Link>
            {currentUser?.role === "User" ? (
              <button onClick={() => addToCartHandler(book)}>
                Add to Loan Cart
              </button>
            ) : null}
          </div>
        );
      })}
    </div>
  );
}

export default BookList;
