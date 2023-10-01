import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import Book from "../components/Book";

function LoanPage() {
  const currentLoan = useAppSelector((state) => state.loans.currentLoan);
  console.log(currentLoan);
  return (
    <>
      <Header></Header>
      <h1 className="top">Loan Details</h1>
      {currentLoan?.loanDetails.map((loanDetail) => {
        // return (
        //   <Book
        //     key={loanDetail.book.bookId}
        //     book={loanDetail.book}
        //   ></Book>
        // );
      })}
    </>
  );
}

export default LoanPage;
