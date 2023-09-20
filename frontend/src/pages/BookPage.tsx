import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { addToCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";

function BookPage() {
  const currentBook = useAppSelector((state) => state.books.currentBook);
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.users.currentUser);

  function addToCartHandler() {
    dispatch(addToCart(currentBook));
  }
  return (
    <>
      <Header />
      <div className="book-page">
        <div className="img-wrapper">
          <img
            src="https://m.media-amazon.com/images/I/81zlbsnFiYL._AC_UF1000,1000_QL80_.jpg"
            alt="image for a book"
          />
        </div>
        <div className="book-details">
          <h2 className="book-name">{currentBook?.bookName}</h2>
          <h3 className="book-author">{currentBook?.authorName}</h3>
          <h2>Description: {currentBook?.description}</h2>
          <h2>Page Count: {currentBook?.pageCount}</h2>
          <h2>Published Date: {currentBook?.publishedDate}</h2>
          <h2>ISBN: {currentBook?.ISBN}</h2>

        </div>
      </div>
      {currentUser?.role === "User" ? (
        <button onClick={addToCartHandler}>Add To Loan Cart</button>
      ) : null}
    </>
  );
}

export default BookPage;
