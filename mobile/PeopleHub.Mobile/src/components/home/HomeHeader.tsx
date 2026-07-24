/**
 * ============================================================
 * PeopleHub Mobile
 * Home Header
 * ============================================================
 */

import React from "react";
import {
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

import { useAuth } from "../../context/AuthContext";

export function HomeHeader(): React.JSX.Element {
  const { user } = useAuth();

  const greeting = (): string => {
    const hour = new Date().getHours();

    if (hour < 12) {
      return "Good Morning";
    }

    if (hour < 17) {
      return "Good Afternoon";
    }

    return "Good Evening";
  };

  return (
    <View style={styles.container}>
      <View>
        <Text style={styles.greeting}>
          {greeting()}
        </Text>

        <Text style={styles.name}>
          {user
            ? `${user.firstName} ${user.lastName}`
            : "Welcome"}
        </Text>
      </View>

      <TouchableOpacity style={styles.avatar}>
        <Ionicons
  name="person"
  size={24}
  color="#FFFFFF"
/>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginTop: 10,
    marginBottom: 20,
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
  },

  greeting: {
  fontSize: 16,
  color: "#6B7280",
},

  name: {
  marginTop: 4,
  fontSize: 24,
  fontWeight: "700",
  color: "#111827",
},

  avatar: {
  width: 48,
  height: 48,
  borderRadius: 24,
  backgroundColor: "#2563EB",
  justifyContent: "center",
  alignItems: "center",
},
});