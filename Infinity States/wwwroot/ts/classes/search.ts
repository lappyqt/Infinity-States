class Search {
    public container: any;

    constructor(container: any) {
        this.container = container;

        // Event listeners
        this.container.onblur = () => this.hide();
    }

    protected hideHeaderItems(): void {
        const headerItems: any = document.querySelectorAll(".hd-item");

        for (let item of headerItems) {
            item.style.display = "none";
        }
    }

    protected showHeaderItems(): void {
        const headerItems: any = document.querySelectorAll(".hd-item");

        for (let item of headerItems) {
            item.style = "";
        }
    }

    protected show(): void {
        this.hideHeaderItems();
        this.container.focus();
        this.container.classList.add("visible");
    }

    protected hide(): void {
        this.showHeaderItems();
        this.container.classList.remove("visible");
    }
}