import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetLoan from "../../interfaces/loans/GetLoan";

interface LoansState {
  loans: GetLoan[];
  loading: boolean;
  error: AxiosError | null;
  currentLoan: GetLoan | null;
}

const initialState: LoansState = {
  loans: [],
  loading: false,
  error: null,
  currentLoan: null,
};

export const getAllLoans = createAsyncThunk(
  "loans/getAllLoans",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.get("http://98.71.53.99/api/v1/loans", {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${jwt_token}`,
        },
      });
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

export const getOwnLoans = createAsyncThunk(
  "loans/getOwnLoans",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.get(
        "http://98.71.53.99/api/v1/loans/own-loans",
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

const loansSlice = createSlice({
  name: "loans",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(getAllLoans.fulfilled, (state, action) => {
        state.loans = action.payload;
        state.loading = false;
      })
      .addCase(getOwnLoans.fulfilled, (state, action) => {
        state.loans = action.payload;
        state.loading = false;
      });
  },
});

export default loansSlice.reducer;
