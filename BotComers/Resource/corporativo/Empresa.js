$(function () {
   
    $('input[id="Empresa_txtAniversario"]').datepicker({
        todayBtn: "linked",
        keyboardNavigation: false,
        forceParse: false,
        format: 'dd/mm/yyyy',
        autoclose: true,
        language: 'es'
    });

    $('input[id="Empresa_txtAniversario"]').datepicker('setDate', new Date());
    
});
//EMPRESA
function SeleccionarEmpresa(row, CodigoUnidadNegocio, CodigoSede) {
    $('#gridEmpresa tbody tr').removeClass('selectedrowGrid');
    $(row).addClass('selectedrowGrid');  
    $('input[id="hdCodigoUnidadNegocioEmpresa"]').val(CodigoUnidadNegocio);
    $('input[id="hdCodigoSedeEmpresa"]').val(CodigoSede);

    ListarCategoriasPrimario();
}

function NuevaEmpresa() {

    $('#Empresa_BtnGuardarEmpresa').html('Registar Producto');

    $('input[id="hdAccionEmpresaCorporativo"]').val('N');
    $('input[id="Empresa_txtNombresDuenio"]').val('');
    $('input[id="Empresa_txtApellidosDuenio"]').val('');
    $('input[id="Empresa_txtCorreoDuenio"]').val('');
    $('select[id="Empresa_ddlPaisDuenio"]').val(0);
    $('input[id="Empresa_txtCelularDuenio"]').val('');
    $('select[id="Empresa_ddlTipoDocumentoEmpresa"]').val(0);
    $('input[id="Empresa_txtNroDocumentoEmpresa"]').val('');
    $('input[id="Empresa_txtRazonSocialEmpresa"]').val('');
    $('input[id="Empresa_txtDireccionEmpresa"]').val('');
    $('input[id="Empresa_txtNombreComercialEmpresa"]').val('');
    $('input[id="Empresa_txtTelefonoEmpresa"]').val('');
    $('input[id="Empresa_txtAniversario"]').datepicker('setDate', new Date());

    $('input[id="Empresa_txtCorreoEmpresa"]').val('');
    $('input[id="Empresa_txtSubDominioEmpresa"]').val('');
    $('select[id="Empresa_ddlEstadoEmpresa"]').val(0);

}

