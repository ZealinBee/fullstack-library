import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk, current } from "@reduxjs/toolkit";

import GetBook from "../../interfaces/books/GetBook";
import CreateBook from "../../interfaces/books/CreateBook";

interface BooksState {
  books: GetBook[];
  loading: boolean;
  error: AxiosError | null;
  currentBook: GetBook | null;
  originalBooks: GetBook[];
  hasFetched: boolean;
}

const initialState: BooksState = {
  books: [],
  loading: false,
  error: null,
  currentBook: null,
  originalBooks: [],
  hasFetched: false,
};

export const getAllBooks = createAsyncThunk("books/getAllBooks", async () => {
  try {
    const response = await axios.get<GetBook[]>(
      "https://integrify-library.azurewebsites.net/api/v1/books"
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
  async ({
    book,
    jwt_token,
  }: {
    book: CreateBook;
    jwt_token: string | null;
  }) => {
    try {
      const response = await axios.post<GetBook>(
        "https://integrify-library.azurewebsites.net/api/v1/books",
        book,
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

export const deleteBook = createAsyncThunk(
  "books/deleteBook",
  async ({
    bookId,
    jwt_token,
  }: {
    bookId: string;
    jwt_token: string | null;
  }) => {
    try {
      await axios.delete<GetBook>(`https://integrify-library.azurewebsites.net/api/v1/books/${bookId}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${jwt_token}`,
        },
      });
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
);

export const updateBook = createAsyncThunk(
  "books/updateBook",
  async ({
    bookId,
    book,
    jwt_token,
  }: {
    bookId: string | undefined;
    book: CreateBook | undefined;
    jwt_token: string | null;
  }) => {
    try {
      const response = await axios.patch<GetBook>(
        `https://integrify-library.azurewebsites.net/api/v1/books/${bookId}`,
        book,
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

const booksSlice = createSlice({
  name: "books",
  initialState,
  reducers: {
    selectCurrentBook: (state, action) => {
      state.currentBook = action.payload;
    },
    searchBooks: (state, action) => {
      const searchValue = action.payload.toLowerCase();
      if (searchValue !== "") {
        state.books = state.originalBooks;
        state.books = state.books.filter(
          (book) =>
            book.bookName.toLowerCase().includes(searchValue) ||
            book.authorName.toLowerCase().includes(searchValue) ||
            book.genreName.toLowerCase().includes(searchValue)
        );
      }else {
        state.books = state.originalBooks;
      }
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(getAllBooks.fulfilled, (state, action) => {
        state.books = action.payload;
        state.originalBooks = action.payload;
        state.loading = false;
        state.error = null;
        state.hasFetched = true;
      })
      .addCase(deleteBook.fulfilled, (state, action) => {
        state.books = state.books.filter(
          (book) => book.bookId !== action.payload
        );
        state.loading = false;
        state.error = null;
      })
      .addCase(updateBook.fulfilled, (state, action) => {
        const updatedBook = action.payload;
        state.currentBook = updatedBook;
        state.books = state.books.map((book) => {
          if (book.bookId === updatedBook.bookId) {
            return updatedBook;
          }
          return book;
        });
        state.loading = false;
        state.error = null;
      })
      .addCase(createBook.fulfilled, (state, action) => {
        state.books.push(action.payload);
        state.loading = false;
        state.error = null;
      });
  },
});

export const { selectCurrentBook, searchBooks } = booksSlice.actions;
export default booksSlice.reducer;
