import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetAuthor from "../../interfaces/authors/GetAuthor";
import { create } from "domain";
import { deleteBook } from "./booksReducer";

interface AuthorsState {
  authors: GetAuthor[];
  loading: boolean;
  error: AxiosError | null;
  currentAuthor: GetAuthor | null;
}

const initialState: AuthorsState = {
  authors: [],
  loading: false,
  error: null,
  currentAuthor: null,
};

export const getAllAuthors = createAsyncThunk(
  "authors/getAllAuthors",
  async () => {
    try {
      const response = await axios.get<GetAuthor[]>(
        "http://98.71.53.99/api/v1/authors"
      );
      console.log(response.data)
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

export const deleteAuthor = createAsyncThunk(
  "authors/deleteAuthor",
  async ({
    authorId,
    jwt_token,
  }: {
    authorId: string;
    jwt_token: string | null;
  }) => {
    try {
      await axios.delete(`http://98.71.53.99/api/v1/authors/${authorId}`, {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${jwt_token}`,
        },
      });
      return authorId;
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

const authorsSlice = createSlice({
  name: "authors",
  initialState,
  reducers: {
    setCurrentAuthor: (state, action) => {
      state.currentAuthor = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(getAllAuthors.fulfilled, (state, action) => {
      state.authors = action.payload;
      state.loading = false;
    })
    .addCase(deleteBook.fulfilled, (state, action) => {
      state.authors = state.authors.filter(
        (author) => author.authorId !== action.payload
      );
      state.loading = false;
      state.error = null;
    })
  },
});

export const { setCurrentAuthor } = authorsSlice.actions;
export default authorsSlice.reducer;
