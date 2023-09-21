import React from "react";
import { Link } from "react-router-dom";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { addToCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { setCurrentAuthor } from "../redux/reducers/authorsReducer";

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
        <div className="book-page__img-wrapper">
          <img
            src={currentBook?.bookImage}
            alt="image for a book"
          />
          {currentUser?.role === "User" ? (
            <button onClick={addToCartHandler}>Add To Loan Cart</button>
          ) : null}
        </div>
        <div className="book-page__book-details">
          <h2 className="book-page__book-name">{currentBook?.bookName}</h2>
          {/* <Link to={`/authors/${currentBook?.authorName}}`}> */}{" "}
          <h3 className="book-page__book-author">{currentBook?.authorName}</h3>
          {/* </Link> */}
          <p className="book-page__book-description">{currentBook?.description}</p>
          <p>ISBN: {currentBook?.ISBN}</p>
          <p>
            {currentBook?.pageCount} pages, first published on{" "}
            {currentBook?.publishedDate}
          </p>
        </div>
      </div>
    </>
  );
}

export default BookPage;
