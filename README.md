<p align="left"><h1 align="left">ADDICTION-REHABILITATION-BACKEND</h1></p>

<p align="left">
	<img src="https://img.shields.io/github/languages/top/amagdykhalil/addiction-rehabilitation-backend?style=plastic&color=0080ff" alt="repo-top-language">
	<img src="https://img.shields.io/github/languages/count/amagdykhalil/addiction-rehabilitation-backend?style=plastic&color=0080ff" alt="repo-language-count">
</p>
<p align="left">Built with the tools and technologies:</p>
<p align="left">
	<img src="https://img.shields.io/badge/ASP.NET%20Core-512BD4.svg?style=plastic&logo=.net&logoColor=white" alt="ASP.NET Core">
	<img src="https://img.shields.io/badge/C%23-239120.svg?style=plastic&logo=c-sharp&logoColor=white" alt="C#">
	<img src="https://img.shields.io/badge/Entity%20Framework%20Core-512BD4.svg?style=plastic&logo=.net&logoColor=white" alt="Entity Framework Core">
	<img src="https://img.shields.io/badge/SQL%20Server-CC2927.svg?style=plastic&logo=microsoftsqlserver&logoColor=white" alt="SQL Server">
	<img src="https://img.shields.io/badge/Docker-2496ED.svg?style=plastic&logo=Docker&logoColor=white" alt="Docker">
	<img src="https://img.shields.io/badge/JWT-000000.svg?style=plastic&logo=jsonwebtokens&logoColor=white" alt="JWT">
	<img src="https://img.shields.io/badge/Clean%20Architecture-6DB33F.svg?style=plastic&logo=archlinux&logoColor=white" alt="Clean Architecture">
	<img src="https://img.shields.io/badge/xUnit-5FA04E.svg?style=plastic&logo=xunit&logoColor=white" alt="xUnit">
	<img src="https://img.shields.io/badge/Bogus-ED8B00.svg?style=plastic&logo=data&logoColor=white" alt="Bogus">
</p>
<br>

## 🚀 Live Demo

