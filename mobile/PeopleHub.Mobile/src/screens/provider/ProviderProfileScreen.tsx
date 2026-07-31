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

import { Ionicons } from "@expo/vector-icons";

import { useNavigation } from "@react-navigation/native";
import type { NativeStackNavigationProp } from "@react-navigation/native-stack";

import type { MainStackParamList } from "../../navigation/MainStackNavigator";

import { useAuth } from "../../context/AuthContext";

import { providerService } from "../../services/providerService";

import type { ProviderProfile } from "../../api/providerApi";

import { colors } from "../../theme/colors";

export function ProviderProfileScreen(): React.JSX.Element {
  
  
  const navigation =
  useNavigation<
    NativeStackNavigationProp<MainStackParamList>
  >();

const { logout } = useAuth();

const [profile, setProfile] =
  useState<ProviderProfile | null>(null);

const [loading, setLoading] =
  useState(true);

const loadProfile = useCallback(async () => {
  try {
    const data =
      await providerService.getProfile();

    setProfile(data);
  } catch {
    Alert.alert(
      "Error",
      "Unable to load profile."
    );
  } finally {
    setLoading(false);
  }
}, []);

useEffect(() => {
  void loadProfile();
}, [loadProfile]);

const handleLogout = async (): Promise<void> => {
 await logout();

};

if (loading) {
  return (
    <SafeAreaView style={styles.center}>
      <ActivityIndicator size="large" color={colors.primary} />
    </SafeAreaView>
  );
}

if (!profile) {
  return (
    <SafeAreaView style={styles.center}>
      <Text style={styles.title}>Provider Profile</Text>

      <Text style={styles.empty}>
        Provider profile not found.
      </Text>
    </SafeAreaView>
  );
}

const MenuItem = (
  icon: keyof typeof Ionicons.glyphMap,
  title: string,
  onPress: () => void
) => (
  <TouchableOpacity
    style={styles.menuItem}
    onPress={onPress}
    activeOpacity={0.8}
  >
    <View style={styles.menuLeft}>
      <Ionicons
        name={icon}
        size={22}
        color={colors.primary}
      />

      <Text style={styles.menuTitle}>
        {title}
      </Text>
    </View>

    <Ionicons
      name="chevron-forward"
      size={20}
      color="#9CA3AF"
    />
  </TouchableOpacity>
);

return (
  <SafeAreaView style={styles.container}>
    <ScrollView
  showsVerticalScrollIndicator={false}
  contentContainerStyle={styles.content}
>
      <View style={styles.header}>
        <TouchableOpacity onPress={() => navigation.goBack()}>
          <Ionicons
            name="arrow-back"
            size={24}
            color="#111"
          />
        </TouchableOpacity>

        <Text style={styles.headerTitle}>
          Profile
        </Text>

        <View style={{ width: 24 }} />
      </View>

      <View style={styles.profileHeader}>
        <View style={styles.avatar}>
          <Ionicons
            name="person"
            size={46}
            color="#FFFFFF"
          />
        </View>

        <Text style={styles.name}>
  Provider
</Text>

        <View style={styles.goldRow}>
          <Ionicons
            name="star"
            size={18}
            color="#F4B400"
          />

          <Text style={styles.goldText}>
            Gold Provider
          </Text>
        </View>

        <View style={styles.verifiedBadge}>
          <Ionicons
            name="checkmark-circle"
            size={16}
            color="#16A34A"
          />

          <Text style={styles.verifiedText}>
            Verified Professional
          </Text>
        </View>
      </View>

    <View style={styles.sectionsContainer}>
    <Text style={styles.sectionTitle}>  
  PERSONAL
</Text>

<View style={styles.sectionCard}>
  {MenuItem(
    "person-outline",
    "Personal Information",
    () => navigation.navigate("EditProviderProfile")
  )}

  {MenuItem(
    "call-outline",
    "Contact Details",
    () => navigation.navigate("EditProviderProfile")
  )}

  {MenuItem(
    "location-outline",
    "Location",
    () => navigation.navigate("EditProviderProfile")
  )}
</View>

<Text style={styles.sectionTitle}>
  PROFESSIONAL
</Text>

<View style={styles.sectionCard}>
  {MenuItem(
    "briefcase-outline",
    "Professional Information",
    () => navigation.navigate("EditProviderProfile")
  )}

  {MenuItem(
    "construct-outline",
    "Experience & Skills",
    () => navigation.navigate("ProviderServices")
  )}

  {MenuItem(
    "time-outline",
    "Availability",
    () => navigation.navigate("AddProviderAvailability")
  )}
</View>

<Text style={styles.sectionTitle}>
  DOCUMENTS
</Text>

<View style={styles.sectionCard}>
  {MenuItem(
    "card-outline",
    "Aadhaar Card",
    () => {}
  )}

  {MenuItem(
    "shield-checkmark-outline",
    "Police Verification",
    () => {}
  )}

  {MenuItem(
    "wallet-outline",
    "Bank Details",
    () => {}
  )}
</View>

<Text style={styles.sectionTitle}>
  ACCOUNT
</Text>

<View style={styles.sectionCard}>
  {MenuItem(
    "notifications-outline",
    "Notifications",
    () => {}
  )}

  {MenuItem(
    "card-outline",
    "Payment Methods",
    () => {}
  )}

  {MenuItem(
    "lock-closed-outline",
    "Privacy",
    () => {}
  )}

  {MenuItem(
    "help-circle-outline",
    "Help & Support",
    () => {}
  )}
</View>

</View>

<TouchableOpacity
  style={styles.logoutCard}
  onPress={handleLogout}
>
  <Ionicons
    name="log-out-outline"
    size={22}
    color="#DC2626"
  />

  <Text style={styles.logoutText}>
    Logout
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
  paddingHorizontal: 20,
  paddingTop: 20,
  paddingBottom: 30,
},

sectionsContainer: {
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
    color: "#111827",
  },

  empty: {
    marginTop: 12,
    color: "#6B7280",
    fontSize: 16,
  },

  header: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    marginBottom: 24,
  },

  headerTitle: {
    fontSize: 22,
    fontWeight: "700",
    color: "#111827",
  },

  profileHeader: {
    backgroundColor: "#FFFFFF",
    borderRadius: 20,
    padding: 24,
    alignItems: "center",
    marginBottom: 24,
  },

  avatar: {
    width: 90,
    height: 90,
    borderRadius: 45,
    backgroundColor: colors.primary,
    justifyContent: "center",
    alignItems: "center",
    marginBottom: 16,
  },

  name: {
    fontSize: 22,
    fontWeight: "700",
    color: "#111827",
    marginBottom: 10,
  },

  goldRow: {
    flexDirection: "row",
    alignItems: "center",
    marginBottom: 10,
  },

  goldText: {
    marginLeft: 6,
    color: "#F4B400",
    fontWeight: "700",
    fontSize: 15,
  },

  verifiedBadge: {
    flexDirection: "row",
    alignItems: "center",
    backgroundColor: "#ECFDF5",
    paddingHorizontal: 12,
    paddingVertical: 6,
    borderRadius: 20,
  },

  verifiedText: {
    marginLeft: 6,
    color: "#16A34A",
    fontWeight: "600",
    fontSize: 14,
  },

  sectionTitle: {
    fontSize: 13,
    fontWeight: "700",
    color: "#6B7280",
    marginBottom: 10,
    marginTop: 8,
    letterSpacing: 1,
  },

  sectionCard: {
    backgroundColor: "#FFFFFF",
    borderRadius: 18,
    marginBottom: 24,
    overflow: "hidden",
  },

  menuItem: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    paddingHorizontal: 18,
    paddingVertical: 18,
    borderBottomWidth: 1,
    borderBottomColor: "#F3F4F6",
  },

  menuLeft: {
    flexDirection: "row",
    alignItems: "center",
  },

  menuTitle: {
    marginLeft: 14,
    fontSize: 16,
    color: "#111827",
    fontWeight: "500",
  },

  logoutCard: {
  flexDirection: "row",
  justifyContent: "center",
  alignItems: "center",
  backgroundColor: "#FFFFFF",
  borderRadius: 18,
  paddingVertical: 18,
  marginTop: 10,
  marginBottom: 40,
},

  logoutText: {
    marginLeft: 10,
    color: "#DC2626",
    fontSize: 17,
    fontWeight: "700",
  },
});