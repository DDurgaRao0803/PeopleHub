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

# Provider Services

| Feature | Status |
|----------|--------|
| Create Service | ⬜ |
| Update Service | ⬜ |
| Delete Service | ⬜ |
| Get Service | ⬜ |
| List Provider Services | ⬜ |

---

# Service Categories

| Feature | Status |
|----------|--------|
| Create Category | ⬜ |
| Update Category | ⬜ |
| Delete Category | ⬜ |
| Get Categories | ⬜ |

---

# Provider Availability

| Feature | Status |
|----------|--------|
| Weekly Schedule | ⬜ |
| Time Slots | ⬜ |
| Holidays | ⬜ |
| Leave Dates | ⬜ |

---

# Bookings

| Feature | Status |
|----------|--------|
| Create Booking | ⬜ |
| Accept Booking | ⬜ |
| Reject Booking | ⬜ |
| Cancel Booking | ⬜ |
| Complete Booking | ⬜ |
| Booking History | ⬜ |

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
| Booking Module | ⬜ |

---

# Flutter App

| Feature | Status |
|----------|--------|
| Authentication | ⬜ |
| Provider Search | ⬜ |
| Booking | ⬜ |
| Notifications | ⬜ |

---

# Current Milestone

🎯 **Provider Services Module**

Planned order

1. Service Categories
2. Provider Services
3. Provider Availability
4. Booking Module

---

# Latest Build

```
dotnet build
```

Status

```
SUCCESS
```

---

# Latest Test Results

```
28 Passed
0 Failed
```