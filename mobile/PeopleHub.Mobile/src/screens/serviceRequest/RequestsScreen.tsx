import React, { useCallback, useEffect, useState } from "react";
import {
  ActivityIndicator,
  FlatList,
  RefreshControl,
  SafeAreaView,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { TouchableOpacity } from "react-native";

import {
  useNavigation,
} from "@react-navigation/native";

import type {
  NativeStackNavigationProp,
} from "@react-navigation/native-stack";

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

import { serviceRequestService } from "../../services";
import { ServiceRequest } from "../../types";
import { useFocusEffect } from "@react-navigation/native";
import { useAuth } from "../../context/AuthContext";

export function RequestsScreen(): React.JSX.Element {

  const navigation =
  useNavigation<
    NativeStackNavigationProp<MainStackParamList>
  >();
  const [requests, setRequests] = useState<ServiceRequest[]>([]);
  const [loading, setLoading] = useState(true);
  const [refreshing, setRefreshing] = useState(false);
  const { user } = useAuth();

const loadRequests = useCallback(async () => {
  try {
    let data: ServiceRequest[];

    if (user?.isProvider === true) {
      data = await serviceRequestService.getMyProviderRequests();
    } else {
      data = await serviceRequestService.getMyCustomerRequests();
    }

    setRequests(data);
  } catch (error) {
    console.error("Failed to load requests:", error);
    setRequests([]);
  } finally {
    setLoading(false);
    setRefreshing(false);
  }
}, [user]);

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
        <Text style={styles.emptyTitle}>No Requests</Text>
        <Text style={styles.emptyText}>
          You do not have any service requests yet.
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
    navigation.navigate("RequestDetails", {
      requestId: item.id,
    })
  }
>
  <Text style={styles.title}>{item.title}</Text>

  <Text style={styles.status}>
    {item.status}
  </Text>

  <Text style={styles.description}>
    {item.description}
  </Text>

  <Text style={styles.date}>
    {new Date(item.requestedDate).toLocaleDateString()}
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