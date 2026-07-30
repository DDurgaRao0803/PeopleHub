import React from "react";
import { StyleSheet, Switch, Text, View } from "react-native";

type AvailabilityCardProps = {
  acceptingRequests: boolean;
  onToggle: (value: boolean) => void;
};

export default function AvailabilityCard({
  acceptingRequests,
  onToggle,
}: AvailabilityCardProps): React.JSX.Element {
  return (
    <View style={styles.card}>
      <View>
        <Text style={styles.title}>Accepting Requests</Text>

        <Text style={styles.subtitle}>
          Customers can send you new jobs.
        </Text>
      </View>

      <Switch
        value={acceptingRequests}
        onValueChange={onToggle}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  card: {
    backgroundColor: "#FFFFFF",
    borderRadius: 16,
    padding: 20,
    marginBottom: 24,
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",

    shadowColor: "#000",
    shadowOpacity: 0.08,
    shadowRadius: 8,
    shadowOffset: {
      width: 0,
      height: 2,
    },

    elevation: 3,
  },

  title: {
    fontSize: 18,
    fontWeight: "700",
    color: "#1F2937",
  },

  subtitle: {
    marginTop: 4,
    fontSize: 14,
    color: "#6B7280",
  },
});