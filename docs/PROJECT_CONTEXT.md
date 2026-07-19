# PeopleHub Project Context

> Upload this document when starting a new ChatGPT conversation.
> It contains the current architecture, implementation status, coding standards, and development workflow.

---

# Project Overview

PeopleHub is a production-grade service marketplace platform built using Clean Architecture.

The platform connects customers with verified service providers and will support provider services, bookings, SmartMatch, messaging, notifications, payments, and administration.

---

# Technology Stack

## Backend

- ASP.NET Core 9
- .NET 9
- Entity Framework Core 9
- SQL Server 2022

## Architecture

- Clean Architecture
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

```
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

1. Start every response with a roadmap.
2. Show the exact file path before every code change.
3. Implement one logical change at a time.
4. Build after every change.
5. Fix build errors before continuing.
6. Run tests after completing a feature.
7. Preserve the existing architecture.
8. Update both PROJECT_CONTEXT.md and API_ROADMAP.md after each milestone.

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

- Role enum
- JWT Role Claim
- Admin Authorization
- Protected Admin APIs

Verified

- User → 403
- Admin → 200

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

## Provider Profile

Completed

- Create
- Get
- Update
- Delete

Repository, Service, Controller, Unit Tests, Integration Tests completed.

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

# Testing

Completed

- xUnit
- Integration Tests
- SQLite In-Memory
- Database Reset between Tests

Current Status

```
28 Tests Passed
0 Failed
```

---

# Build Status

Latest Build

```
dotnet build
```

Status

```
SUCCESS
```

---

# Current Progress

Estimated Completion

**45%**

Current Focus

**Provider Services Module**

---

# AI Working Agreement

When continuing this project:

- Never recreate completed work.
- Continue from the Current Focus section.
- Follow Repository → Service → Controller → Tests.
- Never introduce MediatR.
- Never introduce CQRS.
- Never introduce AutoMapper.
- Never introduce Generic Repository.
- Show file paths before every code change.
- Build after every logical change.
- Maintain passing builds and tests.
- Keep the implementation consistent with the existing codebase.