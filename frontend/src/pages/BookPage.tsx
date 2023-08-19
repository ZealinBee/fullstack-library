import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { addToCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";

function BookPage() {
  const currentBook = useAppSelector((state) => state.books.currentBook);
  const dispatch = useAppDispatch();

  function addToCartHandler() {
    dispatch(addToCart(currentBook));
  }
  return (
    <>
      <Header />
      <h1>Loan Cart</h1>
      <div>
        <h1>Individual Book Page</h1>
        <h2>Book Name: {currentBook?.bookName}</h2>
        <h2>Author Name: {currentBook?.authorName}</h2>
        <h2>ISBN: {currentBook?.ISBN}</h2>
        <h2>Description: {currentBook?.description}</h2>
        <h2>Page Count: {currentBook?.pageCount}</h2>
        <h2>Published Date: {currentBook?.publishedDate}</h2>
      </div>
      <button onClick={addToCartHandler}>Add To Loan Cart</button>
    </>
  );
}

export default BookPage;
