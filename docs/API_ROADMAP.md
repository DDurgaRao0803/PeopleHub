# PeopleHub API Roadmap

> Version: 1.0
>
> This document tracks the implementation status of every backend module.
>
> Update this file immediately after completing a module.

---

# Legend

| Status | Meaning |
|---------|---------|
| ✅ | Completed |
| 🚧 | In Progress |
| ⬜ | Not Started |

---

# Overall Progress

| Item | Status |
|------|--------|
| Backend | 🚧 In Progress |
| Angular Client | ⬜ |
| Flutter App | ⬜ |

Estimated Backend Completion

**60%**

---

# Authentication

| Feature | Status |
|----------|--------|
| Register | ✅ |
| Login | ✅ |
| JWT Authentication | ✅ |
| JWT Authorization | ✅ |
| Refresh Token | ✅ |
| Refresh Token Rotation | ✅ |
| Logout | ✅ |
| Forgot Password | ⬜ |
| Reset Password | ⬜ |
| Change Password | ⬜ |
| OTP | ⬜ |
| RBAC | ✅ |
| Admin Authorization | ✅ |

Completed

- User Registration
- Password Hashing
- JWT Authentication
- Refresh Tokens
- Refresh Token Rotation
- Logout
- `/api/auth/me`
- Role Claims
- Admin Authorization

---

# Users

| Feature | Status |
|----------|--------|
| Get Current User | ✅ |
| Get User By Id | ✅ |
| Get All Users | ✅ |
| Update User | ✅ |
| Delete User | ✅ |
| Upload Profile Image | ⬜ |

---

# Provider Profile

| Feature | Status |
|----------|--------|
| Create Provider Profile | ✅ |
| Update Provider Profile | ✅ |
| Delete Provider Profile | ✅ |
| Get Provider Profile | ✅ |
| Repository | ✅ |
| Service | ✅ |
| Controller | ✅ |
| Unit Tests | ✅ |
| Integration Tests | ✅ |

---

# Provider Skills

| Feature | Status |
|----------|--------|
| Add Skill | ✅ |
| Remove Skill | ✅ |
| List Skills | ✅ |
| Repository | ✅ |
| Service | ✅ |
| Controller | ✅ |

---

# Provider Verification

| Feature | Status |
|----------|--------|
| Create Verification | ✅ |
| Get Verification | ✅ |
| Delete Verification | ✅ |
| Repository | ✅ |
| Service | ✅ |
| Controller | ✅ |
| Unit Tests | ✅ |
| Integration Tests | ✅ |
| Approve Verification | ⬜ |
| Reject Verification | ⬜ |

---

# Service Categories

| Feature | Status |
|----------|--------|
| Create Category | ✅ |
| Update Category | ✅ |
| Delete Category | ✅ |
| Get Categories | ✅ |
| Repository | ✅ |
| Service | ✅ |
| Controller | ✅ |
| Swagger Verification | ✅ |

---

# Provider Availability

| Feature | Status |
|----------|--------|
| Create Availability | ✅ |
| Update Availability | ✅ |
| Delete Availability | ✅ |
| Get Provider Availability | ✅ |
| Repository | ✅ |
| Service | ✅ |
| Controller | ✅ |
| Swagger Verification | ✅ |

---

# Service Requests

| Feature | Status |
|----------|--------|
| Create Request | ✅ |
| Get By Id | ✅ |
| Customer Requests | ✅ |
| Provider Requests | ✅ |
| Accept Request | ✅ |
| Complete Request | ✅ |
| Repository | ✅ |
| Service | ✅ |
| Controller | ✅ |
| EF Configuration | ✅ |
| Migration | ✅ |
| Swagger Verification | ✅ |

Completed

- Domain Entity
- Status Enum
- Repository
- EF Core Configuration
- Service Layer
- Dependency Injection
- Migration
- SQL Server Persistence
- End-to-End Testing


---

# Provider Services

| Feature | Status |
|----------|--------|
| Create Service | ⬜ |
| Update Service | ⬜ |
| Delete Service | ⬜ |
| Get Service | ⬜ |
| List Provider Services | ⬜ |
| Repository | ⬜ |
| Service | ⬜ |
| Controller | ⬜ |
| Unit Tests | ⬜ |
| Integration Tests | ⬜ |

---

# Reviews & Ratings

| Feature | Status |
|----------|--------|
| Create Review | ⬜ |
| Update Review | ⬜ |
| Delete Review | ⬜ |
| Get Reviews | ⬜ |
| Provider Ratings | ⬜ |
| Repository | ⬜ |
| Service | ⬜ |
| Controller | ⬜ |
| Unit Tests | ⬜ |
| Integration Tests | ⬜ |

---

# Messaging

| Feature | Status |
|----------|--------|
| Conversations | ⬜ |
| Send Message | ⬜ |
| Read Messages | ⬜ |
| Delete Message | ⬜ |
| SignalR Integration | ⬜ |
| Repository | ⬜ |
| Service | ⬜ |
| Controller | ⬜ |
| Unit Tests | ⬜ |
| Integration Tests | ⬜ |

---

# Notifications

| Feature | Status |
|----------|--------|
| Push Notifications | ⬜ |
| Email Notifications | ⬜ |
| SMS Notifications | ⬜ |
| In-App Notifications | ⬜ |
| Repository | ⬜ |
| Service | ⬜ |
| Controller | ⬜ |
| Unit Tests | ⬜ |
| Integration Tests | ⬜ |

---

# SmartMatch

| Feature | Status |
|----------|--------|
| Provider Matching | ⬜ |
| Ranking Algorithm | ⬜ |
| Recommendation Engine | ⬜ |
| Background Processing | ⬜ |
| Repository Integration | ⬜ |
| Unit Tests | ⬜ |

---

# Administration

| Feature | Status |
|----------|--------|
| Dashboard | ⬜ |
| User Management | ⬜ |
| Provider Management | ⬜ |
| Provider Verification Review | ⬜ |
| Audit Logs | ⬜ |
| System Settings | ⬜ |

---

# System

| Feature | Status |
|----------|--------|
| Health Checks | ⬜ |
| API Versioning | ⬜ |
| Serilog Logging | ⬜ |
| Global Exception Handling | ⬜ |
| Swagger Security | ✅ |
| Rate Limiting | ⬜ |

---

# Angular Client

| Feature | Status |
|----------|--------|
| Authentication | ⬜ |
| Dashboard | ⬜ |
| Provider Module | ⬜ |
| Service Requests | ⬜ |
| Reviews | ⬜ |
| Administration | ⬜ |

---

# Flutter App

| Feature | Status |
|----------|--------|
| Authentication | ⬜ |
| Provider Search | ⬜ |
| Service Requests | ⬜ |
| Reviews | ⬜ |
| Notifications | ⬜ |

---

# Current Milestone

🎯 **Provider Services Module**

---

# Planned Backend Order

1. Provider Services
2. Reviews & Ratings
3. Messaging
4. Notifications
5. SmartMatch
6. Administration
7. System Enhancements

---

# Latest Build

```text
dotnet build
```

Status

```text
SUCCESS
```

---

# Latest Test Results

```text
28 Passed
0 Failed
```

> Update these numbers after every successful test run.

---

# Release History

## v1.0

Completed Modules

- Authentication
- Users
- Provider Profile
- Provider Skills
- Provider Verification
- Service Categories
- Provider Availability
- Service Requests

Current Focus

- Provider Services

---

# Maintenance Notes

After completing every backend module:

- Update feature status
- Update current milestone
- Update latest build status
- Update latest test count
- Commit documentation changes together with code changes

---

# End of Document