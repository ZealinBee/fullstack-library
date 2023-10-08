import React, { useEffect, useState } from "react";
import { ToastContainer, toast } from "react-toastify";

import Header from "../components/Header";
import BookList from "../components/BookList";
import Search from "../components/Search";

function Homepage() {
  return (
    <>
      <Header />
      <Search></Search>
      <h1 className="top">Discover</h1>
      <BookList />
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
    </>
  );
}

export default Homepage;
