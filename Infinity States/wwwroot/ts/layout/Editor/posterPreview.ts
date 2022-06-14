namespace Editor {
    export class PosterPreview {
        container: any;
        posterContainer: any;
        fileInput: any;

        constructor(container: any, posterContainer: any, fileInput: any) {
            this.container = container;
            this.posterContainer = posterContainer;
            this.fileInput = fileInput;
            
            if (fileInput.hasAttribute('value')) {
                this.appendPosterAsFile();
            }

            this.container.onchange = (event): void => {
                event.preventDefault();
                this.showPreview(this.fileInput.files[0]);
            }

            this.container.ondrop = (event): void => {
                event.preventDefault();
                
                const files = event.dataTransfer.files;
                this.fileInput.files = files;
                this.showPreview(files[0]);
            }
        }

        private showPreview(poster: any): void {
            this.posterContainer.setAttribute('src', URL.createObjectURL(poster));
        }

        private appendPosterAsFile(): void {
            const dataTransfer = new DataTransfer();
            const poster = new File([''], this.fileInput.getAttribute('value').replace('/files/images/', ''), {type: 'image/png'});
            dataTransfer.items.add(poster);
            this.fileInput.files = dataTransfer.files;
        }
    }
}