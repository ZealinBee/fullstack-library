import React, { useState } from "react";

import CreateUser from "../interfaces/users/CreateUser";
import useAppDispatch  from "../redux/hooks/useAppDispatch";
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
    <div>
      <h1>Sign Up</h1>
      <form onSubmit={createAccountHandler}>
        <input
          type="text"
          onChange={formChangeHandler}
          name="firstName"
          value={user.firstName}
          placeholder="First Name"
          required
        />
       <input
          type="text"
          onChange={formChangeHandler}
          name="lastName"
          value={user.lastName}
          placeholder="Last Name"
          required
        />
       <input
          type="email"
          onChange={formChangeHandler}
          name="email"
          value={user.email}
          placeholder="Email"
          required
        />
       <input
          type="password"
          onChange={formChangeHandler}
          name="password"
          value={user.password}
          placeholder="Password"
          required
        />
        <button type="submit">Create Account</button>
      </form>
    </div>
  );
}

export default SignUp;
