
import GetBook from "../books/GetBook";
export default interface GetAuthor
{
    authorId: string;
    authorName: string;
    books: GetBook[];
    authorImage: string;

}