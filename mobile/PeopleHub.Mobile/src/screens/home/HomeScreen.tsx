import React from "react";
import {
  SafeAreaView,
  ScrollView,
  StyleSheet,
} from "react-native";

import {
  CategorySection,
  HomeHeader,
  NearbyProviders,
  QuickActions,
  RecentActivity,
  SearchBar,
} from "../../components/home";

export function HomeScreen(): React.JSX.Element {
  return (
    <SafeAreaView style={styles.container}>
      <ScrollView
        showsVerticalScrollIndicator={false}
        contentContainerStyle={styles.content}
      >
        <HomeHeader />

        <SearchBar />

        <CategorySection />

        <QuickActions />

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