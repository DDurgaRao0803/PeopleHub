import { providerApi } from "../api/providerApi";
import providerServiceApi from "../api/provider/providerServiceApi";
import providerDashboardApi from "../api/provider/providerDashboardApi";

import type {
  CreateProviderProfileRequest,
  ProviderProfile,
  UpdateProviderProfileRequest,
} from "../api/providerApi";

import type {
  ProviderService,
  CreateProviderServiceRequest,
  UpdateProviderServiceRequest,
} from "../types/provider";

class ProviderServiceManager {
  // =========================
  // Provider Profile
  // =========================

  async getProfile(): Promise<ProviderProfile> {
    return providerApi.getProfile();
  }

  async createProfile(
    request: CreateProviderProfileRequest
  ): Promise<ProviderProfile> {
    return providerApi.createProfile(request);
  }

  async updateProfile(
  request: UpdateProviderProfileRequest
): Promise<ProviderProfile> {
  return providerApi.updateProfile(request);
}

  async deleteProfile(): Promise<void> {
    return providerApi.deleteProfile();
  }

  // =========================
  // Nearby Providers
  // =========================

  async getDashboard() {
  return providerDashboardApi.getDashboard();
}

  async getNearby() {
    return providerApi.getNearby();
  }

  // =========================
  // Provider Services
  // =========================

  async getProviderServices(
    providerProfileId: string
  ): Promise<ProviderService[]> {
    return providerServiceApi.getByProvider(providerProfileId);
  }

  async getProviderService(
    id: string
  ): Promise<ProviderService> {
    return providerServiceApi.getById(id);
  }

  async createProviderService(
    request: CreateProviderServiceRequest
  ): Promise<ProviderService> {
    return providerServiceApi.create(request);
  }

  async updateProviderService(
    id: string,
    request: UpdateProviderServiceRequest
  ): Promise<ProviderService> {
    return providerServiceApi.update(id, request);
  }

  async deleteProviderService(
    id: string
  ): Promise<void> {
    return providerServiceApi.delete(id);
  }
}

export const providerService = new ProviderServiceManager();