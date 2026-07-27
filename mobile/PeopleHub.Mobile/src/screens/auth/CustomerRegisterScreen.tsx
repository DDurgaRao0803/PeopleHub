import React, { useState } from "react";

import {
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
import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { radius } from "../../theme/radius";
import { shadows } from "../../theme/shadows";
import { typography } from "../../theme/typography";
import { authService } from "../../services/authService";

import {
  validateEmail,
  validateMobile,
  validatePassword,
  validateRequired,
} from "../../utils";

type Props = NativeStackScreenProps<
  AuthStackParamList,
  "CustomerRegister"
>;

export function CustomerRegisterScreen({
  navigation,
}: Props): React.JSX.Element {

  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [mobile, setMobile] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [acceptedTerms, setAcceptedTerms] = useState(false);

  const [loading, setLoading] = useState(false);
  
  const [errors, setErrors] = useState({
  firstName: "",
  lastName: "",
  mobile: "",
  email: "",
  password: "",
  confirmPassword: "",
  terms: "",
});

  const handleRegister = async (): Promise<void> => {

  const newErrors = {
    firstName:
      validateRequired(firstName, "First name") ?? "",
    lastName:
      validateRequired(lastName, "Last name") ?? "",
    mobile:
      validateMobile(mobile) ?? "",
    email:
      validateEmail(email) ?? "",
    password: "",
    confirmPassword: "",
    terms: "",
  };

  const passwordResult =
    validatePassword(password);


  if (!passwordResult.isValid) {
    newErrors.password =
      "Password does not meet the required criteria.";
  }

  if (!confirmPassword.trim()) {
    newErrors.confirmPassword =
      "Confirm password is required.";
  } else if (password !== confirmPassword) {
    newErrors.confirmPassword =
      "Passwords do not match.";
  }

  if (!acceptedTerms) {
    newErrors.terms =
      "Please accept the Terms & Conditions.";
  }

  setErrors(newErrors);

  const hasErrors =
    Object.values(newErrors).some(
      (error) => error.length > 0,
    );

  if (hasErrors) {
    return;
  }

  try {
  setLoading(true);

  const userId =
  await authService.registerCustomer({
    firstName,
    lastName,
    email,
    password,
    phoneNumber: mobile,
  });

  authService.setPendingRegistration(
    email,
    password,
);

  navigation.navigate("OtpVerification", {
  userId,
  destination: mobile,
  type: "mobile",
  purpose: "register",
});
} catch (error: any) {
  const message =
    error?.response?.data?.message ??
    "Registration failed. Please try again.";

  if (
    message.toLowerCase().includes("email")
  ) {
    setErrors((previous) => ({
      ...previous,
      email: message,
    }));
  } else if (
    message.toLowerCase().includes("phone")
  ) {
    setErrors((previous) => ({
      ...previous,
      mobile: message,
    }));
  } else {
    setErrors((previous) => ({
      ...previous,
      email: message,
    }));
  }
}finally {
  setLoading(false);
}

};

const passwordValidation =
  validatePassword(password);

  const isFormValid =
  validateRequired(firstName, "First name") === null &&
  validateRequired(lastName, "Last name") === null &&
  validateMobile(mobile) === null &&
  validateEmail(email) === null &&
  passwordValidation.isValid &&
  password === confirmPassword &&
  confirmPassword.trim().length > 0 &&
  acceptedTerms;

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
          Create Account
        </Text>

        <Text style={styles.subtitle}>
          Register as a Customer
        </Text>

        <View style={styles.heroContainer}>

  <Text style={styles.heroEmoji}>
    👋
  </Text>

  <Text style={styles.title}>
    Create Account
  </Text>

  <Text style={styles.subtitle}>
    Join PeopleHub and connect{"\n"}
    with trusted professionals.
  </Text>

</View>

<AppCard style={styles.registerCard}>

<AppTextInput
  label="First Name"
  value={firstName}
  onChangeText={(text) => {
    setFirstName(text);

    setErrors((previous) => ({
      ...previous,
      firstName:
        validateRequired(text, "First name") ?? "",
    }));
  }}
/>

        {errors.firstName ? (
          <Text style={styles.errorText}>
            {errors.firstName}
          </Text>
        ) : null}

        <AppTextInput
  label="Last Name"
  value={lastName}
  onChangeText={(text) => {
    setLastName(text);

    setErrors((previous) => ({
      ...previous,
      lastName:
        validateRequired(text, "Last name") ?? "",
    }));
  }}
/>

        {errors.lastName ? (
          <Text style={styles.errorText}>
            {errors.lastName}
          </Text>
        ) : null}

        <AppTextInput
  label="Mobile Number"
  value={mobile}
  onChangeText={(text) => {
    setMobile(text);

    setErrors((previous) => ({
      ...previous,
      mobile:
        validateMobile(text) ?? "",
    }));
  }}
  keyboardType="phone-pad"
/>

        {errors.mobile ? (
          <Text style={styles.errorText}>
            {errors.mobile}
          </Text>
        ) : null}

        <AppTextInput
  label="Email Address"
  value={email}
  onChangeText={(text) => {
    setEmail(text);

    setErrors((previous) => ({
      ...previous,
      email:
        validateEmail(text) ?? "",
    }));
  }}
  keyboardType="email-address"
/>

        {errors.email ? (
          <Text style={styles.errorText}>
            {errors.email}
          </Text>
        ) : null}

        <AppTextInput
  label="Password"
  value={password}
  onChangeText={(text) => {
    setPassword(text);

    const result =
      validatePassword(text);

    setErrors((previous) => ({
      ...previous,
      password: result.isValid
        ? ""
        : "Password does not meet the required criteria.",
    }));
  }}
  secureTextEntry
  showPasswordToggle
/>

        {errors.password ? (
          <Text style={styles.errorText}>
            {errors.password}
          </Text>
        ) : null}

        {password.length > 0 && (
  <View style={styles.passwordChecklist}>

    <Text
      style={
        passwordValidation.hasMinLength
          ? styles.checkValid
          : styles.checkInvalid
      }
    >
      {passwordValidation.hasMinLength ? "✓" : "○"} At least 8 characters
    </Text>

    <Text
      style={
        passwordValidation.hasUppercase
          ? styles.checkValid
          : styles.checkInvalid
      }
    >
      {passwordValidation.hasUppercase ? "✓" : "○"} One uppercase letter
    </Text>

    <Text
      style={
        passwordValidation.hasLowercase
          ? styles.checkValid
          : styles.checkInvalid
      }
    >
      {passwordValidation.hasLowercase ? "✓" : "○"} One lowercase letter
    </Text>

    <Text
      style={
        passwordValidation.hasNumber
          ? styles.checkValid
          : styles.checkInvalid
      }
    >
      {passwordValidation.hasNumber ? "✓" : "○"} One number
    </Text>

    <Text
      style={
        passwordValidation.hasSpecialCharacter
          ? styles.checkValid
          : styles.checkInvalid
      }
    >
      {passwordValidation.hasSpecialCharacter ? "✓" : "○"} One special character
    </Text>

  </View>
)}

        <AppTextInput
  label="Confirm Password"
  value={confirmPassword}
  onChangeText={(text) => {
    setConfirmPassword(text);

    setErrors((previous) => ({
      ...previous,
      confirmPassword:
  !text
    ? ""
    : text === password
      ? ""
      : "Passwords do not match.",
    }));
  }}
  secureTextEntry
  showPasswordToggle
/>

        {errors.confirmPassword ? (
          <Text style={styles.errorText}>
            {errors.confirmPassword}
          </Text>
        ) : null}

        <TouchableOpacity
          style={styles.termsRow}
          onPress={() => {
  const accepted = !acceptedTerms;

  setAcceptedTerms(accepted);

  setErrors((previous) => ({
    ...previous,
    terms: accepted
      ? ""
      : "Please accept the Terms & Conditions.",
  }));
}}
        >
          <View
            style={[
              styles.checkbox,
              acceptedTerms &&
                styles.checkboxSelected,
            ]}
          />

          <Text style={styles.termsText}>
            I accept the Terms & Conditions
          </Text>
        </TouchableOpacity>

        {errors.terms ? (
          <Text style={styles.errorText}>
            {errors.terms}
          </Text>
        ) : null}

        <PrimaryButton
  title="Create Account"
  loading={loading}
  disabled={!isFormValid}
  onPress={handleRegister}
/>

        <TouchableOpacity>
  <Text style={styles.login}>
    Already have an account? Sign In →
  </Text>
</TouchableOpacity>

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
},

heroContainer: {
  alignItems: "center",
  marginBottom: spacing.xxl,
},

heroEmoji: {
  fontSize: 56,
  marginBottom: spacing.md,
},

registerCard: {
  borderRadius: radius.xl,
  ...shadows.md,
},

  termsRow: {
    flexDirection: "row",
    alignItems: "center",
    marginBottom: spacing.lg,
  },

  checkbox: {
    width: 22,
    height: 22,
    borderWidth: 1,
    borderColor: colors.border,
    borderRadius: radius.sm,
    marginRight: spacing.sm,
  },

  checkboxSelected: {
    backgroundColor: colors.primary,
    borderColor: colors.primary,
  },

  termsText: {
    flex: 1,
    ...typography.body,
    color: colors.text.primary,
  },

  login: {
  marginTop: spacing.xl,
  textAlign: "center",
  color: colors.primary,
  ...typography.body,
  fontWeight: "600",
},

errorText: {
  color: colors.error,
  fontSize: 13,
  marginTop: 4,
  marginBottom: 10,
},

passwordChecklist: {
  marginTop: 6,
  marginBottom: spacing.md,
},

checkValid: {
  color: colors.success,
  fontSize: 13,
  marginBottom: 4,
},

checkInvalid: {
  color: colors.text.secondary,
  fontSize: 13,
  marginBottom: 4,
},

version: {
  marginTop: spacing.xl,
  textAlign: "center",
  color: colors.text.secondary,
  ...typography.caption,
},

});