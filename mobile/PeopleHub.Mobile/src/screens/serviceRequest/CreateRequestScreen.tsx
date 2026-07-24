import React, { useEffect, useState } from "react";
import {
  ActivityIndicator,
  Alert,
  Button,
  SafeAreaView,
  StyleSheet,
  Text,
  TextInput,
  View,
} from "react-native";

import { Picker } from "@react-native-picker/picker";
import {
  useNavigation,
  useRoute,
} from "@react-navigation/native";

import type {
  NativeStackNavigationProp,
} from "@react-navigation/native-stack";
import type {
  RouteProp,
} from "@react-navigation/native";

import {
  serviceCategoryService,
  serviceRequestService,
} from "../../services";

import type {
  ServiceCategory,
} from "../../types";

import type {
  MainStackParamList,
} from "../../navigation/MainStackNavigator";

export function CreateRequestScreen(): React.JSX.Element {
  const navigation =
    useNavigation<
      NativeStackNavigationProp<MainStackParamList>
    >();

  const route =
    useRoute<
      RouteProp<
        MainStackParamList,
        "CreateRequest"
      >
    >();

  const selectedCategoryId =
    route.params?.categoryId;

  const selectedCategoryName =
    route.params?.categoryName;

  const [categories, setCategories] = useState<ServiceCategory[]>([]);
  const [serviceCategoryId, setServiceCategoryId] = useState("");
  const [loadingCategories, setLoadingCategories] = useState(true);

  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");

  useEffect(() => {
    const loadCategories = async () => {
      try {
        const result =
          await serviceCategoryService.getCategories();

        setCategories(result);

        if (selectedCategoryId) {
          setServiceCategoryId(selectedCategoryId);
        } else if (result.length > 0) {
          setServiceCategoryId(result[0].id);
        }
      } catch {
        Alert.alert(
          "Error",
          "Unable to load service categories."
        );
      } finally {
        setLoadingCategories(false);
      }
    };

    void loadCategories();
  }, [selectedCategoryId]);

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

    try {
      await serviceRequestService.createRequest({
        serviceCategoryId,
        title,
        description,
        requestedDate: new Date().toISOString(),
      });

      setTitle("");
setDescription("");

if (!selectedCategoryId && categories.length > 0) {
  setServiceCategoryId(categories[0].id);
}

navigation.navigate("MainTabs", {
  screen: "Requests",
});
    } catch {
      Alert.alert(
        "Error",
        "Unable to create service request."
      );
    }
  };

  return (
    <SafeAreaView style={styles.container}>
      <Text style={styles.heading}>
        Create Service Request
      </Text>

      <Text style={styles.label}>
        Service Category
      </Text>

      {selectedCategoryId ? (
        <View style={styles.selectedCategory}>
          <Text style={styles.selectedCategoryText}>
            {selectedCategoryName}
          </Text>
        </View>
      ) : loadingCategories ? (
        <ActivityIndicator size="small" />
      ) : (
        <View style={styles.pickerContainer}>
          <Picker
            selectedValue={serviceCategoryId}
            onValueChange={(value) =>
              setServiceCategoryId(value)
            }
          >
            {categories.map((category) => (
              <Picker.Item
                key={category.id}
                label={category.name}
                value={category.id}
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
        placeholder="Description"
        value={description}
        multiline
        numberOfLines={4}
        onChangeText={setDescription}
      />

      <View style={styles.button}>
        <Button
          title="Create Request"
          onPress={() => {
            void submit();
          }}
        />
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
  },

  heading: {
    fontSize: 22,
    fontWeight: "700",
    marginBottom: 20,
  },

  label: {
    fontSize: 16,
    fontWeight: "600",
    marginBottom: 8,
  },

  pickerContainer: {
    borderWidth: 1,
    borderColor: "#ddd",
    borderRadius: 8,
    marginBottom: 16,
    overflow: "hidden",
  },

  selectedCategory: {
    borderWidth: 1,
    borderColor: "#ddd",
    borderRadius: 8,
    padding: 14,
    marginBottom: 16,
    backgroundColor: "#F8F9FB",
  },

  selectedCategoryText: {
    fontSize: 16,
    fontWeight: "600",
    color: "#2563EB",
  },

  input: {
    borderWidth: 1,
    borderColor: "#ddd",
    borderRadius: 8,
    padding: 12,
    marginBottom: 16,
  },

  textArea: {
    borderWidth: 1,
    borderColor: "#ddd",
    borderRadius: 8,
    padding: 12,
    minHeight: 120,
    textAlignVertical: "top",
    marginBottom: 24,
  },

  button: {
    marginTop: 12,
  },
});