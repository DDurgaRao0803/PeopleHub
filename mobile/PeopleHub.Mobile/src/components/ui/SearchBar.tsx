import React from "react";

import {
  StyleSheet,
  TextInput,
  View,
} from "react-native";

import Ionicons from "@expo/vector-icons/Ionicons";

import { colors } from "../../theme/colors";
import { radius } from "../../theme/radius";
import { shadows } from "../../theme/shadows";

interface SearchBarProps {
  value: string;
  placeholder?: string;
  onChangeText: (text: string) => void;
}

export function SearchBar({
  value,
  placeholder = "Search...",
  onChangeText,
}: SearchBarProps): React.JSX.Element {

  return (
    <View style={styles.container}>

      <Ionicons
        name="search"
        size={20}
        color={colors.text.secondary}
      />

      <TextInput
        value={value}
        placeholder={placeholder}
        placeholderTextColor={colors.text.secondary}
        onChangeText={onChangeText}
        style={styles.input}
        returnKeyType="search"
      />

    </View>
  );
}

const styles = StyleSheet.create({

  container: {

    flexDirection: "row",
    alignItems: "center",

    backgroundColor: colors.surface,

    borderRadius: radius.xl,

    borderWidth: 1,
    borderColor: colors.border,

    paddingHorizontal: 16,

    height: 54,

    ...shadows.sm,

  },

  input: {

    flex: 1,

    marginLeft: 10,

    fontSize: 16,

    color: colors.text.primary,

  },

});