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
  }, []);

  return (
    <div>
      <Header></Header>

      <div className="authors-page">
        <h1>Authors</h1>
        <div className="authors-page__authors">
          {authors.map((author: GetAuthor) => {
            return (
              <div key={author.authorId} className="authors-page__author">
                <img
                  src={author.authorImage}
                  alt="author's face"
                  className="authors-page__author-image"
                />
                <Link to={`/authors/${author.authorName}`}>
                  <button onClick={() => dispatch(setCurrentAuthor(author))}>
                    {author.authorName}
                  </button>
                </Link>
              </div>
            );
          })}
        </div>
      </div>
    </div>
  );
}

export default AuthorsPage;
