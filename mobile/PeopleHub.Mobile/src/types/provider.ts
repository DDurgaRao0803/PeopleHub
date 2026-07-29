export interface NearbyProvider {
  id: string;
  providerProfileId: string;

  fullName: string;
  serviceCategory: string;

  rating: number;
  reviewCount: number;
  distanceKm: number;

  profileImageUrl?: string;

  isAvailable: boolean;
}

export interface ProviderService {
  id: string;
  providerProfileId: string;
  serviceCategoryId: string;

  title: string;
  description: string;

  basePrice: number;
  estimatedDurationMinutes: number;

  isActive: boolean;
}

export interface CreateProviderServiceRequest {
  providerProfileId: string;
  serviceCategoryId: string;

  title: string;
  description: string;

  basePrice: number;
  estimatedDurationMinutes: number;
}

export interface UpdateProviderServiceRequest {
  title: string;
  description: string;

  basePrice: number;
  estimatedDurationMinutes: number;

  isActive: boolean;
}