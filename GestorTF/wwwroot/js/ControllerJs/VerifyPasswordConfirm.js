export function exibirErro(errosPorCampo) {
        const divErro = document.getElementById('erroSenha');
    divErro.classList.add('isactive');

    let mensagens = '';

    // Itera por todas as chaves (campos)
    for (const campo in errosPorCampo) {
        const mensagensDoCampo = errosPorCampo[campo];

        // Garante que é um array antes de iterar
        if (Array.isArray(mensagensDoCampo)) {
            mensagensDoCampo.forEach(msg => {
                mensagens += `<p></strong> ${msg}</p>`;
            });
        }
    }
    debugger
    divErro.innerHTML = mensagens;
}