function GuardarEmpresa(CodigoUnidadNegocio) {

    var Accion = $('input[id="hdAccionEmpresaCorporativo"]').val();
    
    if (Accion == 'E') {
        ActualizarEmpresa();
    } else {
        var entidad = {};

        var file = $("#buscarLogo").get(0).files;
        entidad.CodigoUnidadNegocio = CodigoUnidadNegocio;
        entidad.CodigoSede = 0;
        entidad.NombreDuenio = ConvertToStringFromObject($('input[id="Empresa_txtNombresDuenio"]').val());
        entidad.ApellidosDuenio = ConvertToStringFromObject($('input[id="Empresa_txtApellidosDuenio"]').val());
        entidad.CorreoDuenio = ConvertToStringFromObject($('input[id="Empresa_txtCorreoDuenio"]').val());
        entidad.CodigoPais = ConvertToStringFromObject($('select[id="Empresa_ddlPaisDuenio"] option:selected').val());
        entidad.CelularDuenio = ConvertToStringFromObject($('input[id="Empresa_txtCelularDuenio"]').val());
        entidad.TipoDocumentoEmpresa = ConvertToInt32($('select[id="Empresa_ddlTipoDocumentoEmpresa"] option:selected').val());
        entidad.NroDocumentoEmpresa = ConvertToStringFromObject($('input[id="Empresa_txtNroDocumentoEmpresa"]').val());
        entidad.RazonSocialEmpresa = ConvertToStringFromObject($('input[id="Empresa_txtRazonSocialEmpresa"]').val());
        entidad.DireccionEmpresa = ConvertToStringFromObject($('input[id="Empresa_txtDireccionEmpresa"]').val());
        entidad.NombreComercialEmpresa = ConvertToStringFromObject($('input[id="Empresa_txtNombreComercialEmpresa"]').val());
        entidad.TelefonoEmpresa = ConvertToStringFromObject($('input[id="Empresa_txtTelefonoEmpresa"]').val());
        entidad.Accion = $('input[id="hdAccionEmpresaCorporativo"]').val();
        entidad.ImageFileLogo = file[0];
        entidad.IdEmpresa = $('input[id="Empresa_hdIdEmpresa"]').val();
        entidad.ColorEmpresa = $('input[id="Empresa_txtColorEmpresa"]').val();

        if (new Date($('#Empresa_txtAniversario').val()) == "Invalid Date") {
        }
        else {
            entidad.FechaAniversarioEmpresa = ConvertStringToDateTime($('input[id="Empresa_txtAniversario"]').data('datepicker').getFormattedDate('yyyy-mm-dd'));
        }

        entidad.CorreoEmpresa = ConvertToStringFromObject($('input[id="Empresa_txtCorreoEmpresa"]').val());
        entidad.SubDominio = ConvertToStringFromObject($('input[id="Empresa_txtSubDominioEmpresa"]').val());
        entidad.Estado = ConvertToInt32($('select[id="Empresa_ddlEstadoEmpresa"] option:selected').val());


        if (!IsUndefinedOrNullOrEmpty(entidad.FechaAniversarioEmpresa)) {
            entidad.FechaAniversarioEmpresa = new Date(entidad.FechaAniversarioEmpresa).yyyymmdd();
        }
        else {
            entidad.FechaAniversarioEmpresa = new Date().yyyymmdd();
        }

        if (IsUndefinedOrNullOrEmpty(entidad.NombreDuenio)) {
            MostrarMensaje("Error", "Falta ingresar nombre del duenio");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.ApellidosDuenio)) {
            MostrarMensaje("Error", "Falta ingresar apellido del duenio");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.ApellidosDuenio) || entidad.CodigoPais == 0) {
            MostrarMensaje("Error", "Falta seleccionar el pais");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.TipoDocumentoEmpresa) || entidad.TipoDocumentoEmpresa == 0) {
            MostrarMensaje("Error", "Falta seleccionar tipo de documento empresa");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.NroDocumentoEmpresa)) {
            MostrarMensaje("Error", "Falta ingresar nro tipo documento empresa");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.RazonSocialEmpresa)) {
            MostrarMensaje("Error", "Falta ingresar razon social");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.DireccionEmpresa)) {
            MostrarMensaje("Error", "Falta ingresar direccion empresa");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.NombreComercialEmpresa)) {
            MostrarMensaje("Error", "Falta ingresar nombre comercial");
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.ColorEmpresa)) {
            MostrarMensaje("Error", "Falta seleccionar un color de empresa.");
            return;
        }

        var metodoCorrecto = function (msg) {
            //msg.Success
            if (msg) {
                MostrarMensaje("Informacion", "Registro Guardado Correctamente");

                $('input[id="Empresa_txtNombresDuenio"]').val('');
                $('input[id="Empresa_txtApellidosDuenio"]').val('');
                $('input[id="Empresa_txtCorreoDuenio"]').val('');
                $('select[id="Empresa_ddlPaisDuenio"]').val(0);
                $('input[id="Empresa_txtCelularDuenio"]').val('');
                $('input[id="Empresa_txtNroDocumentoEmpresa"]').val('');
                $('select[id="Empresa_ddlTipoDocumentoEmpresa"]').val(0);
                $('input[id="Empresa_txtRazonSocialEmpresa"]').val('');
                $('input[id="Empresa_txtDireccionEmpresa"]').val('');
                $('input[id="Empresa_txtNombreComercialEmpresa"]').val('');
                $('input[id="Empresa_txtTelefonoEmpresa"]').val('');
                $("#gridEmpresa").DataTable().ajax.reload();

                //$('select[id="Empresa_Producto_CodigoProductoArea"]').val('');
                //$('input[id="Empresa_Producto_EstadoActivo"]').prop('checked', false);
                //$('input[id="Empresa_Producto_EstadoInactivo"]').prop('checked', false);

                document.getElementById('closeModalEmpresaCorporativo').click();    

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
        LlamarAJAX("/corporativo/RegistrarEmpresa", request, metodoCorrecto, metodoError);
    }

   
}

