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
      `${process.env.REACT_APP_FETCH_HOST}/api/v1/books`
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

// When loan is successful, update the state of the loaned books, because the quantity of the books has changed
export const getBookById = createAsyncThunk(
  "books/getBookById",
  async (bookId: string) => {
    try {
      const response = await axios.get<GetBook>(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/books/${bookId}`
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
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/books`,
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
      await axios.delete<GetBook>(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/books/${bookId}`,
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${jwt_token}`,
          },
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
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/books/${bookId}`,
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
      } else {
        state.books = state.originalBooks;
      }
    },
    sortBooks: (state, action) => {
      const sortValue = action.payload;
      if (sortValue === "Upload Date Descending") {
        state.books.sort((a, b) => {
          return (
            new Date(b.publishedDate).getTime() -
            new Date(a.publishedDate).getTime()
          );
        });
      } else if (sortValue === "Upload Date Ascending") {
        state.books.sort((a, b) => {
          return (
            new Date(a.publishedDate).getTime() -
            new Date(b.publishedDate).getTime()
          )
        })
      }else if(sortValue === "Popularity Descending"){
          state.books.sort((a,b) => {
            return b.loanedTimes - a.loanedTimes
          })
      }else if(sortValue === "Popularity Ascending"){
        state.books.sort((a,b) => {
          return a.loanedTimes - b.loanedTimes
        })
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
      })
      .addCase(getBookById.fulfilled, (state, action) => {
        state.books = state.books.map((book) => {
          if (book.bookId === action.payload.bookId) {
            return action.payload;
          }
          return book;
        });
      });
  },
});

export const { selectCurrentBook, searchBooks, sortBooks } = booksSlice.actions;
export default booksSlice.reducer;
