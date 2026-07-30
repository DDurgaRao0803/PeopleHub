export interface ProviderDashboard {
  providerProfileId: string;
  verificationStatus: string;
  averageRating: number;
  completedJobs: number;
  responseRate: number;
  lastActiveUtc: string | null;
  pendingRequests: number;
  acceptedRequests: number;
  completedRequests: number;
  cancelledRequests: number;
}