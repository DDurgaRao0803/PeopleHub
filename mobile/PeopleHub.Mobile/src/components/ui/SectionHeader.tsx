import React from "react";

import {
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

interface SectionHeaderProps {
  title: string;
  actionText?: string;
  onPressAction?: () => void;
}

export function SectionHeader({
  title,
  actionText,
  onPressAction,
}: SectionHeaderProps): React.JSX.Element {

  return (
    <View style={styles.container}>

      <Text style={styles.title}>
        {title}
      </Text>

      {actionText ? (
        <TouchableOpacity
          onPress={onPressAction}
          activeOpacity={0.7}
        >
          <Text style={styles.action}>
            {actionText}
          </Text>
        </TouchableOpacity>
      ) : null}

    </View>
  );
}

const styles = StyleSheet.create({

  container: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: spacing.md,
  },

  title: {
    ...typography.h3,
    color: colors.text.primary,
  },

  action: {
    ...typography.body,
    color: colors.primary,
    fontWeight: "600",
  },

});