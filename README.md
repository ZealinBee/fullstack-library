# Introduction

Welcome to the Integrify Library Management System, an online platform designed to replicate the functionality of your local city library. This comprehensive documentation provides a detailed overview of the system's features, technologies used, deployment details, and instructions for getting started.

### Deployment

[Front-End Link, Click here](https://integrify-library.netlify.app/)  
[Back-end Link (Hosted on Azure App Service)](https://integrify-library.azurewebsites.net/swagger/index.html)

## Table of Content

- [Technologies](#technologies)
- [Project Structure](#project-strucutre)
- [Getting Started](#getting-started)

![Front page](./frontend/readme%20images/frontpage.png)

## Technologies

### FRONTEND:

- REACT
- MUI
- TYPESCRIPT
- SCSS
- Stripe

### BACKEND:

- PostgreSQL
- .NET Core
- Azure

## Getting Started

[FRONTEND LINK](https://integrify-library.netlify.app/)  
[BACKEND LINK (Hosted on Azure App Service)](https://integrify-library.azurewebsites.net/swagger/index.html)

To test the librarian functionalities for the website, the credentials for admin/librarian is:
- email: admin@mail.com
- password: admin123
  
To test the user, feel free to just create an user

## Features

- Authorization and authentication with JWT
- Error handling
- Notification for Book Reservation and Overdue

#### Librarian :

- CRUD operations on the users, ability to make user admin
- CRUD operations on the books
- CRUD operation on the authors
- CRUD operation on the genres
- See all the loans of the users, can manage them as well

#### User :

- Loan books and CRUD on own loans, ability view the loan history
- CRUD on own account
- See all the books and able to search and sort through the books
- Get all authors
- Get all genres
- Create Reservation for Unavailable Book
