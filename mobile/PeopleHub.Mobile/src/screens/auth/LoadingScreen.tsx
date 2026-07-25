import React from "react";
import {
  ActivityIndicator,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

export default function LoadingScreen(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Ionicons
        name="people-circle"
        size={120}
        color={colors.primary}
      />

      <Text style={styles.title}>
        PeopleHub
      </Text>

      <Text style={styles.subtitle}>
        Loading...
      </Text>

      <ActivityIndicator
        size="large"
        color={colors.primary}
        style={{ marginTop: spacing.xl }}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
    justifyContent: "center",
    alignItems: "center",
  },

  title: {
    ...typography.h1,
    color: colors.text.primary,
    marginTop: spacing.lg,
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    marginTop: spacing.sm,
  },
});