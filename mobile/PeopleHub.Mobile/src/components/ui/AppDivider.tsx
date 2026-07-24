import React from "react";
import {
  StyleSheet,
  View,
} from "react-native";

import {
  colors,
  spacing,
} from "../../theme";

export function AppDivider(): React.JSX.Element {
  return (
    <View style={styles.divider} />
  );
}

const styles = StyleSheet.create({
  divider: {
    height: 1,
    backgroundColor: colors.divider,
    marginVertical: spacing.xl,
  },
});