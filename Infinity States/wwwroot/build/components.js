class ToolPanel extends HTMLElement {
    constructor() {
        super();

        this.innerHTML = `
            <div class="tool-panel">
                <a title="Bold" class="tool" data-element="bold">B</a>
                <a title="Italic"class="tool" data-element="italic">I</a>
                <a title="Underline" class="tool" data-element="underline">U</a>
                <a title="Title" class="tool" data-element="title">T</a>
                <a title="Justify left" class="tool" data-element="justifyLeft">JL</a>
                <a title="Justify center" class="tool" data-element="justifyCenter">JC</a>
                <a title="Justify right" class="tool" data-element="justifyRight">JR</a>
                <a title="Justify full" class="tool" data-element="justifyFull">JF</a>
                <a title="Insert ordered list" class="tool" data-element="insertOrderedList">IO</a>
                <a title="Insert unordered list" class="tool" data-element="insertUnorderedList">IU</a>
            </div>
        `;

        if (this.getAttribute("enable-tools") == "true") {
            this.appendTools();
        }
    }

    appendTools() {
        const tools = this.querySelectorAll(".tool");

        for (let tool of tools) {
            tool.onmousedown = () => {
                const command = tool.dataset["element"];
    
                switch (command) {
                    case "title": {
                        let selection = document.getSelection();
                        document.execCommand("insertHTML", false, `<h3>${selection}</h3>`);
                    }
    
                    default: document.execCommand(command, false, null);
                }
            };
        }
    }
}

class ContentTypeSelect extends HTMLElement {
    constructor() {
        super();

        this.innerHTML = `
            <form action="" method="get">
                <div class="content-type-select">
                    <div class="line"></div>
                </div>
            </form>
        `;

        const types = this.getAttribute("types").split(";").reverse();
        const contentTypeSelect = this.querySelector(".content-type-select");

        for (let item of types) {
            const type = document.createElement("span");
            type.innerText = item;

            contentTypeSelect.prepend(type);
        }

        contentTypeSelect.children[0].className = "selected";
    }
}

class ImagePopup extends HTMLElement {
    constructor() {
        super();

        this.innerHTML = `
            <div class="image-popup">
                <img>
            </div>
        `;

        this.modal = new Public.Modal('.image-popup');

        this.popup = this.querySelector('.image-popup');
        this.popupImg = this.querySelector('.image-popup img');
    }

    connectedCallback() {
        this.popup.onclick = (event) => {
            const tagName = event.target.tagName;

            if (tagName != 'IMG' || tagName != 'BUTTON') {
                this.modal.hide();
            }
        }

        this.addEventsForImages();
    }

    showOriginal(src) {
        this.popupImg.src = src;
        this.modal.show();
    }

    addEventsForImages() {
        const images = document.querySelectorAll('.textblock img');

        for (let img of images) {
            img.onclick = () => this.showOriginal(img.getAttribute('src'));
        }
        
    }
}

customElements.define("tool-panel", ToolPanel);
customElements.define("content-type-select", ContentTypeSelect);
customElements.define("image-popup", ImagePopup);