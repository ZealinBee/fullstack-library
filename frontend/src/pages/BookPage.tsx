import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";

function BookPage() {
  const currentBook = useAppSelector((state) => state.books.currentBook);
  return (
    <>
      <Header />
      <div>
        <h1>Book Page</h1>
        <h2>Book Name: {currentBook?.bookName}</h2>
        <h2>Author Name: {currentBook?.authorName}</h2>
        <h2>ISBN: {currentBook?.ISBN}</h2>
        <h2>Description: {currentBook?.description}</h2>
        <h2>Page Count: {currentBook?.pageCount}</h2>
        <h2>Published Date: {currentBook?.publishedDate}</h2>
      </div>
    </>
  );
}

export default BookPage;
