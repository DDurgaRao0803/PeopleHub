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

  async getRequestById(
  id: string
): Promise<ServiceRequest> {
  const { data } = await apiClient.get<ServiceRequest>(
    `/service-requests/${id}`
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
  const response = await apiClient.get<ServiceRequest[]>(
    "/service-requests/my-requests"
  );

  console.log("========== API RESPONSE ==========");
  console.log(response.data);
  console.log("Response Length:", response.data.length);
  console.log("==================================");

  return response.data;
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