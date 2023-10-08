import { PaymentElement } from '@stripe/react-stripe-js'
import React from 'react'
import { useStripe } from '@stripe/react-stripe-js'

function CheckoutForm() {
  const stripe = useStripe()
    function checkoutHandler(e : React.FormEvent<HTMLFormElement>) {
        e.preventDefault()
        if(!stripe) return
    }
  return (
    <form className="check-out-form" onSubmit={checkoutHandler}>
        <PaymentElement />
        <button>Submit</button>
    </form>
  )
}

export default CheckoutForm