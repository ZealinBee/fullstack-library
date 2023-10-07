import React, { useState } from "react";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useNavigate } from "react-router-dom";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { searchBooks } from "../redux/reducers/booksReducer";

function Search() {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");
  const [reset, setReset] = useState(false);
  const navigate = useNavigate();
  
  function onSearchHandler() {
    dispatch(searchBooks(search));
    setReset(true);
    navigate("/");
  }
  function resetHandler() {
    setSearch("");
    setReset(false);
    dispatch(searchBooks(""));
  }
  return (
    <div className="search">
      <div className="search-box">
        <FontAwesomeIcon
          icon={faMagnifyingGlass}
          className="magnifying-glass"
          onClick={onSearchHandler}
        />
        <input
          type="text"
          placeholder="Search for books, authors, genres..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
      </div>
      {reset ? <button className="reset-button" onClick={resetHandler}>Reset</button> : null}
    </div>
  );
}

export default Search;
