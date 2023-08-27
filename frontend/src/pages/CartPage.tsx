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
    dispatch(loanBooks({bookIds, jwt_token}));
    bookIds.forEach((bookId) => dispatch(removeFromCart(bookId)));    
  }

  return (
    <>
      <Header></Header>
      {cartItems.map((item) => (
        <div key={item.bookId}>
          <h2>Book Name: {item.bookName}</h2>
          <button onClick={() => removeFromCartHandler(item.bookId)}>
            Remove from cart
          </button>
        </div>
      ))}
      <button onClick={loanBooksHandler}>Loan books</button>
    </>
  );
}

export default CartPage;
