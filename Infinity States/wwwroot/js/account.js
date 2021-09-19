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
    const response = await JSON.parse(httpGet(window.location + "/FindUserArticles"));

    for (let i = 0; i < response.length; i++) {
        const a = document.createElement("a");
        a.href = "/Articles/Article?id=" + response[i].id;
		a.innerText = response[i].title;

        const deleteButton = document.createElement("a");
        deleteButton.className = "linkButton";
        deleteButton.innerText = "Delete";
        deleteButton.href = "/Account/DeleteArticle?id=" + response[i].id;

        const editButton = document.createElement("a");
        editButton.className = "linkButton";
        editButton.innerText = "Edit";
        editButton.href = "/Articles/Edit?id=" + response[i].id;

		const li = document.createElement("li");
        li.className = "userArticle";
		li.appendChild(a);
        li.appendChild(deleteButton);
        li.appendChild(editButton);

		const ul = document.querySelector(".userArticles");
		ul.appendChild(li);
    }
}