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
    headerShown: true,
  }}
>
      <Stack.Screen
  name="MainTabs"
  component={MainNavigator}
  options={{
    headerShown: false,
  }}
/>

      <Stack.Screen
  name="CreateRequest"
  component={CreateRequestScreen}
  options={{
    title: "Create Request",
  }}
/>

      <Stack.Screen
  name="RequestDetails"
  component={RequestDetailsScreen}
  options={{
    title: "Request Details",
  }}
/>

      <Stack.Screen
  name="ProviderProfile"
  component={ProviderProfileScreen}
  options={{
    title: "Provider Profile",
  }}
/>

      <Stack.Screen
  name="ProviderRequests"
  component={ProviderRequestsScreen}
  options={{
    title: "Requests",
  }}
/>

      <Stack.Screen
  name="ProviderServices"
  component={ProviderServicesScreen}
  options={{
    title: "Services",
  }}
/>

      <Stack.Screen
  name="AddProviderService"
  component={AddProviderServiceScreen}
  options={{
    title: "Add Service",
  }}
/>

<Stack.Screen
  name="EditProviderService"
  component={EditProviderServiceScreen}
  options={{
    title: "Edit Service",
  }}
/>

<Stack.Screen
  name="AddProviderAvailability"
  component={AddProviderAvailabilityScreen}
  options={{
    title: "Add Availability",
  }}
/>

<Stack.Screen
  name="EditProviderAvailability"
  component={EditProviderAvailabilityScreen}
  options={{
    title: "Edit Availability",
  }}
/>

<Stack.Screen
  name="EditProviderProfile"
  component={EditProviderProfileScreen}
  options={{ title: "Edit Profile" }}
/>

    </Stack.Navigator>
  );
}