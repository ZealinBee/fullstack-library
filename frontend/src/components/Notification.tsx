import React from "react";
import ReportGmailerrorredRoundedIcon from "@mui/icons-material/ReportGmailerrorredRounded";
import BookRoundedIcon from "@mui/icons-material/BookRounded";
import { Link, useNavigate } from "react-router-dom";
import CloseIcon from "@mui/icons-material/Close";

import { GetNotification } from "../interfaces/notifications/GetNotification";
import { selectCurrentBook } from "../redux/reducers/booksReducer";
import { setCurrentLoan } from "../redux/reducers/loansReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getBookById } from "../redux/reducers/booksReducer";
import { deleteOwnNotification } from "../redux/reducers/notificationReducer";
import useAppSelector from "../redux/hooks/useAppSelector";

interface NotificationProps {
  notification: GetNotification;
}

function Notification({ notification }: NotificationProps) {
  // The point of this is, that when the user clicks on the notification, it will redirect them to the book or loan page, depending on the notification type
  const navigate = useNavigate();
  const dispatch = useAppDispatch();
  const token = useAppSelector((state) => state.users.currentToken);
  const values = Object.values(notification.notificationData);
  const notificationDataId = values[0];
  let redirectionUrl = "";
  if (notification.notificationType === "BookAvailable") {
    redirectionUrl = `/books/${notificationDataId}`;
  } else {
    redirectionUrl = `/loans/${notificationDataId}`;
  }

  async function redirectionHandler() {
    if (notification.notificationType === "BookAvailable") {
      const book = await dispatch(getBookById(notificationDataId));
      await dispatch(selectCurrentBook(book.payload));
    } else {
      // fix later
      dispatch(setCurrentLoan(notificationDataId));
    }
    navigate(redirectionUrl);
  }

  function deleteHandler() {
    dispatch(
      deleteOwnNotification({
        jwt_token: token,
        notificationId: notification.notificationId,
      })
    );
  }

  return (
    <div className="notification">
      <div className="notification__icon">
        {notification.notificationType === "BookAvailable" ? (
          <BookRoundedIcon />
        ) : (
          <ReportGmailerrorredRoundedIcon />
        )}
      </div>
      <div className="notification__content">
        <Link
          to="#"
          onClick={redirectionHandler}
          className="notification__redirect"
        >
          {notification.notificationMessage}
        </Link>
        <CloseIcon className="notification__delete" onClick={deleteHandler} />
      </div>
    </div>
  );
}

export default Notification;
