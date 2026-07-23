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
    baseUrl:
      "https://super-invention-5vvx7gjj5x5wc77v9-5212.app.github.dev/api",

    timeout: 30000,
  },

  signalR: {
    hubUrl:
      "https://super-invention-5vvx7gjj5x5wc77v9-5212.app.github.dev/hubs",
  },

  storage: {
    accessTokenKey: "peoplehub_access_token",
    refreshTokenKey: "peoplehub_refresh_token",
    userKey: "peoplehub_user",
  },
} as const;

export type AppConfigType = typeof AppConfig;