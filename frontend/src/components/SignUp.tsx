import React, { useState } from "react";
import { Link } from "react-router-dom";

import CreateUser from "../interfaces/users/CreateUser";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { createNewUser } from "../redux/reducers/usersReducer";

function SignUp() {
  const dispatch = useAppDispatch();
  const [user, setUser] = useState<CreateUser>({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
  });

  function createAccountHandler(event: React.FormEvent) {
    event.preventDefault();
    console.log(user);
    const response = dispatch(createNewUser(user));
    console.log(response);
  }

  function formChangeHandler(event: React.ChangeEvent<HTMLInputElement>) {
    setUser((prevState) => {
      return {
        ...prevState,
        [event.target.name]: event.target.value,
      };
    });
  }

  return (
    <div className="sign-up">
      <div className="auth-wrapper">
        <form onSubmit={createAccountHandler}>
        <h1>Sign Up</h1>
        <h3>Make a new account to use our library</h3>
        <label htmlFor="firstName">First Name</label>
          <input
            type="text"
            onChange={formChangeHandler}
            name="firstName"
            value={user.firstName}
            placeholder="First Name"
            required
            id="firstName"
          />
          <label htmlFor="lastName">Last Name</label>
          <input
            type="text"
            onChange={formChangeHandler}
            name="lastName"
            value={user.lastName}
            placeholder="Last Name"
            required
            id="lastName"
          />
          <label htmlFor="email-login">Email</label>
          <input
            type="email"
            onChange={formChangeHandler}
            name="email"
            value={user.email}
            placeholder="Email"
            required
            id="email-login"
          />
          <label htmlFor="password-signup">Password</label>
          <input
            type="password"
            onChange={formChangeHandler}
            name="password"
            value={user.password}
            placeholder="Password"
            required
            id="password-signup"
          />
          <button type="submit" className="auth-button">Create Account</button>
        </form>
        <div className="auth-description"> 
          <h2>Already have an account?</h2>
          <p>To loan and view your personal loans, please login</p>
          <button>Login</button>
        </div>
      </div>
    </div>
  );
}

export default SignUp;
