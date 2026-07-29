import React, {
  useCallback,
  useEffect,
  useState,
} from "react";

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
  useFocusEffect,
  useNavigation,
} from "@react-navigation/native";

import type {
  NativeStackNavigationProp,
} from "@react-navigation/native-stack";

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

import { providerService } from "../../services/providerService";

import type {
  ProviderService,
} from "../../types/provider";

type NavigationProp =
  NativeStackNavigationProp<MainStackParamList>;

export function ProviderServicesScreen(): React.JSX.Element {

  const navigation =
    useNavigation<NavigationProp>();

  const [providerProfileId, setProviderProfileId] =
  useState("");

useEffect(() => {
  const loadProfile = async () => {
    try {
      const profile =
        await providerService.getProfile();

      setProviderProfileId(profile.id);
    } catch {
      Alert.alert(
        "Error",
        "Unable to load provider profile."
      );
    }
  };

  void loadProfile();
}, []);

  const [services, setServices] =
    useState<ProviderService[]>([]);

  const [loading, setLoading] =
    useState(true);

  const [refreshing, setRefreshing] =
    useState(false);
    

  const loadServices = async () => {

    if (!providerProfileId) {
    return;
  }

    try {

      const data =
        await providerService.getProviderServices(
          providerProfileId
        );

      setServices(data);

    } finally {

      setLoading(false);
      setRefreshing(false);

    }
  };

  useEffect(() => {
  if (!providerProfileId) {
    return;
  }

  void loadServices();
}, [providerProfileId]);

  useFocusEffect(
  useCallback(() => {
    if (!providerProfileId) {
      return;
    }

    void loadServices();
  }, [providerProfileId])
);

  const refresh = () => {
    setRefreshing(true);
    void loadServices();
  };

  const deleteService = async (serviceId: string) => {
  console.log("deleteService called", serviceId);

  try {
    await providerService.deleteProviderService(serviceId);

    console.log("Delete API succeeded");

    await loadServices();

    Alert.alert(
      "Success",
      "Provider service deleted successfully."
    );
  } catch (error) {
    console.log("Delete failed", error);

    Alert.alert(
      "Error",
      "Unable to delete provider service."
    );
  }
};

  const renderItem = ({
    item,
  }: {
    item: ProviderService;
  }) => (
    <View style={styles.card}>

      <Text style={styles.title}>
        {item.title}
      </Text>

      <Text style={styles.description}>
        {item.description}
      </Text>

      <Text style={styles.price}>
        ${item.basePrice.toFixed(2)}
      </Text>

      <Text style={styles.duration}>
        {item.estimatedDurationMinutes} Minutes
      </Text>

      <Text
        style={[
          styles.status,
          item.isActive
            ? styles.active
            : styles.inactive,
        ]}
      >
        {item.isActive
          ? "Active"
          : "Inactive"}
      </Text>

      <View style={styles.actions}>

        <TouchableOpacity
          style={styles.editButton}
          onPress={() =>
            navigation.navigate(
              "EditProviderService",
              {
                serviceId: item.id,
              }
            )
          }
        >
          <Text style={styles.buttonText}>
            Edit
          </Text>
        </TouchableOpacity>

        <TouchableOpacity
  style={styles.deleteButton}
  onPress={() => {
    console.log("Delete pressed", item.id);
    deleteService(item.id);
  }}
>
  <Text style={styles.buttonText}>Delete</Text>
</TouchableOpacity>

      </View>

    </View>
  );

  if (loading) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator
          size="large"
        />
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container}>

      <FlatList
        data={services}
        keyExtractor={(item) => item.id}
        renderItem={renderItem}
        refreshControl={
          <RefreshControl
            refreshing={refreshing}
            onRefresh={refresh}
          />
        }
        ListEmptyComponent={
          <View style={styles.emptyContainer}>
            <Text style={styles.emptyText}>
              No provider services found.
            </Text>

            <Text style={styles.emptySubText}>
              Tap + to create your first service.
            </Text>
          </View>
        }
      />

      <TouchableOpacity
        style={styles.fab}
        onPress={() => {
  console.log("Create Service pressed");

  Alert.alert("Debug", "Button pressed");

  navigation.navigate("AddProviderService");
}}
      >
        <Text style={styles.fabText}>
          +
        </Text>
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
    borderRadius: 10,
    padding: 16,
    marginBottom: 16,
    elevation: 2,
  },

  title: {
    fontSize: 18,
    fontWeight: "700",
  },

  description: {
    marginTop: 6,
    color: "#555",
  },

  price: {
    marginTop: 10,
    fontWeight: "600",
  },

  duration: {
    marginTop: 4,
  },

  status: {
    marginTop: 8,
    fontWeight: "600",
  },

  active: {
    color: "green",
  },

  inactive: {
    color: "red",
  },

  actions: {
    flexDirection: "row",
    marginTop: 16,
  },

  editButton: {
    backgroundColor: "#1976D2",
    paddingHorizontal: 18,
    paddingVertical: 10,
    borderRadius: 8,
    marginRight: 12,
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

  emptyContainer: {
    marginTop: 80,
    alignItems: "center",
  },

  emptyText: {
    fontSize: 18,
    fontWeight: "600",
  },

  emptySubText: {
    marginTop: 10,
    color: "#666",
  },

  fab: {
    position: "absolute",
    right: 20,
    bottom: 20,
    width: 60,
    height: 60,
    borderRadius: 30,
    backgroundColor: "#1976D2",
    justifyContent: "center",
    alignItems: "center",
    elevation: 4,
  },

  fabText: {
    color: "#fff",
    fontSize: 30,
    marginTop: -2,
  },

});