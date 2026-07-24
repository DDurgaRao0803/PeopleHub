import React, {
  useEffect,
  useState,
} from "react";
import {
  ActivityIndicator,
  FlatList,
  Pressable,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

import { serviceCategoryService } from "../../services";
import type { ServiceCategory } from "../../types";

const iconMap: Record<string, keyof typeof Ionicons.glyphMap> = {
  Electrician: "flash",
  Plumber: "water",
  Cleaning: "sparkles",
  Painter: "color-palette",
  Driver: "car",
  "IT Support": "laptop",
};

export function CategorySection(): React.JSX.Element {
  const [categories, setCategories] = useState<ServiceCategory[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  const loadCategories = async () => {
  try {
    const response =
      await serviceCategoryService.getCategories();

    setCategories(response);
  } catch {
    setError(true);
  } finally {
    setLoading(false);
  }
};

useEffect(() => {
  void loadCategories();
}, []);

  if (loading) {
    return (
      <View style={styles.loadingContainer}>
        <ActivityIndicator size="large" />
      </View>
    );
  }

  if (error) {
    return (
      <View style={styles.loadingContainer}>
        <Text>Unable to load categories.</Text>
      </View>
    );
  }

  if (categories.length === 0) {
    return (
      <View style={styles.loadingContainer}>
        <Text>No categories available.</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Text style={styles.title}>
        Popular Categories
      </Text>

      <FlatList
        data={categories}
        keyExtractor={(item) => item.id}
        numColumns={2}
        scrollEnabled={false}
        columnWrapperStyle={styles.row}
        renderItem={({ item }) => (
          <Pressable style={styles.card}>
            <Ionicons
              name={iconMap[item.name] ?? "construct"}
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
  loadingContainer: {
    paddingVertical: 40,
    alignItems: "center",
  },

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