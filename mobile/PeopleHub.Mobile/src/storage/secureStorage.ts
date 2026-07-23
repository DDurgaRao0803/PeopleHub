/**
 * ============================================================
 * PeopleHub Mobile
 * Secure Storage Service
 * ============================================================
 */

import * as SecureStore from "expo-secure-store";
import { Platform } from "react-native";

import { AppConfig } from "../config/appConfig";

class SecureStorage {
  private async setItem(key: string, value: string): Promise<void> {
    if (Platform.OS === "web") {
      localStorage.setItem(key, value);
      return;
    }

    await SecureStore.setItemAsync(key, value);
  }

  private async getItem(key: string): Promise<string | null> {
    if (Platform.OS === "web") {
      return localStorage.getItem(key);
    }

    return SecureStore.getItemAsync(key);
  }

  private async removeItem(key: string): Promise<void> {
    if (Platform.OS === "web") {
      localStorage.removeItem(key);
      return;
    }

    await SecureStore.deleteItemAsync(key);
  }

  async setAccessToken(token: string): Promise<void> {
    await this.setItem(AppConfig.storage.accessTokenKey, token);
  }

  async getAccessToken(): Promise<string | null> {
    return this.getItem(AppConfig.storage.accessTokenKey);
  }

  async removeAccessToken(): Promise<void> {
    await this.removeItem(AppConfig.storage.accessTokenKey);
  }

  async setRefreshToken(token: string): Promise<void> {
    await this.setItem(AppConfig.storage.refreshTokenKey, token);
  }

  async getRefreshToken(): Promise<string | null> {
    return this.getItem(AppConfig.storage.refreshTokenKey);
  }

  async removeRefreshToken(): Promise<void> {
    await this.removeItem(AppConfig.storage.refreshTokenKey);
  }

  async setUser<T>(user: T): Promise<void> {
    await this.setItem(
      AppConfig.storage.userKey,
      JSON.stringify(user),
    );
  }

  async getUser<T>(): Promise<T | null> {
    const value = await this.getItem(AppConfig.storage.userKey);

    if (!value) {
      return null;
    }

    return JSON.parse(value) as T;
  }

  async removeUser(): Promise<void> {
    await this.removeItem(AppConfig.storage.userKey);
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