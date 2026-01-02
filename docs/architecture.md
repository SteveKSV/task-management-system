# Task Management System - Architecture

This document describes the overall architecture of the Task Management System, a full-stack web application for managing tasks using a Kanban board. 

It explains the frontend, backend layers, database, and communication between components.



### System Overview

1\. Frontend (React.js)

&nbsp;  - Single Page Application (SPA)

&nbsp;  - Handles user interactions, displays projects, boards, and tasks

&nbsp;  - Communicates with Backend via REST API



2\. Backend (ASP.NET Core Web API)

&nbsp;  - Provides RESTful endpoints

&nbsp;  - Contains business logic and data access

&nbsp;  - Handles authentication and authorization



3\. Database (SQL Server)

&nbsp;  - Stores users, projects, tasks, comments, statuses

&nbsp;  - Accessed via Entity Framework Core



### Frontend Architecture

\- Pages

&nbsp; - Login / Register

&nbsp; - Projects List

&nbsp; - Kanban Board (To Do / In Progress / Done)

&nbsp; - Task Details

\- Components

&nbsp; - TaskCard

&nbsp; - Column

&nbsp; - Header / Footer

\- Communication

&nbsp; - API calls using Axios or Fetch. 



### Backend Architecture

#### Layers

1\. API Layer

&nbsp;  - Controllers handle HTTP requests.

&nbsp;  - Validates input data.

&nbsp;  - Returns JSON responses.



2\. Business Logic Layer (BLL)

&nbsp;  - Implements business rules.

&nbsp;  - Coordinates multiple repositories.

&nbsp;  - Uses DTOs to transfer data between layers.



3\. Data Access Layer (DAL)

&nbsp;  - Entity Framework Core DbContext.

&nbsp;  - Repository pattern to manage CRUD operations.

&nbsp;  - Database models / migrations.



#### DTOs, Services, Repositories

\- DTOs (Data Transfer Objects)

&nbsp; - Simplify data sent to the frontend

&nbsp; - Avoid exposing internal DB structure



\- Services

&nbsp; - Contain core business logic

&nbsp; - Called from Controllers

&nbsp; - Examples: ProjectService, TaskService, UserService



\- Repositories

&nbsp; - Encapsulate data access

&nbsp; - Examples: ProjectRepository, TaskRepository



#### Database

\- Users: Id, Name, Email, PasswordHash.

\- Projects: Id, Name, OwnerId.

\- Tasks: Id, Title, Description, StatusId, ProjectId, Priority, DueDate.

\- TaskStatuses: Id, Name (To Do / In Progress / Done).

\- Comments: Id, TaskId, UserId, Content, CreatedAt.

