/**
 * ============================================================
 * PeopleHub Mobile
 * User API
 * ============================================================
 */

import { apiClient } from "../client";

import type { User } from "../../types";

class UserApi {
  async getCurrentUser(): Promise<User> {
  

  const response = await apiClient.get<User>("/users/me");

  

  return response.data;
}
}

export const userApi = new UserApi();