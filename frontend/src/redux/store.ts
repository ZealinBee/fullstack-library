import { configureStore } from "@reduxjs/toolkit";

import usersReducer from "./reducers/usersReducer";
import booksReducer from "./reducers/booksReducer";
import cartReducer from "./reducers/cartReducer";
import authorsReducer from "./reducers/authorsReducer";
import loansReducer from "./reducers/loansReducer";
import genresReducer from "./reducers/genresReducer";
import reservationsReducer from "./reducers/reservationsReducer";
import notificationReducer from "./reducers/notificationReducer";

const store = configureStore({
  reducer: {
    users: usersReducer,
    books: booksReducer,
    cart: cartReducer,
    authors: authorsReducer,
    loans: loansReducer,
    genres: genresReducer,
    reservations: reservationsReducer,
    notifications: notificationReducer,
  },
});

export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;
