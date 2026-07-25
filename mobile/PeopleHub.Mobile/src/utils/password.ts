export interface PasswordValidationResult {
  isValid: boolean;
  hasMinLength: boolean;
  hasUppercase: boolean;
  hasLowercase: boolean;
  hasNumber: boolean;
  hasSpecialCharacter: boolean;
}

export const validatePassword = (
  password: string,
): PasswordValidationResult => {
  const result = {
    hasMinLength: password.length >= 8,
    hasUppercase: /[A-Z]/.test(password),
    hasLowercase: /[a-z]/.test(password),
    hasNumber: /\d/.test(password),
    hasSpecialCharacter:
      /[!@#$%^&*(),.?":{}|<>]/.test(password),
    isValid: false,
  };

  result.isValid =
    result.hasMinLength &&
    result.hasUppercase &&
    result.hasLowercase &&
    result.hasNumber &&
    result.hasSpecialCharacter;

  return result;
};