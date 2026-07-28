
import React, {
  useCallback,
  useEffect,
  useState,
} from "react";

import {
  ActivityIndicator,
  FlatList,
  RefreshControl,
  SafeAreaView,
  StyleSheet,
  Text,
  TouchableOpacity,
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

import {
  serviceRequestService,
} from "../../services";

import type {
  ServiceRequest,
} from "../../types";



export function ProviderRequestsScreen(): React.JSX.Element {

  console.log("******** ProviderRequestsScreen Loaded ********");
  
  const navigation =
    useNavigation<
      NativeStackNavigationProp<MainStackParamList>
    >();

  const [requests, setRequests] =
    useState<ServiceRequest[]>([]);

  const [loading, setLoading] =
    useState(true);

  const [refreshing, setRefreshing] =
    useState(false);

  const loadRequests = useCallback(async () => {
    try {
      const data =
  await serviceRequestService.getMyProviderRequests();

console.log("Provider Requests:", data);
console.log("Count:", data.length);

setRequests(data);
    } catch (error) {
  console.log("Provider Request Error:", error);
  setRequests([]);
} finally {
      setLoading(false);
      setRefreshing(false);
    }
  }, []);

  useEffect(() => {
    void loadRequests();
  }, [loadRequests]);

  useFocusEffect(
    useCallback(() => {
      void loadRequests();
    }, [loadRequests])
  );

  const onRefresh = async () => {
    setRefreshing(true);
    await loadRequests();
  };

  if (loading) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator size="large" />
      </SafeAreaView>
    );
  }

  if (requests.length === 0) {
    return (
      <SafeAreaView style={styles.center}>
        <Text style={styles.emptyTitle}>
          No Provider Requests
        </Text>

        <Text style={styles.emptyText}>
          There are currently no requests assigned to you.
        </Text>
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container}>
      <FlatList
        data={requests}
        keyExtractor={(item) => item.id}
        refreshControl={
          <RefreshControl
            refreshing={refreshing}
            onRefresh={() => {
              void onRefresh();
            }}
          />
        }
        renderItem={({ item }) => (
          <TouchableOpacity
            style={styles.card}
            activeOpacity={0.8}
            onPress={() =>
              navigation.navigate(
                "RequestDetails",
                {
                  requestId: item.id,
                }
              )
            }
          >
            <Text style={styles.title}>
              {item.title}
            </Text>

            <Text style={styles.status}>
              {item.status}
            </Text>

            <Text style={styles.description}>
              {item.description}
            </Text>

            <Text style={styles.date}>
              {new Date(
                item.requestedDate
              ).toLocaleDateString()}
            </Text>
          </TouchableOpacity>
        )}
      />
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
    padding: 24,
  },

  card: {
    backgroundColor: "#fff",
    borderRadius: 12,
    padding: 16,
    marginBottom: 12,
    elevation: 2,
  },

  title: {
    fontSize: 18,
    fontWeight: "700",
  },

  status: {
    marginTop: 8,
    fontWeight: "600",
    color: "#2563EB",
  },

  description: {
    marginTop: 8,
    color: "#666",
  },

  date: {
    marginTop: 12,
    color: "#999",
    fontSize: 12,
  },

  emptyTitle: {
    fontSize: 20,
    fontWeight: "700",
  },

  emptyText: {
    marginTop: 8,
    color: "#666",
    textAlign: "center",
  },
});