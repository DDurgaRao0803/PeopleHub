import { useCallback, useEffect, useState } from "react";

import {
  serviceRequestService,
} from "../services";

import {
  CreateServiceRequestRequest,
  ServiceRequest,
} from "../types";

export function useServiceRequests() {
  const [requests, setRequests] = useState<ServiceRequest[]>([]);
  const [loading, setLoading] = useState(false);
  const [refreshing, setRefreshing] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const loadRequests = useCallback(async () => {
    try {
      setLoading(true);
      setError(null);

      const data =
        await serviceRequestService.getMyRequests();

      setRequests(data);
    } catch (err: any) {
      setError(
        err?.response?.data?.message ??
        err?.message ??
        "Failed to load requests."
      );
    } finally {
      setLoading(false);
    }
  }, []);

  const refresh = useCallback(async () => {
    try {
      setRefreshing(true);

      const data =
        await serviceRequestService.getMyRequests();

      setRequests(data);
    } catch (err: any) {
      setError(
        err?.response?.data?.message ??
        err?.message ??
        "Failed to refresh requests."
      );
    } finally {
      setRefreshing(false);
    }
  }, []);

  const createRequest = async (
    request: CreateServiceRequestRequest
  ) => {
    const created =
      await serviceRequestService.createRequest(
        request
      );

    await loadRequests();

    return created;
  };

  const acceptRequest = async (
    id: string
  ) => {
    await serviceRequestService.acceptRequest(id);

    await loadRequests();
  };

  const rejectRequest = async (
    id: string
  ) => {
    await serviceRequestService.rejectRequest(id);

    await loadRequests();
  };

  const completeRequest = async (
    id: string
  ) => {
    await serviceRequestService.completeRequest(id);

    await loadRequests();
  };

  useEffect(() => {
    loadRequests();
  }, [loadRequests]);

  return {
    requests,

    loading,

    refreshing,

    error,

    loadRequests,

    refresh,

    createRequest,

    acceptRequest,

    rejectRequest,

    completeRequest,
  };
}