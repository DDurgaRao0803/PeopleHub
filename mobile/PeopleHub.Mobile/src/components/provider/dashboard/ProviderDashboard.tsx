import React, { useEffect, useState } from "react";
import {
  RefreshControl,
  ScrollView,
  StyleSheet,
  View,
  Text,
} from "react-native";

import {
  colors,
  spacing,
} from "../../../theme";

import DashboardHeader from "./DashboardHeader";
import AvailabilityCard from "./AvailabilityCard";
import StatisticsGrid from "./StatisticsGrid";
import ProviderQuickActions from "./QuickActions";
import RecentActivity from "./RecentRequests";
import { Ionicons } from "@expo/vector-icons";
import OnlineStatusCard from "./OnlineStatusCard";
import { useNavigation } from "@react-navigation/native";

import { providerService } from "../../../services/providerService";
import type { ProviderProfile } from "../../../api/providerApi";
import type { ProviderDashboard } from "../../../types/providerDashboard";

type ProviderDashboardProps = {
  firstName: string;
};

export default function ProviderDashboard({
  firstName,
}: ProviderDashboardProps): React.JSX.Element {

  const navigation = useNavigation();

  const [profile, setProfile] = useState<ProviderProfile | null>(null);
  const [loading, setLoading] = useState(true);
  const [dashboard, setDashboard] = useState<ProviderDashboard | null>(null);
  const [refreshing, setRefreshing] = useState(false);

  const quickActions = [
  {
    title: "View Requests",
    subtitle: "View new jobs",
    onPress: () => {},
  },
  {
    title: "My Services",
    subtitle: "Manage services",
    onPress: () => {},
  },
  {
    title: "Availability",
    subtitle: "Set working hours",
    onPress: () => {},
  },
  {
    title: "Reviews",
    subtitle: "Customer ratings",
    onPress: () => {},
  },
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

<OnlineStatusCard
    acceptingRequests={profile?.acceptingRequests ?? false}
    todayEarnings={1250}
    onToggle={handleAvailabilityToggle}
/>

      

      <StatisticsGrid
    completedJobs={dashboard?.completedJobs ?? 0}
    pendingRequests={dashboard?.pendingRequests ?? 0}
    earnings={0}
    rating={dashboard?.averageRating ?? 0}
/>

      <ProviderQuickActions
  actions={quickActions}
  onViewAll={() =>
    navigation.navigate("ProviderQuickActions" as never)
  }
/>

      <RecentActivity />
    </ScrollView>

  </View>
);
}

const styles = StyleSheet.create({
  container: {
  flex: 1,
  backgroundColor: "#F8FAFC",
},

  content: {
  paddingHorizontal: 20,
  paddingTop: 20,
  paddingBottom: 120,
},

  heroBanner: {
  backgroundColor: "#22C55E",
  borderRadius: 24,
  padding: 24,
  marginBottom: 24,
  flexDirection: "row",
  alignItems: "center",
  justifyContent: "space-between",
},

heroContent: {
  flex: 1,
  paddingRight: 16,
},

badge: {
  alignSelf: "flex-start",
  backgroundColor: "rgba(255,255,255,0.18)",
  borderRadius: 50,
  paddingHorizontal: 12,
  paddingVertical: 5,
  marginBottom: 14,
},

badgeText: {
  color: "#FFFFFF",
  fontSize: 11,
  fontWeight: "700",
  letterSpacing: 1,
},

heroTitle: {
  color: "#FFFFFF",
  fontSize: 26,
  fontWeight: "700",
},

heroSubtitle: {
  color: "#F0FDF4",
  fontSize: 15,
  marginTop: 8,
  lineHeight: 22,
},

heroIcon: {
  width: 90,
  height: 90,
  borderRadius: 45,
  backgroundColor: "rgba(255,255,255,0.15)",
  alignItems: "center",
  justifyContent: "center",
},

row: {
  flexDirection: "row",
  justifyContent: "space-between",
  marginBottom: spacing.lg,
},

});