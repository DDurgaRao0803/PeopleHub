import { apiClient } from "./client";
import type { NearbyProvider } from "../types/provider";

class ProviderApi {
  async getNearby(): Promise<NearbyProvider[]> {
    const { data } = await apiClient.get<NearbyProvider[]>(
      "/provider-profiles/nearby"
    );

    return data;
  }
}

export const providerApi = new ProviderApi();