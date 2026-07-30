import React from "react";
import { StyleSheet, Text, View } from "react-native";

import {
  colors,
  radius,
  shadows,
  spacing,
  typography,
} from "../../../theme";

type SummaryCardProps = {
  title: string;
  value: string;
};

const getConfig = (title: string) => {
  switch (title.toLowerCase()) {
    case "jobs":
      return {
        icon: "💼",
        background: "#EEF5FF",
      };

    case "pending":
      return {
        icon: "📥",
        background: "#FFF8E7",
      };

    case "earnings":
      return {
        icon: "💰",
        background: "#ECFDF5",
      };

    case "rating":
      return {
        icon: "⭐",
        background: "#F5EEFF",
      };

    default:
      return {
        icon: "📊",
        background: colors.surface,
      };
  }
};

export default function SummaryCard({
  title,
  value,
}: SummaryCardProps): React.JSX.Element {
  const config = getConfig(title);

  return (
    <View
      style={[
        styles.card,
        {
          backgroundColor: config.background,
        },
      ]}
    >
      <Text style={styles.icon}>
        {config.icon}
      </Text>

      <Text
        numberOfLines={1}
        style={styles.title}
      >
        {title}
      </Text>

      <Text
        numberOfLines={1}
        adjustsFontSizeToFit
        style={styles.value}
      >
        {value}
      </Text>
    </View>
  );
}

const styles = StyleSheet.create({
  card: {
    width: "23%",
    aspectRatio: 1,

    borderRadius: radius.xl,

    justifyContent: "center",
    alignItems: "center",

    ...shadows.sm,
  },

  icon: {
    fontSize: 24,
    marginBottom: spacing.sm,
  },

  title: {
    ...typography.caption,
    color: colors.text.secondary,
    textAlign: "center",
    marginBottom: spacing.xs,
  },

  value: {
    ...typography.title,
    color: colors.text.primary,
    fontWeight: "700",
  },
});