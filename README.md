# Introduction

This is an online library that acts like your local city library. The user can create accounts and loan books, and the librarian can manage the books in the library. The website gives users and librarians different privileges, i.e. access to the different functionalities of the website. The admins have a detailed dashboard where they can manage the users and books, for example, admin have the right to delete an user or give the user the admin. It is also possible for the user to remove their own account. Feel free to make accounts and mess around with the functionalities. The website API is hosted on Azure App Service.

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

## Getting Started

[FRONTEND LINK](https://integrify-library.netlify.app/)  
[BACKEND LINK (Hosted on Azure App Service)](https://integrify-library.azurewebsites.net/swagger/index.html)

To test the librarian functionalities for the website, the credentials for admin/librarian is:
email: admin@mail.com
password: admin123
To test the user, feel free to just create an user
