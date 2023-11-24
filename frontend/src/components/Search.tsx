import React, { useState, useEffect } from "react";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useNavigate } from "react-router-dom";
import {
  Select,
  MenuItem,
  FormControl,
  SelectChangeEvent,
} from "@mui/material";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { searchBooks, sortBooks } from "../redux/reducers/booksReducer";

function Search() {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");
  const [reset, setReset] = useState(false);
  const [select, setSelect] = useState("Upload Date Descending");

  function onSearchHandler() {
    dispatch(searchBooks(search));
    setReset(true);
  }
  function resetHandler() {
    setSearch("");
    setReset(false);
    dispatch(searchBooks(""));
  }

  function selectChangeHandler(e: SelectChangeEvent) {
    setSelect(e.target.value);
    setReset(true);
    dispatch(sortBooks(e.target.value));
  }

  // this useEffect is used to reset the books array to the original state without additional fetching
  useEffect(() => {
    dispatch(searchBooks(""));
  }, [dispatch]);

  return (
    <>
      <div className="search-wrapper">
        <div className="search">
          <div className="search-box">
            <input
              type="text"
              placeholder="Search for books, authors, genres..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
            />
            <FontAwesomeIcon
              icon={faMagnifyingGlass}
              className="magnifying-glass"
              onClick={onSearchHandler}
            />
          </div>
          {reset ? (
            <button className="reset-button" onClick={resetHandler}>
              Reset
            </button>
          ) : null}
        </div>
        <FormControl size="small" sx={{ mt: "1.25rem", ml: "2rem" }}>
          <Select
            labelId="demo-select-small-label"
            id="demo-select-small"
            value={select}
            onChange={selectChangeHandler}
            sx={{ fontSize: "0.85rem", fontFamily: "poppins" }}
          >
            <MenuItem value="Upload Date Descending">
              Newest Uploads
            </MenuItem>
            <MenuItem value="Upload Date Ascending">
              Oldest Uploads
            </MenuItem>
            <MenuItem value="Popularity Descending">
              Most Popular
            </MenuItem>
            <MenuItem value="Popularity Ascending">
              Least Popular
            </MenuItem>
          </Select>
        </FormControl>
      </div>
    </>
  );
}

export default Search;
