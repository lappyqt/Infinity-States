"use strict";
const authorLink = document.querySelector(".author-link");
const scrollToTop = () => window.scrollTo({ top: 0, behavior: "smooth" });
function onScroll() {
    const up = document.querySelector(".up");
    if (window.scrollY > 0) {
        up.style.display = "block";
    }
    else {
        up.style.display = "none";
    }
}
const contentInput = document.querySelector(".content");
function setHiddenValue(currentElement, hiddenElement) {
    hiddenElement.value = currentElement.innerHTML;
}
class PosterPreview {
    constructor() { }
    static showPreview(poster, fileInput) {
        document.querySelector('img.poster').setAttribute('src', URL.createObjectURL(poster));
    }
    static openExplorer(clickableItem, fileInput) {
        if (fileInput.type == "file" && fileInput != null) {
            clickableItem.onclick = () => fileInput.click();
        }
        else {
            throw new Error("Input type is not a 'file' or input equals null");
        }
    }
    static showPreviewOnDrop(dropPlace, fileInput) {
        dropPlace.ondrop = (event) => {
            event.preventDefault();
            const files = event.dataTransfer.files;
            fileInput.files = files;
            this.showPreview(files[0], fileInput);
        };
    }
    static clearFileInput(fileInput) {
        fileInput.value = null;
    }
}
const form = document.querySelector(".form");
const outputError = (text) => document.querySelector(".errorOutput").textContent = text;
function getId() {
    let url = window.location.href;
    return url.substring(url.lastIndexOf('/') + 1);
}
const validateInputsOnEmpty = function () {
    let inputs = form.querySelectorAll("input.data");
    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].value == "") {
            outputError("Please fill in all fields 🖤");
            return false;
        }
    }
    outputError("");
    return true;
};
class Modal {
    constructor(containerSource) {
        this.containerSource = document.querySelector(containerSource);
    }
    show() {
        this.containerSource.classList.add("visible");
    }
    hide() {
        this.containerSource.classList.remove("visible");
    }
}
class CategoriesModal extends Modal {
    constructor(containerSource, categories) {
        super(categories.containerSource);
        this.containerSource = document.querySelector(containerSource);
        this.categories = document.querySelector(categories);
    }
    show() {
        super.show();
        this.containerSource.style.top = `${window.scrollY}px`;
        document.body.style.overflow = "hidden";
    }
    hide() {
        super.hide();
        document.body.style.overflow = "visible";
    }
    selectedCategory() {
        let selectedCategory;
        let childrens = Array.prototype.slice.call(this.categories.children);
        for (let category of childrens) {
            if (category.checked == true) {
                selectedCategory = category;
            }
        }
        return selectedCategory;
    }
}
class DropdownModal extends Modal {
    constructor(containerSource) {
        super(containerSource);
        this.containerSource.onmouseleave = () => this.hide();
    }
    toggle() {
        this.containerSource.classList.toggle("visible");
    }
}
class LocalStorageManager {
    get storage() {
        return localStorage;
    }
    checkItemOnNull(item) {
        if (typeof (item) != 'object' || item == null) {
            throw new Error('Item is not an object or is equals to null');
        }
    }
    saveItemJson(key, item) {
        this.checkItemOnNull(item);
        localStorage.setItem(key, JSON.stringify(item));
    }
    getItemJson(key) {
        return JSON.parse(localStorage.getItem(key));
    }
}
class SessionStorageManager extends LocalStorageManager {
    saveItemJson(key, item) {
        this.checkItemOnNull(item);
        sessionStorage.setItem(key, JSON.stringify(item));
    }
    getItemJson(key) {
        return JSON.parse(sessionStorage.getItem(key));
    }
}
var InputNames;
(function (InputNames) {
    InputNames["Username"] = "username";
    InputNames["Mail"] = "mail";
    InputNames["Password"] = "password";
})(InputNames || (InputNames = {}));
class FormValidation {
    constructor(form, errorLogId) {
        this.form = form;
        this.errorLog = document.getElementById(errorLogId);
    }
    addErrorLogLine(message) {
        this.errorLog.innerHTML += message;
    }
    removeErrorLogLine(message) {
        this.errorLog.innerHTML = this.errorLog.innerHTML.replace(message, "");
    }
    validate() {
        let input;
        let validateProcess = [];
        this.errorLog.innerHTML = "";
        for (input of this.form.elements) {
            if (input.hasAttribute('name')) {
                if (input.name == InputNames.Mail)
                    validateProcess.push(this.emailValidation(input));
                if (input.name == InputNames.Username)
                    validateProcess.push(this.nullValidation(input));
                if (input.name == InputNames.Password)
                    validateProcess.push(this.passwordValidation(input));
            }
        }
        if (!validateProcess.includes(false)) {
            return true;
        }
        return false;
    }
    nullValidation(input) {
        let message = "🔥 Please enter a username <br>";
        if (input.value.length < 1) {
            this.addErrorLogLine(message);
            return false;
        }
        this.removeErrorLogLine(message);
        return true;
    }
    emailValidation(input) {
        const regularExpression = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
        let message = "🔥 Please enter a valid mail <br>";
        if (!input.value.match(regularExpression)) {
            this.addErrorLogLine(message);
            return false;
        }
        this.removeErrorLogLine(message);
        return true;
    }
    passwordValidation(input) {
        const regularExpression = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z\\!?@#$%^&*+-]{6,}$/;
        let message = "🔥 Password min 6 characters (1 upper case and number) <br>";
        if (!input.value.match(regularExpression)) {
            this.addErrorLogLine(message);
            return false;
        }
        this.removeErrorLogLine(message);
        return true;
    }
}
