import React from "react";
import ReportGmailerrorredRoundedIcon from "@mui/icons-material/ReportGmailerrorredRounded";
import BookRoundedIcon from "@mui/icons-material/BookRounded";

import { GetNotification } from "../interfaces/notifications/GetNotification";

interface NotificationProps {
  notification: GetNotification;
}

function Notification({ notification }: NotificationProps) {
  return (
    <div className="notification">
      <div className="notification-icon">
        {notification.notificationType === "BookAvailable" ? (
          <BookRoundedIcon />
        ) : (
          <ReportGmailerrorredRoundedIcon />
        )}
      </div>
      <div className="notification-content">
        {notification.notificationMessage}
      </div>
    </div>
  );
}

export default Notification;
