const SHOW_LOGIN_TIMEOUT = 400;
const HIDE_LOGIN_TIMEOUT = 200;
const SHOW_SIGNUP_TIMEOUT = 100;
const HIDE_SIGNUP_TIMEOUT = 400;
const HIDE_ALL_TIMEOUT = 500;

document.addEventListener("DOMContentLoaded", () => {
    initializeEvents();
});

function initializeEvents() {
    // Add event listeners for switch between login and signup forms
    document.querySelector('.login-button').addEventListener('click', change_to_login);
    document.querySelector('.sign-up-button').addEventListener('click', change_to_sign_up);
    document.querySelector('.hide-forms-button').addEventListener('click', hideLoginAndSignUp);
}

async function change_to_login() {
    switchActiveForm('cont_forms_active_login', '.cont_form_login', '.cont_form_sign_up', SHOW_LOGIN_TIMEOUT, HIDE_LOGIN_TIMEOUT);
}

async function change_to_sign_up() {
    switchActiveForm('cont_forms_active_sign_up', '.cont_form_sign_up', '.cont_form_login', SHOW_SIGNUP_TIMEOUT, HIDE_SIGNUP_TIMEOUT);
}

function switchActiveForm(activeFormClass, showSelector, hideSelector, showTimeout, hideTimeout) {
    document.querySelector('.cont_forms').className = `cont_forms ${activeFormClass}`;
    displayForm(showSelector, hideSelector, showTimeout, hideTimeout);
}

function displayForm(showSelector, hideSelector, showTimeout, hideTimeout) {
    document.querySelector(showSelector).style.display = "block";
    document.querySelector(hideSelector).style.opacity = "0";

    setTimeout(() => {
        document.querySelector(showSelector).style.opacity = "1";
    }, showTimeout);

    setTimeout(() => {
        document.querySelector(hideSelector).style.display = "none";
    }, hideTimeout);
}

function hideLoginAndSignUp() {
    document.querySelector('.cont_forms').className = "cont_forms";
    document.querySelector('.cont_form_sign_up').style.opacity = "0";
    document.querySelector('.cont_form_login').style.opacity = "0";

    setTimeout(() => {
        document.querySelector('.cont_form_sign_up').style.display = "none";
        document.querySelector('.cont_form_login').style.display = "none";
    }, HIDE_ALL_TIMEOUT);
}

async function PasswordCheck(password) {
    try {
        const response = await fetch("api/User/password", {
            method: "POST",
            headers: {
                'Content-Type': "application/json"
            },
            body: JSON.stringify(password),
        });
        const strength = await response.json();
        updatePasswordCheckIndicator(strength);
        return strength;
    } catch (error) {
        console.error("Error checking password", error);
        return null;
    }
}

function updatePasswordCheckIndicator(strength) {
    const colorIndicator = document.getElementById("passwordCheck");
    if (strength === 0) {
        colorIndicator.style.setProperty("background-color", "red");
    } else if (strength === 1) {
        colorIndicator.style.setProperty("background-color", "orange");
    } else if (strength >= 2) {
        colorIndicator.style.setProperty("background-color", "green");
        alert(strength);
    }
}