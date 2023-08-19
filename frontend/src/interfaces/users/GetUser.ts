export default interface GetUser {
    userId: string;
    firstName: string;
    lastName: string;
    email: string;
    role: "Librarian" | "User";
}