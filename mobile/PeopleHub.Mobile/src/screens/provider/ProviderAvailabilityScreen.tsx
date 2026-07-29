import React, { useCallback, useState } from "react";

import {
  useFocusEffect,
  useNavigation,
} from "@react-navigation/native";

import {
  ActivityIndicator,
  Alert,
  FlatList,
  RefreshControl,
  SafeAreaView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import {
  ProviderAvailability,
} from "../../types";

import {
  providerAvailabilityService,
} from "../../services";

export function ProviderAvailabilityScreen() {
  const providerProfileId = "";

  const [availability, setAvailability] = useState<
    ProviderAvailability[]
  >([]);

  const [loading, setLoading] = useState(true);

  const navigation = useNavigation<any>();

  const [refreshing, setRefreshing] =
    useState(false);

  const loadAvailability = async () => {
    try {
      const response =
        await providerAvailabilityService.getAvailability(
          providerProfileId
        );

      setAvailability(response);
    } catch {
      Alert.alert(
        "Error",
        "Unable to load provider availability."
      );
    } finally {
      setLoading(false);
      setRefreshing(false);
    }
  };

  useFocusEffect(
    useCallback(() => {
      loadAvailability();
    }, [])
  );

  const refresh = () => {
    setRefreshing(true);
    loadAvailability();
  };

  const deleteAvailability = (
  availabilityId: string
) => {
  Alert.alert(
    "Delete Availability",
    "Are you sure you want to delete this availability?",
    [
      {
        text: "Cancel",
        style: "cancel",
      },
      {
        text: "Delete",
        style: "destructive",
        onPress: async () => {
          try {
            await providerAvailabilityService.deleteAvailability(
              providerProfileId,
              availabilityId
            );

            await loadAvailability();

            Alert.alert(
              "Success",
              "Availability deleted successfully."
            );
          } catch {
            Alert.alert(
              "Error",
              "Unable to delete availability."
            );
          }
        },
      },
    ]
  );
};

  if (loading) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator size="large" />
      </SafeAreaView>
    );
  }

  return (
  <SafeAreaView style={styles.container}>
    <FlatList
      data={availability}
      keyExtractor={(item) => item.id}
      refreshControl={
        <RefreshControl
          refreshing={refreshing}
          onRefresh={refresh}
        />
      }
      ListEmptyComponent={
        <Text style={styles.emptyText}>
          No availability configured.
        </Text>
      }
      renderItem={({ item }) => (
  <View style={styles.card}>
    <Text style={styles.day}>
      {item.dayOfWeek}
    </Text>

    <Text style={styles.time}>
      {item.startTime} - {item.endTime}
    </Text>

    <View style={styles.buttonRow}>
      <TouchableOpacity
        style={styles.editButton}
        onPress={() =>
          navigation.navigate("EditProviderAvailability", {
            availabilityId: item.id,
            availability: item,
          })
        }
      >
        <Text style={styles.buttonText}>Edit</Text>
      </TouchableOpacity>

      <TouchableOpacity
        style={styles.deleteButton}
        onPress={() => deleteAvailability(item.id)}
      >
        <Text style={styles.buttonText}>Delete</Text>
      </TouchableOpacity>
    </View>
  </View>
)}
    />

    <TouchableOpacity
      style={styles.fab}
      onPress={() =>
        navigation.navigate(
          "AddProviderAvailability"
        )
      }
    >
      <Text style={styles.fabText}>+</Text>
    </TouchableOpacity>
  </SafeAreaView>
);
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 16,
  },

  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },

  card: {
    backgroundColor: "#fff",
    padding: 16,
    marginBottom: 12,
    borderRadius: 10,
    elevation: 2,
  },

  day: {
    fontSize: 18,
    fontWeight: "600",
  },

  time: {
    marginTop: 6,
    color: "#666",
  },

  emptyText: {
    textAlign: "center",
    marginTop: 40,
    color: "#777",
  },

  buttonRow: {
  flexDirection: "row",
  justifyContent: "space-between",
  marginTop: 16,
},

editButton: {
  backgroundColor: "#1976D2",
  paddingHorizontal: 18,
  paddingVertical: 10,
  borderRadius: 8,
},

deleteButton: {
  backgroundColor: "#D32F2F",
  paddingHorizontal: 18,
  paddingVertical: 10,
  borderRadius: 8,
},

buttonText: {
  color: "#fff",
  fontWeight: "600",
},

fab: {
  position: "absolute",
  right: 24,
  bottom: 24,
  width: 56,
  height: 56,
  borderRadius: 28,
  backgroundColor: "#1976D2",
  justifyContent: "center",
  alignItems: "center",
  elevation: 5,
},

fabText: {
  color: "#fff",
  fontSize: 28,
  fontWeight: "bold",
},
});