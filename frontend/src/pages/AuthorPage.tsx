import React from "react";
import { Link } from "react-router-dom";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { deleteAuthor } from "../redux/reducers/authorsReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { selectCurrentBook } from "../redux/reducers/booksReducer";
import Book from "../components/Book";

function AuthorPage() {
  const currentAuthor = useAppSelector((state) => state.authors.currentAuthor);
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const token = useAppSelector((state) => state.users.currentToken);

  function deleteAuthorHandler(authorId: string | undefined) {
    if (!authorId || !token) return;
    dispatch(deleteAuthor({ authorId: authorId, jwt_token: token }));
  }

  return (
    <div>
      <Header></Header>
      <div className="author-page">
        <div className="img-wrapper">
          <img src={currentAuthor?.authorImage} alt="" />
          <h2>{currentAuthor?.authorName}</h2>
          {currentUser?.role === "Librarian" ? (
            <Link to="/authors">
              {" "}
              <button
                onClick={() => deleteAuthorHandler(currentAuthor?.authorId)}
              >
                delete author
              </button>
            </Link>
          ) : null}
        </div>
        <div className="author-page__books-wrapper">
          <h3>{currentAuthor?.authorName}'s books</h3>
          {currentAuthor?.books.map((book) => {
            return <Book book={book}></Book>;
          })}
        </div>
      </div>
    </div>
  );
}

export default AuthorPage;
