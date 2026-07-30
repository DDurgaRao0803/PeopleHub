import React from "react";
import { Image, StyleSheet, Text, View } from "react-native";

import {
  colors,
  radius,
  shadows,
  spacing,
  typography,
} from "../../../theme";

type DashboardHeaderProps = {
  firstName: string;
};

export default function DashboardHeader({
  firstName,
}: DashboardHeaderProps): React.JSX.Element {
  const hour = new Date().getHours();

  let greeting = "Good Evening";

  if (hour < 12) {
    greeting = "Good Morning";
  } else if (hour < 18) {
    greeting = "Good Afternoon";
  }

  return (
    <View style={styles.container}>
      <View style={styles.topRow}>
        <View>
          <Text style={styles.logo}>PeopleHub</Text>
          <Text style={styles.provider}>PROVIDER</Text>
        </View>

        <View style={styles.rightSection}>
          <View style={styles.notificationContainer}>
            <Text style={styles.notificationIcon}>🔔</Text>

            <View style={styles.notificationBadge}>
              <Text style={styles.notificationCount}>3</Text>
            </View>
          </View>

          <Image
            source={{
              uri: "https://i.pravatar.cc/120?img=12",
            }}
            style={styles.avatar}
          />
        </View>
      </View>

      <Text style={styles.greeting}>{greeting}</Text>

      <Text style={styles.name}>
        {firstName} 👋
      </Text>

      <View style={styles.badge}>
        <Text style={styles.badgeText}>⭐ Gold Provider</Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: spacing.xxl,
  },

  topRow: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: spacing.xl,
  },

  logo: {
    ...typography.h2,
    color: colors.text.primary,
  },

  provider: {
    ...typography.caption,
    color: colors.text.secondary,
    letterSpacing: 1,
  },

  rightSection: {
    flexDirection: "row",
    alignItems: "center",
  },

  notificationContainer: {
    marginRight: spacing.lg,
    position: "relative",
  },

  notificationIcon: {
    fontSize: 24,
  },

  notificationBadge: {
    position: "absolute",
    top: -4,
    right: -6,

    width: 18,
    height: 18,
    borderRadius: 9,

    backgroundColor: colors.error,

    justifyContent: "center",
    alignItems: "center",
  },

  notificationCount: {
    color: colors.white,
    fontSize: 10,
    fontWeight: "700",
  },

  avatar: {
    width: 52,
    height: 52,
    borderRadius: 26,
  },

  greeting: {
    ...typography.subtitle,
    color: colors.text.secondary,
  },

  name: {
    ...typography.display,
    color: colors.text.primary,
    marginTop: spacing.xs,
  },

  badge: {
    alignSelf: "flex-start",

    marginTop: spacing.lg,

    paddingHorizontal: spacing.lg,
    paddingVertical: spacing.sm,

    borderRadius: radius.round,

    backgroundColor: "#FFF8E6",

    ...shadows.sm,
  },

  badgeText: {
    ...typography.caption,
    color: "#B7791F",
    fontWeight: "700",
  },
});