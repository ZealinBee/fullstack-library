import React, {useState} from "react";

import SignUp from "../components/SignUp";
import Login from "../components/Login";
import Header from "../components/Header";

function AuthPage() {
  const [isSignup, setIsSignup] = useState(true);
  return (
    <div>
      <Header></Header>
      <div className="auth">
        <SignUp></SignUp>
        <Login></Login>
      </div>
    </div>
  );
}

export default AuthPage;
