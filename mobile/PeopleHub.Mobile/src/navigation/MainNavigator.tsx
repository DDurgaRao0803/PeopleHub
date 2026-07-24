import React from "react";
import { createBottomTabNavigator } from "@react-navigation/bottom-tabs";
import { Ionicons } from "@expo/vector-icons";

import { HomeScreen } from "../screens/common/HomeScreen";
import { SearchScreen } from "../screens/customer";
import { RequestsScreen } from "../screens/serviceRequest";
import { NotificationsScreen } from "../screens/notification";
import { ProfileScreen } from "../screens/provider";

import { colors } from "../theme/colors";

export type MainTabParamList = {
  Home: undefined;
  Search: undefined;
  Requests: undefined;
  Notifications: undefined;
  Profile: undefined;
};

const Tab = createBottomTabNavigator<MainTabParamList>();

export function MainNavigator(): React.JSX.Element {
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

            case "Search":
              iconName = "search";
              break;

            case "Requests":
              iconName = "clipboard";
              break;

            case "Notifications":
              iconName = "notifications";
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
    </Tab.Navigator>
  );
}