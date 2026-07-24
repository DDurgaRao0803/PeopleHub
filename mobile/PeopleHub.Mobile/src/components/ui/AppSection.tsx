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
  children: React.ReactNode;
}

export function AppSection({
  title,
  children,
}: Props): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>
        {title}
      </Text>

      {children}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: spacing.xxxl,
  },

  title: {
    ...typography.title,
    color: colors.text.primary,
    marginBottom: spacing.lg,
  },
});