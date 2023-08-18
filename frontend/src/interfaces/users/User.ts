export default interface User {
    userId: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    salt: string;
    role: "Librarian" | "User";
    createdAt: Date;
    updatedAt: Date;
}