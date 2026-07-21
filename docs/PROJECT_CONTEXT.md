# PeopleHub Project Context

> Version: 1.0
>
> Last Updated: July 2026
>
> This document is the primary reference for the PeopleHub backend project.
> Every new development session should begin by reading this document together with `docs/API_ROADMAP.md`.

---

# Project Overview

PeopleHub is a production-grade service marketplace platform that connects customers with verified service providers.

The platform is designed around Clean Architecture and Domain-Driven Design (DDD), providing a scalable, maintainable, and testable codebase suitable for enterprise-level applications.

The long-term vision is to support:

- Customer & Provider Accounts
- Service Categories
- Provider Services
- Service Requests
- Reviews & Ratings
- SmartMatch Provider Recommendation
- Messaging
- Notifications
- Payments
- Administration Portal
- Mobile Applications

---

# Technology Stack

## Backend

- ASP.NET Core 9
- .NET 9
- Entity Framework Core 9
- SQL Server 2022

## API

- REST API
- OpenAPI / Swagger

## Security

- JWT Authentication
- Refresh Tokens
- Role-Based Authorization (RBAC)

## Validation

- FluentValidation

## Logging

- Microsoft Logging
- Serilog (Planned)

## Testing

- xUnit
- FluentAssertions
- SQLite In-Memory Database
- Integration Testing

## Frontend

- Angular

## Mobile

- Flutter

---

# Architecture

PeopleHub follows:

- Clean Architecture
- Domain Driven Design (DDD)
- Repository Pattern
- Service Layer
- Unit of Work

The architecture emphasizes:

- Separation of concerns
- Dependency inversion
- Testability
- Maintainability
- Explicit object mapping
- Feature isolation

---

# Architecture Principles

## Domain First

Business logic belongs inside the Domain layer.

Infrastructure must never contain business rules.

---

## Repository Pattern

Repositories are responsible only for data access.

Repositories never contain business logic.

---

## Service Layer

Application Services coordinate business operations.

Services:

- validate input
- call repositories
- coordinate Unit of Work
- return DTOs

---

## Unit of Work

All write operations must be committed through Unit of Work.

---

## Dependency Injection

Every service and repository must use constructor injection.

---

# Solution Structure

```
PeopleHub
│
├── docs
│
├── src
│   ├── PeopleHub.API
│   ├── PeopleHub.Application
│   ├── PeopleHub.Common
│   ├── PeopleHub.Contracts
│   ├── PeopleHub.Domain
│   ├── PeopleHub.Infrastructure
│   └── PeopleHub.SmartMatch
│
└── tests
    ├── PeopleHub.UnitTests
    └── PeopleHub.IntegrationTests
```

---

# Domain Structure

The Domain layer follows Domain-Driven Design using Aggregates.

```
PeopleHub.Domain
│
├── Aggregates
│   ├── User
│   ├── Provider
│   ├── Review
│   └── ServiceRequest
│
├── Common
├── Enums
├── Exceptions
├── Interfaces
└── ValueObjects
```

New domain objects should belong to an existing aggregate whenever appropriate.

Avoid creating unnecessary aggregates.

---

# Coding Standards

Every class must:

- Have a single responsibility
- Be placed in its appropriate project
- Be asynchronous where applicable
- Support CancellationToken
- Follow existing naming conventions

Always:

- One class per file
- Constructor Injection
- Explicit Mapping
- Nullable Reference Types
- XML comments where appropriate

---

# Never Use

The following libraries/patterns are intentionally excluded:

- MediatR
- CQRS
- AutoMapper
- Generic Repository
- Placeholder implementations
- TODO implementations

These decisions are architectural and should remain consistent throughout the project.

---

# Dependency Rules

Allowed dependencies:

```
API
    ↓

Application
    ↓

Domain

Infrastructure
    ↓

Application

Infrastructure
    ↓

Domain
```

The Domain project must never depend on any other project.

---

# Persistence

Database:

- SQL Server 2022

ORM:

- Entity Framework Core 9

Transactions:

- Unit of Work

Migrations:

- EF Core Migrations

---

# Authentication

Implemented authentication strategy:

- JWT Access Tokens
- Refresh Tokens
- Refresh Token Rotation
- Logout
- Role Claims
- RBAC Authorization

---

# Testing Strategy

The project includes:

- Unit Tests
- Integration Tests

Integration tests use:

- SQLite In-Memory Database
- Test Authentication
- Isolated Database per Test

Every completed feature should include tests before being marked complete.

---

# Build Policy

The solution should remain buildable at all times.

Required commands after each completed feature:

```bash
dotnet build

dotnet test
```

