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
    REFRESH: "/auth/refresh-token",
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