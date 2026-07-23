const { defineConfig } = require("eslint/config");
const expo = require("eslint-config-expo/flat");

module.exports = defineConfig([
  ...expo,

  {
    ignores: [
      "node_modules/**",
      ".expo/**",
      "dist/**",
      "build/**",
    ],
  },

  {
    rules: {
      "no-console": "warn",
      "no-debugger": "error",
      "prefer-const": "error",
      "no-var": "error",
    },
  },
]);