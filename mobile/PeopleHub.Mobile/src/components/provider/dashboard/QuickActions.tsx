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
  subtitle?: string;
  onPress: () => void;
};

type QuickActionsProps = {
  actions: QuickAction[];
  onViewAll?: () => void;
};

const getConfig = (title: string) => {
  switch (title) {
    case "Requests":
      return {
        icon: "📥",
        background: "#EEF5FF",
      };

    case "My Services":
      return {
        icon: "🛠️",
        background: "#ECFDF5",
      };

    case "Availability":
      return {
        icon: "🟢",
        background: "#FFF8E7",
      };

    case "Reviews":
      return {
        icon: "⭐",
        background: "#F5EEFF",
      };

    default:
      return {
        icon: "📋",
        background: colors.surface,
      };
  }
};

export default function QuickActions({
  actions,
  onViewAll,
}: QuickActionsProps): React.JSX.Element {
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.heading}>
          Quick Actions
        </Text>

        <Pressable onPress={onViewAll}>
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
              <View
    style={[
        styles.iconContainer,
        { backgroundColor: config.background },
    ]}
>
    <Text style={styles.icon}>
        {config.icon}
    </Text>
</View>

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
  alignItems: "stretch",
},

  card: {
  width: "23%",
  height: 82,

  borderRadius: 18,

  alignItems: "center",
  justifyContent: "center",

  paddingVertical: 10,
  paddingHorizontal: 6,

  ...shadows.sm,
},

  icon: {
  fontSize: 22,
  marginBottom: 6,
},

  cardTitle: {
  fontSize: 11,
  fontWeight: "600",
  color: colors.text.primary,
  textAlign: "center",
  lineHeight: 14,
},

  iconContainer: {
  width: 34,
  height: 34,
  borderRadius: 10,
    justifyContent: "center",
    alignItems: "center",
    marginBottom: 14,
},

cardSubtitle: {
    marginTop: 6,
    fontSize: 12,
    color: "#6B7280",
    textAlign: "center",
    lineHeight: 18,
},

});