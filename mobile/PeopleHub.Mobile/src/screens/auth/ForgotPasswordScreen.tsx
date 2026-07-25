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
  "ForgotPassword"
>;

export function ForgotPasswordScreen({
  navigation,
}: Props): React.JSX.Element {

  const [email, setEmail] = useState("");

  const [loading, setLoading] = useState(false);

  const handleContinue = async (): Promise<void> => {

    const value = email.trim();

    if (!/\S+@\S+\.\S+/.test(value)) {

      Alert.alert(
        "Invalid Email",
        "Please enter a valid email address."
      );

      return;

    }

    try {

      setLoading(true);

      // TODO
      // Send Reset OTP API

      navigation.navigate("OtpVerification", {
  destination: value,
  type: "email",
  purpose: "forgot-password",
});

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

          <Text style={styles.title}>
            Forgot Password
          </Text>

          <Text style={styles.subtitle}>
            Enter your registered email address.
            We&apos;ll send you a verification code.
          </Text>

          <AppTextInput
            label="Email Address"
            value={email}
            onChangeText={setEmail}
            placeholder="Enter your email"
            keyboardType="email-address"
          />

          <PrimaryButton
            title="Send OTP"
            loading={loading}
            onPress={handleContinue}
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