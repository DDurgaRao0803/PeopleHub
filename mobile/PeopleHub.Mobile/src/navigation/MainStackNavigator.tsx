import React from "react";
import {
  createNativeStackNavigator,
} from "@react-navigation/native-stack";

import {
  CreateRequestScreen,
  RequestDetailsScreen,
} from "../screens/serviceRequest";

import {
  ProviderRequestsScreen,
} from "../screens/provider";

import { MainNavigator } from "./MainNavigator";
import { ProviderProfileScreen } from "../screens/provider/ProviderProfileScreen";

import type { NavigatorScreenParams } from "@react-navigation/native";
import type { MainTabParamList } from "./MainNavigator";

export type MainStackParamList = {
  MainTabs: NavigatorScreenParams<MainTabParamList>;

  CreateRequest: {
  categoryId?: string;
  categoryName?: string;
};

RequestDetails: {
  requestId: string;
};

ProviderProfile: undefined;

ProviderRequests: undefined;

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
    </Stack.Navigator>
  );
}