import React, { useEffect, useState } from "react";

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
    </>
  );
}

export default Homepage;
