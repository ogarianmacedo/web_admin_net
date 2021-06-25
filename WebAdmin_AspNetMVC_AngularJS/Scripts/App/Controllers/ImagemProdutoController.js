app.controller("ImagemProdutoController", function ($scope, ImagemProdutoService, AlertasService, $httpParamSerializerJQLike) {
    //Retorna produto conforme parametro id da url
    $scope.produto = [];
    $scope.buscarProduto = function ()
    {
        //Pega parametro url
        var params = window.location.search.substring(1).split('&');
        var nomeProduto = params[0].split('=');
        var nome = {
                    Nome: nomeProduto[1]
                };

        ImagemProdutoService.getProdutoPorNome(nome).then(function (result) {
            $scope.produto = result.data;
            $scope.ImagensProduto(result.data);
        });
    }
    $scope.buscarProduto();

    //Adiciona Imagem
    $scope.uploadImagem = function (produto) {
        var input = document.getElementById('imagemProduto');
        var file = input.files[0];
        var nome = Math.random() + "_" + file.name;

        var fd = new FormData();
        fd.append("file", file);

        ImagemProdutoService.adicionarImagem(nome, produto.Id).then(function (result) {
            if (result.data.success === true) {
                ImagemProdutoService.uploadImagem(nome, fd).then(function (result) {
                    if (result.success === true) {
                        $scope.buscarProduto();
                        AlertasService.alertaSucesso("Imagem adicionada com sucesso.");
                    } 
                });
            }else {
                AlertasService.alertaAviso("Erro ao salvar imagem, tente novamente.");
            }
        });
    }

    //Busca imagens do produto
    $scope.imagens = [];
    $scope.ImagensProduto = function (produto) {
        ImagemProdutoService.buscaImagensProduto(produto.Id).then(function (result) {
            if (result.data) {
                $scope.imagens = result.data;
            }
        });
    }

    //Exclui imagem 
    $scope.excluirImagem = function (imagem) {
        ImagemProdutoService.excluirImagemProduto(imagem.Id).then(function (result) {
            if (result.data.success === true) {
                $scope.buscarProduto();
                AlertasService.alertaSucesso("Imagem excluída com sucesso.");
            } else {
                AlertasService.alertaAviso("Erro ao excluir imagem, tente novamente.");
            }
        });
    }
});