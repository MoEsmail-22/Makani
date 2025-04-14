"use strict";

const slideRight = document.querySelector(".toRight");
const slideLeft = document.querySelector(".toLeft");
const slides = document.querySelectorAll(".slide");
let count = 0;

// Get slide width dynamically
function getSlideWidth() {
  return slides[0].offsetWidth;
}

function updateSlider() {
  const widthSlide = getSlideWidth();
  slides.forEach((slide) => {
    slide.style.transition = ".6s ease-out";
    slide.style.transform = `translateX(${-count * widthSlide}px)`;
  });
}

slideRight.addEventListener("click", function () {
  count = (count + 1) % slides.length;
  updateSlider();
});

slideLeft.addEventListener("click", function () {
  count = (count - 1 + slides.length) % slides.length;
  updateSlider();
});

// Recalculate slide width on window resize
window.addEventListener("resize", updateSlider);

function updateSlider() {
  slides.forEach(slide => {
    slide.style.transition = '.6s ease-out';
    slide.style.transform = `translateX(${-count * widthSlide}px)`;
  });
}

function openNav() {
  document.getElementById('mySidenav').style.width = '250px';
}


function updateSlider() {
  slides.forEach(slide => {
    slide.style.transition = '.6s ease-out';
    slide.style.transform = `translateX(${-count * widthSlide}px)`;
  });
}

function openNav() {
  document.getElementById('mySidenav').style.width = '250px';
}

function closeNav() {
  document.getElementById('mySidenav').style.width = '0';
}


function closeNav() {
  document.getElementById('mySidenav').style.width = '0';
}

