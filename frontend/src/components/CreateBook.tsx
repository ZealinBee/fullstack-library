import React, { useState } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { createBook } from "../redux/reducers/booksReducer";
import ICreateBook from "../interfaces/books/CreateBook";

function CreateBook() {
  const dispatch = useAppDispatch();
  const [book, setBook] = useState<ICreateBook>({
    bookName: "",
    authorName: "",
    description: "",
    ISBN: "",
    quantity: 0,
    pageCount: 0,
    publishedDate: new Date().toISOString().split("T")[0], 
  });

  function createBookHandler(event: React.FormEvent) {
    event.preventDefault();
    console.log(book);
    dispatch(createBook(book));
  }

  function formChangeHandler(event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLTextAreaElement>) {
    setBook((prevState) => {
      return {
        ...prevState,
        [event.target.name]: event.target.value,
      };
    });
  }

  return (
    <div className="create-book">
      <h1>Create Book</h1>
      <form onSubmit={createBookHandler}>
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
        <button type="submit">Create Book</button>
      </form>
    </div>
  );
}

export default CreateBook;
