import React from "react";
import {
  ActivityIndicator,
  Pressable,
  StyleSheet,
  Text,
} from "react-native";

import {
  colors,
  radius,
  shadows,
  spacing,
  typography,
} from "../../theme";

type Variant =
  | "primary"
  | "secondary"
  | "outline"
  | "danger";

interface AppButtonProps {
  title: string;

  onPress: () => void;

  variant?: Variant;

  disabled?: boolean;

  loading?: boolean;

  fullWidth?: boolean;
}

export function AppButton({
  title,
  onPress,
  variant = "primary",
  disabled = false,
  loading = false,
  fullWidth = true,
}: AppButtonProps): React.JSX.Element {
  const isOutline = variant === "outline";

  const backgroundColor =
    disabled
      ? colors.text.disabled
      : isOutline
      ? colors.surface
      : variant === "danger"
      ? colors.error
      : variant === "secondary"
      ? colors.secondary
      : colors.primary;

  const borderColor =
    isOutline
      ? colors.primary
      : backgroundColor;

  const textColor =
    isOutline
      ? colors.primary
      : colors.text.inverse;

  return (
    <Pressable
      disabled={disabled || loading}
      onPress={onPress}
      style={({ pressed }) => [
        styles.button,
        {
          backgroundColor,
          borderColor,
          width: fullWidth ? "100%" : undefined,
          opacity: pressed ? 0.9 : 1,
        },
      ]}
    >
      {loading ? (
        <ActivityIndicator color={textColor} />
      ) : (
        <Text
          style={[
            styles.text,
            {
              color: textColor,
            },
          ]}
        >
          {title}
        </Text>
      )}
    </Pressable>
  );
}

const styles = StyleSheet.create({
  button: {
    height: 54,

    justifyContent: "center",
    alignItems: "center",

    borderRadius: radius.lg,

    borderWidth: 1,

    paddingHorizontal: spacing.xl,

    ...shadows.sm,
  },

  text: {
    ...typography.button,
  },
});