import { apiClient } from "../client";

import {
  CreateProviderServiceRequest,
  UpdateProviderServiceRequest,
  ProviderService,
} from "../../types/provider";

const BASE_URL = "/provider-services";

export const providerServiceApi = {
  async create(
    request: CreateProviderServiceRequest
  ): Promise<ProviderService> {
    const response = await apiClient.post(BASE_URL, request);
    return response.data;
  },

  async getById(
    id: string
  ): Promise<ProviderService> {
    const response = await apiClient.get(`${BASE_URL}/${id}`);
    return response.data;
  },

  async getByProvider(
    providerProfileId: string
  ): Promise<ProviderService[]> {
    const response = await apiClient.get(
      `${BASE_URL}/provider/${providerProfileId}`
    );

    return response.data;
  },

  async update(
    id: string,
    request: UpdateProviderServiceRequest
  ): Promise<ProviderService> {
    const response = await apiClient.put(
      `${BASE_URL}/${id}`,
      request
    );

    return response.data;
  },

  async delete(
    id: string
  ): Promise<void> {
    await apiClient.delete(`${BASE_URL}/${id}`);
  },
};

export default providerServiceApi;