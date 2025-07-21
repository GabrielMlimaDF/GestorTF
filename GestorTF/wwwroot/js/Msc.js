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