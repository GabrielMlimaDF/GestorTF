import { exibirErro } from './VerifyPasswordConfirm.js'; // ajuste o caminho se necessário
document.getElementById('btnRegister').addEventListener('click', async function () {
    const name = document.getElementById('Name').value;
    const email = document.getElementById('Email').value;
    const password = document.getElementById('Password').value;
    const confirmPassword = document.getElementById('confirmPassword').value;
    let RoleId = document.getElementById("flexCheckUsuario").checked
        ? 2
        : document.getElementById("flexCheckDefaultAdmin").checked
            ? 1
            : null;
    debugger;
    const data = { name, email, password, RoleId, confirmPassword };

    try {
        const response = await fetch('/v1/user/registers', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include', // IMPORTANTE para enviar os cookies com JWT
            body: JSON.stringify(data)
        });

        if (response.ok) {
            document.getElementById('resultMessage').innerText = 'Usuário cadastrado com sucesso!';
        } else {
            const error = await response.json();
            exibirErro(error)
            /* document.getElementById('erroSenha').innerText = `Erro: ${error.message || response.status}`;*/
        }
    } catch (err) {
        document.getElementById('resultMessage').innerText = 'Erro ao tentar cadastrar usuário.';
    }
});