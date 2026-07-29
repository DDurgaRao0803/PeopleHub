export interface ProviderProfile {
  id: string;
  bio: string;
  experienceYears: number;
}

export interface CreateProviderProfileRequest {
  bio: string;
  experienceYears: number;
}

export interface UpdateProviderProfileRequest {
  bio: string;
  experienceYears: number;
}