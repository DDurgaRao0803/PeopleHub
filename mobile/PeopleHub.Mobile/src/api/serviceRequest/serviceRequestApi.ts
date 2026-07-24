import { apiClient } from "../client";
import {
  CreateServiceRequestRequest,
  ServiceRequest,
} from "../../types";

class ServiceRequestApi {
  // =========================
  // Customer
  // =========================

  async getMyCustomerRequests(): Promise<ServiceRequest[]> {
    const { data } = await apiClient.get<ServiceRequest[]>(
      "/service-requests/my-customer-requests"
    );

    return data;
  }

  // Temporary alias to avoid breaking existing screens
  async getMyRequests(): Promise<ServiceRequest[]> {
    return this.getMyCustomerRequests();
  }

  async createRequest(
    request: CreateServiceRequestRequest
  ): Promise<ServiceRequest> {
    const { data } = await apiClient.post<ServiceRequest>(
      "/service-requests",
      request
    );

    return data;
  }

  // =========================
  // Provider
  // =========================

  async getMyProviderRequests(): Promise<ServiceRequest[]> {
    const { data } = await apiClient.get<ServiceRequest[]>(
      "/service-requests/my-requests"
    );

    return data;
  }

  async acceptRequest(id: string): Promise<void> {
    await apiClient.post(
      `/service-requests/${id}/accept`
    );
  }

  async rejectRequest(id: string): Promise<void> {
    await apiClient.post(
      `/service-requests/${id}/reject`
    );
  }

  async completeRequest(id: string): Promise<void> {
    await apiClient.post(
      `/service-requests/${id}/complete`
    );
  }
}

export const serviceRequestApi = new ServiceRequestApi();