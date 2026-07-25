import React, { useState } from "react";

import {
  StyleSheet,
  Text,
  TextInput,
  View,
  TextInputProps,
  TouchableOpacity,
} from "react-native";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { radius } from "../../theme/radius";
import { typography } from "../../theme/typography";

interface AppTextInputProps extends TextInputProps {
  label: string;
  error?: string;
  showPasswordToggle?: boolean;
}

export function AppTextInput({
  label,
  error,
  secureTextEntry = false,
  showPasswordToggle = false,
  ...props
}: AppTextInputProps): React.JSX.Element {

  const [passwordVisible, setPasswordVisible] =
    useState(false);

  return (
    <View style={styles.container}>

      <Text style={styles.label}>
        {label}
      </Text>

      <View style={styles.inputContainer}>

        <TextInput
          {...props}
          secureTextEntry={
            showPasswordToggle
              ? !passwordVisible
              : secureTextEntry
          }
          autoCapitalize={
            props.autoCapitalize ?? "none"
          }
          autoCorrect={
            props.autoCorrect ?? false
          }
          style={[
            styles.input,
            showPasswordToggle
              ? styles.inputWithIcon
              : undefined,
            error
              ? styles.errorBorder
              : undefined,
            props.style,
          ]}
        />

        {showPasswordToggle && (
          <TouchableOpacity
            style={styles.eyeButton}
            onPress={() =>
              setPasswordVisible(!passwordVisible)
            }
          >
            <Text style={styles.eyeIcon}>
              {passwordVisible ? "🙈" : "👁"}
            </Text>
          </TouchableOpacity>
        )}

      </View>

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
    marginBottom: spacing.md,
  },

  label: {
    ...typography.subtitle,
    marginBottom: spacing.xs,
    color: colors.text.primary,
  },

  inputContainer: {
    position: "relative",
    justifyContent: "center",
  },

  input: {
    borderWidth: 1,
    borderColor: colors.border,
    borderRadius: radius.md,
    paddingHorizontal: spacing.md,
    paddingVertical: spacing.md,
    backgroundColor: colors.surface,
    color: colors.text.primary,
    ...typography.body,
  },

  inputWithIcon: {
    paddingRight: 52,
  },

  eyeButton: {
    position: "absolute",
    right: spacing.md,
    height: "100%",
    justifyContent: "center",
    alignItems: "center",
  },

  eyeIcon: {
    fontSize: 18,
  },

  errorBorder: {
    borderColor: colors.error,
  },

  error: {
    marginTop: spacing.xs,
    color: colors.error,
    ...typography.caption,
  },

});