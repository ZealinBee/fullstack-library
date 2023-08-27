import React from "react";
import { Link } from "react-router-dom";

import useAppSelector from "../redux/hooks/useAppSelector";

function Header() {
  const currentUser = useAppSelector((state) => state.users.currentUser);
  return (
    <div>
      <Link to={"/"}>Home </Link>
      <Link to={"/authors"}>| Authors </Link>
      {currentUser ? null : <Link to={"/auth"}>| Auth </Link>}
      {currentUser?.role === "Librarian" ? (
        <Link to="/dashboard">| Dashboard  </Link>
      ) : null}
      {currentUser ? <Link to="/profile">| Profile </Link> : null}
      {currentUser?.role === "Librarian" || !currentUser ? null : (
        <Link to="/cart">| Loan Cart</Link>
      )}
    </div>
  );
}

export default Header;
