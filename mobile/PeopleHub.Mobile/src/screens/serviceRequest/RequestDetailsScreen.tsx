import React, {
  useEffect,
  useState,
} from "react";

import {
  ActivityIndicator,
  Alert,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
  View,
} from "react-native";

import {
  useRoute,
} from "@react-navigation/native";

import type {
  RouteProp,
} from "@react-navigation/native";

import {
  serviceRequestService,
} from "../../services";

import type {
  ServiceRequest,
} from "../../types";

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

export function RequestDetailsScreen(): React.JSX.Element {
  const route =
    useRoute<
      RouteProp<
        MainStackParamList,
        "RequestDetails"
      >
    >();

  const { requestId } = route.params;

  const [request, setRequest] =
    useState<ServiceRequest | null>(null);

  const [loading, setLoading] =
    useState(true);

  useEffect(() => {
    const loadRequest = async () => {
      try {
        const result =
          await serviceRequestService.getRequestById(
            requestId
          );

        setRequest(result);
      } catch {
        Alert.alert(
          "Error",
          "Unable to load request."
        );
      } finally {
        setLoading(false);
      }
    };

    void loadRequest();
  }, [requestId]);

  if (loading) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator size="large" />
      </SafeAreaView>
    );
  }

  if (!request) {
    return (
      <SafeAreaView style={styles.center}>
        <Text>Request not found.</Text>
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView>

        <View style={styles.card}>

          <Text style={styles.title}>
            {request.title}
          </Text>

          <Text style={styles.status}>
            {request.status}
          </Text>

          <Text style={styles.label}>
            Description
          </Text>

          <Text style={styles.value}>
            {request.description}
          </Text>

          <Text style={styles.label}>
            Requested Date
          </Text>

          <Text style={styles.value}>
            {new Date(
              request.requestedDate
            ).toLocaleString()}
          </Text>

          <Text style={styles.label}>
            Service Category
          </Text>

          <Text style={styles.value}>
            {request.serviceCategoryId}
          </Text>

        </View>

      </ScrollView>
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
    borderRadius: 12,
    padding: 20,
    elevation: 2,
  },

  title: {
    fontSize: 24,
    fontWeight: "700",
    marginBottom: 12,
  },

  status: {
    fontSize: 16,
    fontWeight: "600",
    color: "#2563EB",
    marginBottom: 20,
  },

  label: {
    marginTop: 12,
    fontWeight: "700",
    fontSize: 15,
  },

  value: {
    marginTop: 4,
    fontSize: 15,
    color: "#555",
  },
});