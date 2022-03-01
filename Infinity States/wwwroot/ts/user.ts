const form: any = document.querySelector(".form");

const outputError = (text) => document.querySelector(".errorOutput").textContent = text;

function getId(): string {
    let url = window.location.href;
    return url.substring(url.lastIndexOf('/') + 1);
}

const validateInputsOnEmpty = function (): boolean {
    let inputs = form.querySelectorAll("input.data");

    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].value == "") {
          outputError("Please fill in all fields 🖤");
          return false;
        }
    }
    outputError("");
    return true;
}