[arc-care.netlify.app/login](https://arc-care.netlify.app/login)

## 🔗 Table of Contents

- [📍 Overview](#-overview)
- [📁 Project Structure](#-project-structure)
- [🚀 Getting Started](#-getting-started)
  - [☑️ Prerequisites](#-prerequisites)
  - [⚙️ Installation](#-installation)
  - [🤖 Usage](#🤖-usage)
  - [🧪 Testing](#🧪-testing)
- [📌 Project Roadmap](#-project-roadmap)
- [📑 API Documentation](#-api-documentation)
- [🔑 Test User Credentials](#-test-user-credentials)
- [📊 Logging](#-logging)
- [🔰 Contributing](#-contributing)

---

## 📍 Overview

Addiction Rehabilitation Backend is a RESTful API designed to streamline every stage of patient care—from admission intake and treatment planning to discharge and aftercare.
It supports both inpatient and outpatient treatment paths, therapist session tracking, and progress metrics.
Handles dynamic admission forms, multi‑modal treatment workflows, and comprehensive discharge planning.
Provides a unified platform for managing the patient care in addiction rehabilitation centers.

---

## 📁 Project Structure

```sh
└── addiction-rehabilitation-backend/
    ├── ARC.sln
    ├── README.md
    ├── docker-compose.dcproj
    ├── docker-compose.override.yml # Docker Compose override for local development
    ├── docker-compose.yml
    ├── launchSettings.json
    ├── src
    │   ├── API                  # ASP.NET Core Web API project
    │   ├── ARC.Shared           # Shared localization resources, and email templates
    │   ├── Core
    │   │   ├── ARC.Application  # Application layer: business logic, CQRS, MediatR, validation
    │   │   └── ARC.Domain       # Domain layer: entities, enums, core business models
    │   └── Infrastructure
    │       ├── ARC.Infrastructure # Infrastructure services: email, localization, authentication
    │       └── ARC.Persistence    # Persistence layer: Entity Framework, repositories, migrations
    └── tests
        ├── ARC.IntegrationTests  # Integration tests for infrastructure and persistence layers
        ├── ARC.Tests.Common      # Shared test utilities and data generators
        └── ARC.UnitTests
```

---

## 🚀 Getting Started

### ☑️ Prerequisites

Before getting started with addiction-rehabilitation-backend, ensure your environment meets the following requirements:

- **.NET 9 SDK**
- **Docker** (for running integration tests)
- **SQL Server** (local instance or Docker for testing)

### ⚙️ Installation

Install addiction-rehabilitation-backend using one of the following methods:

**Build from source:**

1. Clone the addiction-rehabilitation-backend repository:

```sh
❯ git clone https://github.com/amagdykhalil/addiction-rehabilitation-backend
```

2. Navigate to the project directory:

```sh
❯ cd addiction-rehabilitation-backend
```

3. Install the project dependencies:

**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet restore
```

4. Run database migrations (to set up the database schema):

```sh
❯ dotnet ef database update
```

---

### 🤖 Usage

Run addiction-rehabilitation-backend using the following command:

**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet run
```

**Using `docker-compose`** &nbsp; [<img align="center" src="https://img.shields.io/badge/Docker%20Compose-2496ED.svg?style=plastic&logo=docker&logoColor=white" />](https://docs.docker.com/compose/)

```sh
❯ docker-compose up --build
```

- This will build and start all services defined in your `docker-compose.yml` (API, database, etc.).
- You can stop the services with:
  ```sh
  ❯ docker-compose down
  ```

### 🧪 Testing

> **Note:** Before running integration tests, make sure Docker is running.

Run the test suite using the following command:

**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet test
```

---

## 📌 Project Roadmap

- [x] **Patient Data Management**: lifecycle management for patient records
- [x] **User Data Management**: lifecycle management for Users, and roles
- [x] **Profile Management**: User profile editing and viewing
- [x] **Authentication & Security**: Login, logout, password reset, email confirmation, and forgot password flows
- [ ] **Dynamic Admission Forms**: Edit and manage the structure of admission forms
- [ ] **Patient Admission & Review**: Handle patient admission forms and review results
- [ ] **Inpatient Treatment Process**: Support for managing inpatient treatment workflows (not yet defined)
- [ ] **Outpatient Treatment Process**: Support for managing outpatient treatment workflows (not yet defined)
- [ ] **Discharge Process**: Comprehensive discharge planning and workflow (not yet defined)

---

## 📑 API Documentation

Interactive API documentation is available via Scalar:

- **URL:** `/scalar/v1`
- Use this to explore and test all available API endpoints.

---

## 🔑 Test User Credentials

You can test authorized endpoints using the following credentials:

- **Email:** `ahmed.magdy.dev9@gmail.com`
- **Password:** `Admin@123`

---

## 📊 Logging

Application logs are managed and visualized using **Seq**:

- **Port:** `5173`
- Visit this address to view, search, and analyze logs in real time.

---

## 🔰 Contributing

- **🐛 [Report Issues](https://github.com/amagdykhalil/addiction-rehabilitation-backend/issues)**: Submit bugs found or log feature requests for the `addiction-rehabilitation-backend` project.
- **💡 [Submit Pull Requests](https://github.com/amagdykhalil/addiction-rehabilitation-backend/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.

<details closed>
<summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your github account.
2. **Clone Locally**: Clone the forked repository to your local machine using a git client.
   ```sh
   git clone https://github.com/amagdykhalil/addiction-rehabilitation-backend
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Install Dependencies**: Restore NuGet packages.
   ```sh
   dotnet restore
   ```
5. **Run Database Migrations**: Ensure your local database is up to date.
   ```sh
   dotnet ef database update
   ```
6. **Make Your Changes**: Develop and test your changes locally.
7. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
8. **Push to github**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
9. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.
10. **Review**: Once your PR is reviewed and approved, it will be merged into the main branch. Congratulations on your contribution!
</details>

<details closed>
<summary>Contributor Graph</summary>
<br>
<p align="left">
   <a href="https://github.com/amagdykhalil/addiction-rehabilitation-backend/graphs/contributors">
      <img src="https://contrib.rocks/image?repo=amagdykhalil/addiction-rehabilitation-backend">
   </a>
</p>
</details>

---
