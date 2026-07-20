# PeopleHub API Roadmap

> This document tracks the implementation status of all API features.

Legend

- ✅ Completed
- 🚧 In Progress
- ⬜ Not Started

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
| Role-Based Access Control (RBAC) | ✅ |
| Admin Authorization | ✅ |

**Completed**

- User Registration
- Password Hashing
- Login
- JWT Access Token
- Refresh Token
- Refresh Token Rotation
- Refresh Token Revocation
- Logout
- `/api/auth/me`
- Role Claim in JWT
- Admin-only endpoint
- User → 403 Forbidden
- Admin → 200 OK

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
| Provider Profile Tests | ✅ |

---

# Provider Skills

| Feature | Status |
|----------|--------|
| Add Skill | ✅ |
| Remove Skill | ✅ |
| List Skills | ✅ |

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
| Swagger Tests | ✅ |

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
| Swagger Tests | ✅ |

---

# Service Requests

| Feature | Status |
|----------|--------|
| Create Service Request | ✅ |
| Get By Id | ✅ |
| Get Customer Requests | ✅ |
| Get Provider Requests | ✅ |
| Accept Request | ✅ |
| Complete Request | ✅ |
| Repository | ✅ |
| Service | ✅ |
| Controller | ✅ |
| Swagger Tests | ✅ |

**Completed**

- Domain Entity
- Status Enum
- EF Core Configuration
- Repository
- Service Layer
- Dependency Injection
- Migration
- SQL Server Persistence
- End-to-End Swagger Testing

---

# Provider Services

| Feature | Status |
|----------|--------|
| Create Service | ⬜ |
| Update Service | ⬜ |
| Delete Service | ⬜ |
| Get Service | ⬜ |
| List Provider Services | ⬜ |

---

# Reviews

| Feature | Status |
|----------|--------|
| Create Review | ⬜ |
| Update Review | ⬜ |
| Delete Review | ⬜ |
| Provider Ratings | ⬜ |

---

# Messaging

| Feature | Status |
|----------|--------|
| Conversations | ⬜ |
| Send Message | ⬜ |
| Read Messages | ⬜ |
| SignalR Integration | ⬜ |

---

# Notifications

| Feature | Status |
|----------|--------|
| Push Notifications | ⬜ |
| Email Notifications | ⬜ |
| SMS Notifications | ⬜ |
| In-App Notifications | ⬜ |

---

# SmartMatch

| Feature | Status |
|----------|--------|
| Provider Matching | ⬜ |
| Ranking Algorithm | ⬜ |
| Recommendation Engine | ⬜ |

---

# Administration

| Feature | Status |
|----------|--------|
| Dashboard | ⬜ |
| User Management | ⬜ |
| Provider Management | ⬜ |
| Provider Verification Review | ⬜ |
| Audit Logs | ⬜ |

---

# System

| Feature | Status |
|----------|--------|
| Health Checks | ⬜ |
| API Versioning | ⬜ |
| Serilog | ⬜ |
| Swagger Security | ✅ |

---

# Angular Client

| Feature | Status |
|----------|--------|
| Authentication | ⬜ |
| Dashboard | ⬜ |
| Provider Module | ⬜ |
| Service Requests | ⬜ |
| Reviews | ⬜ |

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

🎯 **Provider Reviews & Ratings Module**

Planned Order

1. Provider Reviews & Ratings
2. Provider Services
3. Messaging
4. Notifications
5. SmartMatch
6. Administration

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