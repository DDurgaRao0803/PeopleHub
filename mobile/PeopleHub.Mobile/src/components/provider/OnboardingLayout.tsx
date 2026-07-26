import React from "react";

import {
  Alert,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import {
  useNavigation,
  CommonActions,
} from "@react-navigation/native";

import { PrimaryButton } from "../buttons/PrimaryButton";

import { authService } from "../../services/authService";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

interface OnboardingLayoutProps {
  title: string;
  subtitle: string;

  step: number;
  totalSteps: number;

  children: React.ReactNode;

  nextButtonTitle?: string;
  nextDisabled?: boolean;
  onNext?: () => void;

  showBack?: boolean;
  showLogout?: boolean;
}

export function OnboardingLayout({
  title,
  subtitle,
  step,
  totalSteps,
  children,
  nextButtonTitle = "Next",
  nextDisabled = false,
  onNext,
  showBack = true,
  showLogout = true,
}: OnboardingLayoutProps): React.JSX.Element {

  const navigation = useNavigation();

  const progress =
    (step / totalSteps) * 100;

  const handleLogout = () => {

    Alert.alert(
      "Logout",
      "Are you sure you want to logout?",
      [
        {
          text: "Cancel",
          style: "cancel",
        },
        {
          text: "Logout",
          style: "destructive",
          onPress: async () => {

            await authService.logout();

            navigation.dispatch(
              CommonActions.reset({
                index: 0,
                routes: [
                  {
                    name: "Login" as never,
                  },
                ],
              }),
            );
          },
        },
      ],
    );
  };

  return (
    <SafeAreaView style={styles.container}>

      <ScrollView
        keyboardShouldPersistTaps="handled"
        contentContainerStyle={styles.content}
      >

        <View style={styles.header}>

          {showBack ? (
            <TouchableOpacity
              onPress={() => navigation.goBack()}
            >
              <Text style={styles.headerButton}>
                ← Back
              </Text>
            </TouchableOpacity>
          ) : (
            <View />
          )}

          {showLogout ? (
            <TouchableOpacity
              onPress={handleLogout}
            >
              <Text style={styles.headerButton}>
                Logout
              </Text>
            </TouchableOpacity>
          ) : (
            <View />
          )}

        </View>

        <View style={styles.progressBackground}>

          <View
            style={[
              styles.progressFill,
              {
                width: `${progress}%`,
              },
            ]}
          />

        </View>

        <Text style={styles.step}>
          Step {step} of {totalSteps}
        </Text>

        <Text style={styles.title}>
          {title}
        </Text>

        <Text style={styles.subtitle}>
          {subtitle}
        </Text>

        <View style={styles.body}>
          {children}
        </View>

        {onNext && (
          <PrimaryButton
            title={nextButtonTitle}
            disabled={nextDisabled}
            onPress={onNext}
          />
        )}

      </ScrollView>

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: colors.background,
  },

  content: {
    flexGrow: 1,
    padding: spacing.lg,
  },

  header: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: spacing.lg,
  },

  headerButton: {
    color: colors.primary,
    ...typography.body,
    fontWeight: "600",
  },

  progressBackground: {
    height: 8,
    backgroundColor: colors.border,
    borderRadius: 4,
    overflow: "hidden",
    marginBottom: spacing.md,
  },

  progressFill: {
    height: 8,
    backgroundColor: colors.primary,
  },

  step: {
    ...typography.caption,
    color: colors.primary,
    textAlign: "center",
    marginBottom: spacing.sm,
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
    marginBottom: spacing.xl,
  },

  body: {
    flex: 1,
    marginBottom: spacing.xl,
  },

});