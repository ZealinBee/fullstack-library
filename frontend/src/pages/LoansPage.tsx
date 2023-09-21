import React, { useEffect } from "react";

import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getAllLoans, getOwnLoans } from "../redux/reducers/loansReducer";

function LoansPage() {
  const loans = useAppSelector((state) => state.loans.loans);
  const dispatch = useAppDispatch();
  let jwt_token = useAppSelector((state) => state.users.currentToken);
  const currentUser = useAppSelector((state) => state.users.currentUser);

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
      <div className="loans">
        {" "}
        {loans.map((loan) => {
          return (
            <div key={loan.bookId} className="loan">
              {/* Add a unique key for each element in the map */}
              <h2>Book ID: {loan.bookId}</h2>
              <h3>Loan Date: {loan.loanDate}</h3>
              <h3>Due Date: {loan.dueDate}</h3>
            </div>
          );
        })}
      </div>
    </div>
  );
}

export default LoansPage;
