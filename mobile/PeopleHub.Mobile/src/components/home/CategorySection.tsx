import React from "react";
import {
  FlatList,
  Pressable,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

type Category = {
  id: string;
  name: string;
  icon: keyof typeof Ionicons.glyphMap;
};

const categories: Category[] = [
  { id: "1", name: "Electrician", icon: "flash" },
  { id: "2", name: "Plumber", icon: "water" },
  { id: "3", name: "Cleaning", icon: "sparkles" },
  { id: "4", name: "Painter", icon: "color-palette" },
  { id: "5", name: "Driver", icon: "car" },
  { id: "6", name: "IT Support", icon: "laptop" },
];

export function CategorySection(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Popular Categories</Text>

      <FlatList
        data={categories}
        keyExtractor={(item) => item.id}
        numColumns={2}
        scrollEnabled={false}
        columnWrapperStyle={styles.row}
        renderItem={({ item }) => (
          <Pressable style={styles.card}>
            <Ionicons
              name={item.icon}
              size={28}
              color="#2563EB"
            />

            <Text style={styles.label}>
              {item.name}
            </Text>
          </Pressable>
        )}
      />
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

  row: {
    justifyContent: "space-between",
    marginBottom: 12,
  },

  card: {
    width: "48%",
    backgroundColor: "#F8F9FB",
    borderRadius: 14,
    paddingVertical: 20,
    alignItems: "center",
  },

  label: {
    marginTop: 10,
    fontSize: 15,
    fontWeight: "600",
  },
});