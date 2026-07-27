import React, { useState } from "react";
import {
  Alert,
  KeyboardAvoidingView,
  Platform,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import { NativeStackScreenProps } from "@react-navigation/native-stack";

import { AppCard } from "../../components/ui/AppCard";
import { AppTextInput } from "../../components/forms/AppTextInput";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { SocialButton } from "../../components/buttons/SocialButton";

import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { radius } from "../../theme/radius";
import { shadows } from "../../theme/shadows";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

import { useAuth } from "../../context/AuthContext";

type Props = NativeStackScreenProps<
  AuthStackParamList,
  "Login"
>;

export function LoginScreen({
  navigation,
}: Props): React.JSX.Element {

  const { login } = useAuth();

  const [identifier, setIdentifier] = useState("");
  const [password, setPassword] = useState("");

  const [loading, setLoading] = useState(false);

  const [error, setError] = useState("");

  const [loginMode, setLoginMode] = useState<
    "initial" | "email" | "mobile"
  >("initial");

  const isEmail = (value: string): boolean => {
    return /\S+@\S+\.\S+/.test(value);
  };

  const isMobile = (value: string): boolean => {

    const digits = value.replace(/\D/g, "");

    return (
      digits.length >= 8 &&
      digits.length <= 15
    );
  };

  const handleContinue = (): void => {

    if (loginMode === "email") {
      return;
    }

    if (loginMode === "mobile") {
      handleSendOtp();
      return;
    }

    setError(
      "Please enter a valid email or mobile number."
    );

  };

  const handleLogin = async (): Promise<void> => {

    if (!identifier.trim()) {

      Alert.alert(
        "Email Required",
        "Please enter your email address."
      );

      return;

    }

    if (!password.trim()) {

      Alert.alert(
        "Password Required",
        "Please enter your password."
      );

      return;

    }

    try {

      setLoading(true);

      await login({
        email: identifier.trim(),
        password: password.trim(),
      });

      // AppNavigator automatically switches
      // after successful authentication.

    } catch (error: any) {

      const message =
        error?.response?.data?.message ??
        "Invalid email or password.";

      Alert.alert(
        "Login Failed",
        message,
      );

    } finally {

      setLoading(false);

    }

  };

  const handleSendOtp = (): void => {

    navigation.navigate(
      "OtpVerification",
      {
        destination: identifier.trim(),
        type: "mobile",
        purpose: "login",
      }
    );

  };

  const handleGoogle = (): void => {

    Alert.alert(
      "Coming Soon",
      "Google Sign-In will be available soon."
    );

  };

  const handleFacebook = (): void => {

    Alert.alert(
      "Coming Soon",
      "Facebook Sign-In will be available soon."
    );

  };

  const handleCreateAccount = (): void => {

    navigation.navigate(
      "AccountType"
    );

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
        showsVerticalScrollIndicator={false}
      >

        {/* Hero Section */}

        <View style={styles.heroContainer}>

          <Text style={styles.heroEmoji}>
            👋
          </Text>

          <Text style={styles.welcomeText}>
            Welcome Back
          </Text>

          <Text style={styles.heroSubtitle}>
            Sign in to continue using
            {"\n"}
            PeopleHub
          </Text>

        </View>

        {/* Login Card */}

        <AppCard
          style={styles.loginCard}
        >

          <AppTextInput
            label="Email or Mobile Number"
            value={identifier}
            onChangeText={(text) => {

              setIdentifier(text);

              const value = text.trim();

              if (isEmail(value)) {
                setLoginMode("email");
              } else if (isMobile(value)) {
                setLoginMode("mobile");
              } else {
                setLoginMode("initial");
              }

              if (error) {
                setError("");
              }

            }}
            placeholder="Enter your email or mobile number"
            error={error}
          />

          {loginMode === "email" && (
            <>

              <AppTextInput
                label="Password"
                value={password}
                onChangeText={setPassword}
                placeholder="Enter your password"
                secureTextEntry
                showPasswordToggle
              />

              <TouchableOpacity
                onPress={() =>
                  navigation.navigate(
                    "ForgotPassword"
                  )
                }
              >
                <Text
                  style={styles.forgotPassword}
                >
                  Forgot Password?
                </Text>
              </TouchableOpacity>

            </>
          )}

          <PrimaryButton
            title={
              loginMode === "initial"
                ? "Continue"
                : loginMode === "email"
                ? "Login"
                : "Send OTP"
            }
            loading={loading}
            onPress={() => {

              if (
                loginMode === "initial"
              ) {
                handleContinue();
                return;
              }

              if (
                loginMode === "email"
              ) {
                handleLogin();
                return;
              }

              handleSendOtp();

            }}
          />

        </AppCard>

        {/* Divider */}

        <View style={styles.dividerContainer}>

          <View style={styles.divider} />

          <Text style={styles.dividerText}>
            OR CONTINUE WITH
          </Text>

          <View style={styles.divider} />

        </View>

        {/* Social */}

        <SocialButton
          title="Continue with Google"
          provider="google"
          onPress={handleGoogle}
        />

        <View
          style={styles.socialSpacing}
        />

        <SocialButton
          title="Continue with Facebook"
          provider="facebook"
          onPress={handleFacebook}
        />

        {/* Footer */}

        <View style={styles.footer}>

          <Text style={styles.footerText}>
  Don&apos;t have an account?
</Text>

          <TouchableOpacity
            onPress={
              handleCreateAccount
            }
          >
            <Text
              style={styles.createAccount}
            >
              Create Account →
            </Text>
          </TouchableOpacity>

        </View>

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
    justifyContent: "center",
    paddingHorizontal: spacing.lg,
    paddingTop: spacing.xxxl ?? 48,
    paddingBottom: spacing.xxl ?? 32,
  },

  heroContainer: {
    alignItems: "center",
    marginBottom: spacing.xl,
  },

  heroEmoji: {
    fontSize: 52,
    marginBottom: spacing.md,
  },

  welcomeText: {
    ...typography.h1,
    color: colors.text.primary,
    textAlign: "center",
    marginBottom: spacing.sm,
  },

  heroSubtitle: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
    lineHeight: 24,
  },

  loginCard: {
    marginVertical: spacing.lg,
    borderRadius: radius.xl,
    ...shadows.md,
  },

  forgotPassword: {
    ...typography.body,
    color: colors.primary,
    textAlign: "right",
    marginTop: spacing.sm,
    marginBottom: spacing.lg,
    fontWeight: "600",
  },

  dividerContainer: {
    flexDirection: "row",
    alignItems: "center",
    marginVertical: spacing.xl,
  },

  divider: {
    flex: 1,
    height: 1,
    backgroundColor: colors.border,
  },

  dividerText: {
    ...typography.caption,
    color: colors.text.secondary,
    marginHorizontal: spacing.md,
    fontWeight: "600",
    letterSpacing: 0.5,
  },

  socialSpacing: {
    height: spacing.md,
  },

  footer: {
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    marginTop: spacing.xl,
    flexWrap: "wrap",
  },

  footerText: {
    ...typography.body,
    color: colors.text.secondary,
  },

  createAccount: {
    ...typography.body,
    color: colors.primary,
    fontWeight: "700",
    marginLeft: spacing.xs,
  },

  version: {
    ...typography.caption,
    color: colors.text.secondary,
    textAlign: "center",
    marginTop: spacing.xxl,
    opacity: 0.7,
  },

});