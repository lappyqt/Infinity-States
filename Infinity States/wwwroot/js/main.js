const domainName = window.origin;
const currentUrl = document.location.href;
const currentSearch = document.location.search; 

setTimeout(function () {
    const menu = document.querySelector(".menu");
    menu.onclick = () => openMenu();
}, 1);

function httpGet(url, method = "Get", body = null) {
    const xhttp = new XMLHttpRequest();
    xhttp.open(method, url, false);
    xhttp.send(body);
    return xhttp.responseText; 
}

function openMenu() {
    const header = document.querySelector("header");
    const links = document.querySelector(".links");
    const logo = document.querySelector(".logo");

    if (header.classList.contains("opened")) {
        header.classList.remove("opened");
        header.style.height = "52px";
        links.classList.remove("mobile");
        links.style.display = "";
        logo.style.display = "";
    }

    else {
        header.classList.add("opened");
        header.style.height = "130px";
        links.classList.add("mobile");
        links.style.display = "flex";
        logo.style.display = "none";
    }
}