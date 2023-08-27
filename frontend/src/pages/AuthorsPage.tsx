import React, { useEffect } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import useAppSelector from "../redux/hooks/useAppSelector";
import { getAllAuthors } from "../redux/reducers/authorsReducer";
import Header from "../components/Header";
import GetAuthor from "../interfaces/authors/GetAuthor";
import { setCurrentAuthor } from "../redux/reducers/authorsReducer";
import { Link } from "react-router-dom";

function AuthorsPage() {
  const dispatch = useAppDispatch();
  const authors = useAppSelector((state) => state.authors.authors);

  useEffect(() => {
    dispatch(getAllAuthors());
    console.log(authors);
  }, []);

  return (
    <div>
      <Header></Header>
      <h1>Authors Page</h1>
      {authors.map((author: GetAuthor) => {
        return (
          <div key={author.authorId}>
            <h3>Author name:{author.authorName}</h3>
            <Link to={`/authors/${author.authorName}`}>
              <button onClick={() => dispatch(setCurrentAuthor(author))}>
                View details
              </button>
            </Link>
          </div>
        );
      })}
    </div>
  );
}

export default AuthorsPage;
