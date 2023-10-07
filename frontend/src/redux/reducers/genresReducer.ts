import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import GetGenre from "../../interfaces/genres/GetGenre";
import CreateGenre from "../../interfaces/genres/CreateGenre";

interface GenresState {
  genres: GetGenre[];
  loading: boolean;
  error: AxiosError | null;
  currentGenre: GetGenre | null;
}

const initialState: GenresState = {
  genres: [],
  loading: false,
  error: null,
  currentGenre: null,
};

export const getAllGenres = createAsyncThunk(
  "genres/getAllGenres",
  async () => {
    try {
      const response = await axios.get<GetGenre[]>(
        "https://integrify-library.azurewebsites.net/api/v1/genres"
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

const genresSlice = createSlice({
  name: "genres",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getAllGenres.fulfilled, (state, action) => {
      state.genres = action.payload;
      state.loading = false;
      state.error = null;
    })
  }
});

export default genresSlice.reducer;
