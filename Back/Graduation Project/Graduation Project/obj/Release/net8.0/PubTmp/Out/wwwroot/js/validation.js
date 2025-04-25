/**
 * Form Validation Script for Makani Project
 * This script provides client-side validation for all forms with visual feedback
 */

document.addEventListener("DOMContentLoaded", function () {
  // Add validation CSS to head
  const linkElement = document.createElement("link");
  linkElement.rel = "stylesheet";
  linkElement.href = "/css/validation.css";
  document.head.appendChild(linkElement);

  // Find all forms in the document
  const forms = document.querySelectorAll("form");

  forms.forEach((form) => {
    // Skip validation for search/filter forms in Home and Listing pages
    if (
      form.closest(".searchBar") ||
      (form.getAttribute("asp-controller") === "Listing" &&
        form.getAttribute("method") === "get") ||
      // Skip validation for any form in Listing view
      document.querySelector("body").classList.contains("listing-page") ||
      window.location.pathname.includes("/Listing/")
    ) {
      return; // Skip validation for filter forms
    }
    // Add error message elements after each input
    const inputs = form.querySelectorAll(
      'input:not([type="submit"]):not([type="checkbox"]), textarea, select'
    );

    inputs.forEach((input) => {
      // Create error message element if it doesn't exist
      if (
        !input.nextElementSibling ||
        !input.nextElementSibling.classList.contains("error-message")
      ) {
        const errorElement = document.createElement("div");
        errorElement.className = "error-message";
        errorElement.id = `${input.id}-error`;
        input.parentNode.insertBefore(errorElement, input.nextElementSibling);
      }

      // Add validation on blur event
      input.addEventListener("blur", function () {
        validateInput(input);
      });

      // Add validation on input event to provide real-time feedback
      input.addEventListener("input", function () {
        if (input.classList.contains("input-error")) {
          validateInput(input);
        }
      });
    });

    // Add form submission validation
    form.addEventListener("submit", function (event) {
      let isValid = true;

      inputs.forEach((input) => {
        if (!validateInput(input)) {
          isValid = false;
        }
      });

      // Special case for password confirmation
      const password = form.querySelector("#password");
      const confirmPassword = form.querySelector("#conf-pass");

      if (password && confirmPassword) {
        if (password.value !== confirmPassword.value) {
          showError(confirmPassword, "Passwords do not match");
          isValid = false;
        }
      }

      if (!isValid) {
        event.preventDefault();
        // Scroll to the first error
        const firstError = form.querySelector(".input-error");
        if (firstError) {
          firstError.focus();
        }
      }
    });
  });
});

/**
 * Validates a single input field
 * @param {HTMLElement} input - The input element to validate
 * @returns {boolean} - Whether the input is valid
 */
function validateInput(input) {
  // Get the error message element
  const errorElement = document.getElementById(`${input.id}-error`);
  if (!errorElement) return true; // Skip if no error element exists

  // Clear previous errors
  input.classList.remove("input-error");
  errorElement.textContent = "";
  errorElement.classList.remove("visible");

  // Check if the input is required and empty
  if (input.hasAttribute("required") && !input.value.trim()) {
    showError(input, "This field is required");
    return false;
  }

  // Validate based on input type
  switch (input.type) {
    case "email":
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(input.value)) {
        showError(input, "Please enter a valid email address");
        return false;
      }
      break;

    case "password":
      if (input.value.length < 6) {
        showError(input, "Password must be at least 6 characters");
        return false;
      }
      break;

    case "tel":
    case "text":
      if (input.id === "phone") {
        const phoneRegex = /^\+?[0-9\s-()]{8,}$/;
        if (!phoneRegex.test(input.value)) {
          showError(input, "Please enter a valid phone number");
          return false;
        }
      }
      break;

    case "number":
      if (
        input.hasAttribute("min") &&
        parseFloat(input.value) < parseFloat(input.getAttribute("min"))
      ) {
        showError(input, `Value must be at least ${input.getAttribute("min")}`);
        return false;
      }
      break;
  }

  // Validate select elements
  if (input.tagName === "SELECT" && input.value === "") {
    showError(input, "Please select an option");
    return false;
  }

  // Validate textarea
  if (input.tagName === "TEXTAREA" && input.value.trim().length < 10) {
    showError(input, "Please enter at least 10 characters");
    return false;
  }

  return true;
}

/**
 * Shows an error message for an input
 * @param {HTMLElement} input - The input element with error
 * @param {string} message - The error message to display
 */
function showError(input, message) {
  input.classList.add("input-error");
  const errorElement = document.getElementById(`${input.id}-error`);
  if (errorElement) {
    errorElement.textContent = message;
    errorElement.classList.add("visible");
  }
}
