import LoanDetails from "./LoanDetails";

export default interface GetLoan {
    loanId: string;
    bookId: string;
    userId: string;
    loanDate: string;
    dueDate: string;
    returnedDate: string;
    loanDetails: LoanDetails[];
}

