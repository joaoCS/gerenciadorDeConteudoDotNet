jQuery(function ($) {
    $("input[name='cep']").change(function () {
        var cep_code = $(this).val();
        if (cep_code.length <= 0) return;

        $.get("http://api.postmon.com.br/v1/cep/" + cep_code, function (result) {
             
            $("input[name='cep']").val(result.cep);
            $("input[name='estado']").val(result.estado);
            $("input[name='cidade']").val(result.cidade);
            $("input[name='bairro']").val(result.bairro);
           // $("input[name='endereco']").val(result.);

        });
    });
});