import React from "react";

import { useAuth } from "../context/AuthContext";

import SplashScreen from "../screens/auth/SplashScreen";
import { AuthNavigator } from "./AuthNavigator";
import { MainStackNavigator } from "./MainStackNavigator";

export function AppNavigator(): React.JSX.Element {
  const {
    isAuthenticated,
    isLoading,
  } = useAuth();

  if (isLoading) {
    return <SplashScreen />;
  }

  return isAuthenticated ? (
    <MainStackNavigator />
  ) : (
    <AuthNavigator />
  );
}