import React, { useEffect } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetUser from "../interfaces/users/GetUser"
import { getAllUsers } from "../redux/reducers/usersReducer";

function UserList() {
  const dispatch = useAppDispatch();
  let token = useAppSelector((state) => state.users.currentToken);
  const users = useAppSelector((state) => state.users.users); 

  useEffect(() => {
    dispatch(getAllUsers(token));
    console.log(users)
  }, [token, dispatch]);

  return <div>
    <h1>User List</h1>
    {users.map((user: GetUser)=> {
      return (
        <div key={user.email}>
          <h2>Email: {user.email}</h2>
          <h2>First Name: {user.firstName}</h2>
          <h2>User: {user.lastName}</h2>
          <h2>User Role: {user.role}</h2>
        </div>
      )
    })}

  </div>;
}

export default UserList;
