/**
 * ============================================================
 * PeopleHub Mobile
 * API Endpoints
 * ============================================================
 */

export const API_ENDPOINTS = {
  AUTH: {
  LOGIN: "/auth/login",
  REGISTER: "/auth/register",
  FORGOT_PASSWORD: "/auth/forgot-password",
  RESET_PASSWORD: "/auth/reset-password",
  CHANGE_PASSWORD: "/auth/change-password",
  VERIFY_OTP: "/auth/verify-otp",
  REFRESH: "/auth/refresh",
  LOGOUT: "/auth/logout",
},

  PROVIDER: {
    PROFILE: "/provider-profiles",
    SERVICES: "/provider-services",
    AVAILABILITY: "/provider-availability",
    VERIFICATION: "/provider-verifications",
  },

  CUSTOMER: {
    PROFILE: "/users/profile",
  },

  SERVICE_REQUEST: {
    BASE: "/service-requests",
  },

  WALLET: {
    BASE: "/wallet",
  },
} as const;