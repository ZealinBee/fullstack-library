import Book from "../books/Book";

export default interface GetGenre {
    genreId: string;
    genreName: string;
    books: Book[];
}