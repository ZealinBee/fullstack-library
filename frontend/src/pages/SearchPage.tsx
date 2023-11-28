import React from "react";
import { Link } from "react-router-dom";
import { Pagination } from "@mui/material";

import Header from "../components/Header";
import Search from "../components/Search";
import useAppSelector from "../redux/hooks/useAppSelector";
import BookListCard from "../components/BookListCard";
import GetBook from "../interfaces/books/GetBook";
import { usePaginate } from "../utils/usePaginate";

function SearchPage() {
  const searchedBooks = useAppSelector((state) => state.books.searchedBooks) as GetBook[]; 
  const { handlePageChange, paginatedItems } = usePaginate({
    items: searchedBooks,
    booksPerPage: 10,
  });

  return (
    <div>
      <Header />
      <Search />
      <h1 className="top">Search Results</h1>
      {searchedBooks.length === 0 ? (
        <div className="empty">
          <h2>No books found</h2>
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
            count={Math.ceil(searchedBooks.length / 10)}
            onChange={handlePageChange}
            className="pagination"
          ></Pagination>
        </>
      )}
    </div>
  );
}

export default SearchPage;
