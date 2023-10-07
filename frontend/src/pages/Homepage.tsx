import React, { useEffect, useState } from "react";

import Header from "../components/Header";
import BookList from "../components/BookList";
import Search from "../components/Search";
import Footer from "../components/Footer";

function Homepage() {
  return (
    <>
      <Header />
      <h1 className="top">Discover</h1>
      <BookList />
      <Footer></Footer>
    </>
  );
}

export default Homepage;