No feature is considered complete unless:

- Build succeeds
- Tests pass
- Documentation is updated

---

# Documentation

Two project documents are maintained.

PROJECT_CONTEXT.md

Contains:

- architecture
- standards
- workflow
- project rules

API_ROADMAP.md

Contains:

- implementation progress
- module status
- milestones
- build status
- test status

Both documents must remain synchronized.


---

# Development Workflow

Every feature implementation must follow the same workflow to maintain consistency and code quality.

## Standard Development Process

1. Review `PROJECT_CONTEXT.md`
2. Review `docs/API_ROADMAP.md`
3. Identify the current milestone
4. Implement one logical change at a time
5. Build the solution
6. Fix any compilation errors
7. Add or update unit tests
8. Add or update integration tests
9. Execute all tests
10. Update documentation
11. Commit changes
12. Push to the repository

---

# Feature Implementation Order

Unless there is a specific requirement, backend features should be implemented in the following order:

1. Domain
2. Repository
3. EF Core Configuration
4. Database Migration
5. Service Layer
6. Dependency Injection
7. API Controller
8. Unit Tests
9. Integration Tests
10. Swagger Verification
11. Documentation Update

---

# Code Review Checklist

Before completing any feature verify the following:

## Domain

- Entity follows aggregate boundaries
- Business rules belong in Domain
- Value Objects used where appropriate
- Enums correctly defined

## Infrastructure

- Repository implemented
- EF Configuration completed
- Dependency Injection registered

## Application

- Service implemented
- Validation completed
- Async methods used
- CancellationToken supported

## API

- Endpoints documented
- Correct HTTP status codes
- Authorization configured
- Validation responses verified

## Testing

- Unit Tests added
- Integration Tests added
- Existing tests still passing

---

# Git Workflow

Recommended branch strategy:

```
main
│
└── develop
     │
     ├── feature/provider-services
     ├── feature/messaging
     ├── feature/notifications
     └── feature/payments
```

Commit messages should follow Conventional Commits.

Examples:

```
feat: add provider services module

feat: implement provider search

fix: resolve provider availability validation

refactor: simplify review repository

docs: update API roadmap

test: add integration tests for service requests
```

---

# Definition of Done

A feature is considered complete only when all of the following are satisfied:

- Domain completed
- Repository completed
- Service completed
- Controller completed
- Dependency Injection registered
- Migration created (if applicable)
- Build successful
- Unit Tests passing
- Integration Tests passing
- Swagger verified
- Documentation updated
- Changes committed

---

# Documentation Rules

After completing every module update:

## PROJECT_CONTEXT.md

Update only if:

- Architecture changes
- Technology changes
- Workflow changes
- Standards change

## docs/API_ROADMAP.md

Update:

- Module status
- Completed features
- Current milestone
- Latest build status
- Latest test count
- Completion percentage

---

# AI Working Agreement

When continuing this project, the AI assistant should:

- Read PROJECT_CONTEXT.md first
- Read docs/API_ROADMAP.md second
- Continue from the current milestone
- Never recreate completed work
- Follow the existing architecture
- Respect aggregate boundaries
- Make one logical change at a time
- Show affected file paths before code changes
- Build after every logical change
- Resolve build errors before continuing
- Keep the solution compiling
- Preserve coding conventions
- Update documentation after completing each module

The assistant must never introduce:

- MediatR
- CQRS
- AutoMapper
- Generic Repository
- Placeholder implementations
- TODO implementations

---

# Project Statistics

Project Name

PeopleHub

Architecture

Clean Architecture + Domain-Driven Design

Backend

ASP.NET Core 9

ORM

Entity Framework Core 9

Database

SQL Server 2022

Authentication

JWT + Refresh Tokens

Testing

xUnit + SQLite In-Memory

Frontend

Angular

Mobile

Flutter

Documentation

PROJECT_CONTEXT.md

docs/API_ROADMAP.md

---

# Long-Term Modules

The planned backend modules are:

- Authentication
- User Management
- Provider Profiles
- Provider Skills
- Provider Verification
- Service Categories
- Provider Availability
- Service Requests
- Provider Services
- Reviews & Ratings
- Messaging
- Notifications
- SmartMatch
- Payments
- Administration
- Reporting
- Audit Logs

Modules should be completed sequentially unless business priorities change.

---

# Maintenance Guidelines

To keep the project healthy:

- Refactor only when it improves readability or maintainability.
- Avoid unnecessary dependencies.
- Preserve backward compatibility where possible.
- Keep documentation synchronized with implementation.
- Keep tests fast and reliable.
- Keep the build green.

---

# End of Document