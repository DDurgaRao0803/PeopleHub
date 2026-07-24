/**
 * ============================================================
 * PeopleHub Mobile
 * Design System - Typography
 * ============================================================
 */

export const typography = {
  display: {
    fontSize: 36,
    fontWeight: "700",
    lineHeight: 44,
  },

  h1: {
    fontSize: 30,
    fontWeight: "700",
    lineHeight: 38,
  },

  h2: {
    fontSize: 24,
    fontWeight: "700",
    lineHeight: 32,
  },

  h3: {
    fontSize: 20,
    fontWeight: "600",
    lineHeight: 28,
  },

  title: {
    fontSize: 18,
    fontWeight: "600",
    lineHeight: 26,
  },

  subtitle: {
    fontSize: 16,
    fontWeight: "500",
    lineHeight: 24,
  },

  bodyLarge: {
    fontSize: 16,
    fontWeight: "400",
    lineHeight: 24,
  },

  body: {
    fontSize: 14,
    fontWeight: "400",
    lineHeight: 22,
  },

  caption: {
    fontSize: 12,
    fontWeight: "400",
    lineHeight: 18,
  },

  button: {
    fontSize: 16,
    fontWeight: "600",
    lineHeight: 22,
  },
} as const;

export type Typography = typeof typography;