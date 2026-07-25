import React from "react";

import {
  Pressable,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

import type { NativeStackScreenProps } from "@react-navigation/native-stack";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

type Props = NativeStackScreenProps<any>;

export default function SplashScreen({
  navigation,
}: Props): React.JSX.Element {
  return (
    <Pressable
      style={styles.container}
      onPress={() => navigation.replace("Welcome")}
    >
      <View style={styles.content}>
        <Ionicons
          name="people-circle"
          size={120}
          color={colors.primary}
        />

        <Text style={styles.title}>
          PeopleHub
        </Text>

        <Text style={styles.subtitle}>
          Trusted Services.
        </Text>

        <Text style={styles.subtitle}>
          Trusted Professionals.
        </Text>

        <View style={{ height: spacing.xxxl }} />
      </View>
    </Pressable>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
    justifyContent: "center",
    alignItems: "center",
  },

  content: {
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
    marginTop: spacing.sm,
  },
});