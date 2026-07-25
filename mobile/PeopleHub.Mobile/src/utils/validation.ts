export const isRequired = (value: string): boolean => {
  return value.trim().length > 0;
};

export const validateEmail = (email: string): string | null => {
  if (!email.trim()) {
    return "Email is required.";
  }

  const regex =
    /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  if (!regex.test(email)) {
    return "Please enter a valid email address.";
  }

  return null;
};

export const validateMobile = (
  mobile: string,
): string | null => {
  if (!mobile.trim()) {
    return "Mobile number is required.";
  }

  const digits = mobile.replace(/\D/g, "");

  if (digits.length < 8 || digits.length > 15) {
    return "Please enter a valid mobile number.";
  }

  return null;
};

export const validateRequired = (
  value: string,
  field: string,
): string | null => {
  if (!value.trim()) {
    return `${field} is required.`;
  }

  return null;
};