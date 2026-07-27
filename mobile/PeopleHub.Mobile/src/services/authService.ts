/**
 * ============================================================
 * PeopleHub Mobile
 * Authentication Service
 * ============================================================
 */

import { authApi } from "../api";
import { secureStorage } from "../storage";

import type {
  LoginRequest,
  LoginResponse,
  RegisterCustomerRequest,
} from "../types";

class AuthService {

  private pendingRegistration: {
    email: string;
    password: string;
  } | null = null;

  async registerCustomer(
    request: RegisterCustomerRequest,
  ): Promise<string> {

    const response =
      await authApi.register(request);

    return response.userId;
  }

  async registerProvider(
    request: RegisterCustomerRequest,
  ): Promise<string> {

    const response =
      await authApi.register(request);

    return response.userId;
  }

  setPendingRegistration(
    email: string,
    password: string,
  ): void {

    this.pendingRegistration = {
      email,
      password,
    };
  }

  getPendingRegistration(): {
    email: string;
    password: string;
  } | null {

    return this.pendingRegistration;
  }

  clearPendingRegistration(): void {

    this.pendingRegistration = null;
  }

  async verifyOtp(
    userId: string,
    otp: string,
  ): Promise<void> {

    await authApi.verifyOtp({
      userId,
      otp,
    });
  }

  async login(
    request: LoginRequest,
  ): Promise<LoginResponse> {


    const response =
      await authApi.login(request);

    await secureStorage.setAccessToken(
      response.accessToken,
    );

    await secureStorage.setRefreshToken(
      response.refreshToken,
    )

    return response;
  }

  async logout(): Promise<void> {

    try {

      await authApi.logout();

    } catch {


    } finally {

      await secureStorage.clearAuthentication();
    }
  }

  async getAccessToken(): Promise<string | null> {

    const token =
      await secureStorage.getAccessToken();

    return token;
  }

  async getRefreshToken(): Promise<string | null> {

    const token =
      await secureStorage.getRefreshToken();

    return token;
  }

  async forgotPassword(
  email: string,
): Promise<void> {

  await authApi.forgotPassword({
    email,
  });
}

  async isAuthenticated(): Promise<boolean> {

    const token =
      await secureStorage.getAccessToken();

    return token !== null;
  }
}

export const authService = new AuthService();