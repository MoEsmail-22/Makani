'use strict';
const email = document.getElementById('email').value;
const password = document.getElementById('password').value;
const singIn = document.querySelector('.sighIN');

singIn.addEventListener('click', function () {
  location.href = 'index.html';
});
