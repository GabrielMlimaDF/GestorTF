const campoSenha = document.getElementById("Password");
const campoSenhaConfirm = document.getElementById("confirmPassword");
const checkbox = document.getElementById("mostrarSenha");

checkbox.addEventListener("change", () => {
    campoSenha.type = checkbox.checked ? "text" : "password";
    campoSenhaConfirm.type = checkbox.checked ? "text" : "password";
});