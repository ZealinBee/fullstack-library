import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import Book from "../components/Book";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { returnLoan } from "../redux/reducers/loansReducer";
import { ToastContainer, toast } from "react-toastify";
import { current } from "@reduxjs/toolkit";

function LoanPage() {
  const currentLoan = useAppSelector((state) => state.loans.currentLoan);
  const books = useAppSelector((state) => state.books.books);
  const dispatch = useAppDispatch();
  const token = useAppSelector((state) => state.users.currentToken);
  const currentUser = useAppSelector((state) => state.users.currentUser);

  async function returnLoanHandler() {
    if (currentLoan) {
      const response = await dispatch(
        returnLoan({
          loanId: currentLoan.loanId,
          jwt_token: token,
        })
      );
      if (response.meta.requestStatus === "fulfilled") {
        toast.success("Loan returned successfully");
      } else {
        toast.error("Error returning loan");
      }
    }
  }

  console.log(currentLoan);
  return (
    <>
      <Header></Header>
      <h1 className="top">Loan Details</h1>
      <div className="loan-page">
        <div className="books-wrapper">
          {currentLoan?.loanDetails.map((loanDetail) => {
            const book = books.find(
              (book) => book.bookId === loanDetail.bookId
            )!;
            return <Book book={book}></Book>;
          })}
        </div>
        <div className="loan-details">
          {currentUser?.role === "Librarian" ? (
            <h3>User: {currentLoan?.userId}</h3>
          ) : null}
          <h3>Loan date: {currentLoan?.loanDate}</h3>
          <h3>Due date: {currentLoan?.dueDate}</h3>
          <h3>
            {
              /// "0001-01-01" is the default value for returnedDate}
            }
            Returned date:{" "}
            {currentLoan?.returnedDate === "0001-01-01" ? (
              <span>not returned</span>
            ) : (
              currentLoan?.returnedDate
            )}
          </h3>
          {currentUser?.role ===
          "Librarian" ? null : currentLoan?.returnedDate ===
            "0001-01-01" ?             <button onClick={returnLoanHandler}>Return Loan</button>
            : null}
        </div>
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
  );
}

export default LoanPage;
