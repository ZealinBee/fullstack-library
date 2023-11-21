import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetBook from "../interfaces/books/GetBook";
import { selectCurrentBook } from "../redux/reducers/booksReducer";
import { addToCart } from "../redux/reducers/cartReducer";
import Book from "./Book";
import { BeatLoader } from "react-spinners";
import Footer from "./Footer";
import { Pagination } from "@mui/material";
import { usePaginate } from "../utils/usePaginate";

function BookList() {
  const dispatch = useAppDispatch();
  const books = useAppSelector((state) => state.books.books);
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const hasFetched = useAppSelector((state) => state.books.hasFetched);
  const { handlePageChange, paginatedItems } = usePaginate({
    items: books,
    booksPerPage: 10,
  });

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
          <h3>First time loading all the books will take a while</h3>
          <BeatLoader color="#6b58e3" />
        </div>
      ) : (
        <>
          <div className="bookList">
            {paginatedItems.map((book: GetBook) => {
              return (
                <div key={book.bookId} className="bookList__book">
                  <Book book={book}></Book>
                  {currentUser?.role === "User" && (
                    <button
                      onClick={() => addToCartHandler(book)}
                      className="bookList__add-book"
                    >
                      Add to Cart
                    </button>
                  )}
                  {!currentUser && (
                    <Link to={"/auth"}>
                      <button className="bookList__add-book">
                        Log In to Loan
                      </button>
                    </Link>
                  )}
                  {currentUser?.role === "Librarian" && (
                    <Link
                      to={`/books/${book.bookId}`}
                      onClick={() => dispatch(selectCurrentBook(book))}
                    >
                      <button className="bookList__add-book">
                        Manage Book
                      </button>
                    </Link>
                  )}
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
          <Pagination
            count={Math.ceil(books.length / 10)}
            onChange={handlePageChange}
            className="pagination"
          ></Pagination>
          {books.length === 0 ? (
            <div className="empty">
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
