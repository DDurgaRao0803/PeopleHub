import { apiClient } from "./client";
import {
  CreateProviderAvailabilityRequest,
  ProviderAvailability,
  UpdateProviderAvailabilityRequest,
} from "../types";

const BASE_URL = "/providers/profiles";

export const providerAvailabilityApi = {
  getAvailability(providerProfileId: string) {
    return apiClient.get<ProviderAvailability[]>(
      `${BASE_URL}/${providerProfileId}/availability`
    );
  },

  createAvailability(
    providerProfileId: string,
    request: CreateProviderAvailabilityRequest
  ) {
    return apiClient.post<ProviderAvailability>(
      `${BASE_URL}/${providerProfileId}/availability`,
      request
    );
  },

  updateAvailability(
    providerProfileId: string,
    availabilityId: string,
    request: UpdateProviderAvailabilityRequest
  ) {
    return apiClient.put<ProviderAvailability>(
      `${BASE_URL}/${providerProfileId}/availability/${availabilityId}`,
      request
    );
  },

  deleteAvailability(
    providerProfileId: string,
    availabilityId: string
  ) {
    return apiClient.delete<void>(
      `${BASE_URL}/${providerProfileId}/availability/${availabilityId}`
    );
  },
};