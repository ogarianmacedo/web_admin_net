app.service('UsuarioService', function ($http) {

    this.getUsuarios = function () {
        return $http.get("/Usuario/GetUsuarios");
    }

    this.getTipoUsuario = function () {
        return $http.get("/Usuario/GetTipoUsuario");
    }

    this.novoUsuario = function (params) {
        //return $http.post("/Usuario/Adiciona", params);
        //Uso $.ajax depois de varias tentativas com $http.post estava dando erro de __requestverificationtoken is not present 
        //e deverá remover @Html.AntiForgeryToken() da Index.cshtml e remover [ValidateAntiForgeryToken] da Controller.cs
        return $.ajax({
            url: '/Usuario/Adiciona',
            type: "POST",
            data: params
        });
    }

    this.excluir = function (params) {
        return $http.post("/Usuario/Excluir/" + params);
    }

    this.editarUsuario = function (params) {
        return $.ajax({
            url: '/Usuario/Editar/',
            type: "POST",
            data: params
        });
    }

    this.getUsuario = function (params) {
        return $http.post("/Usuario/GetUsuario/" + params);
    }

    this.uploadImagem = function (nome, fd) {
        return $.ajax({
            type: "POST",
            url: '/Usuario/UploadFile?nameFile=' + nome,
            contentType: false,
            processData: false,
            data: fd
        });
    }
})

app.service('TipoUsuarioService', function ($http) {

    this.getTiposUsuario = function () {
        return $http.get("/TipoUsuario/GetTiposUsuario");
    }

    this.novoTipoUsuario = function (params) {
        return $.ajax({
            url: '/TipoUsuario/Adiciona',
            type: "POST",
            data: params
        });
    }

    this.excluirTipoUsuario = function (params) {
        return $http.post("/TipoUsuario/Excluir/" + params);
    }

})

app.service('CategoriaProdutoService', function ($http) {
       
    this.getCategoriasProduto = function () {
        return $http.get("/CategoriaProduto/GetCategoriasProduto");
    }

    this.novaCategoria = function (params) {
        return $.ajax({
            url: '/CategoriaProduto/Adiciona',
            type: "POST",
            data: params
        });
    }

    this.excluirCategoria = function (params) {
        return $http.post("/CategoriaProduto/Excluir/" + params);
    }

})

app.service('ProdutoService', function ($http) {

    this.getProdutos = function () {
        return $http.get("/Produto/GetProdutos");
    }

    this.novoProduto = function (params) {
        return $.ajax({
            url: '/Produto/Adiciona',
            type: "POST",
            data: params
        });
    }

    this.editarProduto = function (params) {
        return $.ajax({
            url: '/Produto/Editar/',
            type: "POST",
            data: params
        });
    }

    this.excluirProduto = function (params) {
        return $http.post("/Produto/Excluir/" + params);
    }

    this.getProduto = function (params) {
        return $http.post("/Produto/GetProduto/" + params);
    }

})

app.service('ImagemProdutoService', function ($http) {

    this.getProdutoPorNome = function (params) {
        return $http.post("/Produto/GetProdutoPorNome", params);
    }

    this.adicionarImagem = function (nome, id) {
        return $http.post("/Produto/AdicionaDadosImagem", {Nome: nome, Id: id});
    }

    this.uploadImagem = function (nome, fd) {
        return $.ajax({
            type: "POST",
            url: '/Produto/UploadFile?nameFile=' + nome,
            contentType: false,
            processData: false,
            data: fd
        });
    }

    this.buscaImagensProduto = function (id) {
        return $http.post("/Produto/BuscaImagensProduto/" + id);
    }

    this.excluirImagemProduto = function (id) {
        return $http.post("/Produto/ExcluirImagem/" + id);
    }

})