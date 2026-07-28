import { serviceRequestApi } from "../api";
import {
  CreateServiceRequestRequest,
} from "../types";

class ServiceRequestService {
  // =========================
  // Customer
  // =========================

  async getMyCustomerRequests() {
    return await serviceRequestApi.getMyCustomerRequests();
  }

  // Temporary alias to avoid breaking existing screens
  async getMyRequests() {
    return await this.getMyCustomerRequests();
  }

  async createRequest(
    request: CreateServiceRequestRequest
  ) {
    return await serviceRequestApi.createRequest(request);
  }

  async getRequestById(id: string) {
  return await serviceRequestApi.getRequestById(id);
}

  // =========================
  // Provider
  // =========================

  async getMyProviderRequests() {
    return await serviceRequestApi.getMyProviderRequests();
  }

  async acceptRequest(id: string) {
    return await serviceRequestApi.acceptRequest(id);
  }

  async rejectRequest(id: string) {
    return await serviceRequestApi.rejectRequest(id);
  }

  async completeRequest(id: string) {
    return await serviceRequestApi.completeRequest(id);
  }
}

export const serviceRequestService =
  new ServiceRequestService();