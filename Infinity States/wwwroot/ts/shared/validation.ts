/*  
    Идея в том, чтобы последовательно проходиться по элементам формы,
    узнавать тип инпута и проводить валидацию по соответствующему типу,
    например: если у инпута тип password, то метод валидации validatePassword().
    
    В случае ошибки менять класс например на .error
*/

enum InputTypes {
    "Text" = "text",
    "Email" = "email",
    "Password" = "password"
}

interface IFormValidation {
    validate(): boolean;
    nullValidation(input: any): boolean;
    emailValidation(input: any): boolean;
    passwordValidation(input: any): boolean;
}

class FormValidation implements IFormValidation {
    public form: any;

    constructor(form: any) {
        this.form = form;
    }

    public validate(): boolean {
        let input: any;

        /*
            Когда я допишу код, здесь может быть такая ситуация,
            что если например пароль введен, а имя пользователя нет,
            то функция validate все равно вернет true т.к. input пароля идет последним...

            В этом случае необходимо добавтить массив, куда будут добавляться результаты каждой валидации (надо еще и выполнять данный метод, чтобы он выводил пользователю ошибку)
            Ну и в конце я просто проверяю если есть хоть один false, то валидация не будет пройдена
        */

        for (input of form.elements) {
            switch (input.type) {
                case InputTypes.Text: return this.nullValidation(input); 
                case InputTypes.Email: return this.emailValidation(input); 
                case InputTypes.Password: return this.passwordValidation(input); 
            }
        }
    }

    nullValidation(input: any): boolean {
        if (input.value.length < 1) {
            return false;
        }

        return true;
    }

    emailValidation(input: any): boolean {
        return
    }

    passwordValidation(input: any): boolean {
        const regularExpression = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z\\!?@#$%^&*+-]{6,}$/;

        if (!input.value.match(regularExpression)) {
            return false;
        }

        return true;
    }
}