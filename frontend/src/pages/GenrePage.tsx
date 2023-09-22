import React, { useEffect } from "react";
import { Link } from "react-router-dom";

import Header from "../components/Header";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import useAppSelector from "../redux/hooks/useAppSelector";
import { getAllGenres } from "../redux/reducers/genresReducer";
import { selectCurrentBook } from "../redux/reducers/booksReducer";

function GenrePage() {
  const dispatch = useAppDispatch();
  const genres = useAppSelector((state) => state.genres.genres);

  useEffect(() => {
    dispatch(getAllGenres());
  }, []);

  return (
    <div>
      <Header></Header>
      <div className="genre-page">
        <h1 className="top">All Genres</h1>
        <ul className="genres">
          {genres.map((genre) => {
            return (
              <>
                <li className="genre">
                  <h2>{genre.genreName}</h2>
                  {genre.books.map((book) => {
                    return (
                      <div className="book">
                        <Link
                          to={`/books/${book.bookId}`}
                          onClick={() => dispatch(selectCurrentBook(book))}
                        >
                          <img
                            src={book.bookImage}
                            alt="an image for the book"
                          />
                        </Link>
                      </div>
                    );
                  })}
                </li>
              </>
            );
          })}
        </ul>
      </div>
    </div>
  );
}

export default GenrePage;
