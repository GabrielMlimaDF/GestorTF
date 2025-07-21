import { exibirNotificacao } from './NotificationBox.js'; // ajuste o caminho se necessário
document.getElementById('btnRegister').addEventListener('click', async function () {
    const nome = document.getElementById('Name').value;
    const email = document.getElementById('Email').value;
    const cnpj = document.getElementById('CNPJ').value.replace(/\D/g, '');
    const telefone = document.getElementById('Tel').value;
    debugger;
    const data = { nome, email, cnpj, telefone};


try {
    const response = await fetch('/v1/client/registers', {
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