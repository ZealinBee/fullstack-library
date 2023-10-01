import React from "react";
import { Link } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";

import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";
import { removeFromCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { loanBooks } from "../redux/reducers/cartReducer";
import { selectCurrentBook } from "../redux/reducers/booksReducer";

function CartPage() {
  const cartItems = useAppSelector((state) => state.cart.cartItems);
  const dispatch = useAppDispatch();
  let jwt_token = useAppSelector((state) => state.users.currentToken);

  function removeFromCartHandler(bookId: string) {
    dispatch(removeFromCart(bookId));
  }

  function loanBooksHandler() {
    const books = cartItems.map((item) => item);
    dispatch(loanBooks({ books, jwt_token }));
    books.forEach((book) => dispatch(removeFromCart(book.bookId)));
  }

  return (
    <>
      <Header></Header>
      <h1 className="top">Loan Cart</h1>
      <div className="loan-cart-items">
        {cartItems.map((item) => (
          <div key={item.bookId} className="loan-cart-item">
            <div className="img-wrapper">
              <img src={item.bookImage} alt="" />
              <div className="book-details">
                <Link to={`/books/${item.bookId}`}>
                  <h2 onClick={() => dispatch(selectCurrentBook(item))}>
                    {item.bookName}
                  </h2>
                </Link>
                <h3>{item.authorName}</h3>
              </div>
            </div>

            <button onClick={() => removeFromCartHandler(item.bookId)}>
              Remove from cart
            </button>
          </div>
        ))}
      </div>

      <button onClick={loanBooksHandler} className="loan-books-button">
        Loan books
      </button>
    </>
  );
}

export default CartPage;
