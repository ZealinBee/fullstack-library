import { PaymentElement } from "@stripe/react-stripe-js";
import React from "react";
import { useStripe, useElements } from "@stripe/react-stripe-js";
import { ToastContainer, toast } from "react-toastify";

function CheckoutForm() {
  const stripe = useStripe();
  const elements = useElements();

  async function checkoutHandler(e: React.FormEvent<HTMLFormElement>) {
    e.preventDefault();
    if (!stripe) return;
    if (!elements) return;
    const { error: submitError } = await elements.submit();
    if (submitError) return;
    const response = await fetch(
      "https://integrify-library.azurewebsites.net/api/v1/create-intent",
      {
        method: "POST",
      }
    );

    const { client_secret: clientSecret } = await response.json();
    const { error } = await stripe.confirmPayment({
      elements,
      clientSecret,
      confirmParams: {
        return_url: "https://integrify-library.netlify.app/payment/success",
      },
    });
    if (error) {
      toast.error("Payment failed");
    }else {
      toast.success("Payment successful");
    }
  }
  return (
    <>
      <form className="check-out-form" onSubmit={checkoutHandler}>
        <PaymentElement />
        <button>Submit</button>
      </form>
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

export default CheckoutForm;
