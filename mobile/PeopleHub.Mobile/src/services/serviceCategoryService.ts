import { serviceCategoriesApi } from "../api";
import type { ServiceCategory } from "../types";

class ServiceCategoryService {
  async getCategories(): Promise<ServiceCategory[]> {
    return await serviceCategoriesApi.getAll();
  }
}

export const serviceCategoryService = new ServiceCategoryService();