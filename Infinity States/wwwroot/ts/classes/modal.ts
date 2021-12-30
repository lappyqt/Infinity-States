interface IModal {
    containerSource: any;
    show();
    hide();
}

class Modal implements IModal {
    public containerSource: any;

    constructor(containerSource) {
        this.containerSource = containerSource;
    }

    show() {
        this.containerSource.classList.add("open");
    }

    hide() {
        this.containerSource.classList.remove("open");
    }
}

class CategoriesModal extends Modal {
    public containerSource: any;
    public categories: any;

    constructor(containerSource, categories) {
        super(categories.containerSource);

        this.containerSource = containerSource;
        this.categories = categories;
    }

    selectedCategory() {
        let selectedCategory;
        let childrens = Array.prototype.slice.call(this.categories.children);

        childrens.forEach(category => {
            if (category.checked == true) {
                selectedCategory = category;
            }
        });

        return selectedCategory;
    }
}