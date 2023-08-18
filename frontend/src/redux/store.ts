import {configureStore} from '@reduxjs/toolkit';

import usersReducer from './reducers/usersReducers';

const store = configureStore({
    reducer: {
        users: usersReducer
    }
})

export type GlobalState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;
export default store;