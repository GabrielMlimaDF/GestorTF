const btnListar = document.getElementById('btnListar');

function carregarListarPartial() {
    $("#contentPartial").load("/Client/ListClient");
}
btnListar.addEventListener('click', carregarListarPartial);


function carregarClientesDoUsuario() {
    fetch("/v1/client/list") // método GET por padrão
        .then(response => {
            if (!response.ok) throw new Error("Erro ao buscar clientes.");
            return response.json();
        })
        .then(clientes => {
            const lista = document.getElementById("listaClientes");
            if (!lista) return;

            if (clientes.length === 0) {
                lista.innerHTML = "<p>Nenhum cliente encontrado.</p>";
                return;
            }

            lista.innerHTML = "<ul>" +
                clientes.map(c => `<li>${c.nome} (ID: ${c.id})</li>`).join("") +
                "</ul>";
        })
        .catch(error => {
            console.error(error);
            document.getElementById("listaClientes").innerHTML =
                "<p>Erro ao carregar clientes.</p>";
        });
}
btnListar.addEventListener('click', carregarClientesDoUsuario);