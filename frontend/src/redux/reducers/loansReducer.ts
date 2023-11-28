import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetLoan from "../../interfaces/loans/GetLoan";
import { getBookById } from "./booksReducer";
import LoanDetails from "../../interfaces/loans/LoanDetails";

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
      const response = await axios.get(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/loans`,
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

export const getOwnLoans = createAsyncThunk(
  "loans/getOwnLoans",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.get(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/loans/own-loans`,
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

export const getLoanById = createAsyncThunk(
  "loans/getLoanById",
  async (loanId: string) => {
    try {
      const response = await axios.get(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/loans/${loanId}`
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

export const returnLoan = createAsyncThunk(
  "loans/returnLoan",
  async (
    {
      jwt_token,
      loanId,
    }: {
      jwt_token: string | null;
      loanId: string;
    },
    { dispatch }
  ) => {
    try {
      const response = await axios.patch(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/loans/return/${loanId}`,
        null,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwt_token}`,
          },
        }
      );
      if (response.data) {
        response.data.loanDetails.forEach((loanDetail: LoanDetails) => {
          // Update the book details in the store
          dispatch(getBookById(loanDetail.bookId));
        });
      }
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
      })
      .addCase(returnLoan.fulfilled, (state, action) => {
        const updatedLoan = action.payload;
        state.currentLoan = updatedLoan;
        state.loading = false;
        state.error = null;
        state.loans = state.loans.map((loan) => {
          if (loan.loanId === updatedLoan.loanId) {
            return updatedLoan;
          }
          return loan;
        });
      });
  },
});

export const { setCurrentLoan } = loansSlice.actions;
export default loansSlice.reducer;
