'use stirct';

function openNav() {
  document.getElementById('mySidenav').style.width = '250px';
}

function closeNav() {
  document.getElementById('mySidenav').style.width = '0';
}

////////////////add the 360 img

const add360But = document.querySelector('.overlay360');
const modle = document.querySelector('.overlay');
const close = document.querySelectorAll('.close-modal');
const ok = document.querySelector('.ok');
const checkButtons = document.querySelectorAll('.button360');
const form = document.querySelector('.form');
const done = document.querySelector('.done');

let fees = 0;

add360But.addEventListener('click', function (e) {
  e.preventDefault();
  modle.classList.remove('none');
  document.querySelector('.blure').style.filter = 'blur(50px)';
});

close.forEach(btn => {
  btn.addEventListener('click', function (e) {
    e.preventDefault();
    modle.classList.add('none');
    document.querySelector('.blure').style.filter = 'blur(0)';
  });
});

ok.addEventListener('click', function (e) {
  e.preventDefault();
  checkButtons.forEach(button => {
    button.classList.add('none');
    form.classList.remove('none');
    done.classList.remove('none');
    fees += 100;
  });
});

done.addEventListener('click', function (e) {
  e.preventDefault();
  modle.classList.add('none');
  document.querySelector('.blure').style.filter = 'blur(0)';
});
