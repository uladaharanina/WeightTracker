# Weight Tracker

## Overview

The **Weight Tracker** is a web application that helps users track their weight over time. It allows users to:

- Add new weight entries.
- Edit and delete existing entries.
- View weight history and generate reports on their progress.

The application is built using **Microservices Architecture**, with two main microservices:

1. **Tracker Microservice** - Handles user weight data (add, edit, delete).
2. **Report Microservice** - Generates and provides reports on the user's weight progress over time.

---

## Features

### Tracker Microservice

- **Add Weight**: Allows the user to add a new weight entry with a timestamp.
- **Edit Weight**: Allows the user to modify an existing weight entry.
- **Delete Weight**: Allows the user to remove a weight entry.

### Report Microservice

- **Generate Reports**: Allows the user to generate weight progress reports.
  - Weekly, Monthly, or Custom Date Range reports.
- **Track Trends**: Provides insights into the user’s weight trend over a specified time period.

---

## Technologies Used

- **Backend**:
  - .NET Core for both microservices (Tracker and Report).
  - Entity Framework Core for database interactions.
  - RESTful APIs for communication between microservices.
- **Database**:
  - SQL Server for storing weight records.
- **Other Tools**:
  - Docker
  - Ocelot for API Gateway
- **Microservices Communication**:
  - RESTful API for communication between the Tracker and Report services.

---

## Installation

### Prerequisites

- .NET Core SDK
- Docker
- SQL Server
