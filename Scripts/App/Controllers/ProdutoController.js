app.controller("ProdutoController", function ($scope, ProdutoService, AlertasService, $httpParamSerializerJQLike, CategoriaProdutoService) {

    $scope.title = "Listagem de Produtos";

    //Lista produtos
    $scope.listaProdutos = [];
    $scope.listarProdutos = function () {
        ProdutoService.getProdutos().then(function (result) {
            $scope.listaProdutos = result.data;
        });
    }
    $scope.listarProdutos();

    //ordenar colunas da listagem
    $scope.ordenarPor = function (coluna) {
        $scope.criterioDeOrdenacao = coluna;
        $scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
    }

    //Lista categorias
    $scope.categoriasProduto = [];
    $scope.listaCategorias = function () {
        CategoriaProdutoService.getCategoriasProduto().then(function (result) {
            $scope.categoriasProduto = result.data;
        });
    }
    $scope.listaCategorias();

    //parametros do model adicionar
    $scope.modalProduto = function () {
        $scope.tituloModal = "Adicionar Novo Produto";
        $scope.categorias = $scope.categoriasProduto;
        $scope.btnEditar = false;
        $scope.btnSalvar = true;
    }

    //limpar modal
    $scope.limparModal = function () {
        $scope.novo = [];
    }

    //Adiciona novo produto
    $scope.novoProduto = function (novo) {

        $scope.antiForgeryToken = angular.element('input[name="__RequestVerificationToken"]').attr('value');
        var token = {
            __RequestVerificationToken: $scope.antiForgeryToken
        }
        var dados = angular.extend(token, novo);
        var dadosFormulario = $httpParamSerializerJQLike(dados);

        if (novo == undefined) {
            AlertasService.alertaAviso("Preencha os campos obrigatórios");
        } else if (novo.Nome == undefined) {
            AlertasService.alertaAviso("Preencha o campo Nome");
        } else if (novo.Descricao == undefined) {
            AlertasService.alertaAviso("Preencha o campo Descrição");
        } else if (novo.ValorVenda == undefined) {
            AlertasService.alertaAviso("Preencha o campo Valor de Venda");
        } else if (novo.ValorPromocao == undefined) {
            AlertasService.alertaAviso("Preencha o campo Valor de Promoção");
        } else if (novo.Quantidade == undefined) {
            AlertasService.alertaAviso("Preencha o campo Quantidade");
        } else if (novo.StPromocao == undefined) {
            AlertasService.alertaAviso("Defina o status de promoção do produto");
        } else if (novo.CategoriaProdutoId == undefined) {
            AlertasService.alertaAviso("Escolha a categoria do produto");
        } else if (novo.ValorCusto == undefined) {
            AlertasService.alertaAviso("Preencha o campo Valor de Custo");
        } else {
            ProdutoService.novoProduto(dados).then(function (result) {
                if (result.success === true) {
                    AlertasService.alertaSucesso("Produto cadastrado com sucesso.");
                    $scope.limparModal();
                    $scope.listarProdutos();
                    $('#modalProduto').modal('hide');
                } else {
                    AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
                }
            });
        }
    }

    //Exibe o modal para editar
    $scope.exibirModalEditar = function (produto) {
        $scope.tituloModal = "Editar Usuário";
        $scope.categorias = $scope.categoriasProduto;
        $scope.novo = angular.copy(produto);
        $scope.btnEditar = true;
        $scope.btnSalvar = false;
        $scope.selected = produto.CategoriaProduto.Id;
    }

    //Editar produto
    $scope.editarProduto = function (dadosProduto) {

        $scope.antiForgeryToken = angular.element('input[name="__RequestVerificationToken"]').attr('value');
        var token = {
            __RequestVerificationToken: $scope.antiForgeryToken
        }
        var dados = angular.extend(token, dadosProduto);
        var dadosFormulario = $httpParamSerializerJQLike(dados);

        ProdutoService.editarProduto(dadosFormulario).then(function (result) {
            if (result.success === true) {
                AlertasService.alertaSucesso("Produto editado com sucesso.");
                $scope.limparModal();
                $scope.listarProdutos();
                $('#modalProduto').modal('hide');
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }

    //parametros do model excluir
    $scope.excluir = function (produto) {
        $scope.nomeProduto = produto.Nome;
        $scope.dadosProduto = produto;
    }

    //Excluir produto
    $scope.excluirProduto = function (dadosProduto) {
        ProdutoService.excluirProduto(dadosProduto.Id).then(function (result) {
            if (result.data.success === true) {
                AlertasService.alertaSucesso("Produto excluído com sucesso.");
                $scope.listarProdutos();
                $('#modalExcluir').modal('hide');
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }

    //Visualizar produto
    $scope.visualizarProduto = function (produto) {
        ProdutoService.getProduto(produto.Id).then(function (result) {
            if (result.data) {
                $scope.dadosProduto = result.data;
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }

});