/**
 * ============================================================
 * PeopleHub Mobile
 * Application Configuration
 * ============================================================
 */

export const AppConfig = {
  app: {
    name: "PeopleHub",
    version: "1.0.0",
  },

  api: {
    // Development URL
    // Replace with your backend URL when testing on a device.
    baseUrl: "http://localhost:5290/api",

    timeout: 30000,
  },

  signalR: {
    hubUrl: "http://localhost:5290/hubs",
  },

  storage: {
    accessTokenKey: "peoplehub_access_token",
    refreshTokenKey: "peoplehub_refresh_token",
    userKey: "peoplehub_user",
  },
} as const;

export type AppConfigType = typeof AppConfig;