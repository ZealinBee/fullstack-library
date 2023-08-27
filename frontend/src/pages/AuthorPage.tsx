import React from "react";

import useAppSelector from "../redux/hooks/useAppSelector";
import Header from "../components/Header";

function AuthorPage() {
  const currentAuthor = useAppSelector((state) => state.authors.currentAuthor);
  console.log(currentAuthor?.books)
  return (
    <div>
      <Header></Header>
      <h2>Author Name: {currentAuthor?.authorName}</h2>
      {currentAuthor?.books.map((book) => {
        return (
          <div key={book.bookId}>
            <h3>Book Name: {book.bookName}</h3>
            <h3>ISBN: {book.ISBN}</h3>
          </div>
        );
      })}
    </div>
  );
}

export default AuthorPage;
