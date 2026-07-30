/**
 * ============================================================
 * PeopleHub Mobile
 * Authentication API
 * ============================================================
 */

import { apiClient } from "../client";
import { API_ENDPOINTS } from "../endpoints";

import type {
  LoginRequest,
  LoginResponse,
  RefreshTokenRequest,
  RefreshTokenResponse,
  RegisterCustomerRequest,
} from "../../types";

export interface RegisterResponse {
  userId: string;
}

export interface VerifyOtpRequest {
  userId: string;
  otp: string;
}

export interface ForgotPasswordRequest {
  email: string;
}

export interface ForgotPasswordResponse {
  message: string;
  userId: string;
}

export interface ResetPasswordRequest {
  userId: string;
  otp: string;
  newPassword: string;
  confirmPassword: string;
}

export class AuthApi {
  async register(
    request: RegisterCustomerRequest,
  ): Promise<RegisterResponse> {
    const { data } =
      await apiClient.post<RegisterResponse>(
        API_ENDPOINTS.AUTH.REGISTER,
        request,
      );

    return data;
  }

  async verifyOtp(
    request: VerifyOtpRequest,
  ): Promise<void> {
    await apiClient.post(
      API_ENDPOINTS.AUTH.VERIFY_OTP,
      request,
    );
  }

  async login(
    request: LoginRequest,
  ): Promise<LoginResponse> {
    const { data } =
      await apiClient.post<LoginResponse>(
        API_ENDPOINTS.AUTH.LOGIN,
        request,
      );

    return data;
  }

  async refreshToken(
    request: RefreshTokenRequest,
  ): Promise<RefreshTokenResponse> {
    const { data } =
      await apiClient.post<RefreshTokenResponse>(
        API_ENDPOINTS.AUTH.REFRESH,
        request,
      );

    return data;
  }

  async forgotPassword(
    request: ForgotPasswordRequest,
  ): Promise<ForgotPasswordResponse> {
    const { data } =
      await apiClient.post<ForgotPasswordResponse>(
        API_ENDPOINTS.AUTH.FORGOT_PASSWORD,
        request,
      );

    return data;
  }

  async resetPassword(
    request: ResetPasswordRequest,
  ): Promise<void> {
    await apiClient.post(
      API_ENDPOINTS.AUTH.RESET_PASSWORD,
      request,
    );
  }

  async logout(
  refreshToken: string,
): Promise<void> {
  await apiClient.post(
    API_ENDPOINTS.AUTH.LOGOUT,
    {
      refreshToken,
    },
  );
}
}

export const authApi = new AuthApi();