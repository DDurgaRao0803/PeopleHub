import React, {
  useEffect,
  useState,
} from "react";
import {
  ActivityIndicator,
  FlatList,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { providerService } from "../../services";
import type { NearbyProvider } from "../../types/provider";

export function NearbyProviders(): React.JSX.Element {
  const [providers, setProviders] = useState<NearbyProvider[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);

  const loadProviders = async () => {
  try {
    const response =
      await providerService.getNearby();

    setProviders(response);
  } catch {
    setError(true);
  } finally {
    setLoading(false);
  }
};

useEffect(() => {
  void loadProviders();
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
        <Text>Unable to load nearby professionals.</Text>
      </View>
    );
  }

  if (providers.length === 0) {
    return (
      <View style={styles.loadingContainer}>
        <Text>No nearby professionals found.</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Text style={styles.title}>
        Nearby Professionals
      </Text>

      <FlatList
        data={providers}
        keyExtractor={(item) => item.providerProfileId}
        scrollEnabled={false}
        renderItem={({ item }) => (
          <View style={styles.card}>
            <Text style={styles.name}>
              {item.fullName}
            </Text>

            <Text style={styles.category}>
              {item.serviceCategory}
            </Text>

            <Text style={styles.rating}>
              ⭐ {item.rating.toFixed(1)}
            </Text>

            <Text
              style={[
                styles.status,
                item.isAvailable
                  ? styles.available
                  : styles.unavailable,
              ]}
            >
              {item.isAvailable
                ? "Available"
                : "Unavailable"}
            </Text>
          </View>
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

  card: {
    backgroundColor: "#F8F9FB",
    borderRadius: 14,
    padding: 18,
    marginBottom: 12,
  },

  name: {
    fontSize: 16,
    fontWeight: "700",
    marginBottom: 4,
  },

  category: {
    fontSize: 14,
    color: "#666",
    marginBottom: 6,
  },

  rating: {
    fontSize: 14,
    marginBottom: 6,
  },

  status: {
    fontSize: 13,
    fontWeight: "600",
  },

  available: {
    color: "#16A34A",
  },

  unavailable: {
    color: "#DC2626",
  },
});