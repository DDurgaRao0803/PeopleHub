import React, { useCallback, useEffect, useState } from "react";

import {
  ActivityIndicator,
  Alert,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import { useNavigation } from "@react-navigation/native";
import type { NativeStackNavigationProp } from "@react-navigation/native-stack";

import type { MainStackParamList } from "../../navigation/MainStackNavigator";

import { providerService } from "../../services/providerService";
import type { ProviderProfile } from "../../api/providerApi";

export function ProviderProfileScreen(): React.JSX.Element {
  const navigation =
    useNavigation<
      NativeStackNavigationProp<MainStackParamList>
    >();

  const [profile, setProfile] =
    useState<ProviderProfile | null>(null);

  const [loading, setLoading] =
    useState(true);

  const loadProfile = useCallback(async () => {
    try {
      const data = await providerService.getProfile();
      setProfile(data);
    } catch {
  Alert.alert(
    "Error",
    "Unable to load provider requests."
  );
} finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    void loadProfile();
  }, [loadProfile]);

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
        <Text style={styles.title}>
          Provider Profile
        </Text>

        <Text style={styles.empty}>
          Provider profile not found.
        </Text>
      </SafeAreaView>
    );
  }

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView
        contentContainerStyle={styles.content}
      >
        <Text style={styles.title}>
          Provider Profile
        </Text>

        <View style={styles.card}>
          <Text style={styles.label}>Bio</Text>
          <Text style={styles.value}>
            {profile.bio || "-"}
          </Text>
        </View>

        <View style={styles.card}>
          <Text style={styles.label}>
            Experience
          </Text>
          <Text style={styles.value}>
            {profile.experienceYears} Years
          </Text>
        </View>


        <TouchableOpacity
          style={styles.primaryButton}
          onPress={() =>
            navigation.navigate(
              "ProviderServices"
            )
          }
        >
          <Text style={styles.buttonText}>
            My Services
          </Text>
        </TouchableOpacity>

        <TouchableOpacity
  style={styles.secondaryButton}
  onPress={() => {
    console.log("Edit Profile pressed");
    Alert.alert("Pressed");
    navigation.navigate("EditProviderProfile");
  }}
>
  <Text style={styles.buttonText}>
    Edit Profile
  </Text>
</TouchableOpacity>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#F5F6FA",
  },

  content: {
    padding: 20,
  },

  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 24,
  },

  title: {
    fontSize: 24,
    fontWeight: "700",
    marginBottom: 24,
  },

  empty: {
    marginTop: 10,
    color: "#666",
  },

  card: {
    backgroundColor: "#FFFFFF",
    padding: 16,
    borderRadius: 12,
    marginBottom: 16,
  },

  label: {
    fontSize: 14,
    color: "#666",
    marginBottom: 6,
  },

  value: {
    fontSize: 16,
    fontWeight: "600",
    color: "#111",
  },

  primaryButton: {
    marginTop: 20,
    backgroundColor: "#2563EB",
    padding: 16,
    borderRadius: 10,
    alignItems: "center",
  },

  secondaryButton: {
    marginTop: 12,
    backgroundColor: "#10B981",
    padding: 16,
    borderRadius: 10,
    alignItems: "center",
  },

  buttonText: {
    color: "#FFFFFF",
    fontWeight: "700",
    fontSize: 16,
  },
});