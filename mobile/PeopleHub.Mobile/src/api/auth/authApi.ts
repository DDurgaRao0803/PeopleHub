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

  async logout(): Promise<void> {

    await apiClient.post(
      API_ENDPOINTS.AUTH.LOGOUT,
    );
  }
}

export const authApi = new AuthApi();