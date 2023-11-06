import { navigateTo } from './index.js'

const accessTokenUrl = "http://localhost:1000/connect/token";

function login(){
    const username = document.getElementById("usernameInput").value;
    const password = document.getElementById("passwordInput").value;

    let dataPairs = []

    dataPairs.push(`${encodeURIComponent("username")}=${encodeURIComponent(username)}`);
    dataPairs.push(`${encodeURIComponent("password")}=${encodeURIComponent(password)}`);
    dataPairs.push(`${encodeURIComponent("grant_type")}=${encodeURIComponent("password")}`);
    dataPairs.push(`${encodeURIComponent("scope")}=${encodeURIComponent("Chat openid profile")}`);
    dataPairs.push(`${encodeURIComponent("client_id")}=${encodeURIComponent("JsId")}`);
    dataPairs.push(`${encodeURIComponent("client_secret")}=${encodeURIComponent("JsSecret")}`);

    let encodedData = dataPairs.join('&').replace(/%20/g, '+');

    const errorMessageContainer = document.querySelector(".auth_form-error");
    const xhr = new XMLHttpRequest();

    xhr.onerror = function() {
        errorMessageContainer.removeAttribute('hidden');
    }

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4){
            try{
                console.log(xhr.responseText)
                const token = JSON.parse(xhr.responseText).access_token;
                localStorage.setItem('accessToken', token);
                
            } catch(e) {
                errorMessageContainer.removeAttribute("hidden");
            }
        }
    }

    xhr.onload = function() {
        if(xhr.status != 200){
            errorMessageContainer.removeAttribute('hidden');
        }
        
        if(xhr.status == 200){
            disposeAuthorize();
            navigateTo('/');
        }
    }

    xhr.open("POST", accessTokenUrl, true)

    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.send(encodedData)
}

export const initializeAuthorize = () => {
    document.getElementById("login").addEventListener("click", login);
}

export const disposeAuthorize = () => {
    document.getElementById("login").removeEventListener("click", login)
}