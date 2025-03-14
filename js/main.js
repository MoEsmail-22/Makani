'use strict';

const slideRight = document.querySelector('.toRight');
const slideLeft = document.querySelector('.toLeft');
const widthSlide = 780;
let count = 1;

slideRight.addEventListener('click', function () {
  document.querySelectorAll('.slide').forEach(slide => {
    slide.style.transition = `  .6s ease-out`;
    slide.style.transform = `translateX(${count * -widthSlide}px)`;
  });
  count++;
});

slideLeft.addEventListener('click', function () {
  document.querySelectorAll('.slide').forEach(slide => {
    slide.style.transition = `  .6s ease-out`;
    slide.style.transform = `translateX(${widthSlide}px)`;
  });
});
