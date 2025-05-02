document.getElementById('flexCheckUsuario').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('flexCheckDefaultAdmin').checked = false;
    }
});
document.getElementById('flexCheckDefaultAdmin').addEventListener('change', function () {
    if (this.checked) {
        document.getElementById('flexCheckUsuario').checked = false;
    }
});