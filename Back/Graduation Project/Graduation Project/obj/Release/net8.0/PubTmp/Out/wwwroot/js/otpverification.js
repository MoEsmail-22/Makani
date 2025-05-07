// OTP Verification Script

function sendOtp() {
  const email = document.querySelector('input[name="Email"]').value;

  if (!email) {
    alert("Email address not found");
    return;
  }

  // Show loading state
  const sendButton = document.querySelector('button[onclick="sendOtp()"]');
  const originalText = sendButton.textContent;
  sendButton.textContent = "Sending...";
  sendButton.disabled = true;

  // Send OTP request
  fetch("/OtpController/SendOtp", {
    method: "POST",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    body: `email=${encodeURIComponent(email)}`,
  })
    .then((response) => response.json())
    .then((data) => {
      if (data.success) {
        // Show OTP input field
        document.getElementById("otpVerificationSection").style.display =
          "block";
        document.getElementById("otp-status").textContent =
          "OTP sent successfully!";
      } else {
        document.getElementById("otp-status").textContent = "";
        alert("Failed to send OTP: " + data.message);
      }
    })
    .catch((error) => {
      console.error("Error:", error);
      document.getElementById("otp-status").textContent = "";
      alert("An error occurred while sending OTP. Please try again.");
    })
    .finally(() => {
      // Reset button state
      sendButton.textContent = originalText;
      sendButton.disabled = false;
    });
}

function verifyOtp() {
  const email = document.querySelector('input[name="Email"]').value;
  const otp = document.getElementById("otpInput").value;

  if (!email) {
    alert("Email address not found");
    return;
  }

  if (!otp) {
    alert("Please enter the OTP sent to your email");
    return;
  }

  // Show loading state
  const verifyButton = document.querySelector('button[onclick="verifyOtp()"]');
  const originalText = verifyButton.textContent;
  verifyButton.textContent = "Verifying...";
  verifyButton.disabled = true;

  // Verify OTP request
  fetch("/OtpController/VerifyOtp", {
    method: "POST",
    headers: {
      "Content-Type": "application/x-www-form-urlencoded",
    },
    body: `email=${encodeURIComponent(email)}&otp=${encodeURIComponent(otp)}`,
  })
    .then((response) => response.json())
    .then((data) => {
      if (data.success) {
        // Enable form submission
        document.getElementById("emailVerified").value = "true";
        document.getElementById("submitButton").disabled = false;
        alert("Email verified successfully! You can now proceed.");
      } else {
        alert("OTP verification failed: " + data.message);
      }
    })
    .catch((error) => {
      console.error("Error:", error);
      alert("An error occurred during verification. Please try again.");
    })
    .finally(() => {
      // Reset button state
      verifyButton.textContent = originalText;
      verifyButton.disabled = false;
    });
}