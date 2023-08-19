import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetBook from "../../interfaces/books/GetBook";
import CreateBook from "../../interfaces/books/CreateBook";

interface BooksState {
  books: GetBook[];
  loading: boolean;
  error: AxiosError | null;
  currentBook: GetBook | null;
}

const initialState: BooksState = {
  books: [],
  loading: false,
  error: null,
  currentBook: null,
};

export const getAllBooks = createAsyncThunk("books/getAllBooks", async () => {
  try {
    const response = await axios.get<GetBook[]>(
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
  async (book: CreateBook) => {
    try {
      const response = await axios.post<CreateBook>(
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


export const deleteBook = createAsyncThunk(
  "books/deleteBook",
  async ({bookId, jwt_token} : {bookId: string, jwt_token: string | null}) => {
    try {
      await axios.delete<GetBook>(
        `http://localhost:5043/api/v1/books/${bookId}`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwt_token}`,
          }
        }
      );
      return bookId;
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
)

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
    .addCase(deleteBook.fulfilled, (state, action) => {
      state.books = state.books.filter(book => book.bookId !== action.payload);
      state.loading = false;
      state.error = null;
    })
  }
});

export default booksSlice.reducer;
