import { apiClient } from "./client";

import type { ServiceCategory } from "../types";

export class ServiceCategoriesApi {
  async getAll(): Promise<ServiceCategory[]> {
    const { data } = await apiClient.get<ServiceCategory[]>(
      "/service-categories",
    );

    return data;
  }
}

export const serviceCategoriesApi = new ServiceCategoriesApi();