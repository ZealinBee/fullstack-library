import {configureStore} from '@reduxjs/toolkit';

import usersReducer from './reducers/usersReducer';
import booksReducer from './reducers/booksReducer';
import cartReducer from './reducers/cartReducer';
import authorsReducer from './reducers/authorsReducer';

const store = configureStore({
    reducer: {
        users: usersReducer,
        books: booksReducer,
        cart: cartReducer,
        authors: authorsReducer
    }
})

export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;