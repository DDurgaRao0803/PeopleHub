export interface ProviderAvailability {
  id: string;
  dayOfWeek: number;
  startTime: string;
  endTime: string;
}

export interface CreateProviderAvailabilityRequest {
  dayOfWeek: number;
  startTime: string;
  endTime: string;
}

export interface UpdateProviderAvailabilityRequest {
  dayOfWeek: number;
  startTime: string;
  endTime: string;
}