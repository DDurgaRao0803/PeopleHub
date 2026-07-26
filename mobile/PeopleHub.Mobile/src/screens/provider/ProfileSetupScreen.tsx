import React from "react";

import { AppTextInput } from "../../components/forms/AppTextInput";
import { OnboardingLayout } from "../../components/provider/OnboardingLayout";

export function ProfileSetupScreen(): React.JSX.Element {
  return (
    <OnboardingLayout
      step={1}
      totalSteps={7}
      title="Complete Your Profile"
      subtitle="Let's set up your provider account."
      nextButtonTitle="Next"
      nextDisabled
      onNext={() => {}}
    >
      <AppTextInput
        label="Business Name"
        placeholder="Enter your business name"
      />

      <AppTextInput
        label="Bio"
        placeholder="Tell customers about yourself"
        multiline
        numberOfLines={4}
      />

      <AppTextInput
        label="Years of Experience"
        placeholder="e.g. 5"
        keyboardType="numeric"
      />
    </OnboardingLayout>
  );
}