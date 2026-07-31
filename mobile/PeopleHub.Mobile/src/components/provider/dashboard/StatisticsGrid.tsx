import React from "react";
import { StyleSheet, Text, View } from "react-native";
import { Ionicons } from "@expo/vector-icons";

type Props = {
  completedJobs: number;
  pendingRequests: number;
  earnings: number;
  rating: number;
};

const getCards = (
  completedJobs: number,
  pendingRequests: number,
  earnings: number,
  rating: number,
) => [
  {
    title: "Rating",
    value: rating.toFixed(1),
    subtitle: "(128 reviews)",
    icon: "star",
    color: "#FFF7E8",
    iconColor: "#FBBF24",
  },
  {
    title: "Jobs Completed",
    value: completedJobs.toString(),
    subtitle: "This Month",
    icon: "briefcase",
    color: "#EEF5FF",
    iconColor: "#3B82F6",
  },
  {
    title: "Pending Requests",
    value: pendingRequests.toString(),
    subtitle: "New",
    icon: "time",
    color: "#FFF4EB",
    iconColor: "#FB923C",
  },
  {
    title: "Total Earnings",
    value: `₹${earnings.toLocaleString()}`,
    subtitle: "This Month",
    icon: "wallet",
    color: "#F3EEFF",
    iconColor: "#6366F1",
  },
];

export default function StatisticsGrid({
  completedJobs,
  pendingRequests,
  earnings,
  rating,
}: Props) {
  return (
    <View style={styles.row}>
      {getCards(
        completedJobs,
        pendingRequests,
        earnings,
        rating,
      ).map((card) => (
        <View
          key={card.title}
          style={[
            styles.card,
            { backgroundColor: card.color },
          ]}
        >
          <Ionicons
            name={card.icon as any}
            size={28}
            color={card.iconColor}
          />

          <Text style={styles.title}>
            {card.title}
          </Text>

          <Text style={styles.value}>
            {card.value}
          </Text>

          <Text style={styles.subtitle}>
            {card.subtitle}
          </Text>
        </View>
      ))}
    </View>
  );
}

const styles = StyleSheet.create({
  row: {
  flexDirection: "row",
  flexWrap: "wrap",
  justifyContent: "space-between",
  marginBottom: 28,
},

  card: {
  width: "48%",
  borderRadius: 20,
  paddingVertical: 22,
  paddingHorizontal: 12,
  marginBottom: 14,
  alignItems: "center",
},

  title: {
  marginTop: 10,
  fontSize: 15,
  fontWeight: "600",
  color: "#374151",
  textAlign: "center",
},

  value: {
    marginTop: 12,
    fontSize: 24,
    fontWeight: "700",
    color: "#111827",
  },

  subtitle: {
  marginTop: 8,
  fontSize: 13,
  color: "#6B7280",
  textAlign: "center",
},
});