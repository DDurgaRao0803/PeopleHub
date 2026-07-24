import React from "react";
import {
  Pressable,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

const actions = [
  { title: "My Requests", icon: "clipboard" },
  { title: "Wallet", icon: "wallet" },
  { title: "Favorites", icon: "heart" },
  { title: "Become Provider", icon: "briefcase" },
] as const;

export function QuickActions(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Quick Actions</Text>

      <View style={styles.grid}>
        {actions.map((action) => (
          <Pressable
            key={action.title}
            style={styles.card}
          >
            <Ionicons
              name={action.icon}
              size={24}
              color="#2563EB"
            />

            <Text style={styles.text}>
              {action.title}
            </Text>
          </Pressable>
        ))}
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

  grid: {
    flexDirection: "row",
    flexWrap: "wrap",
    justifyContent: "space-between",
  },

  card: {
    width: "48%",
    backgroundColor: "#F8F9FB",
    borderRadius: 14,
    paddingVertical: 18,
    marginBottom: 12,
    alignItems: "center",
  },

  text: {
    marginTop: 8,
    textAlign: "center",
    fontWeight: "600",
  },
});