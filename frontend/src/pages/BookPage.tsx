import React, { useEffect } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { addToCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { setCurrentAuthor } from "../redux/reducers/authorsReducer";
import { deleteBook } from "../redux/reducers/booksReducer";
import EditBook from "../components/EditBook";
import { selectCurrentBook } from "../redux/reducers/booksReducer";

function BookPage() {
  let currentBook = useAppSelector((state) => state.books.currentBook);
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.users.currentUser);
  let token = useAppSelector((state) => state.users.currentToken);
  const isBookInCart = useAppSelector((state) =>
    state.cart.cartItems.find((item) => item.bookId === currentBook?.bookId)
  );
  const [editMode, setEditMode] = React.useState(false);
  const navigate = useNavigate();
  
  function addToCartHandler() {
    dispatch(addToCart(currentBook));
  }

  function deleteBookHandler(bookId: string | undefined) {
    if (!bookId) return;
    dispatch(deleteBook({ bookId: bookId, jwt_token: token }));
  }

  useEffect(() => {
    if (currentBook === null) {
      navigate("/");
    }
  }, []);

  return (
    <>
      <Header />
      <div className="book-page">
        <div className="book-page__img-wrapper">
          <img src={currentBook?.bookImage} alt="book" />
          {currentUser?.role === "User" &&
            (!isBookInCart ? (
              <button onClick={addToCartHandler}>Add To Loan Cart</button>
            ) : (
              <button disabled className="bookList__add-book">
                Already in Loan Cart
              </button>
            ))}
        </div>
        <div className="book-page__book-details">
          <h2 className="book-page__book-name">{currentBook?.bookName}</h2>
          {/* <Link to={`/authors/${currentBook?.authorName}}`}> */}{" "}
          <h3 className="book-page__book-author">{currentBook?.authorName}</h3>
          {/* </Link> */}
          <p className="book-page__book-description">
            {currentBook?.description}
          </p>
          <p>isbn: {currentBook?.isbn}</p>
          <p>
            {currentBook?.pageCount} pages, first published on{" "}
            {currentBook?.publishedDate}
          </p>
          {currentUser?.role === "Librarian" ? (
            <>
              <Link to="/">
                <button onClick={() => deleteBookHandler(currentBook?.bookId)}>
                  Delete
                </button>
              </Link>
              <button
                style={{ marginLeft: "0.5rem" }}
                onClick={() => setEditMode(!editMode)}
              >
                Edit
              </button>
            </>
          ) : null}
          {editMode ? <EditBook setEditMode={setEditMode} /> : null}
        </div>
      </div>
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
    </>
  );
}

export default BookPage;
