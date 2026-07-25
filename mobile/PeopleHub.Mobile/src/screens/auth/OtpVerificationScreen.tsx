import React, {
  useEffect,
  useRef,
  useState,
} from "react";

import {
  Alert,
  KeyboardAvoidingView,
  Platform,
  Pressable,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  View,
} from "react-native";

import { NativeStackScreenProps } from "@react-navigation/native-stack";

import { PrimaryButton } from "../../components/buttons/PrimaryButton";
import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { radius } from "../../theme/radius";
import { typography } from "../../theme/typography";

type Props = NativeStackScreenProps<
  AuthStackParamList,
  "OtpVerification"
>;

export function OtpVerificationScreen({
  navigation,
  route,
}: Props): React.JSX.Element {

  const {
  destination,
  type,
  purpose,
} = route.params;

  const inputRef = useRef<TextInput>(null);

  const [otp, setOtp] = useState("");

  const [seconds, setSeconds] = useState(60);

  const [loading, setLoading] = useState(false);

  useEffect(() => {

    inputRef.current?.focus();

  }, []);

  useEffect(() => {

    if (seconds === 0) {
      return;
    }

    const timer = setTimeout(() => {

      setSeconds((value) => value - 1);

    }, 1000);

    return () => clearTimeout(timer);

  }, [seconds]);

  const handleVerify = async (): Promise<void> => {

    if (otp.length !== 6) {

      Alert.alert(
        "Invalid OTP",
        "Please enter the 6-digit OTP."
      );

      return;

    }

    try {

      setLoading(true);

      // TODO:
// Verify OTP API

switch (purpose) {

  case "forgot-password":

    navigation.replace("ResetPassword", {
      destination,
    });

    return;

  case "login":

    Alert.alert(
      "Success",
      "Mobile number verified successfully."
    );

    // TODO:
    // navigation.replace("Home");

    return;

  case "register":

    Alert.alert(
      "Success",
      "OTP verified successfully."
    );

    // TODO:
    // Navigate to Customer/Provider registration

    return;

  case "verify-email":

    Alert.alert(
      "Success",
      "Email verified successfully."
    );

    return;

  default:

    Alert.alert(
      "Success",
      "OTP verified successfully."
    );

    return;
}

    } finally {

      setLoading(false);

    }

  };

  const handleResend = (): void => {

    setOtp("");

    setSeconds(60);

    Alert.alert(
      "OTP Sent",
      "A new OTP has been sent."
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
        >

          <Text style={styles.title}>
            Verify OTP
          </Text>

          <Text style={styles.subtitle}>
  {type === "mobile"
    ? "Enter the verification code sent to your mobile number"
    : "Enter the verification code sent to your email address"}
</Text>

          <Text style={styles.destination}>
  {destination}
</Text>

          <Pressable
            onPress={() =>
              inputRef.current?.focus()
            }
          >

            <View style={styles.otpContainer}>

              {Array.from({ length: 6 }).map(
                (_, index) => {

                  const digit = otp[index] ?? "";

                  return (
                    <View
  key={index}
  style={[
    styles.otpBox,
    index === otp.length &&
    otp.length < 6
      ? styles.activeOtpBox
      : undefined,
  ]}
>
                      <Text style={styles.otpText}>
                        {digit}
                      </Text>
                    </View>
                  );

                }
              )}

            </View>

          </Pressable>

          <TextInput
            ref={inputRef}
            value={otp}
            onChangeText={(text) => {
  const value = text.replace(/\D/g, "");

  if (value.length <= 6) {
    setOtp(value);

    if (value.length === 6) {
      setTimeout(() => {
        handleVerify();
      }, 150);
    }
  }
}}
keyboardType="number-pad"
            maxLength={6}
            style={styles.hiddenInput}
            textContentType="oneTimeCode"
            autoComplete="sms-otp"
          />

          <PrimaryButton
            title="Verify OTP"
            loading={loading}
            onPress={handleVerify}
          />

          {seconds > 0 ? (
            <Text style={styles.timer}>
              Resend OTP in {seconds}s
            </Text>
          ) : (
            <Pressable
              onPress={handleResend}
            >
              <Text style={styles.resend}>
                Resend OTP
              </Text>
            </Pressable>
          )}

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
    marginBottom: spacing.sm,
  },

  subtitle: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
  },

  destination: {
    ...typography.subtitle,
    color: colors.primary,
    textAlign: "center",
    marginTop: spacing.sm,
    marginBottom: spacing.xl,
  },

  otpContainer: {
  flexDirection: "row",
  justifyContent: "center",
  alignItems: "center",
  gap: spacing.sm,
  marginBottom: spacing.xl,
},

 otpBox: {
  width: 48,
  height: 56,
  marginHorizontal: 4,
  borderWidth: 1,
  borderColor: colors.border,
  borderRadius: radius.md,
  justifyContent: "center",
  alignItems: "center",
  backgroundColor: colors.surface,
},

activeOtpBox: {
  borderColor: colors.primary,
  borderWidth: 2,
},

  otpText: {
    ...typography.h2,
    color: colors.text.primary,
  },

  hiddenInput: {
    position: "absolute",
    opacity: 0,
  },

  timer: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
    marginTop: spacing.lg,
  },

  resend: {
    ...typography.body,
    color: colors.primary,
    textAlign: "center",
    marginTop: spacing.lg,
  },

});