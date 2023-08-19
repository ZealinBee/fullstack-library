import React from "react";

import { Link } from "react-router-dom";

function Header() {
  return (
    <div>
      <Link to={"/"}>Home</Link> |<Link to={"/auth"}> Auth </Link>|
      <Link to={"/dashboard"}> Dashboard </Link>
    </div>
  );
}

export default Header;
