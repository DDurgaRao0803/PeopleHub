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
import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { radius } from "../../theme/radius";
import { typography } from "../../theme/typography";

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

  const handleRegister = async (): Promise<void> => {

    if (!firstName.trim()) {
      Alert.alert("Validation", "Enter first name.");
      return;
    }

    if (!lastName.trim()) {
      Alert.alert("Validation", "Enter last name.");
      return;
    }

    if (!mobile.trim()) {
      Alert.alert("Validation", "Enter mobile number.");
      return;
    }

    if (!email.trim()) {
      Alert.alert("Validation", "Enter email.");
      return;
    }

    if (!password) {
      Alert.alert("Validation", "Enter password.");
      return;
    }

    if (password !== confirmPassword) {
      Alert.alert(
        "Validation",
        "Passwords do not match."
      );
      return;
    }

    if (!acceptedTerms) {
      Alert.alert(
        "Validation",
        "Accept Terms & Conditions."
      );
      return;
    }

    try {

      setLoading(true);

      // TODO
      // Registration API

      navigation.navigate("OtpVerification", {
        destination: mobile,
        type: "mobile",
        purpose: "register",
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
            Create Account
          </Text>

          <Text style={styles.subtitle}>
            Register as a Customer
          </Text>

          <AppTextInput
            label="First Name"
            value={firstName}
            onChangeText={setFirstName}
          />

          <AppTextInput
            label="Last Name"
            value={lastName}
            onChangeText={setLastName}
          />

          <AppTextInput
            label="Mobile Number"
            value={mobile}
            onChangeText={setMobile}
            keyboardType="phone-pad"
          />

          <AppTextInput
            label="Email Address"
            value={email}
            onChangeText={setEmail}
            keyboardType="email-address"
          />

          <AppTextInput
            label="Password"
            value={password}
            onChangeText={setPassword}
            secureTextEntry
            showPasswordToggle
          />

          <AppTextInput
            label="Confirm Password"
            value={confirmPassword}
            onChangeText={setConfirmPassword}
            secureTextEntry
            showPasswordToggle
          />

          <TouchableOpacity
            style={styles.termsRow}
            onPress={() =>
              setAcceptedTerms(!acceptedTerms)
            }
          >
            <View
              style={[
                styles.checkbox,
                acceptedTerms && styles.checkboxSelected,
              ]}
            />

            <Text style={styles.termsText}>
              I accept the Terms & Conditions
            </Text>

          </TouchableOpacity>

          <PrimaryButton
            title="Create Account"
            loading={loading}
            onPress={handleRegister}
          />

          <TouchableOpacity
  onPress={() => navigation.navigate("Login")}
>
            <Text style={styles.login}>
              Already have an account? Login
            </Text>
          </TouchableOpacity>

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
    padding: spacing.lg,
    justifyContent: "center",
  },

  title: {
    ...typography.h2,
    color: colors.text.primary,
    textAlign: "center",
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
    marginBottom: spacing.xl,
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
    marginTop: spacing.lg,
    textAlign: "center",
    color: colors.primary,
    ...typography.body,
  },

});