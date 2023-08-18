import React, { useState } from "react";

import LoginUser from "../interfaces/users/LoginUser";
import useAppDispatch  from "../redux/hooks/useAppDispatch";
import { loginUser } from "../redux/reducers/usersReducers";

function Login() {
  const dispatch = useAppDispatch();
  const [user, setUser] = useState<LoginUser>({
    email: "",
    password: "",
  });

  function createAccountHandler(event: React.FormEvent) {
    event.preventDefault();
    console.log(user);
    dispatch(loginUser(user))
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
    <div>
      <h1>Login</h1>
      <form onSubmit={createAccountHandler}>
       <input
          type="email"
          onChange={formChangeHandler}
          name="email"
          value={user.email}
        />
       <input
          type="password"
          onChange={formChangeHandler}
          name="password"
          value={user.password}
        />
        <button type="submit">Login</button>
      </form>
    </div>
  );
}

export default Login;
