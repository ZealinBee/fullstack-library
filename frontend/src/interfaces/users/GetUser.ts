export default interface GetUser {
    firstName: string;
    lastName: string;
    email: string;
    role: "Librarian" | "User";
}