
import React, {
  useCallback,
  useEffect,
  useState,
} from "react";

import {
  ActivityIndicator,
  FlatList,
  SectionList,
  Pressable,
  RefreshControl,
  SafeAreaView,
  ScrollView,
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

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

import {
  serviceRequestService,
} from "../../services";

import type {
  ServiceRequest,
} from "../../types";

import { Ionicons } from "@expo/vector-icons";

function getServiceIcon(
  title: string
): keyof typeof Ionicons.glyphMap {
  const value = title.toLowerCase();

  if (value.includes("plumb")) {
    return "construct-outline";
  }

  if (
    value.includes("electric")
  ) {
    return "flash-outline";
  }

  if (
    value.includes("garden")
  ) {
    return "leaf-outline";
  }

  if (
    value.includes("ac") ||
    value.includes("air")
  ) {
    return "snow-outline";
  }

  if (
    value.includes("clean")
  ) {
    return "sparkles-outline";
  }

  if (
    value.includes("paint")
  ) {
    return "color-palette-outline";
  }

  if (
    value.includes("car")
  ) {
    return "car-sport-outline";
  }

  if (
    value.includes("deliver")
  ) {
    return "bicycle-outline";
  }

  if (
    value.includes("computer")
  ) {
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

export function ProviderRequestsScreen(): React.JSX.Element {
  
  
  const filters = [
  "All",
  "New",
  "Accepted",
  "Completed",
  "Cancelled",
];

  const navigation =
    useNavigation<
      NativeStackNavigationProp<MainStackParamList>
    >();

  const [requests, setRequests] =
    useState<ServiceRequest[]>([]);

  const [loading, setLoading] =
    useState(true);

  const [refreshing, setRefreshing] =
    useState(false);

  const [selectedFilter, setSelectedFilter] =
  useState("All");

  const filteredRequests =
  selectedFilter === "All"
    ? requests
    : requests.filter(
        (request) =>
          request.status.toLowerCase() ===
          selectedFilter.toLowerCase()
      );

  const requestSections = [
  {
    title: "New Requests",
    status: "New",
    data: filteredRequests.filter(
      (request) => request.status === "New"
    ),
  },
  {
    title: "Accepted Requests",
    status: "Accepted",
    data: filteredRequests.filter(
      (request) => request.status === "Accepted"
    ),
  },
  {
    title: "Completed Requests",
    status: "Completed",
    data: filteredRequests.filter(
      (request) => request.status === "Completed"
    ),
  },
  {
    title: "Cancelled Requests",
    status: "Cancelled",
    data: filteredRequests.filter(
      (request) => request.status === "Cancelled"
    ),
  },
];


  const loadRequests = useCallback(async () => {
    try {
      const data =
  await serviceRequestService.getMyProviderRequests();

  console.log("Provider Requests:", data);
  

setRequests(data);
    } catch (error) {
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

    <View style={styles.header}>
      <Pressable onPress={() => navigation.goBack()}>
        <Ionicons
          name="arrow-back"
          size={24}
          color="#111827"
        />
      </Pressable>

      <Text style={styles.headerTitle}>
        Recent Requests
      </Text>

      <Pressable>
        <Ionicons
          name="search-outline"
          size={22}
          color="#111827"
        />
      </Pressable>
    </View>

    {/* Filter Chips */}
    <ScrollView
      horizontal
      showsHorizontalScrollIndicator={false}
      contentContainerStyle={styles.filterContainer}
    >
      {filters.map((filter, index) => (
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

renderSectionHeader={({ section }) => (
  <View style={styles.sectionHeader}>
    <Text style={styles.sectionTitle}>
      {section.title} ({section.data.length})
    </Text>

    <TouchableOpacity
  onPress={() => setSelectedFilter(section.status)}
>
  <Text style={styles.viewAllText}>
    View All
  </Text>
</TouchableOpacity>
  </View>
)}

renderItem={({ item }) => (
  <TouchableOpacity
    style={styles.requestCard}
    activeOpacity={0.85}
    onPress={() =>
      navigation.navigate("RequestDetails", {
        requestId: item.id,
      })
    }
  >
    <View style={styles.cardHeader}>

  <View style={styles.serviceIcon}>
    <Ionicons
  name={getServiceIcon(item.title)}
  size={22}
  color="#16A34A"
/>
  </View>

  <View style={styles.locationRow}>
  <Ionicons
    name="location-outline"
    size={16}
    color="#6B7280"
  />

  <Text
    style={styles.locationText}
    numberOfLines={1}
  >
    Location unavailable
  </Text>
</View>

<View style={styles.footerRow}>
  <View style={styles.timeRow}>
    <Ionicons
      name="time-outline"
      size={16}
      color="#6B7280"
    />

    <Text style={styles.timeText}>
      {new Date(item.requestedDate).toLocaleDateString()}
    </Text>
  </View>

  <Ionicons
    name="chevron-forward"
    size={20}
    color="#9CA3AF"
  />
</View>

  <View style={styles.serviceInfo}>
    <Text
      style={styles.serviceTitle}
      numberOfLines={1}
    >
      {item.title}
    </Text>

    <Text
      style={styles.serviceSubtitle}
      numberOfLines={2}
    >
      {item.description}
    </Text>
  </View>

  <View
    style={[
      styles.statusBadge,
      item.status === "New"
        ? styles.newBadge
        : item.status === "Accepted"
        ? styles.acceptedBadge
        : item.status === "Completed"
        ? styles.completedBadge
        : styles.cancelledBadge,
    ]}
  >
        <Text style={styles.statusBadgeText}>
      {item.status}
    </Text>
  </View>

</View>

</TouchableOpacity>
)}

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
    padding: 16,
  },

  center: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    padding: 24,
  },

  card: {
    backgroundColor: "#fff",
    borderRadius: 12,
    padding: 16,
    marginBottom: 12,
    elevation: 2,
  },

  title: {
    fontSize: 18,
    fontWeight: "700",
  },

  status: {
    marginTop: 8,
    fontWeight: "600",
    color: "#2563EB",
  },

  description: {
    marginTop: 8,
    color: "#666",
  },

  date: {
    marginTop: 12,
    color: "#999",
    fontSize: 12,
  },

  emptyTitle: {
    fontSize: 20,
    fontWeight: "700",
  },

  emptyText: {
    marginTop: 8,
    color: "#666",
    textAlign: "center",
  },

  header: {
  height: 56,
  flexDirection: "row",
  alignItems: "center",
  justifyContent: "space-between",
  paddingHorizontal: 16,
},

headerTitle: {
  fontSize: 20,
  fontWeight: "700",
  color: "#111827",
},

filterContainer: {
  paddingHorizontal: 16,
  paddingVertical: 12,
},

filterChip: {
  height: 34,
  paddingHorizontal: 16,
  borderRadius: 17,
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
  color: "#64748B",
  fontSize: 13,
  fontWeight: "600",
},

activeFilterText: {
  color: "#FFFFFF",
},

requestCard: {
  backgroundColor: "#FFFFFF",
  borderRadius: 20,
  padding: 18,
  marginHorizontal: 16,
  marginBottom: 16,

  borderWidth: 1,
  borderColor: "#F1F5F9",

  elevation: 3,
},

cardHeader: {
  flexDirection: "row",
  alignItems: "flex-start",
},

serviceIcon: {
  width: 56,
  height: 56,
  borderRadius: 28,
  backgroundColor: "#ECFDF5",
  justifyContent: "center",
  alignItems: "center",
  marginRight: 16,
},

serviceInfo: {
  flex: 1,
},

serviceTitle: {
  fontSize: 17,
  fontWeight: "700",
  color: "#111827",
},

serviceSubtitle: {
  marginTop: 4,
  fontSize: 14,
  lineHeight: 20,
  color: "#6B7280",
},

statusBadge: {
  paddingHorizontal: 12,
  paddingVertical: 6,
  borderRadius: 16,
  alignSelf: "flex-start",
},

statusBadgeText: {
  color: "#FFFFFF",
  fontSize: 11,
  fontWeight: "700",
  letterSpacing: 0.5,
  textTransform: "uppercase",
},

newBadge: {
  backgroundColor: "#16A34A",
},

acceptedBadge: {
  backgroundColor: "#2563EB",
},

completedBadge: {
  backgroundColor: "#059669",
},

cancelledBadge: {
  backgroundColor: "#DC2626",
},

locationRow: {
  flexDirection: "row",
  alignItems: "center",
  marginTop: 14,
},

locationText: {
  marginLeft: 6,
  flex: 1,
  color: "#6B7280",
  fontSize: 13,
},

footerRow: {
  flexDirection: "row",
  justifyContent: "space-between",
  alignItems: "center",
  marginTop: 16,
  paddingTop: 14,
  borderTopWidth: 1,
  borderTopColor: "#F3F4F6",
},

timeRow: {
  flexDirection: "row",
  alignItems: "center",
},

timeText: {
  marginLeft: 6,
  color: "#6B7280",
  fontSize: 13,
},

sectionHeader: {
  flexDirection: "row",
  justifyContent: "space-between",
  alignItems: "center",
  marginHorizontal: 16,
  marginBottom: 14,
  marginTop: 8,
},

sectionTitle: {
  fontSize: 20,
  fontWeight: "700",
  color: "#111827",
},

viewAllText: {
  color: "#16A34A",
  fontSize: 14,
  fontWeight: "600",
},


});