# PeopleHub Project Context

> **Purpose**
>
> This document is the single source of truth for the PeopleHub project.
>
> When continuing this project in a new ChatGPT conversation, upload this document first.
> The assistant should use it to understand the project's architecture, current implementation status, and coding standards without recreating completed work.

---

# Project Overview

PeopleHub is a production-grade service marketplace platform built using Clean Architecture.

The platform connects customers with service providers and will support provider management, intelligent SmartMatch, bookings, notifications, messaging, authentication, and administration.

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

## Security

- JWT Authentication
- Refresh Tokens (Planned)
- OTP Authentication (Planned)

## Validation

- FluentValidation

## Logging

- Serilog

## Realtime

- SignalR

## API

- Swagger / OpenAPI
- API Versioning
- Health Checks

## Deployment

- Azure

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
│   ├── PeopleHub.Common
│   ├── PeopleHub.Contracts
│   ├── PeopleHub.Domain
│   ├── PeopleHub.Infrastructure
│   └── PeopleHub.SmartMatch
│
└── tests
```

---

# Architecture Rules

The following rules must always be followed.

## Required

- Clean Architecture
- Repository Pattern
- Service Layer
- One class per file
- One responsibility per class
- Explicit object mapping
- Production-ready code only

## Not Allowed

- MediatR
- CQRS
- AutoMapper
- Generic Repository
- Placeholder implementations
- Dummy code
- TODO implementations

---

# Coding Standards

- Build after every implementation step.
- Fix build errors before continuing.
- No compiler warnings.
- Keep methods focused and readable.
- Use dependency injection.
- Follow SOLID principles where appropriate.

---

# Current Project Status

## Completed

### Domain

- Entity
- AuditableEntity
- ValueObject
- DomainException
- Email Value Object
- PhoneNumber Value Object
- User Aggregate
- Provider Aggregate
- UserRole

### Infrastructure

- ApplicationDbContext
- Repository Pattern
- UnitOfWork
- Entity Configurations

### Security

- Password Hashing
- JWT Token Generator

### Application

- User Registration
- FluentValidation

### Database

- SQL Server configured
- Migration applied
- PasswordHash added to User

---

# Current Milestone

Authentication

---

# Current Task

Configure JWT Authentication in:

```
src/PeopleHub.API/Program.cs
```

After that:

1. Login
2. Authentication Controller
3. Swagger JWT
4. Refresh Tokens
5. OTP

---

# Database

Database Provider

- SQL Server 2022

Latest Migration

```
AddPasswordHashToUser
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

# AI Working Agreement

When continuing this project:

- Never recreate completed work.
- Continue from the Current Task section.
- Preserve the existing architecture.
- Never introduce MediatR.
- Never introduce CQRS.
- Never introduce AutoMapper.
- Always provide the exact file path before requesting code changes.
- Implement one logical step at a time.
- End each implementation step with a successful `dotnet build`.
- Update this document after every major milestone.
- Update `API_ROADMAP.md` whenever feature status changes.

---

# Progress

Estimated Completion

20–25%

Current Focus

Authentication Module