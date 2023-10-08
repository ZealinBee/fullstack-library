import React from 'react'

import Header from '../components/Header'
import { Link } from 'react-router-dom'

function PaymentSuccessPage() {
  return (
    <div>
        <Header></Header>
        <h2 className="top">
            Loan successful! Go to <Link to="/loans">your loans</Link> to see your new loan.
        </h2>
    </div>
  )
}

export default PaymentSuccessPage