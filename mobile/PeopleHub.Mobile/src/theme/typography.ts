/**
 * ============================================================
 * PeopleHub Mobile
 * Theme - Typography
 * ============================================================
 */

export const typography = {
  fontSize: {
    xs: 12,
    sm: 14,
    md: 16,
    lg: 18,
    xl: 22,
    xxl: 28,
    display: 36,
  },

  fontWeight: {
    regular: "400",
    medium: "500",
    semiBold: "600",
    bold: "700",
  },
} as const;

export type Typography = typeof typography;