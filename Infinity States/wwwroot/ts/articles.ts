const authorLink: any = document.querySelector(".author-link");

const scrollToTop = () => window.scrollTo({top: 0, behavior: "smooth"});

function onScroll() {
    const up: any = document.querySelector(".up");

    if (window.scrollY > 0) {
        up.style.display = "block";
    }
    else {
        up.style.display = "none";
    }
}