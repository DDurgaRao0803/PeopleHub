import React from "react";
import {
  Image,
  StyleSheet,
  Switch,
  Text,
  TouchableOpacity,
  View,
} from "react-native";

type Props = {
  acceptingRequests: boolean;
  todayEarnings: number;
  onToggle: (value: boolean) => void;
};

export default function OnlineStatusCard({
  acceptingRequests,
  todayEarnings,
  onToggle,
}: Props): React.JSX.Element {
  return (
    <View style={styles.card}>
      <View style={styles.left}>

        <View style={styles.statusRow}>
          <View
            style={[
              styles.statusDot,
              {
                backgroundColor: acceptingRequests
                  ? "#22C55E"
                  : "#EF4444",
              },
            ]}
          />

          <Text style={styles.statusTitle}>
            {acceptingRequests
              ? "You are Online"
              : "You are Offline"}
          </Text>
        </View>

        <Text style={styles.subtitle}>
          Customers can send you requests
        </Text>

        <Text style={styles.label}>
          Today's Earnings
        </Text>

        <Text style={styles.earnings}>
          ₹{todayEarnings.toLocaleString()}
        </Text>

        <TouchableOpacity style={styles.button}>
          <Text style={styles.buttonText}>
            Pause Requests
          </Text>
        </TouchableOpacity>

      </View>

      <View style={styles.right}>
        <Switch
          value={acceptingRequests}
          onValueChange={onToggle}
          trackColor={{
            false: "#D1D5DB",
            true: "#22C55E",
          }}
        />

        <Image
          source={{
            uri:
              "https://em-content.zobj.net/source/apple/419/construction-worker_1f477.png",
          }}
          style={styles.image}
        />
      </View>

    </View>
  );
}

const styles = StyleSheet.create({
  card: {
    backgroundColor: "#EEFDF5",
    borderRadius: 28,
    padding: 22,
    flexDirection: "row",
    justifyContent: "space-between",
    marginBottom: 28,
    borderWidth: 1,
    borderColor: "#D7F5E3",
  },

  left: {
    flex: 1,
  },

  right: {
    justifyContent: "space-between",
    alignItems: "flex-end",
  },

  statusRow: {
    flexDirection: "row",
    alignItems: "center",
    marginBottom: 8,
  },

  statusDot: {
    width: 14,
    height: 14,
    borderRadius: 7,
    marginRight: 10,
  },

  statusTitle: {
    fontSize: 20,
    fontWeight: "700",
    color: "#16A34A",
  },

  subtitle: {
    color: "#6B7280",
    fontSize: 15,
    marginBottom: 24,
  },

  label: {
    color: "#374151",
    fontSize: 15,
  },

  earnings: {
    fontSize: 30,
    fontWeight: "700",
    marginTop: 6,
    marginBottom: 22,
    color: "#111827",
  },

  button: {
    backgroundColor: "#CFF7DF",
    alignSelf: "flex-start",
    paddingHorizontal: 20,
    paddingVertical: 12,
    borderRadius: 999,
  },

  buttonText: {
    color: "#047857",
    fontWeight: "700",
    fontSize: 15,
  },

  image: {
    width: 120,
    height: 120,
    marginTop: 20,
  },
});