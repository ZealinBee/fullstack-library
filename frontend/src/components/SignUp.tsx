import React, { useState } from "react";

import CreateUser from "../interfaces/users/CreateUser";
import useAppDispatch  from "../redux/hooks/useAppDispatch";
import { createNewUser } from "../redux/reducers/usersReducers";

function Login() {
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
    dispatch(createNewUser(user));
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
      <h1>Sign Up</h1>
      <form onSubmit={createAccountHandler}>
        <input
          type="text"
          onChange={formChangeHandler}
          name="firstName"
          value={user.firstName}
        />
       <input
          type="text"
          onChange={formChangeHandler}
          name="lastName"
          value={user.lastName}
        />
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
        <button type="submit">Create Account</button>
      </form>
    </div>
  );
}

export default Login;
