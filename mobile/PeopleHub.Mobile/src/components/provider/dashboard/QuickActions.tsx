import React from "react";
import {
  Pressable,
  StyleSheet,
  Text,
  View,
} from "react-native";

import {
  colors,
  radius,
  shadows,
  spacing,
  typography,
} from "../../../theme";

type QuickAction = {
  title: string;
  onPress: () => void;
};

type QuickActionsProps = {
  actions: QuickAction[];
};

const getConfig = (title: string) => {
  switch (title.toLowerCase()) {
    case "Requests":
    case "requests":
      return {
        icon: "📥",
        background: "#EEF8F1",
      };

    case "my services":
    case "services":
      return {
        icon: "🛠",
        background: "#EEF5FF",
      };

    case "availability":
      return {
        icon: "📅",
        background: "#F5EEFF",
      };

    case "reviews":
      return {
        icon: "⭐",
        background: "#FFF8E7",
      };

    default:
      return {
        icon: "📌",
        background: colors.surface,
      };
  }
};

export default function QuickActions({
  actions,
}: QuickActionsProps): React.JSX.Element {
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.heading}>
          Quick Actions
        </Text>

        <Pressable>
          <Text style={styles.viewAll}>
            View All &gt;
          </Text>
        </Pressable>
      </View>

      <View style={styles.grid}>
        {actions.map((action) => {
          const config = getConfig(action.title);

          return (
            <Pressable
              key={action.title}
              onPress={action.onPress}
              android_ripple={{
                color: "#E5E7EB",
              }}
              style={({ pressed }) => [
                styles.card,
                {
                  backgroundColor: config.background,
                  opacity: pressed ? 0.85 : 1,
                },
              ]}
            >
              <Text style={styles.icon}>
                {config.icon}
              </Text>

              <Text
                numberOfLines={2}
                style={styles.cardTitle}
              >
                {action.title}
              </Text>
            </Pressable>
          );
        })}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: spacing.xxxl,
  },

  header: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: spacing.lg,
  },

  heading: {
    ...typography.h3,
    color: colors.text.primary,
  },

  viewAll: {
    ...typography.body,
    color: colors.primary,
    fontWeight: "700",
  },

  grid: {
    flexDirection: "row",
    justifyContent: "space-between",
  },

  card: {
    width: "23%",
    aspectRatio: 1,

    borderRadius: radius.xl,

    alignItems: "center",
    justifyContent: "center",

    padding: spacing.sm,

    ...shadows.sm,
  },

  icon: {
    fontSize: 26,
    marginBottom: spacing.sm,
  },

  cardTitle: {
    ...typography.caption,
    color: colors.text.primary,
    textAlign: "center",
  },
});