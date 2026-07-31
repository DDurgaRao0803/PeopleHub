import React from "react";
import { StyleSheet, Text, View } from "react-native";

type Props = {
  firstName: string;
};

export default function ProviderGreeting({
  firstName,
}: Props): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.greeting}>
        Good Morning,
      </Text>

      <Text style={styles.name}>
        {firstName} 👋
      </Text>

      <View style={styles.badge}>
        <Text style={styles.badgeText}>
          ⭐ Gold Provider
        </Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginTop: 12,
    marginBottom: 24,
  },

  greeting: {
    fontSize: 18,
    color: "#6B7280",
    marginBottom: 8,
  },

  name: {
    fontSize: 40,
    fontWeight: "700",
    color: "#111827",
  },

  badge: {
    alignSelf: "flex-start",
    marginTop: 14,
    paddingHorizontal: 16,
    paddingVertical: 8,
    borderRadius: 999,
    backgroundColor: "#FFF7E6",
  },

  badgeText: {
    fontSize: 16,
    fontWeight: "600",
    color: "#D97706",
  },
});