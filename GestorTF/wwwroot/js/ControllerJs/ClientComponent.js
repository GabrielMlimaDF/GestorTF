const btnListar = document.getElementById('btnListar');
const btnCreate = document.getElementById('btnCreate');
function carregarCreatePartial() {
    $("#contentPartial").load("/Client/CreateClient", function () {
        maskCNPJ();
        associarRegistroCliente();// Executa somente após o conteúdo estar no DOM
    });
}
btnCreate.addEventListener('click', carregarCreatePartial);

function carregarListarPartial() {
    $("#contentPartial").load("/Client/ListClient");
    carregarClientesDoUsuario();
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

function maskCNPJ() {
    document.getElementById('CNPJ').addEventListener('input', function (e) {
        let value = e.target.value.replace(/\D/g, ''); // Remove tudo que não for dígito

        if (value.length > 14) value = value.slice(0, 14); // Limita a 14 dígitos

        // Aplica a máscara
        value = value.replace(/^(\d{2})(\d)/, '$1.$2');
        value = value.replace(/^(\d{2})\.(\d{3})(\d)/, '$1.$2.$3');
        value = value.replace(/\.(\d{3})(\d)/, '.$1/$2');
        value = value.replace(/(\d{4})(\d)/, '$1-$2');

        e.target.value = value;
    });
}