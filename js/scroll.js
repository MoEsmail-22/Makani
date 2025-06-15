const toTop = document.querySelector(".toTop");

this.addEventListener("scroll", _ => {
    if (scrollY >= 100)
        toTop.style.opacity = 1;
    else
        toTop.style.opacity = 0;
})