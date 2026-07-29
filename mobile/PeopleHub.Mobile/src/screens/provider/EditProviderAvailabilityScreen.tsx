import React, { useMemo } from "react";
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

  // TODO: Replace with authenticated provider profile id
  const providerProfileId = "";

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
    } catch {
      Alert.alert(
        "Error",
        "Unable to update availability."
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