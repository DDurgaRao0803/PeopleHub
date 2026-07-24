import React from "react";
import {
  ActivityIndicator,
  StyleSheet,
  Text,
  View,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";

import { AppScreen } from "../../components/ui/AppScreen";
import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

export default function SplashScreen(): React.JSX.Element {
  return (
    <AppScreen>
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
          Trusted Services.
          {"\n"}
          Trusted Professionals.
        </Text>

        <ActivityIndicator
          size="large"
          color={colors.primary}
          style={styles.loader}
        />

      </View>
    </AppScreen>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    paddingHorizontal: spacing.xl,
    backgroundColor: colors.background,
  },

  title: {
    ...typography.h1,
    color: colors.primary,
    marginTop: spacing.lg,
    marginBottom: spacing.sm,
    textAlign: "center",
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
    lineHeight: 24,
  },

  loader: {
    marginTop: spacing.xxxl,
  },
});