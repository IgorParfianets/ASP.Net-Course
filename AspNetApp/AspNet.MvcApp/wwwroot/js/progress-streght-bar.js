﻿let password = document.getElementById("password");
let passwordStrength = document.getElementById("password-strength");
let problem = document.getElementById("test");

password.addEventListener("keyup", function () {

    let pass = document.getElementById("password").value;
    checkStrength(pass);
});

let stateClickEye = false;
function toggle() {
    if (stateClickEye) {
        document.getElementById("password").setAttribute("type", "password");
        stateClickEye = false;
    } else {
        document.getElementById("password").setAttribute("type", "text");
        stateClickEye = true;
    }
}

function myFunction(show) {
    show.classList.toggle("fa-eye-slash");
}

function checkStrength(password) {

    const strength = calcStrength(password);
    checkPasswordStrength(password, strength);
}

function calcStrength(password) {
    let strength = 0;

    //If password contains both lower and uppercase characters
    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) {
        strength += 1;
    }
    //If it has numbers and characters
    if (password.match(/([0-9])/)) {
        strength += 1;
    }
    //If it has one special character
    if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) {
        strength += 1;
    }
    //If password is greater than 7
    if (password.length > 7) {
        strength += 1;

    }
    return strength;
}

function checkPasswordStrength(password, strength) {

         problem.textContent = "Минимум 4 символа";
         problem.style.color = "#6C7073";
         passwordStrength.classList.remove("progress-bar-warning");
         passwordStrength.classList.remove("progress-bar-success");
         passwordStrength.classList.remove("progress-bar-great");
          passwordStrength.classList.remove("progress-bar-danger");
          passwordStrength.style = "width: 0%";
    if (strength == 1 && password.length > 3) {
        problem.textContent = "Слабый";
        problem.style.color = "#e90f10";
        passwordStrength.classList.remove("progress-bar-warning");
        passwordStrength.classList.remove("progress-bar-success");
        passwordStrength.classList.remove("progress-bar-great");
        passwordStrength.classList.add("progress-bar-danger");
        passwordStrength.style = "width: 25%";
    } else if (strength == 2 && password.length > 3) {
        problem.textContent = "Средний";
        problem.style.color = "#FF8C00";
        passwordStrength.classList.remove("progress-bar-success");
        passwordStrength.classList.remove("progress-bar-danger");
        passwordStrength.classList.remove("progress-bar-great");
        passwordStrength.classList.add("progress-bar-warning");
        passwordStrength.style = "width: 50%";
    } else if (strength == 3 && password.length > 3) {
        problem.textContent = "Сильный";
        problem.style.color = "#40E0D0";
        passwordStrength.classList.remove("progress-bar-warning");
        passwordStrength.classList.remove("progress-bar-danger");
        passwordStrength.classList.remove("progress-bar-great");
        passwordStrength.classList.add("progress-bar-success");
        passwordStrength.style = "width: 75%";
    } else if (strength == 4 && password.length > 3) {
        problem.textContent = "Очень сильный";
        problem.style.color = "#00ef00";
        passwordStrength.classList.remove("progress-bar-warning");
        passwordStrength.classList.remove("progress-bar-danger");
        passwordStrength.classList.remove("progress-bar-success");
        passwordStrength.classList.add("progress-bar-great");
        passwordStrength.style = "width: 100%";
    }
}