import React, {
  useEffect,
  useMemo,
  useState,
} from "react";

import { providerApi } from "../../api/providerApi";

import {
  Alert,
  SafeAreaView,
  StyleSheet,
} from "react-native";
import {
  useNavigation,
  useRoute,
} from "@react-navigation/native";

import { ProviderAvailabilityForm } from "../../components/provider";
import { providerAvailabilityService } from "../../services";
import {
  ProviderAvailability,
  UpdateProviderAvailabilityRequest,
} from "../../types";

export function EditProviderAvailabilityScreen() {
  const navigation = useNavigation<any>();
  const route = useRoute<any>();

  const [providerProfileId, setProviderProfileId] =
  useState("");

  useEffect(() => {
  const loadProfile = async () => {
    try {
      const profile = await providerApi.getProfile();
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

  const {
    availabilityId,
    availability,
  } = route.params;

  const initialValues =
    useMemo<ProviderAvailability>(
      () => availability,
      [availability]
    );

  const handleSubmit = async (
    request: UpdateProviderAvailabilityRequest
  ) => {

    if (!providerProfileId) {
  Alert.alert(
    "Error",
    "Provider profile not loaded."
  );
  return;
}

    try {
      await providerAvailabilityService.updateAvailability(
        providerProfileId,
        availabilityId,
        request
      );

      Alert.alert(
        "Success",
        "Availability updated successfully."
      );

      navigation.goBack();
    } catch (error: any) {
  console.log("UPDATE STATUS:", error.response?.status);
  console.log("UPDATE DATA:", error.response?.data);

  Alert.alert(
    "Error",
    JSON.stringify(error.response?.data ?? error.message)
  );
}
  };

  return (
    <SafeAreaView style={styles.container}>
      <ProviderAvailabilityForm
        initialValues={initialValues}
        onSubmit={handleSubmit}
      />
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
});