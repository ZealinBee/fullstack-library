import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import {
  CardElement,
  IdealBankElement,
  PaymentElement,
} from "@stripe/react-stripe-js";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import { BeatLoader } from "react-spinners";

import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";
import { removeFromCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { loanBooks } from "../redux/reducers/cartReducer";
import { selectCurrentBook } from "../redux/reducers/booksReducer";
import CheckoutForm from "../components/CheckoutForm";

function CartPage() {
  const cartItems = useAppSelector((state) => state.cart.cartItems);
  const dispatch = useAppDispatch();
  let jwt_token = useAppSelector((state) => state.users.currentToken);
  let loading = useAppSelector((state) => state.cart.loading);

  const stripePromise = loadStripe(
    "pk_test_51NygIWCuxdYH5UkGTReTgNsbWCtEeYZKWa3954W7hjOfHCszJKxhStoNuyFp4RJi85aoZOB6QNqSqGu4zlunQIaA000Pe9AIdy"
  );

  const options = {
    mode: "payment" as "payment",
    amount: 1099,
    currency: "usd",
  };

  function removeFromCartHandler(bookId: string) {
    dispatch(removeFromCart(bookId));
  }

  async function loanBooksHandler() {
    const bookIds = cartItems.map((item) => item.bookId);
    if (bookIds.length === 0) {
      toast.error("Cart is empty");
      return;
    }
    const response = await dispatch(loanBooks({ bookIds, jwt_token }));
    console.log(response);
    if (response.type === "cart/loanBooks/fulfilled") {
      toast.success("Books loaned");
    }
  }
  return (
    <>
      <Header></Header>
      <h1 className="top">Loan Cart</h1>
      {cartItems.length === 0 ? (
        <>
          <div className="empty">
            <h2>Cart is empty</h2>
            <Link to="/">
              <button>Back to Home</button>{" "}
            </Link>
          </div>
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
      ) : (
        <>
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
          <div className="check-out">
            <h4>Cost per book: 1$</h4>
            <h4>Total cost: {cartItems.length}$</h4>
            <h4>The Stripe is on Test mode, it will not take any money</h4>
          </div>
          <Elements stripe={stripePromise} options={options}>
            <CheckoutForm></CheckoutForm>
          </Elements>
          <div className="image-wrapper">
            <img
              src="https://www.eposhybrid.uk/ihybridnew//upload/ck/2035148938.png"
              alt="powered by stripe"
              className="stripe-image"
            />
          </div>
          <div className="loan-books-immediate">
            <h4>
              If you don't want to test the Stripe functionalities, you can just
              immediately loan here
            </h4>
            <button onClick={loanBooksHandler} className="loan-books-button">
              Loan books
              {loading ? (
                <BeatLoader
                  color="white"
                  size="5"
                  cssOverride={{
                    marginLeft: "0.4rem",
                  }}
                ></BeatLoader>
              ) : null}
            </button>
          </div>
        </>
      )}
    </>
  );
}

export default CartPage;
