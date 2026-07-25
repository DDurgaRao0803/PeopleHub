import React from "react";

import {
  SafeAreaView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import { NativeStackScreenProps } from "@react-navigation/native-stack";

import { AuthStackParamList } from "../../navigation/AuthStackParamList";

import { colors } from "../../theme/colors";
import { radius } from "../../theme/radius";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

type Props = NativeStackScreenProps<
  AuthStackParamList,
  "AccountType"
>;

export function AccountTypeScreen({
  navigation,
}: Props): React.JSX.Element {

  return (
    <SafeAreaView style={styles.container}>

      <Text style={styles.title}>
        Create Account
      </Text>

      <Text style={styles.subtitle}>
        Choose how you want to register
      </Text>

      <TouchableOpacity
        style={styles.card}
        onPress={() =>
          navigation.navigate("CustomerRegister")
        }
      >
        <Text style={styles.cardTitle}>
          Customer
        </Text>

        <Text style={styles.cardDescription}>
          Book services and deliveries.
        </Text>
      </TouchableOpacity>

      <TouchableOpacity
        style={styles.card}
        onPress={() => {
          // We'll build this next.
        }}
      >
        <Text style={styles.cardTitle}>
          Service Provider
        </Text>

        <Text style={styles.cardDescription}>
          Offer services and earn money.
        </Text>
      </TouchableOpacity>

    </SafeAreaView>
  );

}

const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: colors.background,
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
    marginTop: spacing.sm,
    marginBottom: spacing.xl,
  },

  card: {
    backgroundColor: colors.surface,
    borderWidth: 1,
    borderColor: colors.border,
    borderRadius: radius.lg,
    padding: spacing.lg,
    marginBottom: spacing.lg,
  },

  cardTitle: {
    ...typography.h3,
    color: colors.text.primary,
  },

  cardDescription: {
    ...typography.body,
    color: colors.text.secondary,
    marginTop: spacing.sm,
  },

});