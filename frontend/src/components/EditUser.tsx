import React, { useState } from "react";
import { ToastContainer, toast } from "react-toastify";

import useAppSelector from "../redux/hooks/useAppSelector";
import { updateUser } from "../redux/reducers/usersReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";

interface EditUserProps {
  setEditMode: React.Dispatch<React.SetStateAction<boolean>>;
  
}

function EditUser({ setEditMode }: EditUserProps) {
  const currentUser = useAppSelector((state) => state.users.currentUser)!;
  const [user, setUser] = useState({
    firstName: currentUser.firstName,
    lastName: currentUser.lastName,
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
      toast.success("User updated");
      setEditMode(false);
    }
  }

  return (
    <div>
      <form className="form edit-user-form" onSubmit={submitFormHandler}>
        <label htmlFor="first-name">First Name</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="firstName"
          value={user.firstName}
          id="first-name"
        />
        <label htmlFor="last-name">Last Name</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="lastName"
          value={user.lastName}
          id="last-name"
        />
        <label htmlFor="user-image">User Image</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="userImage"
          value={user.userImage}
          id="user-image"
        />
        <button type="submit">Update</button>
      </form>

    </div>
  );
}

export default EditUser;
