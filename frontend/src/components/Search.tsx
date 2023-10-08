import React, { useState } from "react";
import { faMagnifyingGlass } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useNavigate } from "react-router-dom";
import { Select, MenuItem, FormControl, SelectChangeEvent } from "@mui/material";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { searchBooks } from "../redux/reducers/booksReducer";

function Search() {
  const dispatch = useAppDispatch();
  const [search, setSearch] = useState("");
  const [reset, setReset] = useState(false);
  const navigate = useNavigate();
  const [select, setSelect] = useState("Publish Date Descending");

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
  }
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
            sx={{fontSize: "0.85rem", fontFamily: "poppins"}}
          >
            <MenuItem value="Publish Date Descending">
              Publish Date Descending
            </MenuItem>
            <MenuItem value="Publish Date Ascending">
              Publish Date Ascending
            </MenuItem>
          </Select>
        </FormControl>
      </div>
    </>
  );
}

export default Search;
