import {configureStore} from '@reduxjs/toolkit';

import usersReducer from './reducers/usersReducer';
import booksReducer from './reducers/booksReducer';

const store = configureStore({
    reducer: {
        users: usersReducer,
        books: booksReducer,
    }
})

export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;