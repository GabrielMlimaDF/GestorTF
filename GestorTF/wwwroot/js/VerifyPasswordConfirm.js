var password = document.getElementById('Password');
var confirmPassword = document.getElementById('confirmPassword');
const mensagemErro = document.getElementById('erroSenha');

function verificarSenha() {
    if (confirmPassword.value && confirmPassword.value !== password.value) {
        confirmPassword.classList.add("erro");
        mensagemErro.style.display = "block";
    } else {
        confirmPassword.classList.remove("erro");
        mensagemErro.style.display = "none";
    }
}

confirmPassword.addEventListener("input", verificarSenha);
password.addEventListener("input", verificarSenha);