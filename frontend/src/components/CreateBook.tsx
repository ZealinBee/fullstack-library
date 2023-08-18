import React, { useState } from "react";

import useAppDispatch from "../redux/hooks/useAppDispatch";
import { createBook } from "../redux/reducers/booksReducer";
import SimpleBook from "../interfaces/books/SimpleBook";

function CreateBook() {
  const dispatch = useAppDispatch();
  const [book, setBook] = useState<SimpleBook>({
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

  function formChangeHandler(event: React.ChangeEvent<HTMLInputElement>) {
    setBook((prevState) => {
      return {
        ...prevState,
        [event.target.name]: event.target.value,
      };
    });
  }

  return (
    <div>
      <h1>Create Book</h1>
      <form onSubmit={createBookHandler}>
        <input
          type="text"
          onChange={formChangeHandler}
          name="bookName"
          value={book.bookName}
        />
        <input
          type="text"
          onChange={formChangeHandler}
          name="authorName"
          value={book.authorName}
        />
        <input
          type="text"
          onChange={formChangeHandler}
          name="description"
          value={book.description}
        />
        <input
          type="text"
          onChange={formChangeHandler}
          name="ISBN"
          value={book.ISBN}
        />
        <input
          type="number"
          onChange={formChangeHandler}
          name="quantity"
          value={book.quantity}
        />
        <input
          type="number"
          onChange={formChangeHandler}
          name="pageCount"
          value={book.pageCount}
        />
        <button type="submit">Create Book</button>
      </form>
    </div>
  );
}

export default CreateBook;
