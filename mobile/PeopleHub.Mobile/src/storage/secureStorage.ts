/**
 * ============================================================
 * PeopleHub Mobile
 * Secure Storage Service
 * ============================================================
 */

import * as SecureStore from "expo-secure-store";

import { AppConfig } from "../config/appConfig";

class SecureStorage {
  async setAccessToken(token: string): Promise<void> {
    await SecureStore.setItemAsync(
      AppConfig.storage.accessTokenKey,
      token,
    );
  }

  async getAccessToken(): Promise<string | null> {
    return SecureStore.getItemAsync(
      AppConfig.storage.accessTokenKey,
    );
  }

  async removeAccessToken(): Promise<void> {
    await SecureStore.deleteItemAsync(
      AppConfig.storage.accessTokenKey,
    );
  }

  async setRefreshToken(token: string): Promise<void> {
    await SecureStore.setItemAsync(
      AppConfig.storage.refreshTokenKey,
      token,
    );
  }

  async getRefreshToken(): Promise<string | null> {
    return SecureStore.getItemAsync(
      AppConfig.storage.refreshTokenKey,
    );
  }

  async removeRefreshToken(): Promise<void> {
    await SecureStore.deleteItemAsync(
      AppConfig.storage.refreshTokenKey,
    );
  }

  async setUser<T>(user: T): Promise<void> {
    await SecureStore.setItemAsync(
      AppConfig.storage.userKey,
      JSON.stringify(user),
    );
  }

  async getUser<T>(): Promise<T | null> {
    const value = await SecureStore.getItemAsync(
      AppConfig.storage.userKey,
    );

    if (!value) {
      return null;
    }

    return JSON.parse(value) as T;
  }

  async removeUser(): Promise<void> {
    await SecureStore.deleteItemAsync(
      AppConfig.storage.userKey,
    );
  }

  async clearAuthentication(): Promise<void> {
    await Promise.all([
      this.removeAccessToken(),
      this.removeRefreshToken(),
      this.removeUser(),
    ]);
  }
}

export const secureStorage = new SecureStorage();