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
  currentUser: GetUser | null;
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

export const getUserProfile = createAsyncThunk(
  "users/getUserProfile",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.get<GetUser>(
        "http://localhost:5043/api/v1/users/profile",
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwt_token}`,
          },
        }
      );
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

export const deleteUser = createAsyncThunk(
  "users/deleteUser",
  async ({
    userId,
    jwt_token,
  }: {
    userId: string;
    jwt_token: string | null;
  }) => {
    try {
      await axios.delete(`http://localhost:5043/api/v1/users/${userId}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${jwt_token}`,
        },
      });
      return userId;
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

export const deleteProfile = createAsyncThunk(
  "users/deleteProfile",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.delete(
        `http://localhost:5043/api/v1/users/profile`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwt_token}`,
          },
        }
      );
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
    builder
      .addCase(loginUser.fulfilled, (state, action) => {
        return {
          ...state,
          isLoggedIn: true,
          currentToken: action.payload,
        };
      })
      .addCase(getAllUsers.fulfilled, (state, action) => {
        return {
          ...state,
          users: action.payload,
        };
      })
      .addCase(deleteUser.fulfilled, (state, action) => {
        return {
          ...state,
          users: state.users.filter((user) => user.userId !== action.payload),
        };
      })
      .addCase(getUserProfile.fulfilled, (state, action) => {
        return {
          ...state,
          currentUser: action.payload,
        };
      })
      .addCase(deleteProfile.fulfilled, (state, action) => {
        return {
          ...state,
          currentUser: null,
          isLoggedIn: false,
          currentToken: null,
        };
      });
  },
});

const usersReducer = usersSlice.reducer;
export default usersReducer;
