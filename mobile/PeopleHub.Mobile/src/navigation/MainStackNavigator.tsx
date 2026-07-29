import React from "react";
import {
  createNativeStackNavigator,
} from "@react-navigation/native-stack";

import type { NavigatorScreenParams } from "@react-navigation/native";
import type { MainTabParamList } from "./MainNavigator";

import { MainNavigator } from "./MainNavigator";

import { ProviderAvailability } from "../types";

import {
  CreateRequestScreen,
  RequestDetailsScreen,
} from "../screens/serviceRequest";

import { EditProviderProfileScreen } from "../screens/provider";

import {
  AddProviderServiceScreen,
  EditProviderServiceScreen,
  ProviderProfileScreen,
  ProviderRequestsScreen,
  ProviderServicesScreen,
  AddProviderAvailabilityScreen,
  EditProviderAvailabilityScreen,
} from "../screens/provider";

export type MainStackParamList = {
  MainTabs: NavigatorScreenParams<MainTabParamList>;

  CreateRequest: {
    categoryId?: string;
    categoryName?: string;
  };

  RequestDetails: {
    requestId: string;
  };

  EditProviderService: {
  serviceId: string;
};

  ProviderProfile: undefined;

  ProviderRequests: undefined;

  ProviderServices: undefined;

  AddProviderService: undefined;

  AddProviderAvailability: undefined;

EditProviderAvailability: {
  availabilityId: string;
  availability: ProviderAvailability;
};

EditProviderProfile: undefined;

};

const Stack =
  createNativeStackNavigator<MainStackParamList>();

export function MainStackNavigator(): React.JSX.Element {

  return (
    <Stack.Navigator
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen
  name="MainTabs"
  component={MainNavigator}
/>

      <Stack.Screen
        name="CreateRequest"
        component={CreateRequestScreen}
      />

      <Stack.Screen
        name="RequestDetails"
        component={RequestDetailsScreen}
      />

      <Stack.Screen
        name="ProviderProfile"
        component={ProviderProfileScreen}
      />

      <Stack.Screen
        name="ProviderRequests"
        component={ProviderRequestsScreen}
      />

      <Stack.Screen
        name="ProviderServices"
        component={ProviderServicesScreen}
      />

      <Stack.Screen
  name="AddProviderService"
  component={AddProviderServiceScreen}
/>

<Stack.Screen
  name="EditProviderService"
  component={EditProviderServiceScreen}
/>

<Stack.Screen
  name="AddProviderAvailability"
  component={AddProviderAvailabilityScreen}
/>

<Stack.Screen
  name="EditProviderAvailability"
  component={EditProviderAvailabilityScreen}
/>

<Stack.Screen
  name="EditProviderProfile"
  component={EditProviderProfileScreen}
  options={{ title: "Edit Profile" }}
/>

    </Stack.Navigator>
  );
}