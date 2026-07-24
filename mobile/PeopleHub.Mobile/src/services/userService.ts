/**
 * ============================================================
 * PeopleHub Mobile
 * User Service
 * ============================================================
 */

import { userApi } from "../api";

import type { User } from "../types";

class UserService {
  async getCurrentUser(): Promise<User> {
    return userApi.getCurrentUser();
  }
}

export const userService = new UserService();