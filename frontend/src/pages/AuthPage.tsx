import React, {useState} from "react";

import SignUp from "../components/SignUp";
import Login from "../components/Login";
import Header from "../components/Header";

function AuthPage() {
  const [isSignup, setIsSignup] = useState(true);

  function toggle() {
    setIsSignup((prevState) => !prevState);
  }
  return (
    <div>
      <Header></Header>
      <div className="auth">
        <SignUp isSignUp={isSignup} setIsSignUp={setIsSignup} toggle={toggle}></SignUp>
        <Login isSignUp={isSignup} setIsSignUp={setIsSignup} toggle={toggle}></Login>
      </div>
    </div>
  );
}

export default AuthPage;
