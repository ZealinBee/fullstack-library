import React, { useState } from "react";
import { ToastContainer, toast } from "react-toastify";

import useAppSelector from "../redux/hooks/useAppSelector";
import { updateUser } from "../redux/reducers/usersReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";

interface EditUserProps {
  setEditMode: React.Dispatch<React.SetStateAction<boolean>>;
  
}

function EditBook({ setEditMode }: EditUserProps) {
  const currentUser = useAppSelector((state) => state.users.currentUser)!;
  const [user, setUser] = useState({
    firstName: currentUser.firstName,
    lastName: currentUser.lastName,
    password: "xxxxxx",
    userImage: currentUser.userImage,
  });
  const token = useAppSelector((state) => state.users.currentToken);
  const dispatch = useAppDispatch();

  function formChangeHandler(
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLTextAreaElement>
  ) {
    setUser((prevState) => {
      return {
        ...prevState,
        [event.target.name]: event.target.value,
      };
    });
  }

  async function submitFormHandler(event: React.FormEvent) {
    event.preventDefault();
    const response = await dispatch(
      updateUser({ userId: currentUser?.userId, user: user, jwt_token: token })
    );
    if (response.type === "users/updateUser/fulfilled") {
      toast.success("User updated, changes will be seen later");
      setEditMode(false);
    }
  }

  return (
    <div>
      <form className="form edit-book-form" onSubmit={submitFormHandler}>
        <label htmlFor="book-name">First Name</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="firstName"
          value={user.firstName}
        />
        <label htmlFor="book-name">Last Name</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="lastName"
          value={user.lastName}
        />
        <label htmlFor="book-name">Password</label>
        <input
          type="password"
          onChange={formChangeHandler}
          name="password"
          value={user.password}
        />
        <label htmlFor="book-name">User Image</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="userImage"
          value={user.userImage}
        />
        <button type="submit">Update</button>
        <ToastContainer />
      </form>
    </div>
  );
}

export default EditBook;
