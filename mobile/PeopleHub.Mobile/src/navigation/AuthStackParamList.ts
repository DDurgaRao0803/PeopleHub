import { ProviderAvailability } from "../types";

export type AuthStackParamList = {
  Splash: undefined;

  Login: undefined;

  ProviderRegister: undefined;

  AddProviderAvailability: undefined;

EditProviderAvailability: {
  availabilityId: string;
  availability: ProviderAvailability;
};

  EmailPassword: {
    email: string;
  };

  OtpVerification: {
    userId?: string;
    destination: string;
    type: "mobile" | "email";
    purpose:
      | "login"
      | "forgot-password"
      | "register"
      | "verify-email";

    email?: string;
    password?: string;
  };

  ForgotPassword: undefined;

  ResetPassword: {
    userId: string;
    otp: string;
    destination: string;
  };

  AccountType: undefined;

  CustomerRegister: undefined;
};