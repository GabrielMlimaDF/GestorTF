import { exibirNotificacao } from './NotificationBox.js'; // ajuste o caminho se necessário
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
        debugger
        if (response.ok) {
            const sucesso = await response.json();
            exibirNotificacao({
                tipo: 'sucesso',
                titulo: 'Tudo certo. Parabéns!',
                mensagens: sucesso.messageSucess || 'Operação realizada com sucesso.'
            });
        } else {
            const erro = await response.json();
            exibirNotificacao({
                tipo: 'erro',
                titulo: 'Ocorreu um erro!',
                mensagens: extrairMensagensDeErro(erro) // função auxiliar opcional
            });
        }
    } catch (err) {
        document.getElementById('resultMessage').innerText = 'Erro ao tentar cadastrar usuário.';
    }
});

function extrairMensagensDeErro(errosPorCampo) {
    const mensagens = [];
    for (const campo in errosPorCampo) {
        const mensagensDoCampo = errosPorCampo[campo];
        if (Array.isArray(mensagensDoCampo)) {
            mensagens.push(...mensagensDoCampo);
        } else if (typeof mensagensDoCampo === 'string') {
            mensagens.push(mensagensDoCampo);
        }
    }
    return mensagens;
}