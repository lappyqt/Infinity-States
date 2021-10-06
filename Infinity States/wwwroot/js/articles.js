const poster = document.querySelector(".poster");
const title = document.querySelector(".title");
const content = document.querySelector(".content");
const authorLink = document.querySelector(".authorLink");
const up = document.querySelector(".up");

const scrollToTop = () => window.scrollTo({top: 0, behavior: "smooth"});

function getArticleId() {
    let url = window.location.href;
    return url.substring(url.lastIndexOf('/') + 1);
}

function loadArticleData() {
    return JSON.parse(httpGet("/Articles/Read/" + getArticleId()));
}

async function loadArticle() {
    let response = await (await fetch("/Articles/Read/" + getArticleId())).json(); 

    poster.src = response.poster;
    title.textContent = response.title;
    content.innerHTML = response.content;

    authorLink.innerHTML = response.author;
    authorLink.href = `/Users/${response.authorId}`;
}

async function loadArticleWithEdit() {
    let data = await (await fetch("/Articles/Read/" + getArticleId())).json(); 

    poster.value = data.poster;
    title.value = data.title;
    content.innerHTML = data.content;
}

async function loadAllArticles() {
    const ul = document.getElementsByClassName("articlesList")[0];
    const articles = await (await fetch(domainName + "/Articles/GetAll")).json();

    for (let i = articles.length - 1; i >= 0; i--) {
        const img = document.createElement("img");
        img.className = "articleImg";
        img.src = articles[i].poster;

        const a = document.createElement("a");
        a.href = document.location.origin + "/Articles/Article/" + articles[i].id;
        a.innerHTML = articles[i].title;

        const author = document.createElement("a");
        author.className = "authorLink";
        author.href = "/users/" + articles[i].authorId;
        author.innerHTML = articles[i].author;

        const p = document.createElement("p");
        p.appendChild(a);

        const li = document.createElement("li");
        li.append(img);
        li.append(p);
        li.append(author);
        ul.appendChild(li);
    }
}

function onScroll() {
    if (window.scrollY > 0) {
        up.style.display = "block";
    }
    else {
        up.style.display = "none";
    }
}