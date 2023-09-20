import React, { useEffect, useState } from "react";

import Header from "../components/Header";
import BookList from "../components/BookList";

function Homepage() {
  return (
    <>
      <Header />
      <h1 style={{ paddingTop: '1rem', marginLeft: 'calc(40% - 550px / 2)', marginBottom: "1rem" }}>Discover (STYLING COMING SOON)</h1>
      <BookList />
    </>
  );
}

export default Homepage;
