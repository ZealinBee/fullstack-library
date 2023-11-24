import React, {useEffect} from "react";

import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getOwnNotifications } from "../redux/reducers/notificationReducer";

function NotificationPage() {
  let token = useAppSelector(state => state.users.currentToken);
  let notifications = useAppSelector(state => state.notifications.notifications);
  const dispatch = useAppDispatch();


  useEffect(() => {
    dispatch(getOwnNotifications(token));
  }, [dispatch, token]);

  return (
    <>
      <Header></Header>
      <h1 className="top">Notifications</h1>

    </>
  );
}

export default NotificationPage;
