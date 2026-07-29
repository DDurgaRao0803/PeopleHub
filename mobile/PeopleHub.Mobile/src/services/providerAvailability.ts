import { providerAvailabilityApi } from "../api";
import {
  CreateProviderAvailabilityRequest,
  ProviderAvailability,
  UpdateProviderAvailabilityRequest,
} from "../types";

export const providerAvailabilityService = {
  async getAvailability(
    providerProfileId: string
  ): Promise<ProviderAvailability[]> {
    const response =
      await providerAvailabilityApi.getAvailability(
        providerProfileId
      );

    return response.data;
  },

  async createAvailability(
    providerProfileId: string,
    request: CreateProviderAvailabilityRequest
  ): Promise<ProviderAvailability> {
    const response =
      await providerAvailabilityApi.createAvailability(
        providerProfileId,
        request
      );

    return response.data;
  },

  async updateAvailability(
    providerProfileId: string,
    availabilityId: string,
    request: UpdateProviderAvailabilityRequest
  ): Promise<ProviderAvailability> {
    const response =
      await providerAvailabilityApi.updateAvailability(
        providerProfileId,
        availabilityId,
        request
      );

    return response.data;
  },

  async deleteAvailability(
    providerProfileId: string,
    availabilityId: string
  ): Promise<void> {
    await providerAvailabilityApi.deleteAvailability(
      providerProfileId,
      availabilityId
    );
  },
};