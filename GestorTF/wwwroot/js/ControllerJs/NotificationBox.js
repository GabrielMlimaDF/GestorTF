const divErro = document.getElementById('notificationBox');
const divTitle = document.getElementById('notBoxTitle');
const errosList = document.getElementById('errosList');
const closeBox = document.getElementById('closeBox');
const iconBox = document.getElementById('iconBox');
export function exibirErro(errosPorCampo) {
    divErro.classList.add('isActive');
    let mensagens = '';
    let title = '';
    // Itera por todas as chaves (campos)
    for (const campo in errosPorCampo) {
        const mensagensDoCampo = errosPorCampo[campo];
        debugger
        // Garante que é um array antes de iterar
        if (Array.isArray(mensagensDoCampo)) {
            mensagensDoCampo.forEach(msg => {
                mensagens += `<p><strong> ${msg}</strong></p>`;
            });
            title = 'Verifique os erros!';
        }
        else {
            mensagens = errosPorCampo;
            title = 'Operação realizada com sucesso!';
        }
    }
    debugger
    divTitle.innerHTML = title;
    errosList.innerHTML = mensagens;
}
function closeErro() {
    divErro.classList.remove('isActive');
    divTitle.innerHTML = '';
    errosList.innerHTML = '';
}
closeBox.addEventListener('click', closeErro);