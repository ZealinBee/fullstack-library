import React from "react";
import { Link } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";

import GetBook from "../interfaces/books/GetBook";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import useAppSelector from "../redux/hooks/useAppSelector";
import Book from "./Book";
import { addToCart } from "../redux/reducers/cartReducer";
import { selectCurrentBook } from "../redux/reducers/booksReducer";
import { createReservation } from "../redux/reducers/reservationsReducer";

interface BookListCardProps {
  book: GetBook;
}

function BookListCard({ book }: BookListCardProps) {
  const dispatch = useAppDispatch();
  const isBookInCart = useAppSelector((state) =>
    state.cart.cartItems.find((item) => item.bookId === book.bookId)
  );
  const isBookReserved = useAppSelector((state) =>
    state.reservations.reservations.find(
      (reservation) => reservation.bookId === book.bookId
    )
  );
  const currentUser = useAppSelector((state) => state.users.currentUser);
  let token = useAppSelector((state) => state.users.currentToken);

  async function addToCartHandler(book: GetBook) {
    const response = await dispatch(addToCart(book));
    if (response.payload === "error") {
      toast.error("Book is already in loan cart");
    } else {
      toast.success(`Book added to loan cart`);
    }
  }

  async function reserveBookHandler() {
    const response = await dispatch(
      createReservation({ bookId: book.bookId, jwt_token: token })
    );
    if (response.type === "reservations/createReservation/fulfilled") {
      toast.success("Book reserved");
    }
  }

  return (
    <div key={book.bookId} className="bookList__book">
      <Book book={book}></Book>
      {currentUser?.role === "User" && (
        <div>
          {
            // Lotta ternary operators here, but it's just to check if the book is in the cart, reserved, or available
          }
          {isBookInCart ? (
            <button className="bookList__add-book" disabled>Already in Cart</button>
          ) : isBookReserved ? (
            <button className="bookList__add-book" disabled>Already Reserved</button>
          ) : book.quantity > 0 ? (
            <button
              onClick={() => addToCartHandler(book)}
              className="bookList__add-book"
            >
              Add to Cart
            </button>
          ) : (
            <button
              onClick={() => reserveBookHandler()}
              className="bookList__add-book"
            >
              Reserve Book
            </button>
          )}
        </div>
      )}

      {!currentUser && (
        <Link to={"/auth"}>
          <button className="bookList__add-book">Log In to Loan</button>
        </Link>
      )}
      {currentUser?.role === "Librarian" && (
        <Link
          to={`/books/${book.bookId}`}
          onClick={() => dispatch(selectCurrentBook(book))}
        >
          <button className="bookList__add-book">Manage Book</button>
        </Link>
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
    </div>
  );
}

export default BookListCard;