function ActualizarEmpresa() {
   // var entidad = {};
    var data = new FormData;

    var file = $("#buscarLogo").get(0).files;

    data.append("CodigoUnidadNegocio" , $('input[id="hdCodigoUnidadNegocioEmpresa"]').val());
    data.append("CodigoSede" , $('input[id="hdCodigoSedeEmpresa"]').val());
    data.append("NombreDuenio" , ConvertToStringFromObject($('input[id="Empresa_txtNombresDuenio"]').val()));
    data.append("ApellidosDuenio" , ConvertToStringFromObject($('input[id="Empresa_txtApellidosDuenio"]').val()));
    data.append("CorreoDuenio" , ConvertToStringFromObject($('input[id="Empresa_txtCorreoDuenio"]').val()));
    data.append("CodigoPais" , ConvertToStringFromObject($('select[id="Empresa_ddlPaisDuenio"] option:selected').val()));
    data.append("CelularDuenio" , ConvertToStringFromObject($('input[id="Empresa_txtCelularDuenio"]').val()));
    data.append("TipoDocumentoEmpresa" , ConvertToInt32($('select[id="Empresa_ddlTipoDocumentoEmpresa"] option:selected').val()));
    data.append("NroDocumentoEmpresa" , ConvertToStringFromObject($('input[id="Empresa_txtNroDocumentoEmpresa"]').val()));
    data.append("RazonSocialEmpresa" , ConvertToStringFromObject($('input[id="Empresa_txtRazonSocialEmpresa"]').val()));
    data.append("DireccionEmpresa" , ConvertToStringFromObject($('input[id="Empresa_txtDireccionEmpresa"]').val()));
    data.append("NombreComercialEmpresa" , ConvertToStringFromObject($('input[id="Empresa_txtNombreComercialEmpresa"]').val()));
    data.append("TelefonoEmpresa" , ConvertToStringFromObject($('input[id="Empresa_txtTelefonoEmpresa"]').val()));
    data.append("Accion" , $('input[id="hdAccionEmpresaCorporativo"]').val());
    data.append("ImageFileLogo" , file[0]);
    data.append("IdEmpresa", $('input[id="Empresa_hdIdEmpresa"]').val());
    data.append("ColorEmpresa", $('input[id="Empresa_txtColorEmpresa"]').val());
    
    data.append("FechaAniversarioEmpresa", ConvertStringToDateTime($('input[id="Empresa_txtAniversario"]').data('datepicker').getFormattedDate('yyyy-mm-dd')));

    data.append("CorreoEmpresa" , ConvertToStringFromObject($('input[id="Empresa_txtCorreoEmpresa"]').val()));
    data.append("SubDominio" , ConvertToStringFromObject($('input[id="Empresa_txtSubDominioEmpresa"]').val()));
    data.append("Estado" , ConvertToInt32($('select[id="Empresa_ddlEstadoEmpresa"] option:selected').val()));
    
    //if (IsUndefinedOrNullOrEmpty(entidad.CodigoUnidadNegocio) || entidad.CodigoUnidadNegocio == 0) {
    //    MostrarMensaje("Error", "Falta seleccionar la unidad de negocio");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.CodigoSede) || entidad.CodigoSede == 0) {
    //    MostrarMensaje("Error", "Falta seleccionar la sede");
    //    return;
    //}
    //else if (!IsUndefinedOrNullOrEmpty(entidad.FechaAniversarioEmpresa)) {
    //    entidad.FechaAniversarioEmpresa = new Date(entidad.FechaAniversarioEmpresa).yyyymmdd();
    //}
    //else {
    //    entidad.FechaAniversarioEmpresa = new Date().yyyymmdd();
    //}

    //if (IsUndefinedOrNullOrEmpty(entidad.NombreDuenio)) {
    //    MostrarMensaje("Error", "Falta ingresar nombre del duenio");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.ApellidosDuenio)) {
    //    MostrarMensaje("Error", "Falta ingresar apellido del duenio");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.CodigoPais) || entidad.CodigoPais == 0) {
    //    MostrarMensaje("Error", "Falta seleccionar el pais");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.TipoDocumentoEmpresa) || entidad.TipoDocumentoEmpresa == 0) {
    //    MostrarMensaje("Error", "Falta seleccionar tipo de documento empresa");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.NroDocumentoEmpresa)) {
    //    MostrarMensaje("Error", "Falta ingresar nro tipo documento empresa");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.RazonSocialEmpresa)) {
    //    MostrarMensaje("Error", "Falta ingresar razon social");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.DireccionEmpresa)) {
    //    MostrarMensaje("Error", "Falta ingresar direccion empresa");
    //    return;
    //}
    //else if (IsUndefinedOrNullOrEmpty(entidad.NombreComercialEmpresa)) {
    //    MostrarMensaje("Error", "Falta ingresar nombre comercial");
    //    return;
    //} else if (IsUndefinedOrNullOrEmpty(entidad.ColorEmpresa)) {
    //    MostrarMensaje("Error", "Falta seleccionar un color de la empresa");
    //    return;
    //} if (file == undefined || file == null) {
    //    alert("Falta seleccionar una imagen.");        
    //    return false;
    //}

    $.ajax({
        type: "Post",
        url: "/corporativo/RegistrarEmpresa",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            metodoCorrecto(response);
        }

    })

    var metodoCorrecto = function (msg) {
        //msg.Success
        if (msg) {
            MostrarMensaje("Informacion", "Registro Guardado Correctamente");

            $('input[id="Empresa_txtNombresDuenio"]').val('');
            $('input[id="Empresa_txtApellidosDuenio"]').val('');
            $('input[id="Empresa_txtCorreoDuenio"]').val('');
            $('select[id="Empresa_ddlPaisDuenio"]').val(0);
            $('input[id="Empresa_txtCelularDuenio"]').val('');
            $('input[id="Empresa_txtNroDocumentoEmpresa"]').val('');
            $('select[id="Empresa_ddlTipoDocumentoEmpresa"]').val(0);
            $('input[id="Empresa_txtRazonSocialEmpresa"]').val('');
            $('input[id="Empresa_txtDireccionEmpresa"]').val('');
            $('input[id="Empresa_txtNombreComercialEmpresa"]').val('');
            $('input[id="Empresa_txtTelefonoEmpresa"]').val('');
            //$("#gridEmpresa").DataTable().ajax.reload();
            
            //$('select[id="Empresa_Producto_CodigoProductoArea"]').val('');
            //$('input[id="Empresa_Producto_EstadoActivo"]').prop('checked', false);
            //$('input[id="Empresa_Producto_EstadoInactivo"]').prop('checked', false);
            document.getElementById('closeModalEmpresaCorporativo').click();
            
        }
        else {
            MostrarMensaje("Error", "vuelva a intentar nuevamente!");
        }
    };
    //var metodoError = function (msg) {
    //    alert(msg);
    //};
    //var request = {
    //    request: entidad
    //};
    //LlamarAJAX("/corporativo/RegistrarEmpresa", request, metodoCorrecto, metodoError);
}

