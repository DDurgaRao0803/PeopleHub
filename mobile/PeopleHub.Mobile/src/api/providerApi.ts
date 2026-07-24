import { apiClient } from "./client";
import type { NearbyProvider } from "../types/provider";

export interface CreateProviderProfileRequest {
  bio: string;
  experienceYears: number;
}

export interface ProviderProfile {
  id: string;
  userId: string;
  bio: string;
  experienceYears: number;
  rating: number;
  totalReviews: number;
  isVerified: boolean;
}

class ProviderApi {
  // =========================
  // Provider Profile
  // =========================

  async getProfile(): Promise<ProviderProfile> {
    const { data } = await apiClient.get<ProviderProfile>(
      "/provider-profiles"
    );

    return data;
  }

  async createProfile(
    request: CreateProviderProfileRequest
  ): Promise<ProviderProfile> {
    const { data } = await apiClient.post<ProviderProfile>(
      "/provider-profiles",
      request
    );

    return data;
  }

  async updateProfile(
    request: CreateProviderProfileRequest
  ): Promise<ProviderProfile> {
    const { data } = await apiClient.put<ProviderProfile>(
      "/provider-profiles",
      request
    );

    return data;
  }

  async deleteProfile(): Promise<void> {
    await apiClient.delete("/provider-profiles");
  }

  // =========================
  // Nearby Providers
  // =========================

  async getNearby(): Promise<NearbyProvider[]> {
    const { data } = await apiClient.get<NearbyProvider[]>(
      "/provider-profiles/nearby"
    );

    return data;
  }
}

export const providerApi = new ProviderApi();