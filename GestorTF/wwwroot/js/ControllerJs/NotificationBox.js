//const divErro = document.getElementById('notificationBox');
//const divTitle = document.getElementById('notBoxTitle');
//const errosList = document.getElementById('errosList');
//const closeBox = document.getElementById('closeBox');
//const iconBox = document.getElementById('iconBox');
//export function exibirErro(errosPorCampo) {
//    divErro.classList.add('isActive');
//    let title = '';
//    let mensagens = '';
//    // Itera por todas as chaves (campos)
//    for (const campo in errosPorCampo) {
//        const mensagensDoCampo = errosPorCampo[campo];
//        debugger
//        // Garante que é um array antes de iterar
//        if (Array.isArray(mensagensDoCampo)) {
//            mensagensDoCampo.forEach(msg => {
//                mensagens += `<p><strong> ${msg}</strong></p>`;
//            });
//            title = 'Verifique os erros!';
//        }
//    }
//    debugger
//    divTitle.innerHTML = title;
//    errosList.innerHTML = mensagens;
//}
//function closeErro() {
//    divErro.classList.remove('isActive');
//    divTitle.innerHTML = '';
//    errosList.innerHTML = '';
//}
//closeBox.addEventListener('click', closeErro);
const divErro = document.getElementById('notificationBox');
const divTitle = document.getElementById('notBoxTitle');
const errosList = document.getElementById('errosList');
const closeBox = document.getElementById('closeBox');
const iconBox = document.getElementById('iconBox');

export function exibirNotificacao({ tipo = 'erro', titulo = '', mensagens }) {
    divErro.classList.add('isActive');
    debugger

    iconBox.innerHTML = tipo === 'sucesso'
        ? `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#016e04" class="bi bi-check-circle" viewBox="0 0 16 16">
  <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16"/>
  <path d="m10.97 4.97-.02.022-3.473 4.425-2.093-2.094a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-1.071-1.05"/>
</svg>`
        : `<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="#a30000" class="bi bi-x-circle" viewBox="0 0 16 16">
                                <path d="M8 15A7 7 0 1 1 8 1a7 7 0 0 1 0 14m0 1A8 8 0 1 0 8 0a8 8 0 0 0 0 16" />
                                <path d="M4.646 4.646a.5.5 0 0 1 .708 0L8 7.293l2.646-2.647a.5.5 0 0 1 .708.708L8.707 8l2.647 2.646a.5.5 0 0 1-.708.708L8 8.707l-2.646 2.647a.5.5 0 0 1-.708-.708L7.293 8 4.646 5.354a.5.5 0 0 1 0-.708" />
                            </svg>`;

    divTitle.innerHTML = titulo;

    // Constrói o conteúdo das mensagens
    let htmlMensagens = '';
    if (Array.isArray(mensagens)) {
        mensagens.forEach(msg => {
            htmlMensagens += `<p><strong>${msg}</strong></p>`;
        });
    } else {
        htmlMensagens = `<p><strong>${mensagens}</strong></p>`;
    }

    errosList.innerHTML = htmlMensagens;
}

// Evento para fechar
function closeErro() {
    divErro.classList.remove('isActive');
    divTitle.innerHTML = '';
    errosList.innerHTML = '';
}

closeBox.addEventListener('click', closeErro);