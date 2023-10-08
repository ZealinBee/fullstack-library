import React, { useEffect } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import useAppSelector from "../redux/hooks/useAppSelector";
import GetUser from "../interfaces/users/GetUser";
import {
  getAllUsers,
  deleteUser,
  makeUserLibrarian,
} from "../redux/reducers/usersReducer";

function UserList() {
  const dispatch = useAppDispatch();
  let token = useAppSelector((state) => state.users.currentToken);
  const users = useAppSelector((state) => state.users.users);

  useEffect(() => {
    dispatch(getAllUsers(token));
  }, [token, dispatch]);

  function deleteUserHandler(userId: string) {
    const isConfirmed = window.confirm(`Are you sure you want to delete user ${userId}?`);
    if (!isConfirmed) {
      return;
    }
    dispatch(deleteUser({ userId: userId, jwt_token: token }));
  }

  function makeUserLibrarianHandler(userEmail: string) {
    dispatch(makeUserLibrarian({ userEmail: userEmail, jwt_token: token }));
  }

  return (
    <div className="user-list">
      <h1>User List</h1>
      {users.map((user: GetUser) => {
        return (
          <div key={user.userId}>
            <div>
              <h3>
                Email: <span>{user.email}</span>
              </h3>
              <h3>
                First Name: <span>{user.firstName}</span>
              </h3>
              <h3>
                User:<span> {user.lastName}</span>
              </h3>
              <h3>
                User Role: <span>{user.role}</span>
              </h3>
            </div>
            <button onClick={() => deleteUserHandler(user.userId)}>
              Delete
            </button>
            {user.role !== "Librarian" ? (
              <button onClick={() => makeUserLibrarianHandler(user.email)}>
                Make User Librarian
              </button>
            ) : null}
          </div>
        );
      })}
    </div>
  );
}

export default UserList;
