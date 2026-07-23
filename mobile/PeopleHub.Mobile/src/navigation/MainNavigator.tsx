import React from "react";
import {
  StyleSheet,
  Text,
  View,
} from "react-native";

export function MainNavigator(): React.JSX.Element {
  return (
    <View style={styles.container}>
      <Text>PeopleHub Home</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
  },
});