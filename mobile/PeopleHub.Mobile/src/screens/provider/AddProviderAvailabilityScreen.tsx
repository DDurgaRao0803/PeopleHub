import React, { useEffect, useState } from "react";
import { providerApi } from "../../api/providerApi";

import { Alert, SafeAreaView, StyleSheet } from "react-native";
import { useNavigation } from "@react-navigation/native";



import { ProviderAvailabilityForm } from "../../components/provider";
import { providerAvailabilityService } from "../../services";
import {
  CreateProviderAvailabilityRequest,
} from "../../types";

export function AddProviderAvailabilityScreen() {
  const navigation = useNavigation<any>();

  const [providerProfileId, setProviderProfileId] =
  useState("");

  useEffect(() => {
  const loadProfile = async () => {
    try {
      const profile = await providerApi.getProfile();

      console.log("Provider Profile:", profile);

      setProviderProfileId(profile.id);

      console.log("Provider Profile Id:", profile.id);
    } catch (error) {
      console.log("Load Profile Error:", error);

      Alert.alert(
        "Error",
        "Unable to load provider profile."
      );
    }
  };

  void loadProfile();
}, []);

  const handleSubmit = async (
  request: CreateProviderAvailabilityRequest
) => {
  if (!providerProfileId) {
    Alert.alert(
      "Error",
      "Provider profile is not loaded yet."
    );
    return;
  }

  try {
    await providerAvailabilityService.createAvailability(
      providerProfileId,
      request
    );

    Alert.alert(
      "Success",
      "Availability added successfully."
    );

    navigation.goBack();
  } catch {
    Alert.alert(
      "Error",
      "Unable to add availability."
    );
  }
};

  return (
    <SafeAreaView style={styles.container}>
      <ProviderAvailabilityForm
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