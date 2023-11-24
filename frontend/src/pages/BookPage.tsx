import React, { useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import HomeRoundedIcon from "@mui/icons-material/HomeRounded";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { addToCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { deleteBook } from "../redux/reducers/booksReducer";
import EditBook from "../components/EditBook";
import { createReservation } from "../redux/reducers/reservationsReducer";
import { current } from "@reduxjs/toolkit";

function BookPage() {
  let currentBook = useAppSelector((state) => state.books.currentBook);
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.users.currentUser);
  let token = useAppSelector((state) => state.users.currentToken);
  const isBookInCart = useAppSelector((state) =>
    state.cart.cartItems.find((item) => item.bookId === currentBook?.bookId)
  );
  const [editMode, setEditMode] = React.useState(false);
  const navigate = useNavigate();
  const isReserved = useAppSelector((state) =>
    state.reservations.reservations.find(
      (reservation) => reservation.bookId === currentBook?.bookId
    )
  );

  function addToCartHandler() {
    dispatch(addToCart(currentBook));
    toast.success("Book added to loan cart");
  }

  function deleteBookHandler(bookId: string | undefined) {
    if (!bookId) return;
    const isConfirmed = window.confirm(
      `Are you sure you want to delete ${currentBook?.bookName}?`
    );
    if (!isConfirmed) {
      return;
    }
    dispatch(deleteBook({ bookId: bookId, jwt_token: token }));
  }

  async function reserveBookHandler() {
    const response = await dispatch(
      createReservation({ bookId: currentBook?.bookId, jwt_token: token })
    );
    if (response.type === "reservations/createReservation/fulfilled") {
      toast.success("Book reserved");
    }
  }

  useEffect(() => {
    if (currentBook === null) {
      navigate("/");
    }
  }, []);

  return (
    <>
      <Header />
      <Link to="/" >
        {" "}
        <button className="back-button">
          Home
        </button>
      </Link>
      {currentBook && (
        <div className="book-page">
          <div className="book-page__img-wrapper">
            <img src={currentBook.bookImage} alt="book" />
            {currentUser?.role === "User" &&
              (!isBookInCart ? (
                currentBook.quantity > 0 ? (
                  <button onClick={addToCartHandler}>Add To Loan Cart</button>
                ) : isReserved ? (
                  <button className="bookList__add-book reserve" disabled>
                    Already Reserved
                  </button>
                ) : (
                  <>
                    <button
                      className="bookList__add-book reserve"
                      onClick={reserveBookHandler}
                    >
                      No Copies Left, Click to Reserve
                    </button>
                  </>
                )
              ) : (
                <button disabled className="bookList__add-book">
                  Already in Cart
                </button>
              ))}
            {!currentUser && (
              <Link to="/auth">
                <button className="bookList__add-book">Log In to loan</button>
              </Link>
            )}
          </div>
          <div className="book-page__book-details">
            <h2 className="book-page__book-name">{currentBook.bookName}</h2>
            <h3 className="book-page__book-author">{currentBook.authorName}</h3>
            <p className="book-page__book-description">
              {currentBook.description}
            </p>
            <p>isbn: {currentBook.isbn}</p>
            <p>
              {currentBook.pageCount} pages, first uploaded to the website on{" "}
              {currentBook.publishedDate}
            </p>
            <p>
              loaned by others{" "}
              <span className="bold">{currentBook.loanedTimes} </span>times{" "}
            </p>
            <p>
              {" "}
              <span className="bold"> {currentBook.quantity}</span> copies
              available
            </p>
            {currentUser?.role === "Librarian" ? (
              <>
                <Link to="/">
                  <button
                    onClick={() => deleteBookHandler(currentBook?.bookId)}
                  >
                    Delete
                  </button>
                </Link>
                <button
                  style={{ marginLeft: "0.5rem" }}
                  onClick={() => setEditMode(!editMode)}
                >
                  Edit
                </button>
              </>
            ) : null}
            {editMode ? <EditBook setEditMode={setEditMode} /> : null}
          </div>
        </div>
      )}
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
    </>
  );
}

export default BookPage;
