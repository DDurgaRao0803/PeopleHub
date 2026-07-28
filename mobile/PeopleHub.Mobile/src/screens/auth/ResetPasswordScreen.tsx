import React, { useState } from "react";

import {
  Alert,
  KeyboardAvoidingView,
  Platform,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { NativeStackScreenProps } from "@react-navigation/native-stack";

import { AppCard } from "../../components/ui/AppCard";
import { AppTextInput } from "../../components/forms/AppTextInput";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";

import { AuthStackParamList } from "../../navigation/AuthStackParamList";
import { authService } from "../../services/authService";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { radius } from "../../theme/radius";
import { shadows } from "../../theme/shadows";
import { typography } from "../../theme/typography";

type Props = NativeStackScreenProps<
  AuthStackParamList,
  "ResetPassword"
>;

export function ResetPasswordScreen({
  navigation,
  route,
}: Props): React.JSX.Element {

  const {
    userId,
    otp,
  } = route.params;

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


      await authService.resetPassword(
        userId,
        otp,
        password,
        confirmPassword,
      );

navigation.navigate("Login");


    } catch (error: any) {

      const message =
        error?.response?.data?.message ??
        "Unable to reset your password.";

      Alert.alert(
        "Reset Password",
        message,
      );

    } finally {

      setLoading(false);

    }

  };

  return (
    <SafeAreaView style={styles.container}>
      <KeyboardAvoidingView
        style={styles.flex}
        behavior={
          Platform.OS === "ios"
            ? "padding"
            : undefined
        }
      >
        <ScrollView
          contentContainerStyle={styles.content}
          keyboardShouldPersistTaps="handled"
        >

          <View style={styles.heroContainer}>

            <Text style={styles.heroEmoji}>
              🔒
            </Text>

            <Text style={styles.title}>
              Reset Password
            </Text>

            <Text style={styles.subtitle}>
              Create a strong new password to secure your account.
            </Text>

          </View>

          <AppCard style={styles.resetCard}>

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

          </AppCard>

          <Text style={styles.version}>
            PeopleHub v1.0
          </Text>

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
    paddingHorizontal: spacing.lg,
    paddingTop: spacing.xxxl,
    paddingBottom: spacing.xxl,
  },

  title: {
    ...typography.h1,
    color: colors.text.primary,
    textAlign: "center",
    marginBottom: spacing.sm,
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
    lineHeight: 24,
    marginBottom: spacing.xxl,
  },

  heroContainer: {
    alignItems: "center",
    marginBottom: spacing.xxl,
  },

  heroEmoji: {
    fontSize: 56,
    marginBottom: spacing.md,
  },

  resetCard: {
    borderRadius: radius.xl,
    ...shadows.md,
  },

  version: {
    marginTop: spacing.xl,
    textAlign: "center",
    color: colors.text.secondary,
    ...typography.caption,
  },

});