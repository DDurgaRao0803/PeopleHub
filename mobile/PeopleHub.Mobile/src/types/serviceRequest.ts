export interface ServiceRequest {
  id: string;
  customerId: string;
  providerProfileId: string | null;
  serviceCategoryId: string;

  title: string;
  description: string;
  requestedDate: string;
  status: string;

  customerName: string;
  customerProfileImageUrl?: string;

  serviceAddress: string;
  city: string;

  latitude?: number;
  longitude?: number;

  estimatedBudget?: number;
}

export interface CreateServiceRequestRequest {
  serviceCategoryId: string;
  title: string;
  description: string;
  requestedDate: string;
}