import React from "react";
import {
  StyleSheet,
  TextInput,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

export function SearchBar(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Ionicons
        name="search"
        size={20}
        color="#777777"
      />

      <TextInput
        placeholder="Search services..."
        placeholderTextColor="#999999"
        style={styles.input}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    height: 54,
    borderRadius: 14,
    backgroundColor: "#F5F5F5",

    flexDirection: "row",
    alignItems: "center",

    paddingHorizontal: 16,
    marginBottom: 24,
  },

  input: {
    flex: 1,
    marginLeft: 12,
    color: "#111111",
    fontSize: 16,
  },
});