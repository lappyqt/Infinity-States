namespace Helpers {
    export class FormHelper {
        constructor() {}
    
        public static setHiddenValue(currentElement: any, hiddenElement: any): void {
            hiddenElement.value = currentElement.innerHTML;
        }
    }
}