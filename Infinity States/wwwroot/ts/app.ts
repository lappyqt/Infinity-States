const domainName: string = window.origin;
const currentUrl: string = document.location.href;
const currentSearch: string = document.location.search; 

setTimeout(function () {
    const menu: any = document.querySelector(".menu");
    menu.onclick = () => openMenu();
}, 1);

function httpGet(url, method = "Get", body = null) {
    const xhttp = new XMLHttpRequest();
    xhttp.open(method, url, false);
    xhttp.send(body);
    return xhttp.responseText; 
}

function openMenu() {
    const header: any = document.querySelector("header");
    const links: any = document.querySelector(".links");
    const logo: any = document.querySelector(".logo");

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