# PatientManagement-DAC

## Overview

The Patient Management System is a web application designed to manage patient information efficiently. The project is built with a modern tech stack, including .NET for the backend and HTML, CSS, Bootstrap, and JavaScript for the frontend.

## Features

- **Authentication**: JWT-based authentication with Bearer tokens.
- **CRUD Operations**: Create, Read, Update, and Delete operations for patient records.
- **Responsive UI**: Simple and responsive user interface built with Bootstrap.

## Tech Stack

- **Backend**: ASP.NET Core 8, Dapper, SQL Server
- **Frontend**: HTML, CSS, Bootstrap, JavaScript
- **Authentication**: JWT (JSON Web Tokens)
- **Repository Pattern**: Generic repository for CRUD operations
- **Architecture**: Clean Architecture

## Project Structure

### Backend

- **Controllers**: Handle HTTP requests and responses.
- **Services**: Business logic and validation.
- **Repositories**: Data access layer using Dapper.
- **DTOs**: Data Transfer Objects for communication between layers.
- **Configuration**: JWT and other configurations.

### Frontend

- **HTML**: Structure of the web pages.
- **CSS**: Styling and layout.
- **JavaScript**: Interactivity and AJAX calls for CRUD operations.
- **Bootstrap**: Responsive design components.

## Setup and Installation

### Backend

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-username/patient-management-system.git
