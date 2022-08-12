"use strict";
var Helpers;
(function (Helpers) {
    class FormHelper {
        constructor() { }
        static setHiddenValue(currentElement, hiddenElement) {
            hiddenElement.value = currentElement.innerHTML;
        }
    }
    Helpers.FormHelper = FormHelper;
})(Helpers || (Helpers = {}));
var Editor;
(function (Editor) {
    class PosterPreview {
        constructor(container, posterContainer, fileInput) {
            this.container = container;
            this.posterContainer = posterContainer;
            this.fileInput = fileInput;
            if (fileInput.hasAttribute('value')) {
                this.appendPosterAsFile();
            }
            this.container.onchange = (event) => {
                event.preventDefault();
                this.showPreview(this.fileInput.files[0]);
            };
            this.container.ondrop = (event) => {
                event.preventDefault();
                const files = event.dataTransfer.files;
                this.fileInput.files = files;
                this.showPreview(files[0]);
            };
        }
        showPreview(poster) {
            this.posterContainer.setAttribute('src', URL.createObjectURL(poster));
        }
        appendPosterAsFile() {
            const dataTransfer = new DataTransfer();
            const poster = new File([''], this.fileInput.getAttribute('value').replace('/files/images/', ''), { type: 'image/png' });
            dataTransfer.items.add(poster);
            this.fileInput.files = dataTransfer.files;
        }
    }
    Editor.PosterPreview = PosterPreview;
})(Editor || (Editor = {}));
var Public;
(function (Public) {
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
    Public.Modal = Modal;
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
    Public.CategoriesModal = CategoriesModal;
    class DropdownModal extends Modal {
        constructor(containerSource) {
            super(containerSource);
            this.containerSource.onmouseleave = () => this.hide();
        }
        toggle() {
            this.containerSource.classList.toggle("visible");
        }
    }
    Public.DropdownModal = DropdownModal;
})(Public || (Public = {}));
var Public;
(function (Public) {
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
    Public.LocalStorageManager = LocalStorageManager;
    class SessionStorageManager extends LocalStorageManager {
        saveItemJson(key, item) {
            this.checkItemOnNull(item);
            sessionStorage.setItem(key, JSON.stringify(item));
        }
        getItemJson(key) {
            return JSON.parse(sessionStorage.getItem(key));
        }
    }
    Public.SessionStorageManager = SessionStorageManager;
})(Public || (Public = {}));
var Public;
(function (Public) {
    let InputNames;
    (function (InputNames) {
        InputNames["Username"] = "username";
        InputNames["Mail"] = "mail";
        InputNames["Password"] = "password";
    })(InputNames = Public.InputNames || (Public.InputNames = {}));
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
            let message = "Please enter a username <br>";
            if (input.value.length < 1) {
                this.addErrorLogLine(message);
                return false;
            }
            this.removeErrorLogLine(message);
            return true;
        }
        emailValidation(input) {
            const regularExpression = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
            let message = "Please enter a valid mail <br>";
            if (!input.value.match(regularExpression)) {
                this.addErrorLogLine(message);
                return false;
            }
            this.removeErrorLogLine(message);
            return true;
        }
        passwordValidation(input) {
            const regularExpression = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z\\!?@#$%^&*+-]{6,}$/;
            let message = "Password min 6 characters (1 upper case and number) <br>";
            if (!input.value.match(regularExpression)) {
                this.addErrorLogLine(message);
                return false;
            }
            this.removeErrorLogLine(message);
            return true;
        }
    }
    Public.FormValidation = FormValidation;
})(Public || (Public = {}));
