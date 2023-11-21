export default interface GetBook {
    bookId: string;
    bookName: string;
    authorName: string;
    description: string;
    isbn: string;
    quantity: number;
    pageCount: number;
    publishedDate: string;
    bookImage: string;
    genreName: string;
    authorId: string;
    loanedTimes: number;
}