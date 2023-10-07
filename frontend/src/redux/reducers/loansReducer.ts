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
      const response = await axios.get("https://integrify-library.azurewebsites.net/api/v1/loans", {
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
        "https://integrify-library.azurewebsites.net/api/v1/own-loans",
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
  reducers: {
    setCurrentLoan(state, action) {
      state.currentLoan = action.payload;
    },
  },
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

export const { setCurrentLoan } = loansSlice.actions;
export default loansSlice.reducer;
