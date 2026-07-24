import { providerApi } from "../api/providerApi";

class ProviderService {
  async getNearby() {
    return await providerApi.getNearby();
  }
}

export const providerService = new ProviderService();