export interface ServiceRequest {
  id: string;
  customerId: string;
 providerProfileId: string | null;
  serviceCategoryId: string;
  title: string;
  description: string;
  requestedDate: string;
  status: string;
}