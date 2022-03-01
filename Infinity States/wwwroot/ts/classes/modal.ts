// To-do change "active" to "visible" 

interface IModal {
    containerSource: any;
    show(): void;
    hide(): void;
}

class Modal implements IModal {
    public containerSource: any;

    constructor(containerSource) {
        this.containerSource = containerSource;
    }

    public show(): void {
        this.containerSource.classList.add("active");
    }

    public hide(): void {
        this.containerSource.classList.remove("active");
    }
}

class CategoriesModal extends Modal implements IModal {
    public containerSource: any;
    public categories: any;

    constructor(containerSource, categories) {
        super(categories.containerSource);

        this.containerSource = containerSource;
        this.categories = categories;
    }

    override show(): void {
        super.show();

        this.containerSource.style.top = `${window.scrollY}px`;
        document.body.style.overflow = "hidden";
    }

    override hide(): void {
        super.hide();
        document.body.style.overflow = "visible";
    }

    protected selectedCategory(): any {
        let selectedCategory: any;
        let childrens = Array.prototype.slice.call(this.categories.children);

        for (let category of childrens) {
            if (category.checked == true) {
                selectedCategory = category;
            }
        }

        return selectedCategory;
    }
}

class DropdownModal extends Modal implements IModal {
    public containerSource: any;

    constructor(containerSource) {
        super(containerSource);
        this.containerSource.onmouseleave = () => this.hide();
    }

    public toggle(): void {
        this.containerSource.classList.toggle("active");
    }
}