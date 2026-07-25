import React from "react";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { ForgotPasswordScreen } from "../screens/auth/ForgotPasswordScreen";
import { ResetPasswordScreen } from "../screens/auth/ResetPasswordScreen";
import { CustomerRegisterScreen } from "../screens/auth/CustomerRegisterScreen";
import {
  LoginScreen,
  SplashScreen,
  OtpVerificationScreen,
  AccountTypeScreen,
} from "../screens/auth";

import { AuthStackParamList } from "./AuthStackParamList";

const Stack = createNativeStackNavigator<AuthStackParamList>();

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
    name="Login"
    component={LoginScreen}
/>

<Stack.Screen
  name="OtpVerification"
  component={OtpVerificationScreen}
/>

<Stack.Screen
  name="AccountType"
  component={AccountTypeScreen}
/>

<Stack.Screen
  name="ForgotPassword"
  component={ForgotPasswordScreen}
/>

<Stack.Screen
  name="ResetPassword"
  component={ResetPasswordScreen}
/>

<Stack.Screen
  name="CustomerRegister"
  component={CustomerRegisterScreen}
/>
    </Stack.Navigator>
  );
  
}