const editor: any = document.querySelector(".editorForm");
const contentInput: any = document.querySelector(".content");

function setHiddenValue(currentElement: any, hiddenElement: any) {
    hiddenElement.value = currentElement.innerHTML;
}