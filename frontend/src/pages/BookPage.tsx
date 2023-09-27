import React from "react";
import { Link } from "react-router-dom";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";
import { addToCart } from "../redux/reducers/cartReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { setCurrentAuthor } from "../redux/reducers/authorsReducer";
import { deleteBook } from "../redux/reducers/booksReducer";
import EditBook from "../components/EditBook";

function BookPage() {
  const currentBook = useAppSelector((state) => state.books.currentBook);
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.users.currentUser);
  let token = useAppSelector((state) => state.users.currentToken);
  const isBookInCart = useAppSelector((state) =>
    state.cart.cartItems.find((item) => item.bookId === currentBook?.bookId)
  );
  const [editMode, setEditMode] = React.useState(false);

  function addToCartHandler() {
    dispatch(addToCart(currentBook));
  }

  function deleteBookHandler(bookId: string | undefined) {
    if (!bookId) return;
    dispatch(deleteBook({ bookId: bookId, jwt_token: token }));
  }

  return (
    <>
      <Header />
      <div className="book-page">
        <div className="book-page__img-wrapper">
          <img src={currentBook?.bookImage} alt="image for a book" />
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
          <p>ISBN: {currentBook?.ISBN}</p>
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
          {editMode ? <EditBook /> : null}
        </div>
      </div>
    </>
  );
}

export default BookPage;
