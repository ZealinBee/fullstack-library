import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import { GetReservation } from "../../interfaces/reservations/GetReservation";

interface ReservationsState {
  reservations: GetReservation[];
  loading: boolean;
  error: AxiosError | null;
}

const initialState: ReservationsState = {
  reservations: [],
  loading: false,
  error: null,
};

export const getOwnReservations = createAsyncThunk(
  "reservations/getOwnReservations",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.get(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/reservations/own-reservations`,
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

export const createReservation = createAsyncThunk(
  "reservations/createReservation",
  async ({
    jwt_token,
    bookId,
  }: {
    jwt_token: string | null;
    bookId: string | undefined;
  }) => {
    try {
      const response = await axios.post(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/reservations`,
      {bookId},
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

const reservationsSlice = createSlice({
  name: "reservations",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getOwnReservations.fulfilled, (state, action) => {
      state.loading = false;
      state.error = null;
      state.reservations = action.payload;
    });
    builder.addCase(createReservation.fulfilled, (state, action) => {
      state.loading = false;
      state.error = null;
      state.reservations.push(action.payload);
    });
  },
});

export default reservationsSlice.reducer;
