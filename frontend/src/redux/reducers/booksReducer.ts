import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import SimpleBook from "../../interfaces/books/SimpleBook";

interface BooksState {
  books: SimpleBook[];
  loading: boolean;
  error: AxiosError | null;
  currentBook: SimpleBook | null;
}

const initialState: BooksState = {
  books: [],
  loading: false,
  error: null,
  currentBook: null,
};

export const getAllBooks = createAsyncThunk("books/getAllBooks", async () => {
  try {
    const response = await axios.get<SimpleBook[]>(
      "http://localhost:5043/api/v1/books"
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
});

export const createBook = createAsyncThunk(
  "books/createBook",
  async (book: SimpleBook) => {
    try {
      const response = await axios.post<SimpleBook>(
        "http://localhost:5043/api/v1/books",
        book,
        {
          headers: {
            "Content-Type": "application/json",
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

const booksSlice = createSlice({
  name: "books",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getAllBooks.fulfilled, (state, action) => {
        state.books = action.payload;
        state.loading = false;
        state.error = null;
    })
  }
});

export default booksSlice.reducer;
