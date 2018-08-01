

function DeletarItem(id) {
    $.ajax({
        url: '/Pessoa/Deletar/' + id,
        type: 'POST',
        dataType: 'text',
        data: id,
        success: window.alert('Deletado com sucesso'),
        error: window.alert('Nao foi possivel deletar')
    });

}

function PreencherColunaPesquisa() {
    var url_ = new URL(location.href);
    var ColunaPesquisa = url_.searchParams.get("ColunaPesquisa");
    var options = document.getElementById('ColunaPesquisaID').getElementsByTagName('option');
    for (x in options) {
        if (options[x].value == ColunaPesquisa) {
            options[x].selected = 'selected';
        }
    }
}
