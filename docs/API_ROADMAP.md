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
| Get Current User | ⬜ |
| Get User By Id | ⬜ |
| Get All Users | ⬜ |
| Update Profile | ⬜ |
| Delete User | ⬜ |
| Upload Profile Image | ⬜ |

---

# Providers

| Feature | Status |
|----------|--------|
| Create Provider | ⬜ |
| Update Provider | ⬜ |
| Delete Provider | ⬜ |
| Get Provider | ⬜ |
| Search Providers | ⬜ |
| Provider Verification | ⬜ |

---

# Service Categories

| Feature | Status |
|----------|--------|
| Create Category | ⬜ |
| Update Category | ⬜ |
| Delete Category | ⬜ |
| Get Categories | ⬜ |

---

# Bookings

| Feature | Status |
|----------|--------|
| Create Booking | ⬜ |
| Update Booking | ⬜ |
| Cancel Booking | ⬜ |
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

🎯 **User Management Module**

Planned order:

1. Get Current User
2. Get User By Id
3. Get All Users (Admin)
4. Update User
5. Delete User (Admin)