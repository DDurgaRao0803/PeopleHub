import React from "react";
import { StyleSheet, Text, View } from "react-native";

export default function RecentActivity(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.heading}>Recent Activity</Text>

      <View style={styles.card}>
        <Text style={styles.emptyTitle}>
          No recent activity
        </Text>

        <Text style={styles.emptySubtitle}>
          Your completed jobs and recent updates will appear here.
        </Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: 24,
  },

  heading: {
    fontSize: 20,
    fontWeight: "700",
    color: "#1F2937",
    marginBottom: 16,
  },

  card: {
    backgroundColor: "#FFFFFF",
    borderRadius: 16,
    padding: 20,

    shadowColor: "#000",
    shadowOpacity: 0.08,
    shadowRadius: 8,
    shadowOffset: {
      width: 0,
      height: 2,
    },

    elevation: 3,
  },

  emptyTitle: {
    fontSize: 18,
    fontWeight: "600",
    color: "#1F2937",
    marginBottom: 8,
  },

  emptySubtitle: {
    fontSize: 14,
    color: "#6B7280",
    lineHeight: 20,
  },
});