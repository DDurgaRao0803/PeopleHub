import React, { useState } from "react";
import {
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
} from "react-native";

import {
  CategorySection,
  HomeHeader,
  NearbyProviders,
  QuickActions as CustomerQuickActions,
  RecentActivity,
  SearchBar,
} from "../../components/home";

import ProviderQuickActions from "../../components/provider/dashboard/QuickActions";

import QuickActions from "../../components/provider/dashboard/QuickActions";

import DashboardHeader from "../../components/provider/dashboard/DashboardHeader";
import AvailabilityCard from "../../components/provider/dashboard/AvailabilityCard";
import { View } from "react-native";
import SummaryCard from "../../components/provider/dashboard/SummaryCard";
import ProviderDashboard from "../../components/provider/dashboard/ProviderDashboard";


import { useAuth } from "../../context/AuthContext";


export function HomeScreen(): React.JSX.Element {

  const { user } = useAuth();
  const [acceptingRequests, setAcceptingRequests] = useState(true);

  if (user?.isProvider) {
  return (
    <SafeAreaView style={styles.container}>
      <ProviderDashboard firstName={user.firstName} />
    </SafeAreaView>
  );
}


  return (
    <SafeAreaView style={styles.container}>
      <ScrollView
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.content}
      >
        <HomeHeader />

        <SearchBar />

        <CategorySection />

        <CustomerQuickActions />

        <NearbyProviders />

        <RecentActivity />
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#FFFFFF",
  },

  content: {
    paddingHorizontal: 20,
    paddingTop: 20,
    paddingBottom: 32,
  },
});