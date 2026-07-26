# PeopleHub - Project Context

## Project Overview

PeopleHub is a production-grade Service Marketplace platform that connects customers with service providers.

Examples:

- Electrician
- Plumber
- Carpenter
- AC Technician
- Painter
- Home Cleaning
- Car Wash
- Delivery Services
- Freelance Services
- Other local services

Future modules include:

- SmartMatch
- Real-time bidding
- Wallet
- Payments
- Notifications
- Live Location Tracking
- Ratings & Reviews
- Provider Verification
- Admin Portal

---

# Architecture

Backend

- ASP.NET Core 9 Web API
- Clean Architecture
- Repository Pattern
- Service Layer
- Unit of Work
- Entity Framework Core
- SQL Server
- JWT Authentication
- Refresh Tokens
- xUnit
- Moq
- FluentAssertions

Frontend

- React Native
- Expo SDK 54
- TypeScript
- React Navigation
- Context API
- Axios
- AsyncStorage

Development

- GitHub
- GitHub Codespaces
- VS Code
- Git

---

# Coding Rules

Always follow existing project conventions.

Do NOT introduce:

- CQRS
- MediatR
- AutoMapper
- Generic Repository

Use:

- Repository Pattern
- Service Layer
- Dependency Injection
- Clean Architecture

Every feature must include

- Domain
- Application
- Infrastructure
- API
- Unit Tests
- Integration Tests

Keep the solution compiling after every logical change.

---

# Current Backend Status

## Authentication

Completed

- Register
- Login
- Refresh Token
- Logout
- Current User
- JWT Authentication
- Password Hashing
- OTP Generation
- OTP Verification
- User Activation
- Email Verification

OTP supports

- Registration
- Phone Verification
- Forgot Password
- Two Factor Authentication

Completed Tests

- Authentication Controller Tests
- OTP Service Tests

Current Test Status

- Build Successful
- 169 Unit Tests Passed
- Integration Tests Passing

---

# Provider Module

Completed

- Provider Profile
- Provider Services
- Provider Availability
- Provider Skills
- Provider Verification
- Provider Dashboard
- Provider Reviews
- Provider Search
- Service Categories
- Service Requests

Infrastructure completed

- EF Configurations
- Repositories
- Controllers
- Services
- DTOs
- Validators
- Tests

---

# Payments

Completed

- Wallet Entity
- Wallet APIs
- Payment APIs
- Configurations
- Tests

---

# Notifications

Infrastructure created.

Email/SMS providers still pending.

---

# SmartMatch

Foundation completed.

Future work

- Auto Provider Matching
- Provider Ranking
- Provider Bidding
- Intelligent Selection

---

# Frontend Status

Completed Screens

Authentication

- Login
- Register
- OTP Verification
- Forgot Password
- Reset Password

Provider

- Registration
- Dashboard (basic)
- Services (basic)

Shared Components

- PrimaryButton
- Input Components
- Theme
- Navigation
- API Layer

Pending Frontend

- Wallet
- Payments
- SmartMatch
- Provider Dashboard
- Notifications
- Live Tracking

---

# Development Workflow

After every feature

1. Build
2. Run Unit Tests
3. Run Integration Tests
4. Commit
5. Push

Commit format

feat(...)
fix(...)
refactor(...)
test(...)
docs(...)

---

# Current Branch

main

Latest completed milestone

Authentication + OTP completed with full unit test coverage.

Next milestone

Forgot Password APIs.