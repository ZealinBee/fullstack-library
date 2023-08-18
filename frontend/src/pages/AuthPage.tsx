import React from "react";

import SignUp from "../components/SignUp";
import Login from "../components/Login";
import Header from "../components/Header";

function AuthPage() {
  return (
    <div>
      <Header></Header>
      <SignUp></SignUp>
      <Login></Login>
    </div>
  );
}

export default AuthPage;
