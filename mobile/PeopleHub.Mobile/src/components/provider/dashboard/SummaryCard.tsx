import React from "react";
import { StyleSheet, Text, View } from "react-native";

type SummaryCardProps = {
  title: string;
  value: string;
};

export default function SummaryCard({
  title,
  value,
}: SummaryCardProps): React.JSX.Element {
  return (
    <View style={styles.card}>
      <Text style={styles.title}>{title}</Text>

      <Text style={styles.value}>{value}</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  card: {
    flex: 1,
    backgroundColor: "#FFFFFF",
    borderRadius: 16,
    padding: 18,
    marginHorizontal: 4,

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
    fontSize: 14,
    color: "#6B7280",
    marginBottom: 8,
  },

  value: {
    fontSize: 22,
    fontWeight: "700",
    color: "#1F2937",
  },
});