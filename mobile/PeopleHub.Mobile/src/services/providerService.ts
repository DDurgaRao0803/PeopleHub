import { providerApi } from "../api/providerApi";
import type {
  CreateProviderProfileRequest,
  ProviderProfile,
} from "../api/providerApi";

class ProviderService {
  // =========================
  // Provider Profile
  // =========================

  async getProfile(): Promise<ProviderProfile> {
    return await providerApi.getProfile();
  }

  async createProfile(
    request: CreateProviderProfileRequest
  ): Promise<ProviderProfile> {
    return await providerApi.createProfile(request);
  }

  async updateProfile(
    request: CreateProviderProfileRequest
  ): Promise<ProviderProfile> {
    return await providerApi.updateProfile(request);
  }

  async deleteProfile(): Promise<void> {
    return await providerApi.deleteProfile();
  }

  // =========================
  // Nearby Providers
  // =========================

  async getNearby() {
    return await providerApi.getNearby();
  }
}

export const providerService = new ProviderService();