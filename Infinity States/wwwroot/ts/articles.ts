const poster: any= document.querySelector(".poster");
const title: any = document.querySelector(".title");
const content: any = document.querySelector(".content");
const authorLink: any = document.querySelector(".authorLink");
const up: any = document.querySelector(".up");

const scrollToTop = () => window.scrollTo({top: 0, behavior: "smooth"});

var Vue: any = Vue; 

Vue.component("relay", {
    props: {
        "value": String,
        "action": String 
    },

    template: `
        <a class="filter" v-on:click="changeFilter">{{ value }}</a>
    `,
    methods: {
        changeFilter : function() {
            document.querySelector(".articlesList").innerHTML = "";
            loadArticles(this.action);
        }
    }
});

new Vue({el: ".filters"});

class Article {
    id: number;
    poster: string;
    title: string;
    content: string;
    authorId: number;
    author: string;
    category: number;

    constructor(id, poster, title, content, authorId, author) {
        this.id = id;
        this.poster = poster;
        this.title = title;
        this.content = content;
        this.authorId = authorId;
        this.author = author;
    }

    static GetId() {
        let url = window.location.href;
        return url.substring(url.lastIndexOf('/') + 1);
    }

    static Load = async () => {
        const response = await (await fetch("/Articles/Read/" + Article.GetId())).json(); 

        poster.src = response.poster;
        title.textContent = response.title;
        content.innerHTML = response.content;
    
        authorLink.innerHTML = response.author;
        authorLink.href = `/Users/${response.authorId}`;
    }

    static LoadWithEdit = async () => {
        const data = await (await fetch("/Articles/Read/" + Article.GetId())).json(); 

        poster.value = data.poster;
        title.value = data.title;
        content.innerHTML = data.content;
    }

    Create(parent: any) {
        const img = document.createElement("img");
        img.className = "articleImg";
        img.src = this.poster;

        const a = document.createElement("a");
        a.href = document.location.origin + "/Articles/Article/" + this.id;
        a.innerHTML = this.title;

        const author = document.createElement("a");
        author.className = "authorLink";
        author.href = "/Users/" + this.authorId;
        author.innerHTML = this.author;

        const p = document.createElement("p");
        p.appendChild(a);

        const li = document.createElement("li");
        li.append(img);
        li.append(p);
        li.append(author);
        parent.appendChild(li);
    }
}

function loadArticleData() {
    return JSON.parse(httpGet("/Articles/Read/" + Article.GetId()));
}

async function loadArticles(action = "/Articles/GetAll") {
    const ul = document.querySelector("ul");
    const articles = await (await fetch(domainName + action)).json();

    for (let i = articles.length - 1; i >= 0; i--) {
        const article = new Article(articles[i].id, articles[i].poster, articles[i].title, articles[i].content, articles[i].authorId, articles[i].author);
        article.Create(ul);
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
