import React, { useState } from "react";
import { Link, NavLink } from "react-router-dom";
import HomeIcon from "@mui/icons-material/Home";
import PersonIcon from "@mui/icons-material/Person";
import LockIcon from "@mui/icons-material/Lock";
import DashboardIcon from "@mui/icons-material/Dashboard";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import PaymentsIcon from "@mui/icons-material/Payments";
import ListAltIcon from "@mui/icons-material/ListAlt";

import useAppSelector from "../redux/hooks/useAppSelector";
import Search from "./Search";

function Header() {
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const [showMenu, setShowMenu] = useState(false);
  return (
    <>
      <div className="horizontal-bar">
        <h1 className="logo" onClick={() => setShowMenu(!showMenu)}>
          <span className="accent">I</span>NTEGR
          <span className="accent">I</span>
          FY L<span className="accent">I</span>B
        </h1>
        <Search></Search>
      </div>

      <nav className="header">
        <ul>
          <li>
            <NavLink
              to={"/"}
              className={({ isActive }) => (isActive ? "active-link" : "")}
            >
              <div className="icon-wrapper">
                <HomeIcon />
              </div>
              <p>Discover</p>
            </NavLink>
          </li>
          <li>
            <NavLink
              to={"/authors"}
              className={({ isActive }) => (isActive ? "active-link" : "")}
            >
              <div className="icon-wrapper">
                <PersonIcon />
              </div>
              <p> Authors </p>
            </NavLink>
          </li>
          <li>
            <NavLink
              to={"/genres"}
              className={({ isActive }) => (isActive ? "active-link" : "")}
            >
              <div className="icon-wrapper">
                <ListAltIcon />
              </div>
              <p> Genres </p>
            </NavLink>
          </li>
          <div className="divider"></div>
          {currentUser ? null : (
            <li>
              <NavLink
                to={"/auth"}
                className={({ isActive }) => (isActive ? "active-link" : "")}
              >
                {" "}
                <div className="icon-wrapper">
                  <LockIcon />
                </div>{" "}
                <p>Auth </p>
              </NavLink>
            </li>
          )}
          {currentUser?.role === "Librarian" ? (
            <li>
              <NavLink
                to="/dashboard"
                className={({ isActive }) => (isActive ? "active-link" : "")}
              >
                <div className="icon-wrapper">
                  <DashboardIcon />
                </div>{" "}
                <p>Dashboard </p>
              </NavLink>
            </li>
          ) : null}{" "}
          {currentUser ? (
            <li>
              <NavLink
                to="/profile"
                className={({ isActive }) => (isActive ? "active-link" : "")}
              >
                <div className="icon-wrapper">
                  <AccountCircleIcon />
                </div>{" "}
                <p>Profile </p>
              </NavLink>
            </li>
          ) : null}
          {currentUser?.role === "Librarian" || !currentUser ? null : (
            <li>
              <NavLink
                to="/cart"
                className={({ isActive }) => (isActive ? "active-link" : "")}
              >
                <div className="icon-wrapper">
                  <ShoppingCartIcon />
                </div>{" "}
                <p>Loan Cart </p>
              </NavLink>
            </li>
          )}
          {currentUser ? (
            <li>
              <NavLink
                to={"/loans"}
                className={({ isActive }) => (isActive ? "active-link" : "")}
              >
                <div className="icon-wrapper">
                  <PaymentsIcon />
                </div>
                {currentUser.role === "Librarian" ? "All Loans" : "My Loans"}
              </NavLink>{" "}
            </li>
          ) : null}
        </ul>
      </nav>
    </>
  );
}

export default Header;
