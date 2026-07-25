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
  baseUrl: "http://192.168.8.74:5212/api",
  timeout: 30000,
},

signalR: {
  hubUrl: "http://192.168.8.74:5212/hubs",
},

  storage: {
    accessTokenKey: "peoplehub_access_token",
    refreshTokenKey: "peoplehub_refresh_token",
    userKey: "peoplehub_user",
  },
} as const;

export type AppConfigType = typeof AppConfig;