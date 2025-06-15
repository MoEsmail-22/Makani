const slideRight = document.querySelector(".toRight");
const slideLeft = document.querySelector(".toLeft");
const slides = document.querySelectorAll(".slide");
const widthSlide = 780;
const totalSlides = slides.length;
let count = 0;

slideRight.addEventListener("click", function () {
  count = (count + 1) % totalSlides;
  updateSlider();
});

slideLeft.addEventListener("click", function () {
  count = (count - 1 + totalSlides) % totalSlides;
  updateSlider();
});

function updateSlider() {
  slides.forEach((slide) => {
    slide.style.transition = ".6s ease-out";
    slide.style.transform = `translateX(${-count * widthSlide}px)`;
  });
}

function openNav() {
  document.getElementById("mySidenav").style.width = "250px";
}

function closeNav() {
  document.getElementById("mySidenav").style.width = "0";
}

/* To top button */
const toTop = document.querySelector(".toTop");

this.addEventListener("scroll", _ => {
  if (scrollY >= 100)
    toTop.classList.add("active");
  else
    toTop.classList.remove("active");
})
