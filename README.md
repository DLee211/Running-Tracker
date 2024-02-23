# Exercise Tracker Application

## Introduction

Welcome to the Exercise Tracker application! This project aims to provide a simple example of utilizing the Repository Pattern for data persistence in software development, particularly focusing on building a C# application. The Repository Pattern serves as a bridge between the business logic of the application and the data access layer, promoting loose coupling, testability, and maintainability.

## Requirements

- **Recording Exercise Data**: The application facilitates the recording of exercise data, focusing on a single type of exercise to keep the application straightforward.
- **Entity Framework**: Utilizes Entity Framework for data access and management.
- **Exercise Model**: The exercise model includes the following properties:
  - `Id` (INT): Unique identifier for each exercise entry.
  - `DateStart` (DateTime): Date and time when the exercise started.
  - `DateEnd` (DateTime): Date and time when the exercise ended.
  - `TimeSpan` (String): Duration of the exercise.
  - `Comments` (String): Additional comments or notes related to the exercise.
- **Application Classes**: The application consists of the following classes:
  - `ExerciseController`: Responsible for handling HTTP requests, interacting with services, and returning responses.
  - `ExerciseService`: Manages the business logic related to exercises.
  - `ExerciseRepository`: Implements data access methods using the Repository Pattern.
- **MVC Razor**: The project utilizes MVC Razor for server-side rendering of views.
- **SQL Server**: The application employs SQL Server for database management.
- **Dependency Injection**: Dependency injection is used to access the repository from the controller, promoting modularity and maintainability.

## Usage

To use the Exercise Tracker application, follow these steps:

1. **Clone the Repository**
2. **Setup Database**: Ensure that you have SQL Server installed and running. Setup your DBconnection on appsettings.json
3. **Run the Application**
