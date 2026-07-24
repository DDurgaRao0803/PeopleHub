import React from "react";
import {
  StyleSheet,
  Text,
 View,
} from "react-native";

export function RecentActivity(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Recent Activity</Text>

      <View style={styles.card}>
        <Text>No recent activity yet.</Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: 20,
  },

  title: {
    fontSize: 20,
    fontWeight: "700",
    marginBottom: 16,
  },

  card: {
    backgroundColor: "#F8F9FB",
    borderRadius: 14,
    padding: 20,
  },
});