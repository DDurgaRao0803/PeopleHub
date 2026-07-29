import React from "react";
import { Alert, SafeAreaView, StyleSheet } from "react-native";
import { useNavigation } from "@react-navigation/native";

import { ProviderAvailabilityForm } from "../../components/provider";
import { providerAvailabilityService } from "../../services";
import {
  CreateProviderAvailabilityRequest,
} from "../../types";

export function AddProviderAvailabilityScreen() {
  const navigation = useNavigation<any>();

  // TODO: Replace with authenticated provider profile id
  const providerProfileId = "";

  const handleSubmit = async (
    request: CreateProviderAvailabilityRequest
  ) => {
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