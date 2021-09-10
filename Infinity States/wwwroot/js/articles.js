const poster = document.querySelector(".poster");
const title = document.querySelector(".title");
const content = document.querySelector(".content");
const up = document.querySelector(".up");

const scrollToTop = () => window.scrollTo({top: 0, behavior: "smooth"});

function getArticleId() {
    let url = window.location.href;
    return url.substring(url.lastIndexOf('=') + 1);
}

function loadArticle() {
    let url = "/Articles/Read?id=" + getArticleId();
    let data = httpGet(url);
    data = data.split("|");
    
    poster.src = data[0];
    title.textContent = data[1];
    content.innerHTML = data[2];
}

function getAllArticles() {
    const url = document.location.origin + "/Articles/GetAll";
    const response = httpGet(url);
    const jsonString = JSON.parse(response);
    return jsonString;
}

function loadAllArticles() {
    const ul = document.getElementsByClassName("articlesList")[0];
    const articles = getAllArticles();

    for (let i = articles.length - 1; i >= 0; i--) {
        const img = document.createElement("img");
        img.className = "articleImg";
        img.src = articles[i].poster;

        const a = document.createElement("a");
        a.href = document.location.origin + "/Articles/Article?id=" + articles[i].id;
        a.innerHTML = articles[i].title;

        const p = document.createElement("p");
        p.appendChild(a);

        const li = document.createElement("li");
        li.append(img);
        li.append(p);
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