import LoanDetails from "./LoanDetails";

export default interface GetLoan {
    bookId: string;
    userId: string;
    loanDate: string;
    dueDate: string;
    returnedDate: string;
    loanDetails: LoanDetails[];
}

