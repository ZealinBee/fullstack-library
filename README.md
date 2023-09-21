# Introduction

This is an online library that acts like your local city library. The user can create accounts and loan books, and the librarian can manage the books in the library. The website gives users and librarians different privileges, i.e. access to the different functionalities of the website. The admins have a detailed dashboard where they can manage the users, for example, admin have the right to delete an user or give the user the admin. It is also possible for the user to remove their own account. Feel free to make accounts and mess around with the functionalities. The website is hosted with azure virtual machine with ubuntu + nginx.

For the backend, this project uses .NET Core, Entity Framework Core, Postgres and Azure.  
For the frontend, this project uses React, typescript and MUI.

### Deployment

[BACKEND SWAGGER UI LINK - Hosted on azure virtual machine ubuntu + nginx](http://98.71.53.99/swagger/index.html)  
[FRONTEND - Also hosted on azure virtual machine ubuntu + nginx](http://98.71.53.99:3000/)

## Table of Content

- [Technologies](#technologies)
- [Project Structure](#project-strucutre)
- [Getting Started](#getting-started)

## Technologies

- REACT
- REACT ROUTER
- MUI
- TYPESCRIPT
- SCSS
- PostgreSQL
- .NET Core
- Entity Framework Core
- Azure
- XUnit

## Features

- Authorization and authentication with JWT
- Error handling

#### Librarian :

- CRUD operations on the users, ability to make user admin
- CRUD operations on the books
- CRUD operation on the authors
- CRUD operation on the genres
- See all the loans of the users, can choose to manage them as well

#### User :

- Loan books and CRUD on own loans, ability view the loan history
- Create new accounts
- Read, update and delete their own profile
- See all the books and able to search and sort through the books
- Get all authors
- Get all genres

## Project Structure

```
not finalized

```

## Getting Started

Clone the repository from github with `git clone` Then `npm i` for the packages and lastly `npm start` and go to your http://localhost:3000 to see the website

To test the librarian functionalities for the website, the credentials for admin/librarian is:
email: admin@mail.com
password: admin123
