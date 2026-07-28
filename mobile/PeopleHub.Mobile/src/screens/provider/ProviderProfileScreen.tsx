import React, { useState } from "react";
import {
  ActivityIndicator,
  Alert,
  SafeAreaView,
  ScrollView,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
} from "react-native";
import { providerService } from "../../services/providerService";
import { colors } from "../../theme/colors";

export function ProviderProfileScreen({ navigation }: any): React.JSX.Element {
  const [bio, setBio] = useState("");
  const [experienceYears, setExperienceYears] = useState("");
  const [loading, setLoading] = useState(false);

  const handleCreateProfile = async () => {
    if (!bio.trim()) {
      Alert.alert("Validation", "Please enter your bio.");
      return;
    }

    const years = Number(experienceYears);

    if (isNaN(years) || years < 0) {
      Alert.alert("Validation", "Please enter valid experience.");
      return;
    }

    try {
      setLoading(true);

      await providerService.createProfile({
        bio: bio.trim(),
        experienceYears: years,
      });

      Alert.alert(
        "Success",
        "Provider profile created successfully."
      );

      // Uncomment after ProviderServices screen
      // navigation.navigate("ProviderServices");
    } catch (error: any) {
      Alert.alert(
        "Error",
        error?.response?.data?.message ??
          "Unable to create provider profile."
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <ScrollView
        contentContainerStyle={styles.content}
        keyboardShouldPersistTaps="handled"
      >
        <Text style={styles.title}>Become a Provider</Text>

        <Text style={styles.label}>Bio</Text>

        <TextInput
          style={[styles.input, styles.bioInput]}
          placeholder="Tell customers about yourself..."
          placeholderTextColor={colors.text.secondary}
          multiline
          value={bio}
          onChangeText={setBio}
        />

        <Text style={styles.label}>Years of Experience</Text>

        <TextInput
          style={styles.input}
          placeholder="0"
          placeholderTextColor={colors.text.secondary}
          keyboardType="numeric"
          value={experienceYears}
          onChangeText={setExperienceYears}
        />

        <TouchableOpacity
          style={styles.button}
          disabled={loading}
          onPress={handleCreateProfile}
        >
          {loading ? (
            <ActivityIndicator color={colors.text.inverse} />
          ) : (
            <Text style={styles.buttonText}>
              Continue
            </Text>
          )}
        </TouchableOpacity>

        <TouchableOpacity
  style={[
    styles.button,
    {
      backgroundColor: "#16A34A",
      marginTop: 12,
    },
  ]}
  onPress={() => navigation.navigate("ProviderRequests")}
>
  <Text style={styles.buttonText}>
    My Provider Requests
  </Text>
</TouchableOpacity>


      </ScrollView>
    </SafeAreaView>
  );

  
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.background,
  },

  content: {
    padding: 20,
  },

  title: {
    fontSize: 28,
    fontWeight: "700",
    marginBottom: 24,
    color: colors.text.primary,
  },

  label: {
    fontSize: 16,
    fontWeight: "600",
    marginBottom: 8,
    color: colors.text.primary,
  },

  input: {
    borderWidth: 1,
    borderColor: colors.border,
    borderRadius: 8,
    padding: 12,
    marginBottom: 20,
    backgroundColor: colors.surface,
    color: colors.text.primary,
  },

  bioInput: {
    minHeight: 120,
    textAlignVertical: "top",
  },

  button: {
    backgroundColor: colors.primary,
    padding: 16,
    borderRadius: 8,
    alignItems: "center",
    marginTop: 8,
  },

  buttonText: {
    color: colors.text.inverse,
    fontWeight: "700",
    fontSize: 16,
  },
});