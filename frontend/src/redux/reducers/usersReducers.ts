import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import User from "../../interfaces/users/User";
import CreateUser from "../../interfaces/users/CreateUser";

interface UsersState {
  users: User[];
  loading: boolean;
  error: AxiosError | null;
  currentUser: User | null;
  isLoggedIn: boolean;
}

const initialState: UsersState = {
  users: [],
  loading: false,
  error: null,
  currentUser: null,
  isLoggedIn: false,
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

const usersSlice = createSlice({
  name: "users",
  initialState,
  reducers: {},
});

const usersReducer = usersSlice.reducer;
export default usersReducer;
