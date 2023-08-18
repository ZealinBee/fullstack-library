import axios, {AxiosError} from 'axios';
import {createSlice} from '@reduxjs/toolkit';

import User from '../../interfaces/users/User';

interface UsersState {
    users: User[];
    loading: boolean;
    error: AxiosError | null;
    currentUser: User | null;
    isLoggedIn: boolean;
}

const initialState: UsersState = {
    users: [],
    loading: false,
    error: null,
    currentUser: null,
    isLoggedIn: false
}

const usersSlice = createSlice({
    name: 'users',
    initialState,
    reducers: {

    }
})

const usersReducer = usersSlice.reducer;
export default usersReducer;