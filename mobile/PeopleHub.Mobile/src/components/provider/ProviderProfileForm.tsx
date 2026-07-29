import React, { useEffect, useState } from "react";
import {
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";

import type {
  ProviderProfile,
  CreateProviderProfileRequest,
  UpdateProviderProfileRequest,
} from "../../api/providerApi";

type FormData =
  | CreateProviderProfileRequest
  | UpdateProviderProfileRequest;

interface ProviderProfileFormProps {
  initialValues?: ProviderProfile;
  loading?: boolean;
  onSubmit: (values: FormData) => void;
}

export function ProviderProfileForm({
  initialValues,
  loading = false,
  onSubmit,
}: ProviderProfileFormProps): React.JSX.Element {
  const [bio, setBio] = useState("");
  const [experienceYears, setExperienceYears] = useState("");

  useEffect(() => {
    if (initialValues) {
      setBio(initialValues.bio);
      setExperienceYears(
        initialValues.experienceYears.toString()
      );
    }
  }, [initialValues]);

  const handleSubmit = () => {
  console.log("ProviderProfileForm: Save pressed");

  onSubmit({
    bio: bio.trim(),
    experienceYears: Number(experienceYears),
  });
};

  return (
    <View>
      <Text style={styles.label}>Bio</Text>

      <TextInput
        style={styles.input}
        value={bio}
        onChangeText={setBio}
        placeholder="Tell customers about yourself"
        multiline
      />

      <Text style={styles.label}>
        Experience (Years)
      </Text>

      <TextInput
        style={styles.input}
        value={experienceYears}
        onChangeText={setExperienceYears}
        keyboardType="numeric"
        placeholder="0"
      />

      <TouchableOpacity
        style={styles.button}
        disabled={loading}
        onPress={handleSubmit}
      >
        <Text style={styles.buttonText}>
          {loading ? "Saving..." : "Save"}
        </Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  label: {
    marginBottom: 6,
    fontWeight: "600",
  },

  input: {
    borderWidth: 1,
    borderColor: "#D1D5DB",
    borderRadius: 8,
    padding: 12,
    marginBottom: 16,
  },

  button: {
    marginTop: 10,
    backgroundColor: "#2563EB",
    padding: 16,
    borderRadius: 10,
    alignItems: "center",
  },

  buttonText: {
    color: "#FFFFFF",
    fontWeight: "700",
    fontSize: 16,
  },
});