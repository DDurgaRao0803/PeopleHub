/**
 * ============================================================
 * PeopleHub Mobile
 * Axios Client
 * ============================================================
 */

import axios from "axios";

import { AppConfig } from "../config/appConfig";
import { secureStorage } from "../storage";

export const apiClient = axios.create({
  baseURL: AppConfig.api.baseUrl,
  timeout: AppConfig.api.timeout,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
});

apiClient.interceptors.request.use(
  async (config) => {
    const token = await secureStorage.getAccessToken();


    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
  },
  (error) => Promise.reject(error),
);