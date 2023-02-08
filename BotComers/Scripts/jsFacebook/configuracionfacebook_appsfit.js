$(function () {

    var app_id = '643613039622433';
    var scopes = 'email';

    window.fbAsyncInit = function () {

        FB.init({
            appId: app_id,
            status: true,
            cookie: true,
            xfbml: true,
            version: 'v8.1'
        });

        facebookLogout();
    };

    function verificarLoginActivo() {
        FB.getLoginStatus(function (response) {
            statusChangeCallback(response, function () { });
        });
    }

    var statusChangeCallback = function (response, callback) {
        console.log(response);
        if (response.status === 'connected') {
            //getFacebookData();
        } else {
            var dominio = getCookie("_SubDominio_PersonaFit");
            window.location.href = "/tienda/miperfil/" + dominio;
            callback(false);
        }
    }

    var checkLoginState = function (callback) {
        FB.getLoginStatus(function (response) {
            callback(response);
        });
    }

    var getFacebookData = function () {

        FB.api('/me', { fields: 'name,email' }, function (response) {
                     
            var entidad = {};
            entidad.CodigoSede = $('select[id="txtUsuario_CodigoSede"] option:selected').val();
            entidad.UserName = response.id;
            entidad.PasswordHash = response.id;

            if (entidad.CodigoSede == undefined || entidad.CodigoSede == 0) {
                $.bootstrapGrowl("Error, Falta seleccionar una sede del centro!", { type: 'danger', width: 'auto' });
                return false;
            }else if (entidad.UserName == "") {
                $.bootstrapGrowl("Error, Falta validar tu facebok!", { type: 'danger', width: 'auto' });
                return false;
            }else if (entidad.PasswordHash == "") {
                $.bootstrapGrowl("Error, Falta validar tu facebok!", { type: 'danger', width: 'auto' });
                return false;
            }

            $('button').attr('disabled', 'disabled');
            var metodoCorrecto = function (msg) {
             
                if (msg == "0") {
                    $('button').removeAttr('disabled');
                    document.getElementById('modalAviso').style.display = '';                    
                    facebookLogout();
                }
                else {
                    window.location.href = msg;
                }
            };
            var metodoError = function (msg) {
                alert(msg);
            };
            var request = {
                request: entidad
            };
            LlamarAJAX("/pg/ingresarPersonaFit", request, metodoCorrecto, metodoError);

        });

    }

    var getFacebookDataValidar = function () {

        FB.api('/me', { fields: 'name,email,first_name,last_name' }, function (response) {
            
            $('#txtUsuario_Nombres').val(response.first_name);
            $('#txtUsuario_Apellidos').val(response.last_name);
            $('#txtUsuario_Email').val(response.email);
            $('#txtUsuario_IdFacebook').val(response.id);
            //$('#txtUsuario_PasswordConfirmacion').val(response.id);
            $('#imgValidacion').attr('src', 'http://graph.facebook.com/' + response.id + '/picture?type=large');

            $('#modalAvisoCorrecto').modal('show');
            $('#lblTituloAvisoCorrecto').html('Muy bien, ' + response.first_name);
            $('#lblDetalleAvisoCorrecto').html('Ahora completa el nro de celular, DNI e ingresa el correo que estas usando actualmente.');
            
        });

    }

    var facebookLogin = function () {
        checkLoginState(function (data) {
            if (data.status !== 'connected') {
                FB.login(function (response) {
                    if (response.status === 'connected')
                        getFacebookData();
                }, { scope: scopes }
                );
            }
        })
    }

    var facebookLogout = function () {
        checkLoginState(function (data) {
            if (data.status === 'connected') {
                FB.logout(function (response) {
                   
                    var expiry = new Date();
                    expiry.setTime(expiry.getTime() - 3600);
                    document.cookie = "_Usuario_PersonaFit=; expires=" + expiry.toGMTString() + "; path=/";
                })
            }
        })
    }

    var facebookValidar = function () {
        checkLoginState(function (data) {
            if (data.status !== 'connected') {
                FB.login(function (response) {
                    if (response.status === 'connected')
                        getFacebookDataValidar();
                }, { scope: scopes }
                );
            }
        })
    }

    $(document).on('click', '#loginFacebook', function (e) {
        e.preventDefault();
        facebookLogout();
        facebookLogin();
    })

    //$(document).on('click', '#logoutFacebook', function (e) {
    //    e.preventDefault();

    //    if (confirm("¿Está seguro?"))
    //        facebookLogout();
    //    else
    //        return false;
    //})

    $(document).on('click', '#validarFacebook', function (e) {
        e.preventDefault();
        facebookLogout();
        facebookValidar();
    })

    function getCookie(cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i].trim();
            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
        }
        return "";
    }

});