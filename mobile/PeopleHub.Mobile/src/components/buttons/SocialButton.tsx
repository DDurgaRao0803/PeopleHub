import React from "react";

import {
  ActivityIndicator,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

import { colors } from "../../theme/colors";
import { radius } from "../../theme/radius";
import { shadows } from "../../theme/shadows";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

export type SocialProvider =
  | "google"
  | "facebook"
  | "apple"
  | "microsoft"
  | "linkedin";

interface SocialButtonProps {
  title: string;
  provider: SocialProvider;
  onPress: () => void;
  loading?: boolean;
  disabled?: boolean;
}

function getIcon(provider: SocialProvider): keyof typeof Ionicons.glyphMap {
  switch (provider) {
    case "google":
      return "logo-google";

    case "facebook":
      return "logo-facebook";

    case "apple":
      return "logo-apple";

    case "microsoft":
      return "logo-windows";

    case "linkedin":
      return "logo-linkedin";

    default:
      return "globe-outline";
  }
}

export function SocialButton({
  title,
  provider,
  onPress,
  loading = false,
  disabled = false,
}: SocialButtonProps): React.JSX.Element {
  const isDisabled = disabled || loading;

  return (
    <TouchableOpacity
      activeOpacity={0.8}
      onPress={onPress}
      disabled={isDisabled}
      style={[
        styles.button,
        isDisabled && styles.disabled,
      ]}
    >
      <View style={styles.content}>
        {loading ? (
          <ActivityIndicator color={colors.primary} />
        ) : (
          <Ionicons
            name={getIcon(provider)}
            size={22}
            color={colors.primary}
          />
        )}

        <Text style={styles.title}>
          {title}
        </Text>
      </View>
    </TouchableOpacity>
  );
}

const styles = StyleSheet.create({
  button: {
    width: "100%",
    backgroundColor: colors.surface,
    borderWidth: 1,
    borderColor: colors.border,
    borderRadius: radius.md,
    paddingVertical: spacing.md,
    paddingHorizontal: spacing.lg,
    marginTop: spacing.md,
    ...shadows.sm,
  },

  disabled: {
    opacity: 0.6,
  },

  content: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
  },

  title: {
    ...typography.body,
    color: colors.text.primary,
    marginLeft: spacing.md,
  },
});