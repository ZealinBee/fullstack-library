import React from 'react'
import { RouterProvider, createBrowserRouter } from "react-router-dom";

import AuthPage from './pages/AuthPage';
import HomePage from "./pages/HomePage";
import NotFoundPage from './pages/NotFoundPage';
import CartPage from './pages/CartPage';
import DashboardPage from './pages/DashboardPage';
import LandingPage from './pages/LandingPage';
import ProfilePage from './pages/ProfilePage';
import BookPage from './pages/BookPage';

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
  }
])

const App = () => {
  return (
    <>
      <RouterProvider router={router} />
    </>
  )
}

export default App