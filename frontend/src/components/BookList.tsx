import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetBook from "../interfaces/books/GetBook";
import { deleteBook, selectCurrentBook } from "../redux/reducers/booksReducer";
import { addToCart } from "../redux/reducers/cartReducer";
import Book from "./Book";

function BookList() {
  const dispatch = useAppDispatch();
  const books = useAppSelector((state) => state.books.books);
  let token = useAppSelector((state) => state.users.currentToken);
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const currentBook = useAppSelector((state) => state.books.currentBook);
  // const isBookInCart = useAppSelector((state) =>
  //   state.cart.cartItems.find((item) => item.bookId === currentBook?.bookId)
  // );

  useEffect(() => {
    dispatch(getAllBooks());
  }, []);

  function addToCartHandler(book: GetBook) {
    dispatch(addToCart(book));
  }

  return (
    <div className="bookList">
      {books.map((book: GetBook) => {
        return (
          <div key={book.bookId} className="bookList__book">
            <Book book={book}></Book>
            {currentUser?.role === "User" ? (
              <button
                onClick={() => addToCartHandler(book)}
                className="bookList__add-book"
              >
                Add to Loan Cart
              </button>
            ) : // !isBookInCart ? (
            //   <button
            //     onClick={() => addToCartHandler(book)}
            //     className="bookList__add-book"
            //   >
            //     Add to Loan Cart
            //   </button>
            // ) : (
            //   <button disabled className="bookList__add-book">
            //     Already in Loan Cart
            //   </button>
            // )
            null}
          </div>
        );
      })}
    </div>
  );
}

export default BookList;
