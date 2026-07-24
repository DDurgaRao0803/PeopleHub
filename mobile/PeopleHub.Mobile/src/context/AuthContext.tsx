/**
 * ============================================================
 * PeopleHub Mobile
 * Authentication Context
 * ============================================================
 */

import React, {
  createContext,
  useContext,
  useEffect,
  useMemo,
  useState,
} from "react";

import { authService, userService } from "../services";

import type {
  LoginRequest,
  User,
} from "../types";;

interface AuthContextValue {
  isAuthenticated: boolean;
  isLoading: boolean;
  user: User | null;

  login: (request: LoginRequest) => Promise<void>;
  logout: () => Promise<void>;
}

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

interface AuthProviderProps {
  children: React.ReactNode;
}

export function AuthProvider({
  children,
}: AuthProviderProps): React.JSX.Element {
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [user, setUser] = useState<User | null>(null);

  useEffect(() => {
    const initialize = async (): Promise<void> => {
  try {
    const authenticated = await authService.isAuthenticated();

    if (!authenticated) {
      setIsAuthenticated(false);
      return;
    }

    const currentUser = await userService.getCurrentUser();

    setUser(currentUser);
    setIsAuthenticated(true);
  } catch {
   

    await authService.logout();

    setUser(null);
    setIsAuthenticated(false);
  } finally {
    setIsLoading(false);
  }
};

    void initialize();
  }, []);

  const login = async (request: LoginRequest): Promise<void> => {
  

  await authService.login(request);

  

  const currentUser = await userService.getCurrentUser();

  

  setUser(currentUser);

  setIsAuthenticated(true);
};

  const logout = async (): Promise<void> => {
  await authService.logout();

  setUser(null);
  setIsAuthenticated(false);
};

  const value = useMemo<AuthContextValue>(
  () => ({
    isAuthenticated,
    isLoading,
    user,
    login,
    logout,
  }),
  [
    isAuthenticated,
    isLoading,
    user,
  ],
);

  return (
    <AuthContext.Provider value={value}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth(): AuthContextValue {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used within an AuthProvider.");
  }

  return context;
}