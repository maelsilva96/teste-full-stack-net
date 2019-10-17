$(document).ready(function () {
    function CpfEValido(cpf) {
        cpf = cpf.replace(/\D/g, '');
        if (cpf.toString().length != 11 || /^(\d)\1{10}$/.test(cpf)) return false;
        var result = true;
        [9, 10].forEach(function (j) {
            var soma = 0, r;
            cpf.split(/(?=)/).splice(0, j).forEach(function (e, i) {
                soma += parseInt(e) * ((j + 2) - (i + 1));
            });
            r = soma % 11;
            r = (r < 2) ? 0 : 11 - r;
            if (r != cpf.substring(j, j + 1)) result = false;
        });
        return result;
    }

    function CpfJaExiste(cpf) {
        var fields = $(document).find('[name="cpfBeneficario[]"]');
        for (var field of fields) {
            var sendoAlterado = $(document).find(".sendo-alterado");
            if (sendoAlterado.length > 0) {
                let hasClass = $(field).parent().parent().hasClass('sendo-alterado');
                if ($(field).val() == cpf && !hasClass) return true;
            } else {
                if ($(field).val() == cpf) {
                    return true;
                }
            }
        }
        return false;
    }

    function HtmlDaTabela(cpf, nome) {
        return $("<tr></tr>").append(
            $("<td></td>").append(cpf).append(
                $("<input>").attr("type", "hidden").attr("name", "cpfBeneficario[]").val(cpf)
            )
        ).append(
            $("<td></td>").append(nome).append(
                $("<input>").attr("type", "hidden").attr("data-cpf", cpf).attr("name", "nomeBeneficario[]").val(nome)
            )
        ).append(
            $("<td></td>").append(
                $("<button></button>").attr("type", "button").addClass("btn btn-info alterarBeneficiario").html("Alterar")
            ).append(
                $("<button></button>").attr("type", "button").addClass("btn btn-danger removerBeneficiario").html("Remover")
            )
        );
    }

    if (typeof obj != "undefined") {
        for (var beneficiario of obj.Beneficiarios) {
            $(document).find("#tabelaDeBeneficiarios").append(
                HtmlDaTabela(
                    beneficiario.Cpf.replace(/^(\d{3})(\d{3})(\d{3})(\d{2})/g, "$1.$2.$3-$4"),
                    beneficiario.Nome
                )
            );
        }
    }

    $(document).on("click", "#incOuAltBeneficiario", function () {
        var cpf = $(document).find("#cpfBeneficiario").val(),
            nome = $(document).find("#nomeBeneficiario").val();
        if (CpfEValido(cpf)) {
            if (!CpfJaExiste(cpf)) {
                $(document).find("#cpfBeneficiario").removeClass("error");
                $(document).find("#cpfBeneficiario, #nomeBeneficiario").val('');
                $(document).find(".sendo-alterado").remove();
                $(document).find("#tabelaDeBeneficiarios").append(HtmlDaTabela(cpf, nome));
                $(document).find("#incOuAltBeneficiario").removeClass("btn-info").addClass("btn-success").html("Incluir")
            } else {
                $(document).find("#cpfBeneficiario").addClass("error");
                alert("Já existe um beneficiário com esse CPF!");
            }
        } else {
            $(document).find("#cpfBeneficiario").addClass("error");
            alert("Cpf inválido!");
        }
    }).on("click", ".removerBeneficiario", function () {
        $(this).parent().parent().remove();
    }).on("click", ".alterarBeneficiario", function () {
        var tr = $(this).parent().parent().addClass("sendo-alterado");
        var cpf = tr.find('[name="cpfBeneficario[]"]').val(),
            nome = tr.find('[name="nomeBeneficario[]"]').val();

        $(document).find("#incOuAltBeneficiario").addClass("btn-info").removeClass("btn-success").html("Salvar")
        $(document).find("#cpfBeneficiario").val(cpf);
        $(document).find("#nomeBeneficiario").val(nome);
    });
});