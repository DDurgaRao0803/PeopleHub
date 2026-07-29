import React, { useEffect, useState } from "react";

import {
  ActivityIndicator,
  Alert,
  SafeAreaView,
  StyleSheet,
} from "react-native";

import {
  useNavigation,
  useRoute,
  RouteProp,
} from "@react-navigation/native";

import type {
  NativeStackNavigationProp,
} from "@react-navigation/native-stack";

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

import { ProviderServiceForm } from "../../components/provider/ProviderServiceForm";
import { providerService } from "../../services/providerService";

import type {
  CreateProviderServiceRequest,
  ProviderService,
  UpdateProviderServiceRequest,
} from "../../types/provider";

type NavigationProp = NativeStackNavigationProp<MainStackParamList>;

type ScreenRouteProp = RouteProp<
  MainStackParamList,
  "EditProviderService"
>;

export function EditProviderServiceScreen(): React.JSX.Element {
  const navigation = useNavigation<NavigationProp>();
  const route = useRoute<ScreenRouteProp>();

  const { serviceId } = route.params;

  const [service, setService] =
    useState<ProviderService | null>(null);

  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadService = async () => {
      try {
        const data =
          await providerService.getProviderService(
            serviceId
          );

        setService(data);
      } catch {
        Alert.alert(
          "Error",
          "Unable to load provider service."
        );

        navigation.goBack();
      } finally {
        setLoading(false);
      }
    };

    void loadService();
  }, [navigation, serviceId]);

  const updateService = async (
    request: CreateProviderServiceRequest
  ) => {
    if (!service) {
      return;
    }

    const updateRequest: UpdateProviderServiceRequest = {
      title: request.title,
      description: request.description,
      basePrice: request.basePrice,
      estimatedDurationMinutes:
        request.estimatedDurationMinutes,
      isActive: service.isActive,
    };

    try {
      await providerService.updateProviderService(
        service.id,
        updateRequest
      );

      Alert.alert(
        "Success",
        "Provider service updated successfully.",
        [
          {
            text: "OK",
            onPress: () => navigation.goBack(),
          },
        ]
      );
    } catch {
      Alert.alert(
        "Error",
        "Unable to update provider service."
      );
    }
  };

  if (loading) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator size="large" />
      </SafeAreaView>
    );
  }

  if (!service) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator />
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container}>
      <ProviderServiceForm
        providerProfileId={
          service.providerProfileId
        }
        submitText="Update Service"
        initialValues={{
          providerProfileId:
            service.providerProfileId,
          serviceCategoryId:
            service.serviceCategoryId,
          title: service.title,
          description:
            service.description,
          basePrice:
            service.basePrice,
          estimatedDurationMinutes:
            service.estimatedDurationMinutes,
        }}
        onSubmit={updateService}
      />
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
  },

  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
});