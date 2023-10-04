import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";

import Header from "../components/Header";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import useAppSelector from "../redux/hooks/useAppSelector";
import {
  getUserProfile,
  deleteProfile,
  logoutUser,
} from "../redux/reducers/usersReducer";
import EditUser from "../components/EditUser";

function ProfilePage() {
  const user = useAppSelector((state) => state.users.currentUser);
  const dispatch = useAppDispatch();
  let token = useAppSelector((state) => state.users.currentToken);
  const navigate = useNavigate();
  const [editMode, setEditMode] = useState(false);

  useEffect(() => {
    if (user === null) {
      navigate("/");
    }
    dispatch(getUserProfile(token));
  }, []);

  async function deleteProfileHandler() {
    await dispatch(deleteProfile(token));
    navigate("/");
  }

  function logoutHandler() {
    dispatch(logoutUser());
    navigate("/");
  }

  return (
    <div>
      <Header></Header>
      <h1 className="heading">Your profile</h1>
      <div className="profile-page">
        <div className="img-wrapper">
          <img src={user?.userImage} alt="profile-pic" />
        </div>
        <div className="profile-details">
          <h2 className="name">
            {user?.firstName} {user?.lastName}
          </h2>
          <h2 className="email">{user?.email}</h2>
          <h2>
            {user?.role} <span>(Role)</span>
          </h2>
          <button onClick={deleteProfileHandler}>Delete account</button>
          <button onClick={() => setEditMode(!editMode)}>Update account</button>
          <button onClick={logoutHandler}>Logout</button>
        </div>
      </div>
      {editMode && <EditUser setEditMode={setEditMode} />}
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
    </div>
  );
}

export default ProfilePage;
