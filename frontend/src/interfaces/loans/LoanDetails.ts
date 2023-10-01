import GetBook from "../books/GetBook";

export default interface LoanDetails {
    loansDetailId: string;
    bookId: string;
    book: GetBook
}