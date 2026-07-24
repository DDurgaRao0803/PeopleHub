import React from "react";
import {
  StyleSheet,
  View,
  ViewProps,
} from "react-native";

import {
  colors,
  radius,
  shadows,
  spacing,
} from "../../theme";

export function AppCard(
  props: ViewProps,
): React.JSX.Element {
  return (
    <View
      {...props}
      style={[
        styles.card,
        props.style,
      ]}
    />
  );
}

const styles = StyleSheet.create({
  card: {
    backgroundColor: colors.card.background,
    borderRadius: radius.xl,
    borderWidth: 1,
    borderColor: colors.card.border,
    padding: spacing.xl,
    ...shadows.sm,
  },
});