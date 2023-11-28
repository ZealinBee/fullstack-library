import React, { useEffect } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllBooks } from "../redux/reducers/booksReducer";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetBook from "../interfaces/books/GetBook";
import { BeatLoader } from "react-spinners";
import Footer from "./Footer";
import { Pagination } from "@mui/material";
import { usePaginate } from "../utils/usePaginate";
import BookListCard from "./BookListCard";
import { sortBooks } from "../redux/reducers/booksReducer";

function BookList() {
  const dispatch = useAppDispatch();
  const books = useAppSelector((state) => state.books.books);
  const hasFetched = useAppSelector((state) => state.books.hasFetched);
  const { handlePageChange, paginatedItems } = usePaginate({
    items: books,
    booksPerPage: 10,
  });

  useEffect(() => {
    if (hasFetched === false) {
      dispatch(getAllBooks());
    } else if (hasFetched === true) {
      dispatch(sortBooks("Upload Date Descending"));
    }
  }, [dispatch, hasFetched]);

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
                <BookListCard book={book} key={book.bookId}></BookListCard>
              );
            })}
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
