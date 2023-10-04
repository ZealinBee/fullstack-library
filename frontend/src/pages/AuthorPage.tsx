import React, { useEffect, useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { deleteAuthor } from "../redux/reducers/authorsReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import Book from "../components/Book";
import EditAuthor from "../components/EditAuthor";

function AuthorPage() {
  const currentAuthor = useAppSelector((state) => state.authors.currentAuthor);
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const token = useAppSelector((state) => state.users.currentToken);
  const navigate = useNavigate();
  const [editMode, setEditMode] = useState(false);

  function deleteAuthorHandler(authorId: string | undefined) {
    if (!authorId || !token) return;
    dispatch(deleteAuthor({ authorId: authorId, jwt_token: token }));
  }

  useEffect(() => {
    if (currentAuthor === null) {
      navigate("/");
    }
  }, []);

  return (
    <div>
      <Header></Header>
      <div className="author-page">
        <div className="img-wrapper">
          <img src={currentAuthor?.authorImage} alt="author-face" />
          <h2>{currentAuthor?.authorName}</h2>
          {currentUser?.role === "Librarian" ? (
            <div className="flex">
              <Link to="/authors">
                <button
                  onClick={() => deleteAuthorHandler(currentAuthor?.authorId)}
                >
                  Delete
                </button>
              </Link>
              <button onClick={() => setEditMode(!editMode)}>
                Edit
              </button>
            </div>
          ) : null}
        </div>
        
        <div className="author-page__books-wrapper">
          <h3>{currentAuthor?.authorName}'s books</h3>
          <div className="books-wrapper">
            {currentAuthor?.books.map((book) => {
              return <Book book={book}></Book>;
            })}
          </div>
        </div>
      </div>
      {editMode && <EditAuthor setEditMode={setEditMode} />}
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
    </div>
  );
}

export default AuthorPage;
