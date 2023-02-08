
function LimpiarHoraAgenda_Prospectossincita() {
    $('#hdHoraAgenda_Prospectossincita').val('');
}

function LimpiarHoraAgenda() {
    $('#hdHoraAgenda').val('');
}

function LimpiarHoraAgenda_Oportunidades() {
    $('#hdHoraReagendarAgenda_Oportunidades').val('');
}

function uspValidarExisteCita_Usuario_Oportunidades() {

    //var CodigoSocio = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoprospecto').val();
    //var CodigoTipoAgenda = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoorigenprospecto').val();
    var Usuario = $("#dllVendedorReagendarAgenda_Oportunidades").data("kendoDropDownList").value();
    var Clave = $('#txtValidarClaveReagendar_Oportunidades').val();
    $('button[type="button"]').attr("disabled", true);

    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Usuario + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidarExisteVendedorActivo > 0) {
                if (msg.ValidacionUsuario > 0) {
                    GuardarAgendaSeguimientoTodos_Oportunidades();
                } else {
                    $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
                }

            } else {
                GuardarAgendaSeguimientoTodos_Oportunidades();
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function uspValidarExisteCita_Usuario_AgendaGeneral() {

    var hdCodigoOrigenProspecto = $('#hdCodigoOrigen_Prospecto').val();

    var CodigoSocio = $('#txtCodigo_SocioIAgenda').val();
    var CodigoTipoAgenda = $('#hdCodigoOrigen').val() == '10' ? hdCodigoOrigenProspecto : $('#hdCodigoOrigen').val();
    var Usuario = $("#txtVendedorIAgenda").data("kendoDropDownList").value();
    var Clave = $('#txtValidarClave').val();
    $('button[type="button"]').attr("disabled", true);

    $.ajax({
        data: '{"CodigoSocio":"' + CodigoSocio + '","CodigoTipoAgenda":"' + CodigoTipoAgenda + '","Usuario":"' + Usuario + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarExisteCita_Usuario_AgendaGeneral",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionExisteCita > 0) {
                $.bootstrapGrowl("ya se agendo citas al cliente.", { type: 'danger', width: 'auto' });
            } else {
                if (msg.ValidacionUsuario > 0) {
                    GuardarAgendaSeguimientoTodos();
                } else {
                    $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
                }
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function uspValidarExisteCita_Usuario_Prospectossincita() {

    $('button[type="button"]').attr("disabled", true);

    var CodigoSocio = $('#hdCodigoProspecto_Prospectossincita').val();
    var CodigoTipoAgenda = $('#hdCodigoOrigen_Prospectossincita').val();
    var Usuario = '';

    if ($("#ddlVendedor_ProspectosSinCita").data("kendoDropDownList").value().toString().toUpperCase() == 'OTROSVENDEDORES') {
        Usuario = getCookie("_Usuario_Business");
    } else {
        Usuario = $('#hdVendedor_Prospectossincita').val();
    }

    var Clave = $('#txtValidarClave_Prospectossincita').val();


    $.ajax({
        data: '{"CodigoSocio":"' + CodigoSocio + '","CodigoTipoAgenda":"' + CodigoTipoAgenda + '","Usuario":"' + Usuario + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarExisteCita_Usuario_AgendaGeneral",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionExisteCita > 0) {
                $.bootstrapGrowl("ya se agendo citas al cliente.", { type: 'danger', width: 'auto' });
            } else {
                if (msg.ValidacionUsuario > 0) {
                    GuardarAgendaSeguimientoTodos_Prospectossincita();
                } else {
                    $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
                }
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function GuardarAgendaSeguimientoTodos() {
    var hdCodigoOrigenProspecto = $('#hdCodigoOrigen_Prospecto').val();

    $('button[type="button"]').attr("disabled", true);
    var CodigoTipoAgenda = $('#hdCodigoOrigen').val() == '10' ? hdCodigoOrigenProspecto : $('#hdCodigoOrigen').val();
    var CodigoSocio = $('#txtCodigo_SocioIAgenda').val();
    var CodigoAgenda = '0';
    var DescTipo = 0;
    var color = "";
    var asunto = $('#txtAsunto').val();
    var diaG = kendo.toString($("#txtFechaAgenda").data('kendoDatePicker').value(), 'dd');
    var MesG = kendo.toString($("#txtFechaAgenda").data('kendoDatePicker').value(), 'MM');
    var AnioG = kendo.toString($("#txtFechaAgenda").data('kendoDatePicker').value(), 'yyyy');
    var fecha = AnioG + "|" + MesG + "|" + diaG + "|";
    var hi = $('#hdHoraAgenda').val() == '' ? '00|00|00' : $('#hdHoraAgenda').val();
    var HoraInicio = fecha + hi;

    var Estado = 1;
    var User = $("#txtVendedorIAgenda").data("kendoDropDownList").value();
    var CodigoPaquete = 0;
    var TipoActividad = $('#ddlTipoActividad_IngresarProspectos').data('kendoDropDownList').value();

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"Codigo":"' + CodigoAgenda + '","CodigoSocio":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","DescTipo":"' + DescTipo + '","Asunto":"' + asunto + '","HoraInicio":"' + HoraInicio + '","Color":"' + color + '","User":"' + User + '","Estado":"' + Estado + '","CodigoPaquete":"' + CodigoPaquete + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/GuardarAgendaSeguimientoTodos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han agregado correctamente.", { type: 'success', width: 'auto' });
                $('#txtAsunto').val('');
                $('#txtValidarClave').val('');
                LimpiarHoraAgenda();
                var hdCodigoOrigenProspecto = $('#hdCodigoOrigen_Prospecto').val();
                var CodigoTipoAgenda = $('#hdCodigoOrigen').val() == '10' ? hdCodigoOrigenProspecto : $('#hdCodigoOrigen').val();
                if (CodigoTipoAgenda == 1) {
                    ListartablaProspectos();
                } else if (CodigoTipoAgenda == 4) {
                    ListarTablaInvitados();
                } else if (CodigoTipoAgenda == 5) {
                    ListarTablaReferidos();
                } else if (CodigoTipoAgenda == 6) {
                    ListarTablaLlamadaEntrante();
                } else if (CodigoTipoAgenda == 7) {
                    uspListarTablaWeb_Paginacion();
                }

            } else {
                $.bootstrapGrowl("Error, no hemos podido guardar la información.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            document.getElementById('loadMe').style.display = 'none';
        }

    });

}

function GuardarAgendaSeguimientoTodos_Prospectossincita() {

    $('button[type="button"]').attr("disabled", true);
    var CodigoTipoAgenda = $('#hdCodigoOrigen_Prospectossincita').val();
    var CodigoSocio = $('#hdCodigoProspecto_Prospectossincita').val();
    var CodigoAgenda = '0';
    var DescTipo = 0;
    var color = "";
    var asunto = $('#txtAsunto_Prospectossincita').val();
    var diaG = kendo.toString($("#txtFechaAgenda_Prospectossincita").data('kendoDatePicker').value(), 'dd');
    var MesG = kendo.toString($("#txtFechaAgenda_Prospectossincita").data('kendoDatePicker').value(), 'MM');
    var AnioG = kendo.toString($("#txtFechaAgenda_Prospectossincita").data('kendoDatePicker').value(), 'yyyy');
    var fecha = AnioG + "|" + MesG + "|" + diaG + "|";
    var hi = $('#hdHoraAgenda_Prospectossincita').val() == '' ? '00|00|00' : $('#hdHoraAgenda_Prospectossincita').val();
    var HoraInicio = fecha + hi;
    var Estado = 1;
    var User = '';
    if ($("#ddlVendedor_ProspectosSinCita").data("kendoDropDownList").value().toString().toUpperCase() == 'OTROSVENDEDORES') {
        User = getCookie("_Usuario_Business");
    } else {
        User = $('#hdVendedor_Prospectossincita').val();
    }
    var CodigoPaquete = 0;

    var TipoActividad = $('#ddlTipoActividad_Prospectossincita').data('kendoDropDownList').value();

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"Codigo":"' + CodigoAgenda + '","CodigoSocio":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","DescTipo":"' + DescTipo + '","Asunto":"' + asunto + '","HoraInicio":"' + HoraInicio + '","Color":"' + color + '","User":"' + User + '","Estado":"' + Estado + '","CodigoPaquete":"' + CodigoPaquete + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/GuardarAgendaSeguimientoTodos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han agregado correctamente.", { type: 'success', width: 'auto' });
                $('#txtAsunto_Prospectossincita').val('');
                $('#txtValidarClave_Prospectossincita').val('');
                LimpiarHoraAgenda_Prospectossincita();
                event_CerrarModalProspectoSinCita();

            } else {
                $.bootstrapGrowl("Error, no hemos podido guardar la información.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            ListarProspectosSinActividadAgendaComercial();
            $('button[type="button"]').attr("disabled", false);
            document.getElementById('loadMe').style.display = 'none';
        }

    });

}

function GuardarAgendaSeguimientoTodos_Oportunidades() {

    $('button[type="button"]').attr("disabled", true);
    var CodigoTipoAgenda = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoorigenprospecto').val();
    var CodigoSocio = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoprospecto').val();
    var CodigoAgenda = '0';
    var DescTipo = 0;
    var color = "";
    var asunto = $('#txtAsuntoReagendar_Oportunidades').val();
    var diaG = kendo.toString($("#txtFechaReagendarAgenda_Oportunidades").data('kendoDatePicker').value(), 'dd');
    var MesG = kendo.toString($("#txtFechaReagendarAgenda_Oportunidades").data('kendoDatePicker').value(), 'MM');
    var AnioG = kendo.toString($("#txtFechaReagendarAgenda_Oportunidades").data('kendoDatePicker').value(), 'yyyy');
    var fecha = AnioG + "|" + MesG + "|" + diaG + "|";
    var hi = $('#hdHoraReagendarAgenda_Oportunidades').val() == '' ? '00|00|00' : $('#hdHoraReagendarAgenda_Oportunidades').val();
    var HoraInicio = fecha + hi;

    var Estado = 1;
    var User = $("#dllVendedorReagendarAgenda_Oportunidades").data("kendoDropDownList").value();
    var CodigoPaquete = 0;
    var TipoActividad = $('#ddlTipoActividad_ActividadesAgendar_Oportunidades').data('kendoDropDownList').value();

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"Codigo":"' + CodigoAgenda + '","CodigoSocio":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","DescTipo":"' + DescTipo + '","Asunto":"' + asunto + '","HoraInicio":"' + HoraInicio + '","Color":"' + color + '","User":"' + User + '","Estado":"' + Estado + '","CodigoPaquete":"' + CodigoPaquete + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/GuardarAgendaSeguimientoTodos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han agregado correctamente.", { type: 'success', width: 'auto' });
                $('#txtAsuntoReagendar_Oportunidades').val('');
                $('#txtValidarClaveReagendar_Oportunidades').val('');
                LimpiarHoraAgenda_Oportunidades();

            } else {
                $.bootstrapGrowl("Error, no hemos podido guardar la información.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {


            event_ListarHistorialActividades_Oportunidades(CodigoSocio, CodigoTipoAgenda);

            $('button[type="button"]').attr("disabled", false);
            document.getElementById('loadMe').style.display = 'none';
        }

    });

}

//INGRESO PROSPECTOS
function listaVendedoresIAgenda() {

    $("#txtVendedorIAgenda,#txtVendedorIAgenda_invitados,#txtVendedorIAgenda_referidos,#txtVendedorIAgenda_llamadaentrante,#txtVendedorIAgenda_web").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarAsesoresComercialesAgenda",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                        }, complete: function () {
                            var User = getCookie("_Usuario_Business");
                            $('#txtVendedorIAgenda').data("kendoDropDownList").value(User.toString().toLowerCase());
                            $('#txtVendedorIAgenda_invitados').data("kendoDropDownList").value(User.toString().toLowerCase());
                            $('#txtVendedorIAgenda_referidos').data("kendoDropDownList").value(User.toString().toLowerCase());
                            $('#txtVendedorIAgenda_llamadaentrante').data("kendoDropDownList").value(User.toString().toLowerCase());

                        }
                    });
                }
            }
        }, change: function () {
            var CodigoOrigen = $('#hdCodigoOrigen').val();
            //if (CodigoOrigen == 1) {
            //    ListartablaProspectos();
            //} else if (CodigoOrigen == 4) {
            //    ListarTablaInvitados();
            //} else if (CodigoOrigen == 5) {
            //    ListarTablaReferidos();
            //} else if (CodigoOrigen == 6) {
            //    ListarTablaLlamadaEntrante();
            //} else if (CodigoOrigen == 7) {
            //    uspListarTablaWeb_Paginacion();
            //}

        }
    }).data("kendoDropDownList");
}

function listardllPaquetesIAgenda() {
    var ddlPaquete = $("#dllPaquetesIAgenda").kendoDropDownList({
        filter: "startswith",
        optionLabel: "SELECCIONE PLAN",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="dllPaquetesIAgenda_listbox"]').val();
                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        }

    }).data("kendoDropDownList");

}

function LimpiarDatos() {
    $('#infoCelular_ProspectoNuevo').html('000000000');
    $('#imginfoCelular_ProspectoNuevo').attr('href', 'https://api.whatsapp.com/');
    //$('#lblNombreCompletoClientes').val("");
    $('#txtNombre_SocioIAgenda').val("");
    $('#txtApellidos_SocioIAgenda').val("");
    $('#txtTelefono_SocioIAgenda').val("");
    $('#txtCelular_SocioIAgenda').val("");
    $('#txtDni_SocioIAgenda').val("");
    $('#txtEmail_SocioIAgenda').val("");
    $("#dllPaquetesIAgenda").data("kendoDropDownList").select(0);
    var todayDate = new Date();
    $('#txtFechaNacimientoIAgenda').data("kendoDatePicker").value(todayDate);
    $('#chkHijosIAgenda').prop('checked', false);
    document.getElementById("txtCantHijos").disabled = true;
    $('#txtCantHijos').val("");
    $('#txtCodigo_SocioIAgenda').val(0);

    document.getElementById('btnActualizarDatosAgenda').style.display = 'none';
    document.getElementById('btnEliminarDatosAgenda').style.display = 'none';
    //limpiando encuesta
    $('input[name="orderObjetivoBox[]"]').prop('checked', false);
    $('input[name="orderComoConocioGymBox[]"]').prop('checked', false);
    $("#rbComoConocioGym8").prop('checked', true);
    $('input[name="orderBoxInteres[]"]').prop('checked', false);
    $('#txtPrecio_SocioIAgenda').val('');
}

function uspListarInteresProspectos() {
    var controlHtml = "";
    $('#DivInteres').html("");
    $.ajax({
        type: "POST",
        url: "/gestionce/uspListarInteresProspectos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            for (var i = 0; i < msg.length; i++) {
                controlHtml += '<div class="form-check form-check-inline" style="padding-left: 0;width: 100%;">' +
                    '<input type="checkbox" class="form-check-input" name="orderBoxInteres[]" id="rbDivInteres' + msg[i].CodigoInteres + '" value="' + msg[i].CodigoInteres + '" >' +
                    '<label class="form-check-label" for="rbDivInteres' + msg[i].CodigoInteres + '" style="cursor:pointer;text-transform:lowercase;">' + msg[i].Descripcion + '</label>' +
                    '</div>';

            }

            $('#DivInteres').append(controlHtml);
        }, complete: function () {

        }
    });
}

function validacionNuevoProspecto() {
    var validator = true;
    var CodigoPaquete = $("#dllPaquetesIAgenda").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaquetesIAgenda").data("kendoDropDownList").value();

    if ($('#txtNombre_SocioIAgenda').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar nombre.", { type: 'danger', width: 'auto' });
    } else if ($('#txtApellidos_SocioIAgenda').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar apellidos.", { type: 'danger', width: 'auto' });
    } else if ($('#txtCelular_SocioIAgenda').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar celular.", { type: 'danger', width: 'auto' });
    } else if ($('#txtPrecio_SocioIAgenda').val() == '') {
        validator = false;
        $.bootstrapGrowl("el precio de la promocion debe ser mayor a 0.", { type: 'danger', width: 'auto' });
    } else if (parseFloat($('#txtPrecio_SocioIAgenda').val()) <= 0) {
        validator = false;
        $.bootstrapGrowl("el precio de la promocion debe ser mayor a 0.", { type: 'danger', width: 'auto' });
    } else if (CodigoPaquete == 0) {
        validator = false;
        $.bootstrapGrowl("Falta seleccionar el plan.", { type: 'danger', width: 'auto' });
    } else if (!$('input[name = "orderObjetivoBox[]"]').is(':checked')) {
        validator = false;
        $.bootstrapGrowl("seleccione sub procedencia.", { type: 'danger', width: 'auto' });
    } else if (!$('input[name = "orderComoConocioGymBox[]"]').is(':checked')) {
        validator = false;
        $.bootstrapGrowl("seleccione un interes.", { type: 'danger', width: 'auto' });
    } else if (!$('input[name = "orderBoxInteres[]"]').is(':checked')) {
        validator = false;
        $.bootstrapGrowl("seleccione el interes del cliente nuevo.", { type: 'danger', width: 'auto' });
    } else if ($('#txtValidarClaveProspecto').val() == '') {
        validator = false;
        $.bootstrapGrowl("Ingresar la clave del vendedor.", { type: 'danger', width: 'auto' });
    }

    return validator;
}

function uspValidarUsuarioIngresadoDeProspecto() {
    var Vendedor = $("#txtVendedorIAgenda").data("kendoDropDownList").value();
    var Clave = $('#txtValidarClaveProspecto').val();
    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionUsuario > 0) {
                GuardarTablaProspectos();
            } else {
                $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
            }

        }
    });
}

function GuardarTablaProspectos() {
    $('button[type="button"]').attr("disabled", true);
    var CodigoOrigenProspecto = $('#hdCodigoOrigen_Prospecto').val();
    var Vendedor = $("#txtVendedorIAgenda").data("kendoDropDownList").value();
    var CodigoOrigen = $('#hdCodigoOrigen_Prospecto').val();
    var CodigoProspecto = $('#txtCodigo_SocioIAgenda').val() == '' ? 0 : $('#txtCodigo_SocioIAgenda').val();
    var Nombres = $('#txtNombre_SocioIAgenda').val();
    var Apellidos = $('#txtApellidos_SocioIAgenda').val();
    var Telefono = $('#txtTelefono_SocioIAgenda').val();
    var Celular = $('#txtCelular_SocioIAgenda').val();
    var Correo = $('#txtEmail_SocioIAgenda').val();
    var Genero = "";
    $('input[name="orderSexoBox[]"]:checked').each(function () {
        Genero += $(this).val();
    });

    var TipoCliente = 1;

    var CodigoTipoPaquete = 0;
    var CodigoPaquete = 0;
    var FechaNacimiento = kendo.toString($("#txtFechaNacimientoIAgenda").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var chkHijos = 0;
    var CantHijos = 0;
    var DNI = $('#txtDni_SocioIAgenda').val();
    var Accion = "N";
    var CodigoTiempo = $("#dllPaquetesIAgenda").data("kendoDropDownList").value() == '' ? 0 : $("#dllPaquetesIAgenda").data("kendoDropDownList").value();
    var Precio = $("#txtPrecio_SocioIAgenda").val() == "" ? 0 : $("#txtPrecio_SocioIAgenda").val();

    //datos de la encuesta
    var CodigoObjetivo = "";
    $('input[name="orderObjetivoBox[]"]:checked').each(function () {
        CodigoObjetivo += $(this).val();
    });

    var CodigoComoConocioGym = "";
    $('input[name="orderComoConocioGymBox[]"]:checked').each(function () {
        CodigoComoConocioGym += $(this).val();
    });

    var xml_Interes = "";
    xml_Interes += "<ds>";
    $('input[name = "orderBoxInteres[]"]:checked').each(function () {
        var Cod = $(this).val();
        xml_Interes += "<d>";
        xml_Interes += "<a>" + Cod + "</a>";
        xml_Interes += "</d>";
    });
    xml_Interes += "</ds>";

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"Vendedor":"' + Vendedor + '","CodigoOrigen":"' + CodigoOrigen + '","CodigoProspecto":"' + CodigoProspecto
            + '","Nombres":"' + Nombres + '","Apellidos":"' + Apellidos + '","Telefono":"' + Telefono + '","Celular":"' + Celular + '","Correo":"' + Correo
            + '","Genero":"' + Genero + '","TipoCliente":"' + TipoCliente + '","CodigoTipoPaquete":"' + CodigoTipoPaquete + '","CodigoPaquete":"' + CodigoPaquete
            + '","Hijos":"' + chkHijos + '","CantHijos":"' + CantHijos + '","DNI":"' + DNI + '","Accion":"' + Accion + '","FechaNacimiento":"' + FechaNacimiento
            + '","CodigoObjetivo":"' + CodigoObjetivo + '","CodigoComoConocioGym":"' + CodigoComoConocioGym + '","xml_Interes":"' + xml_Interes + '","CodigoTiempo":"' + CodigoTiempo + '","Precio":"' + Precio + '"}',
        type: "POST",
        url: "/gestionce/GuardarTablaPropectos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg == 1) {
                $.bootstrapGrowl("Es oblogatorio ingresar el nro de documento DNI/CCI/CE.", { type: 'danger', width: 'auto' });
            } else if (msg == 2) {
                $.bootstrapGrowl("Los datos se han guardado correctamente.", { type: 'success', width: 'auto' });
                ListartablaProspectos();
            } else if (msg == 100) {
                $.bootstrapGrowl("Error, no hemos podido guardar la información, intentelo de nuevo.", { type: 'danger', width: 'auto' });
            } else {
                ListarProspectosExistentesDNI(msg);
                //alert("El nro de documento ya existe");
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            //$('#txtValidarClaveProspecto').val('');
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function evento_EliminarProspecto(CodigoProspecto, Vendedor) {
    var User = getCookie("_Usuario_Business");
    var Vendedor = Vendedor.toString().replace('/', '').replace('/', '');

    if (Vendedor.toString().toUpperCase().trim() == User.toString().toUpperCase().trim()) {
        $('#hdConfirmarEliminarProspecto_Codigo').val(CodigoProspecto);
        document.getElementById('myModalConfirmar_EliminarProspecto').style.display = 'block';
    } else {
        alert("No tienes permiso para eliminar a este prospecto");
    }
}

function evento_ConvertirProspectoACliente(CodigoProspecto, CodigoTipoAgenda, Nombres, Apellidos, Vendedor) {
    var User = getCookie("_Usuario_Business");
    var Vendedor = Vendedor.toString().replace('/', '').replace('/', '');
    var Nombres = Nombres.toString().replace('/', '').replace('/', '');
    var Apellidos = Apellidos.toString().replace('/', '').replace('/', '');

    $('#hdCodigoProspecto_Prospectossincita').val(CodigoProspecto);
    $('#hdCodigoOrigen_Prospectossincita').val(CodigoTipoAgenda);
    $('#txtNombre_SocioIAgenda_view_Prospectossincita').val(Nombres);
    $('#txtApellidos_SocioIAgenda_view_Prospectossincita').val(Apellidos);

    if ($("#ddlVendedor_ProspectosSinCita").data("kendoDropDownList").value().toString().toUpperCase() == 'OTROSVENDEDORES') {
        User = getCookie("_Usuario_Business");
        $('#hdVendedor_Prospectossincita').val(User);
        document.getElementById('myModalConfirmar_EnviarACliente_ProspectoSinCita').style.display = 'block';
    } else {

        if (Vendedor.toString().toUpperCase().trim() == User.toString().toUpperCase().trim()) {
            $('#hdVendedor_Prospectossincita').val(Vendedor);
            document.getElementById('myModalConfirmar_EnviarACliente_ProspectoSinCita').style.display = 'block';
        } else {
            alert("No tienes permiso para converir a cliente a este prospecto");
        }
    }

}

function EliminarTablaPropectos() {
    document.getElementById('loadMe').style.display = 'block';
    document.getElementById('myModalConfirmar_EliminarProspecto').style.display = 'none';
    $('button[type="button"]').attr("disabled", true);
    var CodigoProspecto = $('#hdConfirmarEliminarProspecto_Codigo').val();
    if (CodigoProspecto != '') {
        $.ajax({
            data: '{"CodigoProspecto":"' + CodigoProspecto + '"}',
            type: "POST",
            url: "/gestionce/EliminarTablaPropectos",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg > 0) {
                    $.bootstrapGrowl("El prospecto se elimino correctamente.", { type: 'success', width: 'auto' });
                    ListartablaProspectos();
                    ListarProspectosSinActividadAgendaComercial();
                } else {
                    $.bootstrapGrowl("Error, no hemos podido eliminar la información, intentelo de nuevo.", { type: 'danger', width: 'auto' });
                }
            }, complete: function () {
                $('button[type="button"]').attr("disabled", false);
                $('#hdConfirmarEliminarProspecto_Codigo').val('0');
                document.getElementById('loadMe').style.display = 'none';
            }
        });
    } else {
        $.bootstrapGrowl("Seleccione un prospecto.", { type: 'danger', width: 'auto' });
    }

}

function event_btnCerrarmodaldiv_PropspectosExisteDNI() {
    document.getElementById('modaldiv_PropspectosExisteDNI').style.display = 'none';
}

function ListarProspectosExistentesDNI(datos) {

    document.getElementById('loadMe').style.display = 'block';

    $("#gridProspectosExistentesDNI").empty();
    $("#gridProspectosExistentesDNI").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    options.success(datos);
                    document.getElementById('loadMe').style.display = 'none';
                    $('#modaldiv_PropspectosExisteDNI').show('fast');
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 300,
        columns: [{
            template: '<div style="width:25px;margin-left: -3px;"><label style="background-color:#: ColorOrigen #;width: 21px;border-radius:25px;height: 21px;"></label></div>',
            title: "",
            width: 5
        }, {
            field: "desOrigen",
            title: "<b style='color:#fff;font-weight:bold'>Origen</b>",
            width: 7,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "CodigoProspecto",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 7,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "Nombres",
            title: "<b style='color:#fff;font-weight:bold'>Nombres</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "Apellidos",
            title: "<b style='color:#fff;font-weight:bold'>Apellidos</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "DNI",
            title: "<b style='color:#fff;font-weight:bold'>DNI/CCI/CE</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "Vendedor",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        }, change: function () {
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                //var CodigoCliente = dataItem.CodigoProspecto;
            });
        }
    });
}

function ListartablaProspectos() {
    $('#hdCodigoOrigen_Prospecto').val('1'); //NUEVO
    var descripcion = $('#txtBuscador_prospectos').val();
    var User = $('#txtVendedorIAgenda').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicio").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFin").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';
    $("#gridTablaProspectos").empty();
    $("#gridTablaProspectos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"descripcion":"' + descripcion + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/ListarTablaPropectos_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            if (msg.length > 0) {
                                document.getElementById('btnNuevoAgendaDatos').style.display = '';
                                document.getElementById('btnEliminarDatosAgenda').style.display = 'none';
                                document.getElementById('btnActualizarDatosAgenda').style.display = 'none';
                                document.getElementById('btnGuardarDatosAgenda').style.display = 'none';
                            } else {
                                LimpiarDatos();
                                document.getElementById('btnNuevoAgendaDatos').style.display = '';
                                document.getElementById('btnGuardarDatosAgenda').style.display = 'none';

                                document.getElementById('btnActualizarDatosAgenda').style.display = 'none';
                            }
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                            uspListartablaProspectos_NumeroRegistros();
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 550,
        columns: [{
            field: "CodigoProspecto",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 15,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "Vendedor",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 7,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 10,
            attributes: {
                style: "font-size:12px;"
            }
        }],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        }, change: function () {
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoCliente = dataItem.CodigoProspecto;

                document.getElementById('btnGuardarDatosAgenda').style.display = 'none';
                document.getElementById('btnActualizarDatosAgenda').style.display = 'none';
                document.getElementById('btnEliminarDatosAgenda').style.display = 'none';
                BuscarClientesProspectosPorCodigo(CodigoCliente);
            });
        }
    });
}

function uspListartablaProspectos_NumeroRegistros() {

    var descripcion = $('#txtBuscador_prospectos').val();
    var User = $('#txtVendedorIAgenda').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicio").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFin").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    $.ajax({
        data: '{"descripcion":"' + descripcion + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/uspListartablaProspectos_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListartablaProspectos').html(msg.CantTotal);
            ddlListarTablaProspectos_Paginacion(msg.CantTotal, msg.TamanioPagina);

        }, complete: function () {

        }
    });
}

function ddlListarTablaProspectos_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListartablaProspectos').html(htmlOpcion);
    $("#ddlPaginacionuspListartablaProspectos").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListartablaProspectos").data("kendoDropDownList").value();
            ListarTablaProspectos_ChanguePage(nroPagina);
        }
    });
}

function ListarTablaProspectos_ChanguePage(PageNumber) {

    $('#hdCodigoOrigen_Prospecto').val('1'); //NUEVO
    var descripcion = $('#txtBuscador_prospectos').val();
    var User = $('#txtVendedorIAgenda').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicio").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFin").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';

    $("#gridTablaProspectos").empty();
    $("#gridTablaProspectos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"descripcion":"' + descripcion + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/ListarTablaPropectos_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            if (msg.length > 0) {
                                document.getElementById('btnNuevoAgendaDatos').style.display = '';
                                document.getElementById('btnEliminarDatosAgenda').style.display = 'none';
                                document.getElementById('btnActualizarDatosAgenda').style.display = 'none';
                                document.getElementById('btnGuardarDatosAgenda').style.display = 'none';
                            } else {
                                LimpiarDatos();
                                document.getElementById('btnNuevoAgendaDatos').style.display = '';
                                document.getElementById('btnActualizarDatosAgenda').style.display = 'none';
                            }
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 550,
        columns: [{
            field: "CodigoProspecto",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 15,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "Vendedor",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 7,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 10,
            attributes: {
                style: "font-size:12px;"
            }
        }],
        dataBound: function (e) {

            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        }
        ,
        change: function () {

            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoCliente = dataItem.CodigoProspecto;
                document.getElementById('btnGuardarDatosAgenda').style.display = 'none';
                document.getElementById('btnActualizarDatosAgenda').style.display = 'none';
                document.getElementById('btnEliminarDatosAgenda').style.display = 'none';

                BuscarClientesProspectosPorCodigo(CodigoCliente);

            });
        }
    });
}

function BuscarClientesProspectosPorCodigo(CodigoCliente) {

    $.ajax({
        data: '{"CodigoProspecto":"' + CodigoCliente + '"}',
        type: "POST",
        url: "/gestionce/BuscarClientesProspectosPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtCodigo_SocioIAgenda').val(msg.Codigo);
            $('#txtCodigo_BuscarIAgenda').val(msg.Codigo);
            $('#txtNombre_SocioIAgenda').val(msg.Nombres);
            $('#txtApellidos_SocioIAgenda').val(msg.Apellidos);
            $('#txtTelefono_SocioIAgenda').val(msg.Telefono);
            $('#txtCelular_SocioIAgenda').val(msg.Celular);
            $('#txtFechaNacimientoIAgenda').data('kendoDatePicker').value(msg.desFechaNacimiento);
            //  $('#lblNombreCompletoClientes').val(' su codigo es : ' + msg.Codigo);
            if (msg.Hijos == 1) {
                $('#chkHijosIAgenda').prop('checked', true);
                document.getElementById("txtCantHijos").disabled = false;
            } else {
                $('#chkHijosIAgenda').prop('checked', false);
                document.getElementById("txtCantHijos").disabled = true;
            }
            $('#txtCantHijos').val(msg.CantHijos);
            $('#txtDni_SocioIAgenda').val(msg.DNI);
            $('#txtEmail_SocioIAgenda').val(msg.Correo);
            $('#dllPaquetesIAgenda').data("kendoDropDownList").value(msg.CodigoTiempo);
            $('#txtPrecio_SocioIAgenda').val(msg.Precio);

            $('#rbGenero' + msg.Genero).prop('checked', true);

        }, complete: function () {
            uspBuscarEncuesta1Nuevo(CodigoCliente);
        }
    });
}

function uspBuscarEncuesta1Nuevo(CodigoCliente) {

    $.ajax({
        data: '{"CodigoProspecto":"' + CodigoCliente + '"}',
        type: "POST",
        url: "/gestionce/uspBuscarEncuesta1Nuevo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg == null) {
                $('input[name="orderObjetivoBox[]"]').prop('checked', false);
                $('input[name="orderComoConocioGymBox[]"]').prop('checked', false);
                $('input[name="orderBoxInteres[]"]').prop('checked', false);

            } else {
                $('#rbObjetivo' + msg.CodigoObjetivo).prop('checked', true);
                $('#rbComoConocioGym' + msg.CodigoComoConocioGym).prop('checked', true);

                uspListarEncuestaNuevo2(CodigoCliente);
            }

        }, complete: function () {

        }
    });
}

function uspListarEncuestaNuevo2(CodigoCliente) {
    var CodigoOrigenProspecto = $('#hdCodigoOrigen_Prospecto').val();
    $.ajax({
        data: '{"CodigoProspecto":"' + CodigoCliente + '","CodigoOrigenProspecto":"' + CodigoOrigenProspecto + '"}',
        type: "POST",
        url: "/gestionce/uspListarEncuestaNuevo2",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('input[name="orderBoxInteres[]"]').prop('checked', false);
            for (var i = 0; i < msg.length; i++) {

                $('#rbDivInteres' + msg[i].CodigoInteres).prop('checked', true);
            }
        }
    });
}

function evento_EnviarNuevoASocio(CodigoProspecto) {

    if (CodigoProspecto == 0) {
        $.bootstrapGrowl("Falta seleccionar a un prospecto.", { type: 'primary', width: 'auto' });
    } else {
        document.getElementById('myModalConfDeNuevoASocio').style.display = 'block';
    }

}

//FIN INGRESO PROSPECTOS
function ListarProspectosSinActividadAgendaComercial() {

    var descripcion = $('#txtBuscardorProspectos_ProspectosSinCita').val();
    var User = $('#ddlVendedor_ProspectosSinCita').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicio_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFin_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"descripcion":"' + descripcion + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + 1 + '"}',
        type: "POST",
        url: "/gestionce/UspListarProspectosSinActividadAgendaComercial",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var controlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                controlHtml += '<div class="col-md-2" >';
                controlHtml += '<div class="card card__course" style="cursor:pointer;margin-bottom: 0.5rem;">';
                controlHtml += '<div class="d-flex" ondblclick="event_seleccionarProspectoSinCita(' + msg[i].CodigoProspecto + ',' + msg[i].CodigoOrigen + ',/' + msg[i].ColorOrigen + '/,/' + msg[i].Vendedor + '/);" style="border-left-color: red;text-align: left;height: 37px;border-top-color:' + msg[i].ColorOrigen + ';border-top-style: solid;padding-top: 0.35rem;padding-bottom: 0.35rem;padding-left:0.35rem;">';
                controlHtml += '<span class="course__title" style="font-weight:bold;font-size:12px;">' + msg[i].Nombres + ', ' + msg[i].Apellidos + '</span>';
                controlHtml += '    <div onclick="evento_ConvertirProspectoACliente(' + msg[i].CodigoProspecto + ',' + msg[i].CodigoOrigen + ',/' + msg[i].Nombres + '/,/' + msg[i].Apellidos + '/,/' + msg[i].Vendedor + '/)" title="Convertir a cliente" class="fa fa-paper-plane" style="font-size: 20px;padding: 2px;text-align:center;right:7px;position:absolute;"></div>';
                controlHtml += '</div>';
                controlHtml += '<div style="padding-left: 10px;padding-bottom:5px;">';
                controlHtml += '        <div class="mb-2" ondblclick="event_seleccionarProspectoSinCita(' + msg[i].CodigoProspecto + ',' + msg[i].CodigoOrigen + ',/' + msg[i].ColorOrigen + '/,/' + msg[i].Vendedor + '/);">';
                controlHtml += '            <small class="text-muted">Codigo: ' + msg[i].CodigoProspecto + '</small><br />';
                controlHtml += '            <small class="text-muted">Procedencia: ' + msg[i].desOrigen + '</small><br />';

                if (msg[i].CodigoOrigen != 6) {
                    controlHtml += '            <small class="text-muted" style="font-size: 12px;">SubProcedencia: ' + msg[i].DescripcionCCG + '</small><br />';
                }

                controlHtml += '            <small class="text-muted" style="font-size: 12px;">Objetivo: ' + msg[i].DescripcionSP + '</small><br />';
                controlHtml += '            <small class="text-muted" style="font-size: 12px;">Vendedor: ' + msg[i].Vendedor + '</small><br />';
                controlHtml += '            <small class="text-muted" style="font-size: 12px;">Creado: ' + msg[i].DescFechaCreacion + '</small><br />';
                controlHtml += '            <small class="text-muted">Monto: ' + msg[i].Precio + '</small>';
                controlHtml += '        </div>';
                controlHtml += '        <div class="d-flex" style="border-top-width: 1px;border-top-style: solid;border-top-color: #ddd;">';
                controlHtml += '            <strong class="h4 m-0" style="font-size:14px;font-weight:bold;">';
                controlHtml += '                <a style="display:' + msg[i].EstadoCelular + ';color:#000;font-weight:bold;" target="_blank" href="https://api.whatsapp.com/send?phone=' + msg[i].Celular + '"> <img src="/Content/app/img/whatsapp.png" style="height:15px;cursor:pointer;">' + msg[i].Celular + '</a>';
                controlHtml += '            </strong>';
                controlHtml += '                <div title="Eliminar prospecto" onclick="evento_EliminarProspecto(' + msg[i].CodigoProspecto + ',/' + msg[i].Vendedor + '/)" class="fa fa-trash-alt" style="font-size: 20px;padding: 2px;text-align:center;right:7px;position:absolute;"></div>';
                controlHtml += '         </div>';
                controlHtml += '        </div>';
                controlHtml += '    </div>';
                controlHtml += '</div>';

            }

            $('#divProspestosiincita').html(controlHtml);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            UspListarProspectosSinActividadAgendaComercial_NumeroRegistros();
        }
    });

}

function ListarProspectosSinActividadAgendaComercial_ChanguePage(PageNumber) {

    var descripcion = $('#txtBuscardorProspectos_ProspectosSinCita').val();
    var User = $('#ddlVendedor_ProspectosSinCita').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicio_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFin_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"descripcion":"' + descripcion + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
        type: "POST",
        url: "/gestionce/UspListarProspectosSinActividadAgendaComercial",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var controlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                controlHtml += '<div class="col-md-2" >';
                controlHtml += '<div class="card card__course" style="cursor:pointer;margin-bottom: 0.5rem;">';
                controlHtml += '<div class="d-flex" ondblclick="event_seleccionarProspectoSinCita(' + msg[i].CodigoProspecto + ',' + msg[i].CodigoOrigen + ',/' + msg[i].ColorOrigen + '/,/' + msg[i].Vendedor + '/);" style="border-left-color: red;text-align: left;height: 37px;border-top-color:' + msg[i].ColorOrigen + ';border-top-style: solid;padding-top: 0.35rem;padding-bottom: 0.35rem;padding-left:0.35rem;">';
                controlHtml += '<span class="course__title" style="font-weight:bold;font-size:12px;">' + msg[i].Nombres + ', ' + msg[i].Apellidos + '</span>';
                controlHtml += '</div>';
                controlHtml += '<div style="padding-left: 10px;padding-bottom:5px;">';
                controlHtml += '        <div class="mb-2" ondblclick="event_seleccionarProspectoSinCita(' + msg[i].CodigoProspecto + ',' + msg[i].CodigoOrigen + ',/' + msg[i].ColorOrigen + '/,/' + msg[i].Vendedor + '/);">';
                controlHtml += '            <small class="text-muted">Codigo: ' + msg[i].CodigoProspecto + '</small><br />';
                controlHtml += '            <small class="text-muted">Tipo: ' + msg[i].desOrigen + '</small><br />';

                if (msg[i].CodigoOrigen == 1) {
                    controlHtml += '            <small class="text-muted" style="font-size: 12px;">Procedencia: ' + msg[i].DescripcionCCG + '</small><br />';
                    controlHtml += '            <small class="text-muted" style="font-size: 12px;">Objetivo: ' + msg[i].DescripcionSP + '</small><br />';
                }

                controlHtml += '            <small class="text-muted" style="font-size: 12px;">Vendedor: ' + msg[i].Vendedor + '</small><br />';
                controlHtml += '            <small class="text-muted" style="font-size: 12px;">Creado: ' + msg[i].DescFechaCreacion + '</small><br />';
                controlHtml += '            <small class="text-muted">Monto: ' + msg[i].Precio + '</small>';
                controlHtml += '        </div>';
                controlHtml += '        <div class="d-flex" style="border-top-width: 1px;border-top-style: solid;border-top-color: #ddd;">';
                controlHtml += '            <strong class="h4 m-0" style="font-size:16px;font-weight:bold;">';
                controlHtml += '                <a style="display:' + msg[i].EstadoCelular + ';color:#000;font-weight:bold;" target="_blank" href="https://api.whatsapp.com/send?phone=' + msg[i].Celular + '"> <img src="/Content/app/img/whatsapp.png" style="height:18px;cursor:pointer;">' + msg[i].Celular + '</a>';
                controlHtml += '                        </strong>';
                controlHtml += '                <div style="background-color:' + msg[i].ColorOrigen + ';font-size: 12px;padding: 2px;height: 25px;width:25px;text-align:center;color:#fff;font-weight:bold;border-radius:50%;right:7px;position:absolute;">' + (i + 1) + '</div>';
                controlHtml += '                    </div>';
                controlHtml += '        </div>';
                controlHtml += '    </div>';
                controlHtml += '</div>';

            }

            $('#divProspestosiincita').html(controlHtml);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';

        }
    });

}

function UspListarProspectosSinActividadAgendaComercial_NumeroRegistros() {

    var descripcion = $('#txtBuscardorProspectos_ProspectosSinCita').val();
    var User = $('#ddlVendedor_ProspectosSinCita').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicio_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFin_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"descripcion":"' + descripcion + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/UspListarProspectosSinActividadAgendaComercial_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidad_ProspectosSinCita').html(msg.CantidadTotal);
            $('#lblProyeccion_ProspectosSinCita').html(parseFloat(msg.Precio).toFixed(2));
            ddlListarUspListarProspectosSinActividadAgendaComercial_Paginacion(msg.CantidadTotal, msg.TamanioPagina);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function ddlListarUspListarProspectosSinActividadAgendaComercial_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionUspListarProspectosSinActividadAgendaComercial_Paginacion').html(htmlOpcion);
    $("#ddlPaginacionUspListarProspectosSinActividadAgendaComercial_Paginacion").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionUspListarProspectosSinActividadAgendaComercial_Paginacion").data("kendoDropDownList").value();
            ListarProspectosSinActividadAgendaComercial_ChanguePage(nroPagina);
        }
    });
}



//PROSPECTOS SIN CITA

function event_BuscarClientesProspectosPorCodigo__Prospectossincita(CodigoCliente) {

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"CodigoProspecto":"' + CodigoCliente + '"}',
        type: "POST",
        url: "/gestionce/BuscarClientesProspectosPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#myModal_Prospectossincita_Titulo').html('<a style="display:block;color:#000;font-weight:bold;" target="_blank" href="https://api.whatsapp.com/send?phone=' + msg.Celular + '">' + '(' + msg.Codigo + ') ' + msg.Nombres + ', ' + msg.Apellidos + ' - ' + msg.Celular + '&nbsp;&nbsp;<img src="/Content/app/img/whatsapp.png" style="height:20px;cursor:pointer;"></a>');
            $('#txtNombre_SocioIAgenda_view_Prospectossincita').val(msg.Nombres);
            $('#txtApellidos_SocioIAgenda_view_Prospectossincita').val(msg.Apellidos);
            $('#txtTelefono_SocioIAgenda_view_Prospectossincita').val(msg.Telefono);
            $('#txtCelular_SocioIAgenda_view_Prospectossincita').val(msg.Celular);
            $('#txtEmail_SocioIAgenda_view_Prospectossincita').val(msg.Correo);
            $('#txtPrecio_SocioIAgenda_view_Prospectossincita').val(msg.Precio);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModal_Prospectossincita').style.display = 'block';
        }
    });
}

function event_BuscarClientesDatosInvitadosPorCodigo__Prospectossincita(CodigoInvitado) {

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"CodigoInvitado":"' + CodigoInvitado + '"}',
        type: "POST",
        url: "/gestionce/uspBuscarClientesDatosInvitadosPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#myModal_Prospectossincita_Titulo').html('<a style="display:block;color:#000;font-weight:bold;" target="_blank" href="https://api.whatsapp.com/send?phone=' + msg.Celular + '">' + '(' + msg.CodigoInvitado + ') ' + msg.Nombres + ', ' + msg.Apellidos + ' - ' + msg.Celular + '&nbsp;&nbsp;<img src="/Content/app/img/whatsapp.png" style="height:20px;cursor:pointer;"></a>');
            $('#txtNombre_SocioIAgenda_view_Prospectossincita').val(msg.Nombres);
            $('#txtApellidos_SocioIAgenda_view_Prospectossincita').val(msg.Apellidos);
            $('#txtTelefono_SocioIAgenda_view_Prospectossincita').val(msg.Telefono);
            $('#txtCelular_SocioIAgenda_view_Prospectossincita').val(msg.Celular);
            $('#txtEmail_SocioIAgenda_view_Prospectossincita').val(msg.Correo);
            $('#txtPrecio_SocioIAgenda_view_Prospectossincita').val(msg.Precio);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModal_Prospectossincita').style.display = 'block';
        }
    });
}

function event_BuscarClientesDatosReferidosPorCodigo__Prospectossincita(CodigoReferido) {
    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"CodigoReferido":"' + CodigoReferido + '"}',
        type: "POST",
        url: "/gestionce/uspBuscarClientesDatosReferidosPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#myModal_Prospectossincita_Titulo').html('<a style="display:block;color:#000;font-weight:bold;" target="_blank" href="https://api.whatsapp.com/send?phone=' + msg.Celular + '">' + '(' + msg.CodigoReferido + ') ' + msg.Nombres + ', ' + msg.Apellidos + ' - ' + msg.Celular + '&nbsp;&nbsp;<img src="/Content/app/img/whatsapp.png" style="height:20px;cursor:pointer;"></a>');
            $('#txtNombre_SocioIAgenda_view_Prospectossincita').val(msg.Nombres);
            $('#txtApellidos_SocioIAgenda_view_Prospectossincita').val(msg.Apellidos);
            $('#txtTelefono_SocioIAgenda_view_Prospectossincita').val(msg.Telefono);
            $('#txtCelular_SocioIAgenda_view_Prospectossincita').val(msg.Celular);
            $('#txtEmail_SocioIAgenda_view_Prospectossincita').val(msg.Correo);
            $('#txtPrecio_SocioIAgenda_view_Prospectossincita').val(msg.Precio);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModal_Prospectossincita').style.display = 'block';
        }
    });
}

function event_BuscarClientesDatosLLamadaEPorCodigo__Prospectossincita(CodigoLlamadaE) {
    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"CodigoLlamadaE":"' + CodigoLlamadaE + '"}',
        type: "POST",
        url: "/gestionce/BuscarClientesDatosLLamadaEPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#myModal_Prospectossincita_Titulo').html('<a style="display:block;color:#000;font-weight:bold;" target="_blank" href="https://api.whatsapp.com/send?phone=' + msg.Celular + '">' + '(' + msg.CodigoLlamadaE + ') ' + msg.Nombres + ', ' + msg.Apellidos + ' - ' + msg.Celular + '&nbsp;&nbsp;<img src="/Content/app/img/whatsapp.png" style="height:20px;cursor:pointer;"></a>');
            $('#txtNombre_SocioIAgenda_view_Prospectossincita').val(msg.Nombres);
            $('#txtApellidos_SocioIAgenda_view_Prospectossincita').val(msg.Apellidos);
            $('#txtTelefono_SocioIAgenda_view_Prospectossincita').val(msg.Telefono);
            $('#txtCelular_SocioIAgenda_view_Prospectossincita').val(msg.Celular);
            $('#txtEmail_SocioIAgenda_view_Prospectossincita').val(msg.Correo);
            $('#txtPrecio_SocioIAgenda_view_Prospectossincita').val(msg.Precio);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModal_Prospectossincita').style.display = 'block';
        }
    });
}

//FIN PROSPECTOS SIN CITA

//INGRESO DE INVITADOS

function listardllPaquetesInvitadoIAgenda() {
    var ddlPaquete = $("#dllPaquetesInvitadoIAgenda").kendoDropDownList({
        filter: "startswith",
        optionLabel: "SELECCIONE PLAN",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="dllPaquetesInvitadoIAgenda_listbox"]').val();

                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        }

    }).data("kendoDropDownList");

}

function listaSociosInvitadoPor() {

    $("#ddlSocios_Invitado").kendoAutoComplete({
        dataTextField: "NombreApellido",
        dataValueField: "CodigoSocio",
        template: '<table border="0" style="width:100%;font-size: 12px;">' +
            '<tr>' +
            '<td style="width:100%;>' +
            '<span class="k-state-default" >' +
            '<div style="font-weight: bold;font-size:11px;">' +
            '#:data.Nombres# #:data.Apellidos# - Su codigo: #:data.CodigoSocio#  Su DNI: #:data.DNI# ' +
            '</div>' +
            '</span>' +
            '</td>' +
            '</tr>' +
            '</table>',
        filter: "startswith",
        minLength: 3,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var valor = $('#ddlSocios_Invitado').data('kendoAutoComplete').value();
                    var flag = 1;
                    $.ajax({
                        type: "POST",
                        data: '{"valor":"' + valor + '","flag":"' + flag + '"}',
                        url: "/gestionce/ListaSocios",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });

                }
            }
        }, select: function (e) {
            var dataItem = this.dataItem(e.item.index());
            $('#txtInvitadoPor').val(dataItem.NombreApellido + ' | Codigo: ' + dataItem.CodigoSocio);
            $('#txtCodigoInvitadoPor').val(dataItem.CodigoSocio);
            return false;
        }
    });

}

function sumaFechaDia(d, fecha) {
    var Fecha = new Date();
    var sFecha = fecha || (Fecha.getDate() + "/" + (Fecha.getMonth() + 1) + "/" + Fecha.getFullYear());
    var sep = sFecha.indexOf('/') != -1 ? '/' : '-';
    var aFecha = sFecha.split(sep);
    var fecha = aFecha[2] + '/' + aFecha[1] + '/' + aFecha[0];
    fecha = new Date(fecha);
    fecha.setDate(fecha.getDate() + parseInt(d));
    var anno = fecha.getFullYear();
    var mes = fecha.getMonth() + 1;
    var dia = fecha.getDate();
    mes = (mes < 10) ? ("0" + mes) : mes;
    dia = (dia < 10) ? ("0" + dia) : dia;
    var fechaFinal = dia + sep + mes + sep + anno;
    return (fechaFinal);
}

function LimpiarDatosInvitados() {
    $('#infoCelular_ProspectoInvitado').html('000000000');
    $('#imginfoCelular_ProspectoInvitado').attr('href', 'https://api.whatsapp.com/');
    $('#lblNombreCompletoClientes').val("");
    $('#txtCodigo_SocioIAgenda').val('0');
    $('#txtNombre_SocioIAgenda').focus();
    $('#txtNombreInvitado').val("");
    $('#txtTelefonoInvitado').val("");
    $('#txtApellidoInvitado').val("");
    $('#txtCelularInvitado').val("");
    var todayDate = new Date();
    $('#txtFechaNacimientoInvitado').data('kendoDatePicker').value(todayDate);
    $('#txtFechaInicio').data('kendoDatePicker').value(todayDate);
    $('#txtFechaFinInvitado').data('kendoDatePicker').value(todayDate);
    $('#txtDniInvitado').val("");
    $('#txtCorreoInvitado').val("");
    $('#txtNrDias').val(0);
    $('#txtNrDiasActual').val(0);
    $('#ddlSocios_Invitado').val("");
    $('#txtInvitadoPor').val("");
    $('#txtCodigoInvitadoPor').val(0);
    $("#dllPaquetesInvitadoIAgenda").data("kendoDropDownList").select(0);
    $('#txtPrecioInvitado').val("");
    $('input[name="orderComoConocioGymBox_Prospeccion[]"]').prop('checked', false);
    $('input[name="orderObjetivoBox_Prospeccion[]"]').prop('checked', false);
    //document.getElementById('btnActualizarInvitado').style.display = 'none';
    //document.getElementById('btnEliminarInvitado').style.display = 'none';
}

function validacionNuevoInvitado() {
    var validator = true;

    var CodigoSubProcedencia = '';
    $('input[name="orderComoConocioGymBox_Prospeccion[]"]:checked').each(function () {
        CodigoSubProcedencia += $(this).val();
    });
    var CodigoObjetivo = '';
    $('input[name="orderObjetivoBox_Prospeccion[]"]:checked').each(function () {
        CodigoObjetivo += $(this).val();
    });

    var CodigoPaquete = $("#dllPaquetesInvitadoIAgenda").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaquetesInvitadoIAgenda").data("kendoDropDownList").value();

    if ($('#txtNombreInvitado').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar nombre.", { type: 'danger', width: 'auto' });
    } else if ($('#txtApellidoInvitado').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar apellidos.", { type: 'danger', width: 'auto' });
    } else if ($('#txtCelularInvitado').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar celular.", { type: 'danger', width: 'auto' });
    } else if (CodigoSubProcedencia == '') {
        validator = false;
        $.bootstrapGrowl("Falta seleccionar sub procedencia.", { type: 'danger' });
    } else if (CodigoObjetivo == '') {
        validator = false;
        $.bootstrapGrowl("Falta seleccionar Objetivo.", { type: 'danger' });
    } else if (CodigoPaquete == 0) {
        validator = false;
        $.bootstrapGrowl("Falta seleccionar el plan.", { type: 'danger', width: 'auto' });
    } else if ($('#txtValidarClaveInvitado').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar la clave.", { type: 'danger', width: 'auto' });
    }

    return validator;
}

function uspValidarUsuarioIngresadoDeInvitado() {

    var Vendedor = $("#txtVendedorIAgenda_invitados").data("kendoDropDownList").value();
    var Clave = $('#txtValidarClaveProspecto_invitados').val();
    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionUsuario > 0) {
                GuardarTablaInvitados();
            } else {
                $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
            }

        }
    });
}

function GuardarTablaInvitados() {
    $('button[type="button"]').attr("disabled", true);

    var CodigoInvitado = $('#txtCodigo_SocioIAgenda').val() == '' ? 0 : $('#txtCodigo_SocioIAgenda').val();
    var Nombres = $('#txtNombreInvitado').val();
    var Apellidos = $('#txtApellidoInvitado').val();
    var DNI = $('#txtDniInvitado').val();
    var Telefono = $('#txtTelefonoInvitado').val();
    var Celular = $('#txtCelularInvitado').val();
    var Correo = $('#txtCorreoInvitado').val();
    var FechaNacimiento = kendo.toString($("#txtFechaNacimientoInvitado").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var Estado = true;
    var Genero = "";
    $('input[name="orderSexoInvitadoBox[]"]:checked').each(function () {
        Genero += $(this).val();
    });
    var Direccion = "";
    var InvitadoPor = $('#txtInvitadoPor').val();
    var Codigo_InvitadoPor = $('#txtCodigoInvitadoPor').val();
    var NroDias = $('#txtNrDias').val() == "" ? 0 : $('#txtNrDias').val();
    var NroDiasActual = $('#txtNrDiasActual').val() == "" ? 0 : $('#txtNrDiasActual').val();
    var FechaInicio = kendo.toString($("#txtFechaInicio").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFechaFinInvitado").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    var Accion = "N";
    var CodigoPaquete = 0;
    var Vendedor = $("#txtVendedorIAgenda_invitados").data("kendoDropDownList").value();

    var TipoClienteInvitadoAgenda = 1;

    var CodigoTiempo = $("#dllPaquetesInvitadoIAgenda").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaquetesInvitadoIAgenda").data("kendoDropDownList").value();
    var Precio = $("#txtPrecioInvitado").val() == "" ? 0 : $("#txtPrecioInvitado").val();

    var CodigoSubProcedencia = "";
    $('input[name="orderComoConocioGymBox_Prospeccion[]"]:checked').each(function () {
        CodigoSubProcedencia += $(this).val();
    });

    var CodigoObjetivo = "";
    $('input[name="orderObjetivoBox_Prospeccion[]"]:checked').each(function () {
        CodigoObjetivo += $(this).val();
    });

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"CodigoInvitado":"' + CodigoInvitado + '","Nombres":"' + Nombres + '","Apellidos":"' + Apellidos
            + '","DNI":"' + DNI + '","Telefono":"' + Telefono + '","Celular":"' + Celular + '","Correo":"' + Correo + '","FechaNacimiento":"' + FechaNacimiento
            + '","Estado":"' + Estado + '","Genero":"' + Genero + '","Direccion":"' + Direccion + '","InvitadoPor":"' + InvitadoPor + '","Codigo_InvitadoPor":"' + Codigo_InvitadoPor
            + '","NroDias":"' + NroDias + '","NroDiasActual":"' + NroDiasActual + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","Accion":"' + Accion
            + '","CodigoPaquete":"' + CodigoPaquete + '","Vendedor":"' + Vendedor + '","TipoClienteInvitadoAgenda":"' + TipoClienteInvitadoAgenda + '","CodigoTiempo":"' + CodigoTiempo + '","Precio":"' + Precio + '","CodigoSubProcedencia":"' + CodigoSubProcedencia + '","CodigoObjetivo":"' + CodigoObjetivo + '"}',
        type: "POST",
        url: "/gestionce/GuardarTablaInvitados",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg == 1) {
                $.bootstrapGrowl("Es oblogatorio ingresar el nro de documento DNI/CCI/CE.", { type: 'danger', width: 'auto' });
            } else if (msg == 2) {
                $.bootstrapGrowl("Los datos se han guardado correctamente.", { type: 'success', width: 'auto' });
                ListarTablaInvitados();
            } else if (msg == 100) {
                $.bootstrapGrowl("Error, no hemos podido guardar la información, intentelo de nuevo.", { type: 'danger', width: 'auto' });
            } else {
                ListarProspectosExistentesDNI(msg);
                //alert("El nro de documento ya existe");
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            //$('#txtValidarClaveInvitado').val('');
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function ListarTablaInvitados() {
    $('#hdCodigoOrigen_Prospecto').val('4'); //INVITADOS

    var Buscador = $('#txtBuscador_invitados').val();
    var User = $('#txtVendedorIAgenda_invitados').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioInvitados").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinInvitados").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';
    $("#gridTablaInvitados").empty();
    $("#gridTablaInvitados").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaInvitados_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            if (msg.length > 0) {
                                document.getElementById('btnGuardarInvitado').style.display = 'none';
                            } else {
                                LimpiarDatosInvitados();
                                document.getElementById('btnGuardarInvitado').style.display = 'none';

                            }
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                            uspListartablaInvitados_NumeroRegistros();
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 375,
        columns: [{
            field: "CodigoInvitado",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 13,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 8,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 12,
            attributes: {
                style: "font-size:12px;"
            }
        }
            //, {
            //template: "<div><button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarInvitadoaSocio(#: CodigoInvitado #)'>Enviar cliente</button></div>",
            //title: "",
            //width: 10
            //}
        ],
        dataBound: function (e) {
            var grid = $("#gridTablaInvitados").data("kendoGrid");
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        }
        ,
        change: function () {
            var text = "";
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoInvitado = dataItem.CodigoInvitado;

                document.getElementById('btnActualizarInvitado').style.display = '';
                //document.getElementById('btnEliminarInvitado').style.display = '';
                document.getElementById('btnGuardarInvitado').style.display = 'none';

                BuscarClientesDatosInvitadosPorCodigo(CodigoInvitado);
            });
        }
    });
}

function uspListartablaInvitados_NumeroRegistros() {
    var Buscador = $('#txtBuscador_invitados').val();
    var User = $('#txtVendedorIAgenda_invitados').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioInvitados").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinInvitados").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    $.ajax({
        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/uspListartablaInvitados_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarTablaInvitados').html(msg.CantTotal);
            ddlPaginacionuspListarTablaInvitados_Paginacion(msg.CantTotal, msg.TamanioPagina);

        }, complete: function () {

        }
    });
}

function ddlPaginacionuspListarTablaInvitados_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarTablaInvitados').html(htmlOpcion);
    $("#ddlPaginacionuspListarTablaInvitados").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarTablaInvitados").data("kendoDropDownList").value();
            ListarTablaInvitados_ChanguePage(nroPagina);
        }
    });
}

function ListarTablaInvitados_ChanguePage(PageNumber) {
    $('#hdCodigoOrigen_Prospecto').val('4'); //INVITADOS
    var Buscador = $('#txtBuscador_invitados').val();
    var User = $('#txtVendedorIAgenda_invitados').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioInvitados").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinInvitados").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';
    $("#gridTablaInvitados").empty();
    $("#gridTablaInvitados").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaInvitados_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            if (msg.length > 0) {
                                //document.getElementById('btnActualizarInvitado').style.display = '';
                                //document.getElementById('btnEliminarInvitado').style.display = '';
                                document.getElementById('btnGuardarInvitado').style.display = 'none';
                            } else {
                                LimpiarDatosInvitados();
                                document.getElementById('btnGuardarInvitado').style.display = 'none';
                            }
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 375,
        columns: [{
            field: "CodigoInvitado",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 13,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 8,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 12,
            attributes: {
                style: "font-size:12px;"
            }
        }
            //, {
            //template: "<div><button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarInvitadoaSocio(#: CodigoInvitado #)'>Enviar cliente</button></div>",
            //title: "",
            //width: 10
            //}
        ],
        dataBound: function (e) {
            var grid = $("#gridTablaInvitados").data("kendoGrid");
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        }
        ,
        change: function () {
            var text = "";
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoInvitado = dataItem.CodigoInvitado;

                document.getElementById('btnActualizarInvitado').style.display = '';
                //document.getElementById('btnEliminarInvitado').style.display = '';
                document.getElementById('btnGuardarInvitado').style.display = 'none';

                BuscarClientesDatosInvitadosPorCodigo(CodigoInvitado);
            });
        }
    });
}

function BuscarClientesDatosInvitadosPorCodigo(CodigoInvitado) {

    $.ajax({
        data: '{"CodigoInvitado":"' + CodigoInvitado + '"}',
        type: "POST",
        url: "/gestionce/uspBuscarClientesDatosInvitadosPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtCodigo_SocioIAgenda').val(msg.CodigoInvitado);
            $('#txtCodigo_BuscarIAgenda').val(msg.CodigoInvitado);
            $('#txtNombreInvitado').val(msg.Nombres);
            $('#txtTelefonoInvitado').val(msg.Telefono);
            $('#txtApellidoInvitado').val(msg.Apellidos);
            $('#txtCelularInvitado').val(msg.Celular);
            $('#txtFechaNacimientoInvitado').data('kendoDatePicker').value(msg.DesFechaNacimiento);
            $('#txtDniInvitado').val(msg.DNI);
            $('#txtCorreoInvitado').val(msg.Correo);

            $('#rbGeneroInvitado' + msg.Genero).prop('checked', msg.Genero);

            $('#txtNrDias').val(msg.NroDias);
            $('#txtNrDiasActual').val(msg.NroDiasActual);
            $('#txtFechaInicio').data('kendoDatePicker').value(msg.DescFechaInicio);
            $('#txtFechaFinInvitado').data('kendoDatePicker').value(msg.DescFechaFin);
            $('#ddlSocios_Invitado').val(msg.DesInvitadoPor);
            $('#txtCodigoInvitadoPor').val(msg.Codigo_InvitadoPor);

            $('#rbPotencialInvitado' + msg.TipoCliente).prop('checked', msg.TipoCliente);

            var na = msg.NroDias;
            var ca = msg.NroDiasActual;

            if (msg.Celular == '' && msg.Telefono == '') {
                //$('#lblVerificarTelefonoInvitado').html('* AdGym le recomienda pedir el número de teléfono o celular del invitado.');
                //$('#lblVerificarTelefonoInvitado').show('fast');
            }
            else {
                //$('#lblVerificarTelefonoInvitado').hide('fast');
            }
            $("#dllPaquetesInvitadoIAgenda").data("kendoDropDownList").value(msg.CodigoTiempo);
            $("#txtPrecioInvitado").val(msg.Precio);
            $('#rbSubProcedencia_prospeccion' + msg.CodigoSubProcedencia).prop('checked', true);
            $('#rbObjetivoProspeccion' + msg.CodigoObjetivo).prop('checked', true);
        }
    });
}

function evento_EnviarInvitadoaSocio(CodigoInvitado) {
    if (CodigoInvitado == 0) {
        $.bootstrapGrowl("Seleccione o busque un invitado.", { type: 'primary', width: 'auto' });
    } else {
        document.getElementById('myModalConfDeInvitadoASocio').style.display = 'block';
    }
}

//FIN INVITADOS

//INICIO REFERIDOS

function evento_EnviarReferidoaSocio(CodigoReferido) {
    if (CodigoReferido == 0) {
        $.bootstrapGrowl("Seleccione o busque un referido.", { type: 'primary', width: 'auto' });
    } else {
        document.getElementById('myModalConfDeReferidoASocio').style.display = 'block';
    }
}

function listardllPaqueteReferido() {
    var ddlPaquete = $("#dllPaqueteReferido").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccione",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="dllPaquetesInvitadoIAgenda_listbox"]').val();
                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        }

    }).data("kendoDropDownList");

}

function listaSociosReferidoPor() {

    $("#ddlSocios_Referidos").kendoAutoComplete({
        dataTextField: "NombreApellido",
        dataValueField: "CodigoSocio",
        template: '<table border="0" style="width:100%;font-size: 12px;">' +
            '<tr>' +
            '<td style="width:100%;>' +
            '<span class="k-state-default" >' +
            '<div style="font-weight: bold;font-size:11px;">' +
            '#:data.Nombres# #:data.Apellidos# - Su codigo: #:data.CodigoSocio#  Su DNI: #:data.DNI# ' +
            '</div>' +
            '</span>' +
            '</td>' +
            '</tr>' +
            '</table>',
        filter: "startswith",
        minLength: 3,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var valor = $('#ddlSocios_Referidos').data('kendoAutoComplete').value();
                    var flag = 1;
                    $.ajax({
                        type: "POST",
                        data: '{"valor":"' + valor + '","flag":"' + flag + '"}',
                        url: "/gestionce/ListaSocios",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });

                }
            }
        }, select: function (e) {
            var dataItem = this.dataItem(e.item.index());
            $('#txtReferidoPor').val(dataItem.NombreApellido + ' | Codigo: ' + dataItem.CodigoSocio);
            $('#txtCodigoReferidoPor').val(dataItem.CodigoSocio);
            return false;
        }
    });

}

function ListarTablaReferidos() {
    $('#hdCodigoOrigen_Prospecto').val('5'); //REFERIDOS
    var Buscador = $('#txtBuscador_referidos').val();
    var User = $('#txtVendedorIAgenda_referidos').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioReferidos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinReferidos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';
    $("#gridTablaReferidos").empty();
    $("#gridTablaReferidos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaReferidos_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            if (msg.length > 0) {
                                //document.getElementById('btnEliminarReferido').style.display = '';
                                //document.getElementById('btnActualizarReferido').style.display = '';
                                document.getElementById('btnGuardarReferido').style.display = 'none';
                            } else {
                                LimpiarDatosReferidos();
                                document.getElementById('btnGuardarReferido').style.display = 'none';

                            }
                        }, complete: function () {

                            document.getElementById('loadMe').style.display = 'none';
                            uspListarTablaReferidos_NumeroRegistros();
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 450,
        columns: [{
            field: "CodigoReferido",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 13,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 12,
            attributes: {
                style: "font-size:11px;"
            }
        }
            //, {
            //template: "<button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarReferidoaSocio(#: CodigoReferido #)'>Enviar cliente</button>",
            //title: "",
            //width: 12
            //}
        ],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        }, change: function () {
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoReferido = dataItem.CodigoReferido;

                //document.getElementById('btnActualizarReferido').style.display = '';
                //document.getElementById('btnEliminarReferido').style.display = '';
                document.getElementById('btnGuardarReferido').style.display = 'none';
                BuscarClientesDatosReferidosPorCodigo(CodigoReferido);
            });
        }
    });
}

function uspListarTablaReferidos_NumeroRegistros() {
    var Buscador = $('#txtBuscador_referidos').val();
    var User = $('#txtVendedorIAgenda_referidos').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioReferidos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinReferidos").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    $.ajax({
        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/uspListarTablaReferidos_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarTablaReferidos').html(msg.CantTotal);
            ddlPaginacionuspListarTablaReferidos_Paginacion(msg.CantTotal, msg.TamanioPagina);

        }, complete: function () {

        }
    });
}

function ddlPaginacionuspListarTablaReferidos_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarTablaReferidos').html(htmlOpcion);
    $("#ddlPaginacionuspListarTablaReferidos").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarTablaReferidos").data("kendoDropDownList").value();
            ListarTablaReferidos_ChanguePage(nroPagina);
        }
    });
}

function ListarTablaReferidos_ChanguePage(PageNumber) {
    $('#hdCodigoOrigen_Prospecto').val('5'); //REFERIDOS
    var Buscador = $('#txtBuscador_referidos').val();
    var User = $('#txtVendedorIAgenda_referidos').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioReferidos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinReferidos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';
    $("#gridTablaReferidos").empty();
    $("#gridTablaReferidos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaReferidos_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            if (msg.length > 0) {
                                //document.getElementById('btnEliminarReferido').style.display = '';
                                //document.getElementById('btnActualizarReferido').style.display = '';
                                document.getElementById('btnGuardarReferido').style.display = 'none';
                            } else {
                                LimpiarDatosReferidos();
                                document.getElementById('btnGuardarReferido').style.display = 'none';
                            }
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 450,
        columns: [{
            field: "CodigoReferido",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 13,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 12,
            attributes: {
                style: "font-size:11px;"
            }
        }
            //, {
            //template: "<button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarReferidoaSocio(#: CodigoReferido #)'>Enviar cliente</button>",
            //title: "",
            //width: 12
            //}
        ],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        }, change: function () {
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoReferido = dataItem.CodigoReferido;

                //document.getElementById('btnActualizarReferido').style.display = '';
                //document.getElementById('btnEliminarReferido').style.display = '';
                document.getElementById('btnGuardarReferido').style.display = 'none';
                BuscarClientesDatosReferidosPorCodigo(CodigoReferido);
            });
        }
    });
}

function BuscarClientesDatosReferidosPorCodigo(CodigoReferido) {

    $.ajax({
        data: '{"CodigoReferido":"' + CodigoReferido + '"}',
        type: "POST",
        url: "/gestionce/uspBuscarClientesDatosReferidosPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtCodigo_SocioIAgenda').val(msg.CodigoReferido);
            $('#txtCodigo_BuscarIAgenda').val(msg.CodigoReferido);
            $('#txtNombreReferido').val(msg.Nombres);
            $('#txtTelefonoReferido').val(msg.Telefono);
            $('#txtApellidosReferido').val(msg.Apellidos);
            $('#txtCelularReferido').val(msg.Celular);
            $('#txtFechaNacimientoReferido').data('kendoDatePicker').value(msg.DesFechaNacimiento);
            $('#txtDniReferido').val(msg.DNI);
            $('#txtEmailReferido').val(msg.Correo);
            $('#rbGeneroReferido' + msg.Genero).prop('checked', true);
            $('#ddlSocios_Referidos').val(msg.ReferidoPor);
            $('#txtCodigoReferidoPor').val(msg.CodigoReferidoPor);
            $("#dllPaqueteReferido").data("kendoDropDownList").value(msg.CodigoTiempo);

            $('#txtPrecioReferido').val(msg.Precio);
            $('#rbtSubProcedencia_Digital' + msg.CodigoSubProcedencia).prop('checked', true);
            $('#rbObjetivoDigital' + msg.CodigoObjetivo).prop('checked', true);
        }
    });
}

function LimpiarDatosReferidos() {

    $('#infoCelular_ProspectoReferidos').html('000000000');
    $('#imginfoCelular_ProspectoReferidos').attr('href', 'https://api.whatsapp.com/');

    //$('#lblNombreCompletoClientes').val("");

    $('#txtNombreReferido').val("");
    $('#txtTelefonoReferido').val("");
    $('#txtApellidosReferido').val("");
    $('#txtCelularReferido').val("");
    var todayDate = new Date();
    $('#txtFechaNacimientoReferido').data('kendoDatePicker').value(todayDate);
    $('#txtDniReferido').val("");
    $('#txtEmailReferido').val("");
    $('#ddlSocios_Referidos').val("");
    $('#txtReferidoPor').val("");
    $('#txtCodigoReferidoPor').val(0);
    $("#dllPaqueteReferido").data("kendoDropDownList").select(0);
    $('input[name="orderComoConocioGymBox_Digital[]"]').prop('checked', false);
    $('input[name="orderObjetivoBox_Digital[]"]').prop('checked', false);
    //document.getElementById('btnActualizarReferido').style.display = 'none';
    //document.getElementById('btnEliminarReferido').style.display = 'none';
    $('#txtPrecioReferido').val("");
    $('#txtCodigo_SocioIAgenda').val('0');
    $('#txtNombre_SocioIAgenda').focus();
}

function validacionNuevoReferido() {
    var validator = true;
    var CodigoPaquete = $("#dllPaqueteReferido").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaqueteReferido").data("kendoDropDownList").value();

    if ($('#txtNombreReferido').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar el nombre.", { type: 'danger', width: 'auto' });
    } else if ($('#txtApellidosReferido').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar el apellido.", { type: 'danger', width: 'auto' });
    } else if ($('#txtCelularReferido').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar el celular.", { type: 'danger', width: 'auto' });
    } else if (CodigoPaquete == 0) {
        validator = false;
        $.bootstrapGrowl("Falta seleccionar el plan.", { type: 'danger', width: 'auto' });
    } else if (!$('input[name = "orderComoConocioGymBox_Digital[]"]').is(':checked')) {
        validator = false;
        $.bootstrapGrowl("seleccione sub procedencia.", { type: 'danger', width: 'auto' });
    } else if (!$('input[name = "orderObjetivoBox_Digital[]"]').is(':checked')) {
        validator = false;
        $.bootstrapGrowl("seleccione objetivo.", { type: 'danger', width: 'auto' });
    } else if ($('#txtValidarClaveReferido').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar la clave del vendedor.", { type: 'danger', width: 'auto' });
    }

    return validator;
}

function uspValidarUsuarioIngresadoDeReferido() {
    var Vendedor = $("#txtVendedorIAgenda_referidos").data("kendoDropDownList").value();
    var Clave = $('#txtValidarClaveReferido').val();
    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionUsuario > 0) {
                GuardarTablaReferido();
            } else {
                $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
            }

        }
    });
}

function GuardarTablaReferido() {
    $('button[type="button"]').attr("disabled", true);

    var CodigoReferido = $('#txtCodigo_SocioIAgenda').val() == '' ? 0 : $('#txtCodigo_SocioIAgenda').val();
    var Nombres = $('#txtNombreReferido').val();
    var Apellidos = $('#txtApellidosReferido').val();
    var DNI = $('#txtDniReferido').val();
    var Telefono = $('#txtTelefonoReferido').val();
    var Celular = $('#txtCelularReferido').val();
    var Correo = $('#txtEmailReferido').val();
    var FechaNacimiento = kendo.toString($("#txtFechaNacimientoReferido").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var Estado = true;
    var Genero = "";
    $('input[name="orderSexoReferidoBox[]"]:checked').each(function () {
        Genero += $(this).val();
    });
    var Direccion = "";
    var ReferidoPor = $('#txtReferidoPor').val();
    var CodigoReferidoPor = $('#txtCodigoReferidoPor').val();
    var Accion = "N";
    var CodigoPaquete = 0;
    var Vendedor = $("#txtVendedorIAgenda_referidos").data("kendoDropDownList").value();
    var Hijos = 0;
    var CantHijos = 0;
    var TipoClienteRefidoAgenda = 1;

    var CodigoTiempo = $("#dllPaqueteReferido").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaqueteReferido").data("kendoDropDownList").value();
    var Precio = $("#txtPrecioReferido").val() == "" ? 0 : $("#txtPrecioReferido").val();
    var CodigoSubProcedencia = "";
    $('input[name="orderComoConocioGymBox_Digital[]"]:checked').each(function () {
        CodigoSubProcedencia += $(this).val();
    });

    var CodigoObjetivo = "";
    $('input[name="orderObjetivoBox_Digital[]"]:checked').each(function () {
        CodigoObjetivo += $(this).val();
    });

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"CodigoReferido":"' + CodigoReferido + '","Nombres":"' + Nombres + '","Apellidos":"' + Apellidos
            + '","DNI":"' + DNI + '","Telefono":"' + Telefono + '","Celular":"' + Celular + '","Correo":"' + Correo
            + '","FechaNacimiento":"' + FechaNacimiento + '","Estado":"' + Estado + '","Genero":"' + Genero + '","Direccion":"' + Direccion
            + '","ReferidoPor":"' + ReferidoPor + '","CodigoReferidoPor":"' + CodigoReferidoPor + '","Accion":"' + Accion
            + '","CodigoPaquete":"' + CodigoPaquete + '","Vendedor":"' + Vendedor + '","Hijos":"' + Hijos + '","CantHijos":"' + CantHijos
            + '","TipoClienteRefidoAgenda":"' + TipoClienteRefidoAgenda + '","CodigoTiempo":"' + CodigoTiempo + '","Precio":"' + Precio + '","CodigoSubProcedencia":"' + CodigoSubProcedencia + '","CodigoObjetivo":"' + CodigoObjetivo + '"}',
        type: "POST",
        url: "/gestionce/GuardarTablaReferidos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg == 1) {
                $.bootstrapGrowl("Es oblogatorio ingresar el nro de documento DNI/CCI/CE.", { type: 'danger', width: 'auto' });
            } else if (msg == 2) {
                $.bootstrapGrowl("Los datos se han guardado correctamente.", { type: 'success', width: 'auto' });
                ListarTablaReferidos();
            } else if (msg == 100) {
                $.bootstrapGrowl("Error, no hemos podido guardar la información, intentelo de nuevo.", { type: 'danger', width: 'auto' });
            } else {
                ListarProspectosExistentesDNI(msg);
                //alert("El nro de documento ya existe");
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            //$('#txtValidarClaveReferido').val('');
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

//FIN REFERIDOS
//INICIO LLAMADA ENTRANTE

function listardllPaqueteLlamadaE() {
    var dllPaqueteLlamadaE = $("#dllPaqueteLlamadaE").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccione",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var flag = 1;
                    var nombre = $('input[aria-owns="dllPaqueteLlamadaE_listbox"]').val();
                    $.ajax({
                        data: '{"flag":"' + flag + '","nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        }

    }).data("kendoDropDownList");

}

function validacionNuevaLLamadaE() {
    var validator = true;

    var CodigoPaquete = $("#dllPaqueteLlamadaE").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaqueteLlamadaE").data("kendoDropDownList").value();
    if ($('#txtNombreLlamadaE').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar nombre.", { type: 'danger', width: 'auto' });
    } else if ($('#txtApellidosLlamadaE').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar apellido.", { type: 'danger', width: 'auto' });
    } else if ($('#txtCelularLlamadaE').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar celular.", { type: 'danger', width: 'auto' });
    } else if (CodigoPaquete == 0) {
        validator = false;
        $.bootstrapGrowl("Falta seleccionar el plan.", { type: 'danger', width: 'auto' });
    } else if ($('#txtValidarClaveLlamadaE').val() == '') {
        validator = false;
        $.bootstrapGrowl("Ingresar la clave del vendedor.", { type: 'danger', width: 'auto' });
    } else if (!$('input[name = "orderObjetivoBox_LlamadaE[]"]').is(':checked')) {
        validator = false;
        $.bootstrapGrowl("seleccione objetivo.", { type: 'danger', width: 'auto' });
    }
    return validator;
}

function uspValidarUsuarioIngresadoDeLlamadaE() {
    var Vendedor = $("#txtVendedorIAgenda_llamadaentrante").data("kendoDropDownList").value();
    var Clave = $('#txtValidarClaveProspecto_llamadaentrante').val();

    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionUsuario > 0) {
                GuardarTablaLlamadaEntrante();
            } else {
                $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
            }

        }
    });
}

function GuardarTablaLlamadaEntrante() {

    $('button[type="button"]').attr("disabled", true);
    var CodigoLlamadaE = $('#txtCodigo_SocioIAgenda').val() == '' ? 0 : $('#txtCodigo_SocioIAgenda').val();
    var Nombres = $('#txtNombreLlamadaE').val();
    var Apellidos = $('#txtApellidosLlamadaE').val();
    var DNI = $('#txtDniLlamadaE').val();
    var Telefono = $('#txtTelefonoLlamadaE').val();
    var Celular = $('#txtCelularLlamadaE').val();
    var Correo = $('#txtEmailLlamadaE').val();
    var FechaNacimiento = kendo.toString($("#txtFechaNacimientoLlamadaE").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var Estado = true;
    var Genero = "";
    $('input[name="orderSexoLlamadaEBox[]"]:checked').each(function () {
        Genero += $(this).val();
    });
    var Direccion = "";
    var Accion = "N";
    var CodigoPaquete = 0;
    var Vendedor = $("#txtVendedorIAgenda_llamadaentrante").data("kendoDropDownList").value();
    var Hijos = 0;
    var CantHijos = 0;
    var TipoClienteLlamadaEAgenda = "";
    $('input[name="orderPotencialLlamadaEBox[]"]:checked').each(function () {
        TipoClienteLlamadaEAgenda += $(this).val();
    });
    var CodigoTiempo = $("#dllPaqueteLlamadaE").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaqueteLlamadaE").data("kendoDropDownList").value();
    var Precio = $("#txtPrecioLlamadaE").val() == "" ? 0 : $("#txtPrecioLlamadaE").val();
    var CodigoObjetivo = "";
    $('input[name="orderObjetivoBox_LlamadaE[]"]:checked').each(function () {
        CodigoObjetivo += $(this).val();
    });

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"CodigoLlamadaE":"' + CodigoLlamadaE + '","Nombres":"' + Nombres + '","Apellidos":"' + Apellidos + '","DNI":"' + DNI + '","Telefono":"' + Telefono
            + '","Celular":"' + Celular + '","Correo":"' + Correo + '","FechaNacimiento":"' + FechaNacimiento + '","Estado":"' + Estado + '","Genero":"' + Genero
            + '","Direccion":"' + Direccion + '","Accion":"' + Accion + '","CodigoPaquete":"' + CodigoPaquete + '","Vendedor":"' + Vendedor + '","Hijos":"' + Hijos
            + '","CantHijos":"' + CantHijos + '","TipoClienteLlamadaEAgenda":"' + TipoClienteLlamadaEAgenda + '","CodigoTiempo":"' + CodigoTiempo + '","Precio":"' + Precio + '","CodigoObjetivo":"' + CodigoObjetivo + '"}',
        type: "POST",
        url: "/gestionce/GuardarTablaLlamadaEntrante",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg == 1) {
                $.bootstrapGrowl("Es oblogatorio ingresar el nro de documento DNI/CCI/CE.", { type: 'danger', width: 'auto' });
            } else if (msg == 2) {
                $.bootstrapGrowl("Los datos se han guardado correctamente.", { type: 'success', width: 'auto' });
                ListarTablaLlamadaEntrante();
            } else if (msg == 100) {
                $.bootstrapGrowl("Error, no hemos podido guardar la información, intentelo de nuevo.", { type: 'danger', width: 'auto' });
            } else {
                ListarProspectosExistentesDNI(msg);
                //alert("El nro de documento ya existe");
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            //$('#txtValidarClaveLlamadaE').val('');
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function ListarTablaLlamadaEntrante() {
    $('#hdCodigoOrigen_Prospecto').val('6'); //LLAMADA ENTRANTE
    var Buscador = $('#txtBuscador_llamadaentrante').val();
    var User = $('#txtVendedorIAgenda_llamadaentrante').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioLlamadaE").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinLlamadaE").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';

    $("#gridTablaLlamadaE").empty();
    $("#gridTablaLlamadaE").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaLlamadaEntrante_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            if (msg.length > 0) {
                                //document.getElementById('btnEliminarLlamadaE').style.display = '';
                                //document.getElementById('btnActualizarLlamadaE').style.display = '';
                                document.getElementById('btnGuardarLlamadaE').style.display = 'none';
                            } else {
                                LimpiarDatosLlamadaE();
                                document.getElementById('btnGuardarLlamadaE').style.display = 'none';
                            }
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                            uspListarTablaLlamadaEntrante_NumeroRegistros();
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 450,
        columns: [{
            field: "CodigoLlamadaE",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 15,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 12,
            attributes: {
                style: "font-size:11px;"
            }
        }
            //, {
            //template: "<button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarLlamadaEntranteaCliente(#: CodigoLlamadaE #)'>Enviar cliente</button>",
            //title: "",
            //width: 13
            //}
        ],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoLlamadaE = dataItem.CodigoLlamadaE;

                //document.getElementById('btnActualizarLlamadaE').style.display = '';
                //document.getElementById('btnEliminarLlamadaE').style.display = '';
                document.getElementById('btnGuardarLlamadaE').style.display = 'none';
                BuscarClientesDatosLLamadaEPorCodigo(CodigoLlamadaE);
            });
        }
    });
}

function uspListarTablaLlamadaEntrante_NumeroRegistros() {

    $('#hdCodigoOrigen_Prospecto').val('6'); //LLAMADA ENTRANTE
    var Buscador = $('#txtBuscador_llamadaentrante').val();
    var User = $('#txtVendedorIAgenda_llamadaentrante').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioLlamadaE").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinLlamadaE").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    $.ajax({
        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/uspListarTablaLlamadaEntrante_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarTablaLlamadasE').html(msg.CantTotal);
            ddlPaginacionuspListarTablaLlamadaE_Paginacion(msg.CantTotal, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function ddlPaginacionuspListarTablaLlamadaE_Paginacion(CantidadTotalFilas, TamanioPagina) {

    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }
    $('#ddlPaginacionuspListarTablaLlamadaE').html(htmlOpcion);
    $("#ddlPaginacionuspListarTablaLlamadaE").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarTablaLlamadaE").data("kendoDropDownList").value();
            ListarTablaLlamadaEntrante_ChanguePage(nroPagina);
        }
    });
}

function ListarTablaLlamadaEntrante_ChanguePage(PageNumber) {
    $('#hdCodigoOrigen_Prospecto').val('6'); //LLAMADA ENTRANTE
    var Buscador = $('#txtBuscador_llamadaentrante').val();
    var User = $('#txtVendedorIAgenda_llamadaentrante').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioLlamadaE").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinLlamadaE").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';

    $("#gridTablaLlamadaE").empty();
    $("#gridTablaLlamadaE").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaLlamadaEntrante_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            if (msg.length > 0) {
                                //document.getElementById('btnEliminarLlamadaE').style.display = '';
                                //document.getElementById('btnActualizarLlamadaE').style.display = '';
                                document.getElementById('btnGuardarLlamadaE').style.display = 'none';

                            } else {
                                LimpiarDatosLlamadaE();
                                document.getElementById('btnGuardarLlamadaE').style.display = 'none';

                            }
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 450,
        columns: [{
            field: "CodigoLlamadaE",
            title: "<b style='color:#fff;font-weight:bold'>Codigo</b>",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "NombreCompleto",
            title: "<b style='color:#fff;font-weight:bold'>Nombre completo</b>",
            width: 15,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:23px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<b style='color:#fff;font-weight:bold'>Celular</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Vendedor</b>",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<b style='color:#fff;font-weight:bold'>Creada</b>",
            width: 12,
            attributes: {
                style: "font-size:11px;"
            }
        }
            //, {
            //template: "<button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarLlamadaEntranteaCliente(#: CodigoLlamadaE #)'>Enviar cliente</button>",
            //title: "",
            //width: 13
            //}
        ],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {

            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoLlamadaE = dataItem.CodigoLlamadaE;

                //document.getElementById('btnActualizarLlamadaE').style.display = '';
                //document.getElementById('btnEliminarLlamadaE').style.display = '';
                document.getElementById('btnGuardarLlamadaE').style.display = 'none';
                BuscarClientesDatosLLamadaEPorCodigo(CodigoLlamadaE);
            });
        }
    });
}

function BuscarClientesDatosLLamadaEPorCodigo(CodigoLlamadaE) {

    $.ajax({
        data: '{"CodigoLlamadaE":"' + CodigoLlamadaE + '"}',
        type: "POST",
        url: "/gestionce/BuscarClientesDatosLLamadaEPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtCodigo_SocioIAgenda').val(msg.CodigoLlamadaE);
            $('#txtCodigo_BuscarIAgenda').val(msg.CodigoLlamadaE);
            $('#txtNombreLlamadaE').val(msg.Nombres);
            $('#txtTelefonoLlamadaE').val(msg.Telefono);
            $('#txtApellidosLlamadaE').val(msg.Apellidos);
            $('#txtCelularLlamadaE').val(msg.Celular);
            $('#txtFechaNacimientoLlamadaE').data('kendoDatePicker').value(msg.DesFechaNacimiento);
            $('#txtDniLlamadaE').val(msg.DNI);
            $('#txtEmailLlamadaE').val(msg.Correo);
            $('#rbGeneroLlamadaE' + msg.Genero).prop('checked', msg.Genero);
            $("#dllPaqueteLlamadaE").data("kendoDropDownList").value(msg.CodigoTiempo);
            $('#rbPotencialLlamadaE' + msg.TipoCliente).prop('checked', msg.TipoCliente);
            //$('#lblNombreCompletoClientes').val(' su codigo es : ' + msg.CodigoLlamadaE);
            $('#txtPrecioLlamadaE').val(msg.Precio);
            $('#rbObjetivoLlamadaE' + msg.CodigoObjetivo).prop('checked', true);
        }
    });
}

function LimpiarDatosLlamadaE() {
    $('#infoCelular_ProspectoLlamadaEntrante').html('000000000');
    $('#imginfoCelular_ProspectoLlamadaEntrante').attr('href', 'https://api.whatsapp.com/');

    //$('#lblNombreCompletoClientes').val("");
    $('#txtCodigo_SocioIAgenda').val('0');
    $('#txtNombre_SocioIAgenda').focus();
    $('#txtNombreLlamadaE').val("");
    $('#txtTelefonoLlamadaE').val("");
    $('#txtApellidosLlamadaE').val("");
    $('#txtCelularLlamadaE').val("");
    var todayDate = new Date();
    $('#txtFechaNacimientoLlamadaE').data('kendoDatePicker').value(todayDate);
    $('#txtDniLlamadaE').val("");
    $('#txtEmailLlamadaE').val("");
    $("#dllPaqueteLlamadaE").data("kendoDropDownList").select(0);
    //document.getElementById('btnActualizarLlamadaE').style.display = 'none';
    //document.getElementById('btnEliminarLlamadaE').style.display = 'none';
    $('#txtPrecioLlamadaE').val("");
    $('input[name="orderObjetivoBox_LlamadaE[]"]').prop('checked', false);
}

function evento_EnviarLlamadaEntranteaCliente(CodigoLlamadaE) {
    if (CodigoLlamadaE == 0) {
        $.bootstrapGrowl("Seleccione o busque un prospecto.", { type: 'primary', width: 'auto' });
    } else {
        document.getElementById('myModalConvDeLlamadaASocio').style.display = 'block';

        $('#DivCerrarmyModalConvDeLlamadaASocio,#btnNoLlamadaPasarASocio').click(function () {
            document.getElementById('myModalConvDeLlamadaASocio').style.display = 'none';
        });
    }
}

//FIN LLAMADA ENTRANTE


//INICIO WEB


function listardllPaqueteWeb() {
    var dllPaqueteWeb = $("#dllPaqueteWeb").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccione",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="dllPaqueteWeb_listbox"]').val();
                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        }

    }).data("kendoDropDownList");
}

function LimpiarDatosWeb() {
    $('#infoCelular_ProspectoWeb').html('000000000');
    $('#imginfoCelular_ProspectoWeb').attr('href', 'https://api.whatsapp.com/');

    //$('#lblNombreCompletoClientes').val("");
    $('#txtCodigo_SocioIAgenda').val(0);
    $('#txtNombreWeb').val("");
    $('#txtTelefonoWeb').val("");
    $('#txtApellidosWeb	').val("");
    $('#txtCelularWeb').val("");
    $('#txtEmailWeb').val("");
    $("#dllPaqueteWeb").data("kendoDropDownList").select(0);
    $('#txtPrecioWeb').val("");
    document.getElementById('btnGuardarWeb').style.display = '';
    document.getElementById('btnEliminarWeb').style.display = 'none';
}

function validacionNuevaWeb() {
    var validator = true;
    if ($('#txtNombreWeb').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar nombre.", { type: 'danger', width: 'auto' });
    }

    if ($('#txtApellidosWeb	').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar apellido.", { type: 'danger', width: 'auto' });
    }

    if ($('#txtCelularWeb').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar celular.", { type: 'danger', width: 'auto' });
    }

    var CodigoPaquete = $("#dllPaqueteWeb").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaqueteWeb").data("kendoDropDownList").value();
    if (CodigoPaquete == 0) {
        validator = false;
        $.bootstrapGrowl("Falta seleccionar plan.", { type: 'danger', width: 'auto' });
    }

    if ($('#txtValidarClaveWeb').val() == '') {
        validator = false;
        $.bootstrapGrowl("Falta ingresar la clave del vendedor.", { type: 'danger', width: 'auto' });
    }
    return validator;
}

function uspValidarUsuarioIngresadoDeWeb() {
    var Vendedor = $("#txtVendedorIAgenda_web").data("kendoDropDownList").value();
    var Clave = $('#txtValidarClaveProspecto_web').val();

    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionUsuario > 0) {
                GuardarTablaWeb();
            } else {
                $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'primary', width: 'auto' });
            }

        }
    });
}

function GuardarTablaWeb() {
    $('button[type="button"]').attr("disabled", true);

    var Nombres = $('#txtNombreWeb').val();
    var Apellidos = $('#txtApellidosWeb	').val();
    var Telefono = $('#txtTelefonoWeb').val();
    var Celular = $('#txtCelularWeb').val();
    var Correo = $('#txtEmailWeb').val();
    var Genero = "";
    $('input[name="orderSexoWebBox[]"]:checked').each(function () {
        Genero += $(this).val();
    });
    var Vendedor = $("#txtVendedorIAgenda").data("kendoDropDownList").value();
    var TipoClienteWebAgenda = "";
    $('input[name="orderPotencialWebBox[]"]:checked').each(function () {
        TipoClienteWebAgenda += $(this).val();
    });
    var CodigoTiempo = $("#dllPaqueteWeb").data("kendoDropDownList").value() == "" ? 0 : $("#dllPaqueteWeb").data("kendoDropDownList").value();
    var Precio = $("#txtPrecioWeb").val() == "" ? 0 : $("#txtPrecioWeb").val();
    var Estado = true;
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"Nombres":"' + Nombres + '","Apellidos":"' + Apellidos + '","Telefono":"' + Telefono + '","Celular":"' + Celular + '","Correo":"' + Correo
            + '","Genero":"' + Genero + '","Vendedor":"' + Vendedor + '","TipoClienteWebAgenda":"' + TipoClienteWebAgenda + '","CodigoTiempo":"' + CodigoTiempo + '","Precio":"' + Precio
            + '","Estado":"' + Estado + '"}',
        type: "POST",
        url: "/gestionce/uspRegistrarProspectoWeb",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han guardado correctamente.", { type: 'success', width: 'auto' });
                uspListarTablaWeb_Paginacion();
            } else {
                $.bootstrapGrowl("Error, no hemos podido guardar su información.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            $('#txtValidarClaveWeb').val('');
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function uspListarTablaWeb_Paginacion() {
    var Buscador = $('#txtBuscador_web').val();
    var User = $('#txtVendedorIAgenda_web').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioWeb").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinWeb").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    $("#gridTablaWeb").empty();
    $("#gridTablaWeb").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaWeb_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            if (msg.length > 0) {
                                document.getElementById('btnEliminarWeb').style.display = '';
                                document.getElementById('btnGuardarWeb').style.display = 'none';
                            } else {
                                LimpiarDatosWeb();
                                document.getElementById('btnGuardarWeb').style.display = 'none';
                            }
                        }, complete: function () {
                            uspListarTablaWeb_NumeroRegistros();
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 450,
        columns: [{
            field: "Nombres",
            title: "Nombres",
            width: 10,
            attributes: {
                style: "font-size:10px;"
            }
        }, {
            field: "Apellidos",
            title: "Apellidos",
            width: 15,
            attributes: {
                style: "font-size:10px;"
            }
        }, {
            template: "<button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarProspectoWebaCliente(#: CodigoLlamadaE #)'>Enviar cliente</button>",
            title: "",
            width: 16
        }],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoLlamadaE = dataItem.CodigoLlamadaE;

                $('#infoCelular_ProspectoWeb').html(dataItem.Celular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_ProspectoWeb').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_ProspectoWeb').attr('href', 'https://api.whatsapp.com/');
                }

                //document.getElementById('btnActualizarLlamadaE').style.display = '';
                document.getElementById('btnEliminarWeb').style.display = '';
                document.getElementById('btnGuardarWeb').style.display = 'none';
                uspBuscarPropectoWebPorCodigo(CodigoLlamadaE);
            });
        }
    });
}

function uspListarTablaWeb_NumeroRegistros() {
    var Buscador = $('#txtBuscador_web').val();
    var User = $('#txtVendedorIAgenda_web').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioWeb").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinWeb").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    $.ajax({
        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/uspListarTablaWeb_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarTablaWeb').html(msg.CantTotal);
            ddlpaginacionuspListarTablaWeb_Paginacion(msg.CantTotal, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function ddlpaginacionuspListarTablaWeb_Paginacion(cantidadtotalfilas, tamaniopagina) {

    var cantidadpaginas = parseInt(cantidadtotalfilas / tamaniopagina) + 1;
    var htmlopcion = "";
    for (var i = 1; i <= cantidadpaginas; i++) {
        htmlopcion += "<option value='" + i + "'> " + i + " </option>";
    }
    $('#ddlPaginacionuspListarTablaWeb').html(htmlopcion);
    $("#ddlPaginacionuspListarTablaWeb").kendoDropDownList({
        change: function () {
            var nropagina = $("#ddlPaginacionuspListarTablaWeb").data("kendoDropDownList").value();
            uspListarTablaWeb_ChanguePage(nropagina);
        }
    });
}

function uspListarTablaWeb_ChanguePage(PageNumber) {
    var Buscador = $('#txtBuscador_web').val();
    var User = $('#txtVendedorIAgenda_web').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicioWeb").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFinWeb").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    $("#gridTablaWeb").empty();
    $("#gridTablaWeb").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Buscador":"' + Buscador + '","User":"' + User + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarTablaWeb_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            if (msg.length > 0) {
                                document.getElementById('btnEliminarWeb').style.display = '';
                                document.getElementById('btnGuardarWeb').style.display = 'none';
                            } else {
                                LimpiarDatosWeb();
                                document.getElementById('btnGuardarWeb').style.display = 'none';
                            }
                        }, complete: function () {
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 450,
        columns: [{
            field: "Nombres",
            title: "Nombres",
            width: 10,
            attributes: {
                style: "font-size:10px;"
            }
        }, {
            field: "Apellidos",
            title: "Apellidos",
            width: 15,
            attributes: {
                style: "font-size:10px;"
            }
        }, {
            template: "<button type='button' class='btn btn-sm btn-light' onclick='evento_EnviarProspectoWebaCliente(#: CodigoLlamadaE #)'>Enviar cliente</button>",
            title: "",
            width: 16
        }],
        dataBound: function (e) {
            var grid = $("#gridTablaLlamadaE").data("kendoGrid");
            this.element.find('tbody tr:first').addClass('k-state-selected')
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var text = "";
            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var CodigoLlamadaE = dataItem.CodigoLlamadaE;

                $('#infoCelular_ProspectoWeb').html(dataItem.Celular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_ProspectoWeb').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_ProspectoWeb').attr('href', 'https://api.whatsapp.com/');
                }

                document.getElementById('btnEliminarWeb').style.display = '';
                document.getElementById('btnGuardarWeb').style.display = 'none';
                uspBuscarPropectoWebPorCodigo(CodigoLlamadaE);
            });
        }
    });
}

function uspBuscarPropectoWebPorCodigo(CodigoLlamadaE) {

    $.ajax({
        data: '{"CodigoLlamadaE":"' + CodigoLlamadaE + '"}',
        type: "POST",
        url: "/gestionce/uspBuscarPropectoWebPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtCodigo_SocioIAgenda').val(msg.CodigoLlamadaE);
            $('#txtCodigo_BuscarIAgenda').val(msg.CodigoLlamadaE);
            $('#txtNombreWeb').val(msg.Nombres);
            $('#txtApellidosWeb	').val(msg.Telefono);
            $('#txtTelefonoWeb').val(msg.Apellidos);
            $('#txtCelularWeb').val(msg.Celular);
            $('#txtEmailWeb').val(msg.Correo);
            $('#rbGeneroWeb' + msg.Genero).prop('checked', msg.Genero);
            $("#dllPaqueteWeb").data("kendoDropDownList").value(msg.CodigoTiempo);
            $('#rbPotencialWeb' + msg.TipoCliente).prop('checked', msg.TipoCliente);
            // $('#lblNombreCompletoClientes').val(' su codigo es : ' + msg.CodigoLlamadaE);
            $('#txtPrecioWeb').val(msg.Precio);
        }
    });
}


//FIN WEB




function listaVendedoresIAgendaCaida() {

    $("#dllVendedoresAgendaCaida").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarAsesoresComerciales",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            var User = getCookie("_Usuario_Business");
                            $('#dllVendedoresAgendaCaida').data("kendoDropDownList").value(User);
                        }
                    });
                }
            }
        }
    });
}

function listadllVendedoresReagendarAgenda() {

    $("#dllVendedorReagendarAgenda").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarAsesoresComerciales",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            var User = getCookie("_Usuario_Business");
                            $('#dllVendedorReagendarAgenda').data("kendoDropDownList").value(User.toString().trim());
                        }
                    });
                }
            }
        }
    });

}

function listadllVendedoresReagendarAgenda_Oportunidades() {

    $("#dllVendedorReagendarAgenda_Oportunidades").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarAsesoresComerciales",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            var User = getCookie("_Usuario_Business");
                            $('#dllVendedorReagendarAgenda_Oportunidades').data("kendoDropDownList").value(User);
                        }
                    });
                }
            }
        }
    });

}

function GuardarAgendaSeguimientoReagendarTodos() {
    $('button[type="button"]').attr("disabled", true);

    var CodigoTipoAgenda = $('#hdCodigoTipoAgenda').val();
    var CodigoSocio = $('#hdCodigoSocio').val();
    var CodigoAgenda = '0';
    var DescTipo = 0;
    var color = "";
    var asunto = $('#txtAsuntoReagendar').val();
    var diaG = kendo.toString($("#txtFechaReagendarAgenda").data('kendoDatePicker').value(), 'dd');
    var MesG = kendo.toString($("#txtFechaReagendarAgenda").data('kendoDatePicker').value(), 'MM');
    var AnioG = kendo.toString($("#txtFechaReagendarAgenda").data('kendoDatePicker').value(), 'yyyy');
    var fecha = AnioG + "|" + MesG + "|" + diaG + "|";
    var hi = $('#hdHoraReagendarAgenda').val() == '' ? '00|00|00' : $('#hdHoraReagendarAgenda').val();
    var HoraInicio = fecha + hi;
    var Estado = 1;
    var User = $("#dllVendedorReagendarAgenda").data("kendoDropDownList").value();
    var CodigoPaquete = 0;
    var TipoActividad = $('#ddlTipoActividad_ActividadesAgendar').data('kendoDropDownList').value();

    $.ajax({
        data: '{"Codigo":"' + CodigoAgenda + '","CodigoSocio":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","DescTipo":"' + DescTipo + '","Asunto":"' + asunto + '","HoraInicio":"' + HoraInicio + '","Color":"' + color + '","User":"' + User + '","Estado":"' + Estado + '","CodigoPaquete":"' + CodigoPaquete + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/GuardarAgendaSeguimientoTodos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han agregado correctamente.", { type: 'success', width: 'auto' });
                $('#txtAsuntoReagendar').val('');
                $('#txtValidarClaveReagendar').val('');
                $('#modalReagendarAgenda').hide('fast');

            } else {
                $.bootstrapGrowl("Tenemos problemas para guardar esta actividad.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            ListarGridAgendaGeneral();
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function GuardarAgendaSeguimientoAgendarInactivos() {
    $('button[type="button"]').attr("disabled", true);
    var CodigoTipoAgenda = 3; //reinscripcion
    var CodigoSocio = $('#hdCodigoSocio').val();
    var CodigoAgenda = '0';
    var DescTipo = 0;
    var color = "";
    var asunto = $('#txtAsuntoAgendarInactivo').val();

    var diaG = kendo.toString($("#txtFechaAgendaInactivo").data('kendoDatePicker').value(), 'dd');
    var MesG = kendo.toString($("#txtFechaAgendaInactivo").data('kendoDatePicker').value(), 'MM');
    var AnioG = kendo.toString($("#txtFechaAgendaInactivo").data('kendoDatePicker').value(), 'yyyy');

    var fecha = AnioG + "|" + MesG + "|" + diaG + "|";
    var hi = $('#hdHoraAgendaInactivos').val() == '' ? '00|00|00' : $('#hdHoraAgendaInactivos').val();
    var HoraInicio = fecha + hi;
    var Estado = 1;

    var User = $("#dllVendedorAgendaInactivo").data("kendoDropDownList").value();
    var CodigoPaquete = 0;
    var TipoActividad = $('#ddlTipoActividad_Vencidos').data('kendoDropDownList').value();

    $.ajax({
        data: '{"Codigo":"' + CodigoAgenda + '","CodigoSocio":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","DescTipo":"' + DescTipo + '","Asunto":"' + asunto + '","HoraInicio":"' + HoraInicio + '","Color":"' + color + '","User":"' + User + '","Estado":"' + Estado + '","CodigoPaquete":"' + CodigoPaquete + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/GuardarAgendaSeguimientoTodos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han agregado correctamente.", { type: 'success', width: 'auto' });
                $('#txtAsuntoAgendarInactivo').val('');
                $('#txtValidarClaveAgendarInactivo').val('');
                $('#modalAgendaInactivo').hide('fast');
                uspListarClientesInactivos();
            } else {
                $.bootstrapGrowl("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function GuardarAgendaSeguimientoAgendarRenovaciones() {
    $('button[type="button"]').attr("disabled", true);

    var CodigoTipoAgenda = 2; //renovaciones
    var CodigoSocio = $('#hdCodigoSocio').val();
    var CodigoAgenda = '0';
    var DescTipo = 0;
    var color = "";
    var asunto = $('#txtAsuntoAgendarRenovaciones').val();
    var diaG = kendo.toString($("#txtFechaAgendaRenovaciones").data('kendoDatePicker').value(), 'dd');
    var MesG = kendo.toString($("#txtFechaAgendaRenovaciones").data('kendoDatePicker').value(), 'MM');
    var AnioG = kendo.toString($("#txtFechaAgendaRenovaciones").data('kendoDatePicker').value(), 'yyyy');
    var fecha = AnioG + "|" + MesG + "|" + diaG + "|";
    var hi = $('#hdHoraAgendaRenovaciones').val() == '' ? '00|00|00' : $('#hdHoraAgendaRenovaciones').val();
    var HoraInicio = fecha + hi;
    var Estado = 1;
    var User = $("#dllVendedorAgendaRenovaciones").data("kendoDropDownList").value();
    var CodigoPaquete = 0;
    var TipoActividad = $('#ddlTipoActividad_Renovaciones').data('kendoDropDownList').value();


    $.ajax({
        data: '{"Codigo":"' + CodigoAgenda + '","CodigoSocio":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","DescTipo":"' + DescTipo + '","Asunto":"' + asunto + '","HoraInicio":"' + HoraInicio + '","Color":"' + color + '","User":"' + User + '","Estado":"' + Estado + '","CodigoPaquete":"' + CodigoPaquete + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/GuardarAgendaSeguimientoTodos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han agregado correctamente.", { type: 'success', width: 'auto' });
                $('#txtAsuntoAgendarRenovaciones').val('');
                $('#txtValidarClaveAgendarRenovaciones').val('');
                $('#modalAgendaRenovaciones').hide('fast');

            } else {
                $.bootstrapGrowl("Error al guardar actividad en renovaciones.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            document.getElementById("btnFiltroBusqueda").click();
        }
    });
}

function GuardarAgendaReagendarClientesCaidos() {

    $('button[type="button"]').attr("disabled", true);
    var CodigoTipoAgenda = $('#hdCodTipoAgendaClientesCaidos').val();
    var CodigoSocio = $('#hdCodigoSocioCaidos').val();
    var CodigoAgenda = '0';
    var DescTipo = 0;
    var color = "";
    var asunto = $('#txtAsuntoReangendarClientesCaidos').val();
    var diaG = kendo.toString($("#txtFechaReagendarClientesCaidos").data('kendoDatePicker').value(), 'dd');
    var MesG = kendo.toString($("#txtFechaReagendarClientesCaidos").data('kendoDatePicker').value(), 'MM');
    var AnioG = kendo.toString($("#txtFechaReagendarClientesCaidos").data('kendoDatePicker').value(), 'yyyy');
    var fecha = AnioG + "|" + MesG + "|" + diaG + "|";
    var hi = $('#hdHoraAgendaCitasCaidas').val() == '' ? '00|00|00' : $('#hdHoraAgendaCitasCaidas').val();
    var HoraInicio = fecha + hi;
    var Estado = 1;
    var User = $('#dllVendedoresAgendaCaida').data("kendoDropDownList").value();
    var CodigoPaquete = 0;
    var TipoActividad = $('#ddlTipoActividad_CaidaAgenda').data('kendoDropDownList').value();


    $.ajax({
        data: '{"Codigo":"' + CodigoAgenda + '","CodigoSocio":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","DescTipo":"' + DescTipo + '","Asunto":"' + asunto + '","HoraInicio":"' + HoraInicio + '","Color":"' + color + '","User":"' + User + '","Estado":"' + Estado + '","CodigoPaquete":"' + CodigoPaquete + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/GuardarReagendarAgendaSeguimientoTodosCaidos",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg > 0) {
                $.bootstrapGrowl("Los datos se han agregado correctamente.", { type: 'success', width: 'auto' });
                $('#txtAsuntoReangendarClientesCaidos').val('');
                $('#ModalReagendarClientesCaidos').hide('fast');
                ListarSociosLibresAsesores();
            } else {
                $.bootstrapGrowl("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias", { type: 'danger', width: 'auto' });
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function listardllCreadoPor_Matriculados() {
    var dllAsesoresVentas = $("#ddlUsuarioCreador_Matriculados").kendoDropDownList({
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/usplistardllCreadoPor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            ListardllTiempoPaquete_Matriculados();
                        }
                    });
                }
            }
        }, change: function () {
        }

    }).data("kendoDropDownList");
}

function ListardllTiempoPaquete_Matriculados() {
    var ddlTiempoMembresiaPaqueteBuscador_Matriculados = $("#ddlTiempoMembresiaPaqueteBuscador_Matriculados").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Promoción",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="ddlTiempoMembresiaPaqueteBuscador_Matriculados_listbox"]').val();
                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            ListarTipoAgendaGeneral_Matriculados();
                        }
                    });
                }
            }
        }, change: function () {
            $('#hdddlTiempoMembresiaPaqueteBuscador_Matriculados').val(ddlTiempoMembresiaPaqueteBuscador_Matriculados.value());
        }
    }).data("kendoDropDownList");
}

function ListarTipoAgendaGeneral_Matriculados() {

    $.ajax({
        type: "POST",
        url: "/gestionce/uspListarTipoAgendaCliente",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.length > 0) {
                var control = ' <table style="text-align:left;width:100%;" border="0"> ';
                control += '<tr>';
                control += '<td>';
                control += '<div class="custom-control custom-radio" style="border: 1px solid #000;background-color:#000;color:#fff;border-radius: 8px;padding: 2px;height: 26px;">';
                control += '&nbsp;<input type="radio" class="custom-control-input" name="gruporbdOrigenMatriculados" id="rbdgruporbdOrigenMatriculados_0" value="0" checked>&nbsp;&nbsp;';
                control += '<label class="custom-control-label" for="rbdgruporbdOrigenMatriculados_0" style="cursor:pointer;padding: 2px;">Todos</label>';
                control += '<div>';
                control += '</td>';
                for (var i = 0; i < msg.length; i++) {
                    control += '<td>';
                    control += '<div class="custom-control custom-radio" style="border: 1px solid #000;background-color:#000;color:#fff;border-radius: 8px;padding: 2px;height: 26px;">';
                    control += '&nbsp;<input type="radio" class="custom-control-input" name="gruporbdOrigenMatriculados" id="rbdgruporbdOrigenMatriculados_' + msg[i].Codigo + '" value=' + msg[i].Codigo + ' >&nbsp;&nbsp;';
                    control += '<label class="custom-control-label" for="rbdgruporbdOrigenMatriculados_' + msg[i].Codigo + '" style="cursor:pointer;padding: 2px;">' + msg[i].Descripcion + '</label>';
                    control += '<div>';
                    control += '</td>';
                }
                control += '<tr>';
                control += '</table>';
                $('#divControlOrigenMatriculados').html(control);
            }

        }, complete: function () {

        }
    });

}

function listardllCreadoPor() {
    var dllAsesoresVentas = $("#ddlUsuarioCreador").kendoDropDownList({
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/usplistardllCreadoPor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            ListardllTiempoPaquete();
                        }
                    });
                }
            }
        }, change: function () {
        }

    }).data("kendoDropDownList");
}

function listardllCreadoPor_Auditoria() {

    var dllAsesoresVentas = $("#ddlUsuarioCreador_Auditoria").kendoDropDownList({
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/usplistardllCreadoPor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {
        }

    }).data("kendoDropDownList");
}

function listardllVendedor_ProspectosSinCita() {
    var dllAsesoresVentas = $("#ddlVendedor_ProspectosSinCita").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/usplistardllCreadoPor_ProspectosSinCita",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {
        }

    }).data("kendoDropDownList");
}

function listardllVendedor_Oportunidades() {

    var dllAsesoresVentas = $("#oportunidades-ddlEmbudosVenta_vendedores").kendoDropDownList({
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/usplistardllCreadoPor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            var vendedorlogin = getCookie("_Usuario_Business");
                            var validador = false;


                            for (var i = 0; i < msg.length; i++) {
                                if (msg[i].NombreCompleto.toString().toUpperCase().trim() == vendedorlogin.toString().toUpperCase().trim()) {
                                    validador = true;
                                }

                            }

                            if (validador) {
                                $("#oportunidades-ddlEmbudosVenta_vendedores").data("kendoDropDownList").value(vendedorlogin.toString().trim());
                                $("#oportunidades-ddlEmbudosVenta_vendedores").data("kendoDropDownList").enable(false);
                                $("#oportunidades-ddlEstadoTrato").data("kendoDropDownList").enable(false);
                            } else {
                                $("#oportunidades-ddlEmbudosVenta_vendedores").data("kendoDropDownList").enable(true);
                                $("#oportunidades-ddlEstadoTrato").data("kendoDropDownList").enable(true);
                            }

                        }, complete: function () {
                            event_ListarDDLEmbudoVenta_Oportunidades();
                        }
                    });
                }
            }
        }, change: function () {
            event_CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto_Oportunidades();
        }

    }).data("kendoDropDownList");
}


function ListardllTiempoPaquete() {
    var dllTiempoPaquete = $("#dllTiempoPaquete").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Promoción",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="dllTiempoPaquete_listbox"]').val();
                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            ListarTipoAgendaGeneral();
                        }
                    });
                }
            }
        }, change: function () {
        }
    }).data("kendoDropDownList");
}

function ListarTipoAgendaGeneral() {

    var ddlTipoAgenda = $("#ddlTipoAgendaGeneral").kendoDropDownList({
        optionLabel: "Origen",
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        valueTemplate: '<table><tr><td><span><div style="background:#: data.Color #;-moz-border-radius: 70px; -webkit-border-radius: 70px;border-radius: 70px;width: 15px;height: 15px; "></div></span></td><td><span style="font-size:11px;font-weight:bold;">#: data.Descripcion #</span></td></tr></table>',
        template: '<table><tr><td><span><div style="background:#: data.Color #;-moz-border-radius: 70px; -webkit-border-radius: 70px;border-radius: 70px;width: 15px;height: 15px; "></div></span></td><td><span style="font-size:11px;font-weight:bold;">#: data.Descripcion #</span></td></tr></table>',
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/uspListarTipoAgendaCliente",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            $("#dllTipoCliente").kendoDropDownList();
                            $("#ddlTipoActividad_Actividades").kendoDropDownList();
                        }, complete: function () {
                            ListarGridAgendaGeneral();
                        }
                    });
                }
            }
        }, change: function () {

        }
    }).data("kendoDropDownList");
}

function listardllVendedoresCLientesSinCIta() {
    var ddlUsuarioCreadorClientesSinCita = $("#ddlUsuarioCreadorClientesSinCita").kendoDropDownList({
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/usplistardllCreadoPor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            TiempoMembresiaInactivosPaqueteBuscador();
                        }
                    });
                }
            }
        }, change: function () {
        }

    }).data("kendoDropDownList");
}

function ListarGridAgendaGeneral() {

    var Buscador = $("#txtBuscadorClienteAgendaGeneral").val();
    var FechaDesde = kendo.toString($("#txtFechaDesde_AgendaGeneral").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaHasta = kendo.toString($("#txtFechaHasta_AgendaGeneral").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var CodigoTipoAgenda = $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value() == "" ? 0 : $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value();
    var UsuarioCreador = $('#ddlUsuarioCreador').data('kendoDropDownList').value(); //posicion 0 = Todos
    var CodTiempoPaquete = $('#dllTiempoPaquete').data('kendoDropDownList').value() == "" ? 0 : $('#dllTiempoPaquete').data('kendoDropDownList').value();
    var TipoCliente = 0;
    var TipoActividad = $('#ddlTipoActividad_Actividades').data('kendoDropDownList').value() == "" ? 0 : $('#ddlTipoActividad_Actividades').data('kendoDropDownList').value();

    document.getElementById('loadMe').style.display = 'block';
    $("#gridAgendaGeneral").empty();
    $("#gridAgendaGeneral").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"Buscador":"' + Buscador + '","FechaDesde":"' + FechaDesde + '","FechaHasta":"' + FechaHasta + '","CodigoTipoAgenda":"' + CodigoTipoAgenda + '","UsuarioCreador":"' + UsuarioCreador + '","CodTiempoPaquete":"' + CodTiempoPaquete + '","TipoCliente":"' + TipoCliente + '","PageNumber":"' + 1 + '","TipoActividad":"' + TipoActividad + '"}',
                        url: "/gestionce/uspListarGridAgendaGeneral_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverPaging: false,
                        serverFiltering: false,
                        success: function (msg) {
                            options.success(msg);

                        }, complete: function () {

                            uspListarGridAgendaGeneral_NumeroRegistros();

                            document.getElementById('loadMe').style.display = 'none';

                            $("#gridAgendaGeneral tbody").on("dblclick", "tr", function (e) {

                                $('#flagVentanaHistorialAgenda').val("1");

                                var User = getCookie("_Usuario_Business");
                                var vendedor = $("#lblInfDesVendedor").html();

                                if (User.toUpperCase() == vendedor.toUpperCase()) {

                                    $('#modalReagendarAgenda').show('fast');
                                    var todayDate = new Date();
                                    $("#txtFechaReagendarAgenda").kendoDatePicker();
                                    $('#txtFechaReagendarAgenda').data("kendoDatePicker").value(todayDate);

                                    var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
                                    var CodigoSocio = $("#hdCodigoSocio").val();
                                    event_ListarHistorialActividades_Actividades(CodigoSocio, CodigoTipoAgenda);
                                    event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_reagendar(CodigoTipoAgenda, CodigoSocio);

                                } else {

                                    $('#modalHistorialAgendaObservaciones').show('fast');
                                    var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
                                    var CodigoSocio = $("#hdCodigoSocio").val();
                                    event_ListarHistorialActividades_Observaciones(CodigoSocio, CodigoTipoAgenda);
                                    $('#divMensajeAgendar').html('Este cliente le pertenece a otro vendedor.');

                                }

                            });

                            $('#cerrarmodalReagendarAgenda').click(function () {
                                $('#modalReagendarAgenda').hide('fast');
                            });
                            $('#divCerrarmodalHistorialAgendaObservaciones').click(function () {
                                $('#modalHistorialAgendaObservaciones').hide('fast');
                            });
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: false,
        height: 710,
        columns: [{
            template: '<div style="width:25px;margin-left: -3px;"><label style="background-color:#: ColorAgenda #;width: 21px;border-radius:25px;height: 21px;"></label></div>',
            title: "",
            width: 5
        },
        {
            field: "DescTipoAgenda",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>ORIGEN</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "CantidadCitas",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>ACCION</center>",
            width: 6,
            template: "<img src='#: imgTipoActividad #' style='width:12px;height:12px;' /> #: CantidadCitas #",
            attributes: {
                style: "font-size:12px;text-align:center;font-weight: bold;"
            }
        }, {
            field: "FechaCreacion",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA CREADO</center>",
            width: 21,
            template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "HoraInicioAgenda",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA PROGRAMADO<center>",
            width: 21,
            template: "#= kendo.toString(kendo.parseDate(HoraInicioAgenda, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CODIGO</center>",
            width: 9,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "Nombre",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>NOMBRES</center>",
            width: 22,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "Apellidos",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>APELLIDOS</center>",
            width: 27,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:20px;cursor:pointer;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CELULAR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "DesTiempoPaquete",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>PLAN</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "Costo",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>PRECIO</center>",
            width: 10,
            attributes: {
                style: "font-size:12px;color:black;text-align:center;font-weight:500;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>VENDEDOR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DiasFaltantesCaida",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FALTA</center>",
            template: "#: DiasFaltantesCaida # días",
            width: 9,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<div ><button id='EnviarClienteGridAgendaGeneral_#: CodigoSocio #' style='display:none;font-size:11px;' type='button' class='btn btn-light btn-sm' onclick='evento_EnviarGridGeneralProspectoACliente(/#: Vendedor #/)'>Enviar a cliente</button><button id='EnviarClienteGridAgendaGeneralVendido_#: CodigoSocio #' style='display:none;font-size:11px;' type='button' class='btn btn-light btn-sm' onclick='evento_EnviarGridGeneralVentaCerrada(#: Codigo #,#: CodigoSocio #,#: CodigoTipoAgenda #,/#: Vendedor #/)'>Confirmar Venta</button></div>",
            title: "",
            width: 18
        }],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));

                // $("[id*=ReagendarGridAgendaGeneral_]").hide('fast');
                $("[id*=EnviarClienteGridAgendaGeneral_]").hide('fast');
                $("[id*=EnviarClienteGridAgendaGeneralVendido_]").hide('fast');

                //$("#ReagendarGridAgendaGeneral_" + dataItem.CodigoSocio).show('fast');
                if (dataItem.CodigoTipoAgenda == 2 || dataItem.CodigoTipoAgenda == 3) {
                    $("#EnviarClienteGridAgendaGeneralVendido_" + dataItem.CodigoSocio).show('fast');
                } else {
                    $("#EnviarClienteGridAgendaGeneral_" + dataItem.CodigoSocio).show('fast');
                }

                $('#imginfoCelular_CitasPendientes').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_CitasPendientes').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_CitasPendientes').attr('href', 'https://api.whatsapp.com/');
                }

                $('#imginfoCelular_Observaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/');
                }

                $("#hdCodigoTipoAgenda").val(dataItem.CodigoTipoAgenda);
                $("#hdCodigoSocio").val(dataItem.CodigoSocio);
                $("#txtValor_Reagendar").val(dataItem.Costo);
                $("#lblVendedor_Reagendar").html(dataItem.Vendedor.trim());

                $("#lblInfDesTipoCita").html(dataItem.DescTipoAgenda);
                $("#lblInfNombre").html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombre.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $("#lblInfNombre_Observaciones").html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombre.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $("#lblInfDesVendedor").html(dataItem.Vendedor.trim());
                //datos del cliente a enviar
                $("#lblNombreClienteEnviadoAgendaGeneral").val(dataItem.Nombre);
                $("#lblApellidosClienteEnviadoAgendaGeneral").val(dataItem.Apellidos);
                $("#lblDniClienteEnviadoAgendaGeneral").val(dataItem.DNI);

            });

        }
    });
}

function uspListarGridAgendaGeneral_NumeroRegistros() {

    var Buscador = $("#txtBuscadorClienteAgendaGeneral").val();
    var FechaDesde = kendo.toString($("#txtFechaDesde_AgendaGeneral").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaHasta = kendo.toString($("#txtFechaHasta_AgendaGeneral").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var CodigoTipoAgenda = $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value() == "" ? 0 : $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value();
    var UsuarioCreador = $('#ddlUsuarioCreador').data('kendoDropDownList').value(); //posicion 0 = Todos
    var CodTiempoPaquete = $('#dllTiempoPaquete').data('kendoDropDownList').value() == "" ? 0 : $('#dllTiempoPaquete').data('kendoDropDownList').value();
    var TipoCliente = $('#dllTipoCliente').data('kendoDropDownList').value() == "" ? 0 : $('#dllTipoCliente').data('kendoDropDownList').value();
    var TipoActividad = $('#ddlTipoActividad_Actividades').data('kendoDropDownList').value() == "" ? 0 : $('#ddlTipoActividad_Actividades').data('kendoDropDownList').value();

    $.ajax({
        data: '{"Buscador":"' + Buscador + '","FechaDesde":"' + FechaDesde + '","FechaHasta":"' + FechaHasta + '","CodigoTipoAgenda":"' + CodigoTipoAgenda + '","UsuarioCreador":"' + UsuarioCreador + '","CodTiempoPaquete":"' + CodTiempoPaquete + '","TipoCliente":"' + TipoCliente + '","TipoActividad":"' + TipoActividad + '"}',
        type: "POST",
        url: "/gestionce/uspListarGridAgendaGeneral_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarGridAgendaGeneral').html(msg.CantTotal);
            ddlPaginacionuspListarGridAgendaGeneral_Paginacion(msg.CantTotal, msg.TamanioPagina);
            $('#lblMontoTotal').html(msg.MontoTotal);
            $('#lblDiaCitaCaida').html(msg.DiasCitaCaida);
            $('#lblDiaCitaCaidaRenovaciones').html(msg.DiasCitaCaida);
            $('#lblDiaCitaCaida_SinSeguimiento').html(msg.DiasCitaCaida);
        }, complete: function () {

        }
    });
}

function ddlPaginacionuspListarGridAgendaGeneral_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarGridAgendaGeneral_Paginacion').html(htmlOpcion);
    $("#ddlPaginacionuspListarGridAgendaGeneral_Paginacion").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarGridAgendaGeneral_Paginacion").data("kendoDropDownList").value();
            ListarGridAgendaGeneral_ChanguePage(nroPagina);
        }
    });
}

function ListarGridAgendaGeneral_ChanguePage(PageNumber) {

    var Buscador = $("#txtBuscadorClienteAgendaGeneral").val();
    var FechaDesde = kendo.toString($("#txtFechaDesde_AgendaGeneral").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaHasta = kendo.toString($("#txtFechaHasta_AgendaGeneral").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var CodigoTipoAgenda = $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value() == "" ? 0 : $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value();
    var UsuarioCreador = $('#ddlUsuarioCreador').data('kendoDropDownList').value();
    var CodTiempoPaquete = $('#dllTiempoPaquete').data('kendoDropDownList').value() == "" ? 0 : $('#dllTiempoPaquete').data('kendoDropDownList').value();
    var TipoCliente = $('#dllTipoCliente').data('kendoDropDownList').value() == "" ? 0 : $('#dllTipoCliente').data('kendoDropDownList').value();

    var TipoActividad = $('#ddlTipoActividad_Actividades').data('kendoDropDownList').value() == "" ? 0 : $('#ddlTipoActividad_Actividades').data('kendoDropDownList').value();

    document.getElementById('loadMe').style.display = 'block';
    $("#gridAgendaGeneral").empty();
    $("#gridAgendaGeneral").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"Buscador":"' + Buscador + '","FechaDesde":"' + FechaDesde + '","FechaHasta":"' + FechaHasta + '","CodigoTipoAgenda":"' + CodigoTipoAgenda + '","UsuarioCreador":"' + UsuarioCreador + '","CodTiempoPaquete":"' + CodTiempoPaquete + '","TipoCliente":"' + TipoCliente + '","PageNumber":"' + PageNumber + '","TipoActividad":"' + TipoActividad + '"}',
                        url: "/gestionce/uspListarGridAgendaGeneral_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverPaging: true,
                        serverFiltering: true,
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                            document.getElementById('loadMe').style.display = 'none';

                            $("#gridAgendaGeneral tbody").on("dblclick", "tr", function (e) {

                                $('#flagVentanaHistorialAgenda').val("1");

                                var User = getCookie("_Usuario_Business");
                                var vendedor = $("#lblInfDesVendedor").html();

                                if (User.toUpperCase() == vendedor.toUpperCase()) {

                                    $('#modalReagendarAgenda').show('fast');
                                    var todayDate = new Date();
                                    $("#txtFechaReagendarAgenda").kendoDatePicker();
                                    $('#txtFechaReagendarAgenda').data("kendoDatePicker").value(todayDate);

                                    var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
                                    var CodigoSocio = $("#hdCodigoSocio").val();
                                    event_ListarHistorialActividades_Actividades(CodigoSocio, CodigoTipoAgenda);
                                    event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_reagendar(CodigoTipoAgenda, CodigoSocio);


                                } else {

                                    $('#modalHistorialAgendaObservaciones').show('fast');
                                    var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
                                    var CodigoSocio = $("#hdCodigoSocio").val();
                                    event_ListarHistorialActividades_Observaciones(CodigoSocio, CodigoTipoAgenda);
                                    $('#divMensajeAgendar').html('Este cliente le pertenece a otro vendedor.');

                                }

                            });

                            $('#cerrarmodalReagendarAgenda').click(function () {
                                $('#modalReagendarAgenda').hide('fast');
                            });
                            $('#divCerrarmodalHistorialAgendaObservaciones').click(function () {
                                $('#modalHistorialAgendaObservaciones').hide('fast');
                            });

                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 710,
        columns: [{
            template: '<div style="width:25px;margin-left: -3px;"><label style="background-color:#: ColorAgenda #;width: 21px;border-radius:25px;height: 21px;"></label></div>',
            title: "",
            width: 5
        },
        {
            field: "DescTipoAgenda",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>ORIGEN</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "CantidadCitas",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>ACCION</center>",
            width: 6,
            template: "<img src='#: imgTipoActividad #' style='width:12px;height:12px;' /> #: CantidadCitas #",
            attributes: {
                style: "font-size:12px;text-align:center;font-weight: bold;"
            }
        }, {
            field: "FechaCreacion",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA CREADO</center>",
            width: 21,
            template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "HoraInicioAgenda",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA PROGRAMADO<center>",
            width: 21,
            template: "#= kendo.toString(kendo.parseDate(HoraInicioAgenda, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CODIGO</center>",
            width: 9,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "Nombre",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>NOMBRES</center>",
            width: 22,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "Apellidos",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>APELLIDOS</center>",
            width: 27,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:20px;cursor:pointer;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CELULAR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "DesTiempoPaquete",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>PLAN</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "Costo",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>PRECIO</center>",
            width: 10,
            attributes: {
                style: "font-size:12px;color:black;text-align:center;font-weight:500;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>VENDEDOR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DiasFaltantesCaida",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FALTA</center>",
            template: "#: DiasFaltantesCaida # días",
            width: 9,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<div ><button id='EnviarClienteGridAgendaGeneral_#: CodigoSocio #' style='display:none;font-size:11px;' type='button' class='btn btn-light btn-sm' onclick='evento_EnviarGridGeneralProspectoACliente(/#: Vendedor #/)'>Enviar a cliente</button><button id='EnviarClienteGridAgendaGeneralVendido_#: CodigoSocio #' style='display:none;font-size:11px;' type='button' class='btn btn-light btn-sm' onclick='evento_EnviarGridGeneralVentaCerrada(#: Codigo #,#: CodigoSocio #,#: CodigoTipoAgenda #,/#: Vendedor #/)'>Confirmar Venta</button></div>",
            title: "",
            width: 18
        }],
        dataBound: function (e) {
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));

                $("[id*=EnviarClienteGridAgendaGeneral_]").hide('fast');
                $("[id*=EnviarClienteGridAgendaGeneralVendido_]").hide('fast');

                if (dataItem.CodigoTipoAgenda == 2 || dataItem.CodigoTipoAgenda == 3) {
                    $("#EnviarClienteGridAgendaGeneralVendido_" + dataItem.CodigoSocio).show('fast');
                } else {
                    $("#EnviarClienteGridAgendaGeneral_" + dataItem.CodigoSocio).show('fast');
                }

                $('#imginfoCelular_CitasPendientes').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_CitasPendientes').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_CitasPendientes').attr('href', 'https://api.whatsapp.com/');
                }

                $('#imginfoCelular_Observaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/');
                }

                $("#hdCodigoTipoAgenda").val(dataItem.CodigoTipoAgenda);
                $("#hdCodigoSocio").val(dataItem.CodigoSocio);
                $("#txtValor_Reagendar").val(dataItem.Costo);
                $("#lblVendedor_Reagendar").html(dataItem.Vendedor.trim());

                $("#lblInfDesTipoCita").html(dataItem.DescTipoAgenda);
                $("#lblInfNombre").html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombre.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $("#lblInfNombre_Observaciones").html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombre.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $("#lblInfDesVendedor").html(dataItem.Vendedor.trim());
                //datos del cliente a enviar
                $("#lblNombreClienteEnviadoAgendaGeneral").val(dataItem.Nombre);
                $("#lblApellidosClienteEnviadoAgendaGeneral").val(dataItem.Apellidos);
                $("#lblDniClienteEnviadoAgendaGeneral").val(dataItem.DNI);

            });
        }
    });
}

//AUDITORIA
function ListarGridAgendaGeneralAuditoria() {

    var Buscador = $("#txtBuscadorCliente_Auditoria").val();
    var FechaDesde = kendo.toString($("#txtFechaDesde_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaHasta = kendo.toString($("#txtFechaHasta_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var UsuarioCreador = $('#ddlUsuarioCreador_Auditoria').data('kendoDropDownList').value(); //posicion 0 = Todos

    document.getElementById('loadMe').style.display = 'block';
    $("#gridAuditoria").empty();
    $("#gridAuditoria").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"Buscador":"' + Buscador + '","FechaDesde":"' + FechaDesde + '","FechaHasta":"' + FechaHasta + '","UsuarioCreador":"' + UsuarioCreador + '","PageNumber":"' + 1 + '"}',
                        url: "/gestionce/uspListarGridAgendaGeneralAuditoria_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverPaging: true,
                        serverFiltering: true,
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            uspListarGridAgendaGeneralAuditoria_NumeroRegistros();
                            uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor();
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 550,
        columns: [{
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:16px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "DescTipoAgenda",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>ORIGEN</center>",
            width: 10,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "DescTipoActividad",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>ACCION</center>",
            width: 16,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "FechaCreacion",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>FECHA CREADO</center>",
            width: 19,
            template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "HoraInicioAgenda",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>FECHA PROGRAMADA<center>",
            width: 19,
            template: "#= kendo.toString(kendo.parseDate(HoraInicioAgenda, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>CODIGO</center>",
            width: 9,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "Nombre",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>NOMBRES</center>",
            width: 20,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "Apellidos",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>APELLIDOS</center>",
            width: 23,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>CELULAR</center>",
            width: 15,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "DesTiempoPaquete",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>PLAN</center>",
            width: 12,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "Costo",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>PRECIO</center>",
            width: 9,
            attributes: {
                style: "font-size:11px;color:black;text-align:center;font-weight:500;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>VENDEDOR</center>",
            width: 17,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Asunto",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>ASUNTO</center>",
            width: 30,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }]
        ,
        dataBound: function (e) {
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));

            });
        }
    });
}

function ListarGridAgendaGeneralAuditoria_ChanguePage(PageNumber) {

    var Buscador = $("#txtBuscadorCliente_Auditoria").val();
    var FechaDesde = kendo.toString($("#txtFechaDesde_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaHasta = kendo.toString($("#txtFechaHasta_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var UsuarioCreador = $('#ddlUsuarioCreador_Auditoria').data('kendoDropDownList').value(); //posicion 0 = Todos

    document.getElementById('loadMe').style.display = 'block';
    $("#gridAuditoria").empty();
    $("#gridAuditoria").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"Buscador":"' + Buscador + '","FechaDesde":"' + FechaDesde + '","FechaHasta":"' + FechaHasta + '","UsuarioCreador":"' + UsuarioCreador + '","PageNumber":"' + PageNumber + '"}',
                        url: "/gestionce/uspListarGridAgendaGeneralAuditoria_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverPaging: true,
                        serverFiltering: true,
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 550,
        columns: [{
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:16px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "DescTipoAgenda",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>ORIGEN</center>",
            width: 10,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "DescTipoActividad",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>ACCION</center>",
            width: 16,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "FechaCreacion",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>FECHA CREADO</center>",
            width: 19,
            template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "HoraInicioAgenda",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>FECHA PROGRAMADA<center>",
            width: 19,
            template: "#= kendo.toString(kendo.parseDate(HoraInicioAgenda, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>CODIGO</center>",
            width: 9,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "Nombre",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>NOMBRES</center>",
            width: 20,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "Apellidos",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>APELLIDOS</center>",
            width: 23,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>CELULAR</center>",
            width: 15,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "DesTiempoPaquete",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>PLAN</center>",
            width: 12,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "Costo",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>PRECIO</center>",
            width: 9,
            attributes: {
                style: "font-size:11px;color:black;text-align:center;font-weight:500;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>VENDEDOR</center>",
            width: 17,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Asunto",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>ASUNTO</center>",
            width: 30,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }]
        ,
        dataBound: function (e) {
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));

            });
        }
    });
}

function uspListarGridAgendaGeneralAuditoria_NumeroRegistros() {
    var Buscador = $("#txtBuscadorCliente_Auditoria").val();
    var FechaDesde = kendo.toString($("#txtFechaDesde_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaHasta = kendo.toString($("#txtFechaHasta_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var UsuarioCreador = $('#ddlUsuarioCreador_Auditoria').data('kendoDropDownList').value(); //posicion 0 = Todos

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"Buscador":"' + Buscador + '","FechaDesde":"' + FechaDesde + '","FechaHasta":"' + FechaHasta + '","UsuarioCreador":"' + UsuarioCreador + '"}',
        type: "POST",
        url: "/gestionce/uspListarGridAgendaGeneralAuditoria_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#lblCantidad_Auditoria').html(msg.CantTotal);
            ddlPaginacionuspListarGridAgendaGeneralAuditoria_Paginacion(msg.CantTotal, msg.TamanioPagina);
            //$('#lblMontoTotal').html(msg.MontoTotal);
            //$('#lblDiaCitaCaida').html(msg.DiasCitaCaida);
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function ddlPaginacionuspListarGridAgendaGeneralAuditoria_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarGridAuditoria_Paginacion').html(htmlOpcion);
    $("#ddlPaginacionuspListarGridAuditoria_Paginacion").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarGridAuditoria_Paginacion").data("kendoDropDownList").value();
            ListarGridAgendaGeneralAuditoria_ChanguePage(nroPagina);
        }
    });
}


function uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor() {

    var FechaDesde = kendo.toString($("#txtFechaDesde_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaHasta = kendo.toString($("#txtFechaHasta_Auditoria").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    document.getElementById('loadMe').style.display = 'block';

    $("#floarChart_ActividadesPorVendedor").empty();
    $("#floarChart_ActividadesPorVendedor").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"FechaDesde":"' + FechaDesde + '","FechaHasta":"' + FechaHasta + '"}',
                        url: "/gestionce/uspListarGridAgendaGeneralAuditoria_TotalActividadPorVendedor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverPaging: true,
                        serverFiltering: true,
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 180,
        columns: [{
            field: "UsuarioCreacion",
            title: "<center style='color:#fff;font-size:11px;font-weight: bold;'>VENDEDOR</center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad7",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>M. WHATSAPP <div class='fab fa-whatsapp'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad6",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>M. TEXTO <div class='fas fa-comment-alt'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad5",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>E. CORREO <div class='fas fa-envelope'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad8",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>NOTAS <div class='fas fa-file-signature'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad2",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>LL. CONTESTADA <div class='fas fa-phone-volume'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad3",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>LL. NO CONTESTADA <div class='fas fa-phone-slash'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad4",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>LL. PROGRAMADA <div class='fas fa-phone-square'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad9",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CITAS <div class='fas fa-calendar-alt'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "actividad1",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>REUNION <div class='fas fa-calendar-check'></div></center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }],
        dataBound: function (e) {
            this.select(this.tbody.find('>tr:first'));
        },
        change: function () {
            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));

            });
        }
    });

}


//---FIN AUDITORIA

function ListarCboVendedoresMigrador() {
    var ind = 'M'

    var dllAsesoresVentas = $("#cboVendedorMigrador").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"ind":"' + ind + '"}',
                        type: "POST",
                        url: "/gestionce/ListarCboVendedoresMigrador",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            getCantSocios_Migrador();
                        }
                    });
                }
            }
        }, change: function () {
            Lista_ClientesMigrar();
            getCantSocios_Migrador()
        }

    }).data("kendoDropDownList");
}

function ListarCboVendedoresReceptor() {
    var ind = 'R';
    var dllAsesoresVentas = $("#cboVendedorReceptor").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"ind":"' + ind + '"}',
                        type: "POST",
                        url: "/gestionce/ListarCboVendedoresReceptor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            getCantSocios_Receptor();
                        }
                    });
                }
            }
        }, change: function () {
            getCantSocios_Receptor();
        }

    }).data("kendoDropDownList");
}

function ListarCboVendedoresReceptor_MigracionPorSocio() {
    var ind = 'R';
    var dllAsesoresVentas = $("#MigracionPorSocio_cboVendedorReceptor").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"ind":"' + ind + '"}',
                        type: "POST",
                        url: "/gestionce/ListarCboVendedoresReceptor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        }, change: function () {

        }

    }).data("kendoDropDownList");
}

function Lista_ClientesMigrar() {

    var Vendedor = "";
    var NombreCliente = $('#txtBusqueda_Migracion').val();

    $("#gridListaClientesMigrar").empty();
    $("#gridListaClientesMigrar").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Vendedor":"' + Vendedor + '","NombreCliente":"' + NombreCliente + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarSocios_PorVendedor_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            uspListarSocios_PorVendedor_NumeroRegistros();
                        }
                    });
                }
            }
        },
        height: 280,
        sortable: true,
        columns: [{
            field: "CodigoSocio",
            title: "Codigo",
            width: 10,
            attributes: {
                style: "font-size:10px;"
            }
        }, {
            field: "NombreCompleto",
            title: "Nombres",
            width: 50,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            field: "Vendedor",
            title: "Vendedor",
            width: 15,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            template: '<button onclick="Abrir_Confirmar_Migrar(#: CodigoSocio #)" title="" type="button" class="btn btn-sm btn-primary">migrar ahora</button>',
            width: 15
        }]
    });

}

function uspListarSocios_PorVendedor_NumeroRegistros() {
    var Vendedor = $("#cboVendedorMigrador").data('kendoDropDownList').value();
    var Asesor = "";
    if (Vendedor == undefined) {
        Asesor = $("#cboVendedorMigrador").data('kendoDropDownList').value();
    } else {
        Asesor = Vendedor;
    }
    var NombreCliente = $('#txtBusqueda_Migracion').val();

    $.ajax({
        data: '{"Vendedor":"' + Asesor + '","NombreCliente":"' + NombreCliente + '"}',
        type: "POST",
        url: "/gestionce/uspListarSocios_PorVendedor_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarSocios_PorVendedor').html(msg.CantTotal);
            ddlListarSocios_PorVendedor_Paginacion(msg.CantTotal, msg.TamanioPagina);

        }, complete: function () {

        }
    });
}

function ddlListarSocios_PorVendedor_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlListarSocios_PorVendedor').html(htmlOpcion);
    $("#ddlListarSocios_PorVendedor").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlListarSocios_PorVendedor").data("kendoDropDownList").value();
            Lista_ClientesMigrar_ChanguePage(nroPagina);
        }
    });
}

function Lista_ClientesMigrar_ChanguePage(PageNumber) {
    var Vendedor = $("#cboVendedorMigrador").data('kendoDropDownList').value();
    var Asesor = "";
    if (Vendedor == undefined) {
        Asesor = $("#cboVendedorMigrador").data('kendoDropDownList').value();
    } else {
        Asesor = Vendedor;
    }
    var NombreCliente = $('#txtBusqueda_Migracion').val();
    $("#gridListaClientesMigrar").empty();
    $("#gridListaClientesMigrar").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"Vendedor":"' + Asesor + '","NombreCliente":"' + NombreCliente + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarSocios_PorVendedor_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        },
        height: 280,
        sortable: true,
        columns: [{
            field: "CodigoSocio",
            title: "Codigo",
            width: 10,
            attributes: {
                style: "font-size:10px;"
            }
        }, {
            field: "NombreCompleto",
            title: "Nombres",
            width: 65,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            template: '<button onclick="Abrir_Confirmar_Migrar(#: CodigoSocio #)" title="" type="button" class="btn btn-sm btn-primary">migrar ahora</button>',
            width: 15
        }]
    });

}

function getCantSocios_Migrador() {
    var UserAsesorVenta = $("#cboVendedorMigrador").data('kendoDropDownList').value();
    $.ajax({
        data: '{"UserAsesorVenta":"' + UserAsesorVenta + '"}',
        type: "POST",
        url: "/gestionce/getCantSocios_Migrador",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantSociosMigrador').html(msg.CantidadSociosMigracion);
        }
    });
}

function getCantSocios_Receptor() {
    var UserAsesorVenta = $("#cboVendedorReceptor").data('kendoDropDownList').value();
    $.ajax({
        data: '{"UserAsesorVenta":"' + UserAsesorVenta + '"}',
        type: "POST",
        url: "/gestionce/getCantSocios_Receptor",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantSociosReceptor').html(msg.CantidadSociosMigracion);
        }
    });
}

function Abrir_Confirmar_Migrar(CodigoSocio) {

    $('button[type="button"]').attr("disabled", true);

    var Vendedor = $("#MigracionPorSocio_cboVendedorReceptor").data('kendoDropDownList').value();

    $.ajax({
        data: '{"CodigoSocio":"' + CodigoSocio + '","Vendedor":"' + Vendedor + '"}',
        type: "POST",
        url: "/gestionce/ActualizarAsesorComercial_Cliente",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg > 0) {
                $.bootstrapGrowl("Se migro el socio correctamente.", { type: 'success', width: 'auto' });
                Lista_ClientesMigrar();
                //getCantSocios_Migrador();
                //getCantSocios_Receptor();
            } else {
                $.bootstrapGrowl("Tenemos problemas para realizar esta operación, vuelve a intentarlo mas tarde.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
            ListarGridAgendaGeneral();
        }
    });
}

function validadorMigracion() {
    var valida = true;
    var txtCantMig = parseInt($('#txtCantMigracionParcial').val());

    var tipMigracion = $("#hdRadioMigracion").val();
    if (tipMigracion == 2) {
        if (txtCantMig == "") {
            valida = false;
            document.getElementById("validatorCantMigracionParcial").style.display = '';
        } else {
            var cantSocMigrador = parseInt($("#lblCantSociosMigrador").html());
            if (txtCantMig > cantSocMigrador) {
                valida = false;
                $.bootstrapGrowl("El Migrador no dispone de la cantidad ingresada", { type: 'danger', width: 'auto' });
            }
        }
    }
    return valida;
}

function GrabarMigrarAgenda() {
    document.getElementById('loadMe').style.display = 'block';
    document.getElementById('myModalMigrarAgenda').style.display = 'none';

    var UsuMigrador = $("#cboVendedorMigrador").data('kendoDropDownList').value();
    var UsuReceptor = $("#cboVendedorReceptor").data('kendoDropDownList').value();
    if (UsuMigrador.toUpperCase() == UsuReceptor.toUpperCase()) {
        $.bootstrapGrowl("No se puede migrar socios al mismo vendedor.", { type: 'danger', width: 'auto' });
        document.getElementById('loadMe').style.display = 'none';
        document.getElementById('myModalMigrarAgenda').style.display = 'block';
    } else {

        $('button[type="button"]').attr("disabled", true);
        var tipMigracion = $("#hdRadioMigracion").val();
        var cantMigracion = 0;

        $.ajax({
            data: '{"tipMigracion":"' + tipMigracion + '","cantMigracion":"' + cantMigracion + '","UsuMigrador":"' + UsuMigrador + '","UsuReceptor":"' + UsuReceptor + '"}',
            type: "POST",
            url: "/gestionce/GuardarMigrarAgenda",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg > 0) {
                    $.bootstrapGrowl("La migración ha sido correctamente.", { type: 'success', width: 'auto' });
                    ListarCboVendedoresMigrador();
                    ListarCboVendedoresReceptor();
                    $("#txtCantMigracionParcial").val('');
                } else {
                    $.bootstrapGrowl("Error al migrar, vuelve a intentarlo de nuevo.", { type: 'danger', width: 'auto' });
                }

            }, complete: function () {
                $('button[type="button"]').attr("disabled", false);
                document.getElementById('loadMe').style.display = 'none';
                document.getElementById('myModalMigrarAgenda').style.display = 'none';
                ListarGridAgendaGeneral();
            }
        });

    }
}

function ListarSociosLibresAsesores() {
    var flagBusquedaCliente = $('#txtBuscadorCarteraSocios').val();
    var FechaCaidaDesde = kendo.toString($("#FechaDesdeCaida").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaCaidaHasta = kendo.toString($("#FechaHastaCaida").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    document.getElementById('loadMe').style.display = 'block';
    $("#gvCarteraCliente").empty();
    $("#gvCarteraCliente").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"flagBusquedaCliente":"' + flagBusquedaCliente + '","FechaCaidaDesde":"' + FechaCaidaDesde + '","FechaCaidaHasta":"' + FechaCaidaHasta + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSociosLibresAsesores_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                            uspListarSociosLibresAsesores_NumeroRegistros();

                            $("#gvCarteraCliente tbody").on("dblclick", "tr", function (e) {

                                var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
                                var CodigoSocio = $("#hdCodigoSocio").val();
                                ReagendarClientesCaidos(CodigoTipoAgenda, CodigoSocio);

                            });

                        }
                    });
                }
            }
        },
        selectable: "row",
        height: 725,
        sortable: true,
        columns: [{
            field: "DescTipoAgenda",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>ORIGEN</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>FECHA CREADO</center>",
            width: 20,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DescFechaCaida",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>FECHA CAIDA</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "CodigoSocio",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>CODIGO</center>",
            width: 9,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombre",
            title: "<center style='font-size:11px;color: #fff;font-weight: bold;'>NOMBRES</center>",
            width: 23,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>APELLIDOS</center>",
            width: 25,
            attributes: {

                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "desTiempoPaquete",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>PLAN</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:16px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>CELULAR</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Costo",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>PRECIO</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>VENDEDOR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DescTipoCliente",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>TIPO</center>",
            width: 6,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "CantidadCitas",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>CITAS</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }
            //    ,
            //{
            //    template: '<div><button id="btnReagendarClientesCaidos_#: CodigoSocio #" style="font-size: 11px;display:none;" type="button" class="btn btn-light btn-sm " onclick="ReagendarClientesCaidos(#: TipoAgenda #,#: CodigoSocio #)">Levantar Cita</button></div>',
            //    width: 15
            //}
        ],
        dataBound: function (e) {
            this.select(this.tbody.find('>tr:first'));
        }, change: function () {

            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));
                $("#hdCodigoTipoAgenda").val(dataItem.TipoAgenda);
                $("#hdCodigoSocio").val(dataItem.CodigoSocio);
                //$("#lblInfDesTipoCita").html(dataItem.DescTipoAgenda);
                //$("#lblInfNombre").html(dataItem.Nombre.toString.toUpperCase() + ', ' + dataItem.Apellidos.toString.toUpperCase());
                $("#lblInfNombre_Caidos").html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombre.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $("#lblInfDesVendedor").html(dataItem.UsuarioCreacion);


                $('#imginfoCelular_Caidos').html(dataItem.Celular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Caidos').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Caidos').attr('href', 'https://api.whatsapp.com/');
                }

                $('#flagVentanaHistorialAgenda').val("2");

            });
        }
    });

}

function uspListarSociosLibresAsesores_NumeroRegistros() {
    var flagBusquedaCliente = $('#txtBuscadorCarteraSocios').val();
    var FechaCaidaDesde = kendo.toString($("#FechaDesdeCaida").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaCaidaHasta = kendo.toString($("#FechaHastaCaida").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    $.ajax({
        data: '{"flagBusquedaCliente":"' + flagBusquedaCliente + '","FechaCaidaDesde":"' + FechaCaidaDesde + '","FechaCaidaHasta":"' + FechaCaidaHasta + '"}',
        type: "POST",
        url: "/gestionce/uspListarSociosLibresAsesores_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarCarteraCliente').html(msg.CantTotal);
            ddlListarSociosLibresAsesores_Paginacion(msg.CantTotal, msg.TamanioPagina);

        }, complete: function () {

        }
    });
}

function ddlListarSociosLibresAsesores_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlListarSociosLibresAsesores').html(htmlOpcion);
    $("#ddlListarSociosLibresAsesores").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlListarSociosLibresAsesores").data("kendoDropDownList").value();
            ListarSociosLibresAsesores_ChanguePage(nroPagina);
        }
    });
}

function ListarSociosLibresAsesores_ChanguePage(PageNumber) {
    var flagBusquedaCliente = $('#txtBuscadorCarteraSocios').val();
    var FechaCaidaDesde = kendo.toString($("#FechaDesdeCaida").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaCaidaHasta = kendo.toString($("#FechaHastaCaida").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';
    $("#gvCarteraCliente").empty();
    $("#gvCarteraCliente").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"flagBusquedaCliente":"' + flagBusquedaCliente + '","FechaCaidaDesde":"' + FechaCaidaDesde + '","FechaCaidaHasta":"' + FechaCaidaHasta + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSociosLibresAsesores_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';

                            $("#gvCarteraCliente tbody").on("dblclick", "tr", function (e) {

                                var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
                                var CodigoSocio = $("#hdCodigoSocio").val();
                                ReagendarClientesCaidos(CodigoTipoAgenda, CodigoSocio);

                            });
                        }
                    });
                }
            }
        },
        selectable: "row",
        height: 725,
        sortable: true,
        columns: [{
            field: "DescTipoAgenda",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>ORIGEN</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DescFechaCreacion",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>FECHA CREADO</center>",
            width: 20,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DescFechaCaida",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>FECHA CAIDA</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "CodigoSocio",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>CODIGO</center>",
            width: 9,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombre",
            title: "<center style='font-size:11px;color: #fff;font-weight: bold;'>NOMBRES</center>",
            width: 23,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>APELLIDOS</center>",
            width: 25,
            attributes: {

                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "desTiempoPaquete",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>PLAN</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:16px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>CELULAR</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Costo",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>PRECIO</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>VENDEDOR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DescTipoCliente",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>TIPO</center>",
            width: 6,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "CantidadCitas",
            title: "<center style='font-size:12px;color: #fff;font-weight: bold;'>CITAS</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }
            //    ,
            //{
            //    template: '<div><button id="btnReagendarClientesCaidos_#: CodigoSocio #" style="font-size: 11px;display:none;" type="button" class="btn btn-light btn-sm " onclick="ReagendarClientesCaidos(#: TipoAgenda #,#: CodigoSocio #)">Levantar Cita</button></div>',
            //    width: 15
            //}
        ],
        dataBound: function (e) {
            this.select(this.tbody.find('>tr:first'));
        }, change: function () {

            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));
                $("#hdCodigoTipoAgenda").val(dataItem.TipoAgenda);
                $("#hdCodigoSocio").val(dataItem.CodigoSocio);
                $("#lblInfDesTipoCita").html(dataItem.DescTipoAgenda);
                $("#lblInfNombre").html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombre + ', ' + dataItem.Apellidos);
                $("#lblInfNombre_Caidos").html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombre.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $("#lblInfDesVendedor").html(dataItem.UsuarioCreacion);

                $('#imginfoCelular_Caidos').html(dataItem.Celular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Caidos').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Caidos').attr('href', 'https://api.whatsapp.com/');
                }

                $('#flagVentanaHistorialAgenda').val("2");

            });
        }
    });

}

function VerHistorialAgendaObservaciones() {
    var CodigoTipoAgenda = $('#hdCodigoTipoAgenda').val();
    var CodigoSocio = $('#hdCodigoSocio').val();

    $("#gridHistorialAgendaObservaciones").empty();
    $("#gridHistorialAgendaObservaciones").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSeguimientoAgenda",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                        }
                    });
                }
            }
        },
        sortable: true,
        height: 300,
        columns: [
            {
                field: "FechaCreacion",
                title: "<center><b style='text-align:center;color:#fff;'>Creado</b></center>",
                width: 10,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "HoraInicio",
                title: "<center><b style='text-align:center;color:#fff;'>Fecha Cita</b></center>",
                width: 10,
                template: "#= kendo.toString(kendo.parseDate(HoraInicio, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') #",
                attributes: {

                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Asunto",
                title: "<center><b style='text-align:center;color:#fff;'>Asunto</b></center>",
                width: 40,
                attributes: {

                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Vendedor",
                title: "<center><b style='text-align:center;color:#fff;'>Vendedor</b></center>",
                width: 10,
                attributes: {

                    style: "font-size:11px;text-align:center;"
                }
            }
        ]
    });
}

function ReagendarClientesCaidos(TipoAgenda, CodigoSocio) {
    $("#hdCodTipoAgendaClientesCaidos").val(TipoAgenda);
    $("#hdCodigoSocioCaidos").val(CodigoSocio);

    event_ListarHistorialActividades_Caidos(CodigoSocio, TipoAgenda);
    $("#ModalReagendarClientesCaidos").show('fast');

}

function validarNumeros(e) { // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // backspace
    if (e.ctrlKey && tecla == 86) { return true }; //Ctrl v
    if (e.ctrlKey && tecla == 67) { return true }; //Ctrl c
    if (e.ctrlKey && tecla == 88) { return true }; //Ctrl x
    if (tecla >= 35 && tecla <= 40) { return true; } //direcionales ini-fin
    if (tecla == 46) { return true; } //Supr
    if (tecla >= 96 && tecla <= 106) { return true; } //numpad
    if (tecla == 9) { return true; } //tab

    patron = /[0-9]/; // patron
    te = String.fromCharCode(tecla);
    return patron.test(te); // prueba
}

function BuscarInformeCitasVendedores() {
    var FechaInicio = kendo.toString($("#txtFechaInicioDesdeInformeVendedor").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFechaFinDesdeinformeVendedor").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    $("#gridListarInformeCitaVendedores").empty();
    $("#gridListarInformeCitaVendedores").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
                        type: "POST",
                        url: "/gestionce/ListarInformeCitaVendedores",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                        }, complete: function () {

                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 200,
        columns: [{
            field: "Vendedor",
            title: "Vendedor",
            width: 14,
            attributes: {

                style: "font-size:11px;"
            }
        }, {
            field: "CantCitasVendedores",
            title: "Num citas",
            width: 20,
            attributes: {

                style: "font-size:11px;"
            }
        }]
    });
}

function fechaMascara() {
    $("#txtFechaDesde_AgendaGeneral").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaHasta_AgendaGeneral").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaDesde_Auditoria").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaHasta_Auditoria").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });

    $("#FechaDesdeCaida").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#FechaHastaCaida").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaReagendarClientesCaidos").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaInicioDesdeInformeVendedor").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaFinDesdeinformeVendedor").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaReagendarAgenda").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
}

function cargarMenu() {

    $.ajax({
        type: "POST",
        url: "/gestionce/ListarPerfilMenu",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            for (var i = 0; i < msg.length; i++) {
                if (msg[i].CodigoPerfil == 0) {

                    var codigoMenu = msg[i].CodigoMenu;

                    $('li[data_idserver=' + codigoMenu + ']').hide(0);

                    $('td[data_idserver=' + codigoMenu + ']').hide(0);
                    $('div[data_idserver=' + codigoMenu + ']').hide(0);

                    $('img[data_idserver=' + codigoMenu + ']').hide(0);

                    $('button[data_idserver=' + codigoMenu + ']').hide(0);

                    if (codigoMenu >= 55 && codigoMenu <= 60) {
                        numCliente = 0;
                    }

                    if (codigoMenu == 61) {
                        numAgenda = 0;
                    } else {

                    }

                } else {

                    $('li[data_idserver=' + msg[i].CodigoMenuSuperior + ']').show(0, function () { });
                    $('td[data_idserver=' + msg[i].CodigoMenuSuperior + ']').show(0, function () { });
                    $('img[data_idserver=' + msg[i].CodigoMenuSuperior + ']').show(0, function () { });
                    $('div[data_idserver=' + msg[i].CodigoMenuSuperior + ']').show(0, function () { });
                    $('button[data_idserver=' + msg[i].CodigoMenuSuperior + ']').show(0, function () { });

                }
            }

        }
    });
}

function validarCamposCadena(e) { // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    patron = /[A-Za-z.\s\w\ñ\Ñ]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}

function uspValidarUsuarioIngresadoReagendar() {
    $('button[type="button"]').attr("disabled", true);
    var VendedorGrillaRenovReins = $("#lblInfDesVendedor").html();
    var Clave = $('#txtValidarClaveReagendar').val();

    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + VendedorGrillaRenovReins + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.ValidarExisteVendedorActivo > 0) {
                if (msg.ValidacionUsuario > 0) {
                    GuardarAgendaSeguimientoReagendarTodos();
                } else {
                    $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
                }

            } else {
                GuardarAgendaSeguimientoReagendarTodos();
            }

        }, complete: function () {

            $('button[type="button"]').attr("disabled", false);

        }
    });
}

function uspValidarUsuarioIngresadoAgendarInactivo() {
    $('button[type="button"]').attr("disabled", true);
    var VendedorGrillaRenovReins = $('#dllVendedorAgendaInactivo').data("kendoDropDownList").value();//$("#lblInfDesVendedor").html();
    var Clave = $('#txtValidarClaveAgendarInactivo').val();
    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + VendedorGrillaRenovReins + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.ValidarExisteVendedorActivo > 0) {
                if (msg.ValidacionUsuario > 0) {
                    GuardarAgendaSeguimientoAgendarInactivos();
                } else {
                    $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
                }
            } else {
                GuardarAgendaSeguimientoAgendarInactivos();
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function uspValidarUsuarioIngresadoAgendarRenovaciones() {

    $('button[type="button"]').attr("disabled", true);
    var VendedorGrillaRenovReins = $('#dllVendedorAgendaRenovaciones').data("kendoDropDownList").value();//$("#hdVendedorClienteRenovaciones").val();
    var Clave = $('#txtValidarClaveAgendarRenovaciones').val();

    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + VendedorGrillaRenovReins + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.ValidarExisteVendedorActivo > 0) {
                if (msg.ValidacionUsuario > 0) {
                    GuardarAgendaSeguimientoAgendarRenovaciones();
                } else {
                    $.bootstrapGrowl("la clave del vendedor no es correcta .", { type: 'danger', width: 'auto' });
                }
            } else {
                GuardarAgendaSeguimientoAgendarRenovaciones();
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function EnviarClienteASocioAgendaGeneral() {

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var Vendedor = $("#txtConvertirProspectoenCliente_Usuario").data("kendoDropDownList").value();
    var Clave = $('#txtConvertirProspectoenCliente_Clave').val().trim();
    if (Clave == '') {
        $.bootstrapGrowl("Falta ingresar contraseña.", { type: 'danger', width: 'auto' });
        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
        return false;
    }

    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.ValidacionUsuario > 0) {

                var User = $("#lblInfDesVendedor").html();
                var TipoAgenda = $("#hdCodigoTipoAgenda").val();
                if (TipoAgenda == 1) {

                    var CodigoProspecto = $("#hdCodigoSocio").val();
                    $.ajax({
                        data: '{"CodigoProspecto":"' + CodigoProspecto + '","User":"' + User + '"}',
                        type: "POST",
                        url: "/gestionce/uspActualizarProspectoASocio",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg > 0) {
                                $("#lblCodigoClienteEnviadoAgendaGeneral").val(msg);
                                document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'none';
                                document.getElementById('MyModadlDatosClienteEnviadoAgendaGeneral').style.display = 'block';

                            } else {
                                $.bootstrapGrowl("Error, no se ha podido guardar.", { type: 'danger', width: 'auto' });
                            }
                        }, complete: function () {
                            $('button[type="button"]').attr("disabled", false);
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                } else if (TipoAgenda == 4) {
                    var CodigoInvitado = $("#hdCodigoSocio").val();
                    $.ajax({
                        data: '{"CodigoInvitado":"' + CodigoInvitado + '","User":"' + User + '"}',
                        type: "POST",
                        url: "/gestionce/uspActualizarInvitadoASocio",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg > 0) {
                                $("#lblCodigoClienteEnviadoAgendaGeneral").val(msg);
                                document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'none';
                                document.getElementById('MyModadlDatosClienteEnviadoAgendaGeneral').style.display = 'block';

                            } else {
                                $.bootstrapGrowl("Error, no se ha podido guardar.", { type: 'danger', width: 'auto' });
                            }
                        }, complete: function () {
                            $('button[type="button"]').attr("disabled", false);
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                } else if (TipoAgenda == 5) {
                    var CodigoReferido = $("#hdCodigoSocio").val();
                    $.ajax({
                        data: '{"CodigoReferido":"' + CodigoReferido + '","User":"' + User + '"}',
                        type: "POST",
                        url: "/gestionce/uspActualizarReferidoASocio",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg > 0) {
                                $("#lblCodigoClienteEnviadoAgendaGeneral").val(msg);
                                document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'none';
                                document.getElementById('MyModadlDatosClienteEnviadoAgendaGeneral').style.display = 'block';
                            } else {
                                $.bootstrapGrowl("Error, no se ha podido guardar.", { type: 'danger', width: 'auto' });
                            }
                        }, complete: function () {
                            $('button[type="button"]').attr("disabled", false);
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                } else if (TipoAgenda == 6) {
                    var CodigoLlamadaE = $("#hdCodigoSocio").val();
                    $.ajax({
                        data: '{"CodigoLlamadaE":"' + CodigoLlamadaE + '","User":"' + User + '"}',
                        type: "POST",
                        url: "/gestionce/uspActualizarLlamadaEASocio",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg > 0) {
                                $("#lblCodigoClienteEnviadoAgendaGeneral").val(msg);
                                document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'none';
                                document.getElementById('MyModadlDatosClienteEnviadoAgendaGeneral').style.display = 'block';

                            } else {
                                $.bootstrapGrowl("Error, no se ha podido guardar.", { type: 'danger', width: 'auto' });
                            }
                        }, complete: function () {
                            $('button[type="button"]').attr("disabled", false);
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                } else if (TipoAgenda == 7) {
                    var CodigoWeb = $("#hdCodigoSocio").val();
                    $.ajax({
                        data: '{"CodigoLlamadaE":"' + CodigoWeb + '","User":"' + User + '"}',
                        type: "POST",
                        url: "/gestionce/uspActualizarWebASocio",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg > 0) {
                                $("#lblCodigoClienteEnviadoAgendaGeneral").val(msg);
                                document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'none';
                                document.getElementById('MyModadlDatosClienteEnviadoAgendaGeneral').style.display = 'block';

                            } else {
                                $.bootstrapGrowl("Error, no se ha podido guardar.", { type: 'danger', width: 'auto' });
                            }
                        }, complete: function () {
                            $('button[type="button"]').attr("disabled", false);
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                } else {
                    $.bootstrapGrowl("Este cliente ya es socio", { type: 'danger', width: 'auto' });
                }

            } else {
                $('button[type="button"]').attr("disabled", false);
                document.getElementById('loadMe').style.display = 'none';
                $.bootstrapGrowl("la clave no es correcta.", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });


}

function EstaFocus_BuscadorClienteInantivosSinCitas() {
    $("#txtBuscadorCarteraSociosInancitvosSinCita").css("background-color", "#fbf8e1");
}

function SalidaFocus_BuscadorClienteInantivosSinCitas() {
    $("#txtBuscadorCarteraSociosInancitvosSinCita").css("background-color", "White");
}

function EstaFocus_BuscadorClienteAgendaGeneral() {
    $("#txtBuscadorClienteAgendaGeneral").css("background-color", "#fbf8e1");
}

function EstaFocus_BuscadorProspectos_Oportunidades() {
    $("#oportunidades-txtBusquedaProspectos").css("background-color", "#fbf8e1");
}

function SalidaFocus_BuscadorProspectos_Oportunidades() {
    $("#oportunidades-txtBusquedaProspectos").css("background-color", "White");
}

function SalidaFocus_BuscadorClienteAgendaGeneral() {
    $("#txtBuscadorClienteAgendaGeneral").css("background-color", "White");
}

function EstaFocus_BuscadorClienteAgendaGeneral_Matriculados() {
    $("#txtBuscadorClienteAgendaGeneral_Matriculados").css("background-color", "#fbf8e1");
}

function SalidaFocus_BuscadorClienteAgendaGeneral_Matriculados() {
    $("#txtBuscadorClienteAgendaGeneral_Matriculados").css("background-color", "White");
}

function EstaFocus_BuscadorClienteAgendaRenovaciones() {
    $("#txtBuscador").css("background-color", "#fbf8e1");
}
function SalidaFocus_BuscadorClienteAgendaRenovaciones() {
    $("#txtBuscador").css("background-color", "White");
}

function EstaFocus_BuscadorClienteCitasCaidas() {
    $("#txtBuscadorCarteraSocios").css("background-color", "#fbf8e1");
}
function SalidaFocus_BuscadorClienteCitasCaidas() {
    $("#txtBuscadorCarteraSocios").css("background-color", "White");
}

function evento_ReagendarGridAgendaGeneral() {

    var User = getCookie("_Usuario_Business");
    var vendedor = $("#lblInfDesVendedor").html();

    if (User.toUpperCase() == vendedor.toUpperCase()) {
        $('#modalReagendarAgenda').show('fast');
        var todayDate = new Date();
        $("#txtFechaReagendarAgenda").kendoDatePicker();
        $('#txtFechaReagendarAgenda').data("kendoDatePicker").value(todayDate);

    } else {

        $('#ModalAvisoDeuda').show('fast');
        $('#divMensajeAgendar').html('Esta actividad le pertenece a otro vendedor.');
        $('#btnCerrarModalAvisoDeuda').click(function () {
            $('#ModalAvisoDeuda').hide('fast');
        });
    }

    $('#cerrarmodalReagendarAgenda').click(function () {
        $('#modalReagendarAgenda').hide('fast');
    });

}

function evento_EnviarGridGeneralProspectoACliente(vendedor) {

    document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'block';

    $('#btnNoEnviarComoClienteAgendaGeneral').click(function () {
        document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'none';
    });

    $('#DivModalConfEnviarComoClienteAgendaGeneral').click(function () {
        document.getElementById('myModalConfEnviarComoClienteAgendaGeneral').style.display = 'none';
    });

    vendedor = vendedor.toString().replace('/', '').replace('/', '');
    listaAsesoresVentas_ConvertirProspectoenCliente(vendedor);

}

function evento_EnviarGridGeneralVentaCerrada(Codigo, CodigoSocio, CodigoTipoAgenda, Vendedor) {
    $('#hdConfirmarVenta_Codigo').val(Codigo);
    $('#hdConfirmarVenta_CodigoSocio').val(CodigoSocio);
    $('#hdConfirmarVenta_CodigoTipoAgenda').val(CodigoTipoAgenda);
    document.getElementById('myModalConfirmarVenta').style.display = 'block';
    $('#btnCerarmyModalConfirmarVenta,#btnNo_ConfirmarVenta').click(function () {
        document.getElementById('myModalConfirmarVenta').style.display = 'none';
    });

    Vendedor = Vendedor.toString().replace('/', '').replace('/', '');
    listaAsesoresVentas_ConvertirActividadenVenta(Vendedor);
    //var User = "";
    //var vendedor = $("#lblInfDesVendedor").html();

    //if (User.toUpperCase() == vendedor.toUpperCase()) {

    //} else {
    //    $('#ModalAvisoDeuda').show('fast');
    //    $('#btnCerrarModalAvisoDeuda').click(function () {
    //        $('#ModalAvisoDeuda').hide('fast');
    //    });
    //}

}

function event_confirmarVentaCliente() {

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var Vendedor = $("#txtConfirmarVenta_Usuario").data("kendoDropDownList").value();
    var Clave = $('#txtConfirmarVenta_Clave').val().trim();
    if (Clave == '') {
        $.bootstrapGrowl("Falta ingresar contraseña.", { type: 'danger', width: 'auto' });
        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
        return false;
    }
    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.ValidacionUsuario > 0) {

                var Codigo = $('#hdConfirmarVenta_Codigo').val();
                var CodigoSocio = $('#hdConfirmarVenta_CodigoSocio').val();
                var CodigoTipoAgenda = $('#hdConfirmarVenta_CodigoTipoAgenda').val();
                var User = '';
                $.ajax({
                    data: '{"CodigoCita":"' + Codigo + '","CodigoCliente":"' + CodigoSocio + '","Tipo":"' + CodigoTipoAgenda + '","User":"' + User + '"}',
                    type: "POST",
                    url: "/gestionce/uspCerrarCitaClienteAgenda",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $.bootstrapGrowl("Esta cita se marco como vendido correctamente, ahora debes realizar su matricula...", { type: 'success', width: 'auto' });
                        document.getElementById('myModalConfirmarVenta').style.display = 'none';
                    }, complete: function () {
                        $('button[type="button"]').attr("disabled", false);
                        document.getElementById('loadMe').style.display = 'none';
                        ListarGridAgendaGeneral();
                    }
                });

            } else {
                $('button[type="button"]').attr("disabled", false);
                document.getElementById('loadMe').style.display = 'none';
                $.bootstrapGrowl("la clave  no es correcta .", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function ClickObtenerOrigen_Prospecto(tipo) {

    $('#hdCodigoOrigen_Prospecto').val(tipo);
    if (tipo == 1) {
        document.getElementById("btnNuevoAgendaDatos").click();
    } else if (tipo == 4) {
        document.getElementById("btnNuevoInvitado").click();
    } else if (tipo == 5) {
        document.getElementById("btnNuevoReferido").click();
    } else if (tipo == 6) {
        document.getElementById("btnNuevoLlamadaE").click();
    }
}

function ClickObtenerOrigen(tipo) {
    $('#hdCodigoOrigen').val(tipo);

    if (tipo == 10) {
        $('#divIngresarProspectos').show('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasPendientes').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divRenovaciones').hide('fast');

        $('#divAuditoria').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
    } else if (tipo == 15) {

        $('#hdCodigoOrigen').val('10');
        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').show('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
        if ($('#flagView_ProspectosSinActividad').val() == 0) {
            $('#flagView_ProspectosSinActividad').val("1");
            uspListarMetricas_ConversionLeads_Totales();
        }
    } else if (tipo == 20) {
        $('#divCitasPendientes').show('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
    } else if (tipo == 30) {
        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').show('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
        if ($('#hdFlagPrimeraVezClickModalClientesReciclados').val() == 0) {
            $('#hdFlagPrimeraVezClickModalClientesReciclados').val("1");
            document.getElementById('btnListarClientesReciclados').click();
            listaVendedoresIAgendaCaida();
        }
    } else if (tipo == 40) {
        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divRepartirInactivos').show('fast');
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
    } else if (tipo == 50) {

            var todayDate = new Date();
            var Primerdia = new Date(todayDate.getFullYear(), todayDate.getMonth(), 1);
            var ultimoDia = new Date(todayDate.getFullYear(), todayDate.getMonth() + 1, 0);
        $('#txtFechaInicioFiltro').data("kendoDatePicker").value(Primerdia);
        $('#txtFechaFinFiltro').data("kendoDatePicker").value(ultimoDia);

        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divRenovaciones').show('fast');
        $('#flagVentanaHistorialAgenda').val("2");
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
    } else if (tipo == 60) {

        var todayDate = new Date();
        var Primerdia = new Date(todayDate.getFullYear(), todayDate.getMonth(), 1);
        var ultimoDia = new Date(todayDate.getFullYear(), todayDate.getMonth() + 1, 0);
        $('#txtFechaInicioFiltro').data("kendoDatePicker").value(Primerdia);
        $('#txtFechaFinFiltro').data("kendoDatePicker").value(ultimoDia);

        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
        if ($('#hd1erclickMenuMatriculados').val() == 0) {
            $('#hd1erclickMenuMatriculados').val('1')
            uspListarMatriculadorAgendaComercial_paginacion();
        }

    } else if (tipo == 70) {
        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divAuditoria').show('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').hide('fast');
        if ($('#hdFlagCargarModuloAuditoria').val() == '0') {
            $('#hdFlagCargarModuloAuditoria').val('1');
            listardllCreadoPor_Auditoria();
        }


    } else if (tipo == 80) { //AJUSTES
        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divNegocios').hide('fast');
        $('#divAjustes').show('fast');


    } else if (tipo == 90) { //NEGOCIOS

        $('#divCitasPendientes').hide('fast');
        $('#divProspectosSinCita').hide('fast');
        $('#divCitasCaidas').hide('fast');
        $('#divRenovaciones').hide('fast');
        $('#divRepartirInactivos').hide('fast');
        $('#divAuditoria').hide('fast');
        $('#divIngresarProspectos').hide('fast');
        $('#divAjustes').hide('fast');
        $('#divNegocios').show('fast');

    }
}


function uspListarClientesInactivos() {

    var CodigoTiempoMenbresia = $('#hdddlInactivosTiempoMembresiaPaqueteBuscador').val();
    var Genero = 'MF';
    var EdadRango1 = 0;
    var EdadRango2 = 100;
    var EstadoDeuda = 3;
    var EstadoAsistencia = 'Todos';
    var Ubicaciones = 0;
    var AsesorComercial = $('#ddlUsuarioCreadorClientesSinCita').data('kendoDropDownList').value() == 'Todos' ? 'Ninguno' : $('#ddlUsuarioCreadorClientesSinCita').data('kendoDropDownList').value();
    var Nombre = $('#txtBuscadorCarteraSociosInancitvosSinCita').val();
    var Apellidos = '';
    var CodigoS = 0;
    var DNI = '';
    var Telefono = '';
    var Celular = '';
    var FechaInicio = kendo.toString($("#txtFechaDesde_RepartirInacctivos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFechaHasta_RepartirInacctivos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var CheckTodos = 0;
    var PageNumber = 1;

    document.getElementById('loadMe').style.display = 'block';
    $("#gvClientesInactivosRepartir").empty();
    $("#gvClientesInactivosRepartir").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
                            '","EstadoAsistencia":"' + EstadoAsistencia + '","Ubicaciones":"' + Ubicaciones + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
                            '","Apellidos":"' + Apellidos + '","CodigoS":"' + CodigoS + '","DNI":"' + DNI + '","Telefono":"' + Telefono + '","Celular":"' + Celular + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","CheckTodos":"' + CheckTodos + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarClientesInactivos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                            uspListarClientesInactivos_NumeroRegistros(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, Ubicaciones, AsesorComercial, Nombre, Apellidos, CodigoS, DNI, Telefono, Celular, FechaInicio, FechaFin, CheckTodos);
                            document.getElementById('loadMe').style.display = 'none';

                            $("#gvClientesInactivosRepartir tbody").on("dblclick", "tr", function (e) {

                                evento_AgendarClientesInactivos();

                            });

                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 750,
        columns: [{
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CODIGO</center>",
            width: 4,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombres",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>NOMBRES</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>APELLIDOS</center>",
            width: 11,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "NroIngresoActual",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CHECK</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "DNI",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>DNI</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:18px;cursor:pointer;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 2,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CELULAR</center>",
            width: 6,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "desTiempoPaquete",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>PLAN</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "DesFechaInicio",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA INICIO</center>",
            width: 4,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "DesFechaFin",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA FIN</center>",
            width: 4,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>VENDEDOR</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "VendedorRepartido",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>REPARTIDO</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "ColorAgenda",
            template: '<center><div title="#: flagReinslibre #" style="border-radius: 20px;font-size:12px;font-weight: bold;color: rgb(255,255,255);background-color:#: ColorAgenda #;font-weight:500">#: EstadoCliente #</div></center>',
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>ESTADO</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }],
        dataBound: function (e) {

            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
            $('#hdCodigoTipoAgenda').val("3");

        },
        change: function (e) {

            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));

                $('#hdCodigoTipoAgenda').val("3");
                $('#flagVentanaHistorialAgenda').val("3");
                $('#hdCodigoSocio').val(dataItem.CodigoSocio);
                $('#hdVendedorClienteInactivo').val(dataItem.VendedorRepartido);

                $('#hdflagReinslibre').val(dataItem.flagReinslibre);
                $('#lblInfNombre_Inactivos').html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $('#lblInfNombre_Observaciones').html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $('#txtValor_Inactivos').val(dataItem.Costo);
                $('#lblVendedor_Inactivos').html(dataItem.VendedorRepartido);


                $('#imginfoCelular_Inactivos').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Inactivos').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Inactivos').attr('href', 'https://api.whatsapp.com/');
                }

                $('#imginfoCelular_Observaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/');
                }


            });
        }
    });

}

function uspListarClientesInactivos_ChanguePage(PageNumber) {

    var CodigoTiempoMenbresia = $('#hdddlInactivosTiempoMembresiaPaqueteBuscador').val();
    var Genero = 'MF';
    var EdadRango1 = 0;
    var EdadRango2 = 100;
    var EstadoDeuda = 3;
    var EstadoAsistencia = 'Todos';
    var Ubicaciones = 0;

    var AsesorComercial = $('#ddlUsuarioCreadorClientesSinCita').data('kendoDropDownList').value() == 'Todos' ? 'Ninguno' : $('#ddlUsuarioCreadorClientesSinCita').data('kendoDropDownList').value();
    var Nombre = $('#txtBuscadorCarteraSociosInancitvosSinCita').val();
    var Apellidos = '';
    var CodigoS = 0;
    var DNI = '';
    var Telefono = '';
    var Celular = '';
    var FechaInicio = kendo.toString($("#txtFechaDesde_RepartirInacctivos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFechaHasta_RepartirInacctivos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var CheckTodos = 0;
    document.getElementById('loadMe').style.display = 'block';
    $("#gvClientesInactivosRepartir").empty();
    $("#gvClientesInactivosRepartir").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
                            '","EstadoAsistencia":"' + EstadoAsistencia + '","Ubicaciones":"' + Ubicaciones + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
                            '","Apellidos":"' + Apellidos + '","CodigoS":"' + CodigoS + '","DNI":"' + DNI + '","Telefono":"' + Telefono + '","Celular":"' + Celular + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","CheckTodos":"' + CheckTodos + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarClientesInactivos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            $("#gvClientesInactivosRepartir tbody").on("dblclick", "tr", function (e) {

                                evento_AgendarClientesInactivos();

                            });
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 750,
        columns: [{
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CODIGO</center>",
            width: 4,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombres",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>NOMBRES</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>APELLIDOS</center>",
            width: 11,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "NroIngresoActual",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CHECK</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "DNI",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>DNI</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:18px;cursor:pointer;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 2,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>CELULAR</center>",
            width: 6,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "desTiempoPaquete",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>PLAN</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "DesFechaInicio",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA INICIO</center>",
            width: 4,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "DesFechaFin",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>FECHA FIN</center>",
            width: 4,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>VENDEDOR</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "VendedorRepartido",
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>REPARTIDO</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: uppercase;"
            }
        }, {
            field: "ColorAgenda",
            template: '<center><div title="#: flagReinslibre #" style="border-radius: 20px;font-size:12px;font-weight: bold;color: rgb(255,255,255);background-color:#: ColorAgenda #;font-weight:500">#: EstadoCliente #</div></center>',
            title: "<center style='color:#fff;font-size:12px;font-weight: bold;'>ESTADO</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        },
        change: function (e) {

            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));

                $('#hdCodigoTipoAgenda').val("3");
                $('#flagVentanaHistorialAgenda').val("3");
                $('#hdCodigoSocio').val(dataItem.CodigoSocio);
                $('#hdVendedorClienteInactivo').val(dataItem.VendedorRepartido);
                $('#lblVendedor_Inactivos').html(dataItem.VendedorRepartido);

                $('#hdflagReinslibre').val(dataItem.flagReinslibre);
                $('#lblInfNombre_Inactivos').html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $('#lblInfNombre_Observaciones').html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $('#txtValor_Inactivos').val(dataItem.Costo);

                $('#imginfoCelular_Inactivos').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Inactivos').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Inactivos').attr('href', 'https://api.whatsapp.com/');
                }

                $('#imginfoCelular_Observaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/');
                }

            });
        }
    });

}

function uspListarClientesInactivos_NumeroRegistros(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, Ubicaciones, AsesorComercial, Nombre,
    Apellidos, CodigoS, DNI, Telefono, Celular, FechaInicio, FechaFin, CheckTodos) {
    CodigoTiempoMenbresia = "" ? 0 : CodigoTiempoMenbresia;

    $.ajax({
        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
            '","EstadoAsistencia":"' + EstadoAsistencia + '","Ubicaciones":"' + Ubicaciones + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
            '","Apellidos":"' + Apellidos + '","CodigoS":"' + CodigoS + '","DNI":"' + DNI + '","Telefono":"' + Telefono + '","Celular":"' + Celular + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","CheckTodos":"' + CheckTodos + '"}',
        type: "POST",
        url: "/gestionce/uspListarClientesInactivos_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadTotalClientes').html(msg.CantidadTotalFilas);
            $('#lblCantidadVendedoresActivos').html(msg.CantidadVendedoresActivos);
            $('#lblCantidadRepartidoInactivosPorVendedor').html(msg.CantidadRepartidoInactivosPorVendedor);
            //if (msg.CantidadTotalFilas > 0 && msg.CantidadVendedoresActivos > 0) {
            //    $('#btnrepartirInactivosSinCita').html('Repartir ' + msg.CantidadRepartidoInactivosPorVendedor + ' aprox por vendedor');
            //    $('#btnrepartirInactivosSinCita').prop('disabled', false);

            //} else {
            //    $('#btnrepartirInactivosSinCita').html('Repartir');
            //    $('#btnrepartirInactivosSinCita').prop('disabled', true);
            //}
            ddlListaPaginas(msg.CantidadTotalFilas, msg.TamanioPagina);
        }, complete: function () {

        }
    });

}

function ddlListaPaginas(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionCLientes').html(htmlOpcion);
    $("#ddlPaginacionCLientes").kendoDropDownList({
        change: function () {
            var PageNumber = $("#ddlPaginacionCLientes").data('kendoDropDownList').value();
            uspListarClientesInactivos_ChanguePage(PageNumber);
        }
    });
}

function uspAsignarClienteInactivosSinCitaAVendedores() {

    var FechaInicio = kendo.toString($("#txtFechaDesde_ConfigRepartir").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFechaHasta_ConfigRepartir").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var flagRepartirInactivos = $("#chkRepartirInactivos").is(':checked');
    var flagRepartirRenovaciones = $("#chkRepartirRenovaciones").is(':checked');
    var flagRepartirProspectosSinActividadVendedoresInactivos = $("#chkRepartirProspectosSinActividadVendedoresInactivos").is(':checked');
    var flagRepartirProspectosSinActividadVendedoresActivos = $("#chkRepartirProspectosSinActividadVendedoresActivos").is(':checked');

    var flagRepartirEquitativamenteSegunMeta = true;
    document.getElementById('loadMe').style.display = 'block';
    document.getElementById('divConfirmarRepartirInactivos').style.display = 'none';

    $.ajax({
        data: '{"flagRepartirEquitativamenteSegunMeta":"' + flagRepartirEquitativamenteSegunMeta + '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","flagRepartirInactivos":"' + flagRepartirInactivos + '","flagRepartirRenovaciones":"' + flagRepartirRenovaciones + '","flagRepartirProspectosSinActividadVendedoresInactivos":"' + flagRepartirProspectosSinActividadVendedoresInactivos + '","flagRepartirProspectosSinActividadVendedoresActivos":"' + flagRepartirProspectosSinActividadVendedoresActivos + '"}',
        type: "POST",
        url: "/gestionce/uspAsignarClienteInactivosSinCitaAVendedores",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $.bootstrapGrowl("Se repartio correctamente.", { type: 'success', width: 'auto' });
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function PreguntarRepartirInactivos() {
    document.getElementById('divConfirmarRepartirInactivos').style.display = 'block';

    $('#btnCerrardivConfirmarRepartirInactivos').click(function () {
        document.getElementById('divConfirmarRepartirInactivos').style.display = 'none';
    });
    $('#btnNoConfirmarRepartirInactivos').click(function () {
        document.getElementById('divConfirmarRepartirInactivos').style.display = 'none';
    });

    listaAsesoresVentas_RepartirInactivos();
}

function uspValidarUsuarioIngresadoDeGasto_RepartirInactivos() {

    var Vendedor = $("#txtConfirmarRepartirInactivos_Usuario").data("kendoDropDownList").value();
    var Clave = $('#txtConfirmarRepartirInactivos_Clave').val().trim();

    $.ajax({
        data: '{"VendedorGrillaRenovReins":"' + Vendedor + '","Clave":"' + Clave + '"}',
        type: "POST",
        url: "/gestionce/uspValidarUsuarioIngresado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.ValidacionUsuario > 0) {
                uspAsignarClienteInactivosSinCitaAVendedores();
            } else {

                $.bootstrapGrowl("la clave del responsable no es correcta .", { type: 'danger', width: 'auto' });
            }

        }, complete: function () {

        }
    });
}

function listaAsesoresVentas_RepartirInactivos() {
    document.getElementById('loadMe').style.display = 'block';
    $("#txtConfirmarRepartirInactivos_Usuario").kendoDropDownList({
        filter: "startswith",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="txtConfirmarRepartirInactivos_Usuario_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsable_RepartirInactivos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            document.getElementById('loadMe').style.display = 'none';
                            //$('#txtConfirmarRepartirInactivos_Usuario').data("kendoDropDownList").value(User);
                        }
                    });
                }
            }
        }
    });

}

function listaAsesoresVentas_ConvertirActividadenVenta(Vendedor) {

    document.getElementById('loadMe').style.display = 'block';
    $("#txtConfirmarVenta_Usuario").kendoDropDownList({
        filter: "startswith",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    var nombre = $('input[aria-owns="txtConfirmarVenta_Usuario_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '","vendedor":"' + Vendedor + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsable_ConvertirActividadVenta",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                            var User = getCookie("_Usuario_Business");
                            $('#txtConfirmarVenta_Usuario').data("kendoDropDownList").value(User);
                        }
                    });
                }
            }
        }
    });

}

function listaAsesoresVentas_ConvertirProspectoenCliente(Vendedor) {

    document.getElementById('loadMe').style.display = 'block';
    $("#txtConvertirProspectoenCliente_Usuario").kendoDropDownList({
        filter: "startswith",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    var nombre = $('input[aria-owns="txtConvertirProspectoenCliente_Usuario_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '","vendedor":"' + Vendedor + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsable_ConvertirActividadVenta",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                            var User = getCookie("_Usuario_Business");
                            $('#txtConvertirProspectoenCliente_Usuario').data("kendoDropDownList").value(User);
                        }
                    });
                }
            }
        }
    });

}


function evento_AgendarClientesInactivos() {

    var User = getCookie("_Usuario_Business");
    var vendedor = $('#hdVendedorClienteInactivo').val();

    if ($('#hdflagReinslibre').val() == 'yes') { //LIBERADO

        $('#modalAgendaInactivo').show('fast');
        var todayDate = new Date();
        $("#txtFechaAgendaInactivo").kendoDatePicker();
        $('#txtFechaAgendaInactivo').data("kendoDatePicker").value(todayDate);

        var CodigoTipoAgenda = $('#hdCodigoTipoAgenda').val();
        var CodigoSocio = $('#hdCodigoSocio').val();
        event_ListarHistorialActividades_Inactivos(CodigoSocio, CodigoTipoAgenda);
        event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_vencidos(CodigoTipoAgenda, CodigoSocio);

    } else { //OCUPADO

        if (User.toString().toUpperCase().trim() == vendedor.toString().toUpperCase().trim()) {

            $('#modalAgendaInactivo').show('fast');
            var todayDate = new Date();
            $("#txtFechaAgendaInactivo").kendoDatePicker();
            $('#txtFechaAgendaInactivo').data("kendoDatePicker").value(todayDate);

            var CodigoTipoAgenda = $('#hdCodigoTipoAgenda').val();
            var CodigoSocio = $('#hdCodigoSocio').val();
            event_ListarHistorialActividades_Inactivos(CodigoSocio, CodigoTipoAgenda);
            event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_vencidos(CodigoTipoAgenda, CodigoSocio);

        } else {

            $('#modalHistorialAgendaObservaciones').show('fast');
            var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
            var CodigoSocio = $("#hdCodigoSocio").val();
            event_ListarHistorialActividades_Observaciones(CodigoSocio, CodigoTipoAgenda);
            $('#divMensajeAgendar').html('Este cliente le pertenece a otro vendedor.');
        }

    }

    $('#cerrarmodalAgendaInactivo').click(function () {
        $('#modalAgendaInactivo').hide('fast');
    });

    $('#divCerrarmodalHistorialAgendaObservaciones').click(function () {
        $('#modalHistorialAgendaObservaciones').hide('fast');
    });
}

function evento_AgendarClientesRenovaciones() {

    var User = getCookie("_Usuario_Business");
    var vendedor = $('#hdVendedorClienteRenovaciones').val();

    if ($('#hdflagReinslibre').val() == 'yes' && $('#hdflagTieneCitaRenovacion').val() == 'Agendado') {

        $('#modalAgendaRenovaciones').show('fast');

        var todayDate = new Date();
        $("#txtFechaAgendaRenovaciones").kendoDatePicker();
        $('#txtFechaAgendaRenovaciones').data("kendoDatePicker").value(todayDate);

    } else if ($('#hdflagReinslibre').val() == 'yes' && $('#hdflagTieneCitaRenovacion').val() == 'Sin cita') {

        $('#modalAgendaRenovaciones').show('fast');
        var todayDate = new Date();
        $("#txtFechaAgendaRenovaciones").kendoDatePicker();
        $('#txtFechaAgendaRenovaciones').data("kendoDatePicker").value(todayDate);

        var CodigoTipoAgenda = $('#hdCodigoTipoAgenda').val();
        var CodigoSocio = $('#hdCodigoSocio').val();

        event_ListarHistorialActividades_Renovaciones(CodigoSocio, CodigoTipoAgenda);
        event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_renovaciones(CodigoTipoAgenda, CodigoSocio);

    } else {

        if (User.toString().toUpperCase().trim() == vendedor.toString().toUpperCase().trim()) {

            $('#modalAgendaRenovaciones').show('fast');
            var todayDate = new Date();
            $("#txtFechaAgendaRenovaciones").kendoDatePicker();
            $('#txtFechaAgendaRenovaciones').data("kendoDatePicker").value(todayDate);

            var CodigoTipoAgenda = $('#hdCodigoTipoAgenda').val();
            var CodigoSocio = $('#hdCodigoSocio').val();
            event_ListarHistorialActividades_Renovaciones(CodigoSocio, CodigoTipoAgenda);
            event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_renovaciones(CodigoTipoAgenda, CodigoSocio);

        } else {

            $('#modalHistorialAgendaObservaciones').show('fast');
            var CodigoTipoAgenda = $("#hdCodigoTipoAgenda").val();
            var CodigoSocio = $("#hdCodigoSocio").val();
            event_ListarHistorialActividades_Observaciones(CodigoSocio, CodigoTipoAgenda);
            $('#divMensajeAgendar').html('Este cliente le pertenece a otro vendedor.');

        }

    }

    $('#cerrarmodalAgendaRenovaciones').click(function () {
        $('#modalAgendaRenovaciones').hide('fast');
    });

    $('#divCerrarmodalHistorialAgendaObservaciones').click(function () {
        $('#modalHistorialAgendaObservaciones').hide('fast');
    });

}

function listadllVendedoresAgendarInactivos() {

    $("#dllVendedorAgendaInactivo").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarAsesoresComerciales",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            var User = getCookie("_Usuario_Business");
                            $('#dllVendedorAgendaInactivo').data("kendoDropDownList").value(User.toString().trim());

                        }
                    });
                }
            }
        }
    });

}

function listaVendedoresFiltro() {

    $("#ddlVendedorFiltro").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarAsesorVentas",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                            //$("#ddlVendedorFiltro").data("kendoDropDownList").value(UsuarioCreacion);
                            TiempoMembresiaPaqueteBuscador();
                        }
                    });
                }
            }
        }, change: function () {

        }
    });
}

function TiempoMembresiaPaqueteBuscador() {
    var ddlTiempoMembresiaPaqueteBuscador = $("#ddlTiempoMembresiaPaqueteBuscador").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccione Tiempo",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="ddlTiempoMembresiaPaqueteBuscador_listbox"]').val() == 'undefined' ? '' : $('input[aria-owns="ddlTiempoMembresiaPaqueteBuscador_listbox"]').val();
                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            //$('#btnFiltroBusqueda').click();
                        }
                    });
                }
            }
        }, change: function () {
            var codigoTiempoMenbresia = ddlTiempoMembresiaPaqueteBuscador.value();
            $('#hdddlTiempoMembresiaPaqueteBuscador').val(codigoTiempoMenbresia);
        }
    }).data("kendoDropDownList");
}

function TiempoMembresiaInactivosPaqueteBuscador() {
    var ddlTiempoInactivosMembresiaPaqueteBuscador = $("#ddlTiempoInactivosMembresiaPaqueteBuscador").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccione Tiempo",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="ddlTiempoInactivosMembresiaPaqueteBuscador_listbox"]').val() == 'undefined' ? '' : $('input[aria-owns="ddlTiempoInactivosMembresiaPaqueteBuscador_listbox"]').val();
                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            $('#uspListarClientesInactivos').click();
                        }
                    });
                }
            }
        }, change: function () {
            var codigoTiempoMenbresia = ddlTiempoInactivosMembresiaPaqueteBuscador.value();
            $('#hdddlInactivosTiempoMembresiaPaqueteBuscador').val(codigoTiempoMenbresia);
        }
    }).data("kendoDropDownList");
}

//RENOVACIONES
function uspListarClientesAgendaComercialRenovacion(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre,
    FechaInicio, FechaFin, PageNumber) {

    document.getElementById('loadMe').style.display = 'block';
    $("#gvListadoAgendaRenovacion").empty();
    $("#gvListadoAgendaRenovacion").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
                            '","EstadoAsistencia":"' + EstadoAsistencia + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
                            '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarClientesAgendaComercialRenovacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                            uspListarClientesAgendaComercialRenovacion_NumeroRegistros(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre, FechaInicio, FechaFin);
                            document.getElementById('loadMe').style.display = 'none';


                            $("#gvListadoAgendaRenovacion tbody").on("dblclick", "tr", function (e) {

                                evento_AgendarClientesRenovaciones();

                            });


                        }
                    });
                }
            }
        },
        selectable: "row",
        height: 750,
        columns: [{
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CODIGO</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombres",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>NOMBRES</center>",
            width: 17,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>APELLIDOS</center>",
            width: 22,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "NroIngresoActual",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CHECK</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:18px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CELULAR</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Descripcion",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PLAN</center>",
            width: 28,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Costo",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PRECIO</center>",
            width: 6,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaInicio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA INICIO</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaFin",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA FIN</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND ANTERIOS</center>",
            width: 17,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "VendedorRepartido",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND REPARTIDO</center>",
            width: 17,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "desCita",
            template: '<center><div title="#: flagReinslibre #" style="border-radius: 20px;background-color:#: ColorAgenda #;font-size:12px;color: rgb(255,255,255)">#: desCita#</div></center>',
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>ESTADO</center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }],
        dataBound: function (e) {

            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
            $('#hdCodigoTipoAgenda').val("2");

        },
        change: function (e) {

            var grid = this;
            grid.select().each(function () {

                var dataItem = grid.dataItem($(this));

                $('#hdCodigoTipoAgenda').val("2");
                $('#flagVentanaHistorialAgenda').val("2");
                $('#hdCodigoSocio').val(dataItem.CodigoSocio);
                $('#hdVendedorClienteRenovaciones').val(dataItem.VendedorRepartido);

                $('#hdflagReinslibre').val(dataItem.flagReinslibre);
                $('#hdflagTieneCitaRenovacion').val(dataItem.desCita);

                $('#lblInfNombre_Renovaciones').html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $('#lblInfNombre_Observaciones').html(dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $('#txtValor_Renovaciones').val(dataItem.Costo);
                $('#lblVendedor_Renovaciones').html(dataItem.VendedorRepartido);

                $('#imginfoCelular_Renovaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Renovaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Renovaciones').attr('href', 'https://api.whatsapp.com/');
                }

                $('#imginfoCelular_Observaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/');
                }

            });
        }

    });

}

function uspListarClientesAgendaComercialRenovacion_NumeroRegistros(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre,
    FechaInicio, FechaFin) {

    $.ajax({
        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
            '","EstadoAsistencia":"' + EstadoAsistencia + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
            '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/uspListarClientesAgendaComercialRenovacion_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadTotalClientesAgendaRenovacion').html(msg.CantidadTotalFilas);
            ddlPaginacionAgendaRenovacion(msg.CantidadTotalFilas, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function ddlPaginacionAgendaRenovacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionAgendaRenovacion').html(htmlOpcion);
    $("#ddlPaginacionAgendaRenovacion").kendoDropDownList({
        change: function () {

            var ckH = $('#ckHombres').is(':checked');
            var ckM = $('#ckMujeres').is(':checked');
            var Sexo = '';
            if (ckH == true && ckM == true) {
                Sexo = 'MF';
            } else if (ckH == true) {
                Sexo = 'M';
            } else if (ckM == true) {
                Sexo = 'F';
            } else {
                Sexo = 'MF';
            }

            var EdadRango = $('#hdSlinderPrimero').val();
            if (EdadRango == '') {
                var EdadRango = '0,100';
            }
            var EdadRango1 = EdadRango.split(',')[0];
            var EdadRango2 = EdadRango.split(',')[1];
            var EstadoAsistencia = "";
            $('input[name="orderBoxNew[]"]:checked').each(function () {
                EstadoAsistencia += $(this).val();
            });

            var ckD = $('#ckDeudores').is(':checked');
            var ckND = $('#ckNoDeudores').is(':checked');
            var EstadoDeuda = '';
            if (ckD == true && ckND == true) {
                EstadoDeuda = '3';
            } else if (ckD == true) {
                EstadoDeuda = '1';
            } else if (ckND == true) {
                EstadoDeuda = '2';
            } else {
                EstadoDeuda = '3';
            }
            var PageNumber = $("#ddlPaginacionAgendaRenovacion").data('kendoDropDownList').value();
            var CodigoTiempoMenbresia = $('#hdddlTiempoMembresiaPaqueteBuscador').val() == "" ? 0 : $('#hdddlTiempoMembresiaPaqueteBuscador').val();
            var valorNombre = $('#txtBuscador').val();
            var FechaInicio = kendo.toString($("#txtFechaInicioFiltro").data('kendoDatePicker').value(), 'MM/dd/yyyy');
            var FechaFin = kendo.toString($("#txtFechaFinFiltro").data('kendoDatePicker').value(), 'MM/dd/yyyy');
            var VendedorFiltro = $("#ddlVendedorFiltro").data("kendoDropDownList").value();
            document.getElementById('loadMe').style.display = 'block';
            uspListarClientesAgendaComercialRenovacion_ChanguePage(CodigoTiempoMenbresia, Sexo, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, VendedorFiltro,
                valorNombre, FechaInicio, FechaFin, PageNumber);
        }
    });
}

function uspListarClientesAgendaComercialRenovacion_ChanguePage(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre,
    FechaInicio, FechaFin, PageNumber) {

    $("#gvListadoAgendaRenovacion").empty();
    $("#gvListadoAgendaRenovacion").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
                            '","EstadoAsistencia":"' + EstadoAsistencia + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
                            '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarClientesAgendaComercialRenovacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';

                            $("#gvListadoAgendaRenovacion tbody").on("dblclick", "tr", function (e) {

                                evento_AgendarClientesRenovaciones();

                            });

                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 750,
        columns: [{
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CODIGO</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombres",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>NOMBRES</center>",
            width: 17,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>APELLIDOS</center>",
            width: 22,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "NroIngresoActual",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CHECK</center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:18px;cursor:pointer;margin-left: -4px;' /> </a></center>",
            title: "<center style='color:#fff;'></center>",
            width: 5,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CELULAR</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Descripcion",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PLAN</center>",
            width: 28,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Costo",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PRECIO</center>",
            width: 6,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaInicio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA INICIO</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaFin",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA FIN</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND ANTERIOS</center>",
            width: 17,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "VendedorRepartido",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND REPARTIDO</center>",
            width: 17,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "desCita",
            template: '<center><div title="#: flagReinslibre #" style="border-radius: 20px;background-color:#: ColorAgenda #;font-size:12px;color: rgb(255,255,255)">#: desCita#</div></center>',
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>ESTADO</center>",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }],
        dataBound: function (e) {

            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
            $('#hdCodigoTipoAgenda').val("2");

        },
        change: function (e) {

            var grid = this;
            grid.select().each(function () {

                var dataItem = grid.dataItem($(this));

                $('#hdCodigoTipoAgenda').val("2");
                $('#flagVentanaHistorialAgenda').val("2");
                $('#hdCodigoSocio').val(dataItem.CodigoSocio);
                $('#hdVendedorClienteRenovaciones').val(dataItem.VendedorRepartido);

                $('#hdflagReinslibre').val(dataItem.flagReinslibre);
                $('#hdflagTieneCitaRenovacion').val(dataItem.desCita);

                $('#lblInfNombre_Renovaciones').html('(' + dataItem.CodigoSocio + ') ' + dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());
                $('#lblInfNombre_Observaciones').html(dataItem.Nombres.toString().toUpperCase() + ', ' + dataItem.Apellidos.toString().toUpperCase());

                $('#imginfoCelular_Renovaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Renovaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Renovaciones').attr('href', 'https://api.whatsapp.com/');
                }

                $('#imginfoCelular_Observaciones').css('display', dataItem.EstadoCelular);
                if (dataItem.Celular != '') {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/send?phone=' + dataItem.Celular);
                } else {
                    $('#imginfoCelular_Observaciones').attr('href', 'https://api.whatsapp.com/');
                }



            });
        }

    });

}

function uspListarClientesAgendaComercialRenovacionInscritos(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre,
    FechaInicio, FechaFin, PageNumber) {

    document.getElementById('loadMe').style.display = 'block';
    $("#gvListadoAgendaRenovacionInscritos").empty();
    $("#gvListadoAgendaRenovacionInscritos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
                            '","EstadoAsistencia":"' + EstadoAsistencia + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
                            '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarClientesAgendaComercialRenovacionInscritos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                            uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre, FechaInicio, FechaFin);

                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 700,
        columns: [{
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CODIGO</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombres",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>NOMBRES</center>",
            width: 20,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>APELLIDOS</center>",
            width: 25,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Telefono",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>TELEFONO</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CELULAR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Descripcion",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PLAN</center>",
            width: 25,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Costo",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PRECIO</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaInicio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA INICIO</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaFin",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA FIN</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND ANTERIOR</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "VendedorRepartido",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND REPARTIDO</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }],
        dataBound: function (e) {

            var grid = $("#gvListadoAgendaRenovacion").data("kendoGrid");
            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        }, change: function (e) {

            var grid = this;
            grid.select().each(function () {

                var dataItem = grid.dataItem($(this));
                var CodigoMembresia = dataItem.CodigoMembresia;
                var CodigoSocio = dataItem.CodigoSocio;

                $('#hdCodigoSocio').val(dataItem.CodigoSocio);
                $('#hdVendedorClienteRenovaciones').val(dataItem.VendedorRepartido);
                //$("[id*=AgendarClientesRenovaciones_]").hide('fast');
                //$("#AgendarClientesRenovaciones_" + dataItem.CodigoSocio).show('fast');

            });
        }


    });

}

function uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre,
    FechaInicio, FechaFin) {

    $.ajax({
        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
            '","EstadoAsistencia":"' + EstadoAsistencia + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
            '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '"}',
        type: "POST",
        url: "/gestionce/uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadTotalClientesAgendaRenovacionInscritos').html(msg.CantidadTotalFilas);
            ddlPaginacionAgendaRenovacionInscritos(msg.CantidadTotalFilas, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function ddlPaginacionAgendaRenovacionInscritos(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionAgendaRenovacionInscritos').html(htmlOpcion);
    $("#ddlPaginacionAgendaRenovacionInscritos").kendoDropDownList({
        change: function () {

            var ckH = $('#ckHombres').is(':checked');
            var ckM = $('#ckMujeres').is(':checked');
            var Sexo = '';
            if (ckH == true && ckM == true) {
                Sexo = 'MF';
            } else if (ckH == true) {
                Sexo = 'M';
            } else if (ckM == true) {
                Sexo = 'F';
            } else {
                Sexo = 'MF';
            }

            var EdadRango = $('#hdSlinderPrimero').val();
            if (EdadRango == '') {
                var EdadRango = '0,100';
            }
            var EdadRango1 = EdadRango.split(',')[0];
            var EdadRango2 = EdadRango.split(',')[1];
            var EstadoAsistencia = "";
            $('input[name="orderBoxNew[]"]:checked').each(function () {
                EstadoAsistencia += $(this).val();
            });

            var ckD = $('#ckDeudores').is(':checked');
            var ckND = $('#ckNoDeudores').is(':checked');
            var EstadoDeuda = '';
            if (ckD == true && ckND == true) {
                EstadoDeuda = '3';
            } else if (ckD == true) {
                EstadoDeuda = '1';
            } else if (ckND == true) {
                EstadoDeuda = '2';
            } else {
                EstadoDeuda = '3';
            }
            var PageNumber = $("#ddlPaginacionAgendaRenovacionInscritos").data('kendoDropDownList').value();
            var CodigoTiempoMenbresia = $('#hdddlTiempoMembresiaPaqueteBuscador').val() == "" ? 0 : $('#hdddlTiempoMembresiaPaqueteBuscador').val();
            var valorNombre = $('#txtBuscador').val();
            var FechaInicio = kendo.toString($("#txtFechaInicioFiltro").data('kendoDatePicker').value(), 'MM/dd/yyyy');
            var FechaFin = kendo.toString($("#txtFechaFinFiltro").data('kendoDatePicker').value(), 'MM/dd/yyyy');
            var VendedorFiltro = $("#ddlVendedorFiltro").data("kendoDropDownList").value();
            document.getElementById('loadMe').style.display = 'block';

            uspListarClientesAgendaComercialRenovacionInscritos_ChanguePage(CodigoTiempoMenbresia, Sexo, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, VendedorFiltro,
                valorNombre, FechaInicio, FechaFin, PageNumber);
        }
    });
}

function uspListarClientesAgendaComercialRenovacionInscritos_ChanguePage(CodigoTiempoMenbresia, Genero, EdadRango1, EdadRango2, EstadoDeuda, EstadoAsistencia, AsesorComercial, Nombre,
    FechaInicio, FechaFin, PageNumber) {

    $("#gvListadoAgendaRenovacionInscritos").empty();
    $("#gvListadoAgendaRenovacionInscritos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoTiempoMenbresia":"' + CodigoTiempoMenbresia + '","Genero":"' + Genero + '","EdadRango1":"' + EdadRango1 + '","EdadRango2":"' + EdadRango2 + '","EstadoDeuda":"' + EstadoDeuda +
                            '","EstadoAsistencia":"' + EstadoAsistencia + '","AsesorComercial":"' + AsesorComercial + '","Nombre":"' + Nombre +
                            '","FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarClientesAgendaComercialRenovacionInscritos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 700,
        columns: [{
            field: "CodigoSocio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CODIGO</center>",
            width: 7,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Nombres",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>NOMBRES</center>",
            width: 20,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Apellidos",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>APELLIDOS</center>",
            width: 25,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Telefono",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>TELEFONO</center>",
            width: 12,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Celular",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>CELULAR</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Descripcion",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PLAN</center>",
            width: 25,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Costo",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>PRECIO</center>",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaInicio",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA INICIO</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesFechaFin",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>FECHA FIN</center>",
            width: 15,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Vendedor",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND ANTERIOR</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "VendedorRepartido",
            title: "<center style='color:#fff;font-weight:bold;font-size:12px;'>VEND REPARTIDO</center>",
            width: 13,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }],
        dataBound: function (e) {

            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        },
        change: function (e) {

            var grid = this;
            grid.select().each(function () {

                var dataItem = grid.dataItem($(this));

                $('#hdCodigoSocio').val(dataItem.CodigoSocio);
                $('#hdVendedorClienteRenovaciones').val(dataItem.VendedorRepartido);

            });
        }

    });

}



function listadllVendedoresAgendarRenovaciones() {

    $("#dllVendedorAgendaRenovaciones").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarAsesoresComerciales",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            var User = getCookie("_Usuario_Business");
                            $('#dllVendedorAgendaRenovaciones').data("kendoDropDownList").value(User.toString().trim());
                        }
                    });
                }
            }
        }
    });

}


function ActivarEventoSelectChangeRenovacion() {
    $('#flagVentanaHistorialAgenda').val("2");
}
//PROSPECTOS ELIMINADOS
function event_CerrarModal_uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion() {
    $('#myModal_ProspectosEliminados').hide('fast');
}
function uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion() {

    var descripcion = $('#txtBuscardorProspectos_ProspectosEliminados').val();
    var PageNumber = 1;

    document.getElementById('loadMe').style.display = 'block';
    $("#gridProspectosEliminados").empty();
    $("#gridProspectosEliminados").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"descripcion":"' + descripcion +
                            '","PageNumber":"' + PageNumber + '"}',
                        url: "/gestionce/uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            options.success(msg);

                        }, complete: function () {
                            $('#myModal_ProspectosEliminados').show('fast');
                            uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro();
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 315,
        columns: [
            {
                field: "CodigoProspecto",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Cod. prospecto</center>",
                width: 7,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Nombres",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Nombres</center>",
                width: 8,
                attributes: {
                    style: "font-size:10px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "Apellidos",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Apellidos</center>",
                width: 12,
                attributes: {
                    style: "font-size:10px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "DNI",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>DNI</center>",
                width: 6,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Celular",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>Celular</center>",
                width: 8,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Vendedor",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Vendedor</center>",
                width: 8,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "CodigoSocio",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Codigo Socio</center>",
                width: 6,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Observacion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Observacion</center>",
                width: 9,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Creado</center>",
                width: 9,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "DescFechaCreacion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Creado</center>",
                width: 10,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "UsuarioEdicion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Modificado</center>",
                width: 7,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "DescFechaEdicion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Modificado</center>",
                width: 10,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }]
        ,
        dataBound: function (e) {
            var grid = $("#gridProspectosEliminados").data("kendoGrid");
            this.select(this.tbody.find('>tr:first'));
        }
    });

}

function uspListarProspectosHistorialEliminadosEnviadosACliente_ChanguePage(PageNumber) {

    var descripcion = $('#txtBuscardorProspectos_ProspectosEliminados').val();

    document.getElementById('loadMe').style.display = 'block';
    $("#gridProspectosEliminados").empty();
    $("#gridProspectosEliminados").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"descripcion":"' + descripcion +
                            '","PageNumber":"' + PageNumber + '"}',
                        url: "/gestionce/uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            options.success(msg);

                        }, complete: function () {
                            $('#myModal_ProspectosEliminados').show('fast');
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 315,
        columns: [
            {
                field: "CodigoProspecto",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Cod. prospecto</center>",
                width: 7,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Nombres",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Nombres</center>",
                width: 8,
                attributes: {
                    style: "font-size:10px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "Apellidos",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Apellidos</center>",
                width: 12,
                attributes: {
                    style: "font-size:10px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "DNI",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>DNI</center>",
                width: 6,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Celular",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>Celular</center>",
                width: 8,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Vendedor",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Vendedor</center>",
                width: 8,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "CodigoSocio",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Codigo Socio</center>",
                width: 6,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "Observacion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Observacion</center>",
                width: 9,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Creado</center>",
                width: 9,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "DescFechaCreacion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Creado</center>",
                width: 10,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "UsuarioEdicion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Modificado</center>",
                width: 7,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }, {
                field: "DescFechaEdicion",
                title: "<center style='color:#fff;font-size: 10px;font-weight: bold;'>Modificado</center>",
                width: 10,
                attributes: {
                    style: "font-size:10px;text-align:center;"
                }
            }]
        ,
        dataBound: function (e) {
            var grid = $("#gridProspectosEliminados").data("kendoGrid");
            this.select(this.tbody.find('>tr:first'));
        }
    });

}

function uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro() {

    var descripcion = $('#txtBuscardorProspectos_ProspectosEliminados').val();
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"descripcion":"' + descripcion + '"}',
        type: "POST",
        url: "/gestionce/uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidad_ProspectosEliminados').html(msg.CantidadTotal);
            ddlListaruspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion(msg.CantidadTotal, msg.TamanioPagina);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function ddlListaruspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion').html(htmlOpcion);
    $("#ddlPaginacionuspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion").data("kendoDropDownList").value();
            uspListarProspectosHistorialEliminadosEnviadosACliente_ChanguePage(nroPagina);
        }
    });
}

//FIN PROSPECTOS ELIMINADOS
//MATRICULADOS
function uspListarMatriculadorAgendaComercial_paginacion() {

    var Nombres = $('#txtBuscadorClienteAgendaGeneral_Matriculados').val();
    var FechaInicio = kendo.toString($("#FechaDesdeCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#FechaHastaCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var Hi = $('#txtHoraInicioMatricula').val();
    var Hf = $('#txtHoraFinMatricula').val();
    var AsesorDeVentas = $("#ddlUsuarioCreador_Matriculados").data("kendoDropDownList").value();

    var CodigoOrigenMatriculados = "";
    $('input[name="gruporbdOrigenMatriculados"]:checked').each(function () {
        CodigoOrigenMatriculados += $(this).val();
    });

    CodigoOrigenMatriculados = CodigoOrigenMatriculados == '' ? 0 : CodigoOrigenMatriculados;
    var CodigoTiempoMenbresia = $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val() == '' ? 0 : $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val();
    var PageNumber = 1;

    document.getElementById('loadMe').style.display = 'block';
    $("#gridMatriculadorAgendaComercial").empty();
    $("#gridMatriculadorAgendaComercial").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"CodigoMebresiaOrigen":"' + CodigoOrigenMatriculados +
                            '","Nombres":"' + Nombres +
                            '","FechaInicio":"' + FechaInicio +
                            '","FechaFin":"' + FechaFin +
                            '","Hi":"' + Hi +
                            '","Hf":"' + Hf +
                            '","CodTiempoMenbresia":"' + CodigoTiempoMenbresia +
                            '","UsuarioCreacion":"' + AsesorDeVentas +
                            '","PageNumber":"' + PageNumber + '"}',
                        url: "/gestionce/uspListarMatriculadorAgendaComercial_paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {

                            $('#txt_CantidadMatriculadosNuevos').html(msg.Paging.OutValue_Int1);
                            $('#txt_CantidadMatriculadosRenovaciones').html(msg.Paging.OutValue_Int2);
                            $('#txt_CantidadMatriculadosReinscripciones').html(msg.Paging.OutValue_Int3);

                            $('#txt_MatriculadosNuevos').html(msg.Paging.OutValue_Deciaml1);
                            $('#txt_MatriculadosRenovaciones').html(msg.Paging.OutValue_Deciaml2);
                            $('#txt_MatriculadosReinscripciones').html(msg.Paging.OutValue_Deciaml3);

                            uspListarMatriculadorAgendaComercial_NumeroRegistros(msg.Paging.TotalRecord, msg.Paging.PageRecords);

                            options.success(msg.List);



                        }, complete: function () {

                            document.getElementById('loadMe').style.display = 'none';
                            uspEstadisticaVentasPorTiempoMembresia_Ventas();

                            $("#gridMatriculadorAgendaComercial tbody").on("dblclick", "tr", function (e) {

                                $('#modalHistorialSeguimientoMatriculados').show('fast');
                                var CodigoTipoAgenda = "2";
                                var CodigoSocio = $("#hdCodigoSocio").val();
                                event_ListarHistorialActividades_Matriculados(CodigoSocio, CodigoTipoAgenda);

                            });

                            $('#divCerrarmodalHistorialSeguimientoMatriculados').click(function () {
                                $('#modalHistorialSeguimientoMatriculados').hide('fast');
                            });


                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 600,
        columns: [
            {
                field: "desOrigenMembresia",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>ORIGEN</center>",
                width: 10,
                attributes: {
                    style: "font-size:11px;text-align:left;"
                }
            }, {
                field: "desTiempoPaquete",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>TIEMPO</center>",
                width: 8,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "CodigoSocio",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>CODIGO</center>",
                width: 7,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Nombres",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>NOMBRES</center>",
                width: 15,
                attributes: {
                    style: "font-size:11px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "Apellidos",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>APELLIDOS</center>",
                width: 15,
                attributes: {
                    style: "font-size:11px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "DNI",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>DNI</center>",
                width: 10,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:16px;cursor:pointer;margin-left: -4px;' /> </a></center>",
                title: "<center style='color:#fff;'></center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Celular",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>CELULAR</center>",
                width: 11,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "FechaCreacion",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>INSCRIPCION</center>",
                width: 13,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd hh:mm:ss'), 'dd/MM/yyyy hh:mm') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "FechaInicio",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>FECHA INICIO</center>",
                width: 10,
                template: "#= kendo.toString(kendo.parseDate(FechaInicio, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "FechaFin",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>FECHA FIN</center>",
                width: 10,
                template: "#= kendo.toString(kendo.parseDate(FechaFin, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Costo",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>PRECIO</center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Pago",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>PAGO</center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Debe",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>DEBE</center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "AsesorComercial",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>VENDEDOR</center>",
                width: 15,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }]
        ,
        dataBound: function (e) {
            var grid = $("#gridMatriculadorAgendaComercial").data("kendoGrid");
            //this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        }, change: function () {
            var grid = this;
            grid.select().each(function (e) {
                var dataItem = grid.dataItem($(this));

                $("#hdCodigoSocio").val(dataItem.CodigoSocio);


            });

        }
    });
}

function uspListarMatriculadorAgendaComercial_NumeroRegistros(TotalRecord, PageRecords) {

    $('#lblCantidadMatriculadosAgendaComercial').html('Cantidad: ' + TotalRecord);
    ddlPaginacionuspListarMatriculadorAgendaComercial_NumeroRegistros(TotalRecord, PageRecords);

    //var Nombres = $('#txtBuscadorClienteAgendaGeneral_Matriculados').val();
    //var FechaInicio = kendo.toString($("#FechaDesdeCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    //var FechaFin = kendo.toString($("#FechaHastaCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    //var Hi = $('#txtHoraInicioMatricula').val();
    //var Hf = $('#txtHoraFinMatricula').val();
    //var AsesorDeVentas = $("#ddlUsuarioCreador_Matriculados").data("kendoDropDownList").value();

    //var CodigoOrigenMatriculados = "";
    //$('input[name="gruporbdOrigenMatriculados"]:checked').each(function () {
    //    CodigoOrigenMatriculados += $(this).val();
    //});

    //CodigoOrigenMatriculados = CodigoOrigenMatriculados == '' ? 0 : CodigoOrigenMatriculados;
    //var CodigoTiempoMenbresia = $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val() == '' ? 0 : $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val();

    //$.ajax({
    //    type: "POST",
    //    data: '{"CodigoMebresiaOrigen":"' + CodigoOrigenMatriculados +
    //        '","Nombres":"' + Nombres +
    //        '","FechaInicio":"' + FechaInicio +
    //        '","FechaFin":"' + FechaFin +
    //        '","Hi":"' + Hi +
    //        '","Hf":"' + Hf +
    //        '","CodTiempoMenbresia":"' + CodigoTiempoMenbresia +
    //        '","UsuarioCreacion":"' + AsesorDeVentas + '"}',
    //    url: "/gestionce/uspListarMatriculadorAgendaComercial_NumeroRegistros",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {

    //        $('#lblCantidadMatriculadosAgendaComercial').html('Cantidad: ' + msg.CantidadRegistros);
    //        ddlPaginacionuspListarMatriculadorAgendaComercial_NumeroRegistros(msg.CantidadRegistros, msg.TamanioPagina);

    //    }, complete: function () {

    //    }
    //});
}

function ddlPaginacionuspListarMatriculadorAgendaComercial_NumeroRegistros(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarMatriculadorAgendaComercial_paginacion').html(htmlOpcion);
    $("#ddlPaginacionuspListarMatriculadorAgendaComercial_paginacion").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarMatriculadorAgendaComercial_paginacion").data("kendoDropDownList").value();

            uspListarMatriculadorAgendaComercial_ChanguePage(nroPagina);

        }
    });
}

function uspListarMatriculadorAgendaComercial_ChanguePage(PageNumber) {
    document.getElementById('loadMe').style.display = 'block';

    var Nombres = $('#txtBuscadorClienteAgendaGeneral_Matriculados').val();
    var FechaInicio = kendo.toString($("#FechaDesdeCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#FechaHastaCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var Hi = $('#txtHoraInicioMatricula').val();
    var Hf = $('#txtHoraFinMatricula').val();
    var AsesorDeVentas = $("#ddlUsuarioCreador_Matriculados").data("kendoDropDownList").value();

    var CodigoOrigenMatriculados = "";
    $('input[name="gruporbdOrigenMatriculados"]:checked').each(function () {
        CodigoOrigenMatriculados += $(this).val();
    });
    CodigoOrigenMatriculados = CodigoOrigenMatriculados == '' ? 0 : CodigoOrigenMatriculados;
    var CodigoTiempoMenbresia = $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val() == '' ? 0 : $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val();

    $("#gridMatriculadorAgendaComercial").empty();
    $("#gridMatriculadorAgendaComercial").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"CodigoMebresiaOrigen":"' + CodigoOrigenMatriculados +
                            '","Nombres":"' + Nombres +
                            '","FechaInicio":"' + FechaInicio +
                            '","FechaFin":"' + FechaFin +
                            '","Hi":"' + Hi +
                            '","Hf":"' + Hf +
                            '","CodTiempoMenbresia":"' + CodigoTiempoMenbresia +
                            '","UsuarioCreacion":"' + AsesorDeVentas +
                            '","PageNumber":"' + PageNumber + '"}',
                        url: "/gestionce/uspListarMatriculadorAgendaComercial_paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        serverPaging: true,
                        serverFiltering: true,
                        success: function (msg) {
                            options.success(msg.List);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        sortable: true,
        height: 600,
        columns: [
            {
                field: "desOrigenMembresia",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>ORIGEN</center>",
                width: 10,
                attributes: {
                    style: "font-size:11px;text-align:left;"
                }
            }, {
                field: "desTiempoPaquete",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>TIEMPO</center>",
                width: 8,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "CodigoSocio",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>CODIGO</center>",
                width: 7,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Nombres",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>NOMBRES</center>",
                width: 15,
                attributes: {
                    style: "font-size:11px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "Apellidos",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>APELLIDOS</center>",
                width: 15,
                attributes: {
                    style: "font-size:11px;text-align:center;text-transform: uppercase;"
                }
            }, {
                field: "DNI",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>DNI</center>",
                width: 10,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                template: "<center><a style='display:#:EstadoCelular#' target='_blank' href='https://api.whatsapp.com/send?phone=#:Celular#'> <img src='/Content/app/img/whatsapp.png' style='height:16px;cursor:pointer;margin-left: -4px;' /> </a></center>",
                title: "<center style='color:#fff;'></center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Celular",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>CELULAR</center>",
                width: 11,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "FechaCreacion",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>INSCRIPCION</center>",
                width: 13,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd hh:mm:ss'), 'dd/MM/yyyy hh:mm') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "FechaInicio",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>FECHA INICIO</center>",
                width: 10,
                template: "#= kendo.toString(kendo.parseDate(FechaInicio, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "FechaFin",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>FECHA FIN</center>",
                width: 10,
                template: "#= kendo.toString(kendo.parseDate(FechaFin, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Costo",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>PRECIO</center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Pago",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>PAGO</center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Debe",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>DEBE</center>",
                width: 5,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "AsesorComercial",
                title: "<center style='color:#fff;font-size: 11px;font-weight: bold;'>VENDEDOR</center>",
                width: 15,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }],
        dataBound: function (e) {
            var grid = $("#gridMatriculadorAgendaComercial").data("kendoGrid");
            //this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        }
    });
}

//exportar Matriculados
function ExportarMatriculados() {
    document.getElementById('loadMe').style.display = 'block';

    var Nombres = "";
    var FechaInicio = kendo.toString($("#FechaDesdeCierre").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var FechaFin = kendo.toString($("#FechaHastaCierre").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var Hi = $('#txtHoraInicioMatricula').val();
    var Hf = $('#txtHoraFinMatricula').val();
    var AsesorDeVentas = $("#ddlUsuarioCreador_Matriculados").data("kendoDropDownList").value();

    var CodigoOrigenMatriculados = "";
    $('input[name="gruporbdOrigenMatriculados"]:checked').each(function () {
        CodigoOrigenMatriculados += $(this).val();
    });

    CodigoOrigenMatriculados = CodigoOrigenMatriculados == '' ? 0 : CodigoOrigenMatriculados;
    var CodigoTiempoMenbresia = $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val() == '' ? 0 : $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val();

    var data = new FormData();
    data.append('Nombres', Nombres);
    data.append('FechaInicio', FechaInicio);
    data.append('FechaFin', FechaFin);
    data.append('Hi', Hi);
    data.append('Hf', Hf);
    data.append('UsuarioCreacion', AsesorDeVentas);
    data.append('CodigoTiempoMenbresia', CodigoTiempoMenbresia);
    data.append('CodigoMebresiaOrigen', CodigoOrigenMatriculados);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'HttpHandler/ExportaruspListarMatriculadorAgendaComercial_paginacion.ashx', true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "InformeMatriculados.xls";
            a.style.display = "none";
            document.body.appendChild(a);
            a.click();
        } else {
            alert('Unable to download excel.')
        }
    };
    xhr.send(data);

    document.getElementById('loadMe').style.display = 'none';
}

//exportar renovaciones
function ExportarTodoRenovaciones() {

    var CodigoUnidadNegocio = getCookie('_CodigoUnidadNegocio_Business');
    var CodigoSede = getCookie('_CodigoSede_Business');
    var CodigoTiempoMenbresia = 0;
    var Genero = 'MF';
    var EdadRango1 = 0;
    var EdadRango2 = 100;
    var EstadoDeuda = '3';
    var EstadoAsistencia = 'Todos';
    var Ubicaciones = 0;
    var TipoUbicaciones = 1;
    var AsesorComercial = 'Ninguno';
    var Nombre = '';
    var Apellidos = '';
    var CodigoS = 0;
    var DNI = '';
    var Telefono = '';
    var Celular = '';
    var FechaInicio = kendo.toString($("#txtFechaInicioFiltro").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var FechaFin = kendo.toString($("#txtFechaFinFiltro").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var CheckTodos = 0;
    var data = new FormData();

    data.append('CodigoUnidadNegocio', CodigoUnidadNegocio);
    data.append('CodigoSede', CodigoSede);
    data.append('CodigoTiempoMenbresia', CodigoTiempoMenbresia);
    data.append('Genero', Genero);
    data.append('EdadRango1', EdadRango1);
    data.append('EdadRango2', EdadRango2);
    data.append('EstadoDeuda', EstadoDeuda);
    data.append('EstadoAsistencia', EstadoAsistencia);
    data.append('Ubicaciones', Ubicaciones);
    data.append('AsesorComercial', AsesorComercial);
    data.append('Nombre', Nombre);
    data.append('Apellidos', Apellidos);
    data.append('CodigoS', CodigoS);
    data.append('DNI', DNI);
    data.append('Telefono', Telefono);
    data.append('Celular', Celular);
    data.append('FechaInicio', FechaInicio);
    data.append('FechaFin', FechaFin);
    data.append('CheckTodos', CheckTodos);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/ExportarExcel/Appsfit/ExportarPostVentaSegmentacionRenovaciones.ashx', true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "InformeSociosRenovaciones.xls";
            a.style.display = "none";
            document.body.appendChild(a);
            a.click();
        } else {
            alert('Unable to download excel.')
        }
    };
    xhr.send(data);
};

//exportar vencidos
function ExportarTodoVencidos() {

    var CodigoUnidadNegocio = getCookie('_CodigoUnidadNegocio_Business');
    var CodigoSede = getCookie('_CodigoSede_Business');
    var CodigoTiempoMenbresia = 0;
    var Genero = 'MF';
    var EdadRango1 = 0;
    var EdadRango2 = 100;
    var EstadoDeuda = '3';
    var EstadoAsistencia = 'Todos';
    var Ubicaciones = 0;
    var TipoUbicaciones = 1;
    var AsesorComercial = 'Ninguno';
    var Nombre = '';
    var Apellidos = '';
    var CodigoS = 0;
    var DNI = '';
    var Telefono = '';
    var Celular = '';
    var FechaInicio = kendo.toString($("#txtFechaDesde_RepartirInacctivos").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var FechaFin = kendo.toString($("#txtFechaHasta_RepartirInacctivos").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var CheckTodos = 0;
    var data = new FormData();

    data.append('CodigoUnidadNegocio', CodigoUnidadNegocio);
    data.append('CodigoSede', CodigoSede);
    data.append('CodigoTiempoMenbresia', CodigoTiempoMenbresia);
    data.append('Genero', Genero);
    data.append('EdadRango1', EdadRango1);
    data.append('EdadRango2', EdadRango2);
    data.append('EstadoDeuda', EstadoDeuda);
    data.append('EstadoAsistencia', EstadoAsistencia);
    data.append('Ubicaciones', Ubicaciones);
    data.append('AsesorComercial', AsesorComercial);
    data.append('Nombre', Nombre);
    data.append('Apellidos', Apellidos);
    data.append('CodigoS', CodigoS);
    data.append('DNI', DNI);
    data.append('Telefono', Telefono);
    data.append('Celular', Celular);
    data.append('FechaInicio', FechaInicio);
    data.append('FechaFin', FechaFin);
    data.append('CheckTodos', CheckTodos);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/ExportarExcel/Appsfit/ExportarPostVentaSegmentacionInactivos.ashx', true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "InformeSociosVencidos.xls";
            a.style.display = "none";
            document.body.appendChild(a);
            a.click();
        } else {
            alert('Unable to download excel.')
        }
    };
    xhr.send(data);
};




//exportar sin seguimiento
function ExportarSinSeguimientos() {

    
    var FechaInicio = kendo.toString($("#FechaDesdeCaida").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var FechaFin = kendo.toString($("#FechaHastaCaida").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var data = new FormData();;
    data.append('FechaInicio', FechaInicio);
    data.append('FechaFin', FechaFin);
   
    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/ExportarExcel/Appsfit/ExportarSinSeguimientoAgendaComercial.ashx', true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "SinSeguimiento.xls";
            a.style.display = "none";
            document.body.appendChild(a);
            a.click();
        } else {
            console.log("--")
            alert('Unable to download excel.')
        }
    };
    xhr.send(data);
};




//exportar Citas pendientes
function ExportarCitasPendientes() {

    document.getElementById('loadMe').style.display = 'block';
    var Buscador = $("#txtBuscadorClienteAgendaGeneral").val();
    var FechaDesde = kendo.toString($("#txtFechaDesde_AgendaGeneral").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var FechaHasta = kendo.toString($("#txtFechaHasta_AgendaGeneral").data('kendoDatePicker').value(), 'yyyy-MM-dd');
    var CodigoTipoAgenda = $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value() == "" ? 0 : $('#ddlTipoAgendaGeneral').data('kendoDropDownList').value();
    var UsuarioCreador = $('#ddlUsuarioCreador').data('kendoDropDownList').value(); //posicion 0 = Todos
    var CodTiempoPaquete = $('#dllTiempoPaquete').data('kendoDropDownList').value() == "" ? 0 : $('#dllTiempoPaquete').data('kendoDropDownList').value();
    var TipoCliente = 0;

    var data = new FormData();
    data.append('Buscador', Buscador);
    data.append('FechaDesde', FechaDesde);
    data.append('FechaHasta', FechaHasta);
    data.append('CodigoTipoAgenda', CodigoTipoAgenda);
    data.append('UsuarioCreador', UsuarioCreador);

    data.append('CodTiempoPaquete', CodTiempoPaquete);
    data.append('TipoCliente', TipoCliente);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'HttpHandler/ExportarReporteCitasPendientes_ExportarExcel.ashx', true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "InformeCitasPendientes.xls";
            a.style.display = "none";
            document.body.appendChild(a);
            a.click();
        } else {
            alert('Unable to download excel.')
        }
    };
    xhr.send(data);

    document.getElementById('loadMe').style.display = 'none';
}

function SEGListarPerfilMenu() {

    var entidad = {};

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';

        for (var i = 0; i < msg.length; i++) {

            if (msg[i].Estado) {
                $('[data-controlmodulo="' + msg[i].CodigoMenu + '"]').show('fast');

            }
        }

        buscarConfiguracion();
        uspUpdateCitasCaidas();
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        //request: entidad
    };
    LlamarAJAX("/gestionce/SEGListarPerfilMenu", request, metodoCorrecto, metodoError);

}

function buscarConfiguracion() {
    //return;
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        type: "POST",
        url: "/gestionce/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#hdCodigoPais').val(msg.Pais);
            if (msg.ConsultasNumeroDocumento_Estado) {

                //$('.ConsultasNumeroDocumento_Activo').show('fast');

                //$('#txtConsultasNumeroDocumento_UsuarioCreacion').val(msg.ConsultasNumeroDocumento_UsuarioCreacion);
                //$('#txtConsultasNumeroDocumento_FechaPago').val(msg.ConsultasNumeroDocumento_FechaPago_Texto);
                //$('#txtConsultasNumeroDocumento_FechaCreacion').val(msg.ConsultasNumeroDocumento_FechaCreacion_Texto);
                //onkeydown = "return validarNumeros(event)"
                //var Origen = $('#hdCodigoOrigen_Prospecto').val();

                //walk in
                $('#txtDni_SocioIAgenda').keydown(function (event) {

                    $('#txtDni_SocioIAgenda').css("border", "1px #00d27a solid");
                    $('#txtDni_SocioIAgenda').css("background-color", "rgb(0 210 122 / 30 %)");
                    if (event.which == 13) {
                        if ($(this).val().length == 8) {
                            $('#txtDni_SocioIAgenda').css("background-color", "#fff");
                            uspConsultarDNIporRENIEC($(this).val());
                        } else {
                            alert("EL NÚMERO DE DOCUMENTO TENER 8 DIGITOS");
                        }

                        event.preventDefault();
                    }

                });
                $('#txtDni_SocioIAgenda').keypress(function (event) {

                    $('#txtDni_SocioIAgenda').css("border", "1px #00d27a solid");
                    $('#txtDni_SocioIAgenda').css("background-color", "rgb(0 210 122 / 30 %)");

                });
                $('#txtDni_SocioIAgenda').keyup(function (event) {

                    $('#txtDni_SocioIAgenda').css("border", "1px #ced4da solid");
                });

                //prospeccion
                $('#txtDniInvitado').keydown(function (event) {

                    $('#txtDniInvitado').css("border", "1px #00d27a solid");
                    $('#txtDniInvitado').css("background-color", "rgb(0 210 122 / 30 %)");
                    if (event.which == 13) {
                        if ($(this).val().length == 8) {
                            $('#txtDniInvitado').css("background-color", "#fff");
                            uspConsultarDNIporRENIEC($(this).val());
                        } else {
                            alert("EL NÚMERO DE DOCUMENTO TENER 8 DIGITOS");
                        }

                        event.preventDefault();
                    }

                });
                $('#txtDniInvitado').keypress(function (event) {

                    $('#txtDniInvitado').css("border", "1px #00d27a solid");
                    $('#txtDniInvitado').css("background-color", "rgb(0 210 122 / 30 %)");

                });
                $('#txtDniInvitado').keyup(function (event) {

                    $('#txtDniInvitado').css("border", "1px #ced4da solid");
                });

                //digital
                $('#txtDniReferido').keydown(function (event) {

                    $('#txtDniReferido').css("border", "1px #00d27a solid");
                    $('#txtDniReferido').css("background-color", "rgb(0 210 122 / 30 %)");
                    if (event.which == 13) {
                        if ($(this).val().length == 8) {
                            $('#txtDniReferido').css("background-color", "#fff");
                            uspConsultarDNIporRENIEC($(this).val());
                        } else {
                            alert("EL NÚMERO DE DOCUMENTO TENER 8 DIGITOS");
                        }

                        event.preventDefault();
                    }

                });
                $('#txtDniReferido').keypress(function (event) {

                    $('#txtDniReferido').css("border", "1px #00d27a solid");
                    $('#txtDniReferido').css("background-color", "rgb(0 210 122 / 30 %)");

                });
                $('#txtDniReferido').keyup(function (event) {

                    $('#txtDniReferido').css("border", "1px #ced4da solid");
                });

                //llamada entrante
                $('#txtDniLlamadaE').keydown(function (event) {

                    $('#txtDniLlamadaE').css("border", "1px #00d27a solid");
                    $('#txtDniLlamadaE').css("background-color", "rgb(0 210 122 / 30 %)");
                    if (event.which == 13) {
                        if ($(this).val().length == 8) {
                            $('#txtDniLlamadaE').css("background-color", "#fff");
                            uspConsultarDNIporRENIEC($(this).val());
                        } else {
                            alert("EL NÚMERO DE DOCUMENTO TENER 8 DIGITOS");
                        }

                        event.preventDefault();
                    }

                });
                $('#txtDniLlamadaE').keypress(function (event) {

                    $('#txtDniLlamadaE').css("border", "1px #00d27a solid");
                    $('#txtDniLlamadaE').css("background-color", "rgb(0 210 122 / 30 %)");

                });
                $('#txtDniLlamadaE').keyup(function (event) {

                    $('#txtDniLlamadaE').css("border", "1px #ced4da solid");
                });

            }

            document.getElementById('loadMe').style.display = 'none';

        }, complete: function () {

        }
    });

}

function uspConsultarDNIporRENIEC(Number) {
    var Origen = $('#hdCodigoOrigen_Prospecto').val();
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"Number":"' + Number + '"}',
        type: "POST",
        url: "/gestionce/ConsultarDNIporRENIEC",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg == undefined || msg == null) {
                alert("LA API DE CONSULTAS DNI DE TERCEROS ESTA TENIENDO PROBLEMAS");
            } else if (msg == "Not Found") {
                alert("NO SE ENCONTRO ESTE NRO DE DOCUMENTO EN LA BASE DE DATOS DE LA API DEL TERCERO.");
                if (Origen == 1) { //walk in
                    $('#txtNombre_SocioIAgenda').val('');
                    $('#txtApellidos_SocioIAgenda').val('');
                    $('#txtDni_SocioIAgenda').css("border", "1px #ced4da solid");
                } else if (Origen == 4) {//prospeccion
                    $('#txtNombreInvitado').val('');
                    $('#txtApellidoInvitado').val('');
                    $('#txtDniInvitado').css("border", "1px #ced4da solid");
                } else if (Origen == 5) {//digital
                    $('#txtNombreReferido').val('');
                    $('#txtApellidosReferido').val('');
                    $('#txtDniReferido').css("border", "1px #ced4da solid");
                } else if (Origen == 6) {//llamada entrante
                    $('#txtNombreLlamadaE').val('');
                    $('#txtApellidosLlamadaE').val('');
                    $('#txtDniLlamadaE').css("border", "1px #ced4da solid");
                }

            }
            else {

                var datos = JSON.parse(msg);

                if (Origen == 1) { //walk in
                    $('#txtNombre_SocioIAgenda').val(datos.nombres);
                    $('#txtApellidos_SocioIAgenda').val(datos.apellidoPaterno + ' ' + datos.apellidoMaterno);
                    $('#txtDni_SocioIAgenda').css("border", "1px #00d27a solid");
                } else if (Origen == 4) {//prospeccion
                    $('#txtNombreInvitado').val(datos.nombres);
                    $('#txtApellidoInvitado').val(datos.apellidoPaterno + ' ' + datos.apellidoMaterno);
                    $('#txtDniInvitado').css("border", "1px #00d27a solid");
                } else if (Origen == 5) {//digital
                    $('#txtNombreReferido').val(datos.nombres);
                    $('#txtApellidosReferido').val(datos.apellidoPaterno + ' ' + datos.apellidoMaterno);
                    $('#txtDniReferido').css("border", "1px #00d27a solid");
                } else if (Origen == 6) {//llamada entrante
                    $('#txtNombreLlamadaE').val(datos.nombres);
                    $('#txtApellidosLlamadaE').val(datos.apellidoPaterno + ' ' + datos.apellidoMaterno);
                    $('#txtDniLlamadaE').css("border", "1px #00d27a solid");
                }

            }

            //alert(msg.Content);
            //alert(msg.StatusCode);
            //msg.Content

            //msg.apellidoPaterno
            //msg.apellidoMaterno

        }, complete: function () {

            document.getElementById('loadMe').style.display = 'none';
            if (Origen == 1) { //walk in
                $('#txtDni_SocioIAgenda').css("border", "1px #ced4da solid");
            } else if (Origen == 4) {//prospeccion
                $('#txtDniInvitado').css("border", "1px #ced4da solid");
            } else if (Origen == 5) {//digital
                $('#txtDniReferido').css("border", "1px #ced4da solid");
            } else if (Origen == 6) {//llamada entrante
                $('#txtDniLlamadaE').css("border", "1px #ced4da solid");
            }

        }
    });

}


function uspUpdateCitasCaidas() {

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        type: "POST",
        url: "/gestionce/uspUpdateCitasCaidas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('button[type="button"]').attr("disabled", false);
            document.getElementById('loadMe').style.display = 'none';

        }, complete: function () {

            $.bootstrapGrowl("Se verifico las citas caidas correctamente.", { type: 'success', width: 'auto' });

        }
    });

}

function event_abrirmodalIntereses() {
    document.getElementById('myModalInteresConfirmarEliminar').style.display = 'none';
    uspListarInteres();
}

function event_cerrarmodalConfirmarEliminarIntereses() {
    document.getElementById('myModalInteresConfirmarEliminar').style.display = 'none';

}

function event_EliminarmodalIntereses(CodigoInteres) {
    $('#hdCodigoInteres').val(CodigoInteres);

    document.getElementById('myModalInteresConfirmarEliminar').style.display = 'block';
}

function event_cerrarmyModal_Prospectossincita() {
    document.getElementById('myModal_Prospectossincita').style.display = 'none';
}

function uspListarInteres() {

    $("#gridInteres").empty();
    $("#gridInteres").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/uspListarInteres",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        },
        height: 300,
        sortable: true,
        columns: [{
            field: "Descripcion",
            title: "Descripcion",
            width: 30,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            template: "<button type='button' style='font-size: 12px;' class='btn btn-primary btn-xs' onclick='event_EliminarmodalIntereses(#: CodigoInteres #)'>Eliminar</button>",
            width: 10
        }]
    });

}

function GuardarInteres() {

    $('button[type="button"]').attr("disabled", true);
    var Descripcion = $('#txtDescripcionInteres').val();

    if (Descripcion == '') {
        alert("Falta ingresar la descripción del interes.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"descripcion":"' + Descripcion + '"}',
        type: "POST",
        url: "/gestionce/GuardarInteres",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg > 0) {
                $.bootstrapGrowl("Los datos se guardarón correctamente.", { type: 'success', width: 'auto' });
                $('#txtDescripcionInteres').val('');
                uspListarInteres();
                uspListarInteresProspectos();
            } else {
                $.bootstrapGrowl("Tenemos problemas para guardar el interes.", { type: 'danger', width: 'auto' });
            }
            document.getElementById('loadMe').style.display = 'none';
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

function ConfirmarEliminarInteres() {
    $('button[type="button"]').attr("disabled", true);

    var CodigoInteres = $('#hdCodigoInteres').val();
    $.ajax({
        data: '{"CodigoInteres":"' + CodigoInteres + '"}',
        type: "POST",
        url: "/gestionce/uspEliminarInteres",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg > 0) {
                $.bootstrapGrowl("El interes se elimino correctamente.", { type: 'success', width: 'auto' });

                document.getElementById('myModalInteresConfirmarEliminar').style.display = 'none';
                uspListarInteres();
                uspListarInteresProspectos();
            } else {
                $.bootstrapGrowl("Tenemos problemas para guardar el interes.", { type: 'danger', width: 'auto' });
            }
        }, complete: function () {
            $('button[type="button"]').attr("disabled", false);
        }
    });
}

//PROSPECTOS SIN CITA
function event_seleccionarProspectoSinCita(CodigoProspecto, CodigoOrigen, colorOrigen, vendedor) {

    $('#hdCodigoProspecto_Prospectossincita').val(CodigoProspecto);
    $('#hdCodigoOrigen_Prospectossincita').val(CodigoOrigen);
    $('#lblVendedor_Prospectossincita').html(vendedor.toString().replace('/', '').replace('/', ''));
    $('#myModal_Prospectossincita_ColorOrigen').css('background-color', colorOrigen.toString().replace('/', '').replace('/', ''));


    event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_prospectosSinCita(CodigoOrigen, CodigoProspecto);

    if ($("#ddlVendedor_ProspectosSinCita").data("kendoDropDownList").value().toString().toUpperCase() == 'OTROSVENDEDORES') {

        $('#hdVendedor_Prospectossincita').val(getCookie("_Usuario_Business"));

        if (CodigoOrigen == 1) { //WALKING
            event_BuscarClientesProspectosPorCodigo__Prospectossincita(CodigoProspecto);
        } else if (CodigoOrigen == 4) { //INVITADOS
            event_BuscarClientesDatosInvitadosPorCodigo__Prospectossincita(CodigoProspecto);
        } else if (CodigoOrigen == 5) { //REFERIDOS
            event_BuscarClientesDatosReferidosPorCodigo__Prospectossincita(CodigoProspecto);
        } else if (CodigoOrigen == 6) { //LLAMADAS ENTRANTES
            event_BuscarClientesDatosLLamadaEPorCodigo__Prospectossincita(CodigoProspecto);
        }

    } else {

        vendedor = vendedor.toString().replace('/', '').replace('/', '');
        $('#hdVendedor_Prospectossincita').val(vendedor);

        if (vendedor.toString().toUpperCase() != getCookie("_Usuario_Business").toString().toUpperCase()) {
            alert("No puedes operar este prospecto porque le pertenece ha " + vendedor);
            return;
        } else {

            if (CodigoOrigen == 1) { //WALKING
                event_BuscarClientesProspectosPorCodigo__Prospectossincita(CodigoProspecto);
            } else if (CodigoOrigen == 4) { //INVITADOS
                event_BuscarClientesDatosInvitadosPorCodigo__Prospectossincita(CodigoProspecto);
            } else if (CodigoOrigen == 5) { //REFERIDOS
                event_BuscarClientesDatosReferidosPorCodigo__Prospectossincita(CodigoProspecto);
            } else if (CodigoOrigen == 6) { //LLAMADAS ENTRANTES
                event_BuscarClientesDatosLLamadaEPorCodigo__Prospectossincita(CodigoProspecto);
            }
        }
    }


}

function event_CerrarModalProspectoSinCita() {
    document.getElementById('myModal_Prospectossincita').style.display = 'none';
    $('#hdCodigoProspecto_Prospectossincita').val('0');
    $('#hdCodigoOrigen_Prospectossincita').val('0');
    $('#hdVendedor_Prospectossincita').val('');
}

function event_uspListar_gimnasio_crm_4_etapahistorial(CodigoEmbudoVenta, CodigoTratoProspecto) {

    var entidad = {};

    entidad.CodigoEmbudoVenta = CodigoEmbudoVenta;
    entidad.CodigoTratoProspecto = CodigoTratoProspecto;

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {

        if (msg) {

            var ControlHtml = '';
            if (msg.length == 0) {
                ControlHtml += '<div class="alert alert-secondary" role="alert" style="font-size: 13px;">¡Ups! Este prospecto no tiene un historial de etapas.</div>';
                $('div[id="divHistorialEtapas_Oportunidades"]').html(ControlHtml);
            } else {
                for (var i = 0; i < msg.length; i++) {

                    ControlHtml += '<div class="comment">';
                    ControlHtml += '     <div class="comment-author-ava"><div style="padding: 11px;border-radius:25px;text-align: center;width:50px;height: 50px;background-color: #0075ff;color:#fff;"> ' + (i + 1) + ' </div></div>';
                    ControlHtml += '     <div class="comment-body">';
                    ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Etapa: ' + msg[i].NombreEtapa + '</span></div>';
                    ControlHtml += '         <div class="comment-footer"><span class="comment-meta">creación: ' + msg[i].DescFechaCreacion + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">responsable: ' + msg[i].UsuarioCreacion + '</span></div>';
                    ControlHtml += '     </div>';
                    ControlHtml += ' </div>';

                }
            }

            $('#divHistorialEtapas_Oportunidades').html(ControlHtml);

        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }

        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_4_etapahistorial", request, metodoCorrecto, metodoError);


}

function event_ListarHistorialActividades_Actividades(CodigoSocio, CodigoTipoAgenda) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
        type: "POST",
        url: "/gestionce/ListarSeguimientoAgenda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var ControlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                ControlHtml += '<div class="comment">';
                ControlHtml += '     <div class="comment-author-ava"><img src="' + msg[i].imgTipoActividad + '" alt="' + msg[i].desActividad + '"></div>';
                ControlHtml += '     <div class="comment-body">';
                ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Actividad: ' + msg[i].desActividad + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">vendedor: ' + msg[i].Vendedor + '</span></div>';
                ControlHtml += '         <p class="comment-text">' + msg[i].Asunto + '</p>';
                ControlHtml += '         <div class="comment-footer"><span class="comment-meta">Fecha creación: ' + msg[i].fechaTexto + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">Fecha actividad: ' + msg[i].fechaActividadTexto + '</span></div>';
                ControlHtml += '     </div>';
                ControlHtml += ' </div>';

            }
            $('#divHistorialActividades_Actividades').html(ControlHtml + '<br /><br /><br /><br />');

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function event_ListarHistorialActividades_Oportunidades(CodigoSocio, CodigoTipoAgenda) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
        type: "POST",
        url: "/gestionce/ListarSeguimientoAgenda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //alert('imgTipoActividad: ' + msg[i].imgTipoActividad + ' desActividad: ' + msg[i].desActividad + ' Vendedor: ' + msg[i].Vendedor + ' Asunto: ' + msg[i].Asunto + ' fechaTexto: ' + msg[i].fechaTexto + ' fechaActividadTexto: ' + msg[i].fechaActividadTexto);
            var ControlHtml = '';
            if (msg.length == 0) {
                ControlHtml += '<div class="alert alert-secondary" role="alert" style="font-size: 13px;">¡Ups! Este prospecto no tiene un historial de actividades. Te recomendamos encarecidamente que agregues actividades con tus prospectos.</div>';
                $('div[id="divHistorialActividades_Oportunidades"]').html(ControlHtml);
            } else {
                for (var i = 0; i < msg.length; i++) {

                    ControlHtml += '<div class="comment">';
                    ControlHtml += '     <div class="comment-author-ava"><img src="' + msg[i].imgTipoActividad + '" alt="' + msg[i].desActividad + '"></div>';
                    ControlHtml += '     <div class="comment-body">';
                    ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Actividad: ' + msg[i].desActividad + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">vendedor: ' + msg[i].Vendedor + '</span></div>';
                    ControlHtml += '         <p class="comment-text">' + msg[i].Asunto + '</p>';
                    ControlHtml += '         <div class="comment-footer"><span class="comment-meta">Fecha creación: ' + msg[i].fechaTexto + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">Fecha actividad: ' + msg[i].fechaActividadTexto + '</span></div>';
                    ControlHtml += '     </div>';
                    ControlHtml += ' </div>';

                }
            }

            $('#divHistorialActividades_Oportunidades').html(ControlHtml);

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function event_ListarHistorialActividades_Renovaciones(CodigoSocio, CodigoTipoAgenda) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
        type: "POST",
        url: "/gestionce/ListarSeguimientoAgenda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var ControlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                ControlHtml += '<div class="comment">';
                ControlHtml += '     <div class="comment-author-ava"><img src="' + msg[i].imgTipoActividad + '" alt="' + msg[i].desActividad + '"></div>';
                ControlHtml += '     <div class="comment-body">';
                ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Actividad: ' + msg[i].desActividad + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">vendedor: ' + msg[i].Vendedor + '</span></div>';
                ControlHtml += '         <p class="comment-text">' + msg[i].Asunto + '</p>';
                ControlHtml += '         <div class="comment-footer"><span class="comment-meta">Fecha creación: ' + msg[i].fechaTexto + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">Fecha actividad: ' + msg[i].fechaActividadTexto + '</span></div>';
                ControlHtml += '     </div>';
                ControlHtml += ' </div>';

            }

            if (msg.length == 0) {
                ControlHtml += '<div class="alert alert-secondary" role="alert">';
                ControlHtml += '    <strong>No encontramos ningún mensaje de seguimiento</strong>';
                ControlHtml += '</div>';
            }

            $('#divHistorialActividades_Renovaciones').html(ControlHtml + '<br /><br /><br /><br />');

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function event_ListarHistorialActividades_Inactivos(CodigoSocio, CodigoTipoAgenda) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
        type: "POST",
        url: "/gestionce/ListarSeguimientoAgenda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var ControlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                ControlHtml += '<div class="comment">';
                ControlHtml += '     <div class="comment-author-ava"><img src="' + msg[i].imgTipoActividad + '" alt="' + msg[i].desActividad + '"></div>';
                ControlHtml += '     <div class="comment-body">';
                ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Actividad: ' + msg[i].desActividad + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">vendedor: ' + msg[i].Vendedor + '</span></div>';
                ControlHtml += '         <p class="comment-text">' + msg[i].Asunto + '</p>';
                ControlHtml += '         <div class="comment-footer"><span class="comment-meta">Fecha creación: ' + msg[i].fechaTexto + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">Fecha actividad: ' + msg[i].fechaActividadTexto + '</span></div>';
                ControlHtml += '     </div>';
                ControlHtml += ' </div>';

            }

            if (msg.length == 0) {
                ControlHtml += '<div class="alert alert-secondary" role="alert">';
                ControlHtml += '    <strong>No encontramos ningún mensaje de seguimiento</strong>';
                ControlHtml += '</div>';
            }

            $('#divHistorialActividades_Inactivos').html(ControlHtml + '<br /><br /><br /><br />');

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function event_ListarHistorialActividades_Observaciones(CodigoSocio, CodigoTipoAgenda) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
        type: "POST",
        url: "/gestionce/ListarSeguimientoAgenda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var ControlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                ControlHtml += '<div class="comment">';
                ControlHtml += '     <div class="comment-author-ava"><img src="' + msg[i].imgTipoActividad + '" alt="' + msg[i].desActividad + '"></div>';
                ControlHtml += '     <div class="comment-body">';
                ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Actividad: ' + msg[i].desActividad + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">vendedor: ' + msg[i].Vendedor + '</span></div>';
                ControlHtml += '         <p class="comment-text">' + msg[i].Asunto + '</p>';
                ControlHtml += '         <div class="comment-footer"><span class="comment-meta">Fecha creación: ' + msg[i].fechaTexto + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">Fecha actividad: ' + msg[i].fechaActividadTexto + '</span></div>';
                ControlHtml += '     </div>';
                ControlHtml += ' </div>';

            }

            if (msg.length == 0) {
                ControlHtml += '<div class="alert alert-secondary" role="alert">';
                ControlHtml += '    <strong>No encontramos ningún mensaje de seguimiento</strong>';
                ControlHtml += '</div>';
            }

            $('#divHistorialActividades_Observaciones').html(ControlHtml + '<br /><br /><br /><br />');

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function event_ListarHistorialActividades_Matriculados(CodigoSocio, CodigoTipoAgenda) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
        type: "POST",
        url: "/gestionce/ListarSeguimientoAgenda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var ControlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                ControlHtml += '<div class="comment">';
                ControlHtml += '     <div class="comment-author-ava"><img src="' + msg[i].imgTipoActividad + '" alt="' + msg[i].desActividad + '"></div>';
                ControlHtml += '     <div class="comment-body">';
                ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Actividad: ' + msg[i].desActividad + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">vendedor: ' + msg[i].Vendedor + '</span></div>';
                ControlHtml += '         <p class="comment-text">' + msg[i].Asunto + '</p>';
                ControlHtml += '         <div class="comment-footer"><span class="comment-meta">Fecha creación: ' + msg[i].fechaTexto + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">Fecha actividad: ' + msg[i].fechaActividadTexto + '</span></div>';
                ControlHtml += '     </div>';
                ControlHtml += ' </div>';

            }

            if (msg.length == 0) {
                ControlHtml += '<div class="alert alert-secondary" role="alert">';
                ControlHtml += '    <strong>No encontramos ningún mensaje de seguimiento</strong>';
                ControlHtml += '</div>';
            }

            $('#divHistorialActividades_Matriculados').html(ControlHtml + '<br /><br /><br /><br />');

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function event_ListarHistorialActividades_Caidos(CodigoSocio, CodigoTipoAgenda) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"codSocio":"' + CodigoSocio + '","tipo":"' + CodigoTipoAgenda + '"}',
        type: "POST",
        url: "/gestionce/ListarSeguimientoAgenda",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var ControlHtml = '';
            for (var i = 0; i < msg.length; i++) {

                ControlHtml += '<div class="comment">';
                ControlHtml += '     <div class="comment-author-ava"><img src="' + msg[i].imgTipoActividad + '" alt="' + msg[i].desActividad + '"></div>';
                ControlHtml += '     <div class="comment-body">';
                ControlHtml += '         <div class="comment-title"><span class="comment-meta" style="background-color: #0075ff;padding-right: 11px;padding-left: 11px;padding-bottom: 2px;padding-top: 2px;border-radius: 10px;color: #fff;font-size: 11px;">Actividad: ' + msg[i].desActividad + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">vendedor: ' + msg[i].Vendedor + '</span></div>';
                ControlHtml += '         <p class="comment-text">' + msg[i].Asunto + '</p>';
                ControlHtml += '         <div class="comment-footer"><span class="comment-meta">Fecha creación: ' + msg[i].fechaTexto + '</span>&nbsp;&nbsp;&nbsp;<span class="comment-meta">Fecha actividad: ' + msg[i].fechaActividadTexto + '</span></div>';
                ControlHtml += '     </div>';
                ControlHtml += ' </div>';

            }

            if (msg.length == 0) {
                ControlHtml += '<div class="alert alert-secondary" role="alert">';
                ControlHtml += '    <strong>No encontramos ningún mensaje de seguimiento</strong>';
                ControlHtml += '</div>';
            }

            $('#divHistorialActividades_Caidos').html(ControlHtml + '<br /><br /><br /><br />');

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

//EXPORTAR EXCEL
function ExportarMatriculadosClientes() {

    var CodigoUnidadNegocio = getCookie('_CodigoUnidadNegocio_Business');
    var CodigoSede = getCookie('_CodigoSede_Business');

    var Nombres = $('#txtBuscadorClienteAgendaGeneral_Matriculados').val();
    var FechaInicio = kendo.toString($("#FechaDesdeCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#FechaHastaCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var Hi = $('#txtHoraInicioMatricula').val();
    var Hf = $('#txtHoraFinMatricula').val();
    var AsesorDeVentas = $("#ddlUsuarioCreador_Matriculados").data("kendoDropDownList").value();

    var CodigoOrigenMatriculados = "";
    $('input[name="gruporbdOrigenMatriculados"]:checked').each(function () {
        CodigoOrigenMatriculados += $(this).val();
    });

    CodigoOrigenMatriculados = CodigoOrigenMatriculados == '' ? 0 : CodigoOrigenMatriculados;
    var CodigoTiempoMenbresia = $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val() == '' ? 0 : $("#hdddlTiempoMembresiaPaqueteBuscador_Matriculados").val();

    var data = new FormData();
    data.append('CodigoUnidadNegocio', CodigoUnidadNegocio);
    data.append('CodigoSede', CodigoSede);
    data.append('Nombres', Nombres);
    data.append('FechaInicio', FechaInicio);
    data.append('FechaFin', FechaFin);
    data.append('Hi', Hi);
    data.append('Hf', Hf);
    data.append('AsesorDeVentas', AsesorDeVentas);
    data.append('CodigoOrigenMatriculados', CodigoOrigenMatriculados);
    data.append('CodigoTiempoMenbresia', CodigoTiempoMenbresia);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/ExportarExcel/Appsfit/ExportarMatriculadosClientes.ashx', true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "InformeMatriculadosClientes.xls";
            a.style.display = "none";
            document.body.appendChild(a);
            a.click();
        } else {
            alert('Unable to download excel.')
        }
    };
    xhr.send(data);
};

function ExportarProspectosSinActividad() {

    var CodigoUnidadNegocio = getCookie('_CodigoUnidadNegocio_Business');
    var CodigoSede = getCookie('_CodigoSede_Business');

    var Descripcion = $('#txtBuscardorProspectos_ProspectosSinCita').val();
    var Vendedor = $('#ddlVendedor_ProspectosSinCita').data("kendoDropDownList").value();
    var FechaInicio = kendo.toString($("#txtFiltroFechaInicio_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFiltroFechaFin_ProspectosSinCita").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    document.getElementById('loadMe').style.display = 'block';

    var data = new FormData();
    data.append('CodigoUnidadNegocio', CodigoUnidadNegocio);
    data.append('CodigoSede', CodigoSede);
    data.append('Descripcion', Descripcion);
    data.append('Vendedor', Vendedor);
    data.append('FechaInicio', FechaInicio);
    data.append('FechaFin', FechaFin);

    var xhr = new XMLHttpRequest();
    xhr.open('POST', '/ExportarExcel/Appsfit/ExportarProspectosSinActividad.ashx', true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        document.getElementById('loadMe').style.display = 'none';
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
            var downloadUrl = URL.createObjectURL(blob);
            var a = document.createElement("a");
            a.href = downloadUrl;
            a.download = "InformeProspectosSinActividad.xls";
            a.style.display = "none";
            document.body.appendChild(a);
            a.click();
        } else {
            alert('Unable to download excel.')
        }
    };
    xhr.send(data);
};


//AJUSTES EMBUDO VENTA
function event_nuevoEmvudoVenta() {
    document.getElementById('modalNuevoEmbudoVenta').style.display = 'block';
}

function event_cerrarmodalNuevoEmbudoVenta() {
    document.getElementById('modalNuevoEmbudoVenta').style.display = 'none';
    document.getElementById('modalNuevoEmbudoVenta_Editar').style.display = 'none';
}

function event_GuardarEmbudoVenta() {

    var entidad = {};

    entidad.Nombre = ConvertToStringFromObject($('input[id="embudoVenta-txtNombre"]').val());
    entidad.Descripcion = $('#embudoVenta-txtDescripcion').val();

    if (IsUndefinedOrNullOrEmpty(entidad.Nombre)) {
        alert("Falta ingresar nombre del embudo.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
        alert("Falta ingresar la descripción ¿para que se usara este nuevo embudo?.");
        return;
    }

    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {
        //msg.Success
        if (msg) {
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se guardo correctamente los datos.", { type: 'success', width: 'auto' });
            event_cerrarmodalNuevoEmbudoVenta();
            $('input[id="embudoVenta-txtNombre"]').val('');
            $('input[id="embudoVenta-txtDescripcion"]').val('');
            event_ListarDDLEmbudoVenta();
        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_1_embudosventaplantilla", request, metodoCorrecto, metodoError);

}

function event_ActualizarEmbudoVenta() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#embudoVenta-ddlEmbudosVenta").data("kendoDropDownList").value();
    entidad.Nombre = ConvertToStringFromObject($('input[id="embudoVenta-txtNombre-editar"]').val());
    entidad.Descripcion = $('#embudoVenta-txtDescripcion-editar').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Nombre)) {
        alert("Falta ingresar nombre del embudo.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
        alert("Falta ingresar la descripción ¿para que se usara este embudo?.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {

        if (msg) {
            document.getElementById('loadMe').style.display = 'none';
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se actualizo correctamente los datos.", { type: 'success', width: 'auto' });
            event_cerrarmodalNuevoEmbudoVenta();
            $('input[id="embudoVenta-txtNombre-editar"]').val('');
            $('input[id="embudoVenta-txtDescripcion-editar"]').val('');
            event_ListarDDLEmbudoVenta();
        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspActualizar_gimnasio_crm_1_embudosventaplantilla", request, metodoCorrecto, metodoError);

}

function event_ListarDDLEmbudoVenta() {
    var ddlEmbudosVenta = $("#embudoVenta-ddlEmbudosVenta").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccionar embudo",
        dataTextField: "Nombre",
        dataValueField: "CodigoEmbudoVenta",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {
            event_CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla();
        }
    }).data("kendoDropDownList");
}

function event_ListarDDLEmbudoVenta_Oportunidades() {
    var ddlEmbudosVenta = $("#oportunidades-ddlEmbudosVenta").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccionar embudo",
        dataTextField: "Nombre",
        dataValueField: "CodigoEmbudoVenta",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {
            event_CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla_Oportunidades();
        }
    }).data("kendoDropDownList");
}

function event_ListarDDLEmbudoVenta_ProspectosSinCita() {
    var ddlEmbudosVenta = $("#ddlEmbudoVenta_prospectosSinCita").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccionar embudo",
        dataTextField: "Nombre",
        dataValueField: "CodigoEmbudoVenta",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {
            event_ListarDDLEtapaVenta_ProspectosSinCita();
        }
    }).data("kendoDropDownList");
}

function event_ListarDDLEtapaVenta_ProspectosSinCita() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_prospectosSinCita").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg.length == 0) {
            var control = '';
            control += '<div class="alert alert-secondary border-2 d-flex align-items-center" role="alert">';
            control += '  <p class="mb-0 flex-1" style="font-size:11px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas.</p>';
            control += '</div>';
            $('#ddlEtapaVenta_prospectosSinCita').html(control);
        } else {
            var control = '';
            for (var i = 0; i < msg.length; i++) {
                control += '<input type="checkbox" class="btn-check" data-codigoetapa="' + msg[i].CodigoEtapa + '" value="' + (i + 1) + '" id="chkEtapas_ProspectosSinCita_' + (i + 1) + '" name="chkEtapas_ProspectosSinCita" onclick="eventChange_chkEtapas_prospectosWalking_sinActividad(this)" >';
                control += '<label class="btn btn-outline-success btn-sm" for="chkEtapas_ProspectosSinCita_' + (i + 1) + '" data-bs-toggle="tooltip" data-bs-placement="bottom" title="' + msg[i].NombreEtapa + '" alt="' + msg[i].NombreEtapa + '" >' + (i + 1) + '</label>';
            }
            $('#ddlEtapaVenta_prospectosSinCita').html(control);

            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            })
        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_ListarDDLEmbudoVenta_Walking() {
    var ddlEmbudosVenta = $("#ddlEmbudoVenta_prospectosWalking").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccionar embudo",
        dataTextField: "Nombre",
        dataValueField: "CodigoEmbudoVenta",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {

            event_ListarDDLEtapaVenta_Walking();
        }
    }).data("kendoDropDownList");
}

function event_ListarDDLEtapaVenta_Walking() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_prospectosWalking").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg.length == 0) {
            var control = '';
            control += '<div class="alert alert-secondary border-2 d-flex align-items-center" role="alert">';
            control += '  <p class="mb-0 flex-1" style="font-size:11px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas.</p>';
            control += '</div>';
            $('#ddlEtapaVenta_prospectosWalking').html(control);
        } else {
            var control = '';
            for (var i = 0; i < msg.length; i++) {
                control += '<input type="checkbox" class="btn-check" data-codigoetapa="' + msg[i].CodigoEtapa + '" value="' + (i + 1) + '" id="chkEtapas_prospectosWalking_' + (i + 1) + '" name="chkEtapas_prospectosWalking" onclick="eventChange_chkEtapas_prospectosWalking(this)" >';
                control += '<label class="btn btn-outline-success btn-sm" for="chkEtapas_prospectosWalking_' + (i + 1) + '" data-bs-toggle="tooltip" data-bs-placement="bottom" title="' + msg[i].NombreEtapa + '" alt="' + msg[i].NombreEtapa + '" >' + (i + 1) + '</label>';
            }
            $('#ddlEtapaVenta_prospectosWalking').html(control);

            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            })
        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_BuscarEmbudoVenta() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#embudoVenta-ddlEmbudosVenta").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    document.getElementById('modalNuevoEmbudoVenta_Editar').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);
        $('input[id="embudoVenta-txtNombre-editar"]').val(msg.Nombre);
        $('input[id="embudoVenta-txtDescripcion-editar"]').val(msg.Descripcion);
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspBuscar_gimnasio_crm_1_embudosventaplantilla", request, metodoCorrecto, metodoError);

}

function event_ListarDDLEmbudoVenta_Reagendar() {
    var ddlEmbudosVenta = $("#ddlEmbudoVenta_Reagendar").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccionar embudo",
        dataTextField: "Nombre",
        dataValueField: "CodigoEmbudoVenta",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {

            event_ListarDDLEtapaVenta_Reagendar();
        }
    }).data("kendoDropDownList");
}

function event_ListarDDLEtapaVenta_Reagendar() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_Reagendar").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg.length == 0) {
            var control = '';
            control += '<div class="alert alert-secondary border-2 d-flex align-items-center" role="alert">';
            control += '  <p class="mb-0 flex-1" style="font-size:11px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas.</p>';
            control += '</div>';
            $('#ddlEtapaVenta_Reagendar').html(control);
        } else {
            var control = '';
            for (var i = 0; i < msg.length; i++) {
                control += '<input type="checkbox" class="btn-check" data-codigoetapa="' + msg[i].CodigoEtapa + '" value="' + (i + 1) + '" id="chkEtapas_prospectosReagendar_' + (i + 1) + '" name="chkEtapas_prospectosReagendar" onclick="eventChange_chkEtapas_prospectosReagendar(this)" >';
                control += '<label class="btn btn-outline-success btn-sm" for="chkEtapas_prospectosReagendar_' + (i + 1) + '" data-bs-toggle="tooltip" data-bs-placement="bottom" title="' + msg[i].NombreEtapa + '" alt="' + msg[i].NombreEtapa + '" >' + (i + 1) + '</label>';
            }
            $('#ddlEtapaVenta_Reagendar').html(control);

            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            })
        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_ListarDDLEmbudoVenta_Inactivos() {
    var ddlEmbudosVenta = $("#ddlEmbudoVenta_Inactivos").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccionar embudo",
        dataTextField: "Nombre",
        dataValueField: "CodigoEmbudoVenta",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {

            event_ListarDDLEtapaVenta_Inactivos();
        }
    }).data("kendoDropDownList");
}

function event_ListarDDLEtapaVenta_Inactivos() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_Inactivos").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg.length == 0) {
            var control = '';
            control += '<div class="alert alert-secondary border-2 d-flex align-items-center" role="alert">';
            control += '  <p class="mb-0 flex-1" style="font-size:11px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas.</p>';
            control += '</div>';
            $('#ddlEtapaVenta_Inactivos').html(control);
        } else {
            var control = '';
            for (var i = 0; i < msg.length; i++) {
                control += '<input type="checkbox" class="btn-check" data-codigoetapa="' + msg[i].CodigoEtapa + '" value="' + (i + 1) + '" id="chkEtapas_prospectosInactivos_' + (i + 1) + '" name="chkEtapas_prospectosInactivos" onclick="eventChange_chkEtapas_prospectosInactivos(this)" >';
                control += '<label class="btn btn-outline-success btn-sm" for="chkEtapas_prospectosInactivos_' + (i + 1) + '" data-bs-toggle="tooltip" data-bs-placement="bottom" title="' + msg[i].NombreEtapa + '" alt="' + msg[i].NombreEtapa + '" >' + (i + 1) + '</label>';
            }
            $('#ddlEtapaVenta_Inactivos').html(control);

            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            })
        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_ListarDDLEmbudoVenta_Renovaciones() {
    var ddlEmbudosVenta = $("#ddlEmbudoVenta_Renovaciones").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Seleccionar embudo",
        dataTextField: "Nombre",
        dataValueField: "CodigoEmbudoVenta",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_1_embudosventaplantilla",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        }, change: function () {

            event_ListarDDLEtapaVenta_Renovaciones();
        }
    }).data("kendoDropDownList");
}

function event_ListarDDLEtapaVenta_Renovaciones() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_Renovaciones").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg.length == 0) {
            var control = '';
            control += '<div class="alert alert-secondary border-2 d-flex align-items-center" role="alert">';
            control += '  <p class="mb-0 flex-1" style="font-size:11px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas.</p>';
            control += '</div>';
            $('#ddlEtapaVenta_Renovaciones').html(control);
        } else {
            var control = '';
            for (var i = 0; i < msg.length; i++) {
                control += '<input type="checkbox" class="btn-check" data-codigoetapa="' + msg[i].CodigoEtapa + '" value="' + (i + 1) + '" id="chkEtapas_prospectosRenovaciones_' + (i + 1) + '" name="chkEtapas_prospectosRenovaciones" onclick="eventChange_chkEtapas_prospectosRenovaciones(this)" >';
                control += '<label class="btn btn-outline-success btn-sm" for="chkEtapas_prospectosRenovaciones_' + (i + 1) + '" data-bs-toggle="tooltip" data-bs-placement="bottom" title="' + msg[i].NombreEtapa + '" alt="' + msg[i].NombreEtapa + '" >' + (i + 1) + '</label>';
            }
            $('#ddlEtapaVenta_Renovaciones').html(control);

            var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
            var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
                return new bootstrap.Tooltip(tooltipTriggerEl)
            })
        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}


//AJUSTES ETAPAS DE VENTA
var docReady = function docReady(fn) {
    // see if DOM is already available
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', fn);
    } else {
        setTimeout(fn, 1);
    }
};
var draggableInit_EmbudosVentaEditable = function draggableInit_EmbudosVentaEditable() {
    var Selectors = {
        BODY: 'body',
        KANBAN_CONTAINER: '.container-fluid',
        KABNBAN_COLUMN: '.kanban-container-embudosventaeditable',
        KANBAN_ITEMS_CONTAINER: '.kanban-container-embudosventaeditable',
        KANBAN_ITEM: '.kanban-column',
        ADD_CARD_FORM: '.add-card-form'
    };
    var Events = {
        DRAG_START: 'drag:start',
        DRAG_STOP: 'drag:stop'
    };
    var ClassNames = {
        FORM_ADDED: 'form-added'
    };
    var columns = document.querySelectorAll(Selectors.KABNBAN_COLUMN);
    var columnContainers = document.querySelectorAll(Selectors.KANBAN_ITEMS_CONTAINER);
    var container = document.querySelector(Selectors.KANBAN_CONTAINER);

    if (columnContainers.length) {

        // Initialize Sortable
        var sortable = new window.Draggable.Sortable(columnContainers, {
            draggable: Selectors.KANBAN_ITEM,
            delay: 200,
            mirror: {
                appendTo: Selectors.BODY,
                constrainDimensions: true
            },
            scrollable: {
                draggable: Selectors.KANBAN_ITEM,
                scrollableElements: [].concat(_toConsumableArray(columnContainers), [container])
            }
        }); // Hide form when drag start

        sortable.on(Events.DRAG_START, function () {

            columns.forEach(function (column) {

                utils.hasClass(column, ClassNames.FORM_ADDED) && column.classList.remove(ClassNames.FORM_ADDED);
            });
        }); // Place forms and other contents bottom of the sortable container

        sortable.on(Events.DRAG_STOP, function (_ref2) {

            var el = _ref2.data.source;
            var columnContainer = el.closest(Selectors.KANBAN_ITEMS_CONTAINER);
            var form = columnContainer.querySelector(Selectors.ADD_CARD_FORM);
            !el.nextElementSibling && columnContainer.appendChild(form);
        });
    }
};

function event_CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#embudoVenta-ddlEmbudosVenta").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg.length == 0) {
            event_nuevoEtapaVenta();
        } else {
            event_ConstruirDatosEtapaVenta(msg);
        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_nuevoEtapaVenta() {
    //aqui creacion de control
    var control = '<div class="container-fluid" data-layout="container-fluid">';
    control += '<div class="alert alert-secondary" role="alert" style="font-size: 13px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas.</div>';
    control += '<div class="content kanban-content-embudosventaeditable">';
    control += '<div class="kanban-container kanban-container-embudosventaeditable scrollbar me-n3" >';

    for (var i = 1; i < 6; i++) {

        control += '<div class="kanban-column" data-index="' + i + '" id="kanban-column-editable-' + i + '" >';
        control += '  <div class="kanban-column-header" style="background-color: #fff;">';

        if (i == 1) {
            control += '<b class="mb-0" style="color:#000;font-size:14px;" data-index="' + i + '" id="etapaVenta-lblNombreEtapa-' + i + '">Negociación comenzada</b>';
        } else if (i == 2) {
            control += '<b class="mb-0" style="color:#000;font-size:14px;" data-index="' + i + '" id="etapaVenta-lblNombreEtapa-' + i + '">Cualificado</b>';
        } else if (i == 3) {
            control += '<b class="mb-0" style="color:#000;font-size:14px;" data-index="' + i + '" id="etapaVenta-lblNombreEtapa-' + i + '">Contactado</b>';
        } else if (i == 4) {
            control += '<b class="mb-0" style="color:#000;font-size:14px;" data-index="' + i + '" id="etapaVenta-lblNombreEtapa-' + i + '">Demostración programada</b>';
        } else if (i == 5) {
            control += '<b class="mb-0" style="color:#000;font-size:14px;" data-index="' + i + '" id="etapaVenta-lblNombreEtapa-' + i + '">Propuesta enviada</b>';
        }

        control += '<input type="hidden" value="" data-index="' + i + '" id="etapaVenta-CodigoEtapa-' + i + '" />';

        control += '<div class="fas fa-grip-lines-vertical" style="cursor:pointer;"></div>';
        control += '  </div>';
        control += '  <div class="kanban-items-container scrollbar">';
        control += '   <div class="kanban-item">';
        control += '      <div class="card kanban-item-card hover-actions-trigger">';
        control += '          <div class="card-body">';
        control += '              <div class="mb-3">';
        control += '                  <label class="form-label" for="etapaVenta-txtNombreEtapa-' + i + '" style="font-size: 13px;text-align: right;">Nombre:</label>';

        if (i == 1) {
            control += '<input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtNombreEtapa-' + i + '" onkeypress="event_etapaVenta_CambioNombre(this)" onkeydown="event_etapaVenta_CambioNombre(this)" onkeyup="event_etapaVenta_CambioNombre(this)" style="font-size: 13px;width: 100%;" type="text" value="Negociación comenzada"> ';
        } else if (i == 2) {
            control += '<input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtNombreEtapa-' + i + '" onkeypress="event_etapaVenta_CambioNombre(this)" onkeydown="event_etapaVenta_CambioNombre(this)" onkeyup="event_etapaVenta_CambioNombre(this)" style="font-size: 13px;width: 100%;" type="text" value="Cualificado"> ';
        } else if (i == 3) {
            control += '<input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtNombreEtapa-' + i + '" onkeypress="event_etapaVenta_CambioNombre(this)" onkeydown="event_etapaVenta_CambioNombre(this)" onkeyup="event_etapaVenta_CambioNombre(this)" style="font-size: 13px;width: 100%;" type="text" value="Contactado"> ';
        } else if (i == 4) {
            control += '<input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtNombreEtapa-' + i + '" onkeypress="event_etapaVenta_CambioNombre(this)" onkeydown="event_etapaVenta_CambioNombre(this)" onkeyup="event_etapaVenta_CambioNombre(this)" style="font-size: 13px;width: 100%;" type="text" value="Demostración programada"> ';
        } else if (i == 5) {
            control += '<input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtNombreEtapa-' + i + '" onkeypress="event_etapaVenta_CambioNombre(this)" onkeydown="event_etapaVenta_CambioNombre(this)" onkeyup="event_etapaVenta_CambioNombre(this)" style="font-size: 13px;width: 100%;" type="text" value="Propuesta enviada"> ';
        }

        control += '</div>';
        control += '                  <div class="mb-3">';
        control += '                      <label class="form-label" for="etapaVenta-txtProbabilidadNegocio-' + i + '" style="font-size: 13px;text-align: right;">Probabilidad:</label>';
        control += '                      <a tabindex="0" role="button" data-bs-toggle="popover" data-bs-trigger="focus" title="Dismissible popover" data-bs-content="And heres some amazing content. Its very engaging. Right?">';
        control += '                          <div class="fas fa-question-circle"></div>';
        control += '                      </a>';
        control += '                      <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtProbabilidadNegocio-' + i + '" style="font-size: 13px;width: 100%;" type="number" value="100">';

        control += '</div>';
        control += '                      <div class="mb-3">';
        control += '                          <div class="form-check form-switch">';
        control += '                              <input class="form-check-input" data-index="' + i + '" id="etapaVenta-rbtNegocioEstancandose-' + i + '" type="checkbox" onclick="event_etapaVenta_rbtNegocioEstancandose(this);" />';

        control += '                              <label class="form-check-label" for="etapaVenta-rbtNegocioEstancandose-' + i + '" style="font-size: 13px;">Estancado en (días)</label>';
        control += '                              <a tabindex="0" role="button" data-bs-toggle="popover" data-bs-trigger="focus" title="Dismissible popover" data-bs-content="And heres some amazing content. Its very engaging. Right?">';
        control += '                                  <div class="fas fa-question-circle"></div>';
        control += '                              </a>';
        control += '                          </div>';
        control += '                          <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtDiasAvisoInactividad-' + i + '" style="font-size: 13px;width: 100%;display:none;" type="number" value="0">';
        control += '                                                      </div>';

        control += '                      </div>';
        control += '                  </div>';
        control += '              </div>';
        control += '          </div>';
        control += '          <div class="kanban-column-footer">';
        control += '              <button onclick="event_EliminarConfirmarEtapaVenta(' + i + ');" class="btn btn-link btn-sm d-block w-100 btn-add-card text-decoration-none text-600" type="button"><span class="fas fa-trash-alt"></span>Eliminar etapa</button>';
        control += '          </div>';
        control += '      </div>';
    }

    control += '<div class="kanban-column" data-index="0">';
    control += '    <div class="bg-100 p-card rounded-lg">';

    control += '            <h3>Añadir nueva etapa</h3>';
    control += '            <h4 style="color:#000;">Las etapas del embudo representan los pasos de tu proceso de ventas</h4>';

    control += '    </div>';
    control += '    <button onclick="event_añadir1EtapaVenta()" class="btn btn-primary btn-sm w-100" ><span class="fas fa-plus me-1" data-fa-transform="shrink-3"></span>Nueva etapa</button>';
    control += '</div>';



    control += '</div>';
    control += '</div>';
    control += '</div>';


    $('main[id="main_ControlEmbudosVentaEditable"]').html(control);
    docReady(draggableInit_EmbudosVentaEditable);
}

function event_añadir1EtapaVenta() {
    var i = $('.kanban-container-embudosventaeditable > div').length;

    var control = '<div class="kanban-column" data-index="' + i + '" id="kanban-column-editable-' + i + '" >';
    control += '  <div class="kanban-column-header" style="background-color: #fff;">';

    control += '<b class="mb-0" style="color:#000;font-size:14px;" data-index="' + i + '" id="etapaVenta-lblNombreEtapa-' + i + '">Nueva etapa ' + i + '</b>';

    control += '<input type="hidden" value="" data-index="' + i + '" id="etapaVenta-CodigoEtapa-' + i + '" />';

    control += '<div class="fas fa-grip-lines-vertical" style="cursor:pointer;"></div>';
    control += '  </div>';
    control += '  <div class="kanban-items-container scrollbar">';
    control += '   <div class="kanban-item">';
    control += '      <div class="card kanban-item-card hover-actions-trigger">';
    control += '          <div class="card-body">';
    control += '              <div class="mb-3">';
    control += '                  <label class="form-label" for="etapaVenta-txtNombreEtapa-' + i + '" style="font-size: 13px;text-align: right;">Nombre:</label>';

    control += '<input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtNombreEtapa-' + i + '" onkeypress="event_etapaVenta_CambioNombre(this)" onkeydown="event_etapaVenta_CambioNombre(this)" onkeyup="event_etapaVenta_CambioNombre(this)" style="font-size: 13px;width: 100%;" type="text" value="Nueva etapa ' + i + '"> ';

    control += '</div>';
    control += '                  <div class="mb-3">';
    control += '                      <label class="form-label" for="etapaVenta-txtProbabilidadNegocio-' + i + '" style="font-size: 13px;text-align: right;">Probabilidad:</label>';
    control += '                      <a tabindex="0" role="button" data-bs-toggle="popover" data-bs-trigger="focus" title="Dismissible popover" data-bs-content="And heres some amazing content. Its very engaging. Right?">';
    control += '                          <div class="fas fa-question-circle"></div>';
    control += '                      </a>';
    control += '                      <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtProbabilidadNegocio-' + i + '" style="font-size: 13px;width: 100%;" type="number" value="100">';

    control += '</div>';
    control += '                      <div class="mb-3">';
    control += '                          <div class="form-check form-switch">';
    control += '                              <input class="form-check-input" data-index="' + i + '" id="etapaVenta-rbtNegocioEstancandose-' + i + '" type="checkbox" onclick="event_etapaVenta_rbtNegocioEstancandose(this);" />';

    control += '                              <label class="form-check-label" for="etapaVenta-rbtNegocioEstancandose-' + i + '" style="font-size: 13px;">Estancado en (días)</label>';
    control += '                              <a tabindex="0" role="button" data-bs-toggle="popover" data-bs-trigger="focus" title="Dismissible popover" data-bs-content="And heres some amazing content. Its very engaging. Right?">';
    control += '                                  <div class="fas fa-question-circle"></div>';
    control += '                              </a>';
    control += '                          </div>';
    control += '                          <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtDiasAvisoInactividad-' + i + '" style="font-size: 13px;width: 100%;display:none;" type="number" value="0">';
    control += '                                                      </div>';

    control += '                      </div>';
    control += '                  </div>';
    control += '              </div>';
    control += '          </div>';
    control += '          <div class="kanban-column-footer">';
    control += '              <button onclick="event_EliminarConfirmarEtapaVenta(' + i + ');" class="btn btn-link btn-sm d-block w-100 btn-add-card text-decoration-none text-600" type="button"><span class="fas fa-trash-alt"></span>Eliminar etapa</button>';
    control += '          </div>';
    control += '      </div>';

    $('.kanban-container-embudosventaeditable > div:last').before(control);

}

function event_ConstruirDatosEtapaVenta(datos) {
    //aqui creacion de control
    var control = '<div class="container-fluid" data-layout="container-fluid">';
    control += '<div class="content kanban-content-embudosventaeditable">';
    control += '<div class="kanban-container kanban-container-embudosventaeditable scrollbar me-n3" >';
    $.each(datos, function (index, item) {

        var i = (index + 1);
        control += '<div class="kanban-column" data-index="' + i + '" id="kanban-column-editable-' + i + '" >';
        control += '  <div class="kanban-column-header" style="background-color: #fff;">';
        control += '    <b class="mb-0" style="color:#000;font-size:14px;" data-index="' + i + '" id="etapaVenta-lblNombreEtapa-' + i + '">' + item.NombreEtapa + '</b>';
        control += '    <input type="hidden" value="' + item.CodigoEtapa + '" data-index="' + i + '" id="etapaVenta-CodigoEtapa-' + i + '" />';

        control += '<div class="fas fa-grip-lines-vertical" style="cursor:pointer;"></div>';
        control += '  </div>';
        control += '  <div class="kanban-items-container scrollbar">';
        control += '   <div class="kanban-item">';
        control += '      <div class="card kanban-item-card hover-actions-trigger">';
        control += '          <div class="card-body">';
        control += '              <div class="mb-3">';
        control += '                  <label class="form-label" for="etapaVenta-txtNombreEtapa-' + i + '" style="font-size: 13px;text-align: right;">Nombre:</label>';
        control += '                  <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtNombreEtapa-' + i + '" onkeypress="event_etapaVenta_CambioNombre(this)" onkeydown="event_etapaVenta_CambioNombre(this)" onkeyup="event_etapaVenta_CambioNombre(this)" style="font-size: 13px;width: 100%;" type="text" value="' + item.NombreEtapa + '"> ';
        control += '              </div>';
        control += '              <div class="mb-3">';
        control += '                      <label class="form-label" for="etapaVenta-txtProbabilidadNegocio-' + i + '" style="font-size: 13px;text-align: right;">Probabilidad:</label>';
        control += '                      <a tabindex="0" role="button" data-bs-toggle="popover" data-bs-trigger="focus" title="Dismissible popover" data-bs-content="And heres some amazing content. Its very engaging. Right?">';
        control += '                          <div class="fas fa-question-circle"></div>';
        control += '                      </a>';
        control += '                      <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtProbabilidadNegocio-' + i + '" style="font-size: 13px;width: 100%;" type="number" value="' + item.ProbabilidadNegocio + '">';
        control += '              </div>';
        control += '              <div class="mb-3">';
        control += '                      <div class="form-check form-switch">';
        if (item.NegocioEstancandose) {
            control += '                <input class="form-check-input" data-index="' + i + '" id="etapaVenta-rbtNegocioEstancandose-' + i + '" type="checkbox" onclick="event_etapaVenta_rbtNegocioEstancandose(this);" checked />';

        } else {
            control += '                  <input class="form-check-input" data-index="' + i + '" id="etapaVenta-rbtNegocioEstancandose-' + i + '" type="checkbox" onclick="event_etapaVenta_rbtNegocioEstancandose(this);" />';

        }

        control += '                          <label class="form-check-label" for="etapaVenta-rbtNegocioEstancandose-' + i + '" style="font-size: 13px;">Estancado en (días)</label>';
        control += '                          <a tabindex="0" role="button" data-bs-toggle="popover" data-bs-trigger="focus" title="Dismissible popover" data-bs-content="And heres some amazing content. Its very engaging. Right?">';
        control += '                              <div class="fas fa-question-circle"></div>';
        control += '                          </a>';
        control += '                      </div>';

        if (item.NegocioEstancandose) {
            control += '                  <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtDiasAvisoInactividad-' + i + '" style="font-size: 13px;width: 100%;" type="number" value="' + item.DiasAvisoInactividad + '">';
        } else {
            control += '                  <input class="form-control form-control-sm" data-index="' + i + '" id="etapaVenta-txtDiasAvisoInactividad-' + i + '" style="font-size: 13px;width: 100%;display:none;" type="number" value="' + item.DiasAvisoInactividad + '">';
        }


        control += '                        </div>';

        control += '                      </div>';
        control += '                  </div>';
        control += '              </div>';
        control += '          </div>';
        control += '          <div class="kanban-column-footer">';
        control += '              <button onclick="event_EliminarConfirmarEtapaVenta(' + i + ');" class="btn btn-link btn-sm d-block w-100 btn-add-card text-decoration-none text-600" type="button"><span class="fas fa-trash-alt"></span>Eliminar etapa</button>';
        control += '          </div>';
        control += '      </div>';


    });

    control += '<div class="kanban-column" data-index="0">';
    control += '    <div class="bg-100 p-card rounded-lg">';

    control += '            <h3>Añadir nueva etapa</h3>';
    control += '            <h4 style="color:#000;">Las etapas del embudo representan los pasos de tu proceso de ventas</h4>';

    control += '    </div>';
    control += '    <button onclick="event_añadir1EtapaVenta()" class="btn btn-primary btn-sm w-100" ><span class="fas fa-plus me-1" data-fa-transform="shrink-3"></span>Nueva etapa</button>';
    control += '</div>';


    control += '</div>';
    control += '</div>';
    control += '</div>';

    $('main[id="main_ControlEmbudosVentaEditable"]').html(control);
    docReady(draggableInit_EmbudosVentaEditable);
}

function event_GuardarEtapaVenta() {

    var entidad = new Array();
    var orden = 1;
    $('.kanban-container-embudosventaeditable > div').each(function () {
        var i = $(this).attr('data-index');
        if (i != 0) {
            var item = {};
            item.CodigoEmbudoVenta = $("#embudoVenta-ddlEmbudosVenta").data("kendoDropDownList").value();
            item.CodigoEtapa = $('#etapaVenta-CodigoEtapa-' + i).val();
            item.NombreEtapa = $('#etapaVenta-txtNombreEtapa-' + i).val();
            item.OrdenEtapa = orden;
            item.ProbabilidadNegocio = $('#etapaVenta-txtProbabilidadNegocio-' + i).val();
            item.NegocioEstancandose = $('#etapaVenta-rbtNegocioEstancandose-' + i).is(':checked');
            item.DiasAvisoInactividad = $('#etapaVenta-rbtNegocioEstancandose-' + i).is(':checked') ? $('#etapaVenta-txtDiasAvisoInactividad-' + i).val() : 0;

            entidad.push(item);
            orden++;
        }

    });

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {

        if (msg) {
            document.getElementById('loadMe').style.display = 'none';
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se guardo correctamente los datos.", { type: 'success', width: 'auto' });
        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_EliminarConfirmarEtapaVenta(index) {

    if ($('#etapaVenta-CodigoEtapa-' + index).val() == '') {
        $('#kanban-column-editable-' + index).remove();
    } else {
        document.getElementById('modalConfirmarEliminarEtapaVenta').style.display = 'block';
        $('#hdEtapaVenta-CodigoEtapa-EliminarConfirmar').val($('#etapaVenta-CodigoEtapa-' + index).val());
    }
}

function eventChange_chkEtapas_prospectosWalking(control) {
    //obtener el valor
    var valorClick = $(control).val();
    $('#hdCodigoEtapaVenta_prospectosWalking').val($(control).attr('data-codigoetapa'));

    $('input:checkbox[name=chkEtapas_prospectosWalking]').each(function () {
        var valorControl = $(this).val();
        if (valorControl <= valorClick) {
            $(this).prop('checked', true);
        }
        else {
            $(this).prop('checked', false);
        }
    });

}

function eventChange_chkEtapas_prospectosReagendar(control) {
    //obtener el valor
    var valorClick = $(control).val();
    $('#hdCodigoEtapaVenta_Reagendar').val($(control).attr('data-codigoetapa'));

    $('input:checkbox[name=chkEtapas_prospectosReagendar]').each(function () {
        var valorControl = $(this).val();
        if (valorControl <= valorClick) {
            $(this).prop('checked', true);
        }
        else {
            $(this).prop('checked', false);
        }
    });

}

function eventChange_chkEtapas_prospectosInactivos(control) {
    //obtener el valor
    var valorClick = $(control).val();
    $('#hdCodigoEtapaVenta_Inactivos').val($(control).attr('data-codigoetapa'));

    $('input:checkbox[name=chkEtapas_prospectosInactivos]').each(function () {
        var valorControl = $(this).val();
        if (valorControl <= valorClick) {
            $(this).prop('checked', true);
        }
        else {
            $(this).prop('checked', false);
        }
    });

}

function eventChange_chkEtapas_prospectosRenovaciones(control) {
    //obtener el valor
    var valorClick = $(control).val();
    $('#hdCodigoEtapaVenta_Renovaciones').val($(control).attr('data-codigoetapa'));

    $('input:checkbox[name=chkEtapas_prospectosRenovaciones]').each(function () {
        var valorControl = $(this).val();
        if (valorControl <= valorClick) {
            $(this).prop('checked', true);
        }
        else {
            $(this).prop('checked', false);
        }
    });

}

function eventChange_chkEtapas_prospectosWalking_sinActividad(control) {
    //obtener el valor
    var valorClick = $(control).val();
    $('#hdCodigoEtapaVenta_prospectosSinCita').val($(control).attr('data-codigoetapa'));

    $('input:checkbox[name=chkEtapas_ProspectosSinCita]').each(function () {
        var valorControl = $(this).val();
        if (valorControl <= valorClick) {
            $(this).prop('checked', true);
        }
        else {
            $(this).prop('checked', false);
        }
    });

}

function event_cerrarmodalConfirmarEliminarEtapaVenta() {
    document.getElementById('modalConfirmarEliminarEtapaVenta').style.display = 'none';
}

function event_EliminarEtapaVenta() {
    //entonces aqui eliminar la etapa por su codigo
    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#embudoVenta-ddlEmbudosVenta").data("kendoDropDownList").value();
    entidad.CodigoEtapa = $('#hdEtapaVenta-CodigoEtapa-EliminarConfirmar').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {

        if (msg) {
            document.getElementById('modalConfirmarEliminarEtapaVenta').style.display = 'none';
            document.getElementById('loadMe').style.display = 'none';
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se actualizo correctamente los datos.", { type: 'success', width: 'auto' });
            event_CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla();
        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspEliminar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_etapaVenta_rbtNegocioEstancandose(control) {
    var index = $(control).attr('data-index');

    if ($('#etapaVenta-rbtNegocioEstancandose-' + index).is(':checked')) {
        document.getElementById("etapaVenta-txtDiasAvisoInactividad-" + index).style.display = 'block';
    } else {
        document.getElementById("etapaVenta-txtDiasAvisoInactividad-" + index).style.display = 'none';
    }
}

function event_etapaVenta_CambioNombre(control) {
    var index = $(control).attr('data-index');
    $('#etapaVenta-lblNombreEtapa-' + index).html($('#etapaVenta-txtNombreEtapa-' + index).val());
}
//INGRESAR PROSPECTO NUEVO
function event_IngresarNuevoProspecto() {
    document.getElementById("myModal_IngresarNuevoProspecto").style.display = 'block';
}

function event_CerrarModalIngresarNuevoProspecto() {
    document.getElementById("myModal_IngresarNuevoProspecto").style.display = 'none';
}

//TRATOS
function event_guardartratosprospecto() {
    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_prospectosWalking").data("kendoDropDownList").value();
    entidad.CodigoEtapa = ConvertToStringFromObject($('input[id="hdCodigoEtapaVenta_prospectosWalking"]').val());
    entidad.NombreTrato = ConvertToStringFromObject($('input[id="txtTituloTrato_prospectosWalking"]').val());
    entidad.CodigoTratoProspecto = '';
    entidad.CodigoEstadoEtapa = 1;//ESTADO ABIERTO
    entidad.FechaPrevistaCierre = $("#txtFechaCierrePrevista_prospectosWalking").data('kendoDatePicker').value();
    entidad.CodigoMoneda = 0;


    if ($('#hdCodigoOrigen_Prospecto').val() == '1') {
        entidad.Valor = ConvertToStringFromObject($('input[id="txtPrecio_SocioIAgenda"]').val());
        entidad.Vendedor = $("#txtVendedorIAgenda").data("kendoDropDownList").value();
    } else if ($('#hdCodigoOrigen_Prospecto').val() == '4') {
        entidad.Valor = ConvertToStringFromObject($('input[id="txtPrecioInvitado"]').val());
        entidad.Vendedor = $("#txtVendedorIAgenda_invitados").data("kendoDropDownList").value();
    } else if ($('#hdCodigoOrigen_Prospecto').val() == '5') {
        entidad.Valor = ConvertToStringFromObject($('input[id="txtPrecioReferido"]').val());
        entidad.Vendedor = $('#txtVendedorIAgenda_referidos').data("kendoDropDownList").value();
    } else if ($('#hdCodigoOrigen_Prospecto').val() == '6') {
        entidad.Valor = ConvertToStringFromObject($('input[id="txtPrecioLlamadaE"]').val());
        entidad.Vendedor = $("#txtVendedorIAgenda_llamadaentrante").data("kendoDropDownList").value();
    } else {
        entidad.Valor = 0;
    }

    entidad.CodigoOrigenProspecto = $('#hdCodigoOrigen_Prospecto').val();
    entidad.CodigoProspecto = $('#txtCodigo_SocioIAgenda').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.NombreTrato)) {
        alert("Falta ingresar el nombre del trato.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.FechaPrevistaCierre)) {
        alert("Falta ingresar la fecha prevista del cierre de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto) || entidad.CodigoProspecto == 0) {
        alert("Falta seleccionar un prospecto.");
        return;
    }

    //alert('Origen: ' + entidad.CodigoOrigenProspecto);
    //return false;
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

            //event_cerrarmodalNuevoEmbudoVenta();
            //$('input[id="embudoVenta-txtNombre"]').val('');
            //$('input[id="embudoVenta-txtDescripcion"]').val('');
            //event_ListarDDLEmbudoVenta();
        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);

}

function event_actualizaretapa_tratosprospecto() {
    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#oportunidades-ddlEmbudosVenta").data("kendoDropDownList").value();
    entidad.CodigoEtapa = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoetapa').val();
    entidad.CodigoTratoProspecto = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoTratoProspecto)) {
        alert("Falta seleccionar un trato.");
        return;
    }

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspActualizar_gimnasio_crm_3_tratosprospectoEtapa", request, metodoCorrecto, metodoError);

}

function event_guardartratosprospecto_prospectossinactividad() {
    var entidad = {};
    if ($('#hdCodigoTratoProspecto_prospectosSinCita').val() != '') {
        entidad.CodigoEmbudoVenta = ConvertToStringFromObject($('input[id="hdCodigoEmbudoVenta_prospectosSinCita"]').val());
    } else {
        entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_prospectosSinCita").data("kendoDropDownList").value();
    }

    entidad.CodigoEtapa = ConvertToStringFromObject($('input[id="hdCodigoEtapaVenta_prospectosSinCita"]').val());
    entidad.NombreTrato = ConvertToStringFromObject($('input[id="txtTituloTrato_prospectosSinCita"]').val());
    entidad.CodigoTratoProspecto = ConvertToStringFromObject($('input[id="hdCodigoTratoProspecto_prospectosSinCita"]').val());
    entidad.CodigoEstadoEtapa = 1;//ESTADO ABIERTO
    entidad.FechaPrevistaCierre = $("#txtFechaCierrePrevista_prospectosSinCita").data('kendoDatePicker').value();
    entidad.CodigoMoneda = 0;
    entidad.Valor = ConvertToStringFromObject($('input[id="txtPrecio_SocioIAgenda_view_Prospectossincita"]').val());
    if ($("#ddlVendedor_ProspectosSinCita").data("kendoDropDownList").value().toString().toUpperCase() == 'OTROSVENDEDORES') {
        entidad.Vendedor = getCookie("_Usuario_Business");
    } else {
        entidad.Vendedor = $('#hdVendedor_Prospectossincita').val();
    }

    entidad.CodigoOrigenProspecto = $('#hdCodigoOrigen_Prospectossincita').val();
    entidad.CodigoProspecto = $('#hdCodigoProspecto_Prospectossincita').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.NombreTrato)) {
        alert("Falta ingresar el nombre del trato.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.FechaPrevistaCierre)) {
        alert("Falta ingresar la fecha prevista del cierre de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto) || entidad.CodigoProspecto == 0) {
        alert("Falta seleccionar un prospecto.");
        return;
    }

    //alert('Origen: ' + entidad.CodigoOrigenProspecto);
    //return false;
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

            //event_cerrarmodalNuevoEmbudoVenta();
            //$('input[id="embudoVenta-txtNombre"]').val('');
            //$('input[id="embudoVenta-txtDescripcion"]').val('');
            //event_ListarDDLEmbudoVenta();
        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);

}

function event_guardartratosReagendar() {
    var entidad = {};

    if ($('#hdCodigoTratoProspecto_Reagendar').val() != '') {
        entidad.CodigoEmbudoVenta = ConvertToStringFromObject($('input[id="hdCodigoEmbudoVenta_Reagendar"]').val());
    } else {
        entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_Reagendar").data("kendoDropDownList").value();
    }

    entidad.CodigoEtapa = ConvertToStringFromObject($('input[id="hdCodigoEtapaVenta_Reagendar"]').val());
    entidad.NombreTrato = ConvertToStringFromObject($('input[id="txtTituloTrato_Reagendar"]').val());
    entidad.CodigoTratoProspecto = ConvertToStringFromObject($('input[id="hdCodigoTratoProspecto_Reagendar"]').val());;
    entidad.CodigoEstadoEtapa = 1;//ESTADO ABIERTO
    entidad.FechaPrevistaCierre = $("#txtFechaCierrePrevista_Reagendar").data('kendoDatePicker').value();

    entidad.CodigoMoneda = 0;
    entidad.Valor = $('#txtValor_Reagendar').val();
    entidad.Vendedor = $("#lblVendedor_Reagendar").html();

    entidad.CodigoOrigenProspecto = $('#hdCodigoTipoAgenda').val();
    entidad.CodigoProspecto = $('#hdCodigoSocio').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.NombreTrato)) {
        alert("Falta ingresar el nombre del trato.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.FechaPrevistaCierre)) {
        alert("Falta ingresar la fecha prevista del cierre de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto) || entidad.CodigoProspecto == 0) {
        alert("Falta seleccionar un prospecto.");
        return;
    }

    //alert('Origen: ' + entidad.CodigoOrigenProspecto);
    //return false;
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

            //event_cerrarmodalNuevoEmbudoVenta();
            //$('input[id="embudoVenta-txtNombre"]').val('');
            //$('input[id="embudoVenta-txtDescripcion"]').val('');
            //event_ListarDDLEmbudoVenta();
        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);

}

function event_guardartratosInactivos() {
    var entidad = {};

    if ($('#hdCodigoTratoProspecto_Inactivos').val() != '') {
        entidad.CodigoEmbudoVenta = ConvertToStringFromObject($('input[id="hdCodigoEmbudoVenta_Inactivos"]').val());
    } else {
        entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_Inactivos").data("kendoDropDownList").value();
    }

    entidad.CodigoEtapa = ConvertToStringFromObject($('input[id="hdCodigoEtapaVenta_Inactivos"]').val());
    entidad.NombreTrato = ConvertToStringFromObject($('input[id="txtTituloTrato_Inactivos"]').val());
    entidad.CodigoTratoProspecto = ConvertToStringFromObject($('input[id="hdCodigoTratoProspecto_Inactivos"]').val());;
    entidad.CodigoEstadoEtapa = 1;//ESTADO ABIERTO
    entidad.FechaPrevistaCierre = $("#txtFechaCierrePrevista_Inactivos").data('kendoDatePicker').value();

    entidad.CodigoMoneda = 0;
    entidad.Valor = $('#txtValor_Inactivos').val();
    entidad.Vendedor = $('#dllVendedorAgendaInactivo').data("kendoDropDownList").value();//getCookie("_Usuario_Business");//$("#lblVendedor_Inactivos").html();

    entidad.CodigoOrigenProspecto = $('#hdCodigoTipoAgenda').val();
    entidad.CodigoProspecto = $('#hdCodigoSocio').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.NombreTrato)) {
        alert("Falta ingresar el nombre del trato.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.FechaPrevistaCierre)) {
        alert("Falta ingresar la fecha prevista del cierre de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto) || entidad.CodigoProspecto == 0) {
        alert("Falta seleccionar un prospecto.");
        return;
    }

    //alert('Origen: ' + entidad.CodigoOrigenProspecto);
    //return false;
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);

}

function event_guardartratosRenovaciones() {
    var entidad = {};

    if ($('#hdCodigoTratoProspecto_Renovaciones').val() != '') {
        entidad.CodigoEmbudoVenta = ConvertToStringFromObject($('input[id="hdCodigoEmbudoVenta_Renovaciones"]').val());
    } else {
        entidad.CodigoEmbudoVenta = $("#ddlEmbudoVenta_Renovaciones").data("kendoDropDownList").value();
    }

    entidad.CodigoEtapa = ConvertToStringFromObject($('input[id="hdCodigoEtapaVenta_Renovaciones"]').val());
    entidad.NombreTrato = ConvertToStringFromObject($('input[id="txtTituloTrato_Renovaciones"]').val());
    entidad.CodigoTratoProspecto = ConvertToStringFromObject($('input[id="hdCodigoTratoProspecto_Renovaciones"]').val());;
    entidad.CodigoEstadoEtapa = 1;//ESTADO ABIERTO
    entidad.FechaPrevistaCierre = $("#txtFechaCierrePrevista_Renovaciones").data('kendoDatePicker').value();

    entidad.CodigoMoneda = 0;
    entidad.Valor = $('#txtValor_Renovaciones').val();
    entidad.Vendedor = $('#dllVendedorAgendaRenovaciones').data("kendoDropDownList").value();//$("#lblVendedor_Renovaciones").html();

    entidad.CodigoOrigenProspecto = $('#hdCodigoTipoAgenda').val();
    entidad.CodigoProspecto = $('#hdCodigoSocio').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.NombreTrato)) {
        alert("Falta ingresar el nombre del trato.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.FechaPrevistaCierre)) {
        alert("Falta ingresar la fecha prevista del cierre de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto) || entidad.CodigoProspecto == 0) {
        alert("Falta seleccionar un prospecto.");
        return;
    }

    //alert('Origen: ' + entidad.CodigoOrigenProspecto);
    //return false;
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);

}

function event_CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla_Oportunidades() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#oportunidades-ddlEmbudosVenta").data("kendoDropDownList").value();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        event_ListarDDLEtapaVenta_Oportunidades(msg);

        if (msg.length == 0) {

            var control = '';
            control += '<div class="alert alert-secondary" role="alert" style="font-size: 13px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas en ajustes.</div>';
            $('div[id="main_ControlEmbudosVentaOportunidades"]').html(control);
        } else {
            //aqui creacion de control
            var control = '';

            $.each(msg, function (index, item) {

                var i = (index + 1);
                control += '<div class="kanban-column kanban-column-oportunidades" data-kanbancolumnoportunidades="' + item.CodigoEtapa + '" >';
                control += '    <div class="kanban-column-header" style="background-color: #0075ff;border-bottom: 2px #0075ff solid;">';
                control += '       <center><b class="mb-0" style="font-size:14px;background-color: #0075ff;color: #fff;text-transform: uppercase;" >' + item.NombreEtapa + '</b></center>';
                control += '    </div>';
                control += '    <div class="kanban-items-container kanban-items-container-oportunidades scrollbar" onMouseOver="javascript:event_onMouseOver_EditarTrato_EmbudosVentaOportunidades_getcodigoetapa(this);" data-codigoetapa="' + item.CodigoEtapa + '" id="oportunidades-column-codigoetapa-' + item.CodigoEtapa + '" style="height: 790px;">';

                control += '    </div>';
                control += '    <div class="kanban-column-footer">';
                control += '        <center class="badge" style="font-size:18px;color:#000;font-weight: bold;" id="oportunidades-footer-codigoetapa-' + item.CodigoEtapa + '">S/ 0.00<center>';
                control += '    </div>';
                control += '</div>';


            });

            $('div[id="main_ControlEmbudosVentaOportunidades"]').html(control);
            event_CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto_Oportunidades();

        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_2_etapasplantilla", request, metodoCorrecto, metodoError);

}

function event_ListarDDLEtapaVenta_Oportunidades(msg) {

    if (msg.length == 0) {
        var control = '';
        control += '<div class="alert alert-secondary border-2 d-flex align-items-center" role="alert">';
        control += '  <p class="mb-0 flex-1" style="font-size:11px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas.</p>';
        control += '</div>';
        $('#ddlEtapaVenta_Reagendar_Oportunidades').html(control);
    } else {
        var control = '';
        for (var i = 0; i < msg.length; i++) {
            control += '<input type="checkbox" class="btn-check" data-codigoetapa="' + msg[i].CodigoEtapa + '" value="' + (i + 1) + '" id="chkEtapas_prospectosOportunidades_' + (i + 1) + '" name="chkEtapas_prospectosOportunidades" onclick="eventChange_chkEtapas_prospectosOportunidades(this)" >';
            control += '<label class="btn btn-outline-success btn-sm" for="chkEtapas_prospectosOportunidades_' + (i + 1) + '" data-bs-toggle="tooltip" data-bs-placement="bottom" title="' + msg[i].NombreEtapa + '" alt="' + msg[i].NombreEtapa + '" >' + (i + 1) + '</label>';
        }
        $('#ddlEtapaVenta_Reagendar_Oportunidades').html(control);

        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl)
        })
    }

}

function eventChange_chkEtapas_prospectosOportunidades(control) {
    //obtener el valor
    var valorClick = $(control).val();
    $('#hdCodigoEtapaVenta_Reagendar_Oportunidades').val($(control).attr('data-codigoetapa'));
    $('input[id="hdEditarTrato_EmbudosVentaOportunidades_getcodigoetapa"]').val($(control).attr('data-codigoetapa'));

    $('input:checkbox[name=chkEtapas_prospectosOportunidades]').each(function () {
        var valorControl = $(this).val();
        if (valorControl <= valorClick) {
            $(this).prop('checked', true);
        }
        else {
            $(this).prop('checked', false);
        }
    });

}

function event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_oportunidades(CodigoEmbudoVenta, CodigoTratoProspecto, CodigoOrigenProspecto, CodigoProspecto) {

    var entidad = {};
    entidad.CodigoEmbudoVenta = CodigoEmbudoVenta;
    entidad.CodigoTratoProspecto = CodigoTratoProspecto;
    entidad.CodigoOrigenProspecto = CodigoOrigenProspecto;
    entidad.CodigoProspecto = CodigoProspecto;

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoTratoProspecto)) {
        alert("Falta seleccionar un trato.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta enviar el origen del prospecto.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        $('#txtTituloTrato_Reagendar_Oportunidades').val(msg.NombreTrato);
        $('#txtValor_Reagendar_Oportunidades').val(msg.Valor);
        $('#txtFechaCierrePrevista_Reagendar_Oportunidades').data('kendoDatePicker').value(msg.FechaPrevistaCierre);
        $('#hdCodigoEtapaVenta_Reagendar_Oportunidades').val(msg.CodigoEtapa);
        $("#ddlEstadoTrato_Reagendar_Oportunidades").data("kendoDropDownList").value(msg.CodigoEstadoEtapa);

        //var valoretapa = 1;
        //data-codigoetapa

        $('input:checkbox[name=chkEtapas_prospectosOportunidades]').each(function () {
            $(this).prop('checked', false);
        });
        $('input:checkbox[name=chkEtapas_prospectosOportunidades]').each(function () {

            $(this).prop('checked', true);
            var valorControl = $(this).attr('data-codigoetapa');
            if (valorControl == msg.CodigoEtapa) {
                $(this).prop('checked', true);
                //valoretapa = $(this).val();
                return false;
            }

        });

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);

}

function event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_reagendar(CodigoOrigenProspecto, CodigoProspecto) {

    var entidad = {};
    entidad.CodigoOrigenProspecto = CodigoOrigenProspecto;
    entidad.CodigoProspecto = CodigoProspecto;

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto)) {
        alert("Falta seleccionar un prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg != 0) {

            $('#lblEstadoTieneTrato_Reagendar').html('Si tiene 1 trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_Reagendar").style.display = 'none';
            document.getElementById("divNuevo_EtapaEmbudo_Reagendar").style.display = 'none';
            document.getElementById("divEditar_EmbudoVenta_Reagendar").style.display = '';
            document.getElementById("divEditar_EtapaEmbudo_Reagendar").style.display = '';
            $('#txtNombreEmbudoVenta_Reagendar').val(msg.DesEmbudoVenta);
            $('#txtEtapaEmbudo_Reagendar').val(msg.NombreEtapa);
            $('#hdCodigoEmbudoVenta_Reagendar').val(msg.CodigoEmbudoVenta);
            $('#hdCodigoEtapaVenta_Reagendar').val(msg.CodigoEtapa);
            $('#hdCodigoTratoProspecto_Reagendar').val(msg.CodigoTratoProspecto);

            $('#txtTituloTrato_Reagendar').val(msg.NombreTrato);
            $('#txtValor_Reagendar').val(msg.Valor);
            $('#txtFechaCierrePrevista_Reagendar').data('kendoDatePicker').value(msg.FechaPrevistaCierre);

        } else {
            $('#lblEstadoTieneTrato_Reagendar').html('No tiene ningún trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_Reagendar").style.display = '';
            document.getElementById("divNuevo_EtapaEmbudo_Reagendar").style.display = '';
            document.getElementById("divEditar_EmbudoVenta_Reagendar").style.display = 'none';
            document.getElementById("divEditar_EtapaEmbudo_Reagendar").style.display = 'none';

            $('#txtNombreEmbudoVenta_Reagendar').val('');
            $('#txtEtapaEmbudo_Reagendar').val('');
            $('#hdCodigoEmbudoVenta_Reagendar').val('');
            $('#hdCodigoEtapaVenta_Reagendar').val('');
            $('#hdCodigoTratoProspecto_Reagendar').val('');

            $('#txtTituloTrato_Reagendar').val('');

        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto", request, metodoCorrecto, metodoError);

}

function event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_vencidos(CodigoOrigenProspecto, CodigoProspecto) {

    var entidad = {};
    entidad.CodigoOrigenProspecto = CodigoOrigenProspecto;
    entidad.CodigoProspecto = CodigoProspecto;

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto)) {
        alert("Falta seleccionar un prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg != 0) {

            $('#lblEstadoTieneTrato_Inactivos').html('Si tiene 1 trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_Inactivos").style.display = 'none';
            document.getElementById("divNuevo_EtapaEmbudo_Inactivos").style.display = 'none';
            document.getElementById("divEditar_EmbudoVenta_Inactivos").style.display = '';
            document.getElementById("divEditar_EtapaEmbudo_Inactivos").style.display = '';
            $('#txtNombreEmbudoVenta_Inactivos').val(msg.DesEmbudoVenta);
            $('#txtEtapaEmbudo_Inactivos').val(msg.NombreEtapa);
            $('#hdCodigoEmbudoVenta_Inactivos').val(msg.CodigoEmbudoVenta);
            $('#hdCodigoEtapaVenta_Inactivos').val(msg.CodigoEtapa);
            $('#hdCodigoTratoProspecto_Inactivos').val(msg.CodigoTratoProspecto);

            $('#txtTituloTrato_Inactivos').val(msg.NombreTrato);
            $('#txtValor_Inactivos').val(msg.Valor);
            $('#txtFechaCierrePrevista_Inactivos').data('kendoDatePicker').value(msg.FechaPrevistaCierre);

        } else {

            $('#lblEstadoTieneTrato_Inactivos').html('No tiene ningún trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_Inactivos").style.display = '';
            document.getElementById("divNuevo_EtapaEmbudo_Inactivos").style.display = '';
            document.getElementById("divEditar_EmbudoVenta_Inactivos").style.display = 'none';
            document.getElementById("divEditar_EtapaEmbudo_Inactivos").style.display = 'none';

            $('#txtNombreEmbudoVenta_Inactivos').val('');
            $('#txtEtapaEmbudo_Inactivos').val('');
            $('#hdCodigoEmbudoVenta_Inactivos').val('');
            $('#hdCodigoEtapaVenta_Inactivos').val('');
            $('#hdCodigoTratoProspecto_Inactivos').val('');

            $('#txtTituloTrato_Inactivos').val('');

        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto", request, metodoCorrecto, metodoError);

}

function event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_renovaciones(CodigoOrigenProspecto, CodigoProspecto) {

    var entidad = {};
    entidad.CodigoOrigenProspecto = CodigoOrigenProspecto;
    entidad.CodigoProspecto = CodigoProspecto;

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto)) {
        alert("Falta seleccionar un prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg != 0) {

            $('#lblEstadoTieneTrato_Renovaciones').html('Si tiene 1 trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_Renovaciones").style.display = 'none';
            document.getElementById("divNuevo_EtapaEmbudo_Renovaciones").style.display = 'none';
            document.getElementById("divEditar_EmbudoVenta_Renovaciones").style.display = '';
            document.getElementById("divEditar_EtapaEmbudo_Renovaciones").style.display = '';
            $('#txtNombreEmbudoVenta_Renovaciones').val(msg.DesEmbudoVenta);
            $('#txtEtapaEmbudo_Renovaciones').val(msg.NombreEtapa);
            $('#hdCodigoEmbudoVenta_Renovaciones').val(msg.CodigoEmbudoVenta);
            $('#hdCodigoEtapaVenta_Renovaciones').val(msg.CodigoEtapa);
            $('#hdCodigoTratoProspecto_Renovaciones').val(msg.CodigoTratoProspecto);

            $('#txtTituloTrato_Renovaciones').val(msg.NombreTrato);
            $('#txtValor_Renovaciones').val(msg.Valor);
            $('#txtFechaCierrePrevista_Renovaciones').data('kendoDatePicker').value(msg.FechaPrevistaCierre);

        } else {

            $('#lblEstadoTieneTrato_Renovaciones').html('No tiene ningún trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_Renovaciones").style.display = '';
            document.getElementById("divNuevo_EtapaEmbudo_Renovaciones").style.display = '';
            document.getElementById("divEditar_EmbudoVenta_Renovaciones").style.display = 'none';
            document.getElementById("divEditar_EtapaEmbudo_Renovaciones").style.display = 'none';

            $('#txtNombreEmbudoVenta_Renovaciones').val('');
            $('#txtEtapaEmbudo_Renovaciones').val('');
            $('#hdCodigoEmbudoVenta_Renovaciones').val('');
            $('#hdCodigoEtapaVenta_Renovaciones').val('');
            $('#hdCodigoTratoProspecto_Renovaciones').val('');

            $('#txtTituloTrato_Renovaciones').val('');

        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto", request, metodoCorrecto, metodoError);

}

function event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto_prospectosSinCita(CodigoOrigenProspecto, CodigoProspecto) {

    var entidad = {};
    entidad.CodigoOrigenProspecto = CodigoOrigenProspecto;
    entidad.CodigoProspecto = CodigoProspecto;

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoProspecto)) {
        alert("Falta seleccionar un prospecto.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoOrigenProspecto)) {
        alert("Falta seleccionar el origen del prospecto.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        if (msg != 0) {

            $('#lblEstadoTieneTrato_prospectossincita').html('Si tiene 1 trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_prospectosSinCita").style.display = 'none';
            document.getElementById("divNuevo_EtapaEmbudo_prospectosSinCita").style.display = 'none';
            document.getElementById("divEditar_EmbudoVenta_prospectosSinCita").style.display = '';
            document.getElementById("divEditar_EtapaEmbudo_prospectosSinCita").style.display = '';
            $('#txtNombreEmbudoVenta_prospectosSinCita').val(msg.DesEmbudoVenta);
            $('#txtEtapaEmbudo_prospectosSinCita').val(msg.NombreEtapa);
            $('#hdCodigoEmbudoVenta_prospectosSinCita').val(msg.CodigoEmbudoVenta);
            $('#hdCodigoEtapaVenta_prospectosSinCita').val(msg.CodigoEtapa);
            $('#hdCodigoTratoProspecto_prospectosSinCita').val(msg.CodigoTratoProspecto);

            $('#txtTituloTrato_prospectosSinCita').val(msg.NombreTrato);
            $('#txtValor_prospectosSinCita').val(msg.Valor);
            $('#txtFechaCierrePrevista_prospectosSinCita').data('kendoDatePicker').value(msg.FechaPrevistaCierre);

        } else {

            $('#lblEstadoTieneTrato_prospectossincita').html('No tiene ningún trato abierto');
            document.getElementById("divNuevo_EmbudoVenta_prospectosSinCita").style.display = '';
            document.getElementById("divNuevo_EtapaEmbudo_prospectosSinCita").style.display = '';
            document.getElementById("divEditar_EmbudoVenta_prospectosSinCita").style.display = 'none';
            document.getElementById("divEditar_EtapaEmbudo_prospectosSinCita").style.display = 'none';

            $('#txtNombreEmbudoVenta_prospectosSinCita').val('');
            $('#txtEtapaEmbudo_prospectosSinCita').val('');
            $('#hdCodigoEmbudoVenta_prospectosSinCita').val('');
            $('#hdCodigoEtapaVenta_prospectosSinCita').val('');
            $('#hdCodigoTratoProspecto_prospectosSinCita').val('');

            $('#txtTituloTrato_prospectosSinCita').val('');

        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_abierto", request, metodoCorrecto, metodoError);

}

function event_actualizartratosReagendar_Oportunidades() {
    var entidad = {};
    entidad.CodigoEmbudoVenta = ConvertToStringFromObject($('input[id="hdEditarTrato_EmbudosVentaOportunidades_getcodigoembudoventa"]').val());
    entidad.CodigoEtapa = ConvertToStringFromObject($('input[id="hdCodigoEtapaVenta_Reagendar_Oportunidades"]').val());
    entidad.NombreTrato = ConvertToStringFromObject($('input[id="txtTituloTrato_Reagendar_Oportunidades"]').val());
    entidad.CodigoTratoProspecto = ConvertToStringFromObject($('input[id="hdEditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto"]').val());
    entidad.CodigoEstadoEtapa = $("#ddlEstadoTrato_Reagendar_Oportunidades").data("kendoDropDownList").value();
    entidad.FechaPrevistaCierre = $("#txtFechaCierrePrevista_Reagendar_Oportunidades").data('kendoDatePicker').value();
    entidad.CodigoMoneda = 0;
    entidad.Valor = $('#txtValor_Reagendar_Oportunidades').val();

    if (entidad.CodigoEstadoEtapa == 1 || entidad.CodigoEstadoEtapa == 2) {
        entidad.Nota = '';
    } else {
        entidad.Nota = ConvertToStringFromObject($('input[id="txNota_Reagendar_Oportunidades"]').val());
    }

    entidad.Vendedor = $("#lblVendedor_Reagendar_Oportunidades").html();

    entidad.CodigoOrigenProspecto = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoorigenprospecto').val();
    entidad.CodigoProspecto = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoprospecto').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoEtapa)) {
        alert("Falta seleccionar una etapa.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.NombreTrato)) {
        alert("Falta ingresar el nombre del trato.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.FechaPrevistaCierre)) {
        alert("Falta ingresar la fecha prevista del cierre de venta.");
        return;
    } else if (entidad.CodigoEstadoEtapa == 3 || entidad.CodigoEstadoEtapa == 4) {
        //ESTADO TRATO
        //1	Abierto
        //2	Ganado
        //3	Perdido
        //4	Eliminado
        if (IsUndefinedOrNullOrEmpty(entidad.Nota)) {
            alert("Falta ingresar el motivo de perder o eliminar el trato.");
            return;
        }

    }

    //alert('Origen: ' + entidad.CodigoOrigenProspecto);
    //return false;
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspRegistrar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);

}

function event_actualizartratosEstadoReagendar_Oportunidades(CodigoEstadoEtapa) {
    var entidad = {};
    entidad.CodigoEmbudoVenta = ConvertToStringFromObject($('input[id="hdEditarTrato_EmbudosVentaOportunidades_getcodigoembudoventa"]').val());
    entidad.CodigoTratoProspecto = ConvertToStringFromObject($('input[id="hdEditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto"]').val());
    entidad.CodigoEstadoEtapa = CodigoEstadoEtapa;//ESTADO ABIERTO

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoTratoProspecto)) {
        alert("Falta seleccionar un trato.");
        return;
    }

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    var metodoCorrecto = function (msg) {

        if (msg.Success) {

            if (msg.MessageList[0].Codigo == 0) {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
            } else {
                $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'success', width: 'auto' });
            }

        }
        else {
            $.bootstrapGrowl(msg.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
        }

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/uspActualizar_gimnasio_crm_3_tratosprospectoEstado", request, metodoCorrecto, metodoError);

}

function event_CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto_Oportunidades() {

    var entidad = {};
    entidad.CodigoEmbudoVenta = $("#oportunidades-ddlEmbudosVenta").data("kendoDropDownList").value();
    entidad.Vendedor = $("#oportunidades-ddlEmbudosVenta_vendedores").data("kendoDropDownList").value();
    entidad.CodigoEstadoEtapa = $("#oportunidades-ddlEstadoTrato").data("kendoDropDownList").value();
    entidad.Nombres = $("#oportunidades-txtBusquedaProspectos").val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoEmbudoVenta)) {
        alert("Falta seleccionar un embudo de venta.");
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        //alert(msg.length);

        if (msg.length == 0) {

            //var control = '';
            //control += '<div class="alert alert-secondary" role="alert" style="font-size: 13px;">¡Ups! Al parecer todavía no has creado ninguna etapa de ventas. Te recomendamos encarecidamente que definas al menos 2 etapas de ventas en ajustes.</div>';
            //$('div[id="main_ControlEmbudosVentaOportunidades"]').html(control);

        } else {

            var TotalProyectado_General = 0;
            var CantidadTratos_General = 0;

            //aqui creacion de control de tratos
            $('.kanban-container-embudosventaoportunidades > div').each(function () {
                var control_codigoetapa = $(this).attr('data-kanbancolumnoportunidades');

                var control = '';
                var TotalProyectado = 0;
                var CantidadTratos = 0;
                $.each(msg, function (index, item) {

                    if (control_codigoetapa == item.CodigoEtapa) {

                        control += '<div class="kanban-item kanban-item-oportunidades" tabindex="0">';
                        control += '  <div class="card kanban-item-card" ondblclick="javascript:event_ondblclick_EditarTrato_EmbudosVentaOportunidades(this);"  onMouseOver="javascript:event_onMouseOver_EditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto(this);" data-codigoembudoventa="' + item.CodigoEmbudoVenta + '" data-codigotratoprospecto="' + item.CodigoTratoProspecto + '" data-codigoorigenprospecto="' + item.CodigoOrigenProspecto + '" data-codigoprospecto="' + item.CodigoProspecto + '" data-vendedor="' + item.Vendedor + '" data-nombres="' + item.Nombres + '" data-apellidos="' + item.Apellidos + '" data-celular="' + item.Celular + '">';
                        control += '      <div class="card-body" style="padding: 10px;border-left: 4px ' + item.ColorOrigenProspecto + ' solid;">';


                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 12px;">' + item.NombreTrato + '</p>';
                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 12px;">' + item.Nombres + ', ' + item.Apellidos + '</p>';
                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 11px;">Codigo: ' + item.CodigoProspecto + '</p>';
                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 12px;">Objetivo: ' + item.DesObjetivo + '</p>';
                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 11px;">Procedencia: ' + item.DesOrigenProspecto + '</p>';
                        if (item.CodigoOrigenProspecto != 2 && item.CodigoOrigenProspecto != 3) {
                            control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 11px;">Sub Procedencia: ' + item.DesComoConocioGym + '</p>';
                        }
                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 11px;">Fecha cierre: ' + item.DesFechaPrevistaCierre + '</p>';
                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 11px;">Valor: S/. ' + item.Valor + '</p>';
                        control += '          <p class="mb-0 fw-medium font-sans-serif" style="font-size: 11px;">Vendedor: ' + item.Vendedor + '</p>';

                        control += '          <div class="kanban-item-footer cursor-default">';
                        control += '              <div class="z-index-2" style="font-size: 13px;color: #000;font-weight: bold;">';
                        control += '                <span class="badge" style="font-size:12px;font-weight:bold;display:block;text-align: left;"><a target="_blank" style="color:#000;" href="https://api.whatsapp.com/send?phone=' + item.Celular + '"><img style="height:18px;" src="/Content/iconos/whatsapp-icono-negro.png">' + item.Celular + '</a></span>';
                        control += '              </div>';
                        control += '              <div class="z-index-2">';
                        control += '              <span style="background-color: ' + item.ColorEstadoActividad + ';border-radius: 19px;padding: 1px;color: #fff;width: 18px;height: 18px;cursor: pointer;" class="' + item.IconoEstadoActividad + ' me-2"></span>';
                        control += '              </div>';
                        control += '          </div>';
                        control += '      </div>';
                        control += '  </div>';
                        control += '</div>';

                        TotalProyectado += parseFloat(item.Valor);
                        TotalProyectado_General += parseFloat(item.Valor);
                        CantidadTratos++;
                        CantidadTratos_General++;
                    }

                });

                $('div[id="oportunidades-column-codigoetapa-' + control_codigoetapa + '"]').html(control);
                $('center[id="oportunidades-footer-codigoetapa-' + control_codigoetapa + '"]').html('💰 ' + TotalProyectado.toFixed(2) + ' - ' + CantidadTratos + ' Tratos');
            });

            $('#lblTotalProyeccion_oportunidades').html('Proyección ' + TotalProyectado_General + ' 💰');
            $('#lblTotalCantidadTratos_oportunidades').html(CantidadTratos_General + ' tratos');
            //docReady(draggableInit_EmbudosVentaOportunidades);
        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_uspListar_gimnasio_crm_3_tratosprospecto", request, metodoCorrecto, metodoError);
}

var draggableInit_EmbudosVentaOportunidades = function draggableInit_EmbudosVentaEditable() {
    var Selectors = {
        BODY: 'body',
        KANBAN_CONTAINER: '.kanban-container-embudosventaoportunidades',
        KABNBAN_COLUMN: '.kanban-column-oportunidades',
        KANBAN_ITEMS_CONTAINER: '.kanban-items-container-oportunidades',
        KANBAN_ITEM: '.kanban-item-oportunidades',
        ADD_CARD_FORM: '.add-card-form'
    };
    var Events = {
        DRAG_START: 'drag:start',
        DRAG_STOP: 'drag:stop'
    };
    var ClassNames = {
        FORM_ADDED: 'form-added'
    };
    var columns = document.querySelectorAll(Selectors.KABNBAN_COLUMN);
    var columnContainers = document.querySelectorAll(Selectors.KANBAN_ITEMS_CONTAINER);
    var container = document.querySelector(Selectors.KANBAN_CONTAINER);

    if (columnContainers.length) {

        // Initialize Sortable
        var sortable = new window.Draggable.Sortable(columnContainers, {
            draggable: Selectors.KANBAN_ITEM,
            delay: 200,
            mirror: {
                appendTo: Selectors.BODY,
                constrainDimensions: true
            },
            scrollable: {
                draggable: Selectors.KANBAN_ITEM,
                scrollableElements: [].concat(_toConsumableArray(columnContainers), [container])
            }
        }); // Hide form when drag start

        sortable.on(Events.DRAG_START, function () {

            var vendedortrato = $('#hdEditarTrato_EmbudosVentaOportunidades_getvendedor').val();
            var vendedorlogin = getCookie("_Usuario_Business");

            if (vendedortrato.toString().toUpperCase().trim() != vendedorlogin.toString().toUpperCase().trim()) {
                alert("Solo los vendedores deberian mover de etapa a los tratos.");
            }
            //else {
            //    alert("No tienes permiso para mover este trato a otra etapa.");
            //    return;
            //}

            columns.forEach(function (column) {
                utils.hasClass(column, ClassNames.FORM_ADDED) && column.classList.remove(ClassNames.FORM_ADDED);
            });

        }); // Place forms and other contents bottom of the sortable container

        sortable.on(Events.DRAG_STOP, function (_ref2) {
            //var codigoetapa = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoetapa').val();
            //var codigotratoprospecto = $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto').val();
            //alert('codigoetapa: ' + codigoetapa);
            //alert('codigotratoprospecto: ' + codigotratoprospecto);
            //ACTUALIZAR LA ETAPA DEL TRATO
            event_actualizaretapa_tratosprospecto();

            var el = _ref2.data.source;
            var columnContainer = el.closest(Selectors.KANBAN_ITEMS_CONTAINER);
            var form = columnContainer.querySelector(Selectors.ADD_CARD_FORM);
            !el.nextElementSibling && columnContainer.appendChild(form);
            alert(form);
        });
    }
};

function event_onMouseOver_EditarTrato_EmbudosVentaOportunidades_getcodigoetapa(control) {
    var codigoetapa = $(control).attr('data-codigoetapa');
    $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoetapa').val(codigoetapa);
}

function event_onMouseOver_EditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto(control) {

    var codigotratoprospecto = $(control).attr('data-codigotratoprospecto');
    var vendedor = $(control).attr('data-vendedor');

    $('#hdEditarTrato_EmbudosVentaOportunidades_getvendedor').val(vendedor);
    $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto').val(codigotratoprospecto);
}

function event_ondblclick_EditarTrato_EmbudosVentaOportunidades(control) {
    var nombres = $(control).attr('data-nombres');
    var apellidos = $(control).attr('data-apellidos');
    var vendedor = $(control).attr('data-vendedor');
    var celular = $(control).attr('data-celular');

    $('#lblInfNombre_Oportunidades').html(nombres.toUpperCase() + ',  ' + apellidos.toUpperCase());
    $('#lblVendedor_Reagendar_Oportunidades').html(vendedor);

    if (celular != '') {
        $('#imginfoCelular_CitasPendientes_Oportunidades').attr('href', 'https://api.whatsapp.com/send?phone=' + celular);
    } else {
        $('#imginfoCelular_CitasPendientes_Oportunidades').attr('href', 'https://api.whatsapp.com/');
    }

    var CodigoEmbudoVenta = $(control).attr('data-codigoembudoventa');
    var CodigoTratoProspecto = $(control).attr('data-codigotratoprospecto');
    var CodigoOrigenProspecto = $(control).attr('data-codigoorigenprospecto');
    var CodigoProspecto = $(control).attr('data-codigoprospecto');

    $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoembudoventa').val(CodigoEmbudoVenta);
    $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigotratoprospecto').val(CodigoTratoProspecto);
    $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoorigenprospecto').val(CodigoOrigenProspecto);
    $('#hdEditarTrato_EmbudosVentaOportunidades_getcodigoprospecto').val(CodigoProspecto);
    event_CentroEntrenamiento_uspBuscar_gimnasio_crm_3_tratosprospecto_oportunidades(CodigoEmbudoVenta, CodigoTratoProspecto, CodigoOrigenProspecto, CodigoProspecto);
    event_ListarHistorialActividades_Oportunidades(CodigoProspecto, CodigoOrigenProspecto);
    event_uspListar_gimnasio_crm_4_etapahistorial(CodigoEmbudoVenta, CodigoTratoProspecto);
    $('#modalReagendarAgenda_Oportunidades').show('fast');
}

//AJUSTES
function evemt_changeMenu(valor) {

    if (valor == 1) { //INTERESES
        $('#liConfiguracion_Intereses').addClass('active');
        $('#liConfiguracion_Embudos').removeClass('active');
        document.getElementById("DivConfiguracion_Intereses").style.display = '';
        document.getElementById("DivConfiguracion_Embudos").style.display = 'none';
        event_abrirmodalIntereses();
    } else if (valor == 2) { //EMBUDOS
        $('#liConfiguracion_Intereses').removeClass('active');
        $('#liConfiguracion_Embudos').addClass('active');
        document.getElementById("DivConfiguracion_Intereses").style.display = 'none';
        document.getElementById("DivConfiguracion_Embudos").style.display = '';
        event_ListarDDLEmbudoVenta();
    }
}

//ESTADISTICAS MATRICULADOS
function uspEstadisticaVentasPorTiempoMembresia_Ventas() {

    var FechaInicio = kendo.toString($("#FechaDesdeCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#FechaHastaCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var AsesorDeVentas = $("#ddlUsuarioCreador_Matriculados").data("kendoDropDownList").value();
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';

    var tiempo = [];
    var ventamembresia = [];

    $.ajax({
        data: '{"FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","AsesorComercial":"' + AsesorDeVentas + '"}',
        type: "POST",
        url: "/gestionce/uspEstadisticaVentasPorTiempoMembresia_Ventas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            for (var i in msg) {
                tiempo.push(msg[i].DescripcionProducto);
                ventamembresia.push(msg[i].TotalNeto);
            }
            var canvas = document.getElementById('floarChart_uspEstadisticaVentasPorTiempoMembresia_Ventas');
            var ctx = canvas.getContext('2d');
            ctx.clearRect(10, 10, canvas.width, canvas.height);
            ctx.clearRect(140, 140, canvas.width, canvas.height);
            ctx.clearRect(270, 270, canvas.width, canvas.height);

            var myChart = new Chart(ctx, {
                type: 'horizontalBar',
                data: {
                    labels: tiempo,
                    datasets: [{
                        label: 'Tiempo',
                        data: ventamembresia,
                        backgroundColor: [
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)'
                        ],
                        borderColor: [
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: 'VENTAS POR TIEMPO DE MEMBRESIA'
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });

        }, complete: function () {
            tiempo = [];
            ventamembresia = [];
            $('button[type="button"]').attr("disabled", false);
            document.getElementById('loadMe').style.display = 'none';
            uspEstadisticaMatriculadosPorNombrePlan();
        }
    });

}

function uspEstadisticaMatriculadosPorNombrePlan() {

    var FechaInicio = kendo.toString($("#FechaDesdeCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#FechaHastaCierre").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var AsesorDeVentas = $("#ddlUsuarioCreador_Matriculados").data("kendoDropDownList").value();
    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';

    var plan = [];
    var venta = [];
    var cantidad = [];

    $.ajax({
        data: '{"FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","AsesorComercial":"' + AsesorDeVentas + '"}',
        type: "POST",
        url: "/gestionce/uspEstadisticaMatriculadosPorNombrePlan",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            for (var i in msg) {
                plan.push(msg[i].DescripcionProducto);
                venta.push(msg[i].TotalNeto);
                cantidad.push(msg[i].Cantidad);
            }

            var canvas = document.getElementById('floarChart_uspEstadisticaMatriculadosPorNombrePlan');
            var ctx = canvas.getContext('2d');
            ctx.clearRect(10, 10, canvas.width, canvas.height);
            ctx.clearRect(140, 140, canvas.width, canvas.height);
            ctx.clearRect(270, 270, canvas.width, canvas.height);

            var myChart = new Chart(ctx, {
                type: 'horizontalBar',
                data: {
                    labels: plan,
                    datasets: [{
                        label: 'Ventas',
                        data: venta,
                        backgroundColor: [
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)'
                        ],
                        borderColor: [
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)',
                            'rgb(255, 28, 6)'
                        ],
                        borderWidth: 1
                    }, {
                        label: 'Cantidad',
                        data: cantidad,
                        backgroundColor: [
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)'
                        ],
                        borderColor: [
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)',
                            'rgb(255 171 40)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    title: {
                        display: true,
                        text: 'RANKING DE PLANES MEMBRESIAS'
                    },
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });

        }, complete: function () {
            tiempo = [];
            ventamembresia = [];
            $('button[type="button"]').attr("disabled", false);
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

function uspListarMetricas_ConversionLeads_Totales() {
    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        type: "POST",
        url: "/gestionce/uspListarMetricas_ConversionLeads_Totales",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var control = '';

            var tabla_kpiConversionLeads_totalkalking = 0;
            var tabla_kpiConversionLeads_totalprospeccion = 0;
            var tabla_kpiConversionLeads_totaldigital = 0;
            var tabla_kpiConversionLeads_totalllamadaentr = 0;
            var tabla_kpiConversionLeads_totalporvencer = 0;
            var tabla_kpiConversionLeads_totalvencidos = 0;

            var tabla_kpiConversionLeads_totalcontactos = 0;
            var tabla_kpiConversionLeads_totalcitas = 0;
            var tabla_kpiConversionLeads_totalporreunion = 0;
            var tabla_kpiConversionLeads_totalporcierres = 0;
            var tabla_kpiConversionLeads_totalventas = 0;

            for (var i = 0; i < msg.length; i++) {

                control += '<tr class="border-bottom border-200">';
                control += '<td class="align-middle text-center">' + msg[i].NombreCompleto + '</td>';
                control += '<td class="align-middle text-center">' + msg[i].ConversionLeads_CantidadWalking + '</td>';
                control += '<td class="align-middle text-center">' + msg[i].ConversionLeads_CantidadProspeccion + '</td>';
                control += '<td class="align-middle text-center">' + msg[i].ConversionLeads_CantidadDigital + '</td>';
                control += '<td class="align-middle text-center">' + msg[i].ConversionLeads_CantidadLlamadaE + '</td>';
                control += '<td class="align-middle text-center">' + msg[i].ConversionLeads_CantidadRenovaciones + '</td>';
                control += '<td class="align-middle text-center">' + msg[i].ConversionLeads_CantidadReinscripciones + '</td>';

                control += '<td class="align-middle text-center" style="font-weight: bold;">' + msg[i].ConversionLeads_CantidadTotalBD + '</td>';
                control += '<td class="align-middle text-center" style="font-weight: bold;">' + msg[i].ConversionLeads_CantidadTotalActividadesCitas + '</td>';
                control += '<td class="align-middle text-center" style="font-weight: bold;">' + msg[i].ConversionLeads_CantidadTotalActividadesReunion + '</td>';
                control += '<td class="align-middle text-center" style="font-weight: bold;">' + msg[i].ConversionLeads_CantidadClientesVenta + '</td>';
                control += '<td class="align-middle text-center" style="font-weight: bold;">' + msg[i].ConversionLeads_VentaTotal + '</td>';
                control += '</tr>';


                tabla_kpiConversionLeads_totalkalking += parseFloat(msg[i].ConversionLeads_CantidadWalking);
                tabla_kpiConversionLeads_totalprospeccion += parseFloat(msg[i].ConversionLeads_CantidadProspeccion);
                tabla_kpiConversionLeads_totaldigital += parseFloat(msg[i].ConversionLeads_CantidadDigital);
                tabla_kpiConversionLeads_totalllamadaentr += parseFloat(msg[i].ConversionLeads_CantidadLlamadaE);
                tabla_kpiConversionLeads_totalporvencer += parseFloat(msg[i].ConversionLeads_CantidadRenovaciones);
                tabla_kpiConversionLeads_totalvencidos += parseFloat(msg[i].ConversionLeads_CantidadReinscripciones);

                tabla_kpiConversionLeads_totalcontactos += parseFloat(msg[i].ConversionLeads_CantidadTotalBD);
                tabla_kpiConversionLeads_totalcitas += parseFloat(msg[i].ConversionLeads_CantidadTotalActividadesCitas);
                tabla_kpiConversionLeads_totalporreunion += parseFloat(msg[i].ConversionLeads_CantidadTotalActividadesReunion);
                tabla_kpiConversionLeads_totalporcierres += parseFloat(msg[i].ConversionLeads_CantidadClientesVenta);
                tabla_kpiConversionLeads_totalventas += parseFloat(msg[i].ConversionLeads_VentaTotal);

            }

            $('#tabla_kpiConversionLeads').html(control);

            $('#tabla_kpiConversionLeads_totalkalking').html(parseFloat(tabla_kpiConversionLeads_totalkalking).toFixed(0));
            $('#tabla_kpiConversionLeads_totalprospeccion').html(parseFloat(tabla_kpiConversionLeads_totalprospeccion).toFixed(0));
            $('#tabla_kpiConversionLeads_totaldigital').html(parseFloat(tabla_kpiConversionLeads_totaldigital).toFixed(0));
            $('#tabla_kpiConversionLeads_totalllamadaentr').html(parseFloat(tabla_kpiConversionLeads_totalllamadaentr).toFixed(0));
            $('#tabla_kpiConversionLeads_totalporvencer').html(parseFloat(tabla_kpiConversionLeads_totalporvencer).toFixed(0));
            $('#tabla_kpiConversionLeads_totalvencidos').html(parseFloat(tabla_kpiConversionLeads_totalvencidos).toFixed(0));

            $('#tabla_kpiConversionLeads_totalcontactos').html(parseFloat(tabla_kpiConversionLeads_totalcontactos).toFixed(0));
            $('#tabla_kpiConversionLeads_totalcitas').html(parseFloat(tabla_kpiConversionLeads_totalcitas).toFixed(0));
            $('#tabla_kpiConversionLeads_totalporreunion').html(parseFloat(tabla_kpiConversionLeads_totalporreunion).toFixed(0));
            $('#tabla_kpiConversionLeads_totalporcierres').html(parseFloat(tabla_kpiConversionLeads_totalporcierres).toFixed(0));
            $('#tabla_kpiConversionLeads_totalventas').html(parseFloat(tabla_kpiConversionLeads_totalventas).toFixed(2));

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}