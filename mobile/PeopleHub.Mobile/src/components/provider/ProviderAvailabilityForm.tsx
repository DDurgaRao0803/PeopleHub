import React, { useEffect, useState } from "react";
import {
  ActivityIndicator,
  Button,
  StyleSheet,
  Text,
  TextInput,
  View,
} from "react-native";

import {
  CreateProviderAvailabilityRequest,
  ProviderAvailability,
} from "../../types";

interface ProviderAvailabilityFormProps {
  initialValues?: ProviderAvailability;
  loading?: boolean;
  onSubmit: (
    request: CreateProviderAvailabilityRequest
  ) => Promise<void>;
}

const days = [
  { label: "Sunday", value: 0 },
  { label: "Monday", value: 1 },
  { label: "Tuesday", value: 2 },
  { label: "Wednesday", value: 3 },
  { label: "Thursday", value: 4 },
  { label: "Friday", value: 5 },
  { label: "Saturday", value: 6 },
];

export function ProviderAvailabilityForm({
  initialValues,
  loading,
  onSubmit,
}: ProviderAvailabilityFormProps) {
  const [dayOfWeek, setDayOfWeek] = useState(0);
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");

  const [error, setError] = useState("");

  useEffect(() => {
    if (!initialValues) {
      return;
    }

    setDayOfWeek(initialValues.dayOfWeek);
    setStartTime(initialValues.startTime);
    setEndTime(initialValues.endTime);
  }, [initialValues]);

  const submit = async () => {
    setError("");

    if (!startTime.trim()) {
      setError("Start time is required.");
      return;
    }

    if (!endTime.trim()) {
      setError("End time is required.");
      return;
    }

    if (endTime <= startTime) {
      setError(
        "End time must be after start time."
      );
      return;
    }

    await onSubmit({
      dayOfWeek,
      startTime,
      endTime,
    });
  };

  if (loading) {
    return <ActivityIndicator size="large" />;
  }

  return (
    <View style={styles.container}>
      <Text style={styles.label}>
        Day Of Week
      </Text>

      {days.map((day) => (
        <Button
          key={day.value}
          title={
            day.value === dayOfWeek
              ? `✓ ${day.label}`
              : day.label
          }
          onPress={() =>
            setDayOfWeek(day.value)
          }
        />
      ))}

      <Text style={styles.label}>
        Start Time (HH:mm)
      </Text>

      <TextInput
        style={styles.input}
        value={startTime}
        onChangeText={setStartTime}
        placeholder="09:00"
      />

      <Text style={styles.label}>
        End Time (HH:mm)
      </Text>

      <TextInput
        style={styles.input}
        value={endTime}
        onChangeText={setEndTime}
        placeholder="18:00"
      />

      {!!error && (
        <Text style={styles.error}>
          {error}
        </Text>
      )}

      <Button
        title="Save Availability"
        onPress={submit}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    padding: 16,
  },

  label: {
    marginTop: 16,
    marginBottom: 6,
    fontWeight: "600",
  },

  input: {
    borderWidth: 1,
    borderColor: "#ccc",
    borderRadius: 8,
    padding: 12,
  },

  error: {
    color: "red",
    marginVertical: 12,
  },
});