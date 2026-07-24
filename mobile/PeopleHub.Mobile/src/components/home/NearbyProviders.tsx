import React from "react";
import {
  StyleSheet,
  Text,
  View,
} from "react-native";

export function NearbyProviders(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Nearby Professionals</Text>

      <View style={styles.card}>
        <Text style={styles.name}>★★★★★ John Electrician</Text>
        <Text>2.1 km away</Text>
      </View>

      <View style={styles.card}>
        <Text style={styles.name}>★★★★★ Ahmed Plumbing</Text>
        <Text>3.4 km away</Text>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: 28,
  },

  title: {
    fontSize: 20,
    fontWeight: "700",
    marginBottom: 16,
  },

  card: {
    backgroundColor: "#F8F9FB",
    borderRadius: 14,
    padding: 18,
    marginBottom: 12,
  },

  name: {
    fontWeight: "700",
    marginBottom: 4,
  },
});