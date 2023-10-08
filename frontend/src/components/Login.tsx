import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import { BeatLoader } from "react-spinners";

import LoginUser from "../interfaces/users/LoginUser";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { loginUser } from "../redux/reducers/usersReducer";
import useAppSelector from "../redux/hooks/useAppSelector";

function Login({
  isSignUp,
  setIsSignUp,
  toggle,
}: {
  isSignUp: boolean;
  setIsSignUp: React.Dispatch<React.SetStateAction<boolean>>;
  toggle: () => void;
}) {
  const dispatch = useAppDispatch();
  const [user, setUser] = useState<LoginUser>({
    email: "",
    password: "",
  });
  const navigate = useNavigate();
  let loading = useAppSelector((state) => state.users.loading);

  async function createAccountHandler(event: React.FormEvent) {
    event.preventDefault();
    const response = await dispatch(loginUser(user));
    if (response.meta.requestStatus === "fulfilled") {
      toast.success("Logged in");
      setTimeout(() => {
        navigate("/");
      }, 500);
    } else {
      toast.error("Invalid credentials");
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
    <div className={"login" + (isSignUp ? " toggle-off" : "")}>
      <div className={"auth-wrapper"}>
        <div className="auth-description login-description">
          <h2>Don't have an account yet?</h2>
          <p>Create an account to start loaning books in our library app</p>
          <button onClick={() => toggle()}>Sign up</button>
        </div>
        <form onSubmit={createAccountHandler} className="auth-form">
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
            Login{" "}
            {loading ? (
              <BeatLoader 
                color="white"
                size="5"
                cssOverride={{
                  marginLeft: "0.4rem",
                }}
              ></BeatLoader>
            ) : null}
          </button>
        </form>
      </div>
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
    </div>
  );
}

export default Login;
