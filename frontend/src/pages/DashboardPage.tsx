import React, { useEffect } from "react";
import { useNavigate } from "react-router-dom";

import CreateBook from "../components/CreateBook";
import UserList from "../components/UserList";
import Header from "../components/Header";
import useAppSelector from "../redux/hooks/useAppSelector";

function DashboardPage() {
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const navigate = useNavigate();
  useEffect(() => {
    if (!currentUser) {
      navigate("/auth");
    }
    if (currentUser?.role !== "Librarian") {
      navigate("/");
    }
  }, []);

  return (
    <div>
      <Header />
      <h1>Dashboard</h1>
      <CreateBook />
      <UserList />
    </div>
  );
}

export default DashboardPage;
