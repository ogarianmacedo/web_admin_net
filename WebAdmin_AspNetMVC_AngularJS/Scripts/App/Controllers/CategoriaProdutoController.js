app.controller("CategoriaProdutoController", function ($scope, CategoriaProdutoService, AlertasService, $httpParamSerializerJQLike) {
    $scope.title = "Listagem de Categorias";

    //Listar categorias
    $scope.listaCategorias = [];
    $scope.listarCategorias = function () {
        CategoriaProdutoService.getCategoriasProduto().then(function (result) {
            $scope.listaCategorias = result.data;
        });
    }
    $scope.listarCategorias();

    //ordenar colunas da listagem
    $scope.ordenarPor = function (coluna) {
        $scope.criterioDeOrdenacao = coluna;
        $scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
    }

    //parametros do model adicionar
    $scope.modalCategoriaProduto = function () {
        $scope.tituloModal = "Adicionar Nova Categoria";
    }

    //limpar modal
    $scope.limparModal = function () {
        $scope.novo = [];
    }

    //função de salvar nova categoria
    $scope.novaCategoria = function (novo) {
        $scope.antiForgeryToken = angular.element('input[name="__RequestVerificationToken"]').attr('value');
        var token = {
            __RequestVerificationToken: $scope.antiForgeryToken
        }
        var dados = angular.extend(token, novo);
        var dadosFormulario = $httpParamSerializerJQLike(dados);

        if (novo == undefined) {
            AlertasService.alertaAviso("Preencha o campo obrigatório");
        } else {
            CategoriaProdutoService.novaCategoria(dadosFormulario).then(function (result) {
                if (result.success === true) {
                    AlertasService.alertaSucesso("Nova categoria cadastrada com sucesso.");
                    $scope.limparModal();
                    $scope.listarCategorias();
                    $('#modalCategoria').modal('hide');
                } else {
                    AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
                }
            });
        }
    }

    //parametros do model excluir
    $scope.excluir = function (tipo) {
        $scope.nomeCategoria = tipo.Nome;
        $scope.dadosCategoria = tipo;
    }

    //excluir categoria
    $scope.excluirCategoria = function (dadosCategoria) {
        CategoriaProdutoService.excluirCategoria(dadosCategoria.Id).then(function (result) {
            if (result.data.success === true) {
                AlertasService.alertaSucesso("Categoria excluída com sucesso.");
                $scope.listarCategorias();
                $('#modalExcluir').modal('hide');
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }
});