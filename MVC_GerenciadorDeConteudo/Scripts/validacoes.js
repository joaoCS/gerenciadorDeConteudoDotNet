var validaExclusao = function (id, evento) {
    if (confirm("Confirma exclusão?")) {
        return true;
    }
    event.preventDefault();
    return false;
}