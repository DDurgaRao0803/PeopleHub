# PeopleHub Project Context

> **Purpose**
>
> Upload this document when starting a new ChatGPT conversation.
> It contains the current architecture, completed implementation, coding standards, and the next milestone.

---

# Project Overview

PeopleHub is a production-grade service marketplace platform built using Clean Architecture.

The platform connects customers with service providers and will support provider management, SmartMatch, bookings, notifications, messaging, authentication, and administration.

---

# Technology Stack

## Backend

- ASP.NET Core 9
- .NET 9
- Entity Framework Core 9
- SQL Server 2022 (Docker)

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

## Logging

- Serilog

## API

- Swagger / OpenAPI
- API Versioning
- Health Checks

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
│   └── PeopleHub.SmartMatch
│
└── tests
```

---

# Architecture Rules

Always follow these rules.

## Required

- Clean Architecture
- Repository Pattern
- Service Layer
- Unit of Work
- Explicit object mapping
- Production-ready code
- One class per file

## Never Use

- MediatR
- CQRS
- AutoMapper
- Generic Repository
- Placeholder implementations
- TODO implementations

---

# Development Workflow

The assistant should always:

- Start every response with a roadmap/progress block.
- Show the exact file path before every code change.
- Implement one logical change at a time.
- Build after every change.
- Fix build errors before continuing.
- Preserve the existing architecture.

---

# Completed Modules

## Domain

Completed

- Entity
- AuditableEntity
- ValueObject
- DomainException
- Email Value Object
- PhoneNumber Value Object
- User Aggregate
- Provider Aggregate
- Role enum

---

## Infrastructure

Completed

- ApplicationDbContext
- Repository Pattern
- Unit Of Work
- Entity Configurations
- Password Hashing
- JWT Token Generator
- JWT Authentication
- JWT Authorization

---

## Authentication

Completed

- Register
- Login
- Password Hashing
- JWT Access Token
- Refresh Token
- Refresh Token Rotation
- Refresh Token Revocation
- Logout
- `/api/auth/me`

---

## RBAC

Completed

- Role enum
- Role column in User
- Role migration
- Existing users migrated to Role = User
- JWT Role Claim
- `[Authorize(Roles = "Admin")]`
- Admin endpoint

Verified

- User → 403 Forbidden
- Admin → 200 OK

---

# Database

Provider

- SQL Server 2022 (Docker)

Latest Completed Migration

```
AddUserRole
```

Database Updated

```sql
UPDATE Users
SET Role = 1
WHERE Role = 0;
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

# Current Status

Authentication & Authorization Module

✅ COMPLETE

---

# Next Module

User Management

Recommended order

1. Get Current User
2. Get User By Id
3. Get All Users (Admin)
4. Update User
5. Delete User (Admin)

After User Management

- Global Exception Middleware
- FluentValidation Improvements
- Pagination
- Filtering
- Sorting
- Serilog
- Unit Tests
- Integration Tests

---

# AI Working Agreement

When continuing this project:

- Never recreate completed work.
- Continue from the Next Module section.
- Keep using Clean Architecture.
- Never introduce MediatR.
- Never introduce CQRS.
- Never introduce AutoMapper.
- Show the file path before each code change.
- Make one change at a time.
- Build after every implementation step.
- Update both `PROJECT_CONTEXT.md` and `API_ROADMAP.md` after each completed milestone.

---

# Progress

Estimated Completion

**30–35%**

Current Focus

**User Management Module**