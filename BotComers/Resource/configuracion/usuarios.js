

function RegistrarUsuario() {

    var Accion = 'N';
    
    var entidad = {};
    debugger;
   
    entidad.FullName = ConvertToStringFromObject($('input[id="txtUsuario_Nombre"]').val()) + ' ' + ConvertToStringFromObject($('input[id="txtUsuario_Apellido"]').val());
    entidad.UserName = ConvertToStringFromObject($('input[id="txtUsuario_usuario"]').val());
    entidad.PasswordHash = ConvertToStringFromObject($('input[id="txtUsuario_Contrasenia"]').val());
    entidad.Email = ConvertToStringFromObject($('input[id="txtUsuario_Correo"]').val());
    entidad.PhoneNumber = ConvertToStringFromObject($('input[id="txtUsuario_Celular"]').val());
    entidad.CodigoCargo = ConvertToStringFromObject($('select[id="txtUsuario_CodigoCargo"] option:selected').val());    
    entidad.Accion = Accion;
    
    if (IsUndefinedOrNullOrEmpty(entidad.FullName)) {
        $('#txtUsuario_Nombre_validation').show();
        $('#txtUsuario_Nombre_validation').delay(4000).hide(600);
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.UserName)) {
        $('#txtUsuario_usuario_validation').show();
        $('#txtUsuario_usuario_validation').delay(4000).hide(600);
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.PasswordHash)) {
        $('#txtUsuario_Contrasenia_validation').show();
        $('#txtUsuario_Contrasenia_validation').delay(4000).hide(600);
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Email)) {
        $('#txtUsuario_Correo_validation').show();
        $('#txtUsuario_Correo_validation').delay(4000).hide(600);
        return;
    } 
    else if (entidad.PasswordHash != ConvertToStringFromObject($('input[id="txtUsuario_ConfirmarContrasenia"]').val())) {
        alert("la confirmación de contraseña deben ser iguales");
        return;
    }

    $('button').attr('disabled', 'disabled');
    var metodoCorrecto = function (msg) {
        //msg.Success
        if (msg) {

            $('button').removeAttr('disabled');
            $('.alert-success').show();
            $('.alert-success').delay(4000).hide(600);

            $('input[id="txtUsuario_Nombre"]').val('');
            $('input[id="txtUsuario_Apellido"]').val('');
            $('input[id="txtUsuario_usuario"]').val('');
            $('input[id="txtUsuario_Contrasenia"]').val('');
            $('input[id="txtUsuario_Correo"]').val('');
            $('input[id="txtUsuario_Celular"]').val('');
            $('input[id="txtUsuario_ConfirmarContrasenia"]').val('');
        }
        else {
            MostrarMensaje("Error", "vuelva a intentar nuevamente!");
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/configuracion/RegistrarUsuario", request, metodoCorrecto, metodoError);

}

function CambiarClaveUsuario() {

    var Accion = 'N';

    var entidad = {};
  
    entidad.PasswordHashActual = ConvertToStringFromObject($('input[id="txtUsuario_ContraseniaActual"]').val()) + ' ' + ConvertToStringFromObject($('input[id="txtUsuario_Apellido"]').val());
    entidad.PasswordHashNueva = ConvertToStringFromObject($('input[id="txtUsuario_ContraseniaNueva"]').val());
    entidad.Id = ConvertToStringFromObject($('input[id="hdIDusuario"]').val());
    
    if (IsUndefinedOrNullOrEmpty(entidad.PasswordHashActual)) {
        $('#txtUsuario_ContraseniaActual_validation').show();
        $('#txtUsuario_ContraseniaActual_validation').delay(4000).hide(600);
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.PasswordHashNueva)) {
        $('#txtUsuario_ContraseniaNueva_validation').show();
        $('#txtUsuario_ContraseniaNueva_validation').delay(4000).hide(600);
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Id)) {
        alert("no se encontro el ID del usuario");
        return;
    } 

    $('button').attr('disabled', 'disabled');
    var metodoCorrecto = function (msg) {
      
        if (msg > 0) {
                        
            $('button').removeAttr('disabled');
            $('.alert-success').show();
            $('.alert-success').delay(4000).hide(600);

            $('input[id="txtUsuario_ContraseniaActual"]').val('');
            $('input[id="txtUsuario_ContraseniaNueva"]').val('');
            
        }
        else {
            $('button').removeAttr('disabled');
            alert("la contraseña no es correcta");
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/configuracion/EjecutarCambiarClave", request, metodoCorrecto, metodoError);

}
