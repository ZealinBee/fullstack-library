import React from "react";

import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";
import { removeFromCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { loanBooks } from "../redux/reducers/cartReducer";

function CartPage() {
  const cartItems = useAppSelector((state) => state.cart.cartItems);
  const dispatch = useAppDispatch();
  let jwt_token = useAppSelector((state) => state.users.currentToken);

  function removeFromCartHandler(bookId: string) {
    dispatch(removeFromCart(bookId));
  }

  function loanBooksHandler() {
    const bookIds = cartItems.map((item) => item.bookId);
    dispatch(loanBooks({ bookIds, jwt_token }));
    bookIds.forEach((bookId) => dispatch(removeFromCart(bookId)));
  }

  return (
    <>
      <Header></Header>
      <h1 className="top">Loan Cart</h1>
      <div className="loan-cart-items">
        {cartItems.map((item) => (
          <div key={item.bookId} className="loan-cart-item">
            <div className="img-wrapper">
              <img
                src="https://m.media-amazon.com/images/I/81zlbsnFiYL._AC_UF1000,1000_QL80_.jpg"
                alt=""
              />
              <div className="book-details">
              <h2>{item.bookName}</h2>
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
