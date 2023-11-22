import React, { useEffect } from "react";
import useAppSelector from "../redux/hooks/useAppSelector";
import useAppDispatch from "../redux/hooks/useAppDispatch";
import { getOwnReservations } from "../redux/reducers/reservationsReducer";

import Header from "../components/Header";
import Book from "../components/Book";

function ReservationPage() {
  const reservations = useAppSelector(
    (state) => state.reservations.reservations
  );
  let jwt_token = useAppSelector((state) => state.users.currentToken);
  const dispatch = useAppDispatch();
  const currentUser = useAppSelector((state) => state.users.currentUser);
  const books = useAppSelector((state) => state.books.books);

  useEffect(() => {
    dispatch(getOwnReservations(jwt_token));
  }, []);

  function removeReservationHandler(reservationId: string) {}

  return (
    <>
      <Header></Header>
      {currentUser?.role === "Librarian" ? (
        <div>
          <h1 className="top">All Reservations</h1>
        </div>
      ) : (
        <div>
          <h1 className="top">My Reservations</h1>
        </div>
      )}

      <div className="reservations-page">
        {reservations.map((reservation) => {
          const book = books.find(
            (book) => book.bookId === reservation.bookId
          );
          if (!book) return null;
          return (
            <>
              <Book book={book}></Book>
              {/* <button
                onClick={() =>
                  removeReservationHandler(reservation.reservationId)
                }
              >
                Remove
              </button> */}
            </>
          );
        })}
      </div>
    </>
  );
}

export default ReservationPage;
