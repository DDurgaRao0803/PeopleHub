/**
 * ============================================================
 * PeopleHub Mobile
 * Axios Client
 * ============================================================
 */

import axios from "axios";

import { AppConfig } from "../config/appConfig";

export const apiClient = axios.create({
  baseURL: AppConfig.api.baseUrl,
  timeout: AppConfig.api.timeout,
  headers: {
    "Content-Type": "application/json",
    Accept: "application/json",
  },
});