const contentInput: any = document.querySelector(".content");

function setHiddenValue(currentElement: any, hiddenElement: any): void {
    hiddenElement.value = currentElement.innerHTML;
}

class PosterPreview {
    constructor() {}

    public static showPreview(poster: any, fileInput: any) {
        document.querySelector('img.poster').setAttribute('src', URL.createObjectURL(poster));
    }

    public static openExplorer(clickableItem: any, fileInput: any): void {
        if (fileInput.type == "file" && fileInput != null) {
            clickableItem.onclick = () => fileInput.click();
        } 
        else {
            throw new Error("Input type is not a 'file' or input equals null");
        }
    }

    public static showPreviewOnDrop(dropPlace: any, fileInput: any): void {
        dropPlace.ondrop = (event): void => {
            event.preventDefault();

            const files = event.dataTransfer.files
            fileInput.files = files;
            this.showPreview(files[0], fileInput);
        } 
    }

    public static clearFileInput(fileInput: any): void {
        fileInput.value = null;
    }
}