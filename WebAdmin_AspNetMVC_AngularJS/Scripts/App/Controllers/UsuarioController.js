app.controller("UsuarioController", function ($scope, UsuarioService, AlertasService, $httpParamSerializerJQLike) {
    $scope.title = "Listagem de Usuários";

    // Lista usuarios
    $scope.listaUsuarios = [];
    $scope.listarUsuarios = function () {
        UsuarioService.getUsuarios().then(function(result) {
            $scope.listaUsuarios = result.data;
        });
    }
    $scope.listarUsuarios();

    // Lista tipos de usuario (perfil)
    $scope.tiposUsuario = [];
    $scope.tipoUsuario = function () {
        UsuarioService.getTipoUsuario().then(function (result) {
            $scope.tiposUsuario = result.data;
        });
    }
    $scope.tipoUsuario();

    //ordenar colunas da listagem
    $scope.ordenarPor = function (coluna) {
        $scope.criterioDeOrdenacao = coluna;
        $scope.direcaoDaOrdenacao = !$scope.direcaoDaOrdenacao;
    }

    //parametros do model adicionar
    $scope.modalUsuario = function () {
        $scope.tituloModal = "Adicionar Novo Usuário";
        $scope.tipos = $scope.tiposUsuario;
        $scope.btnEditar = false;
        $scope.btnSalvar = true;
    }
    
    //limpar modal
    $scope.limparModal = function () {
        $scope.novo = [];
    }

    //função de salvar novo usuario
    $scope.novoUsuario = function (novo) {
        //Tratamento da imagem de usuário
        var input = document.getElementById('imagemUsuario');
        var file = input.files[0];
        var nome = Math.random() + "_" + file.name;

        var fd = new FormData();
        fd.append("file", file);
        novo.Imagem = nome;

        //Salva usuário novo
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
        } else if (novo.Email == undefined) {
            AlertasService.alertaAviso("Preencha o campo E-mail");
        } else if (novo.TipoUsuarioId == undefined) {
            AlertasService.alertaAviso("Preencha o campo Perfil Usuário");
        } else if (novo.Senha == undefined) {
            AlertasService.alertaAviso("Preencha o campo Senha");
        } else {
            if (novo.Senha != novo.ConfirmacaoSenha) {
                AlertasService.alertaAviso("A confirmação de senha não confere.");
            } else {
                UsuarioService.novoUsuario(dadosFormulario).then(function (result) {
                    if (result.success === true) {
                        //salva imagem
                        UsuarioService.uploadImagem(nome, fd).then(function (result) {
                            if (result.success === false) {
                                AlertasService.alertaAviso("Erro ao salvar imagem, tente novamente.");
                            } 
                        });

                        AlertasService.alertaSucesso("Usuário cadastrado com sucesso.");
                        $scope.limparModal();
                        $scope.listarUsuarios();
                        $('#modalUsuario').modal('hide');
                    } else {
                        AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
                    }
                });
            }
        }
    }

    //parametros do model excluir
    $scope.excluir = function (usuario) {
        $scope.nomeUsuario = usuario.Nome;
        $scope.dadosUsuario = usuario;
    }

    //Excluir usuario
    $scope.excluirUsuario = function (dadosUsuario) {
        UsuarioService.excluir(dadosUsuario.Id).then(function (result) {
            if (result.data.success === true) {
                AlertasService.alertaSucesso("Usuário excluído com sucesso.");
                $('#modalExcluir').modal('hide');
                $scope.listarUsuarios();
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }

    //Exibe o mesmo modal que salva novo usuário para editar
    $scope.exibirModalEditar = function (usuario) {
        $scope.tituloModal = "Editar Usuário";
        $scope.tipos = $scope.tiposUsuario;
        $scope.novo = angular.copy(usuario);
        $scope.btnEditar = true;
        $scope.btnSalvar = false;
        $scope.selected = usuario.TipoUsuario.Id;
    }

    //Edita usuário
    $scope.editarUsuario = function (dadosUsuario) {

        //Tratamento da imagem de usuário
        var input = document.getElementById('imagemUsuario');
        if (input.files[0] != undefined) {
            var file = input.files[0];
            var nome = Math.random() + "_" + file.name;

            var fd = new FormData();
            fd.append("file", file);
            dadosUsuario.Imagem = nome;
        }

        //Editar usuário novo
        $scope.antiForgeryToken = angular.element('input[name="__RequestVerificationToken"]').attr('value');
        var token = {
            __RequestVerificationToken: $scope.antiForgeryToken
        }
        var dados = angular.extend(token, dadosUsuario);
        var dadosFormulario = $httpParamSerializerJQLike(dados);

        UsuarioService.editarUsuario(dadosFormulario).then(function (result) {
            if (result.success === true) {
                //salva imagem
                UsuarioService.uploadImagem(nome, fd).then(function (result) {
                    if (result.success === false) {
                        AlertasService.alertaAviso("Erro ao salvar imagem, tente novamente.");
                    }
                });

                AlertasService.alertaSucesso("Usuário editado com sucesso.");
                $('#modalUsuario').modal('hide');
                $scope.limparModal();
                $scope.listarUsuarios();
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }

    //Visualizar usuário
    $scope.visualizarUsuario = function (usuario) {
        UsuarioService.getUsuario(usuario.Id).then(function (result) {
            if (result.data) {
                $scope.dadosUsuario = result.data;
            } else {
                AlertasService.alertaAviso("Ocorreu algum erro, tente novamente.");
            }
        });
    }
});