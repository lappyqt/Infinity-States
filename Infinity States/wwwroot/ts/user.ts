const form : any = document.querySelector(".form");

const outputError = (text) => document.querySelector(".errorOutput").textContent = text;

function getId() {
    let url = window.location.href;
    return url.substring(url.lastIndexOf('/') + 1);
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