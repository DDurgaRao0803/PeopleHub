import React, { useEffect, useState } from "react";
import {
  StyleSheet,
  Switch,
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
  const [acceptingRequests, setAcceptingRequests] =
  useState(true);

  useEffect(() => {
    if (initialValues) {
      setBio(initialValues.bio);
      setExperienceYears(
        initialValues.experienceYears.toString()
      );
      setAcceptingRequests(
  initialValues.acceptingRequests
);

    }
  }, [initialValues]);

  const handleSubmit = () => {

  onSubmit({
  bio: bio.trim(),
  experienceYears: Number(experienceYears),
  acceptingRequests,
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

      <View style={styles.switchContainer}>
  <View style={{ flex: 1 }}>
    <Text style={styles.label}>
      Receive New Requests
    </Text>

    <Text style={styles.switchDescription}>
      Turn this off to temporarily stop receiving
      new service requests.
    </Text>
  </View>

  <TouchableOpacity
  style={{
    backgroundColor: acceptingRequests ? "#22C55E" : "#EF4444",
    paddingHorizontal: 20,
    paddingVertical: 10,
    borderRadius: 8,
  }}
  onPress={() => {
    const newValue = !acceptingRequests;

    setAcceptingRequests(newValue);
  }}
>
  <Text style={{ color: "#fff", fontWeight: "700" }}>
    {acceptingRequests ? "ON" : "OFF"}
  </Text>
</TouchableOpacity>
</View>

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

  switchContainer: {
  flexDirection: "row",
  justifyContent: "space-between",
  alignItems: "center",
  marginBottom: 20,
},

switchDescription: {
  fontSize: 12,
  color: "#666",
  marginTop: 4,
},
});