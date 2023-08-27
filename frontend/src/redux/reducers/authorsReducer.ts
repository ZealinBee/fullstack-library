import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetAuthor from "../../interfaces/authors/GetAuthor";

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
            "http://localhost:5043/api/v1/authors"
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
)

const authorsSlice = createSlice({
  name: "authors",
  initialState,
  reducers: {
    setCurrentAuthor: (state, action) => {
        state.currentAuthor = action.payload;
    }
  },
  extraReducers: (builder) => {
    builder.addCase(getAllAuthors.fulfilled, (state, action) => {
        state.authors = action.payload;
        state.loading = false;
    })
  }
});

export const { setCurrentAuthor } = authorsSlice.actions;
export default authorsSlice.reducer;
