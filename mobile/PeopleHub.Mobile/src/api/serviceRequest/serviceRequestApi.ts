import { apiClient } from "../client";
import { ServiceRequest } from "../../types";

class ServiceRequestApi {
  async getMyRequests(): Promise<ServiceRequest[]> {
    const { data } = await apiClient.get<ServiceRequest[]>(
      "/service-requests/my-requests"
    );

    return data;
  }
}

export const serviceRequestApi = new ServiceRequestApi();