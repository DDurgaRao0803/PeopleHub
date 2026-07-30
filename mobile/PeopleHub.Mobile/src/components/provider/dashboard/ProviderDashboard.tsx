import React, { useEffect, useState } from "react";
import { ScrollView, StyleSheet, Text, View } from "react-native";

import DashboardHeader from "./DashboardHeader";
import AvailabilityCard from "./AvailabilityCard";
import SummaryCard from "./SummaryCard";
import ProviderQuickActions from "./QuickActions";
import RecentActivity from "./RecentActivity";

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
    <ScrollView
      showsVerticalScrollIndicator={false}
      contentContainerStyle={styles.content}
    >
      <DashboardHeader firstName={firstName} />

      <AvailabilityCard
        acceptingRequests={profile?.acceptingRequests ?? false}
        onToggle={handleAvailabilityToggle}
      />

      <View style={styles.row}>
  <SummaryCard
    title="⭐ Rating"
    value={(dashboard?.averageRating ?? 0).toFixed(1)}
  />

  <SummaryCard
    title="📦 Jobs"
    value={(dashboard?.completedJobs ?? 0).toString()}
  />
</View>

<SummaryCard
  title="📈 Response"
  value={`${dashboard?.responseRate ?? 0}%`}
/>

<ProviderQuickActions actions={quickActions} />

      <RecentActivity />
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  content: {
    paddingBottom: 32,
  },

  section: {
    marginBottom: 24,
  },

  sectionTitle: {
    fontSize: 20,
    fontWeight: "700",
    color: "#1F2937",
    marginBottom: 16,
  },

  row: {
    flexDirection: "row",
    marginBottom: 12,
  },
});