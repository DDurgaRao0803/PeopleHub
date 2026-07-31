import React, {
  useCallback,
  useEffect,
  useMemo,
  useState,
} from "react";

import {
  ActivityIndicator,
  Pressable,
  RefreshControl,
  SafeAreaView,
  ScrollView,
  SectionList,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

import {
  useFocusEffect,
  useNavigation,
} from "@react-navigation/native";

import type {
  NativeStackNavigationProp,
} from "@react-navigation/native-stack";

import { Ionicons } from "@expo/vector-icons";

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

import {
  serviceRequestService,
} from "../../services";

import type {
  ServiceRequest,
} from "../../types";

function getServiceIcon(
  title: string
): keyof typeof Ionicons.glyphMap {

  const value = title.toLowerCase();

  if (value.includes("clean")) {
    return "sparkles-outline";
  }

  if (value.includes("plumb")) {
    return "construct-outline";
  }

  if (value.includes("electric")) {
    return "flash-outline";
  }

  if (value.includes("paint")) {
    return "color-palette-outline";
  }

  if (value.includes("garden")) {
    return "leaf-outline";
  }

  if (
    value.includes("ac") ||
    value.includes("air")
  ) {
    return "snow-outline";
  }

  if (value.includes("car")) {
    return "car-sport-outline";
  }

  if (value.includes("deliver")) {
    return "bicycle-outline";
  }

  if (value.includes("computer")) {
    return "laptop-outline";
  }

  if (
    value.includes("mobile") ||
    value.includes("phone")
  ) {
    return "phone-portrait-outline";
  }

  return "construct-outline";
}

function getServiceColor(
  title: string
) {

  const value = title.toLowerCase();

  if (value.includes("clean")) {
    return {
      background: "#DCFCE7",
      icon: "#16A34A",
    };
  }

  if (value.includes("electric")) {
    return {
      background: "#FEF3C7",
      icon: "#D97706",
    };
  }

  if (value.includes("plumb")) {
    return {
      background: "#DBEAFE",
      icon: "#2563EB",
    };
  }

  if (value.includes("paint")) {
    return {
      background: "#F3E8FF",
      icon: "#9333EA",
    };
  }

  return {
    background: "#E5E7EB",
    icon: "#374151",
  };
}

function getRelativeTime(
  value?: string
) {

  if (!value) {
    return "Just now";
  }

  const created = new Date(value);

  const minutes = Math.floor(
    (Date.now() - created.getTime()) / 60000
  );

  if (minutes < 1) {
    return "Just now";
  }

  if (minutes < 60) {
    return `${minutes} min ago`;
  }

  const hours = Math.floor(minutes / 60);

  if (hours < 24) {
    return `${hours} hr ago`;
  }

  const days = Math.floor(hours / 24);

  return `${days} day ago`;
}

function getStatusColor(
  status: string
) {

  switch (status.toLowerCase()) {

    case "new":
      return "#16A34A";

    case "accepted":
      return "#2563EB";

    case "completed":
      return "#059669";

    case "cancelled":
      return "#DC2626";

    default:
      return "#6B7280";
  }
}

export function ProviderRequestsScreen(): React.JSX.Element {

  const navigation =
    useNavigation<
      NativeStackNavigationProp<MainStackParamList>
    >();

  const filters = [
    "All",
    "New",
    "Accepted",
    "Completed",
    "Cancelled",
  ];

  const [requests, setRequests] =
    useState<ServiceRequest[]>([]);

  const [loading, setLoading] =
    useState(true);

  const [refreshing, setRefreshing] =
    useState(false);

  const [selectedFilter, setSelectedFilter] =
    useState("All");

  const filteredRequests =
    useMemo(() => {

      if (selectedFilter === "All") {
        return requests;
      }

      return requests.filter(
        request =>
          request.status.toLowerCase() ===
          selectedFilter.toLowerCase()
      );

    }, [
      requests,
      selectedFilter,
    ]);

  const requestSections =
    useMemo(
      () => [
        {
          title: "New Requests",
          status: "New",
          data: filteredRequests.filter(
            item => item.status === "New"
          ),
        },
        {
          title: "Accepted Requests",
          status: "Accepted",
          data: filteredRequests.filter(
            item => item.status === "Accepted"
          ),
        },
        {
          title: "Completed Requests",
          status: "Completed",
          data: filteredRequests.filter(
            item => item.status === "Completed"
          ),
        },
        {
          title: "Cancelled Requests",
          status: "Cancelled",
          data: filteredRequests.filter(
            item => item.status === "Cancelled"
          ),
        },
      ],
      [filteredRequests]
    );

  const loadRequests =
    useCallback(async () => {

      try {

        const data =
          await serviceRequestService.getMyProviderRequests();

        setRequests(data);

      } catch {

        setRequests([]);

      } finally {

        setLoading(false);
        setRefreshing(false);

      }

    }, []);

  useEffect(() => {
    void loadRequests();
  }, [loadRequests]);

  useFocusEffect(
    useCallback(() => {

      void loadRequests();

    }, [loadRequests])
  );

  const onRefresh = async () => {

    setRefreshing(true);

    await loadRequests();

  };

  if (loading) {

    return (
      <SafeAreaView style={styles.center}>
        <ActivityIndicator size="large" />
      </SafeAreaView>
    );

  }

  if (requests.length === 0) {

    return (
      <SafeAreaView style={styles.center}>

        <Text style={styles.emptyTitle}>
          No Provider Requests
        </Text>

        <Text style={styles.emptyText}>
          There are currently no requests assigned to you.
        </Text>

      </SafeAreaView>
    );

  }

    return (
    <SafeAreaView style={styles.container}>

      {/* Header */}

      <View style={styles.header}>

        <Pressable
          style={styles.headerButton}
          onPress={() => navigation.goBack()}
        >
          <Ionicons
            name="arrow-back"
            size={22}
            color="#111827"
          />
        </Pressable>

        <Text style={styles.headerTitle}>
          Recent Requests
        </Text>

        <Pressable style={styles.headerButton}>
          <Ionicons
            name="search-outline"
            size={22}
            color="#111827"
          />
        </Pressable>

      </View>

      {/* Filters */}

      <ScrollView
        horizontal
        showsHorizontalScrollIndicator={false}
        contentContainerStyle={styles.filterContainer}
      >
        {filters.map(filter => (

          <Pressable
            key={filter}
            onPress={() => setSelectedFilter(filter)}
            style={[
              styles.filterChip,
              selectedFilter === filter &&
                styles.activeChip,
            ]}
          >

            <Text
              style={[
                styles.filterText,
                selectedFilter === filter &&
                  styles.activeFilterText,
              ]}
            >
              {filter}
            </Text>

          </Pressable>

        ))}
      </ScrollView>

      <SectionList
        sections={requestSections.filter(
          section => section.data.length > 0
        )}
        keyExtractor={(item) => item.id}
        refreshControl={
          <RefreshControl
            refreshing={refreshing}
            onRefresh={onRefresh}
          />
        }

        contentContainerStyle={{
  paddingBottom: 25,
}}

        renderSectionHeader={({ section }) => (

          <View style={styles.sectionHeader}>

            <View>

              <Text style={styles.sectionTitle}>
                {section.title}
              </Text>

              <Text style={styles.sectionCount}>
                {section.data.length} request
                {section.data.length !== 1 ? "s" : ""}
              </Text>

            </View>

            <TouchableOpacity
              onPress={() =>
                setSelectedFilter(section.status)
              }
            >

              <Text style={styles.viewAllText}>
                View All
              </Text>

            </TouchableOpacity>

          </View>

        )}

        renderItem={({ item }) => {

  const iconColors =
    getServiceColor(item.title);

  return (

    <TouchableOpacity
      activeOpacity={0.85}
      style={styles.requestCard}
      onPress={() =>
        navigation.navigate(
  "RequestDetails",
          {
            requestId: item.id,
          }
        )
      }
    >

      {/* Left Icon */}

      <View
        style={[
          styles.serviceIcon,
          {
            backgroundColor:
              iconColors.background,
          },
        ]}
      >

        <Ionicons
          name={getServiceIcon(item.title)}
          size={20}
          color={iconColors.icon}
        />

      </View>

      {/* Middle */}

      <View style={styles.middleSection}>

        <Text
          numberOfLines={1}
          style={styles.serviceTitle}
        >
          {item.title}
        </Text>

        <Text
          numberOfLines={1}
          style={styles.serviceSubtitle}
        >
          {item.description}
        </Text>

        <View style={styles.locationRow}>

          <Ionicons
            name="location-outline"
            size={13}
            color="#6B7280"
          />

          <Text
            numberOfLines={1}
            style={styles.locationText}
          >
            {item.serviceAddress}
          </Text>

        </View>

      </View>

      {/* Right */}

      <View style={styles.rightSection}>

        <View
          style={[
            styles.statusBadge,
            {
              backgroundColor:
                getStatusColor(
                  item.status
                ),
            },
          ]}
        >

          <Text
            style={
              styles.statusBadgeText
            }
          >
            {item.status}
          </Text>

        </View>

        <Text style={styles.timeText}>
          "Just now"
        </Text>

        <Ionicons
          name="chevron-forward"
          size={18}
          color="#D1D5DB"
        />

      </View>

    </TouchableOpacity>

  );

}}

        ListEmptyComponent={
          <View style={styles.center}>

            <Text style={styles.emptyTitle}>
              No Requests Found
            </Text>

            <Text style={styles.emptyText}>
              No requests match the selected filter.
            </Text>

          </View>
        }
      />

    </SafeAreaView>
  );

}
const styles = StyleSheet.create({

  container: {
    flex: 1,
    backgroundColor: "#F8FAFC",
  },

  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 24,
  },

  emptyTitle: {
    fontSize: 20,
    fontWeight: "700",
    color: "#111827",
  },

  emptyText: {
    marginTop: 8,
    textAlign: "center",
    fontSize: 14,
    color: "#6B7280",
  },

  header: {
    height: 56,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",
    paddingHorizontal: 16,
    backgroundColor: "#FFFFFF",
  },

  headerButton: {
    width: 40,
    height: 40,
    justifyContent: "center",
    alignItems: "center",
  },

  headerTitle: {
    fontSize: 20,
    fontWeight: "700",
    color: "#111827",
  },

  filterContainer: {
    paddingHorizontal: 16,
    paddingVertical: 14,
  },

  filterChip: {
    height: 36,
    paddingHorizontal: 18,
    borderRadius: 18,
    backgroundColor: "#FFFFFF",
    borderWidth: 1,
    borderColor: "#E5E7EB",
    justifyContent: "center",
    alignItems: "center",
    marginRight: 10,
  },

  activeChip: {
    backgroundColor: "#16A34A",
    borderColor: "#16A34A",
  },

  filterText: {
    fontSize: 13,
    fontWeight: "600",
    color: "#6B7280",
  },

  activeFilterText: {
    color: "#FFFFFF",
  },

    sectionHeader: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    paddingHorizontal: 16,
    marginTop: 18,
    marginBottom: 10,
  },

  sectionTitle: {
    fontSize: 18,
    fontWeight: "700",
    color: "#111827",
  },

  sectionCount: {
    marginTop: 2,
    fontSize: 12,
    color: "#6B7280",
  },

  viewAllText: {
    fontSize: 13,
    fontWeight: "600",
    color: "#16A34A",
  },

  requestCard: {
  flexDirection: "row",
  alignItems: "center",
  backgroundColor: "#FFFFFF",
  marginHorizontal: 12,
  marginBottom: 10,
  paddingVertical: 12,
  paddingHorizontal: 14,
  borderRadius: 14,
  borderWidth: 1,
  borderColor: "#EEF2F7",

  shadowColor: "#000",
  shadowOpacity: 0.04,
  shadowRadius: 6,
  shadowOffset: {
    width: 0,
    height: 2,
  },

  elevation: 2,
},

  serviceIcon: {
  width: 44,
  height: 44,
  borderRadius: 22,
  justifyContent: "center",
  alignItems: "center",
  marginRight: 12,
},

  middleSection: {
  flex: 1,
  justifyContent: "center",
  paddingRight: 8,
},

  serviceTitle: {
    fontSize: 16,
    fontWeight: "700",
    color: "#111827",
  },

  serviceSubtitle: {
    marginTop: 3,
    fontSize: 13,
    color: "#6B7280",
  },

  locationRow: {
  flexDirection: "row",
  alignItems: "center",
  marginTop: 4,
},

  locationText: {
    flex: 1,
    marginLeft: 5,
    fontSize: 12,
    color: "#6B7280",
  },

  rightSection: {
  height: 54,
  marginLeft: 8,
  justifyContent: "space-between",
  alignItems: "flex-end",
},

  statusBadge: {
    paddingHorizontal: 10,
    paddingVertical: 4,
    borderRadius: 12,
  },

  statusBadgeText: {
    color: "#FFFFFF",
    fontSize: 10,
    fontWeight: "700",
  },

  timeText: {
    fontSize: 11,
    color: "#6B7280",
  },

});