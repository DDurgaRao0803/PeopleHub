/**
 * ============================================================
 * PeopleHub Mobile
 * Theme - Colors
 * ============================================================
 */

export const colors = {
  primary: "#2563EB",
  primaryDark: "#1D4ED8",
  primaryLight: "#60A5FA",

  secondary: "#10B981",

  accent: "#F59E0B",

  success: "#22C55E",
  warning: "#F59E0B",
  error: "#EF4444",
  info: "#0EA5E9",

  background: "#F8FAFC",
  surface: "#FFFFFF",

  text: {
    primary: "#111827",
    secondary: "#6B7280",
    disabled: "#9CA3AF",
    inverse: "#FFFFFF",
  },

  border: "#E5E7EB",
  divider: "#F1F5F9",

  overlay: "rgba(0,0,0,0.4)",

  transparent: "transparent",
} as const;

export type Colors = typeof colors;