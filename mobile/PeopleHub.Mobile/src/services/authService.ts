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

  async register(
  request: RegisterCustomerRequest,
): Promise<void> {
  await authApi.register(request);
}

  async login(request: LoginRequest): Promise<LoginResponse> {
    const response = await authApi.login(request);

    await secureStorage.setAccessToken(response.accessToken);
    await secureStorage.setRefreshToken(response.refreshToken);

    return response;
  }

  async logout(): Promise<void> {
    try {
      await authApi.logout();
    } finally {
      await secureStorage.clearAuthentication();
    }
  }

  async getAccessToken(): Promise<string | null> {
    return secureStorage.getAccessToken();
  }

  async getRefreshToken(): Promise<string | null> {
    return secureStorage.getRefreshToken();
  }

  async isAuthenticated(): Promise<boolean> {
    const token = await secureStorage.getAccessToken();

    return token !== null;
  }
}

export const authService = new AuthService();