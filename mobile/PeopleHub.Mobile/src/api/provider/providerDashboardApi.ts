import { apiClient } from "../client";

import type { ProviderDashboard } from "../../types/providerDashboard";

const BASE_URL = "/provider/dashboard";

export const providerDashboardApi = {
  async getDashboard(): Promise<ProviderDashboard> {
    const response = await apiClient.get<ProviderDashboard>(BASE_URL);
    return response.data;
  },
};

export default providerDashboardApi;