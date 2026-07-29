import React, { useEffect, useState } from "react";

import {
  ActivityIndicator,
  Alert,
  Button,
  StyleSheet,
  Text,
  TextInput,
  View,
} from "react-native";

import { Picker } from "@react-native-picker/picker";

import { serviceCategoriesApi } from "../../api/serviceCategoriesApi";

import type {
  ServiceCategory,
} from "../../types";

import type {
  CreateProviderServiceRequest,
} from "../../types/provider";

interface ProviderServiceFormProps {
  providerProfileId: string;

  initialValues?: Partial<CreateProviderServiceRequest>;

  submitText: string;

  onSubmit(
    request: CreateProviderServiceRequest
  ): Promise<void>;
}

export function ProviderServiceForm({
  providerProfileId,
  initialValues,
  submitText,
  onSubmit,
}: ProviderServiceFormProps): React.JSX.Element {

  const [categories, setCategories] =
    useState<ServiceCategory[]>([]);

  const [loadingCategories, setLoadingCategories] =
    useState(true);

  const [serviceCategoryId, setServiceCategoryId] =
    useState(
      initialValues?.serviceCategoryId ?? ""
    );

  const [title, setTitle] =
    useState(initialValues?.title ?? "");

  const [description, setDescription] =
    useState(initialValues?.description ?? "");

  const [basePrice, setBasePrice] =
    useState(
      initialValues?.basePrice?.toString() ?? ""
    );

  const [duration, setDuration] =
    useState(
      initialValues?.estimatedDurationMinutes?.toString() ?? ""
    );

  useEffect(() => {
    const loadCategories = async () => {
      try {
        const data =
          await serviceCategoriesApi.getAll();

        setCategories(data);

        if (
          !serviceCategoryId &&
          data.length > 0
        ) {
          setServiceCategoryId(data[0].id);
        }
      } catch {
        Alert.alert(
          "Error",
          "Unable to load categories."
        );
      } finally {
        setLoadingCategories(false);
      }
    };

    void loadCategories();
  }, []);

  const submit = async () => {
    if (
      !serviceCategoryId ||
      !title.trim() ||
      !description.trim()
    ) {
      Alert.alert(
        "Validation",
        "Please complete all required fields."
      );

      return;
    }

    const price =
      Number(basePrice);

    const estimatedDuration =
      Number(duration);

    if (
      Number.isNaN(price) ||
      price <= 0
    ) {
      Alert.alert(
        "Validation",
        "Base price must be greater than zero."
      );

      return;
    }

    if (
      Number.isNaN(
        estimatedDuration
      ) ||
      estimatedDuration <= 0
    ) {
      Alert.alert(
        "Validation",
        "Duration must be greater than zero."
      );

      return;
    }

    await onSubmit({
      providerProfileId,
      serviceCategoryId,
      title,
      description,
      basePrice: price,
      estimatedDurationMinutes:
        estimatedDuration,
    });
  };

  return (
    <View>

      <Text style={styles.label}>
        Service Category
      </Text>

      {loadingCategories ? (
        <ActivityIndicator />
      ) : (
        <View style={styles.picker}>
          <Picker
            selectedValue={
              serviceCategoryId
            }
            onValueChange={(value) =>
              setServiceCategoryId(value)
            }
          >
            {categories.map((item) => (
              <Picker.Item
                key={item.id}
                label={item.name}
                value={item.id}
              />
            ))}
          </Picker>
        </View>
      )}

      <TextInput
        style={styles.input}
        placeholder="Title"
        value={title}
        onChangeText={setTitle}
      />

      <TextInput
        style={styles.textArea}
        multiline
        numberOfLines={4}
        placeholder="Description"
        value={description}
        onChangeText={setDescription}
      />

      <TextInput
        style={styles.input}
        placeholder="Base Price"
        keyboardType="numeric"
        value={basePrice}
        onChangeText={setBasePrice}
      />

      <TextInput
        style={styles.input}
        placeholder="Estimated Duration (Minutes)"
        keyboardType="numeric"
        value={duration}
        onChangeText={setDuration}
      />

      <Button
        title={submitText}
        onPress={() => {
          void submit();
        }}
      />

    </View>
  );
}

const styles = StyleSheet.create({

  label: {
    fontSize: 16,
    fontWeight: "600",
    marginBottom: 8,
  },

  picker: {
    borderWidth: 1,
    borderColor: "#DDD",
    borderRadius: 8,
    marginBottom: 16,
    overflow: "hidden",
  },

  input: {
    borderWidth: 1,
    borderColor: "#DDD",
    borderRadius: 8,
    padding: 12,
    marginBottom: 16,
  },

  textArea: {
    borderWidth: 1,
    borderColor: "#DDD",
    borderRadius: 8,
    padding: 12,
    minHeight: 120,
    textAlignVertical: "top",
    marginBottom: 16,
  },

});