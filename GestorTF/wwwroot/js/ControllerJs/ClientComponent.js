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
    $("#contentPartial").load("/Client/ListClient", function () {
        carregarClientesDoUsuario();
    });
}
btnListar.addEventListener('click', carregarListarPartial);

async function carregarClientesDoUsuario() {
    const lista = document.getElementById("listaClientes");
    if (!lista) return;

    try {
        const response = await fetch("/v1/client/list");
        if (!response.ok) throw new Error("Erro ao buscar clientes.");

        const clientes = await response.json();
        debugger;
        if (clientes.length === 0) {
            lista.innerHTML = "<p>Nenhum cliente encontrado.</p>";
            return;
        }

        lista.innerHTML = clientes.map(c => criarCardCliente(c)).join("");
    } catch (error) {
        console.error(error);
        lista.innerHTML = "<p>Erro ao carregar clientes.</p>";
    }
}

function criarCardCliente(cliente) {
    const statusClasse = cliente.ativo ? "ativo" : "inativo";

    return `
    <div class="card-container-list">
      <div class="status-list-client ${statusClasse}">
        <span class="status-dot-client ${statusClasse}"></span> ${cliente.ativo ? "Ativo" : "Inativo"}
      </div>
      <div class="card-content-client">
        <div class="info-group-client">
          <div class="info-label-client">Nome completo</div>
          <div class="info-value-client">${cliente.nome}</div>
        </div>
        <div class="info-group-client">
          <div class="info-label-client">Email</div>
          <div class="info-value-client">${cliente.email}</div>
        </div>
        <div class="info-group-client">
          <div class="info-label-client">CNPJ</div>
          <div class="info-value-client">${cliente.cnpj}</div>
        </div>
        <div class="info-group-client">
          <div class="info-label-client">Tel</div>
          <div class="info-value-client">${cliente.telefone}</div>
        </div>
      </div>
      <div class="menu-icon-client">⋯</div>
    </div>
  `;
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