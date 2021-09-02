const form = document.querySelector(".form");

const outputError = (text) => document.querySelector(".errorOutput").textContent = text; 

function httpGet(url, method = "Get", body = null) {
    let xhttp = new XMLHttpRequest();
    xhttp.open(method, url, false);
    xhttp.send(body);
    return xhttp.responseText; 
}

function checkInputs() {
    let inputs = form.querySelectorAll("input.data");

    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].value == "") {
          outputError("To register, you must fill the entire floor");
          return false;
        }
    }
    outputError("");
    return true;
}

function loadUserArticles() {
    let response = JSON.parse(httpGet(window.location + "/FindUserArticles"));

    for (let i = 0; i < response.length; i++) {
        const a = document.createElement("a");
		a.innerText = response[i].title;

		const li = document.createElement("li");
		li.appendChild(a);

		const ul = document.querySelector(".userArticles");
		ul.appendChild(li);
    }
}