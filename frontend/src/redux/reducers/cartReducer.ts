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

const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    addToCart: (state, action) => {
      const book = action.payload;
      const existingBook = state.cartItems.find((item) => item.bookId === book.bookId);
      if (existingBook) {
        return;
      } else {
        state.cartItems.push({ ...book });
      }
    },
    removeFromCart: (state, action) => {
      const bookToRemove = action.payload;
      const existingBook = state.cartItems.find((item) => item.bookId === bookToRemove);
      if (existingBook) {
        state.cartItems = state.cartItems.filter((item) => item.bookId !== bookToRemove);
      }else {
        return;
      }
    }
  },
});

export const { addToCart, removeFromCart } = cartSlice.actions;
export default cartSlice.reducer;
