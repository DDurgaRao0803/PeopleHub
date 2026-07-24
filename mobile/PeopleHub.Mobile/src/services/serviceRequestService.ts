import { serviceRequestApi } from "../api";

class ServiceRequestService {
  async getMyRequests() {
    return await serviceRequestApi.getMyRequests();
  }
}

export const serviceRequestService = new ServiceRequestService();