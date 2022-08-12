namespace Public {
    export enum InputNames {
        "Username" = "username",
        "Mail" = "mail",
        "Password" = "password",
    }
    
    export interface IFormValidation {
        validate();
        nullValidation(input: any): boolean;
        emailValidation(input: any): boolean;
        passwordValidation(input: any): boolean;
    }
    
    export class FormValidation implements IFormValidation {
        public form: any;
        private errorLog: any;
    
        constructor(form: any, errorLogId: string) {
            this.form = form;
            this.errorLog = document.getElementById(errorLogId);
        }
    
        private addErrorLogLine(message: string) {
            this.errorLog.innerHTML += message;
        }
    
        private removeErrorLogLine(message: string) {
            this.errorLog.innerHTML = this.errorLog.innerHTML.replace(message, "");
        }
    
        public validate() {
            let input: any;
            let validateProcess = [];
    
            this.errorLog.innerHTML = "";
    
            for (input of this.form.elements) {
                if (input.hasAttribute('name')) {
                    if (input.name == InputNames.Mail) validateProcess.push(this.emailValidation(input));
                    if (input.name == InputNames.Username) validateProcess.push(this.nullValidation(input));
                    if (input.name == InputNames.Password) validateProcess.push(this.passwordValidation(input));
                }
            }
    
            if (!validateProcess.includes(false)) {
                return true;
            }
    
            return false;
        }
    
        public nullValidation(input: any): boolean {
            let message = "Please enter a username <br>";
    
            if (input.value.length < 1) {
                this.addErrorLogLine(message);
                return false;
            }
    
            this.removeErrorLogLine(message);
            return true;
        }
    
        public emailValidation(input: any): boolean {
            const regularExpression = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
            let message = "Please enter a valid mail <br>";
    
            if (!input.value.match(regularExpression)) {
                this.addErrorLogLine(message);
                return false;
            }
    
            this.removeErrorLogLine(message);
            return true;
        }
    
        public passwordValidation(input: any): boolean {
            const regularExpression = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z\\!?@#$%^&*+-]{6,}$/;
            let message = "Password min 6 characters (1 upper case and number) <br>";
    
            if (!input.value.match(regularExpression)) {
                this.addErrorLogLine(message);
                return false;
            }
    
            this.removeErrorLogLine(message);
            return true;
        }
    }
}