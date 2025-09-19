# 📚 LibraryWeb

**LibraryWeb** is a web application for simulating a library system.  
The project consists of a backend API built with **.NET 8 (Clean Architecture, EF Core, JWT Auth)** and a frontend client built with **Angular**.  
The system supports book and author management, user authentication, borrowing books, and admin features.  
The application runs inside **Docker** (backend + frontend + PostgreSQL).

---

## ✨ Features

### 🔹 Backend (Web API)
- **Books management**:
  - Get all books
  - Get book by Id / ISBN
  - Add, update, delete book
  - Upload and store book images
  - Borrow books by users
  - *Send notification when the due date expires* ✅
- **Authors management**:
  - Get all authors
  - Get author by Id
  - Add, update, delete author
  - Get all books by author
- **Authentication & Authorization**:
  - Registration & login
  - JWT access & refresh tokens (policy-based authorization)

### 🔹 Frontend (Angular)
- User registration and authentication
- View list of books with:
  - Pagination
  - Search by title
  - Filter by genre/author
- Book details page:
  - Borrow a book if available
  - Show status if not available
- Admin panel:
  - Add/edit/delete books
  - Confirmation modal before deletion
- User page: view borrowed books

---

## 🛠 Tech Stack

### 🔹 Backend
- **Framework:** .NET 8, ASP.NET Core Web API  
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API layers)  
- **ORM / Database:** Entity Framework Core (Fluent API) + PostgreSQL  
- **Mapping:** AutoMapper  
- **Validation:** FluentValidation  
- **Authentication & Authorization:** ASP.NET Identity + JWT access & refresh tokens (policy-based)  
- **Exception Handling:** Global middleware for exception handling  
- **Unit Testing:** xUnit 
- **Documentation:** Swagger / OpenAPI  
- **Docker:** Dockerfile for backend, Docker Compose for multi-container setup  

### 🔹 Frontend
- **Framework:** Angular 17+  
- **State Management:** RxJS / Services (or NgRx if used)  
- **Routing:** Angular Router  
- **Forms & Validation:** Reactive Forms + client-side validation  

### 🔹 DevOps / Tools
- **Containerization:** Docker + Docker Compose (backend + frontend + PostgreSQL)  
- **Version Control:** Git  
- **IDE / Editor:** Visual Studio / VS Code  
- **Database Migrations:** EF Core Migrations  
- **API Testing:** Swagger UI / Postman (optional)  

### 🔹 Optional / Future Enhancements
- Email notifications for due books  
- Role-based enhancements for admin panel

---
## 🏗 Architecture

The project follows **Clean Architecture** with the following layers:

- **Domain** – Entities, value objects, business rules  
- **Application** – Use cases, services, validation, DTOs  
- **Infrastructure** – EF Core, PostgreSQL, repositories, authentication, file storage  
- **Presentation (API)** – Controllers, Swagger, global exception middleware  
- **Frontend (Angular)** – SPA client for users & admins  

---

## 🛠 Tech Stack

- **Backend**: .NET 8, ASP.NET Core Web API  
- **Database**: PostgreSQL, EF Core (Fluent API)  
- **ORM**: Entity Framework Core  
- **Mapping**: AutoMapper  
- **Validation**: FluentValidation  
- **Authentication**: ASP.NET Identity + JWT access & refresh tokens  
- **Documentation**: Swagger / OpenAPI  
- **Testing**: xUnit (unit tests for services and repository methods)  
- **Frontend**: Angular 17+  
- **Containerization**: Docker & Docker Compose  

---

## 🚀 Getting Started

### Prerequisites
- Docker
- Docker Compose

### Installation & Run
```bash
git clone https://github.com/Olewwwka/LibraryWeb
cd LibraryWeb
docker-compose up --build
```

After starting, the application will be available at:
- **API**: http://localhost:5000
- **Frontend**: http://localhost:4200/login
- **Swagger UI**: http://localhost:5000/swagger/index.html

## 🔐 Test Users
| Role       | Email              | Password    |
|------------|--------------------|-----------|
| Admin | admin@library.com | Admin123! |
| User   | user@library.com  | User123!  |
