import React, {
  useEffect,
  useState,
} from "react";

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

  const [providerProfileId, setProviderProfileId] =
  useState("");

useEffect(() => {
  const loadProfile = async () => {
    try {
      const profile =
        await providerService.getProfile();

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

  const createService = async (
    request: CreateProviderServiceRequest
  ) => {
    try {

      await providerService.createProviderService(request);

Alert.alert(
  "Success",
  "Provider service created successfully."
);

navigation.goBack();

    } catch (error: any) {


  Alert.alert(
    "Error",
    JSON.stringify(error.response?.data ?? error.message)
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