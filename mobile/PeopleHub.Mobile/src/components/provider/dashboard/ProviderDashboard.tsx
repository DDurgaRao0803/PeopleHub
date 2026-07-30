import React, { useEffect, useState } from "react";
import {
  RefreshControl,
  ScrollView,
  StyleSheet,
  View,
} from "react-native";

import {
  colors,
  spacing,
} from "../../../theme";

import DashboardHeader from "./DashboardHeader";
import AvailabilityCard from "./AvailabilityCard";
import SummaryCard from "./SummaryCard";
import ProviderQuickActions from "./QuickActions";
import RecentActivity from "./RecentRequests";

import { providerService } from "../../../services/providerService";
import type { ProviderProfile } from "../../../api/providerApi";
import type { ProviderDashboard } from "../../../types/providerDashboard";

type ProviderDashboardProps = {
  firstName: string;
};

export default function ProviderDashboard({
  firstName,
}: ProviderDashboardProps): React.JSX.Element {

  const [profile, setProfile] = useState<ProviderProfile | null>(null);
  const [loading, setLoading] = useState(true);
  const [dashboard, setDashboard] = useState<ProviderDashboard | null>(null);
  const [refreshing, setRefreshing] = useState(false);

  const quickActions = [
    { title: "View Requests", onPress: () => {} },
    { title: "My Services", onPress: () => {} },
    { title: "Availability", onPress: () => {} },
    { title: "Reviews", onPress: () => {} },
  ];

  const loadProfile = async () => {
    try {
      const [profileData, dashboardData] = await Promise.all([
  providerService.getProfile(),
  providerService.getDashboard(),
]);

setProfile(profileData);
setDashboard(dashboardData);
    } catch (error) {
      console.error("Failed to load provider profile", error);
    } finally {
      setLoading(false);
    }
  };

const handleRefresh = async () => {
  setRefreshing(true);

  try {
    await loadProfile();
  } finally {
    setRefreshing(false);
  }
};


  const handleAvailabilityToggle = async (value: boolean) => {
    if (!profile) {
      return;
    }

    try {
      const updatedProfile = await providerService.updateProfile({
        bio: profile.bio,
        experienceYears: profile.experienceYears,
        acceptingRequests: value,
      });

      setProfile(updatedProfile);
    } catch (error) {
      console.error("Failed to update provider availability", error);
    }
  };

  useEffect(() => {
    loadProfile();
  }, []);

  if (loading) {
    return <View />;
  }

  return (
  <View style={styles.container}>
    <ScrollView
      showsVerticalScrollIndicator={false}
      contentContainerStyle={styles.content}
      refreshControl={
        <RefreshControl
          refreshing={refreshing}
          onRefresh={handleRefresh}
          tintColor={colors.primary}
        />
      }
    >
      <DashboardHeader firstName={firstName} />

      <AvailabilityCard
        acceptingRequests={profile?.acceptingRequests ?? false}
        onToggle={handleAvailabilityToggle}
      />

      <View style={styles.row}>
        <SummaryCard
          title="Jobs"
          value={(dashboard?.completedJobs ?? 0).toString()}
        />

        <SummaryCard
          title="Pending"
          value={(dashboard?.pendingRequests ?? 0).toString()}
        />

        <SummaryCard
          title="Earnings"
          value="₹0"
        />

        <SummaryCard
          title="Rating"
          value={(dashboard?.averageRating ?? 0).toFixed(1)}
        />
      </View>

      <ProviderQuickActions actions={quickActions} />

      <RecentActivity />
    </ScrollView>

  </View>
);
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
  },

  content: {
    paddingHorizontal: spacing.lg,
    paddingTop: spacing.lg,
    paddingBottom: 110,
  },

  row: {
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: spacing.lg,
  },
});