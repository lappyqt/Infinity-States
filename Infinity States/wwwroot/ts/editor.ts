const editor: any = document.querySelector(".editorForm");
const contentInput: any = document.querySelector(".content");

function autoGrow() {
    contentInput.style.height = "auto";
    contentInput.style.height = `${contentInput.scrollHeight}px`;
}

function publish() {
    if (editor.elements[1].value === "" || editor.elements[2].value === "") {
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