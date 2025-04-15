document.querySelector("form").addEventListener("submit", function (e) {
  const password = document.getElementById("password").value;
  const confirmPass = document.getElementById("conf-pass").value;
  const errorElement = document.getElementById("passwordError");

  if (password !== confirmPass) {
    e.preventDefault(); // Stop form submission
    errorElement.style.display = "block";

    // Highlight fields in red
    document.getElementById("password").style.borderColor = "red";
    document.getElementById("conf-pass").style.borderColor = "red";
  } else {
    errorElement.style.display = "none";
    // Form will submit normally if validation passes
  }
});

document.querySelectorAll(".toggle-password").forEach((icon) => {
  icon.addEventListener("click", function () {
    const input = this.previousElementSibling;
    const isPassword = input.type === "password";

    // Toggle input type
    input.type = isPassword ? "text" : "password";

    // Toggle eye icon
    this.classList.toggle("fa-eye-slash");
    this.classList.toggle("fa-eye");
  });
});
