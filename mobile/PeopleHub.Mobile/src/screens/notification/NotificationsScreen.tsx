import React from "react";
import {
  SafeAreaView,
  StyleSheet,
  Text,
} from "react-native";

export function NotificationsScreen(): React.JSX.Element {
  return (
    <SafeAreaView style={styles.container}>
      <Text>Notifications Screen</Text>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
});