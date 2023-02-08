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
            var dominio = getCookie("_SubDominio_PersonaTiendaVirtual");
            window.location.href = "/tienda/checkout/" + dominio;
            callback(false);
        }
    }

    var checkLoginState = function (callback) {
        FB.getLoginStatus(function (response) {
            callback(response);
        });
    }

    var getFacebookData = function () {
        
        FB.api('/me', { fields: 'name,email'}, function (response) {
            $('#txtUsuarioLogin_Correo').val(response.id);
            $('#txtUsuarioLogin_Contrasenia').val(response.id);
                                
            $('#imgPersona').attr('src', 'http://graph.facebook.com/' + response.id + '/picture?type=large');

            if ($('#txtUsuarioLogin_Correo').val() == "") {
                $.bootstrapGrowl("Error, Falta ingresar su usuario ó correo!", { type: 'danger', width: 'auto' });
                return false;
            }

            if ($('#txtUsuarioLogin_Contrasenia').val() == "") {
                $.bootstrapGrowl("Error, Falta ingresar su contraseña!", { type: 'danger', width: 'auto' });
                return false;
            }

            var entidad = {};
            entidad.UserName = response.id;
            entidad.PasswordHash = response.id;

            $('button').attr('disabled', 'disabled');
            var metodoCorrecto = function (msg) {
                
                if (msg == 0) {
                    $('button').removeAttr('disabled');
                    $('#modalAvisoLogin').modal('show');
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
            LlamarAJAX("/tienda/ingresarPersonaFit", request, metodoCorrecto, metodoError);
            
        });

       
        
    }

    var getFacebookDataValidar = function () {

        FB.api('/me', { fields: 'name,email,first_name,last_name' }, function (response) {
            console.log(response);
            $('#txtUsuario_Nombres').addClass('borderValidacionFacebook');
            $('#txtUsuario_Apellidos').addClass('borderValidacionFacebook');
            $('#txtUsuario_Correo').addClass('borderValidacionFacebook');
            $('#txtUsuario_PasswordHash').addClass('borderValidacionFacebook');
            $('#txtUsuario_Nombres').val(response.first_name);
            $('#txtUsuario_Apellidos').val(response.last_name);
            $('#txtUsuario_Correo').val(response.email);
            $('#txtUsuario_PasswordHash').val(response.id);
            $('#imgValidacion').attr('src', 'http://graph.facebook.com/' + response.id + '/picture?type=large');
        });
        
    }

    var facebookLogin = function () {
        checkLoginState(function (data) {            
            if (data.status !== 'connected') {
                FB.login(function (response) {                 
                    if (response.status === 'connected')
                        getFacebookData();
                },{scope: scopes}
                );
            }
        })
    }

    var facebookLogout = function () {
        checkLoginState(function (data) {
            if (data.status === 'connected') {
                FB.logout(function (response) {
                    //$('#facebook-session').before(btn_login);
                    //$('#facebook-session').remove();
                    var expiry = new Date();
                    expiry.setTime(expiry.getTime() - 3600);
                    document.cookie = "_Usuario_PersonaTiendaVirtual=; expires=" + expiry.toGMTString() + "; path=/";                    
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
