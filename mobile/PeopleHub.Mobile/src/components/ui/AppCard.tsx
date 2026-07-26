import React from "react";

import {
  StyleProp,
  StyleSheet,
  View,
  ViewStyle,
} from "react-native";

import { colors } from "../../theme/colors";
import { radius } from "../../theme/radius";
import { shadows } from "../../theme/shadows";

interface AppCardProps {
  children: React.ReactNode;
  style?: StyleProp<ViewStyle>;
}

export function AppCard({
  children,
  style,
}: AppCardProps): React.JSX.Element {

  return (
    <View
      style={[
        styles.card,
        style,
      ]}
    >
      {children}
    </View>
  );
}

const styles = StyleSheet.create({

  card: {

    backgroundColor: colors.surface,

    borderRadius: radius.lg,

    padding: 16,

    borderWidth: 1,
    borderColor: colors.border,

    ...shadows.sm,

  },

});