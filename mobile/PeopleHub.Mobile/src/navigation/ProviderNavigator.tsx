import React from "react";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { Ionicons } from "@expo/vector-icons";

import { colors } from "../theme/colors";


import { HomeScreen } from "../screens/home/HomeScreen";

import {
  ProviderProfileScreen,
  ProviderServicesScreen,
  ProviderRequestsScreen,
  ProviderAvailabilityScreen,
} from "../screens/provider";


export type ProviderTabParamList = {
  Home: undefined;
  Services: undefined;
  Availability: undefined;
  Requests: undefined;
  Profile: undefined;
};

const Tab = createBottomTabNavigator<ProviderTabParamList>();

export function ProviderNavigator(): React.JSX.Element {
  return (
    <Tab.Navigator
      initialRouteName="Home"
      screenOptions={({ route }) => ({
        headerShown: false,

        tabBarActiveTintColor: colors.primary,
        tabBarInactiveTintColor: "#8E8E93",

        tabBarStyle: {
          height: 62,
          paddingBottom: 8,
          paddingTop: 6,
        },

        tabBarIcon: ({ color, size }) => {
          let iconName: keyof typeof Ionicons.glyphMap = "home";

          switch (route.name) {
  case "Home":
    iconName = "home";
    break;

  case "Services":
    iconName = "construct";
    break;

  case "Availability":
    iconName = "calendar";
    break;

  case "Requests":
    iconName = "clipboard";
    break;

  case "Profile":
    iconName = "person";
    break;
}

          return (
            <Ionicons
              name={iconName}
              size={size}
              color={color}
            />
          );
        },
      })}
    >
      <Tab.Screen
        name="Home"
        component={HomeScreen}
      />

      <Tab.Screen
    name="Services"
    component={ProviderServicesScreen}
/>

      <Tab.Screen
        name="Requests"
        component={ProviderRequestsScreen }
      />

      <Tab.Screen
    name="Availability"
    component={ProviderAvailabilityScreen}
/>

      <Tab.Screen
        name="Profile"
        component={ProviderProfileScreen }
      />
    </Tab.Navigator>
  );
}