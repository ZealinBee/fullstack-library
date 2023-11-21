import { useState } from "react";
import GetBook from "../interfaces/books/GetBook";

interface PaginateProps {
  items: GetBook[];
  booksPerPage: number;
}

export const usePaginate = ({ items, booksPerPage }: PaginateProps) => {
  const [currentPage, setCurrentPage] = useState(1);
  const indexOfLastBook = currentPage * booksPerPage;
  const indexOfFirstBook = indexOfLastBook - booksPerPage;
  const paginatedItems = items.slice(indexOfFirstBook, indexOfLastBook);
  
  function handlePageChange(event: React.ChangeEvent<unknown>, value: number) {
    setCurrentPage(value);
  }

  return {
    handlePageChange,
    paginatedItems
  };
};
