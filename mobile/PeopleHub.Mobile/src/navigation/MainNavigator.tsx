import React from "react";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { Ionicons } from "@expo/vector-icons";

import { HomeScreen } from "../screens/home/HomeScreen";
import { SearchScreen } from "../screens/customer";
import { RequestsScreen } from "../screens/serviceRequest";
import { NotificationsScreen } from "../screens/notification";
import { ProfileScreen } from "../screens/customer/ProfileScreen";

import { colors } from "../theme/colors";

import { useAuth } from "../context/AuthContext";


import {
  ProviderProfileScreen,
  ProviderServicesScreen,
  ProviderRequestsScreen,
  ProviderEarningsScreen,
} from "../screens/provider";

export type MainTabParamList = {
  Home: undefined;
  Search: undefined;
  Requests: undefined;
  Notifications: undefined;
  Profile: undefined;
};

const Tab = createBottomTabNavigator<MainTabParamList>();

export function MainNavigator(): React.JSX.Element {

  const { user } = useAuth();


  return (
  <Tab.Navigator
    initialRouteName="Home"
    screenOptions={({ route }) => ({
      headerShown: false,

      tabBarActiveTintColor: colors.primary,
      tabBarInactiveTintColor: "#8E8E93",

      tabBarLabelStyle: {
  fontSize: 12,
  fontWeight: "600",
  marginBottom: 2,
},

tabBarItemStyle: {
  justifyContent: "center",
  alignItems: "center",
},

      tabBarStyle: {
  height: 72,

  paddingTop: 8,
  paddingBottom: 8,

  backgroundColor: "#FFFFFF",

  borderTopWidth: 1,
  borderTopColor: "#E5E7EB",

  elevation: 8,

  shadowColor: "#000",
  shadowOffset: {
    width: 0,
    height: -2,
  },
  shadowOpacity: 0.05,
  shadowRadius: 6,
},

      tabBarIcon: ({ color, size }) => {
        let iconName: keyof typeof Ionicons.glyphMap = "home";

        switch (route.name) {
          case "Home":
            iconName = "home";
            break;

          case "Search":
  iconName = user?.isProvider
    ? "clipboard"
    : "search";
  break;

case "Requests":
  iconName = user?.isProvider
    ? "construct"
    : "clipboard";
  break;

case "Notifications":
  iconName = user?.isProvider
    ? "wallet"
    : "notifications";
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
    {user?.isProvider ? (
      <>
  <Tab.Screen
    name="Home"
    component={HomeScreen}
  />

  <Tab.Screen
    name="Search"
    component={ProviderRequestsScreen}
    options={{ title: "Requests" }}
  />

  <Tab.Screen
    name="Requests"
    component={ProviderServicesScreen}
    options={{ title: "Services" }}
  />

  <Tab.Screen
    name="Notifications"
    component={ProviderEarningsScreen}
    options={{ title: "Earnings" }}
  />

  <Tab.Screen
    name="Profile"
    component={ProviderProfileScreen}
  />
</>
    ) : (
      <>
        <Tab.Screen
          name="Home"
          component={HomeScreen}
        />

        <Tab.Screen
          name="Search"
          component={SearchScreen}
        />

        <Tab.Screen
          name="Requests"
          component={RequestsScreen}
        />

        <Tab.Screen
          name="Notifications"
          component={NotificationsScreen}
        />

        <Tab.Screen
          name="Profile"
          component={ProfileScreen}
        />
      </>
    )}
  </Tab.Navigator>
);
}