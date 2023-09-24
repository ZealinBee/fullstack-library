import React, { useEffect, useState } from "react";

import Header from "../components/Header";
import BookList from "../components/BookList";

function Homepage() {
  return (
    <>
      <Header />
      <h1 className="top">Discover (MORE STYLING AND FEATURE COMING SOON)</h1>
      <BookList />
    </>
  );
}

export default Homepage;
