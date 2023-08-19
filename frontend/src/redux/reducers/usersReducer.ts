import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import User from "../../interfaces/users/User";
import CreateUser from "../../interfaces/users/CreateUser";
import LoginUser from "../../interfaces/users/LoginUser";
import GetUser from "../../interfaces/users/GetUser";

interface UsersState {
  users: GetUser[];
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
          },
        }
      );
      console.log(response.data);
      return response.data;
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

export const authenticateUser = createAsyncThunk(
  "users/authenticateUser",
  async (jwt_token: string) => {}
);

export const getAllUsers = createAsyncThunk(
  "users/getAllUsers",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.get<GetUser[]>(
        "http://localhost:5043/api/v1/users",
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwt_token}`,
          },
        }
      );
      console.log(response.data);
      return response.data;
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
    .addCase(getAllUsers.fulfilled, (state, action) => {
      return {
        ...state,
        users: action.payload, 
      }
    })
  }
});

const usersReducer = usersSlice.reducer;
export default usersReducer;
