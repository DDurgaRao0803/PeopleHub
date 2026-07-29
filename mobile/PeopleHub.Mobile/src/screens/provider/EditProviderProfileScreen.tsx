import React, { useEffect, useState } from "react";
import {
  ActivityIndicator,
  Alert,
  SafeAreaView,
  StyleSheet,
} from "react-native";

import { useNavigation } from "@react-navigation/native";
import type { NativeStackNavigationProp } from "@react-navigation/native-stack";

import type { MainStackParamList } from "../../navigation/MainStackNavigator";

import {
  providerApi,
  type ProviderProfile,
  type UpdateProviderProfileRequest,
} from "../../api/providerApi";

import { ProviderProfileForm } from "../../components/provider";

export function EditProviderProfileScreen(): React.JSX.Element {
  const navigation =
    useNavigation<
      NativeStackNavigationProp<MainStackParamList>
    >();

  const [profile, setProfile] =
    useState<ProviderProfile | null>(null);

    useEffect(() => {
}, [profile]);

  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);

  useEffect(() => {
    const loadProfile = async () => {
      try {
        const data = await providerApi.getProfile();
        setProfile(data);
      } catch {
        Alert.alert(
          "Error",
          "Unable to load provider profile."
        );
      } finally {
        setLoading(false);
      }
    };

    void loadProfile();
  }, []);

  const handleSubmit = async (
  values: UpdateProviderProfileRequest
) => {

  try {
    setSaving(true);

    await providerApi.updateProfile(values);

    Alert.alert("Success", "Profile updated.");

navigation.goBack();

  } catch (error) {

    Alert.alert(
      "Error",
      "Unable to update profile."
    );
  } finally {
    setSaving(false);
  }
};

  if (loading) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator size="large" />
      </SafeAreaView>
    );
  }

  if (!profile) {
    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator size="large" />
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container}>
      <ProviderProfileForm
        initialValues={profile}
        loading={saving}
        onSubmit={handleSubmit}
      />
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: "#F5F6FA",
  },

  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
});