# PeopleHub Project Context

> Upload this document when starting a new ChatGPT conversation.
> It contains the current architecture, implementation status, coding standards, and development workflow.

---

# Project Overview

PeopleHub is a production-grade service marketplace platform built using Clean Architecture.

The platform connects customers with verified service providers and will support provider services, service requests, reviews, SmartMatch, messaging, notifications, payments, and administration.

---

# Technology Stack

## Backend

- ASP.NET Core 9
- .NET 9
- Entity Framework Core 9
- SQL Server 2022

## Architecture

- Clean Architecture
- Domain Driven Design (DDD)
- Repository Pattern
- Service Layer
- Unit of Work

## Security

- JWT Authentication
- Refresh Tokens
- Role-Based Authorization (RBAC)

## Validation

- FluentValidation

## Testing

- xUnit
- Integration Tests
- SQLite In-Memory Database

## API

- Swagger / OpenAPI

## Frontend

- Angular 20

## Mobile

- Flutter

---

# Solution Structure

```text
PeopleHub
│
├── docs
├── src
│   ├── PeopleHub.API
│   ├── PeopleHub.Application
│   ├── PeopleHub.Contracts
│   ├── PeopleHub.Domain
│   ├── PeopleHub.Infrastructure
│   ├── PeopleHub.Common
│   └── PeopleHub.SmartMatch
│
└── tests
    ├── PeopleHub.UnitTests
    └── PeopleHub.IntegrationTests
```

---

# Architecture Rules

Always follow these rules.

## Required

- Clean Architecture
- Domain Driven Design (DDD)
- Repository Pattern
- Service Layer
- Unit of Work
- EF Core
- Explicit object mapping
- One class per file
- Constructor Injection
- Async APIs
- CancellationToken support

## Never Use

- MediatR
- CQRS
- AutoMapper
- Generic Repository
- Placeholder implementations
- TODO implementations

---

# Development Workflow

Always:

1. Start every response with **Progress**.
2. Show the exact file path before every code change.
3. Implement one logical change at a time.
4. Build after every change.
5. Fix build errors before continuing.
6. Run tests after completing a feature.
7. Preserve the existing architecture.
8. Update both `PROJECT_CONTEXT.md` and `API_ROADMAP.md` after each completed module.

---

# Completed Modules

## Infrastructure

Completed

- Repository Pattern
- Unit of Work
- EF Core
- ApplicationDbContext
- Entity Configurations
- JWT Authentication
- JWT Authorization

---

## Authentication

Completed

- Register
- Login
- Refresh Token
- Refresh Token Rotation
- Logout
- `/api/auth/me`

---

## RBAC

Completed

- Role Enum
- JWT Role Claim
- Admin Authorization

Verified

- User → 403 Forbidden
- Admin → 200 OK

---

## User Management

Completed

- Create User
- Get Current User
- Get User By Id
- Get All Users
- Update User
- Delete User

---

## Service Categories

Completed

- Create
- Update
- Delete
- Get
- Repository
- Service
- Controller
- Swagger Testing

---

## Provider Profile

Completed

- Create
- Get
- Update
- Delete

Repository, Service, Controller, Unit Tests, Integration Tests completed.

---

## Provider Skills

Completed

- Add Skill
- Remove Skill
- List Skills

---

## Provider Verification

Completed

- Domain Entity
- Repository
- Service
- Dependency Injection
- Controller
- Unit Tests
- Integration Tests

---

## Provider Availability

Completed

- Create Availability
- Update Availability
- Delete Availability
- Get Provider Availability
- Repository
- Service
- Controller
- Swagger Testing

---

## Service Requests

Completed

### Domain

- ServiceRequest Entity
- ServiceRequestStatus Enum

### Persistence

- Repository
- EF Core Configuration
- Migration

### Application

- Contracts
- Service Layer
- Dependency Injection

### API

- Controller
- Swagger

### Verified End-to-End

- Create Service Request
- Get By Id
- Get Customer Requests
- Get Provider Requests
- Accept Request
- Complete Request

---

# Testing

Completed

- xUnit
- Integration Tests
- SQLite In-Memory
- Database Reset between Tests

Current Status

```text
28 Tests Passed
0 Failed
```

---

# Build Status

Latest Build

```text
dotnet build
```

Status

```text
SUCCESS
```

---

# Current Progress

Estimated Completion

**60%**

Current Focus

**Provider Reviews & Ratings Module**

---

# AI Working Agreement

When continuing this project:

- Never recreate completed work.
- Continue from the Current Focus section.
- Follow Domain → Repository → Service → Controller → Tests.
- Never introduce MediatR.
- Never introduce CQRS.
- Never introduce AutoMapper.
- Never introduce Generic Repository.
- Show file paths before every code change.
- Build after every logical change.
- Maintain passing builds and tests.
- Update both roadmap documents after each completed module.
- Keep the implementation consistent with the existing codebase.