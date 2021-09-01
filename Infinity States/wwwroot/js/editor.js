const editor = document.querySelector(".editorForm");
const content = document.querySelector(".content");

window.addEventListener("load", loadDraft);
window.addEventListener("beforeunload", saveDraft);

content.addEventListener("input", autoGrow);
editor.addEventListener("submit", publish);

function initialize() {
    content.style.height = "auto";
    content.rows = 10;
}

function autoGrow() {
    content.style.height = "auto";
    content.style.height = `${content.scrollHeight}px`;
}

function publish() {
    if (editor.elements[1].value === "" && editor.elements[2].value === "") {
        editor.action = "Editor";
        alert("Please enter a value");
    }

    setTimeout(() => {
        editor.elements[1].value = "";
        editor.elements[2].value = "";

        localStorage.setItem("title", "");
        localStorage.setItem("content", "");
    }, 1);
}

function saveDraft() {
    localStorage.setItem("title", editor.elements[1].value);
    localStorage.setItem("content", editor.elements[2].value);
}

function loadDraft() {
    editor.elements[1].value = localStorage.getItem("title");
    editor.elements[2].value = localStorage.getItem("content");
}

initialize();