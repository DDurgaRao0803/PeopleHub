import React from "react";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

import {
  LoginScreen,
  SplashScreen,
  WelcomeScreen,
} from "../screens/auth";

const Stack = createNativeStackNavigator();

export function AuthNavigator(): React.JSX.Element {
  return (
    <Stack.Navigator
      initialRouteName="Splash"
      screenOptions={{
        headerShown: false,
      }}
    >
      <Stack.Screen
        name="Splash"
        component={SplashScreen}
      />

      <Stack.Screen
        name="Welcome"
        component={WelcomeScreen}
      />

      <Stack.Screen
        name="Login"
        component={LoginScreen}
      />
    </Stack.Navigator>
  );
}