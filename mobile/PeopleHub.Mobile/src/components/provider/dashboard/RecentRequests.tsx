import React from "react";
import {
  Pressable,
  StyleSheet,
  Text,
  View,
} from "react-native";

import { Ionicons } from "@expo/vector-icons";

import {
  colors,
  radius,
  shadows,
  spacing,
  typography,
} from "../../../theme";

type RequestStatus =
  | "New"
  | "Accepted"
  | "Completed";

type RecentRequest = {
  id: string;
  service: string;
  location: string;
  time: string;
  status: RequestStatus;
};

const requests: RecentRequest[] = [
  {
    id: "1",
    service: "Plumbing Service",
    location: "Riyadh",
    time: "10 mins ago",
    status: "New",
  },
  {
    id: "2",
    service: "AC Repair",
    location: "Al Malaz",
    time: "Yesterday",
    status: "Accepted",
  },
];

const getBadgeColor = (status: RequestStatus) => {
  switch (status) {
    case "New":
      return "#EAF8EE";

    case "Accepted":
      return "#EEF5FF";

    case "Completed":
      return "#F3F4F6";
  }
};

const getTextColor = (status: RequestStatus) => {
  switch (status) {
    case "New":
      return "#16A34A";

    case "Accepted":
      return "#2563EB";

    case "Completed":
      return "#6B7280";
  }
};

export default function RecentActivity(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.heading}>
          Recent Requests
        </Text>

        <Pressable>
          <Text style={styles.viewAll}>
            View All &gt;
          </Text>
        </Pressable>
      </View>

      {requests.map((request) => (
        <Pressable
          key={request.id}
          style={styles.card}
        >
          <View style={styles.left}>
            <Text style={styles.service}>
              {request.service}
            </Text>

            <View style={styles.locationRow}>
              <Ionicons
                name="location-outline"
                size={14}
                color={colors.text.secondary}
              />

              <Text style={styles.location}>
                {request.location}
              </Text>
            </View>

            <Text style={styles.time}>
              {request.time}
            </Text>
          </View>

          <View style={styles.right}>
            <View
              style={[
                styles.badge,
                {
                  backgroundColor: getBadgeColor(request.status),
                },
              ]}
            >
              <Text
                style={[
                  styles.badgeText,
                  {
                    color: getTextColor(request.status),
                  },
                ]}
              >
                {request.status}
              </Text>
            </View>

            <Ionicons
              name="chevron-forward"
              size={18}
              color="#9CA3AF"
            />
          </View>
        </Pressable>
      ))}
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: spacing.xxxl,
  },

  header: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: spacing.lg,
  },

  heading: {
    ...typography.h3,
    color: colors.text.primary,
  },

  viewAll: {
    ...typography.body,
    color: colors.primary,
    fontWeight: "600",
  },

  card: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",

    backgroundColor: colors.surface,

    borderRadius: radius.xl,

    padding: spacing.lg,

    marginBottom: spacing.md,

    ...shadows.sm,
  },

  left: {
    flex: 1,
  },

  service: {
    ...typography.subtitle,
    color: colors.text.primary,
    marginBottom: spacing.xs,
  },

  locationRow: {
    flexDirection: "row",
    alignItems: "center",
  },

  location: {
    ...typography.body,
    color: colors.text.secondary,
    marginLeft: spacing.xs,
  },

  time: {
    ...typography.caption,
    color: colors.text.secondary,
    marginTop: spacing.sm,
  },

  right: {
    alignItems: "flex-end",
  },

  badge: {
    paddingHorizontal: spacing.md,
    paddingVertical: spacing.xs,
    borderRadius: 20,
    marginBottom: spacing.md,
  },

  badgeText: {
    fontSize: 12,
    fontWeight: "700",
  },
});