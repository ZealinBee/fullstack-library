import React, { useState } from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import { updateBook } from "../redux/reducers/booksReducer";
import useAppDispatch from "../redux/hooks/useAppDispatch";

function EditBook() {
  const currentBook = useAppSelector((state) => state.books.currentBook)!;
  const [book, setBook] = useState({
    bookName: currentBook.bookName,
    authorName: currentBook.authorName,
    description: currentBook.description,
    ISBN: currentBook.ISBN,
    quantity: currentBook.quantity,
    pageCount: currentBook.pageCount,
    publishedDate: currentBook.publishedDate,
    bookImage: currentBook.bookImage,
    genreName: currentBook.genreName,
  });
  const token = useAppSelector((state) => state.users.currentToken);
  const dispatch = useAppDispatch();

  function formChangeHandler(
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLTextAreaElement>
  ) {
    setBook((prevState) => {
      return {
        ...prevState,
        [event.target.name]: event.target.value,
      };
    });
  }

  function submitFormHandler(event: React.FormEvent) {
    event.preventDefault();
    dispatch(
      updateBook({ bookId: currentBook?.bookId, book: book, jwt_token: token })
    );
  }

  return (
    <div>
      <form className="form edit-book-form" onSubmit={submitFormHandler}>
        <label htmlFor="book-name">Book Name</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="bookName"
          value={book.bookName}
          required
          placeholder="Book Name"
          id="book-name"
        />
        <label htmlFor="author-name">Author Name</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="authorName"
          value={book.authorName}
          required
          placeholder="Author Name"
          id="author-name"
        />
        <label htmlFor="description">Description</label>
        <textarea
          onChange={formChangeHandler}
          name="description"
          value={book.description}
          required
          placeholder="Description"
          id="description"
        />
        <label htmlFor="isbn">ISBN</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="ISBN"
          value={book.ISBN}
          required
          placeholder="ISBN"
          id="isbn"
        />
        <label htmlFor="quantity">Quantity in Library</label>
        <input
          type="number"
          onChange={formChangeHandler}
          name="quantity"
          value={book.quantity}
          required
          placeholder="Quantity in Library"
          id="quantity"
        />
        <label htmlFor="page-count">Page Count</label>
        <input
          type="number"
          onChange={formChangeHandler}
          name="pageCount"
          value={book.pageCount}
          required
          placeholder="Page Count"
          id="page-count"
        />
        <label htmlFor="genre-name">Genre Name</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="genreName"
          value={book.genreName}
          required
          placeholder="Genre Name"
          id="genre-name"
        />
        <label htmlFor="book-image">Book Image URL</label>
        <input
          type="text"
          onChange={formChangeHandler}
          name="bookImage"
          value={book.bookImage}
          required
          placeholder="Book Image URL"
          id="book-image"
        />
        <button type="submit">Update Book</button>
      </form>
    </div>
  );
}

export default EditBook;
