import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import Book from "../components/Book";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { returnLoan } from "../redux/reducers/loansReducer";
import { ToastContainer, toast } from "react-toastify";

function LoanPage() {
  const currentLoan = useAppSelector((state) => state.loans.currentLoan);
  const books = useAppSelector((state) => state.books.books);
  const dispatch = useAppDispatch();
  const token = useAppSelector((state) => state.users.currentToken);

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
          <h3>Loan date: {currentLoan?.loanDate}</h3>
          <h3>Due date: {currentLoan?.dueDate}</h3>
          <h3>
            Returned date:{" "}
            {currentLoan?.returnedDate ? (
              currentLoan?.returnedDate
            ) : (
              <span>not returned</span>
            )}
          </h3>
          <button onClick={returnLoanHandler}>Return Loan</button>
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
