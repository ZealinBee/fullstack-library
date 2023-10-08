import React, { useEffect } from "react";
import { Link } from "react-router-dom";

import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllLoans, getOwnLoans } from "../redux/reducers/loansReducer";
import { setCurrentLoan } from "../redux/reducers/loansReducer";
import Book from "../components/Book";

function LoansPage() {
  const loans = useAppSelector((state) => state.loans.loans);
  const dispatch = useAppDispatch();
  let jwt_token = useAppSelector((state) => state.users.currentToken);
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const books = useAppSelector((state) => state.books.books);

  useEffect(() => {
    if (currentUser?.role === "Librarian") {
      dispatch(getAllLoans(jwt_token));
    } else {
      dispatch(getOwnLoans(jwt_token));
    }
  }, []);

  return (
    <div>
      <Header />
      {currentUser?.role === "Librarian" ? (
        <div>
          <h1 className="top">All Loans</h1>
        </div>
      ) : (
        <div>
          <h1 className="top">My Loans</h1>
        </div>
      )}
      <div className="loans-page">
        <div className="active-loans">
          <h2>Active Loans</h2>
          {loans.map((loan) => {
            if (loan.returnedDate) return null;
            return (
              <div key={loan.bookId} className="loan">
                <div className="books-wrapper">
                  {loan.loanDetails.map((loanDetail) => {
                    const book = books.find(
                      (book) => book.bookId === loanDetail.bookId
                    )!;
                    return (
                      <>
                        <Book book={book}></Book>
                      </>
                    );
                  })}
                </div>

                <Link
                  to={`/loans/${loan.loanId}`}
                  onClick={() => dispatch(setCurrentLoan(loan))}
                >
                  <button>View Details</button>
                </Link>
              </div>
            );
          })}
        </div>
        <div className="returned-loans">
          <h2>Returned Loans</h2>
          {loans.map((loan) => {
            if (!loan.returnedDate) return null;
            return (
              <div key={loan.bookId} className="loan">
                <div className="books-wrapper">
                  {loan.loanDetails.map((loanDetail) => {
                    const book = books.find(
                      (book) => book.bookId === loanDetail.bookId
                    )!;
                    return (
                      <>
                        <Book book={book}></Book>
                      </>
                    );
                  })}
                </div>

                <Link
                  to={`/loans/${loan.loanId}`}
                  onClick={() => dispatch(setCurrentLoan(loan))}
                >
                  <button>View Details</button>
                </Link>
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}

export default LoansPage;
