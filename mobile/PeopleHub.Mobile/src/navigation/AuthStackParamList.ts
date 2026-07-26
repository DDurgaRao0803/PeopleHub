export type AuthStackParamList = {
  Splash: undefined;
  Login: undefined;
  ProviderRegister: undefined;
  

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
};

  ForgotPassword: undefined;

  ResetPassword: {
    destination: string;
  };

  AccountType: undefined;
  CustomerRegister: undefined;
};