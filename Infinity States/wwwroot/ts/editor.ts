const editor: any = document.querySelector(".editorForm");
const contentInput: any = document.querySelector(".content");

function publish(): void {
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

function appendEditorTools(): void {
    const tools: any = document.querySelectorAll(".tool");

    tools.forEach(tool => {
        tool.addEventListener("mousedown", () => {
            let command = tool.dataset["element"];
            document.execCommand(command, false, null);     
        });
    });
}

function setHiddenValue(currentElement: any, hiddenElement: any) {
    hiddenElement.value = currentElement.innerHTML;
}

function saveDraft(): void {
    localStorage.setItem("title", editor.elements[1].value);
    localStorage.setItem("content", editor.elements[2].value);
}

function loadDraft(): void {
    editor.elements[1].value = localStorage.getItem("title");
    editor.elements[2].value = localStorage.getItem("content");
}