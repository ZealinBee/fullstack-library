import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetBook from "../interfaces/books/GetBook";
import { deleteBook, selectCurrentBook } from "../redux/reducers/booksReducer";
import { addToCart } from "../redux/reducers/cartReducer";
import Book from "./Book";
import { BeatLoader } from "react-spinners";
import Footer from "./Footer";

function BookList() {
  const dispatch = useAppDispatch();
  const books = useAppSelector((state) => state.books.books);
  let token = useAppSelector((state) => state.users.currentToken);
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const currentBook = useAppSelector((state) => state.books.currentBook);
  const isBookInCart = useAppSelector((state) =>
    state.cart.cartItems.find((item) => item.bookId === currentBook?.bookId)
  );
  const hasFetched = useAppSelector((state) => state.books.hasFetched);

  useEffect(() => {
    if (hasFetched === false) {
      dispatch(getAllBooks());
    }
  }, [dispatch, hasFetched]);

  async function addToCartHandler(book: GetBook) {
    const response = await dispatch(addToCart(book));
    if (response.payload === "error") {
      toast.error("Book is already in loan cart");
    } else {
      toast.success(`Book added to loan cart`);
    }
  }

  return (
    <>
      {!hasFetched ? (
        <div className="loader">
          <BeatLoader color="#6b58e3" />
        </div>
      ) : (
        <>
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
                      Add to Cart
                    </button>
                  ) : null}
                </div>
              );
            })}

            <ToastContainer
              position="bottom-right"
              autoClose={5000}
              hideProgressBar={false}
              newestOnTop={false}
              closeOnClick
              rtl={false}
              pauseOnFocusLoss
              draggable
              pauseOnHover
              theme="light"
            />
          </div>
          {books.length === 0 ? (
            <div className="no-books">
              <h2>No books found</h2>
            </div>
          ) : (
            <Footer />
          )}
        </>
      )}
    </>
  );
}

export default BookList;
