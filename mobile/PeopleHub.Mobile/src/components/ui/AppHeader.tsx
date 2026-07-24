import React from "react";
import {
  StyleSheet,
  Text,
  View,
} from "react-native";

import {
  colors,
  spacing,
  typography,
} from "../../theme";

interface Props {
  title: string;
  subtitle?: string;
}

export function AppHeader({
  title,
  subtitle,
}: Props): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>
        {title}
      </Text>

      {subtitle ? (
        <Text style={styles.subtitle}>
          {subtitle}
        </Text>
      ) : null}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: spacing.xxxl,
  },

  title: {
    ...typography.h1,
    color: colors.text.primary,
  },

  subtitle: {
    ...typography.bodyLarge,
    color: colors.text.secondary,
    marginTop: spacing.sm,
  },
});