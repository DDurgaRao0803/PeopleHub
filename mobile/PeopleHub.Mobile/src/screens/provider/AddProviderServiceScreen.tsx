import React from "react";

import {
  Alert,
  SafeAreaView,
  StyleSheet,
} from "react-native";

import {
  useNavigation,
} from "@react-navigation/native";

import type {
  NativeStackNavigationProp,
} from "@react-navigation/native-stack";

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

import {
  ProviderServiceForm,
} from "../../components/provider/ProviderServiceForm";

import {
  providerService,
} from "../../services/providerService";

import type {
  CreateProviderServiceRequest,
} from "../../types/provider";

export function AddProviderServiceScreen(): React.JSX.Element {

  const navigation =
    useNavigation<
      NativeStackNavigationProp<MainStackParamList>
    >();

  /**
   * TODO
   *
   * Replace this with the authenticated
   * provider profile id.
   */
  const providerProfileId = "";

  const createService = async (
    request: CreateProviderServiceRequest
  ) => {
    try {

      await providerService.createProviderService(
        request
      );

      Alert.alert(
        "Success",
        "Provider service created successfully.",
        [
          {
            text: "OK",
            onPress: () =>
              navigation.goBack(),
          },
        ]
      );

    } catch {

      Alert.alert(
        "Error",
        "Unable to create provider service."
      );

    }
  };

  return (
    <SafeAreaView style={styles.container}>

      <ProviderServiceForm
        providerProfileId={providerProfileId}
        submitText="Create Service"
        onSubmit={createService}
      />

    </SafeAreaView>
  );
}

const styles = StyleSheet.create({

  container: {
    flex: 1,
    padding: 20,
  },

});