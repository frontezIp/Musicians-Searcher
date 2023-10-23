import { initializeAuthorize, disposeAuthorize } from './authorize.js'
import { startChat, disposeChat } from './chat.js'

export const navigateTo = url => {
    history.pushState(null, null, url);
    router();
}

const router = async() => {
    const routes = [
        { path: "/", view: () => chatHandler()},
        { path: "/login", view: () => loginHandler()}
    ];

    const potentialMatches = routes.map(route => {
        return {
            route: route,
            isMatch: location.pathname === route.path
        };
    });

    let match = potentialMatches.find(potentialMatch => potentialMatch.isMatch);

    if (!match){
        match = {
            route : routes[0],
            isMatch: true
        }
    }

    if (!isAuthenticated()){
        navigateTo('/login')
    }
    
    match.route.view()
}

const parseHtml = (htmlContent) => {
    const parser  = new DOMParser();
    const parsedHtml = parser.parseFromString(htmlContent, 'text/html')
    console.log(parsedHtml.body.innerHTML);
    return parsedHtml
}

const parseJWT = (token) => {
    var base64Url = token.split('.')[1];
    var base64 = decodeURIComponent(atob(base64Url).split('').map((c) => {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
    return JSON.parse(base64);
}

const isAuthenticated = () => {
    const token = localStorage.getItem('accessToken')
    if (token == "undefined" || token == undefined || token === null){
        return false;  
    }

    var parsedToken = parseJWT(token);
    const expirationTimestamp = parsedToken.exp * 1000;
    const currentTimestamp = Date.now();

    return currentTimestamp <= expirationTimestamp;
}

window.addEventListener("popstate", router);

document.addEventListener("DOMContentLoaded", () => {
    document.body.addEventListener("click", e => {
        console.log(e.target)
        if (e.target.matches("[data-link]")) {
            console.log('Hello')
            e.preventDefault();
            return;
        }
    })
    router();
})

const cleanPage = () => {
    document.body.innerHTML = '';
    const appDiv = document.createElement('div');
    appDiv.id = 'app';
    document.body.appendChild(appDiv)
}

const loginHandler = () => {
    fetch('authorize.html')
    .then(response => response.text())
    .then(htmlContent => {
        const parsedHtml = parseHtml(htmlContent);
        cleanPage();
        document.querySelector('#app').innerHTML = parsedHtml.body.innerHTML;
        document.body.className = "auth_body"
        initializeAuthorize();
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

const chatHandler = () => {
    fetch('chat.html')
    .then(response => response.text())
    .then(htmlContent => {
        const parsedHtml = parseHtml(htmlContent);
        cleanPage();
        document.body.innerHTML = parsedHtml.body.innerHTML
        document.body.className = "chat_body"
        startChat();
    })
    .catch(error => {
        console.error('Error:', error);
    })
}