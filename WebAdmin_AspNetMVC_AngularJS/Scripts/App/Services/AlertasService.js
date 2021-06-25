app.service("AlertasService", function () {
    var setOptions = function () {
        toastr.options.positionClass = "toast-top-right";
        toastr.options.closeButton = true;
        toastr.options.showMethod = 'slideDown';
        toastr.options.hideMethod = 'slideUp';
        toastr.options.progressBar = true;
    }

    this.alertaSucesso = function (msg) {
        setOptions();
        return toastr.success(msg);
    }

    this.alertaInfo = function (msg) {
        setOptions();
        return toastr.info(msg);
    };

    this.alertaAviso = function (msg) {
        setOptions();
        return toastr.warning(msg);
    };
   
    this.alertaError = function (msg) {
        setOptions();
        return toastr.error(msg);
    };
});