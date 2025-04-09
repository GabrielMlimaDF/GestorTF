//Handler menu create user
document.addEventListener("DOMContentLoaded", function () {
    // Definindo as referências das divs
    const bl1 = document.getElementById("bl1");
    const bl2 = document.getElementById("bl2");
    const bl3 = document.getElementById("bl3");

    const menuBl1 = document.getElementById("menu-bl1");
    const menuBl2 = document.getElementById("menu-bl2");
    const menuBl3 = document.getElementById("menu-bl3");

    // Função para mostrar a div ativa e esconder a outra
    function showActiveDiv(activeDiv, activeMenuItem, inactiveDivs, inactiveMenuItems) {
        // Exibe a div ativa e esconde as inativas
        activeDiv.style.display = "block";
        inactiveDivs.forEach(div => div.style.display = "none");

        // Atualiza a classe "active" no item do menu
        activeMenuItem.classList.add("active");
        inactiveMenuItems.forEach(item => item.classList.remove("active"));
    }

    // Inicializa com a div "Informações Pessoais" visível
    showActiveDiv(bl1, menuBl1, [bl2, bl3], [menuBl2, menuBl3]);

    // Adiciona os eventos de clique nos itens do menu
    menuBl1.addEventListener("click", function () {
        showActiveDiv(bl1, menuBl1, [bl2, bl3], [menuBl2, menuBl3]);
    });

    menuBl2.addEventListener("click", function () {
        showActiveDiv(bl2, menuBl2, [bl1, bl3], [menuBl1, menuBl3]);
    });

    menuBl3.addEventListener("click", function () {
        showActiveDiv(bl3, menuBl3, [bl1, bl2], [menuBl1, menuBl2]);
    });
});
