# PeopleHub Project Context

## Project Name

PeopleHub

## Purpose

PeopleHub is a service marketplace platform connecting customers with service providers.

The platform supports:

- Service requests
- Provider discovery
- Provider verification
- SmartMatch provider selection
- Service tracking
- Reviews and ratings

---

# Technology Stack

## Backend

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Clean Architecture
- Repository Pattern
- Service Layer
- Unit of Work

## Testing

- xUnit
- FluentAssertions
- Moq
- Integration Tests
- WebApplicationFactory

---

# Architecture Rules

Follow:

- Clean Architecture
- Domain-driven structure
- Repository Pattern
- Service Layer
- EF Core
- SQL Server

Do NOT introduce:

- MediatR
- CQRS
- AutoMapper
- Generic Repository

---

# Solution Structure

```
src/
 ├── PeopleHub.API
 ├── PeopleHub.Application
 ├── PeopleHub.Contracts
 ├── PeopleHub.Domain
 ├── PeopleHub.Infrastructure
 └── PeopleHub.SmartMatch

tests/
 ├── PeopleHub.UnitTests
 └── PeopleHub.IntegrationTests
```

---

# Completed Features

## Authentication

Status: Completed ✅

Implemented:

- Registration
- Login
- JWT Authentication
- Refresh Token
- Logout

---

## Provider Module

Status: Completed ✅

Implemented:

- Provider Profile
- Provider Skills
- Provider Availability
- Provider Verification

---

## Service Module

Status: Completed ✅

Implemented:

- Service Categories
- Service Requests
- Service Request lifecycle
- Completion flow

---

## Reviews

Status: Completed ✅

Implemented:

- Customer reviews
- Provider rating records

---

## Provider Search

Status: Completed ✅

Implemented:

- Keyword search
- Category filtering
- Verification filtering
- Pagination

---

## Administration

Status: Completed ✅

Implemented:

- Admin Dashboard
- User Management
- Provider Verification Management

---

## SmartMatch

Status: Completed ✅

Implemented:

- Candidate filtering
- Verification Rule
- Skill Rule
- Availability Rule
- Provider scoring
- Best provider selection

Current endpoint:

```
POST /api/SmartMatch/{serviceRequestId}
```

Example response:

```json
{
  "selectedProviderId": "provider-id",
  "candidateCount": 1
}
```

---

# Current Development Phase

## SmartMatch Assignment Workflow

Status: In Progress 🚧

Goal:

Convert SmartMatch selection into a complete business workflow.

Expected flow:

```
Customer Service Request

        |

        v

SmartMatch Engine

        |

        v

Select Provider

        |

        v

Assign Provider

        |

        v

Update Request Status

        |

        v

Provider Accept / Reject

        |

        v

Service Execution
```

---

# Development Rules

Before every change:

1. Show file path
2. Make one logical change
3. Build solution
4. Fix errors immediately
5. Add/update tests
6. Keep solution compiling

---

# Current Git Status

Latest successful commit:

```
e35c47e

Complete SmartMatch provider selection and related fixes
```

Tests:

```
Total: 142
Failed: 0
Succeeded: 142
```

Build:

```
Successful
```