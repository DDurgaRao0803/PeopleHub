import React from "react";

import { useAuth } from "../context/AuthContext";

import LoadingScreen from "../screens/auth/LoadingScreen";
import { AuthNavigator } from "./AuthNavigator";
import { MainStackNavigator } from "./MainStackNavigator";

export function AppNavigator(): React.JSX.Element {
  const {
    isAuthenticated,
    isLoading,
  } = useAuth();

  if (isLoading) {
    return <LoadingScreen />;
  }

  return isAuthenticated ? (
    <MainStackNavigator />
  ) : (
    <AuthNavigator />
  );
}