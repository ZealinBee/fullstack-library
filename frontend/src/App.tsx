import React, {useEffect} from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import "react-toastify/dist/ReactToastify.css";
import { gapi } from "gapi-script";

import AuthPage from "./pages/AuthPage";
import HomePage from "./pages/HomePage";
import NotFoundPage from "./pages/NotFoundPage";
import CartPage from "./pages/CartPage";
import DashboardPage from "./pages/DashboardPage";
import LandingPage from "./pages/LandingPage";
import ProfilePage from "./pages/ProfilePage";
import BookPage from "./pages/BookPage";
import AuthorsPage from "./pages/AuthorsPage";
import AuthorPage from "./pages/AuthorPage";
import LoansPage from "./pages/LoansPage";
import GenrePage from "./pages/GenrePage";
import LoanPage from "./pages/LoanPage";
import PaymentSuccessPage from "./pages/PaymentSuccessPage";
import ReservationPage from "./pages/ReservationPage";
import NotificationPage from "./pages/NotificationPage";

const router = createBrowserRouter([
  {
    path: "/",
    element: <HomePage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/auth",
    element: <AuthPage />,
  },
  {
    path: "/cart",
    element: <CartPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/dashboard",
    element: <DashboardPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/welcome",
    element: <LandingPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/profile",
    element: <ProfilePage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/books/:id",
    element: <BookPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/authors",
    element: <AuthorsPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/authors/:id",
    element: <AuthorPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/loans",
    element: <LoansPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/genres",
    element: <GenrePage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/loans/:id",
    element: <LoanPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/payment/success",
    element: <PaymentSuccessPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/reservations",
    element: <ReservationPage />,
    errorElement: <NotFoundPage />,
  },
  {
    path: "/notifications",
    element: <NotificationPage />,
    errorElement: <NotFoundPage />,
  },

]);

const App = () => {
  useEffect(() => {
    function start() {
      gapi.client.init({
        clientId: "1096646429447-u3c733v9j1sf3t0maqkmn9tmp4ka3c3q.apps.googleusercontent.com",
        scope: "https://www.googleapis.com/auth/gmail.readonly"
      })
    }
    gapi.load('client:auth2', start)
  }, [])
  return (
    <>
      <RouterProvider router={router} />
    </>
  );
};

export default App;
