import React from "react";
import {
  Pressable,
  StyleSheet,
  Text,
  View,
} from "react-native";

type QuickAction = {
  title: string;
  onPress: () => void;
};

type QuickActionsProps = {
  actions: QuickAction[];
};

export default function QuickActions({
  actions,
}: QuickActionsProps): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text style={styles.heading}>Quick Actions</Text>

      <View style={styles.grid}>
        {actions.map((action) => (
          <Pressable
            key={action.title}
            style={styles.card}
            onPress={action.onPress}
          >
            <Text style={styles.cardTitle}>{action.title}</Text>
          </Pressable>
        ))}
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    marginBottom: 24,
  },

  heading: {
    fontSize: 20,
    fontWeight: "700",
    color: "#1F2937",
    marginBottom: 16,
  },

  grid: {
    flexDirection: "row",
    flexWrap: "wrap",
    justifyContent: "space-between",
  },

  card: {
    width: "48%",
    backgroundColor: "#FFFFFF",
    borderRadius: 16,
    paddingVertical: 20,
    alignItems: "center",
    marginBottom: 12,

    shadowColor: "#000",
    shadowOpacity: 0.08,
    shadowRadius: 8,
    shadowOffset: {
      width: 0,
      height: 2,
    },

    elevation: 3,
  },

  cardTitle: {
    fontSize: 16,
    fontWeight: "600",
    color: "#1F2937",
    textAlign: "center",
  },
});