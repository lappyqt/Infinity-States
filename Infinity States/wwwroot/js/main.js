const domainName = window.origin;
const currentUrl = document.location.href;
const input = document.getElementsByTagName("input")[2];

function httpGet(url, method = "Get", body = null) {
    const xhttp = new XMLHttpRequest();
    xhttp.open(method, url, false);
    xhttp.send(body);
    return xhttp.responseText; 
}