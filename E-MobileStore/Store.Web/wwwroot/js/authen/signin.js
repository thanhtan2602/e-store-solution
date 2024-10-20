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
});
const passwordField = document.getElementById('password-1');
const usernameField = document.getElementById('username');
//signup
passwordField.addEventListener("input", validateForm);
usernameField.addEventListener("input", validateForm);
const warning = document.querySelector(".text-danger");

const submitButton = document.querySelector(".form-submit");

function validateForm() {
    const password = passwordField.value;
    const username = usernameField.value;
    if (password !== "" && username !== "") {
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
