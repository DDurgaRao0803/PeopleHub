import React, {
  useCallback,
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
  TouchableOpacity,
} from "react-native";

import {
  useNavigation,
  useRoute,
} from "@react-navigation/native";

import type {
  RouteProp,
} from "@react-navigation/native";

import type {
  NativeStackNavigationProp,
} from "@react-navigation/native-stack";

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

  const navigation =
  useNavigation<
    NativeStackNavigationProp<MainStackParamList>
  >();

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

    const [processing, setProcessing] =
  useState(false);

  const loadRequest = useCallback(async () => {
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
}, [requestId]);

const handleAccept = async () => {
  if (!request) {
    return;
  }

  try {
    setProcessing(true);

    await serviceRequestService.acceptRequest(request.id);

    navigation.goBack();
  } catch {
    Alert.alert(
      "Error",
      "Unable to accept the request."
    );
  } finally {
    setProcessing(false);
  }
};

const handleReject = async () => {
  if (!request) {
    return;
  }

  try {
    setProcessing(true);

    await serviceRequestService.rejectRequest(request.id);

    Alert.alert(
      "Success",
      "Request rejected successfully."
    );

    navigation.goBack();
  } catch {
    Alert.alert(
      "Error",
      "Unable to reject the request."
    );
  } finally {
    setProcessing(false);
  }
};

useEffect(() => {
  void loadRequest();
}, [requestId, loadRequest]);

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

          {request.status === "Pending" && (

  <View style={styles.buttonContainer}>

    <TouchableOpacity
      style={[
        styles.button,
        styles.rejectButton,
      ]}
      disabled={processing}
      onPress={() => {
        void handleReject();
      }}
    >
      <Text style={styles.buttonText}>
        Reject
      </Text>
    </TouchableOpacity>

    <TouchableOpacity
      style={[
        styles.button,
        styles.acceptButton,
      ]}
      disabled={processing}
      onPress={() => {
        void handleAccept();
      }}
    >
      <Text style={styles.buttonText}>
        {processing
          ? "Processing..."
          : "Accept"}
      </Text>
    </TouchableOpacity>

  </View>

)}

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

  buttonContainer: {
  flexDirection: "row",
  justifyContent: "space-between",
  marginTop: 24,
},

button: {
  flex: 1,
  paddingVertical: 14,
  borderRadius: 10,
  alignItems: "center",
},

acceptButton: {
  backgroundColor: "#16A34A",
  marginLeft: 8,
},

rejectButton: {
  backgroundColor: "#DC2626",
  marginRight: 8,
},

buttonText: {
  color: "#FFFFFF",
  fontWeight: "700",
  fontSize: 16,
},
});