/**
 * ============================================================
 * PeopleHub Mobile
 * Design System - Colors
 * ============================================================
 */

export const colors = {
  /**
   * Brand
   */
  primary: "#2563EB",
  primaryDark: "#1D4ED8",
  primaryLight: "#DBEAFE",

  secondary: "#14B8A6",
  secondaryDark: "#0F766E",
  secondaryLight: "#CCFBF1",

  accent: "#F59E0B",

  /**
   * Status
   */
  success: "#16A34A",
  warning: "#F59E0B",
  error: "#DC2626",
  info: "#0284C7",

  /**
   * Backgrounds
   */
  background: "#F8FAFC",
  backgroundSecondary: "#F1F5F9",

  surface: "#FFFFFF",
  surfaceSecondary: "#F8FAFC",

  /**
   * Text
   */
  text: {
    primary: "#111827",
    secondary: "#6B7280",
    tertiary: "#9CA3AF",
    disabled: "#D1D5DB",
    inverse: "#FFFFFF",
  },

  /**
   * Borders
   */
  border: "#E5E7EB",
  borderDark: "#CBD5E1",
  divider: "#F1F5F9",

  /**
   * Icons
   */
  icon: {
    primary: "#2563EB",
    secondary: "#6B7280",
    disabled: "#9CA3AF",
    inverse: "#FFFFFF",
  },

  /**
   * Cards
   */
  card: {
    background: "#FFFFFF",
    border: "#E5E7EB",
  },

  /**
   * Buttons
   */
  button: {
    primary: "#2563EB",
    primaryPressed: "#1D4ED8",

    secondary: "#FFFFFF",
    secondaryBorder: "#2563EB",

    danger: "#DC2626",
    success: "#16A34A",
  },

  /**
   * Inputs
   */
  input: {
    background: "#FFFFFF",
    border: "#D1D5DB",
    borderFocused: "#2563EB",
    placeholder: "#9CA3AF",
  },

  /**
   * Misc
   */
  overlay: "rgba(0,0,0,0.45)",
  shadow: "rgba(15,23,42,0.08)",

  transparent: "transparent",
} as const;

export type Colors = typeof colors;