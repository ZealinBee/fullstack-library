import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

import LoginUser from "../interfaces/users/LoginUser";
import useAppDispatch  from "../redux/hooks/useAppDispatch";
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
    const response = await dispatch(loginUser(user))
    if(response.meta.requestStatus === 'fulfilled'){
      navigate('/')
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
