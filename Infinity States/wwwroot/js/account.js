const form = document.querySelector(".form");

const outputError = (text) => document.querySelector(".errorOutput").textContent = text; 

function checkInputs() {
    let inputs = form.querySelectorAll("input.data");

    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].value == "") {
          outputError("To register, you must fill the entire floor");
          return false;
        }
    }
    outputError("");
    return true;
}

async function loadUserArticles() {
    const response = await (await fetch(window.location + "/FindUserArticles")).json();

    let i = response.length;
    while (i--) {
        const deleteIcon = document.createElement("img");
        deleteIcon.className = "elementIcon";
        deleteIcon.width = 16;
        deleteIcon.height = 16;
        deleteIcon.src = "../img/delete.svg";

        const editIcon = document.createElement("img");
        editIcon.className = "elementIcon";
        editIcon.width = 16;
        editIcon.height = 16;
        editIcon.src = "../img/edit.svg";

        const a = document.createElement("a");
        a.href = "/Articles/Article/" + response[i].id;
		a.innerText = response[i].title;

        const deleteButton = document.createElement("a");
        deleteButton.className = "linkButton";
        deleteButton.href = "/Account/DeleteArticle?id=" + response[i].id;
        deleteButton.append(deleteIcon);

        const editButton = document.createElement("a");
        editButton.className = "linkButton";
        editButton.href = "/Articles/Edit/" + response[i].id;
        editButton.append(editIcon);

		const li = document.createElement("li");
        li.className = "userArticle";
		li.appendChild(a);
        li.appendChild(deleteButton);
        li.appendChild(editButton);

		const ul = document.querySelector(".userArticles");
		ul.appendChild(li);
    }
}

async function loadUserData() {
    const id = currentUrl.split("/").pop();
    const response = await (await fetch(`/users/data/${id}`)).json();

    const username = document.querySelector(".username");
    username.innerText = response.username;
}

async function getCurrentUserArticles() {
    const ul = document.querySelector(".articlesList");
    const id = currentUrl.split("/").pop();
    const response = await (await fetch(`/users/articles/${id}`)).json();  

    for (let i = response.length - 1; i >= 0; i--) {
        const img = document.createElement("img");
        img.className = "articleImg";
        img.src = response[i].poster;

        const a = document.createElement("a");
        a.href = document.location.origin + "/Articles/Article/" + response[i].id;
        a.innerHTML = response[i].title;

        const p = document.createElement("p");
        p.appendChild(a);

        const li = document.createElement("li");
        li.append(img);
        li.append(p);
        ul.appendChild(li);
    }
}