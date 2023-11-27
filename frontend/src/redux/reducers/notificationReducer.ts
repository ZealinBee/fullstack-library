import axios, { AxiosError } from "axios";
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";

import { GetNotification } from "../../interfaces/notifications/GetNotification";

interface NotificationState {
  notifications: GetNotification[];
  loading: boolean;
  error: string | null;
}

const initialState: NotificationState = {
  notifications: [],
  loading: false,
  error: null,
};

export const getOwnNotifications = createAsyncThunk(
  "notifications/getOwnNotifications",
  async (jwt_token: string | null) => {
    try {
      const response = await axios.get(
        `${process.env.REACT_APP_FETCH_HOST}/api/v1/notifications/own-notifications`,
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

const notificationSlice = createSlice({
  name: "notifications",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getOwnNotifications.fulfilled, (state, action) => {
      state.loading = false;
      state.error = null;
      state.notifications = action.payload;
    });
  }
});

export default notificationSlice.reducer;
