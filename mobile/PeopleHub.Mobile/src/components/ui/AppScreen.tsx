import React from "react";
import {
  SafeAreaView,
  ScrollView,
  StyleSheet,
  ViewStyle,
} from "react-native";

import {
  colors,
  spacing,
} from "../../theme";

interface Props {
  children: React.ReactNode;
  scrollable?: boolean;
  style?: ViewStyle;
}

export function AppScreen({
  children,
  scrollable = true,
  style,
}: Props): React.JSX.Element {
  if (scrollable) {
    return (
      <SafeAreaView style={styles.safe}>
        <ScrollView
          contentContainerStyle={[
            styles.content,
            style,
          ]}
          showsVerticalScrollIndicator={false}
        >
          {children}
        </ScrollView>
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView
      style={[
        styles.safe,
        styles.content,
        style,
      ]}
    >
      {children}
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safe: {
    flex: 1,
    backgroundColor: colors.background,
  },

  content: {
    flexGrow: 1,
    padding: spacing.xl,
  },
});