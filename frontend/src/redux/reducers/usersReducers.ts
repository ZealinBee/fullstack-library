import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import User from "../../interfaces/users/User";
import CreateUser from "../../interfaces/users/CreateUser";
import LoginUser from "../../interfaces/users/LoginUser";

interface UsersState {
  users: User[];
  loading: boolean;
  error: AxiosError | null;
  currentUser: User | null;
  isLoggedIn: boolean;
  currentToken: string | null;
}

const initialState: UsersState = {
  users: [],
  loading: false,
  error: null,
  currentUser: null,
  isLoggedIn: false,
  currentToken: null,
};

export const createNewUser = createAsyncThunk(
  "users/createNewUser",
  async (user: CreateUser) => {
    try {
      const result = await axios.post(
        "http://localhost:5043/api/v1/users",
        user,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );
      return result.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const responseData = error.response?.data;
        const warningMessage = responseData.message;
        throw new Error(warningMessage);
      } else {
        console.error(error);
        throw error;
      }
    }
  }
);

export const loginUser = createAsyncThunk(
  "users/loginUser",
  async (user: LoginUser) => {
    try {
      const response = await axios.post<string>(
        "http://localhost:5043/api/v1/auth",
        user,
        {
          headers: {
            "Content-Type": "application/json",
          }
        }
      );
      return response.data;
    }catch (error) {
      if (axios.isAxiosError(error)) {
        const responseData = error.response?.data;
        const warningMessage = responseData.message;
        throw new Error(warningMessage);
      } else {
        console.error(error);
        throw error;
      }
    }
  }
)

export const authenticateUser = createAsyncThunk(
  "users/authenticateUser",
  async (jwt_token: string) => {

  }
)

const usersSlice = createSlice({
  name: "users",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(loginUser.fulfilled, (state, action) => {
      return {
        ...state,
        isLoggedIn: true,
        currentToken: action.payload,
      }
    })
  }
});

const usersReducer = usersSlice.reducer;
export default usersReducer;
