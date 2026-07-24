import React from "react";
import {
  createNativeStackNavigator,
} from "@react-navigation/native-stack";

import { MainNavigator } from "./MainNavigator";
import { CreateRequestScreen } from "../screens/serviceRequest";
import { ProfileScreen } from "../screens/provider";

import type { NavigatorScreenParams } from "@react-navigation/native";
import type { MainTabParamList } from "./MainNavigator";

export type MainStackParamList = {
  MainTabs: NavigatorScreenParams<MainTabParamList>;

  CreateRequest: {
    categoryId?: string;
    categoryName?: string;
  };

  ProviderProfile: undefined;
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
        name="ProviderProfile"
        component={ProfileScreen}
      />
    </Stack.Navigator>
  );
}