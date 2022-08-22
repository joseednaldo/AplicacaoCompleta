function AjaxModal() {
    //1 função
    $(function () {
        $.ajaxSetup({ cache: false });

        $("a[data-modal]").on("click",
            function (e) {
                $('#myModalContent').load(this.href,
                    function () {
                        $('#myModal').modal({
                            keyboard: true
                        }, 'show');
                        bindForm(this);
                    });

                return false;
            });
    });


    //2 função
    function bindForm(dialog) {
        $('form', dialog).submit(function () {

            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.success) {
                        debugger;
                        $('#myModal').modal('hide');
                        $('#EnderecoTarget').load(result.url);
                    } else {
                        $('#myModalContent').html(result);
                        bindForm(dialog);
                    }
                }
            });

            return false;

        });
    }


}

function BuscaCep() {
    $(document).ready(function () {

        function limap_formulario_cep() {

            $("#Endereco_Logradouro").val("");
            $("#Endereco_Bairro").val("");
            $("#Endereco_Cidade").val("");
            $("#Endereco_Estado").val("");
        }

        //quabndo o campo cep perde o foco evento (blur).
        $("#Endereco_Cep").blur(function () {

            //nova variavel "cep somente  com digitos
            var cep = $(this).val().replace(/\D/g, '');

            //veridicar se o campo "cep" possui vaor informado.
            if (cep != "") {

                //expressão regular para validar cep.
                var validacep = /^[0-9]{8}$/;

                if (validacep.test(cep)) {
                    //Prenche os campos com "..." enquanto consutla o webservice  
                    $("#Endereco_Logradouro").val("...");
                    $("#Endereco_Bairro").val("...");
                    $("#Endereco_Cidade").val("...");
                    $("#Endereco_Estado").val("...");

                    debugger;
                    //Consultando o webservice viaceo.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {
                            if (!("erro" in dados)) {
                                //atualiza os campos com os valores retornado da consulta.
                                $("#Endereco_Logradouro").val(dados.logradouro);
                                $("#Endereco_Bairro").val(dados.bairro);
                                $("#Endereco_Cidade").val(dados.localidade);
                                $("#Endereco_Estado").val(dados.uf);
                            } else {
                                //cep pesquisado nao foi encontrado
                                limap_formulario_cep();
                                alert("CEP não encontrato.");
                            }
                        });
                } else {
                    //cep é inválido
                    limap_formulario_cep();
                    alert("Formato de CEP inválido.");
                }
            } else {
                //cep sem valor
                limap_formulario_cep();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});