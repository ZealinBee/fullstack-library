import React from "react";

import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";
import { removeFromCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";

function CartPage() {
  const cartItems = useAppSelector((state) => state.cart.cartItems);
  const dispatch = useAppDispatch();

  function removeFromCartHandler(bookId: string) {
    dispatch(removeFromCart(bookId));
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
    </>
  );
}

export default CartPage;
