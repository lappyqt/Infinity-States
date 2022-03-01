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
    }, 1);

    localStorage.clear();
}

function appendEditorTools(): void {
    const tools: any = document.querySelectorAll(".tool");

    for (let tool of tools) {
        tool.addEventListener("mousedown", function() {
            const command = tool.dataset["element"];

            switch (command) {
                case "title": {
                    let selection = document.getSelection();
                    document.execCommand("insertHTML", false, `<h3>${selection}</h3>`);
                }

                default: document.execCommand(command, false, null);
            }
        });
    }
}

function setHiddenValue(currentElement: any, hiddenElement: any) {
    hiddenElement.value = currentElement.innerHTML;
}

function saveDraft(): void {
    const editorArticleDraft: object = {
		title: editor.elements[1].value,
		content: contentInput.innerHTML,    // Div with contentededible
    };
	
    localStorage.setItem("articleDraft", JSON.stringify(editorArticleDraft));
}

function loadDraft(): void {
    const draft = JSON.parse(localStorage.getItem("articleDraft"));

	editor.elements[1].value = draft.title;
	contentInput.innerHTML = draft.content;
}
