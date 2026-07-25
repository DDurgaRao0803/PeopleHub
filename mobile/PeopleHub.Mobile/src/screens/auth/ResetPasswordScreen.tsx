import React, { useState } from "react";

import {
  Alert,
  KeyboardAvoidingView,
  Platform,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
} from "react-native";

import { NativeStackScreenProps } from "@react-navigation/native-stack";

import { AppTextInput } from "../../components/forms/AppTextInput";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";

import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

type Props = NativeStackScreenProps<
  AuthStackParamList,
  "ResetPassword"
>;

export function ResetPasswordScreen({
  navigation,
}: Props): React.JSX.Element {

  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [loading, setLoading] = useState(false);

  const handleReset = async (): Promise<void> => {

    if (!password.trim()) {

      Alert.alert(
        "Password Required",
        "Please enter a new password."
      );

      return;

    }

    if (password !== confirmPassword) {

      Alert.alert(
        "Password Mismatch",
        "Passwords do not match."
      );

      return;

    }

    try {

      setLoading(true);

      // TODO:
      // Reset Password API

      Alert.alert(
        "Success",
        "Your password has been updated successfully.",
        [
          {
            text: "OK",
            onPress: () => navigation.navigate("Login"),
          },
        ]
      );

    } finally {

      setLoading(false);

    }

  };

  return (
    <SafeAreaView style={styles.container}>
      <KeyboardAvoidingView
        style={styles.flex}
        behavior={Platform.OS === "ios" ? "padding" : undefined}
      >
        <ScrollView
          contentContainerStyle={styles.content}
          keyboardShouldPersistTaps="handled"
        >
          <Text style={styles.title}>
            Reset Password
          </Text>

          <Text style={styles.subtitle}>
            Create a new password for your account.
          </Text>

          <AppTextInput
            label="New Password"
            value={password}
            onChangeText={setPassword}
            secureTextEntry
            showPasswordToggle
            placeholder="Enter new password"
          />

          <AppTextInput
            label="Confirm Password"
            value={confirmPassword}
            onChangeText={setConfirmPassword}
            secureTextEntry
            showPasswordToggle
            placeholder="Confirm new password"
          />

          <PrimaryButton
            title="Reset Password"
            loading={loading}
            onPress={handleReset}
          />
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );

}

const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: colors.background,
  },

  flex: {
    flex: 1,
  },

  content: {
    flexGrow: 1,
    justifyContent: "center",
    paddingHorizontal: spacing.lg,
  },

  title: {
    ...typography.h2,
    color: colors.text.primary,
    textAlign: "center",
    marginBottom: spacing.md,
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
    marginBottom: spacing.xl,
  },

});