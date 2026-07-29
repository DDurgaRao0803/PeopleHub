/**
 * ============================================================
 * PeopleHub Mobile
 * Customer Profile Screen
 * ============================================================
 */

import React from "react";
import {
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

import { useAuth } from "../../context/AuthContext";
import { colors } from "../../theme/colors";
import { spacing } from "../../theme/spacing";
import { typography } from "../../theme/typography";

export function ProfileScreen(): React.JSX.Element {

  const {
    user,
    logout,
  } = useAuth();

  const navigation =
  useNavigation<
    NativeStackNavigationProp<MainStackParamList>
  >();

  const handleLogout = async (): Promise<void> => {


    try {
      await logout();

    } catch  {
      
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView
        contentContainerStyle={styles.content}
        showsVerticalScrollIndicator={false}
      >
        <View style={styles.header}>
          <View style={styles.avatar}>
            <Text style={styles.avatarText}>
              {user?.firstName?.charAt(0).toUpperCase() ?? "U"}
            </Text>
          </View>

          <Text style={styles.name}>
            {`${user?.firstName ?? ""} ${user?.lastName ?? ""}`.trim() ||
              "Unknown User"}
          </Text>

          <Text style={styles.role}>
            {user?.role ?? "Customer"}
          </Text>
        </View>

        <View style={styles.card}>
          <Text style={styles.label}>
            Email
          </Text>

          <Text style={styles.value}>
            {user?.email ?? "-"}
          </Text>

          <View style={styles.divider} />
        </View>

        <TouchableOpacity
  style={styles.menuButton}
  onPress={() => {
    if (user?.isProvider) {
      navigation.navigate("EditProviderProfile");
    } else {
      Alert.alert(
        "Coming Soon",
        "Customer profile editing will be available soon."
      );
    }
  }}
>
  <Text style={styles.menuText}>
    Edit Profile
  </Text>
</TouchableOpacity>

        <TouchableOpacity
          style={styles.menuButton}
          onPress={() =>
            Alert.alert(
              "Coming Soon",
              "Settings will be available soon.",
            )
          }
        >
          <Text style={styles.menuText}>
            Settings
          </Text>
        </TouchableOpacity>

        <TouchableOpacity
          style={styles.logoutButton}
          onPress={handleLogout}
        >
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
    backgroundColor: colors.background,
  },

  content: {
    padding: spacing.lg,
  },

  header: {
    alignItems: "center",
    marginBottom: spacing.xl,
  },

  avatar: {
    width: 90,
    height: 90,
    borderRadius: 45,
    backgroundColor: colors.primary,
    justifyContent: "center",
    alignItems: "center",
    marginBottom: spacing.md,
  },

  avatarText: {
    color: colors.text.inverse,
    fontSize: 34,
    fontWeight: "700",
  },

  name: {
    ...typography.h2,
    color: colors.text.primary,
  },

  role: {
    ...typography.body,
    color: colors.text.secondary,
    marginTop: spacing.xs,
  },

  card: {
    backgroundColor: colors.surface,
    borderRadius: 12,
    padding: spacing.lg,
    marginBottom: spacing.xl,
  },

  label: {
    ...typography.caption,
    color: colors.text.secondary,
  },

  value: {
    ...typography.body,
    color: colors.text.primary,
    marginTop: spacing.xs,
    marginBottom: spacing.md,
  },

  divider: {
    height: 1,
    backgroundColor: colors.border,
    marginVertical: spacing.sm,
  },

  menuButton: {
    backgroundColor: colors.surface,
    padding: spacing.lg,
    borderRadius: 12,
    marginBottom: spacing.md,
  },

  menuText: {
    ...typography.body,
    color: colors.text.primary,
    fontWeight: "600",
  },

  logoutButton: {
    backgroundColor: "#E53935",
    padding: spacing.lg,
    borderRadius: 12,
    alignItems: "center",
    marginTop: spacing.lg,
  },

  logoutText: {
    color: "#FFFFFF",
    fontWeight: "700",
    fontSize: 16,
  },
});