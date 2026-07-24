import React from "react";
import {
  StyleSheet,
  Text,
  TextInput,
  View,
  TextInputProps,
} from "react-native";

import {
  colors,
  radius,
  spacing,
  typography,
} from "../../theme";

interface AppTextFieldProps extends TextInputProps {
  label: string;
  error?: string;
}

export function AppTextField({
  label,
  error,
  style,
  ...props
}: AppTextFieldProps): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.label}>{label}</Text>

      <TextInput
        {...props}
        placeholderTextColor={colors.input.placeholder}
        style={[
          styles.input,
          error && styles.errorInput,
          style,
        ]}
      />

      {error ? (
        <Text style={styles.error}>
          {error}
        </Text>
      ) : null}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: spacing.xxl,
  },

  label: {
    ...typography.subtitle,
    color: colors.text.primary,
    marginBottom: spacing.sm,
  },

  input: {
    height: 54,
    borderWidth: 1,
    borderColor: colors.input.border,
    borderRadius: radius.lg,
    paddingHorizontal: spacing.lg,
    backgroundColor: colors.surface,
    ...typography.bodyLarge,
  },

  errorInput: {
    borderColor: colors.error,
  },

  error: {
    marginTop: spacing.xs,
    color: colors.error,
    ...typography.caption,
  },
});