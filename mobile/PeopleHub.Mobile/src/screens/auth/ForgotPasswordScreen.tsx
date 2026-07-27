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
import { authService } from "../../services/authService";
import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { radius } from "../../theme/radius";
import { shadows } from "../../theme/shadows";
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

      await authService.forgotPassword(value);

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

          <View style={styles.heroContainer}>

  <Text style={styles.heroEmoji}>
    🔑
  </Text>

  <Text style={styles.title}>
    Forgot Password?
  </Text>

  <Text style={styles.subtitle}>
    Enter your registered email address and we&apos;ll
    send you a verification code.
  </Text>

</View>

<AppCard style={styles.resetCard}>

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