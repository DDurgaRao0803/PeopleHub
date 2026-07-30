import React from "react";
import {
  Pressable,
  StyleSheet,
  Switch,
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

type AvailabilityCardProps = {
  acceptingRequests: boolean;
  onToggle: (value: boolean) => void;
};

export default function AvailabilityCard({
  acceptingRequests,
  onToggle,
}: AvailabilityCardProps): React.JSX.Element {
  return (
    <View style={styles.card}>
      <View style={styles.left}>
        <View style={styles.statusRow}>
          <View
            style={[
              styles.dot,
              {
                backgroundColor: acceptingRequests
                  ? colors.success
                  : colors.error,
              },
            ]}
          />

          <Text style={styles.title}>
            {acceptingRequests
              ? "You are Online"
              : "You are Offline"}
          </Text>
        </View>

        <Text style={styles.subtitle}>
          Customers can send you requests
        </Text>

        <Text style={styles.earningsLabel}>
          Today's Earnings
        </Text>

        <Text style={styles.earnings}>
          ₹1,250
        </Text>

        <Pressable style={styles.pauseButton}>
          <Text style={styles.pauseText}>
            Pause Requests
          </Text>
        </Pressable>
      </View>

      <View style={styles.right}>
        <Switch
          value={acceptingRequests}
          onValueChange={onToggle}
          trackColor={{
            false: colors.border,
            true: colors.primary,
          }}
          thumbColor={colors.white}
        />

        <View style={styles.imagePlaceholder}>
          <Text style={styles.worker}>👷</Text>
        </View>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  card: {
    backgroundColor: colors.dashboard.heroBackground,
    borderColor: colors.dashboard.heroBorder,
    borderWidth: 1,
    borderRadius: radius.xl,
    padding: spacing.xl,
    marginBottom: spacing.xxxl,
    flexDirection: "row",
    justifyContent: "space-between",
    ...shadows.md,
  },

  left: {
    flex: 1,
  },

  right: {
    justifyContent: "space-between",
    alignItems: "center",
  },

  statusRow: {
    flexDirection: "row",
    alignItems: "center",
  },

  dot: {
    width: 10,
    height: 10,
    borderRadius: 5,
    marginRight: spacing.sm,
  },

  title: {
    ...typography.h2,
    color: colors.success,
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    marginTop: spacing.sm,
  },

  earningsLabel: {
    ...typography.body,
    marginTop: spacing.xxl,
    color: colors.text.secondary,
  },

  earnings: {
    ...typography.display,
    color: colors.text.primary,
    marginTop: spacing.sm,
  },

  pauseButton: {
    marginTop: spacing.xl,
    backgroundColor: "#CFEED9",
    paddingVertical: spacing.md,
    paddingHorizontal: spacing.xl,
    borderRadius: radius.round,
    alignSelf: "flex-start",
  },

  pauseText: {
    ...typography.subtitle,
    color: "#0E8F53",
    fontWeight: "700",
  },

  imagePlaceholder: {
    width: 120,
    height: 160,
    justifyContent: "center",
    alignItems: "center",
  },

  worker: {
    fontSize: 80,
  },
});