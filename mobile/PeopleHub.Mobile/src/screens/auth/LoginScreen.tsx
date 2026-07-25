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

import { AppTextInput } from "../../components/forms/AppTextInput";
import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { SocialButton } from "../../components/buttons/SocialButton";

import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

type Props = NativeStackScreenProps<
  AuthStackParamList,
  "Login"
>;

export function LoginScreen({
  navigation,
}: Props): React.JSX.Element {

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

    return digits.length >= 8 &&
           digits.length <= 15;
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

  if (!password.trim()) {

    Alert.alert(
      "Password Required",
      "Please enter your password."
    );

    return;
  }

  try {

    setLoading(true);

    // TODO:
    // Backend Login API

    Alert.alert(
      "Coming Soon",
      "Backend authentication will be connected next."
    );

  } finally {

    setLoading(false);

  }
};

const handleSendOtp = (): void => {

  navigation.navigate("OtpVerification", {
  destination: identifier.trim(),
  type: "mobile",
  purpose: "login",
});

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

    navigation.navigate("AccountType");
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
          showsVerticalScrollIndicator={false}
        >
          <View style={styles.header}>
            <Text style={styles.appName}>
              PeopleHub
            </Text>

            <Text style={styles.title}>
              Welcome Back
            </Text>

            <Text style={styles.subtitle}>
              Sign in with your email or mobile number
            </Text>
          </View>

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
  onPress={() => navigation.navigate("ForgotPassword")}
>
  <Text style={styles.forgotPassword}>
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
    if (loginMode === "initial") {
      handleContinue();
      return;
    }

    if (loginMode === "email") {
      handleLogin();
      return;
    }

     handleSendOtp();

  }}
/>

          <View style={styles.dividerContainer}>
            <View style={styles.divider} />

            <Text style={styles.dividerText}>
              OR
            </Text>

            <View style={styles.divider} />
          </View>

          <SocialButton
            title="Continue with Google"
            provider="google"
            onPress={handleGoogle}
          />

          <SocialButton
            title="Continue with Facebook"
            provider="facebook"
            onPress={handleFacebook}
          />

          <View style={styles.footer}>
            <Text style={styles.footerText}>
  Don&apos;t have an account?
</Text>

            <TouchableOpacity
              onPress={handleCreateAccount}
            >
              <Text style={styles.createAccount}>
                Create Account
              </Text>
            </TouchableOpacity>
          </View>
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
    paddingVertical: spacing.xl,
  },

  header: {
    marginBottom: spacing.xl,
    alignItems: "center",
  },

  appName: {
    ...typography.h1,
    color: colors.primary,
    marginBottom: spacing.sm,
    textAlign: "center",
  },

  title: {
    ...typography.h2,
    color: colors.text.primary,
    textAlign: "center",
    marginBottom: spacing.sm,
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
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

  forgotPassword: {
  ...typography.body,
  color: colors.primary,
  textAlign: "right",
  marginBottom: spacing.lg,
},
});
