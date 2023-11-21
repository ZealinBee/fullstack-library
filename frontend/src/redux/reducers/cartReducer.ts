import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetBook from "../../interfaces/books/GetBook";
import LoanDetails from "../../interfaces/loans/LoanDetails";
import { getBookById } from "./booksReducer";

interface CartState {
  cartItems: GetBook[];
  loading: boolean;
  error: string | null;
}

const initialState: CartState = {
  cartItems: [],
  loading: false,
  error: null,
};

export const loanBooks = createAsyncThunk(
  "cart/loanBooks",
  async (
    {
      bookIds,
      jwt_token,
    }: {
      bookIds: string[];
      jwt_token: string | null;
    },
    { dispatch }
  ) => {
    try {
      const dateOnlyString = new Date().toISOString().slice(0, 10);
      const loanData = {
        loanDate: dateOnlyString,
        bookIds: bookIds,
      };
      const response = await axios.post(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/loans`,
        loanData,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwt_token}`,
          },
        }
      );
      response.data.loanDetails.forEach((loan: LoanDetails) => {
        dispatch(getBookById(loan.bookId));
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

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action) => {
      const book = action.payload;
      const existingBook = state.cartItems.find(
        (item) => item.bookId === book.bookId
      );
      if (existingBook) {
        state.error = "error";
      } else {
        state.error = null;
        state.cartItems.push({ ...book });
      }
    },
    removeFromCart: (state, action) => {
      const bookToRemove = action.payload;
      const existingBook = state.cartItems.find(
        (item) => item.bookId === bookToRemove
      );
      if (existingBook) {
        state.error = null;
        state.cartItems = state.cartItems.filter(
          (item) => item.bookId !== bookToRemove
        );
      }
    },
  },
  extraReducers: (builder) => {
    builder.addCase(loanBooks.pending, (state, action) => {
      state.loading = true;
    });
    builder.addCase(loanBooks.fulfilled, (state, action) => {
      state.loading = false;
      state.cartItems = [];
    });
  },
});

export const { addToCart, removeFromCart } = cartSlice.actions;
export default cartSlice.reducer;
