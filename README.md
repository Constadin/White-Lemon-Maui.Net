# White Lemon

## Social Networking Application

**White Lemon** is an independent full-stack social networking application built with **C#**, **ASP.NET Core Web API**, and **.NET MAUI**.

The application combines a cross-platform mobile client with a layered backend architecture, supporting user authentication, social discovery, post interaction, and profile management.

> **Project type:** Independent Full-Stack Software Project

---

## Application Preview

### Main interface

![White Lemon application overview](https://github.com/user-attachments/assets/1a5d2bf6-b13f-4adc-976e-3e817af343cc)

### Mobile screens

<p align="center">
  <img src="https://github.com/user-attachments/assets/b02ec24a-3577-41de-82a4-d02db352450b" width="23%" alt="White Lemon mobile screen 1" />
  <img src="https://github.com/user-attachments/assets/2f934e1f-aeaf-4b76-bf31-d73ab06412de" width="23%" alt="White Lemon mobile screen 2" />
  <img src="https://github.com/user-attachments/assets/481eb2e1-fe55-4633-98e4-2f20b1ddb6b6" width="23%" alt="White Lemon mobile screen 3" />
  <img src="https://github.com/user-attachments/assets/3dc71020-3557-4542-902e-e8d066b75751" width="23%" alt="White Lemon mobile screen 4" />
</p>

---

## Overview

White Lemon was designed as a modular social application with clear separation between the mobile user interface, application use cases, domain logic, infrastructure services, and HTTP API.

The solution demonstrates end-to-end application development, including:

- Cross-platform mobile UI development
- Backend API development
- User authentication workflows
- Social feed and post interactions
- User discovery and profile management
- Layered software architecture
- Shared communication models between client and server

---

## Core Features

- User registration and login
- Social post feed
- Top-rated post display
- Create and publish posts
- Like, share, save, and comment interactions
- Discover suggested users
- User connection and friendship actions
- User profile management
- Personal posts and bookmarked content
- Cross-platform mobile experience
- Client-to-API communication

---

## Technologies

- **C#**
- **.NET**
- **.NET MAUI**
- **XAML**
- **ASP.NET Core Web API**
- **REST APIs**
- **Dependency Injection**
- **Layered Architecture**
- **Object-Oriented Programming**

---

## Solution Architecture

The solution is divided into separate projects, with each project having a specific responsibility.

```text
WhiteLemon/
├── WhiteLemon.API/
├── WhiteLemon.Application/
├── WhiteLemon.Domain/
├── WhiteLemon.Infrastructure/
├── WhiteLemon.Shared/
├── WhiteLemonMauiUI/
├── .gitattributes
├── .gitignore
├── LICENSE.txt
└── README.md
```

### `WhiteLemon.Domain`

Contains the core domain model and business rules.

Typical responsibilities include:

- Domain entities
- Value objects
- Business rules
- Domain-level validation

The Domain project should remain independent from the UI, API, and infrastructure implementations.

### `WhiteLemon.Application`

Contains the application workflows, use cases, contracts, and abstractions required by the system.

Typical responsibilities include:

- Application services
- Use cases
- Interfaces
- Validation
- Data-transfer orchestration

### `WhiteLemon.Infrastructure`

Provides concrete implementations for the abstractions defined by the Application layer.

Typical responsibilities include:

- Data persistence
- Repository implementations
- External services
- Authentication infrastructure
- API-related integrations

### `WhiteLemon.API`

Acts as the backend HTTP entry point of the application.

Typical responsibilities include:

- API endpoints
- Request and response handling
- Authentication and authorization
- Dependency registration
- Application configuration

### `WhiteLemon.Shared`

Contains models and contracts shared between the backend and the .NET MAUI client.

Typical responsibilities include:

- Request models
- Response models
- Data transfer objects
- Shared constants

### `WhiteLemonMauiUI`

Contains the cross-platform mobile client developed with .NET MAUI.

Main responsibilities include:

- Application pages
- Reusable UI components
- Navigation
- User interaction
- API communication
- Mobile presentation logic

---

## Dependency Direction

```text
WhiteLemonMauiUI
        |
        v
   WhiteLemon.API
        |
        v
WhiteLemon.Application
        |
        v
 WhiteLemon.Domain

WhiteLemon.Infrastructure
        |
        └── Implements Application abstractions

WhiteLemon.Shared
        |
        └── Shared communication contracts
```

This separation improves maintainability, testability, and the ability to replace infrastructure or UI implementations without changing the core business logic.

---

## Getting Started

### Prerequisites

Install the following tools:

- Visual Studio 2022
- .NET SDK compatible with the solution
- .NET MAUI workload
- Android emulator or physical Android device
- A configured backend environment

### Clone the repository

```bash
git clone https://github.com/Constadin/White-Lemon.git
cd White-Lemon
```

> Replace the repository URL above if the GitHub repository uses a different name.

### Restore dependencies

```bash
dotnet restore
```

### Build the solution

```bash
dotnet build
```

### Run the backend API

```bash
dotnet run --project WhiteLemon.API
```

### Run the mobile application

1. Open the solution in Visual Studio 2022.
2. Set `WhiteLemonMauiUI` as the startup project.
3. Select an Android emulator or connected device.
4. Confirm that the application's API base address points to the running backend.
5. Start the application.

When running the backend locally with an Android emulator, the API may need to use the emulator-accessible host address rather than `localhost`.

---

## Application Flow

```text
User
  |
  v
.NET MAUI Mobile UI
  |
  v
REST API Request
  |
  v
ASP.NET Core API
  |
  v
Application Use Case
  |
  v
Domain Logic
  |
  v
Infrastructure / Persistence
  |
  v
API Response
  |
  v
Updated Mobile UI
```

---

## Engineering Goals

The project focuses on:

- Clear separation of concerns
- Modular project organization
- Reusable mobile UI components
- Maintainable client-server communication
- Domain isolation
- Interface-based infrastructure design
- Scalable feature development
- Professional end-to-end application structure

---

## Future Improvements

Potential future development may include:

- Real-time notifications
- Direct messaging
- Media upload and processing
- Advanced search and filtering
- Push notifications
- Improved account privacy controls
- Moderation and reporting tools
- Automated testing
- Offline synchronization
- Performance monitoring
- CI/CD workflows

---

## Project Status

This project is an independent, professional-grade software development project created for portfolio development, experimentation, and continued technical improvement.

It is not presented as self-employed or client-commissioned work.

---

## Author

**Nikolas Konstantinidis**

Full-Stack .NET Developer

- GitHub: [Constadin](https://github.com/Constadin)
- LinkedIn: [Nikos Konstantinidis](https://www.linkedin.com/in/nikos-konstantinidis-developer/)

---

## License

This project includes a `LICENSE.txt` file.

Refer to that file for the applicable terms and conditions.
