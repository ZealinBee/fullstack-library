import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

import LoginUser from "../interfaces/users/LoginUser";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { loginUser } from "../redux/reducers/usersReducer";

function Login() {
  const dispatch = useAppDispatch();
  const [user, setUser] = useState<LoginUser>({
    email: "",
    password: "",
  });
  const navigate = useNavigate();

  async function createAccountHandler(event: React.FormEvent) {
    event.preventDefault();
    console.log(user);
    const response = await dispatch(loginUser(user));
    if (response.meta.requestStatus === "fulfilled") {
      navigate("/");
    }
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
    <div className="login">
      <div className="auth-wrapper">
        <div className="auth-description login-description">
          <h2>Don't have an account yet?</h2>
          <p>Create an account to start loaning books in our library app</p>
          <button>Sign up</button>
        </div>
        <form onSubmit={createAccountHandler}>
          <h1>Login</h1>
          <h3>Log in to your account to use our library</h3>
          <label htmlFor="email-signup">Email</label>
          <input
            type="email"
            onChange={formChangeHandler}
            name="email"
            value={user.email}
            placeholder="Email"
            id="email-signup"
          />
          <label htmlFor="password-login">Password</label>
          <input
            type="password"
            onChange={formChangeHandler}
            name="password"
            value={user.password}
            placeholder="Password"
            id="password-login"
          />
          <button type="submit" className="auth-button">
            Login
          </button>
        </form>
      </div>
    </div>
  );
}

export default Login;
