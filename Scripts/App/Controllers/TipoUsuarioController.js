app.controller("TipoUsuarioController", function ($scope, TipoUsuarioService, AlertasService, $httpParamSerializerJQLike) {

    $scope.title = "Listagem de Usuários";

    //Lista tipos usuario
    $scope.listaTipoUsuario = [];
    $scope.listarTiposUsuario = function () {
        TipoUsuarioService.getTiposUsuario().then(function (result) {
            $scope.listaTipoUsuario = result.data;
        });
    }
    $scope.listarTiposUsuario();

    //ordenar colunas da listagem
    $scope.ordenarPor = function (coluna) {
        $scope.criterioDeOrdenacao = coluna;
        $scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
    }

    //parametros do model adicionar
    $scope.modalTipoUsuario = function () {
        $scope.tituloModal = "Adicionar Novo Tipo Usuário";
    }

    //limpar modal
    $scope.limparModal = function () {
        $scope.novo = [];
    }

    //função de salvar novo tipo usuario
    $scope.novoTipoUsuario = function (novo) {

        $scope.antiForgeryToken = angular.element('input[name="__RequestVerificationToken"]').attr('value');
        var token = {
            __RequestVerificationToken: $scope.antiForgeryToken
        }
        var dados = angular.extend(token, novo);
        var dadosFormulario = $httpParamSerializerJQLike(dados);

        if (novo == undefined) {
            AlertasService.alertaAviso("Preencha o campo obrigatório");
        } else {
            TipoUsuarioService.novoTipoUsuario(dadosFormulario).then(function (result) {
                if (result.success === true) {
                    AlertasService.alertaSucesso("Novo tipo usuário cadastrado com sucesso.");
                    $scope.limparModal();
                    $scope.listarTiposUsuario();
                    $('#modalTipoUsuario').modal('hide');
                } else {
                    AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
                }
            });
        }
    }

    //parametros do model excluir
    $scope.excluir = function (tipo) {
        $scope.nomeTipoUsuario = tipo.Nome;
        $scope.dadosTipoUsuario = tipo;
    }

    //excluir tipo usuario
    $scope.excluirTipoUsuario = function (dadosTipoUsuario) {
        TipoUsuarioService.excluirTipoUsuario(dadosTipoUsuario.Id).then(function (result) {
            if (result.data.success === true) {
                AlertasService.alertaSucesso("Tipo usuário excluído com sucesso.");
                $scope.listarTiposUsuario();
                $('#modalExcluir').modal('hide');
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }

});