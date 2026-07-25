import React from "react";
import {
  SafeAreaView,
  StyleSheet,
  Text,
  View,
} from "react-native";
import { Ionicons } from "@expo/vector-icons";

import { NativeStackScreenProps } from "@react-navigation/native-stack";

import { PrimaryButton } from "../../components/buttons";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

type Props = NativeStackScreenProps<any>;

export function WelcomeScreen({
  navigation,
}: Props): React.JSX.Element {
  return (
    <SafeAreaView style={styles.container}>
      <View style={styles.content}>
        <Ionicons
          name="people-circle"
          size={90}
          color={colors.primary}
        />

        <Text style={styles.title}>
          PeopleHub
        </Text>

        <Text style={styles.subtitle}>
          Trusted Services.
          {"\n"}
          Trusted Professionals.
        </Text>

        <Text style={styles.description}>
          Connect with skilled professionals
          or grow your business with PeopleHub.
        </Text>

        <View style={styles.buttons}>
          <PrimaryButton
            title="Login"
            onPress={() => navigation.navigate("Login")}
          />

          <View style={{ height: spacing.md }} />

          <PrimaryButton
            title="Register as Customer"
            onPress={() =>
              navigation.navigate("RegisterCustomer")
            }
          />

          <View style={{ height: spacing.md }} />

          <PrimaryButton
            title="Register as Provider"
            onPress={() =>
              navigation.navigate("RegisterProvider")
            }
          />
        </View>
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
  },

  content: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: spacing.xl,
  },

  title: {
    ...typography.h1,
    color: colors.text.primary,
    marginTop: spacing.lg,
  },

  subtitle: {
    ...typography.subtitle,
    color: colors.text.secondary,
    textAlign: "center",
    marginTop: spacing.md,
  },

  description: {
    ...typography.body,
    color: colors.text.secondary,
    textAlign: "center",
    marginTop: spacing.lg,
    marginBottom: spacing.xxxl,
  },

  buttons: {
    width: "100%",
  },
});