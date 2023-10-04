import React, { useState } from "react";
import { ToastContainer, toast } from "react-toastify";

import useAppSelector from "../redux/hooks/useAppSelector";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { updateAuthor } from "../redux/reducers/authorsReducer";

interface EditAuthorProps {
  setEditMode: React.Dispatch<React.SetStateAction<boolean>>;
  
}

function EditAuthor({ setEditMode }: EditAuthorProps) {
  const currentAuthor = useAppSelector((state) => state.authors.currentAuthor)!;
  const [author, setAuthor] = useState({
    authorName: currentAuthor.authorName,
    authorImage: currentAuthor.authorImage,
  });
  const token = useAppSelector((state) => state.users.currentToken);
  const dispatch = useAppDispatch();

  function formChangeHandler(
    event:
      | React.ChangeEvent<HTMLInputElement>
      | React.ChangeEvent<HTMLTextAreaElement>
  ) {
    setAuthor((prevState) => {
      return {
        ...prevState,
        [event.target.name]: event.target.value,
      };
    });
  }

  async function submitFormHandler(event: React.FormEvent) {
    event.preventDefault();
    const response = await dispatch(
        updateAuthor({ authorId: currentAuthor?.authorId, author: author, jwt_token: token })
    );
    if (response.type === "authors/updateAuthor/fulfilled") {
      toast.success("Author updated");
      setEditMode(false);
    }
  }

  return (
    <div>
      <form className="form edit-author-form" onSubmit={submitFormHandler}>
        
        <button type="submit">Update</button>
      </form>

    </div>
  );
}

export default EditAuthor;
