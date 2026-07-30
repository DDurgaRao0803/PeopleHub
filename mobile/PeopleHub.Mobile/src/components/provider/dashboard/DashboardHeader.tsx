import React from "react";
import { StyleSheet, Text, View } from "react-native";

type DashboardHeaderProps = {
  firstName: string;
};

export default function DashboardHeader({
  firstName,
}: DashboardHeaderProps): React.JSX.Element {
  const hour = new Date().getHours();

  let greeting = "Good Evening";

  if (hour < 12) {
    greeting = "Good Morning";
  } else if (hour < 18) {
    greeting = "Good Afternoon";
  }

  return (
    <View style={styles.container}>
      <Text style={styles.greeting}>
        {greeting}, {firstName} 👋
      </Text>

      <Text style={styles.subtitle}>
        Ready to accept work today?
      </Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: 24,
  },

  greeting: {
    fontSize: 28,
    fontWeight: "700",
    color: "#1F2937",
  },

  subtitle: {
    marginTop: 6,
    fontSize: 16,
    color: "#6B7280",
  },
});