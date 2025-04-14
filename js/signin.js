'use strict';
const email = document.getElementById('email').value;
const password = document.getElementById('password').value;
const singIn = document.querySelector('.forgot');

singIn.addEventListener('click', function () {
  if (!email && !password) {
    alert('Enter ur email and pssword');
  } else location.href = 'index.html';
});
