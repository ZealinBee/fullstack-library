import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import Book from "../components/Book";

function LoanPage() {
  const currentLoan = useAppSelector((state) => state.loans.currentLoan);
  const books = useAppSelector((state) => state.books.books);
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
          {/* <h3>Return date: {currentLoan?.dueDate}</h3> */}
        </div>
      </div>
    </>
  );
}

export default LoanPage;
