$(document).ready(function () {
    $('#eye-1').click(function () {
        $(this).toggleClass('show');
        $(this).children('i').toggleClass('fa-eye-slash fa-eye');
        if ($(this).hasClass('show')) {
            $('#password-1').attr('type', 'text');
        }
        else {
            $('#password-1').attr('type', 'password');
        }
    });
    $('#eye-2').click(function () {
        $(this).toggleClass('show');
        $(this).children('i').toggleClass('fa-eye-slash fa-eye');
        if ($(this).hasClass('show')) {
            $('#password-2').attr('type', 'text');
        }
        else {
            $('#password-2').attr('type', 'password');
        }
    });
});
const passwordField = document.getElementById('password-1');
const confirmPasswordField = document.getElementById('password-2');
const usernameField = document.getElementById('username');
const firstnameField = document.getElementById('firstname');
const lastnameField = document.getElementById('lastname');
const emailField = document.getElementById('email');
const warning = document.querySelector(".text-danger");

passwordField.addEventListener("input", validateForm);
confirmPasswordField.addEventListener("input", validateForm);
usernameField.addEventListener("input", validateForm);
firstnameField.addEventListener("input", validateForm);
lastnameField.addEventListener("input", validateForm);
emailField.addEventListener("input", validateForm);

const submitButton = document.querySelector(".form-submit");

function validateForm() {
    const password = passwordField.value;
    const confirmPassword = confirmPasswordField.value;
    const username = usernameField.value;
    const firstname = firstnameField.value;
    const lastname = lastnameField.value;
    const email = emailField.value;
    if (password !== "" && confirmPassword !== "" && username !== "" && firstname !== "" && lastname !== "" && email !== "") {

        submitButton.classList.add("form-submit-after")
        submitButton.disabled = false;
        warning.style.display = "none";
    }
    else {
        submitButton.classList.remove("form-submit-after")
        submitButton.disabled = true;
        warning.style.display = "block";
    }
}
