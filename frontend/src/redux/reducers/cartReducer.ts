import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetBook from "../../interfaces/books/GetBook";

interface CartState {
  cartItems: GetBook[];
  loading: boolean;
  error: AxiosError | null;
}

const initialState: CartState = {
  cartItems: [],
  loading: false,
  error: null,
};

export const loanBooks = createAsyncThunk(
  "cart/loanBooks",
  async ({ bookIds, jwt_token }: { bookIds: string[]; jwt_token: string | null }) => {
    try {
      const dateOnlyString = new Date().toISOString().slice(0, 10);
      const loanData = {
        bookIds: bookIds,
        loanDate: dateOnlyString,
      };
      const response = await axios.post(
        "http://98.71.53.99/api/v1/loans",
        loanData,
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
        return;
      } else {
        state.cartItems.push({ ...book });
      }
    },
    removeFromCart: (state, action) => {
      const bookToRemove = action.payload;
      const existingBook = state.cartItems.find(
        (item) => item.bookId === bookToRemove
      );
      if (existingBook) {
        state.cartItems = state.cartItems.filter(
          (item) => item.bookId !== bookToRemove
        );
      } else {
        return;
      }
    },
  },
});

export const { addToCart, removeFromCart } = cartSlice.actions;
export default cartSlice.reducer;
