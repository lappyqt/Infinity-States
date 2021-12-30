const poster: any = document.querySelector(".poster");
const title: any = document.querySelector(".title");
const content: any = document.querySelector(".content");
const authorLink: any = document.querySelector(".author-link");
const up: any = document.querySelector(".up");

const scrollToTop = () => window.scrollTo({top: 0, behavior: "smooth"});

function onScroll() {
    if (window.scrollY > 0) {
        up.style.display = "block";
    }
    else {
        up.style.display = "none";
    }
}
