# Project Management Backend API

## Overview

This is the backend for the Project Management Application built using .NET Core. It provides RESTful API services for managing projects, tasks, and user roles. The backend implements role-based access control and includes features for tracking overdue tasks.

## Table of Contents

- [Technologies Used](#technologies-used)
- [API Endpoints](#api-endpoints)
- [Installation](#installation)
- [Configuration](#configuration)
- [Running the Application](#running-the-application)
- [Testing](#testing)
- [Contributing](#contributing)
- [License](#license)

## Technologies Used

- **Framework**: .NET Core
- **Database**: SQL Server (Entity Framework for ORM)
- **Authentication**: JWT (JSON Web Tokens)
- **Dependency Injection**: Built-in .NET Core DI

## API Endpoints

### Authentication

- **POST /auth/login**
  - Description: Authenticate a user and return a JWT token.
  - Body: 
    ```json
    {
      "username": "string",
      "password": "string"
    }
    ```

### Projects

- **GET /api/project**
  - Description: Retrieve all projects.
  
- **POST /api/project**
  - Description: Create a new project.
  - Body: 
    ```json
    {
      "projectName": "string",
      "description": "string",
      "startDate": "2024-01-01T00:00:00Z",
      "endDate": "2024-12-31T00:00:00Z",
      "budget": 1000,
      "owner": "string",
      "status": "string"
    }
    ```

- **PUT /api/project/{id}**
  - Description: Update an existing project.
  - Path Parameter: `id` - Project ID.
  - Body: Same as above.

- **DELETE /api/project/{id}**
  - Description: Delete a project by ID.

### Tasks

- **GET /api/task/project/{projectId}**
  - Description: Retrieve all tasks associated with a specific project.

- **POST /api/task/project/{projectId}**
  - Description: Create a new task under a specific project.
  - Body:
    ```json
    {
      "taskName": "string",
      "description": "string",
      "assignedTo": "string",
      "startDate": "2024-01-01T00:00:00Z",
      "endDate": "2024-01-31T00:00:00Z",
      "priority": "string",
      "status": "string"
    }
    ```

- **PUT /api/task/{id}**
  - Description: Update an existing task.
  - Path Parameter: `id` - Task ID.
  - Body: Same as above.

- **DELETE /api/task/{id}**
  - Description: Delete a task by ID.

### Overdue Tasks

- **GET /api/task/overdue**
  - Description: Retrieve overdue tasks.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/kareemElbadawy/ProjectManagementApp2.git