function BuscarEmpresa(CodigoUnidadNegocio,CodigoSede) {
    var entidad = {};
    
    entidad.CodigoUnidadNegocio = CodigoUnidadNegocio;
    entidad.CodigoSede = CodigoSede;
    
    if (IsUndefinedOrNullOrEmpty(entidad.CodigoUnidadNegocio)) {
        MostrarMensaje("Error", "Falta selecionar unidad de negocio");
        return;
    }
    else if (IsUndefinedOrNullOrEmpty(entidad.CodigoSede)) {
        MostrarMensaje("Error", "Falta seleccionar sede");
        return;
    }
    
    var metodoCorrecto = function (msg) {
        debugger;
        //msg.Success
        if (msg) {

            $('#Empresa_BtnGuardarEmpresa').html('Actualizar Producto');

            $('input[id="hdCodigoUnidadNegocioEmpresa"]').val(CodigoUnidadNegocio);
            $('input[id="hdCodigoSedeEmpresa"]').val(CodigoSede);
            $('input[id="hdAccionEmpresaCorporativo"]').val('E');

            $('input[id="Empresa_txtNombresDuenio"]').val(msg.NombreDuenio);
            $('input[id="Empresa_txtApellidosDuenio"]').val(msg.ApellidosDuenio);
            $('input[id="Empresa_txtCorreoDuenio"]').val(msg.CorreoDuenio);
            $('select[id="Empresa_ddlPaisDuenio"]').val(msg.CodigoPais);
            $('input[id="Empresa_txtCelularDuenio"]').val(msg.CelularDuenio);
            $('select[id="Empresa_ddlTipoDocumentoEmpresa"]').val(msg.TipoDocumentoEmpresa);
            $('input[id="Empresa_txtNroDocumentoEmpresa"]').val(msg.NroDocumentoEmpresa);
            $('input[id="Empresa_txtRazonSocialEmpresa"]').val(msg.RazonSocialEmpresa);
            $('input[id="Empresa_txtDireccionEmpresa"]').val(msg.DireccionEmpresa);
            $('input[id="Empresa_txtNombreComercialEmpresa"]').val(msg.NombreComercialEmpresa);
            $('input[id="Empresa_txtTelefonoEmpresa"]').val(msg.TelefonoEmpresa);
            var fechaG = ConvertirJsonFechaToDatetime(msg.FechaAniversarioEmpresa);
            $('input[id="Empresa_txtAniversario"]').datepicker('setDate', fechaG);

            $('input[id="Empresa_txtCorreoEmpresa"]').val(msg.CorreoEmpresa);
            $('input[id="Empresa_txtSubDominioEmpresa"]').val(msg.SubDominio);
            $('select[id="Empresa_ddlEstadoEmpresa"]').val(msg.Estado);
            $('input[id="Empresa_hdIdEmpresa"]').val(msg.IdEmpresa);

            document.getElementById('btnEditarEmpresa').click();
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
    LlamarAJAX("/corporativo/BuscarEmpresa", request, metodoCorrecto, metodoError);
}

function CargarDetalleIconos() {

    var Descripcion = ConvertToStringFromObject($('select[id="Categorias_ddlTipoIcono"] option:selected').val());
    var metodoCorrecto = function (data) {
        var content_A = new Array();
        //content_A.push('<option value="0">Seleccionar</option>');
        for (var i = 0; i < data.length; i++) {
           
            content_A.push('<tr>');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<img src="' + data[i].urlImagen + '" alt="" />');
            content_A.push('</td>');
            content_A.push('<td>');
            content_A.push('<span class="tx-11 d-block">"' + data[i].urlImagen + '"</span>');
            content_A.push('</td>');
            content_A.push('<td><input type="radio" name="rbtDetalleIcono" value="' + data[i].urlImagen + '" id="rbtDetalleIcono_' + data[i].Codigo + '"  style="height:25px;width:25px;" /></td>');         
            content_A.push('</tr>');
                
        }
        $('#Categorias_ddlDetalleIcono').html(content_A.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        Descripcion: Descripcion
    };

    LlamarAJAX('/corporativo/ListarIconosCategorias', request, metodoCorrecto, metodoError);
}

//CATEGORIAS
function NuevaCategoria(nivel) {
    $('input[id="hdNivelCategorias"]').val(nivel);
    $('input[id="hdAccionCategorias"]').val('N');
    document.getElementById('Categorias_BtnEliminarCategorias').style.display = 'none';
}

function GuardarCategorias() {
    var nivel = $('input[id="hdNivelCategorias"]').val();
    if (nivel == 1) {
        GuardarCategoriasPrimario();
    } else if (nivel == 2) {
        GuardarCategoriasSegundo();
    } else if (nivel == 3) {
        GuardarCategoriasTercero();
    } else if (nivel == 4) {
        GuardarCategoriasCuarta();
    }

}

function GuardarCategoriasPrimario() {

    var Accion = $('input[id="hdAccionCategorias"]').val();

    if (Accion == 'E') {
        ActualizarCategorias();
    } else {
        var entidad = {};
        debugger;
        entidad.CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
        entidad.CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
        entidad.CodigoMenu = 0;
        entidad.CodigoMenuSuperior = 0;
        entidad.Descripcion = ConvertToStringFromObject($('input[id="Categorias_txtDescripcion"]').val());
        entidad.UrlUbicacion = ConvertToStringFromObject($('input[id="Categorias_txtUrlDireccion"]').val());
        entidad.UrlImagen = $("input[name='rbtDetalleIcono']:checked").val();
        entidad.Tipo = '';
        entidad.Orden = ConvertToStringFromObject($('input[id="Categorias_txtOrden"]').val());
        entidad.Estado = 1;
        entidad.Accion = Accion;
        
        if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
            MostrarMensaje("Error", "Falta ingresar descripción");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.UrlImagen)) {
            MostrarMensaje("Error", "Falta seleccionar icono");
            return;
        }      

        var metodoCorrecto = function (msg) {         
            if (msg) {
                MostrarMensaje("Informacion", "Registro Guardado Correctamente");

                $('input[id="Categorias_txtDescripcion"]').val('');
                $('input[id="Categorias_txtUrlDireccion"]').val('');
                $('input[id="Categorias_txtOrden"]').val('');
               
                ListarCategoriasPrimario();              
                document.getElementById('closeModalCategorias').click();

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
        LlamarAJAX("/corporativo/RegistrarCategorias", request, metodoCorrecto, metodoError);
    }
}

function ListarCategoriasPrimario() {

    var CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
    var CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
  
    var CodigoMenuSuperior = 0;
    var metodoCorrecto = function (data) {
        var content_A = new Array();
        //content_A.push('<option value="0">Seleccionar</option>');
        for (var i = 0; i < data.length; i++) {

            content_A.push('<tr onclick="javascript:SeleccionarCategoriaPrimero(this,' + data[i].CodigoMenu +');">');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<img src="' + data[i].UrlImagen + '" alt="" />');
            content_A.push('</td>');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<label>' + data[i].Orden + '</label>');
            content_A.push('</td>');
            content_A.push('<td>');
            content_A.push('<span class="tx-11 d-block">' + data[i].Descripcion + '</span>');
            content_A.push('</td>');
            content_A.push('<td><i onclick="javascript:BuscarCategorias(1,' + data[i].CodigoMenu + ',' + data[i].CodigoMenuSuperior +');" class="fa fa-cog tx-20" data-toggle="modal" data-target="#modalCategorias"></i></td>');
            content_A.push('</tr>');

        }
        $('#table_CategoriaPrimero').html(content_A.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        CodigoUnidadNegocio: CodigoUnidadNegocio,
        CodigoSede: CodigoSede,
        CodigoMenuSuperior: CodigoMenuSuperior
    };

    LlamarAJAX('/corporativo/uspListarCategoriasPrimario', request, metodoCorrecto, metodoError);
}

function SeleccionarCategoriaPrimero(row, CodigoMenu) {
    $('#table_CategoriaPrimero tr').removeClass('selectedrowGrid');
    $(row).addClass('selectedrowGrid');
    $('input[id="hdCodigoMenuSuperior"]').val(CodigoMenu);

    ListarCategoriasSecundario();
}

function ListarCategoriasSecundario() {

    var CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
    var CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
    var CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperior"]').val();

    var metodoCorrecto = function (data) {
        var content_A = new Array();
        //content_A.push('<option value="0">Seleccionar</option>');
        for (var i = 0; i < data.length; i++) {

            content_A.push('<tr onclick="javascript:SeleccionarCategoriaSegundo(this,' + data[i].CodigoMenu + ');">');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<img src="' + data[i].UrlImagen + '" alt="" />');
            content_A.push('</td>');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<label>' + data[i].Orden + '</label>');
            content_A.push('</td>');
            content_A.push('<td>');
            content_A.push('<span class="tx-11 d-block">' + data[i].Descripcion + '</span>');
            content_A.push('</td>');
            content_A.push('<td><i onclick="javascript:BuscarCategorias(1,' + data[i].CodigoMenu + ',' + data[i].CodigoMenuSuperior +');" class="fa fa-cog tx-20" data-toggle="modal" data-target="#modalCategorias" ></i></td>');
            content_A.push('</tr>');

        }
        $('#table_CategoriaSecundario').html(content_A.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        CodigoUnidadNegocio: CodigoUnidadNegocio,
        CodigoSede: CodigoSede,
        CodigoMenuSuperior: CodigoMenuSuperior
    };

    LlamarAJAX('/corporativo/uspListarCategoriasPrimario', request, metodoCorrecto, metodoError);
}

function GuardarCategoriasSegundo() {

    var Accion = $('input[id="hdAccionCategorias"]').val();

    if (Accion == 'E') {
        ActualizarCategorias();
    } else {
        var entidad = {};
        debugger;
        entidad.CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
        entidad.CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
        entidad.CodigoMenu = 0;
        entidad.CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperior"]').val();
        entidad.Descripcion = ConvertToStringFromObject($('input[id="Categorias_txtDescripcion"]').val());
        entidad.UrlUbicacion = ConvertToStringFromObject($('input[id="Categorias_txtUrlDireccion"]').val());
        entidad.UrlImagen = $("input[name='rbtDetalleIcono']:checked").val();
        entidad.Tipo = '';
        entidad.Orden = ConvertToStringFromObject($('input[id="Categorias_txtOrden"]').val());
        entidad.Estado = 1;
        entidad.Accion = Accion;

        if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
            MostrarMensaje("Error", "Falta ingresar descripción");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.UrlImagen)) {
            MostrarMensaje("Error", "Falta seleccionar icono");
            return;
        }

        var metodoCorrecto = function (msg) {
           
            if (msg) {
                MostrarMensaje("Informacion", "Registro Guardado Correctamente");

                $('input[id="Categorias_txtDescripcion"]').val('');
                $('input[id="Categorias_txtUrlDireccion"]').val('');
                $('input[id="Categorias_txtOrden"]').val('');

                ListarCategoriasSecundario();             
                document.getElementById('closeModalCategorias').click();

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
        LlamarAJAX("/corporativo/RegistrarCategorias", request, metodoCorrecto, metodoError);
    }
}

function SeleccionarCategoriaSegundo(row, CodigoMenu) {
    $('#table_CategoriaSecundario tr').removeClass('selectedrowGrid');
    $(row).addClass('selectedrowGrid');
    $('input[id="hdCodigoMenuSuperiorSecundario"]').val(CodigoMenu);

    ListarCategoriasTercero();
}

function ListarCategoriasTercero() {

    var CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
    var CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
    var CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperiorSecundario"]').val();

    var metodoCorrecto = function (data) {
        var content_A = new Array();
        //content_A.push('<option value="0">Seleccionar</option>');
        for (var i = 0; i < data.length; i++) {

            content_A.push('<tr onclick="javascript:SeleccionarCategoriaTercero(this,' + data[i].CodigoMenu + ');">');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<img src="' + data[i].UrlImagen + '" alt="" />');
            content_A.push('</td>');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<label>' + data[i].Orden + '</label>');
            content_A.push('</td>');
            content_A.push('<td>');
            content_A.push('<span class="tx-11 d-block">' + data[i].Descripcion + '</span>');
            content_A.push('</td>');
            content_A.push('<td><i onclick="javascript:BuscarCategorias(1,' + data[i].CodigoMenu + ',' + data[i].CodigoMenuSuperior +');" class="fa fa-cog tx-20" data-toggle="modal" data-target="#modalCategorias" ></i></td>');
            content_A.push('</tr>');

        }
        $('#table_CategoriaTercero').html(content_A.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        CodigoUnidadNegocio: CodigoUnidadNegocio,
        CodigoSede: CodigoSede,
        CodigoMenuSuperior: CodigoMenuSuperior
    };

    LlamarAJAX('/corporativo/uspListarCategoriasPrimario', request, metodoCorrecto, metodoError);
}

function GuardarCategoriasTercero() {

    var Accion = $('input[id="hdAccionCategorias"]').val();

    if (Accion == 'E') {
        ActualizarCategorias();
    } else {
        var entidad = {};
        debugger;
        entidad.CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
        entidad.CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
        entidad.CodigoMenu = 0;
        entidad.CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperiorSecundario"]').val();
        entidad.Descripcion = ConvertToStringFromObject($('input[id="Categorias_txtDescripcion"]').val());
        entidad.UrlUbicacion = ConvertToStringFromObject($('input[id="Categorias_txtUrlDireccion"]').val());
        entidad.UrlImagen = $("input[name='rbtDetalleIcono']:checked").val();
        entidad.Tipo = '';
        entidad.Orden = ConvertToStringFromObject($('input[id="Categorias_txtOrden"]').val());
        entidad.Estado = 1;
        entidad.Accion = Accion;

        if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
            MostrarMensaje("Error", "Falta ingresar descripción");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.UrlImagen)) {
            MostrarMensaje("Error", "Falta seleccionar icono");
            return;
        }

        var metodoCorrecto = function (msg) {
            if (msg) {
                MostrarMensaje("Informacion", "Registro Guardado Correctamente");

                $('input[id="Categorias_txtDescripcion"]').val('');
                $('input[id="Categorias_txtUrlDireccion"]').val('');
                $('input[id="Categorias_txtOrden"]').val('');

                ListarCategoriasTercero();            
                document.getElementById('closeModalCategorias').click();
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
        LlamarAJAX("/corporativo/RegistrarCategorias", request, metodoCorrecto, metodoError);
    }
}

function SeleccionarCategoriaTercero(row, CodigoMenu) {
    $('#table_CategoriaTercero tr').removeClass('selectedrowGrid');
    $(row).addClass('selectedrowGrid');
    $('input[id="hdCodigoMenuSuperiorTercero"]').val(CodigoMenu);

    ListarCategoriasCuarta();
}

function ListarCategoriasCuarta() {

    var CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
    var CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
    var CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperiorTercero"]').val();

    var metodoCorrecto = function (data) {
        var content_A = new Array();
        //content_A.push('<option value="0">Seleccionar</option>');
        for (var i = 0; i < data.length; i++) {

            content_A.push('<tr onclick="">');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<img src="' + data[i].UrlImagen + '" alt="" />');
            content_A.push('</td>');
            content_A.push('<td class="pd-l-20">');
            content_A.push('<label>' + data[i].Orden + '</label>');
            content_A.push('</td>');
            content_A.push('<td>');
            content_A.push('<span class="tx-11 d-block">' + data[i].Descripcion + '</span>');
            content_A.push('</td>');
            content_A.push('<td><i onclick="javascript:BuscarCategorias(1,' + data[i].CodigoMenu + ',' + data[i].CodigoMenuSuperior +');" class="fa fa-cog tx-20" data-toggle="modal" data-target="#modalCategorias" ></i></td>');
            content_A.push('</tr>');

        }
        $('#table_CategoriaCuarto').html(content_A.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        CodigoUnidadNegocio: CodigoUnidadNegocio,
        CodigoSede: CodigoSede,
        CodigoMenuSuperior: CodigoMenuSuperior
    };

    LlamarAJAX('/corporativo/uspListarCategoriasPrimario', request, metodoCorrecto, metodoError);
}

function GuardarCategoriasCuarta() {

    var Accion = $('input[id="hdAccionCategorias"]').val();

    if (Accion == 'E') {
        ActualizarCategorias();
    } else {
        var entidad = {};
        debugger;
        entidad.CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
        entidad.CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
        entidad.CodigoMenu = 0;
        entidad.CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperiorTercero"]').val();
        entidad.Descripcion = ConvertToStringFromObject($('input[id="Categorias_txtDescripcion"]').val());
        entidad.UrlUbicacion = ConvertToStringFromObject($('input[id="Categorias_txtUrlDireccion"]').val());
        entidad.UrlImagen = $("input[name='rbtDetalleIcono']:checked").val();
        entidad.Tipo = '';
        entidad.Orden = ConvertToStringFromObject($('input[id="Categorias_txtOrden"]').val());
        entidad.Estado = 1;
        entidad.Accion = Accion;

        if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
            MostrarMensaje("Error", "Falta ingresar descripción");
            return;
        }
        else if (IsUndefinedOrNullOrEmpty(entidad.UrlImagen)) {
            MostrarMensaje("Error", "Falta seleccionar icono");
            return;
        }

        var metodoCorrecto = function (msg) {
            if (msg) {
                MostrarMensaje("Informacion", "Registro Guardado Correctamente");

                $('input[id="Categorias_txtDescripcion"]').val('');
                $('input[id="Categorias_txtUrlDireccion"]').val('');
                $('input[id="Categorias_txtOrden"]').val('');

                ListarCategoriasCuarta();
                document.getElementById('closeModalCategorias').click();
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
        LlamarAJAX("/corporativo/RegistrarCategorias", request, metodoCorrecto, metodoError);
    }
}

function BuscarCategorias(nivel, CodigoMenu, CodigoMenuSuperior) {
   
    $('input[id="hdNivelCategorias"]').val(nivel);
    $('input[id="hdAccionCategorias"]').val('E');
    $('input[id="hdCodigoMenuActualizar"]').val(CodigoMenu);
    $('input[id="hdCodigoMenuSuperiorActualizar"]').val(CodigoMenuSuperior);
    document.getElementById('Categorias_BtnEliminarCategorias').style.display = '';
         
    var entidad = {};
    debugger;
    entidad.CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
    entidad.CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
    entidad.CodigoMenu = CodigoMenu;
    entidad.CodigoMenuSuperior = CodigoMenuSuperior;
    
    var metodoCorrecto = function (msg) {
        if (msg) {
            
            $('input[id="Categorias_txtDescripcion"]').val(msg.Descripcion);
            $('input[id="Categorias_txtUrlDireccion"]').val(msg.UrlUbicacion);
            $('input[id="Categorias_txtOrden"]').val(msg.Orden);
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
    LlamarAJAX("/corporativo/BuscarCategorias", request, metodoCorrecto, metodoError);

}

function ActualizarCategorias() {
    
    var Accion = $('input[id="hdAccionCategorias"]').val();
    var entidad = {};
    debugger;
    entidad.CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
    entidad.CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
    entidad.CodigoMenu = $('input[id="hdCodigoMenuActualizar"]').val();
    entidad.CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperiorActualizar"]').val();
    entidad.Descripcion = ConvertToStringFromObject($('input[id="Categorias_txtDescripcion"]').val());
    entidad.UrlUbicacion = ConvertToStringFromObject($('input[id="Categorias_txtUrlDireccion"]').val());
    entidad.Tipo = '';
    entidad.Orden = ConvertToStringFromObject($('input[id="Categorias_txtOrden"]').val());
    entidad.Accion = Accion;

    if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
        MostrarMensaje("Error", "Falta ingresar descripción");
        return;
    }   

    var metodoCorrecto = function (msg) {
        if (msg) {
            MostrarMensaje("Informacion", "Registro Actualizado Correctamente");

            $('input[id="Categorias_txtDescripcion"]').val('');
            $('input[id="Categorias_txtUrlDireccion"]').val('');
            $('input[id="Categorias_txtOrden"]').val('');

            document.getElementById('closeModalCategorias').click();

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
    LlamarAJAX("/corporativo/RegistrarCategorias", request, metodoCorrecto, metodoError);
}

function EliminarCategorias() {

    var Accion = $('input[id="hdAccionCategorias"]').val();
    var entidad = {};
    debugger;
    entidad.CodigoUnidadNegocio = $('input[id="hdCodigoUnidadNegocioEmpresa"]').val();
    entidad.CodigoSede = $('input[id="hdCodigoSedeEmpresa"]').val();
    entidad.CodigoMenu = $('input[id="hdCodigoMenuActualizar"]').val();
    entidad.CodigoMenuSuperior = $('input[id="hdCodigoMenuSuperiorActualizar"]').val();
    
    var metodoCorrecto = function (msg) {
        if (msg) {
            MostrarMensaje("Informacion", "Registro Eliminado Correctamente");
            
            document.getElementById('closeModalCategorias').click();

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
    LlamarAJAX("/corporativo/EliminarCategorias", request, metodoCorrecto, metodoError);
}




