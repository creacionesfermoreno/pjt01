//INICIO RESERVAS

function mostraModalAforo() {
    $('#myModalAforoActual').show('fast');
}

function ObtenerDiaSemana(dia) {
    var NombreSemana = "";
    switch (dia) {
        case 1: NombreSemana = 'DOMINGO'; break;
        case 2: NombreSemana = 'LUNES'; break;
        case 3: NombreSemana = 'MARTES'; break;
        case 4: NombreSemana = 'MIÉRCOLES'; break;
        case 5: NombreSemana = 'JUEVES'; break;
        case 6: NombreSemana = 'VIERNES'; break;
        case 7: NombreSemana = 'SÁBADO'; break;
        default: NombreSemana = 'ERROR'; break;
    }
    return NombreSemana;
}

function buscarReservas() {

    var TipoSala = $("#ddlTipoSala").data('kendoDropDownList').value();

    if (TipoSala == 1) {

        document.getElementById("ventanaSalaMaquinas").style.display = 'none';
        document.getElementById("ventanaSalaGrupal").style.display = '';

        document.getElementById("divClasesMaquinas_Fechas").style.display = 'none';
        document.getElementById("divClasesGrupales_Fechas").style.display = '';

    } else if (TipoSala >= 100) {

        document.getElementById("ventanaSalaMaquinas").style.display = '';
        document.getElementById("ventanaSalaGrupal").style.display = 'none';

        document.getElementById("divClasesMaquinas_Fechas").style.display = '';
        document.getElementById("divClasesGrupales_Fechas").style.display = 'none';

    }

    Obtener3FechasTodo();

}

function Obtener3FechasTodo() {

    document.getElementById('loadMe').style.display = 'block';

    var fecha = new Date();
    var entidad = {};
    entidad.FechaHoraReserva = kendo.toString(fecha, 'MM/dd/yyyy');
    entidad.CodigoSocio = $('#hdCodigo').val();
    entidad.CodigoMembresia = $('#hdCodigoMembresiaOrigen').val();
    entidad.CodigoPaquete = $('#hdCodigoPaqueteOrigen').val();

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoMembresia == '' || entidad.CodigoMembresia == 0) {
        $.bootstrapGrowl("Falta seleccionar una membresia.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoPaquete == '' || entidad.CodigoPaquete == 0) {
        $.bootstrapGrowl("Falta seleccionar una membresia.", { type: 'danger', width: 'auto' });
        return;
    }

    var metodoCorrecto = function (data) {

        var semana1 = ObtenerDiaSemana(data.DiaSemanaHoy);
        var semana2 = ObtenerDiaSemana(data.DiaSemana1);
        var semana3 = ObtenerDiaSemana(data.DiaSemana2);

        var content_claseGrupalesFechas = new Array();
        //FECHA HOY
        content_claseGrupalesFechas.push('<div class="col-sm-4" >');
        content_claseGrupalesFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseGrupalesFechas.push('<div id="divGrupales_Hoy" class="seleccionado" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(this,' + data.DiaSemanaHoy + ',' + undefined + ',' + data.validacionTieneReservaHoy + ');" data-fecha="' + data.FechaHoyTextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaHoy + '" >');
        content_claseGrupalesFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana1 + ' - ' + data.FechaHoyTexto + '</b>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        //FECHA DESPUES 1
        content_claseGrupalesFechas.push('<div class="col-sm-4" >');
        content_claseGrupalesFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseGrupalesFechas.push('<div id="divGrupales_Despues1" class="dseleccionado" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(this,' + data.DiaSemana1 + ',' + undefined + ',' + data.validacionTieneReservaDespues1 + ');" data-fecha="' + data.FechaDespues1TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues1 + '" >');
        content_claseGrupalesFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana2 + ' - ' + data.FechaDespues1Texto + '</b>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        //FECHA DESPUES 2
        content_claseGrupalesFechas.push('<div class="col-sm-4" >');
        content_claseGrupalesFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseGrupalesFechas.push('<div id="divGrupales_Despues2" class="dseleccionado" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(this,' + data.DiaSemana2 + ',' + undefined + ',' + data.validacionTieneReservaDespues2 + ');" data-fecha="' + data.FechaDespues2TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues2 + '" >');
        content_claseGrupalesFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana3 + ' - ' + data.FechaDespues2Texto + '</b>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');

        $('#divClasesGrupales_Fechas').html(content_claseGrupalesFechas.join(' '));

        //-------CLASES MAQUINAS
        if ($('#hdflag_consalamaquinas').val() == '1') {

            var content_claseFechas = new Array();
            //FECHA HOY
            content_claseFechas.push('<div class="col-4" >');
            content_claseFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
            content_claseFechas.push('<div id="divMaquinas_Hoy" class="seleccionado" style="text-align:center;padding:3px;border-radius: 5px;border: 0px #ccc solid;" onclick="javascript:uspListarPresencial_SalaMaquinas_HorarioTemporal(this,' + data.DiaSemanaHoy + ',' + undefined + ',' + data.validacionTieneReservaHoy + ');" data-fecha="' + data.FechaHoyTextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaHoy + '"  >');
            content_claseFechas.push('<b class="tx-center tx-14" style="font-weight:bold;">' + semana1 + ' - ' + data.FechaHoyTexto + '</b>');
            content_claseFechas.push('</div>');
            content_claseFechas.push('</div>');
            content_claseFechas.push('</div>');
            //FECHA DESPUES 1
            content_claseFechas.push('<div class="col-4" >');
            content_claseFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
            content_claseFechas.push('<div id="divMaquinas_Despues1" class="dseleccionado" style="text-align:center;padding:3px;border-radius: 5px;border: 0px #ccc solid;" onclick="javascript:uspListarPresencial_SalaMaquinas_HorarioTemporal(this,' + data.DiaSemana1 + ',' + undefined + ',' + data.validacionTieneReservaDespues1 + ');" data-fecha="' + data.FechaDespues1TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues1 + '" >');
            content_claseFechas.push('<b class="tx-center tx-14" style="font-weight:bold;">' + semana2 + ' - ' + data.FechaDespues1Texto + '</b>');
            content_claseFechas.push('</div>');
            content_claseFechas.push('</div>');
            content_claseFechas.push('</div>');
            //FECHA DESPUES 2
            content_claseFechas.push('<div class="col-4" >');
            content_claseFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
            content_claseFechas.push('<div id="divMaquinas_Despues2" class="dseleccionado" style="text-align:center;padding:3px;border-radius: 5px;border: 0px #ccc solid;" onclick="javascript:uspListarPresencial_SalaMaquinas_HorarioTemporal(this,' + data.DiaSemana2 + ',' + undefined + ',' + data.validacionTieneReservaDespues2 + ');" data-fecha="' + data.FechaDespues2TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues2 + '" >');
            content_claseFechas.push('<b class="tx-center tx-14" style="font-weight:bold;">' + semana3 + ' - ' + data.FechaDespues2Texto + '</b>');
            content_claseFechas.push('</div>');
            content_claseFechas.push('</div>');
            content_claseFechas.push('</div>');

            $('#divClasesMaquinas_Fechas').html(content_claseFechas.join(' '));

            var TipoSala = $("#ddlTipoSala").data('kendoDropDownList').value();
            if (TipoSala == 1) {

                uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(undefined, data.DiaSemanaHoy, data.FechaHoyTextoParametro, data.validacionTieneReservaHoy);

            } else if (TipoSala >= 100) {

                uspListarPresencial_SalaMaquinas_HorarioTemporal(undefined, data.DiaSemanaHoy, data.FechaHoyTextoParametro, data.validacionTieneReservaHoy);

            }

        } else {
            uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(undefined, data.DiaSemanaHoy, data.FechaHoyTextoParametro, data.validacionTieneReservaHoy);
        }

        document.getElementById('loadMe').style.display = 'none';
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/gestionce/CentroEntrenamiento_uspObtenerFechasReservas_Configuracion', request, metodoCorrecto, metodoError);

}

function Obtener3FechasClasesGrupales(fechaSeleccionada) {
    document.getElementById('loadMe').style.display = 'block';
    var fecha = new Date();
    var entidad = {};
    entidad.FechaHoraReserva = kendo.toString(fecha, 'MM/dd/yyyy');
    entidad.CodigoSocio = $('#hdCodigo').val();
    entidad.CodigoMembresia = $('#hdCodigoMembresiaOrigen').val();
    entidad.CodigoPaquete = $('#hdCodigoPaqueteOrigen').val();

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoMembresia == '' || entidad.CodigoMembresia == 0) {
        $.bootstrapGrowl("Falta seleccionar una membresia.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoPaquete == '' || entidad.CodigoPaquete == 0) {
        $.bootstrapGrowl("Falta seleccionar una membresia.", { type: 'danger', width: 'auto' });
        return;
    }

    var metodoCorrecto = function (data) {

        var semana1 = ObtenerDiaSemana(data.DiaSemanaHoy);
        var semana2 = ObtenerDiaSemana(data.DiaSemana1);
        var semana3 = ObtenerDiaSemana(data.DiaSemana2);

        var content_claseGrupalesFechas = new Array();

        //FECHA HOY
        var cssClase = fechaSeleccionada == data.FechaHoyTextoParametro ? "seleccionado" : "dseleccionado";
        if (cssClase == "seleccionado") {
            uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(undefined, data.DiaSemanaHoy, fechaSeleccionada, data.validacionTieneReservaHoy);
        }
        content_claseGrupalesFechas.push('<div class="col-sm-4" >');
        content_claseGrupalesFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseGrupalesFechas.push('<div id="divGrupales_Hoy" class="' + cssClase + '" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(this,' + data.DiaSemanaHoy + ',' + undefined + ',' + data.validacionTieneReservaHoy + ');" data-fecha="' + data.FechaHoyTextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaHoy + '" >');
        content_claseGrupalesFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana1 + ' - ' + data.FechaHoyTexto + '</b>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        //FECHA DESPUES 1
        var cssClase = fechaSeleccionada == data.FechaDespues1TextoParametro ? "seleccionado" : "dseleccionado";
        if (cssClase == "seleccionado") {
            uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(undefined, data.DiaSemana1, fechaSeleccionada, data.validacionTieneReservaDespues1);
        }
        content_claseGrupalesFechas.push('<div class="col-sm-4" >');
        content_claseGrupalesFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseGrupalesFechas.push('<div id="divGrupales_Despues1" class="' + cssClase + '" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(this,' + data.DiaSemana1 + ',' + undefined + ',' + data.validacionTieneReservaDespues1 + ');" data-fecha="' + data.FechaDespues1TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues1 + '" >');
        content_claseGrupalesFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana2 + ' - ' + data.FechaDespues1Texto + '</b>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        //FECHA DESPUES 2
        var cssClase = fechaSeleccionada == data.FechaDespues2TextoParametro ? "seleccionado" : "dseleccionado";
        if (cssClase == "seleccionado") {
            uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(undefined, data.DiaSemana2, fechaSeleccionada, data.validacionTieneReservaDespues2);
        }
        content_claseGrupalesFechas.push('<div class="col-sm-4">');
        content_claseGrupalesFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;" >');
        content_claseGrupalesFechas.push('<div id="divGrupales_Despues2" class="' + cssClase + '" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(this,' + data.DiaSemana2 + ',' + undefined + ',' + data.validacionTieneReservaDespues2 + ');" data-fecha="' + data.FechaDespues2TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues2 + '" >');
        content_claseGrupalesFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana3 + ' - ' + data.FechaDespues2Texto + '</b>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');
        content_claseGrupalesFechas.push('</div>');

        $('#divClasesGrupales_Fechas').html(content_claseGrupalesFechas.join(' '));

        //-------CLASES MAQUINAS
        var content_claseFechas = new Array();
        //FECHA HOY
        content_claseFechas.push('<div class="col-sm-4" >');
        content_claseFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseFechas.push('<div id="divMaquinas_Hoy" class="seleccionado" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_SalaMaquinas_HorarioTemporal(this,' + data.DiaSemanaHoy + ',' + undefined + ',' + data.validacionTieneReservaHoy + ');" data-fecha="' + data.FechaHoyTextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaHoy + '"  >');
        content_claseFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana1 + ' - ' + data.FechaHoyTexto + '</b>');
        content_claseFechas.push('</div>');
        content_claseFechas.push('</div>');
        content_claseFechas.push('</div>');
        //FECHA DESPUES 1
        content_claseFechas.push('<div class="col-sm-4" >');
        content_claseFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseFechas.push('<div id="divMaquinas_Despues1" class="dseleccionado" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_SalaMaquinas_HorarioTemporal(this,' + data.DiaSemana1 + ',' + undefined + ',' + data.validacionTieneReservaDespues1 + ');" data-fecha="' + data.FechaDespues1TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues1 + '" >');
        content_claseFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana2 + ' - ' + data.FechaDespues1Texto + '</b>');
        content_claseFechas.push('</div>');
        content_claseFechas.push('</div>');
        content_claseFechas.push('</div>');
        //FECHA DESPUES 2
        content_claseFechas.push('<div class="col-sm-4" >');
        content_claseFechas.push('<div class="card-contact pd-t-1 pd-r-1 pd-b-1 pd-l-1 clase" style="border-radius: 5px;">');
        content_claseFechas.push('<div id="divMaquinas_Despues2" class="dseleccionado" style="text-align:center;padding:3px;border-radius: 5px;" onclick="javascript:uspListarPresencial_SalaMaquinas_HorarioTemporal(this,' + data.DiaSemana2 + ',' + undefined + ',' + data.validacionTieneReservaDespues2 + ');" data-fecha="' + data.FechaDespues2TextoParametro + '" data-validacionTieneReserva="' + data.validacionTieneReservaDespues2 + '" >');
        content_claseFechas.push('<b class="tx-center tx-13" style="font-weight:bold;">' + semana3 + ' - ' + data.FechaDespues2Texto + '</b>');
        content_claseFechas.push('</div>');
        content_claseFechas.push('</div>');
        content_claseFechas.push('</div>');

        $('#divClasesMaquinas_Fechas').html(content_claseFechas.join(' '));

        document.getElementById('loadMe').style.display = 'none';
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/gestionce/CentroEntrenamiento_uspObtenerFechasReservas_Configuracion', request, metodoCorrecto, metodoError);

}

function ocultarAviso() {
    $('#modalAviso').hide('fast');
    $('#modalConfirmarReserva').hide('fast');
    $('#modal-info').hide('fast');
    $('#myModalAforoActual').hide('fast');
}

function mostrarAviso() {
    $('#modalAviso').show('fast');
}

//HORARIOS SALA GRUPALES
function uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(control, DiaNumero, fecha, validacionTieneReserva) {
    document.getElementById('loadMe').style.display = 'block';
    if (control != undefined) {

        if ($(control).attr('id') == 'divGrupales_Hoy') {
            $('#divGrupales_Hoy').addClass('seleccionado');
            $('#divGrupales_Hoy').removeClass('dseleccionado');
            $('#divGrupales_Despues1').removeClass('seleccionado');
            $('#divGrupales_Despues2').removeClass('seleccionado');

            $('#divGrupales_Despues1').addClass('dseleccionado');
            $('#divGrupales_Despues2').addClass('dseleccionado');

        } else if ($(control).attr('id') == 'divGrupales_Despues1') {
            $('#divGrupales_Despues1').addClass('seleccionado');
            $('#divGrupales_Despues1').removeClass('dseleccionado');
            $('#divGrupales_Hoy').removeClass('seleccionado');
            $('#divGrupales_Despues2').removeClass('seleccionado');

            $('#divGrupales_Hoy').addClass('dseleccionado');
            $('#divGrupales_Despues2').addClass('dseleccionado');

        } else if ($(control).attr('id') == 'divGrupales_Despues2') {
            $('#divGrupales_Despues2').addClass('seleccionado');
            $('#divGrupales_Despues2').removeClass('dseleccionado');
            $('#divGrupales_Despues1').removeClass('seleccionado');
            $('#divGrupales_Hoy').removeClass('seleccionado');

            $('#divGrupales_Despues1').addClass('dseleccionado');
            $('#divGrupales_Hoy').addClass('dseleccionado');
        }
    }

    if (fecha == undefined) {
        fecha = $(control).attr('data-fecha');
    }

    var entidad = {};
    entidad.DiaNumero = DiaNumero;
    entidad.FechaHoraReserva = fecha;
    entidad.CodigoSocio = $('#hdCodigo').val();

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    }

    var metodoCorrecto = function (data) {
        document.getElementById('loadMe').style.display = 'none';
        $('#gridClasesGrupales').html('');

        if (data.length > 0) {
            var validacionTieneReservaFecha = validacionTieneReserva;
            var content_clase = new Array();
            for (var i = 0; i < data.length; i++) {

                content_clase.push('<div class="col-lg-3 col-md-2  mg-t-5" style="background-color: #fff;padding-bottom: 10px;" >');
                content_clase.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 3px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:2px;border-radius: 25px;">');
                content_clase.push('<b class="tx-center tx-16" style="margin-top: 8px;font-weight:bold;" title="CODIGO-CONFI: ' + data[i].CodigoHorarioClasesConfiguracion + '  CODIGO-REAL: ' + data[i].CodigoHorarioClasesTiempoReal + '">' + data[i].HoraInicioTexto + ' - ' + data[i].HoraFinTexto + '</b></br>');
                content_clase.push('<b class="tx-center tx-12" style="font-weight: bold;">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + data[i].NombreProfesionalFitness + '</b>');
                content_clase.push('</div>');
                content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;font-weight: bold;">');
                content_clase.push('<b class="tx-center" style="font-size:13px;margin-top: 15px;">AFORO: ' + data[i].CantidadPlazas + '</b></br>');
                content_clase.push('<b class="tx-center" style="font-size:13px;margin-top: 11px;">' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + '</b>');

                content_clase.push('</div>');

                if (data[i].CodigoHorarioClasesConfiguracionAsistencias != '' && data[i].CodigoHorarioClasesConfiguracionAsistencias != null) {

                    if (data[i].validacionCancelarCita == 1) { //1= puede cancelar la cita
                        content_clase.push('<div data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data[i].CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data[i].CodigoSocio + '" data-fecha="' + fecha + '"  onclick="javascript:CancelarCitaClaseGrupal(this);" style="background-color:red;color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                        content_clase.push('<b class="tx-center" style="font-size:13px;padding-top: 12px;">CANCELAR</b>');
                        content_clase.push('</div>');
                    } else if (data[i].validacionCancelarCita == 2) {//2 = NO puede cancelar la cita
                        content_clase.push('<div style="background-color:#0075ff;color:#000;text-align:center;padding:5px;border-radius: 12px;"> ');
                        content_clase.push('<b class="tx-center" style="font-size:13px;padding-top: 12px;">RESERVADO</b>');
                        content_clase.push('</div>');
                    }

                } else {
                    //VALIDAMOS SI TIENE RESERVA NO PUEDE RESERVAR OTROS HORARIOS DURANTE EL DIA
                    if (validacionTieneReservaFecha > 0) {
                        content_clase.push('<div onclick="mostrarAviso();" disabled style="background-color:#808080;color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                        content_clase.push('<b class="tx-center" style="font-size:13px;padding-top: 12px;">RESERVAR</b>');
                        content_clase.push('</div>');
                    } else {
                        if (data[i].CantidadPlazas <= 0) {
                            content_clase.push('<div disabled style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                            content_clase.push('<b class="tx-center" style="font-size:13px;padding-top: 12px;">' + data[i].EstadoReserva + '</b>');
                            content_clase.push('</div>');
                        } else {
                            content_clase.push('<div data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" data-horainicio="' + data[i].HoraInicioTexto + '" data-horafin="' + data[i].HoraFinTexto + '"  data-disciplina="' + data[i].Disciplina + '" data-fecha="' + fecha + '" onclick="javascript:visualizarCuposDisponibles(this);" style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                            content_clase.push('<b class="tx-center" style="font-size:13px;padding-top: 12px;">' + data[i].EstadoReserva + '</b>');
                            content_clase.push('</div>');
                        }
                    }

                }

                content_clase.push('</div>');
                content_clase.push('</div>');
            }
            $('#gridClasesGrupales').html(content_clase.join(' '));

        } else {

            $('#modal-info').show('fast');

            var content_clase = new Array();
            content_clase.push('<div style="padding: 13px;margin-top:30px;" class="alert alert-solid alert-primary" role="alert">');
            content_clase.push('<strong>Upps!</strong> Lo sentimos, se terminó las clases grupales en este día.');
            content_clase.push('</div>');
            $('#gridClasesGrupales').html(content_clase.join(' '));

        }

    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/gestionce/uspListarPresencial_HorarioClasesConfiguracionPaginaWeb', request, metodoCorrecto, metodoError);
}

//REGISTRAR RESERVA SALA GRUPALES
function visualizarCuposDisponibles(control) {
    var id = $(control).attr('data-id');
    var id_tiemporeal = $(control).attr('data-idtiemporeal');
    var dia = $(control).attr('data-dianumero');
    var horainicio = $(control).attr('data-horainicio');
    var horafin = $(control).attr('data-horafin');
    var disciplina = $(control).attr('data-disciplina');
    var fecha = $(control).attr('data-fecha');

    UspRegistrarPresencial_HorarioClasesAsistencias(id, id_tiemporeal, dia, disciplina, horainicio, horafin, fecha, 'G');
}

function UspRegistrarPresencial_HorarioClasesAsistencias(id, id_tiemporeal, dia, disciplina, horainicio, horafin, fecha, tipo) {
    document.getElementById('loadMe').style.display = 'block';

    var Accion = 'N';
    var entidad = {};
    entidad.CodigoHorarioClasesConfiguracion = id;
    entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal;
    entidad.CodigoHorarioClasesConfiguracionAsistencias = "";
    entidad.FechaReservacion = fecha;
    entidad.DiaNumero = dia;
    entidad.Accion = Accion;
    entidad.CodigoSocio = $('#hdCodigo').val();
    entidad.CodigoMembresia = $('#hdCodigoMembresiaOrigen').val();
    entidad.CodigoPaquete = $('#hdCodigoPaqueteOrigen').val();

    //alert("CodigoHorarioClasesConfiguracion: " + CodigoHorarioClasesConfiguracion);
    //alert("CodigoHorarioClasesConfiguracionTiempoReal: " + CodigoHorarioClasesConfiguracionTiempoReal);

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoMembresia == '' || entidad.CodigoMembresia == 0) {
        $.bootstrapGrowl("Falta seleccionar una membresia.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoHorarioClasesConfiguracion == '') {
        $.bootstrapGrowl("Falta selecionar una clase", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.DiaNumero == '') {
        $.bootstrapGrowl("Falta seleccionar la fecha", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';

        if (Accion == "N") {

            $('button[type="button"]').attr("disabled", false);
            $('#modalConfirmarReserva_titulo').html("ACABAS DE RESERVAR TU CLASE EN " + disciplina + ", DE " + horainicio + " A " + horafin);
            document.getElementById("modalConfirmarReserva_norma").style.display = 'block';
            document.getElementById("modalConfirmarReserva_nota").style.display = 'block';
            $('#modalConfirmarReserva').show('fast');

            if (tipo == 'G') {
                Obtener3FechasClasesGrupales(fecha);
            } else if (tipo == 'M') {
                Obtener3FechasTodo();
            }


        } else if (Accion == "E") {
            $('button[type="button"]').attr("disabled", false);
            $('button[id="btnNuevoProfesor"]').attr("disabled", true);
            $('button[id="btnGuardarProfesor"]').attr("disabled", false);
            $.bootstrapGrowl("Se actualizo correctamente los datos.", { type: 'success', width: 'auto' });
        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }

    };
    var metodoError = function (msg) {
        alert("Error: " + msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/UspRegistrarPresencial_HorarioClasesAsistencias", request, metodoCorrecto, metodoError);

}

function CancelarCitaClaseGrupal(control) {
    var id = $(control).attr('data-id');
    var id_tiemporeal = $(control).attr('data-idtiemporeal');
    var id_asistencia = $(control).attr('data-idasistencias');
    var codigosocio = $(control).attr('data-codigosocio');
    var dia = $(control).attr('data-dianumero');
    var fecha = $(control).attr('data-fecha');

    UspActualizarPresencial_DesactivarHorarioClasesAsistencias(id, id_tiemporeal, id_asistencia, codigosocio, dia, fecha, 'G');
}

//HORARIOS RANGO HORA MAQUINAS
function uspListarPresencial_SalaMaquinas_HorarioTemporal(control, DiaNumero, fecha, validacionTieneReserva) {
    document.getElementById('loadMe').style.display = 'block';

    if (control != undefined) {

        if ($(control).attr('id') == 'divMaquinas_Hoy') {
            $('#divMaquinas_Hoy').addClass('seleccionado');
            $('#divMaquinas_Hoy').removeClass('dseleccionado');
            $('#divMaquinas_Despues1').removeClass('seleccionado');
            $('#divMaquinas_Despues2').removeClass('seleccionado');

            $('#divMaquinas_Despues1').addClass('dseleccionado');
            $('#divMaquinas_Despues2').addClass('dseleccionado');

        } else if ($(control).attr('id') == 'divMaquinas_Despues1') {
            $('#divMaquinas_Despues1').addClass('seleccionado');
            $('#divMaquinas_Despues1').removeClass('dseleccionado');
            $('#divMaquinas_Hoy').removeClass('seleccionado');
            $('#divMaquinas_Despues2').removeClass('seleccionado');

            $('#divMaquinas_Hoy').addClass('dseleccionado');
            $('#divMaquinas_Despues2').addClass('dseleccionado');
        } else if ($(control).attr('id') == 'divMaquinas_Despues2') {
            $('#divMaquinas_Despues2').addClass('seleccionado');
            $('#divMaquinas_Despues2').removeClass('dseleccionado');
            $('#divMaquinas_Despues1').removeClass('seleccionado');
            $('#divMaquinas_Hoy').removeClass('seleccionado');

            $('#divMaquinas_Despues1').addClass('dseleccionado');
            $('#divMaquinas_Hoy').addClass('dseleccionado');
        }
    }

    if (fecha == undefined) {
        fecha = $(control).attr('data-fecha');
    }

    if (validacionTieneReserva == undefined) {
        validacionTieneReserva = $(control).attr('data-validacionTieneReserva');
    }

    uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_MAQUINAS(control, DiaNumero, fecha, validacionTieneReserva);
    //ESTE METODO YA SE USA
    //LlamarAJAX('/gestionce/uspListarPresencial_SalaMaquinas_HorarioTemporal', request, metodoCorrecto, metodoError);

}

//MOSTRAR CLASE RESERVADA
//ESTE METODO YA NO SE USA
function uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(DiaNumero, fecha) {

    document.getElementById('loadMe').style.display = 'block';

    var entidad = {};
    entidad.DiaNumero = DiaNumero;
    entidad.FechaHoraReserva = fecha;
    entidad.CodigoSocio = $('#hdCodigo').val();

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    }

    var metodoCorrecto = function (data) {
        document.getElementById('loadMe').style.display = 'none';

        if (data.validacionCancelarCita != 0) {

            if (data.TipoSala == 1) { //TIPO SALA GRUPALES

                $('#lblEstadoTieneReserva_texto').html('Felicidades, ya tienes una reserva en la sala grupal.');

                var content_clasereservada = new Array();
                content_clasereservada.push('<br /><div class="col-sm-12" style="background-color: transparent;padding-bottom: 5px;">');
                content_clasereservada.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 3px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:2px;border-radius: 12px;">');
                content_clasereservada.push('<b class="tx-center tx-12" style="margin-top: 8px;font-weight:bold;">' + data.HoraInicioTexto + ' - ' + data.HoraFinTexto + '</b><br>');
                content_clasereservada.push('<b class="tx-center tx-11" style="font-weight: bold;">' + data.Disciplina + ' - ' + data.DesSala + '</b>');
                content_clasereservada.push('</div>');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;">');
                content_clasereservada.push('<b class="tx-center" style="font-size:13px;margin-top: 15px;font-weight: bold;">AFORO: ' + data[i].CantidadPlazas + '</b></br>');
                content_clasereservada.push('<b class="tx-center" style="font-size:13px;margin-top: 11px;">' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + '</b>');

                content_clasereservada.push('</div>');

                if (data.validacionCancelarCita == 1) { //1= puede cancelar la cita
                    content_clasereservada.push('<div data-id="' + data.CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data.CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data.CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data.CodigoSocio + '" data-fecha="' + fecha + '"  onclick="javascript:CancelarCitaClaseGrupal(this);" style="background-color:red;color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                    content_clasereservada.push('<b class="tx-center" style="font-size:13px;padding-top: 12px;">CANCELAR</b>');
                    content_clasereservada.push('</div>');
                } else if (data.validacionCancelarCita == 2) {//2 = NO puede cancelar la cita
                    content_clasereservada.push('<div style="background-color:#ccc;color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                    content_clasereservada.push('<b class="tx-center" style="font-size:13px;padding-top: 12px;">RESERVADO</b>');
                    content_clasereservada.push('</div>');
                }

                content_clasereservada.push('</div>');
                content_clasereservada.push('</div>');
                $('#divClaseReservada').html(content_clasereservada.join(' '));

            } else if (data.TipoSala == 2) { //TIPO SALA MAQUINAS

                $('#lblEstadoTieneReserva_texto').html('<b style="color:#0075ff;">Felicidades, ya tienes una reserva en la sala de maquinas.</b>');
                //agregamos al div para que el usuario pueda cancelar su reserva muy facil
                var content_clasereservada = new Array();
                content_clasereservada.push('<br /><div class="col-md-12" style="background-color: transparent;padding-bottom: 5px;" >');
                content_clasereservada.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 3px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:2px;border-radius: 12px;">');
                content_clasereservada.push('<b class="tx-center tx-12" style="margin-top: 8px;font-weight:bold;">' + data.HoraInicioTexto + ' - ' + data.HoraFinTexto + '</b>');
                content_clasereservada.push('</div>');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;">');
                content_clasereservada.push('<b class="tx-center" style="font-size:13px;margin-top: 10px;font-weight:bold;">AFORO: ' + data.CantidadPlazas + '</b></br>');
                content_clasereservada.push('<b class="tx-center" style="font-size:13px;margin-top: 11px;">' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + '</b>');

                content_clasereservada.push('</div>');

                if (data.validacionCancelarCita == 1) { //1= puede cancelar la cita
                    content_clasereservada.push('<div data-id="' + data.CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data.CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data.CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data.CodigoSocio + '" data-fecha="' + fecha + '" onclick="javascript:CancelarCitaMaquinas(this);" style="background-color:red;color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                    content_clasereservada.push('<b class="tx-center" style="font-size:12px;padding-top: 12px;">CANCELAR</b>');
                    content_clasereservada.push('</div>');
                } else if (data.validacionCancelarCita == 2) {//2 = NO puede cancelar la cita
                    content_clasereservada.push('<div style="background-color:#ccc;color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                    content_clasereservada.push('<b class="tx-center" style="font-size:12px;padding-top: 12px;">RESERVADO</b>');
                    content_clasereservada.push('</div>');
                }

                content_clasereservada.push('</div>');
                content_clasereservada.push('</div>');
                $('#divClaseReservada').html(content_clasereservada.join(' '));

            }

        } else {
            $('#lblEstadoTieneReserva_texto').html('Ups, aún no tienes una reserva en esta fecha');
            $('#divClaseReservada').html('');
        }


    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/gestionce/CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS', request, metodoCorrecto, metodoError);

}

//MOSTRAR CLASES DE HOY MAQUINAS Y CLASES GRUPALES
function CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy() {

    document.getElementById('loadMe').style.display = 'block';

    var entidad = {};
    entidad.CodigoSocio = 0;//$('#hdCodigo').val();

    //if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
    //    $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
    //    return;
    //}

    var metodoCorrecto = function (data) {
        document.getElementById('loadMe').style.display = 'none';

        if (data.length > 0) {

            var content_clase = new Array();
            var contadorClasesGrupales = 0;
            var contadorClasesMaquinas = 0;

            for (var i = 0; i < data.length; i++) {

                if (data[i].TipoSala == 2) { //CLASES DE MAQUINAS
                    if (contadorClasesMaquinas == 0) {
                        content_clase.push('<li style="text-align:left;padding-top: 3px;color:#000;font-weight:bold;">CLASE DE MAQUINAS</li>');
                        content_clase.push('<li onclick="ValidarBuscarDiasHorarioPaquete_ReservarYMarcarAsistencia(this);" style="text-align:left;padding-top: 3px;padding-right:10px;" data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" ><div style="padding-left: 15px;padding-right:5px;height: 23px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;font-weight: 500;" href="#">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraInicio), "hh:mm tt") + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraFin), "hh:mm tt") + ' - ' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + ' </a></div></li>');
                    } else {
                        content_clase.push('<li onclick="ValidarBuscarDiasHorarioPaquete_ReservarYMarcarAsistencia(this);" style="text-align:left;padding-top: 3px;padding-right:10px;" data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" ><div style="padding-left: 15px;padding-right:5px;height: 23px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;font-weight: 500;" href="#">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraInicio), "hh:mm tt") + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraFin), "hh:mm tt") + ' - ' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + ' </a></div></li>');
                    }

                    contadorClasesMaquinas++;
                }

            }

            for (var i = 0; i < data.length; i++) {

                if (data[i].TipoSala == 1) { //CLASES GRUPALES
                    if (contadorClasesGrupales == 0) {
                        content_clase.push('<li style="text-align:left;padding-top: 3px;color:#000;font-weight:bold;">CLASES GRUPALES</li>');
                        content_clase.push('<li onclick="ValidarBuscarDiasHorarioPaquete_ReservarYMarcarAsistencia(this);" style="text-align:left;padding-top: 3px;padding-right:10px;" data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" ><div style="padding-left: 15px;padding-right:5px;height: 23px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;font-weight: 500;" href="#">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + data[i].NombreProfesionalFitness + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraInicio), "hh:mm tt") + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraFin), "hh:mm tt") + ' - ' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + ' </a></div></li>');
                    } else {
                        content_clase.push('<li onclick="ValidarBuscarDiasHorarioPaquete_ReservarYMarcarAsistencia(this);" style="text-align:left;padding-top: 3px;padding-right:10px;" data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" ><div style="padding-left: 15px;padding-right:5px;height: 23px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;font-weight: 500;" href="#">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + data[i].NombreProfesionalFitness + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraInicio), "hh:mm tt") + ' - ' + kendo.toString(ConvertirJsonFechaToDatetime(data[i].HoraFin), "hh:mm tt") + ' - ' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + ' </a></div></li>');
                    }

                    contadorClasesGrupales++;
                }

            }

            $('#ulLista_CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy').html(content_clase.join(' '));

            //<li style="text-align:left;padding-top: 3px;color:#000;font-weight:bold;">CLASE DE MAQUINAS</li>
            //<li style="text-align:left;padding-top: 3px;padding-right:10px;"><div style="padding-left: 15px;padding-right:5px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;" href="#">PISO 1 MUSCULACION 06:00 AM - 07:00 AM 0 de 35</a></div></li>
            //<li style="text-align:left;padding-top: 3px;padding-right:10px;"><div style="padding-left: 15px;padding-right:5px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;" href="#">PISO 2 MUSCULACION 07:00 AM - 08:00 AM 0 de 35</a></div></li>
            //<li style="text-align:left;padding-top: 3px;padding-right:10px;"><div style="padding-left: 15px;padding-right:5px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;" href="#">PISO 3 MUSCULACION 08:00 AM - 09:00 AM 0 de 35</a></div></li>
            //<li style="text-align:left;padding-top: 3px;color:#000;font-weight:bold;">CLASES GRUPALES</li>
            //<li style="text-align:left;padding-top: 3px;padding-right:10px;"><div style="padding-left: 15px;padding-right:5px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;" href="#">BAILE - AEROBICOS - MARCO ANTONIO - 06:00 AM - 07:00 AM 0 de 25</a></div></li>
            //<li style="text-align:left;padding-top: 3px;padding-right:10px;"><div style="padding-left: 15px;padding-right:5px;"><span class="far fa-calendar-alt" style="color: #000;"></span><a style="color: #000;text-decoration:none;padding-left:5px;" href="#">BOX - AEROBICOS - MAURO QUISPE - 01:00 PM - 02:00 PM 0 de 15</a></div></li>

        } else {
            $('#ulLista_CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy').html('<li style="text-align:left;padding-top: 3px;color:#000;font-weight:bold;">NO HEMOS ENCONTRADO CLASES EN ESTE HORARIO</li>');
        }

    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/gestionce/CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy', request, metodoCorrecto, metodoError);
}

//GUARDAR LA RESERVA Y AL MISMO TIEMPO MARCAR ASISTENCIA
function UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia(id, id_tiemporeal, codigosocio, CodigoMenbresia) {

    document.getElementById('loadMe').style.display = 'block';
    var entidad = {};
    entidad.CodigoHorarioClasesConfiguracion = id;
    entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal;
    entidad.CodigoSocio = codigosocio;
    entidad.CodigoMembresia = CodigoMenbresia;
    // entidad.CodigoSocio = $('#hdCodigo').val();

    if (entidad.CodigoHorarioClasesConfiguracion == '') {
        $.bootstrapGrowl("Falta selecionar una clase", { type: 'danger', width: 'auto' });
        return;
    } if (entidad.CodigoHorarioClasesConfiguracionTiempoReal == '') {
        $.bootstrapGrowl("Falta selecionar una clase real", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoSocio == '') {
        $.bootstrapGrowl("Falta selecionar una socio", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoMembresia == '') {
        $.bootstrapGrowl("Falta selecionar una membresia", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {

        var Codigo = msg.split('|')[0];
        var Mensaje = msg.split('|')[1];
        var flagExisteClase = msg.split('|')[2];
        if (Codigo == 0) {
            $.bootstrapGrowl(Mensaje, { type: 'danger', width: 'auto' });
        } else {

            $.bootstrapGrowl("SE MARCO LA ASISTENCIA CORRECTAMENTE", { type: 'success', width: 'auto', align: 'center' });
            verAsistencias(CodigoMenbresia);
            verReservas(CodigoMenbresia);
            //alert('flagExisteClase: ' + flagExisteClase);
            if (flagExisteClase == 0) {
                CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy();
            }

            var chkMarcadorAutomatico = $('#chkMarcadorAutomatico').is(':checked');
            if (chkMarcadorAutomatico == true) {
                $('#txtBuscadorGeneral').val('');
                $('#txtBuscadorGeneral').focus();
            }

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
    LlamarAJAX("/gestionce/UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia", request, metodoCorrecto, metodoError);

}


function uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_MAQUINAS(control, DiaNumero, fecha, validacionTieneReserva) {

    document.getElementById('loadMe').style.display = 'block';
    var horainicio = '06:00 AM';
    var horafin = '06:00 AM';

    var entidad = {};
    entidad.DiaNumero = DiaNumero;
    entidad.CodigoSala = $("#ddlTipoSala").data('kendoDropDownList').value();//100;
    entidad.FechaHoraReserva = fecha;
    entidad.HoraInicio = fecha + ' ' + horainicio;
    entidad.HoraFin = fecha + ' ' + horafin;
    entidad.CodigoSocio = $('#hdCodigo').val();

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    }

    var metodoCorrecto = function (data) {
        document.getElementById('loadMe').style.display = 'none';

        if (data.length > 0) {

            var validacionTieneReservaFecha = validacionTieneReserva;

            var content_clase = new Array();
            for (var i = 0; i < data.length; i++) {

                content_clase.push('<div class="col-lg-3 mg-t-5" style="background-color: #fff;padding-bottom: 10px;" >');
                content_clase.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 3px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:2px;border-radius: 12px;" title="CODIGO-CONFI: ' + data[i].CodigoHorarioClasesConfiguracion + '  CODIGO-REAL: ' + data[i].CodigoHorarioClasesTiempoReal + '">');
                content_clase.push('<b class="tx-center tx-13" style="margin-top: 8px;font-weight:bold;">' + data[i].HoraInicioTexto + ' - ' + data[i].HoraFinTexto + '</b>');
                content_clase.push('</div>');
                content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;">');
                content_clase.push('<b class="tx-center" style="font-size:13px;margin-top: 10px;font-weight:bold;">AFORO: ' + data[i].CantidadPlazas + '</b></br>');
                content_clase.push('<b class="tx-center" style="font-size:13px;margin-top: 11px;">' + data[i].CantidadAsistencias + ' de ' + data[i].CapacidadPermitida + '</b>');

                content_clase.push('</div>');

                if (data[i].CodigoHorarioClasesConfiguracionAsistencias != '' && data[i].CodigoHorarioClasesConfiguracionAsistencias != null) {

                    if (data[i].validacionCancelarCita == 1) { //1= puede cancelar la cita
                        content_clase.push('<div data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data[i].CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data[i].CodigoSocio + '" data-fecha="' + fecha + '" onclick="javascript:CancelarCitaMaquinas(this);" style="background-color:red;color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                        content_clase.push('<b class="tx-center" style="font-size:12px;padding-top: 12px;">CANCELAR</b>');
                        content_clase.push('</div>');
                    } else if (data[i].validacionCancelarCita == 2) {//2 = NO puede cancelar la cita
                        content_clase.push('<div style="background-color:#EFEFF4;color:#000;text-align:center;padding:5px;border-radius: 12px;"> ');
                        content_clase.push('<b class="tx-center" style="font-size:12px;padding-top: 12px;">RESERVADO</b>');
                        content_clase.push('</div>');
                    }

                } else {

                    //VALIDAMOS SI TIENE RESERVA NO PUEDE RESERVAR OTROS HORARIOS DURANTE EL DIA
                    if (validacionTieneReservaFecha > 0) {
                        content_clase.push('<div onclick="mostrarAviso();" disabled style="background-color:#EFEFF4;color:#000;text-align:center;padding:5px;border-radius: 12px;"> ');
                        content_clase.push('<b class="tx-center" style="font-size:12px;padding-top: 12px;">RESERVAR</b>');
                        content_clase.push('</div>');
                    } else {
                        if (data[i].CantidadPlazas <= 0) {
                            content_clase.push('<div disabled style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                            content_clase.push('<b class="tx-center" style="font-size:12px;padding-top: 12px;">' + data[i].EstadoReserva + '</b>');
                            content_clase.push('</div>');
                        } else {
                            content_clase.push('<div data-id-maquinas="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero-maquinas="' + DiaNumero + '" data-idtiemporeal-maquinas="' + data[i].CodigoHorarioClasesTiempoReal + '" data-horainicio-maquinas="' + data[i].HoraInicioTexto + '" data-horafin-maquinas="' + data[i].HoraFinTexto + '"  data-disciplina-maquinas="' + data[i].Disciplina + '" data-fecha="' + fecha + '" onclick="javascript:visualizarCuposDisponiblesMaquinas(this);" style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:5px;border-radius: 12px;"> ');
                            content_clase.push('<b class="tx-center" style="font-size:12px;padding-top: 12px;">' + data[i].EstadoReserva + '</b>');
                            content_clase.push('</div>');
                        }
                    }

                }

                content_clase.push('</div>');
                content_clase.push('</div>');

            }
            $('#gridClases').html(content_clase.join(' '));

        } else {
            $('#modal-info').show('fast');
            $('#gridClases').html('');
        }

    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/gestionce/uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS', request, metodoCorrecto, metodoError);
}

function visualizarCuposDisponiblesMaquinas(control) {
    var id = $(control).attr('data-id-maquinas');
    var id_tiemporeal = $(control).attr('data-idtiemporeal-maquinas');
    var dia = $(control).attr('data-dianumero-maquinas');
    var horainicio = $(control).attr('data-horainicio-maquinas');
    var horafin = $(control).attr('data-horafin-maquinas');
    var disciplina = $(control).attr('data-disciplina-maquinas');
    var fecha = $(control).attr('data-fecha');

    UspRegistrarPresencial_HorarioClasesAsistencias(id, id_tiemporeal, dia, disciplina, horainicio, horafin, fecha, 'M');
}

//CANCELAR RESERVA MAQUINAS
function CancelarCitaMaquinas(control) {
    var id = $(control).attr('data-id');
    var id_tiemporeal = $(control).attr('data-idtiemporeal');
    var id_asistencia = $(control).attr('data-idasistencias');
    var codigosocio = $(control).attr('data-codigosocio');
    var dia = $(control).attr('data-dianumero');
    var fecha = $(control).attr('data-fecha');

    UspActualizarPresencial_DesactivarHorarioClasesAsistencias(id, id_tiemporeal, id_asistencia, codigosocio, dia, fecha, 'M');
}

function UspActualizarPresencial_DesactivarHorarioClasesAsistencias(id, id_tiemporeal, id_asistencia, codigosocio, dia, fecha, tipo) {
    document.getElementById('loadMe').style.display = 'block';
    var entidad = {};
    entidad.CodigoHorarioClasesConfiguracion = id;
    entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal;
    entidad.CodigoHorarioClasesConfiguracionAsistencias = id_asistencia;
    entidad.CodigoSocio = codigosocio;
    // entidad.CodigoSocio = $('#hdCodigo').val();

    //if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
    //    $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
    //    return;
    //}

    if (entidad.CodigoHorarioClasesConfiguracion == '') {
        $.bootstrapGrowl("Falta selecionar una clase", { type: 'danger', width: 'auto' });
        return;
    } if (entidad.CodigoHorarioClasesConfiguracionTiempoReal == '') {
        $.bootstrapGrowl("Falta selecionar una clase real", { type: 'danger', width: 'auto' });
        return;
    } if (entidad.CodigoHorarioClasesConfiguracionAsistencias == '') {
        $.bootstrapGrowl("No tienes cita reservada", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoSocio == '') {
        $.bootstrapGrowl("Falta selecionar una socio", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        if (msg == 100) {
            $('button[type="button"]').attr("disabled", false);
            $('#modalConfirmarReserva_titulo').html("TÚ RESERVA ESTA CANCELADA, AHORA PUEDES ELEJIR OTRO HORARIO");
            document.getElementById("modalConfirmarReserva_norma").style.display = 'none';
            document.getElementById("modalConfirmarReserva_nota").style.display = 'none';
            $('#modalConfirmarReserva').show('fast');
            if (tipo == 'G') {
                Obtener3FechasClasesGrupales(fecha);
            } else if (tipo == 'M') {
                Obtener3FechasTodo();
            }
        }
        else {
            $.bootstrapGrowl("Error, No se ha podido cancelar la reserva!", { type: 'danger', width: 'auto' });
        }


    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias", request, metodoCorrecto, metodoError);

}

function Cancelar_UspActualizarPresencial_DesactivarHorarioClasesAsistencias_Checking() {
    document.getElementById('myModalCancelarReservaClases').style.display = 'none';
}

function Consultar_UspActualizarPresencial_DesactivarHorarioClasesAsistencias_Checking(id, id_tiemporeal, id_asistencia, codigosocio) {
    $('#hdCancelarReserva_id').val(id);
    $('#hdCancelarReserva_id_tiemporeal').val(id_tiemporeal);
    $('#hdCancelarReserva_id_asistencia').val(id_asistencia);
    $('#hdCancelarReserva_codigosocio').val(codigosocio);

    //alert('id: ' + id);
    //alert('id_tiemporeal: ' + id_tiemporeal);
    //alert('id_asistencia: ' + id_asistencia);
    //alert('codigosocio: ' + codigosocio);
    //AQUI ABRIR UNA VENTA EMERGENTE
    document.getElementById('myModalCancelarReservaClases').style.display = 'block';
}

function Confirmar_UspActualizarPresencial_DesactivarHorarioClasesAsistencias_Checking() {

    var id = $('#hdCancelarReserva_id').val();
    var id_tiemporeal = $('#hdCancelarReserva_id_tiemporeal').val();
    var id_asistencia = $('#hdCancelarReserva_id_asistencia').val();
    var codigosocio = $('#hdCancelarReserva_codigosocio').val();

    document.getElementById('loadMe').style.display = 'block';
    var entidad = {};
    entidad.CodigoHorarioClasesConfiguracion = id;
    entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal;
    entidad.CodigoHorarioClasesConfiguracionAsistencias = id_asistencia;
    entidad.CodigoSocio = codigosocio;
    // entidad.CodigoSocio = $('#hdCodigo').val();

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    }

    if (entidad.CodigoHorarioClasesConfiguracion == '') {
        $.bootstrapGrowl("Falta selecionar una clase", { type: 'danger', width: 'auto' });
        return;
    } if (entidad.CodigoHorarioClasesConfiguracionTiempoReal == '') {
        $.bootstrapGrowl("Falta selecionar una clase real", { type: 'danger', width: 'auto' });
        return;
    } if (entidad.CodigoHorarioClasesConfiguracionAsistencias == '') {
        $.bootstrapGrowl("No tienes cita reservada", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoSocio == '') {
        $.bootstrapGrowl("Falta selecionar una socio", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        if (msg == 100) {
            $('button[type="button"]').attr("disabled", false);
            document.getElementById('myModalCancelarReservaClases').style.display = 'none';
            $.bootstrapGrowl("TÚ RESERVA HA SIDO CANCELADA.", { type: 'success', width: 'auto' });
            //AQUI VOLVER A CARGAR LAS RESERVAS
            var codMembresia = $('#hdCodigoMembresiaOrigen').val();
            verReservas(codMembresia);
            var CodigoSocio = $('#hdCodigo').val();
            verAsistencias(codMembresia);

            BuscarAsistenciaEfectiva(codMembresia, CodigoSocio);
        }
        else {
            $.bootstrapGrowl("Error, No se ha podido cancelar la reserva!", { type: 'danger', width: 'auto' });
        }


    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias", request, metodoCorrecto, metodoError);

}

function Confirmar_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias_Checking(id, id_tiemporeal, id_asistencia, CodigoSocio, CodigoMembresia) {

    var flagPaqueteSedePermiso = $('#hdflagPaqueteSedePermiso').val();
    if (flagPaqueteSedePermiso == 1) {//PERMISO PARA INGRESAR A LA SEDE

        //VALIDAR ACCESO AL HORARIO DEL PLAN ADQUIRIDO
        var ObtenerDisponibilidadHorarioPaquete = $('#hdflagDisponibilidadHorarioPaquete').val();
        if (ObtenerDisponibilidadHorarioPaquete > 0) {

            var CantDiasEfec = $('#hdCantidadTotalAsistenciaEfectivo').val();
            var NroIngresoActual = $('#hdNroIngresoActual').val();

            if (parseInt(CantDiasEfec) < parseInt(NroIngresoActual)) {
                $.bootstrapGrowl("NRO ASISTENCIAS LLEGO A SU LIMITE, REVISA EL NRO DE SESIONES DE LA MEMBRESIA", { type: 'danger', width: 'auto' });
                return;
            }

        } else if (ObtenerDisponibilidadHorarioPaquete == 0) {
            document.getElementById("tabla_Info_Inicio_Cliente").style.display = '';
            $('#lblMensajeInicio_Cliente').html('HORARIO NO DISPONIBLE');
            $('#lblEstadoCliente').html('SU PLAN NO TIENE ACCESO EN ESTE HORARIO');
            $.bootstrapGrowl("EL PLAN DEL CLIENTE NO TIENE ACCESO PARA INGRESAR EN ESTE HORARIO", { type: 'danger', width: 'auto' });
            $('#btnMarcarAsistencia_Cliente').hide('fast');
            $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
            $('#InforMembresias_Cliente').css('background-color', 'red');
            return;
        }

        var entidad = {};
        entidad.CodigoHorarioClasesConfiguracion = id;
        entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal;
        entidad.CodigoHorarioClasesConfiguracionAsistencias = id_asistencia;
        entidad.CodigoSocio = CodigoSocio;
        entidad.CodigoMembresia = CodigoMembresia;
        // entidad.CodigoSocio = $('#hdCodigo').val();

        if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
            $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
            return;
        } else if (entidad.CodigoHorarioClasesConfiguracion == '') {
            $.bootstrapGrowl("Falta selecionar una clase", { type: 'danger', width: 'auto' });
            return;
        } else if (entidad.CodigoHorarioClasesConfiguracionTiempoReal == '') {
            $.bootstrapGrowl("Falta selecionar una clase real", { type: 'danger', width: 'auto' });
            return;
        } else if (entidad.CodigoHorarioClasesConfiguracionAsistencias == '') {
            $.bootstrapGrowl("No tienes clase reservada", { type: 'danger', width: 'auto' });
            return;
        }

        document.getElementById('loadMe').style.display = 'block';
        $('button[type="button"]').attr("disabled", true);

        var metodoCorrecto = function (msg) {
            document.getElementById('loadMe').style.display = 'none';
            $('button[type="button"]').attr("disabled", false);

            var Codigo = msg.split('|')[0];
            var Mensaje = msg.split('|')[1];

            if (Codigo == 0) {
                $.bootstrapGrowl("NRO ASISTENCIAS LLEGO A SU LIMITE, REVISA EL NRO DE SESIONES DE LA MEMBRESIA", { type: 'danger', width: 'auto' });
            } else {

                $.bootstrapGrowl("SE MARCO LA ASISTENCIA CORRECTAMENTE", { type: 'success', width: 'auto', align: 'center' });
                verAsistencias(CodigoMenbresia);
                verReservas(CodigoMenbresia);
                var chkMarcadorAutomatico = $('#chkMarcadorAutomatico').is(':checked');
                if (chkMarcadorAutomatico == true) {
                    $('#txtBuscadorGeneral').val('');
                    $('#txtBuscadorGeneral').focus();
                }

            }

        };
        var metodoError = function (msg) {
            alert(msg);
        };
        var request = {
            request: entidad
        };
        LlamarAJAX("/gestionce/CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias", request, metodoCorrecto, metodoError);

    } else {
        $.bootstrapGrowl("LA MEMBRESIA DEL CLIENTE NO TIENE PERMISO PARA INGRESAR A ESTA SEDE.", { type: 'danger', width: 'auto' });
    }

}

function Confirmar_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias_CheckingMasivo(id, id_tiemporeal, id_asistencia, CodigoSocio, CodigoMembresia, controlBoton) {
    
    var entidad = {};
    entidad.CodigoHorarioClasesConfiguracion = id.toString().replace('/', '').replace('/', '').replace('/', '');
    entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal.toString().replace('/', '').replace('/', '').replace('/', '');
    entidad.CodigoHorarioClasesConfiguracionAsistencias = id_asistencia.toString().replace('/', '').replace('/', '').replace('/', '');
    entidad.CodigoSocio = CodigoSocio;
    entidad.CodigoMembresia = CodigoMembresia;
    // entidad.CodigoSocio = $('#hdCodigo').val();
    //alert("CodigoHorarioClasesConfiguracionTiempoReal: " + id.toString().replace('/', ''));
    //alert("CodigoHorarioClasesConfiguracionTiempoReal: " + id_tiemporeal.toString().replace('/', ''));
    //alert("CodigoHorarioClasesConfiguracionAsistencias: " + id_asistencia.toString().replace('/', ''));
    //alert("CodigoSocio: " + CodigoSocio);
    //alert("CodigoMembresia: " + CodigoMembresia);

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoHorarioClasesConfiguracion == '') {
        $.bootstrapGrowl("Falta selecionar una clase", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoHorarioClasesConfiguracionTiempoReal == '') {
        $.bootstrapGrowl("Falta selecionar una clase real", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoHorarioClasesConfiguracionAsistencias == '') {
        $.bootstrapGrowl("No tienes clase reservada", { type: 'danger', width: 'auto' });
        return;
    }

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        $('button[type="button"]').attr("disabled", false);

        var Codigo = msg.split('|')[0];
        var Mensaje = msg.split('|')[1];

        if (Codigo == 0) {
            $.bootstrapGrowl("NRO ASISTENCIAS LLEGO A SU LIMITE, REVISA EL NRO DE SESIONES DE LA MEMBRESIA", { type: 'danger', width: 'auto' });
        } else {

            $.bootstrapGrowl("SE MARCO LA ASISTENCIA CORRECTAMENTE", { type: 'success', width: 'auto', align: 'center' });
            $(controlBoton).attr("disabled", true);            
            //verAsistencias(CodigoMenbresia);
            //verReservas(CodigoMenbresia);
            //var chkMarcadorAutomatico = $('#chkMarcadorAutomatico').is(':checked');
            //if (chkMarcadorAutomatico == true) {
            //    $('#txtBuscadorGeneral').val('');
            //    $('#txtBuscadorGeneral').focus();
            //}

        }

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias", request, metodoCorrecto, metodoError);


    //var flagPaqueteSedePermiso = $('#hdflagPaqueteSedePermiso').val();
    //if (flagPaqueteSedePermiso == 1) {//PERMISO PARA INGRESAR A LA SEDE

    //    //VALIDAR ACCESO AL HORARIO DEL PLAN ADQUIRIDO
    //    var ObtenerDisponibilidadHorarioPaquete = $('#hdflagDisponibilidadHorarioPaquete').val();
    //    if (ObtenerDisponibilidadHorarioPaquete > 0) {

    //        var CantDiasEfec = $('#hdCantidadTotalAsistenciaEfectivo').val();
    //        var NroIngresoActual = $('#hdNroIngresoActual').val();

    //        if (parseInt(CantDiasEfec) < parseInt(NroIngresoActual)) {
    //            $.bootstrapGrowl("NRO ASISTENCIAS LLEGO A SU LIMITE, REVISA EL NRO DE SESIONES DE LA MEMBRESIA", { type: 'danger', width: 'auto' });
    //            return;
    //        }

    //    } else if (ObtenerDisponibilidadHorarioPaquete == 0) {
    //        document.getElementById("tabla_Info_Inicio_Cliente").style.display = '';
    //        $('#lblMensajeInicio_Cliente').html('HORARIO NO DISPONIBLE');
    //        $('#lblEstadoCliente').html('SU PLAN NO TIENE ACCESO EN ESTE HORARIO');
    //        $.bootstrapGrowl("EL PLAN DEL CLIENTE NO TIENE ACCESO PARA INGRESAR EN ESTE HORARIO", { type: 'danger', width: 'auto' });
    //        $('#btnMarcarAsistencia_Cliente').hide('fast');
    //        $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
    //        $('#InforMembresias_Cliente').css('background-color', 'red');
    //        return;
    //    }

        
    //} else {
    //    $.bootstrapGrowl("LA MEMBRESIA DEL CLIENTE NO TIENE PERMISO PARA INGRESAR A ESTA SEDE.", { type: 'danger', width: 'auto' });
    //}

}


//REGISTRAR RESERVA MAQUINAS
function UspRegistrarPresencial_HorarioClasesAsistencias(id, id_tiemporeal, dia, disciplina, horainicio, horafin, fecha, tipo) {
    document.getElementById('loadMe').style.display = 'block';

    var Accion = 'N';

    var entidad = {};
    entidad.CodigoHorarioClasesConfiguracion = id;
    entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal;
    entidad.CodigoHorarioClasesConfiguracionAsistencias = "";
    entidad.FechaReservacion = fecha;
    entidad.DiaNumero = dia;
    entidad.Accion = Accion;
    entidad.CodigoSocio = $('#hdCodigo').val();
    entidad.CodigoMembresia = $('#hdCodigoMembresiaOrigen').val();
    entidad.CodigoPaquete = $('#hdCodigoPaqueteOrigen').val();

    if (entidad.CodigoSocio == '' || entidad.CodigoSocio == 0) {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoMembresia == '' || entidad.CodigoMembresia == 0) {
        $.bootstrapGrowl("Falta seleccionar una membresia.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoPaquete == '' || entidad.CodigoPaquete == 0) {
        $.bootstrapGrowl("Falta seleccionar una membresia.", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CodigoHorarioClasesConfiguracion == '') {
        $.bootstrapGrowl("Falta selecionar una clase", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.DiaNumero == '') {
        $.bootstrapGrowl("Falta seleccionar la fecha", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';

        if (Accion == "N") {
            $('button[type="button"]').attr("disabled", false);
            $('#modalConfirmarReserva_titulo').html("ACABAS DE RESERVAR TU CLASE EN " + disciplina + ", DE " + horainicio + " A " + horafin);
            document.getElementById("modalConfirmarReserva_norma").style.display = 'block';
            document.getElementById("modalConfirmarReserva_nota").style.display = 'block';
            $('#modalConfirmarReserva').show('fast');

            if (tipo == 'G') {
                Obtener3FechasClasesGrupales(fecha);
            } else if (tipo == 'M') {
                Obtener3FechasTodo();
            }

        } else if (Accion == "E") {
            $('button[type="button"]').attr("disabled", false);
            $('button[id="btnNuevoProfesor"]').attr("disabled", true);
            $('button[id="btnGuardarProfesor"]').attr("disabled", false);
            $.bootstrapGrowl("Se actualizo  correctamente los datos.", { type: 'success', width: 'auto' });
        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }

    };
    var metodoError = function (msg) {
        alert("Error: " + msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/UspRegistrarPresencial_HorarioClasesAsistencias", request, metodoCorrecto, metodoError);

}

//LISTA DE PERSONAS RESERVARON SE VISUALIZARA EN EL MODULO CLIENTES

function uspListarPresencial_HorarioClasesConfiguracionChecking() {
    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        type: "POST",
        url: "/gestionce/CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionChecking",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.length == 0) {
                document.getElementById('divClasesTiempoReal').style.display = 'none';
            } else {
                document.getElementById('divClasesTiempoReal').style.display = '';
            }

            //1. crear lista de encabezados
            var listaEncabezado = []; // new Array();
            var indexPrimeraFila = 0;
            var idReal = '';
            for (var i = 0; i < msg.length; i++) {

                //if (msg[i].CodigoSala < 100) { //recorremos solo salas grupales

                if (i == 0) {
                    idReal = msg[i].CodigoHorarioClasesTiempoReal;
                    indexPrimeraFila = 1;
                }

                if (idReal == msg[i].CodigoHorarioClasesTiempoReal) {

                    if (indexPrimeraFila == 1) {
                        var entidadEncabezado = {};
                        entidadEncabezado.CodigoHorarioClasesTiempoReal = msg[i].CodigoHorarioClasesTiempoReal;
                        entidadEncabezado.CodigoSala = msg[i].CodigoSala;
                        entidadEncabezado.DesSala = msg[i].DesSala;
                        entidadEncabezado.Disciplina = msg[i].Disciplina;
                        entidadEncabezado.HoraInicio = msg[i].HoraInicio;
                        entidadEncabezado.HoraFin = msg[i].HoraFin;
                        entidadEncabezado.FechaInicioTexto = msg[i].FechaInicioTexto;
                        entidadEncabezado.HoraInicioTexto = msg[i].HoraInicioTexto;
                        entidadEncabezado.HoraFinTexto = msg[i].HoraFinTexto;
                        entidadEncabezado.Color = msg[i].Color;
                        entidadEncabezado.CapacidadPermitida = msg[i].CapacidadPermitida;
                        entidadEncabezado.CantidadAsistencias = msg[i].CantidadAsistencias;
                        entidadEncabezado.CantidadPlazas = msg[i].CantidadPlazas;
                        entidadEncabezado.NotaAlarma = msg[i].NotaAlarma;

                        listaEncabezado.push(entidadEncabezado);

                    }
                    indexPrimeraFila++;

                } else if (idReal != msg[i].CodigoHorarioClasesTiempoReal) {
                    idReal = msg[i].CodigoHorarioClasesTiempoReal;
                    indexPrimeraFila = 1;
                    if (indexPrimeraFila == 1) {
                        var entidadEncabezado = {};
                        entidadEncabezado.CodigoHorarioClasesTiempoReal = msg[i].CodigoHorarioClasesTiempoReal;
                        entidadEncabezado.CodigoSala = msg[i].CodigoSala;
                        entidadEncabezado.DesSala = msg[i].DesSala;
                        entidadEncabezado.Disciplina = msg[i].Disciplina;
                        entidadEncabezado.HoraInicio = msg[i].HoraInicio;
                        entidadEncabezado.HoraFin = msg[i].HoraFin;
                        entidadEncabezado.FechaInicioTexto = msg[i].FechaInicioTexto;
                        entidadEncabezado.HoraInicioTexto = msg[i].HoraInicioTexto;
                        entidadEncabezado.HoraFinTexto = msg[i].HoraFinTexto;
                        entidadEncabezado.Color = msg[i].Color;
                        entidadEncabezado.CapacidadPermitida = msg[i].CapacidadPermitida;
                        entidadEncabezado.CantidadAsistencias = msg[i].CantidadAsistencias;
                        entidadEncabezado.CantidadPlazas = msg[i].CantidadPlazas;
                        entidadEncabezado.NotaAlarma = msg[i].NotaAlarma;
                        listaEncabezado.push(entidadEncabezado);

                    }
                    indexPrimeraFila++;

                }
                //}

            }

            //for (var e = 0; e < listaEncabezado.length; e++) {
            //    alert('SALA: ' + listaEncabezado[e].DesSala + 'DISCIPLINA: ' + listaEncabezado[e].Disciplina + 'HORA INICIO: ' + listaEncabezado[e].HoraInicio + 'HORA FIN: ' + listaEncabezado[e].HoraFin);
            //}

            //2. Anexar armar clases grupales

            if (listaEncabezado.length > 0) {
                var content_clasegrupal = [];

                for (var a = 0; a < listaEncabezado.length; a++) {

                    if (listaEncabezado[a].CodigoSala < 100) {//recorremos solo salas grupales
                        content_clasegrupal.push('<div class="col-sm-12 col-lg-12" style="background-color: #e4e6e9;padding-bottom: 10px;">');
                        content_clasegrupal.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 10px;padding: 10px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                        //content_clasegrupal.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;padding-top: 12px;border-radius: 12px;border-bottom-right-radius: 0;border-bottom-left-radius: 0;">');
                        //content_clasegrupal.push('<p class="tx-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 26px;">' + listaEncabezado[a].FechaInicioTexto + ' - ' + listaEncabezado[a].HoraInicioTexto + ' - ' + listaEncabezado[a].HoraFinTexto + ' - AFORO:' + listaEncabezado[a].CantidadPlazas + '</p>');
                        //content_clasegrupal.push('<p class="tx-center" style="font-weight: bold;font-size: 18px;">' + listaEncabezado[a].Disciplina + ' - ' + listaEncabezado[a].DesSala + ' - ' + listaEncabezado[a].CantidadAsistencias + ' de ' + listaEncabezado[a].CapacidadPermitida + '</p>');
                        //content_clasegrupal.push('<p class="tx-center" style="font-weight: bold;font-size: 18px;color:red;">' + listaEncabezado[a].NotaAlarma + '</p>');
                        //content_clasegrupal.push('</div>');

                        content_clasegrupal.push('<div class="row" style="border-radius:6px;background-color: #0075ff;color:#fff;padding-bottom: 6px;margin-bottom: 10px;margin-left: 0px;margin-right: 0px;">');
                        content_clasegrupal.push('<div class="col-5 border-right text-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 17px;"> HORA: ' + listaEncabezado[a].HoraInicioTexto + ' - ' + listaEncabezado[a].HoraFinTexto + '</div>');
                        content_clasegrupal.push('<div class="col-4 border-right text-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 17px;"> FECHA: ' + listaEncabezado[a].FechaInicioTexto + '</div>');
                        content_clasegrupal.push('<div class="col-3 text-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 17px;"> AFORO: ' + listaEncabezado[a].CantidadPlazas + '</div>');
                        content_clasegrupal.push('</div>');
                        content_clasegrupal.push('<div class="row" style="color:#000;padding-bottom: 6px;margin-bottom: 10px;margin-left: 0px;margin-right: 0px;">');
                        content_clasegrupal.push('<div class="col-12 text-center tx-18" style="font-weight: bold;font-size: 16px;color:#ff0000;text-transform: uppercase;">' + listaEncabezado[a].NotaAlarma + '</div>');
                        content_clasegrupal.push('</div>');

                        content_clasegrupal.push('<div class="row" style="color:#000;padding-bottom: 6px;margin-bottom: 10px;margin-left: 0px;margin-right: 0px;">');
                        content_clasegrupal.push('<div class="col-8 border-right text-center tx-18" style="font-weight:bold;font-size: 17px;">' + listaEncabezado[a].Disciplina + ' - ' + listaEncabezado[a].DesSala + '</div>');
                        content_clasegrupal.push('<div class="col-4 text-center tx-18" style="font-weight: bold;font-size: 17px;"> ESTADO: ' + listaEncabezado[a].CantidadAsistencias + ' de ' + listaEncabezado[a].CapacidadPermitida + '</div>');
                        content_clasegrupal.push('</div>');

                        content_clasegrupal.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;font-weight: bold;">');

                        content_clasegrupal.push('<table class="table">');
                        content_clasegrupal.push('        <thead style="">');
                        content_clasegrupal.push('            <tr>');
                        content_clasegrupal.push('                <th style="background-color: #0075ff;color: #fff;">Cliente</th>');
                        content_clasegrupal.push('                <th style="width: 35px;background-color: #0075ff;color: #fff;">Codigo</th>');
                        content_clasegrupal.push('                <th style="width: 35px;background-color: #0075ff;color: #fff;">Documento</th>');
                        content_clasegrupal.push('                <th style="width: 35px;background-color: #0075ff;color: #fff;">Chat</th>');
                        content_clasegrupal.push('                <th style="width: 35px;background-color: #0075ff;color: #fff;">Estado</th>');
                        content_clasegrupal.push('            </tr>');
                        content_clasegrupal.push('        </thead>');
                        content_clasegrupal.push('        <tbody style="">');

                        for (var cl = 0; cl < msg.length; cl++) {

                            if (listaEncabezado[a].CodigoHorarioClasesTiempoReal == msg[cl].CodigoHorarioClasesTiempoReal) {

                                content_clasegrupal.push('            <tr style="height:10px;">');
                                content_clasegrupal.push('                    <td>');
                                content_clasegrupal.push('                        <div class="media align-items-center">');
                                content_clasegrupal.push('                            <div class="avatar avatar-xs mr-2">');
                                content_clasegrupal.push('                                <img src="' + msg[cl].ImagenUrl + '" alt="Avatar" class="avatar-img rounded-circle">');
                                content_clasegrupal.push('                            </div>');
                                content_clasegrupal.push('                            <div class="media-body">');
                                content_clasegrupal.push('                                <span class="js-lists-values-employee-name">' + msg[cl].Nombres + ', ' + msg[cl].Apellidos + '</span>');
                                content_clasegrupal.push('                            </div>');
                                content_clasegrupal.push('                        </div>');
                                content_clasegrupal.push('                    </td>');
                                content_clasegrupal.push('                    <td><small class="text-muted" style="font-size:13px;font-weight:bold;">' + msg[cl].CodigoSocio + '</small></td>');
                                content_clasegrupal.push('                    <td><small class="text-muted" style="font-size:13px;font-weight:bold;">' + msg[cl].DNI + '</small></td>');
                                content_clasegrupal.push('                    <td><span class="badge" style="font-size:13px;font-weight:bold;display:' + msg[cl].EstadoCelular + '"><a target="_blank" style="color:#000;" href="https://api.whatsapp.com/send?phone=' + msg[cl].Whatsapp + '"><img style="height:18px;" src="/Content/iconos/whatsapp-icono-negro.png" />' + msg[cl].Celular + '</a></span></td>');
                                content_clasegrupal.push('                    <td>');                                
                                content_clasegrupal.push('                      <button onclick="Confirmar_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias_CheckingMasivo(/' + msg[cl].CodigoHorarioClasesConfiguracion + '/,/' + msg[cl].CodigoHorarioClasesTiempoReal + '/,/' + msg[cl].CodigoHorarioClasesConfiguracionAsistencias + '/,' + msg[cl].CodigoSocio + ',' + msg[cl].CodigoMembresia + ',this);" class="btn btn-primary btn-sm me-1 mb-1" type="button" style="display:' + msg[cl].flagVistaBotonMarcarAsistencia + '">');
                                content_clasegrupal.push('                          <i class="fa-solid fa-check"></i>&nbsp;Marcar Asistencia');
                                content_clasegrupal.push('                      </button>');
                                content_clasegrupal.push('                      <div style="display:' + msg[cl].flagVistaImagenAsistio + '"><i class="fa-solid fa-check"></i>&nbsp;</div>');
                                content_clasegrupal.push('                    </td>');
                                content_clasegrupal.push('            </tr>');

                            }


                        }


                        content_clasegrupal.push('        </tbody>');
                        content_clasegrupal.push('</table>');

                        content_clasegrupal.push('</div>');

                        content_clasegrupal.push('</div>');
                        content_clasegrupal.push('</div>');
                    }

                }

                $('#divclasestiemporeal_salasgrupales').html(content_clasegrupal.join(' '));

            }

            //3.  Anexar armar clases maquinas

            if (listaEncabezado.length > 0) {
                var content_clasemaquina = new Array();

                for (var a = 0; a < listaEncabezado.length; a++) {

                    if (listaEncabezado[a].CodigoSala >= 100) {//recorremos solo salas maquinas
                        content_clasemaquina.push('<div class="col-sm-12 col-lg-12" style="background-color: #e4e6e9;padding-bottom: 10px;">');
                        content_clasemaquina.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 10px;padding: 10px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                        content_clasemaquina.push('<div class="row" style="border-radius:6px;background-color: #0075ff;color:#fff;padding-bottom: 6px;margin-bottom: 10px;margin-left: 0px;margin-right: 0px;">');
                        content_clasemaquina.push('<div class="col-5 border-right text-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 17px;"> HORA: ' + listaEncabezado[a].HoraInicioTexto + ' - ' + listaEncabezado[a].HoraFinTexto + '</div>');
                        content_clasemaquina.push('<div class="col-4 border-right text-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 17px;"> FECHA: ' + listaEncabezado[a].FechaInicioTexto + '</div>');
                        content_clasemaquina.push('<div class="col-3 text-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 17px;"> AFORO: ' + listaEncabezado[a].CantidadPlazas + '</div>');
                        content_clasemaquina.push('</div>');
                        content_clasemaquina.push('<div class="row" style="color:#000;padding-bottom: 6px;margin-bottom: 10px;margin-left: 0px;margin-right: 0px;">');
                        content_clasemaquina.push('<div class="col-12 text-center tx-18" style="font-weight: bold;font-size: 16px;color:#ff0000;text-transform: uppercase;">' + listaEncabezado[a].NotaAlarma + '</div>');
                        content_clasemaquina.push('</div>');

                        content_clasemaquina.push('<div class="row" style="color:#000;padding-bottom: 6px;margin-bottom: 10px;margin-left: 0px;margin-right: 0px;">');
                        content_clasemaquina.push('<div class="col-8 border-right text-center tx-18" style="font-weight:bold;font-size: 17px;">' + listaEncabezado[a].Disciplina + ' - ' + listaEncabezado[a].DesSala + '</div>');
                        content_clasemaquina.push('<div class="col-4 text-center tx-18" style="font-weight: bold;font-size: 17px;"> ESTADO: ' + listaEncabezado[a].CantidadAsistencias + ' de ' + listaEncabezado[a].CapacidadPermitida + '</div>');
                        content_clasemaquina.push('</div>');


                        content_clasemaquina.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;font-weight: bold;">');

                        content_clasemaquina.push('<table class="table">');
                        content_clasemaquina.push('        <thead style="">');
                        content_clasemaquina.push('            <tr>');
                        content_clasemaquina.push('                <th style="background-color: #fff;color: #000;">Cliente</th>');
                        content_clasemaquina.push('                <th style="width: 35px;background-color: #fff;color: #000;">Codigo</th>');
                        content_clasemaquina.push('                <th style="width: 35px;background-color: #fff;color: #000;">Documento</th>');
                        content_clasemaquina.push('                <th style="width: 35px;background-color: #fff;color: #000;">Chat</th>');
                        content_clasemaquina.push('                <th style="width: 35px;background-color: #fff;color: #000;">Estado</th>');
                        content_clasemaquina.push('            </tr>');
                        content_clasemaquina.push('        </thead>');
                        content_clasemaquina.push('        <tbody style="">');

                        for (var cl = 0; cl < msg.length; cl++) {

                            if (listaEncabezado[a].CodigoHorarioClasesTiempoReal == msg[cl].CodigoHorarioClasesTiempoReal) {

                                content_clasemaquina.push('            <tr style="height:10px;">');
                                content_clasemaquina.push('                    <td>');
                                content_clasemaquina.push('                        <div class="media align-items-center">');
                                content_clasemaquina.push('                            <div class="avatar avatar-xs mr-2">');
                                content_clasemaquina.push('                                <img src="' + msg[cl].ImagenUrl + '" alt="Avatar" class="avatar-img rounded-circle">');
                                content_clasemaquina.push('                            </div>');
                                content_clasemaquina.push('                            <div class="media-body">');
                                content_clasemaquina.push('                                <span class="js-lists-values-employee-name">' + msg[cl].Nombres + ', ' + msg[cl].Apellidos + '</span>');
                                content_clasemaquina.push('                            </div>');
                                content_clasemaquina.push('                        </div>');
                                content_clasemaquina.push('                    </td>');
                                content_clasemaquina.push('                    <td><small class="text-muted" style="font-size:13px;font-weight:bold;">' + msg[cl].CodigoSocio + '</small></td>');
                                content_clasemaquina.push('                    <td><small class="text-muted" style="font-size:13px;font-weight:bold;">' + msg[cl].DNI + '</small></td>');
                                content_clasemaquina.push('                    <td><span class="badge" style="font-size:13px;font-weight:bold;display:' + msg[cl].EstadoCelular + '"><a target="_blank" style="color:#000;" href="https://api.whatsapp.com/send?phone=' + msg[cl].Whatsapp + '"><img style="height:18px;" src="/Content/iconos/whatsapp-icono-negro.png" />' + msg[cl].Celular + '</a></span></td>');
                                content_clasemaquina.push('                     <td>');
                                content_clasemaquina.push('                       <button onclick="Confirmar_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias_CheckingMasivo(/' + msg[cl].CodigoHorarioClasesConfiguracion + '/,/' + msg[cl].CodigoHorarioClasesTiempoReal + '/,/' + msg[cl].CodigoHorarioClasesConfiguracionAsistencias + '/,' + msg[cl].CodigoSocio + ',' + msg[cl].CodigoMembresia + ',this);" class="btn btn-primary btn-sm me-1 mb-1" type="button" style="display:' + msg[cl].flagVistaBotonMarcarAsistencia + '">');
                                content_clasemaquina.push('                           <i class="fa-solid fa-check"></i>&nbsp;Marcar Asistencia');
                                content_clasemaquina.push('                       </button>');
                                content_clasemaquina.push('                       <div style="display:' + msg[cl].flagVistaImagenAsistio + '"><i class="fa-solid fa-check"></i>&nbsp;</div>');
                                content_clasemaquina.push('                     </td>');
                                content_clasemaquina.push('            </tr>');

                            }


                        }


                        content_clasemaquina.push('        </tbody>');
                        content_clasemaquina.push('</table>');

                        content_clasemaquina.push('</div>');

                        content_clasemaquina.push('</div>');
                        content_clasemaquina.push('</div>');
                    }

                }

                $('#divclasestiemporeal_salamaquinas').html(content_clasemaquina.join(' '));

            }

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}

//FIN RESERVAS


//funcion para la reserva de clases

function uspListarSalas() {

    var entidad = {};

    var metodoCorrecto = function (data) {

        var content_salamaquinas = new Array();
        if (data.length > 0) {

            $('#hdflag_consalamaquinas').val('1');
            for (var i = 0; i < data.length; i++) {
                content_salamaquinas.push('<option value="' + data[i].CodigoSala + '">' + data[i].Descripcion + '</option>');
            }

            content_salamaquinas.push('<option value="1">SALAS GRUPALES</option>');
        } else {
            $('#hdflag_consalamaquinas').val('0');
            content_salamaquinas.push('<option value="1">SALAS GRUPALES</option>');
        }

        $('#ddlTipoSala').html(content_salamaquinas.join(' '));

        $("#ddlTipoSala").kendoDropDownList({
            change: function () {
                buscarReservas();
            }
        });

        //Obtener3FechasTodo();
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/gestionce/uspListarSalaMaquinas_Presencial', request, metodoCorrecto, metodoError);
}


//funciones asistencia de colaboradores
function eventClick_ModalChekingColaboradores() {

    event_escribiendo();

    $('#divPaso1_contenedorMarcarAsistenciaPersonal').show('fast');
    $('#divPaso2_contenedorMarcarAsistenciaPersonal').hide('fast');

    if ($('#hdflagModalCheckingColaboradores').val() == '0') {
        $('#hdflagModalCheckingColaboradores').val('1');


        $("#gridPersonalFijo_AsistenciaTurno1").kendoGrid({
            height: 200
        });
        $("#gridPersonalFijo_AsistenciaTurno2").kendoGrid({
            height: 200
        });

        ListarPersonalAdministrativo();

    }

}

function ListarPersonalAdministrativo() {
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        type: "POST",
        url: "/operacionesfit/ListarPersonalAdministrativo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var lista = msg.List
            for (var i = 0; i < lista.length; i++) {
                if (lista[i].FechaNacimiento != null || lista[i].FechaNacimiento != '') {
                    lista[i].FechaNacimiento = kendo.toString(new Date(parseInt(lista[i].FechaNacimiento.replace('/Date(', ''))), 'dd/MM/yyyy');
                }
            }

            var ControlHTML = '';
            for (var i = 0; i < lista.length; i++) {
                ControlHTML += '<div class="col-md-6 col-xl-2">                                                                                                                                                                                                                ';
                ControlHTML += '    <div class="card m-b-30">                                                                                                                                                                                                                  ';
                ControlHTML += '        <div class="member-card pt-2">                                                                                                                                                                                                         ';
                ControlHTML += '            <div class="thumb-lg member-thumb mx-auto"><img src="' + lista[i].UrlImagen + '" class="rounded-circle img-thumbnail" alt="profile-image"></div>                                                           ';
                ControlHTML += '                <div class="">                                                                                                                                                                                                                 ';
                ControlHTML += '                    <center>                                                                                                                                                                                                                   ';
                ControlHTML += '                        <h5 style="font-size:12px;font-weight:bold;">' + lista[i].Nombres.toString().toUpperCase() + ', ' + lista[i].ApellidoPaterno.toString().toUpperCase() + '</h5>                                                                                                                                             ';
                ControlHTML += '                        <h5 style="font-size:11px;">' + lista[i].DescripcionCargo + '</h5>                                                                                                                                                                     ';
                ControlHTML += '                        <p class="text-muted" style="font-size:11px;font-weight:bold;">Nro doc: ' + lista[i].NumeroDocumento + '</p>                                                                                                                                   ';
                ControlHTML += '                    </center>                                                                                                                                                                                                                  ';
                ControlHTML += '                </div>                                                                                                                                                                                                                         ';
                ControlHTML += '            </div>                                                                                                                                                                                                                             ';
                ControlHTML += '            <ul class="list-group list-group-flush" style="margin-top: -20px;">                                                                                                                                                                ';
                ControlHTML += '                <li class="list-group-item" style="font-size:11px;">                                                                                                                                                                           ';
                ControlHTML += '                    <center class="float-left"><a style="display:' + lista[i].EstadoCelular + '" target="_blank" href="https://api.whatsapp.com/send?phone=' + lista[i].Celular + '"> <img src="/Content/app/img/whatsapp.png" style="height:12px;cursor:pointer;"> </a></center>    ';
                ControlHTML += '                        &nbsp;&nbsp;' + lista[i].Celular + '                                                                                                                                                                                                ';
                ControlHTML += '                    </li>                                                                                                                                                                                                                      ';
                ControlHTML += '                </ul>                                                                                                                                                                                                                          ';
                ControlHTML += '                <div class="card-body">                                                                                                                                                                                                        ';
                ControlHTML += '                    <div class="float-right btn-group btn-group-sm">                                                                                                                                                                           ';
                ControlHTML += '                        <a href="#" onclick="javascript:StartAsistensia(/' + lista[i].NumeroDocumento + '/);" class="btn btn-primary tooltips" data-placement="top" data-toggle="tooltip" data-original-title="Visualizar el horario del personal"><i class="fa fa-pencil"></i> Ver horario</a>                          ';
                ControlHTML += '                    </div>                                                                                                                                                                                                                     ';
                ControlHTML += '                    <ul class="social-links list-inline mb-0">                                                                                                                                                                                 ';
                ControlHTML += '                    </ul>                                                                                                                                                                                                                      ';
                ControlHTML += '                </div>                                                                                                                                                                                                                         ';
                ControlHTML += '            </div>                                                                                                                                                                                                                             ';
                ControlHTML += '        </div>                                                                                                                                                                                                                                 ';

            }

            $('#divContenedorPersonalAdministrativo').html(ControlHTML);


            //options.success(lista);
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            ListarPersonalClasesGrupales();
        },
        error: function (a, b, c) {
            alert(a);
        }
    });


};

function ListarPersonalClasesGrupales() {
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        type: "POST",
        url: "/gestionce/uspListarPersonalClasesGrupales",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var lista = msg.List
            //for (var i = 0; i < lista.length; i++) {
            //    if (lista[i].FechaNacimiento != null || lista[i].FechaNacimiento != '') {
            //        lista[i].FechaNacimiento = kendo.toString(new Date(parseInt(lista[i].FechaNacimiento.replace('/Date(', ''))), 'dd/MM/yyyy');
            //    }
            //}

            var ControlHTML = '';
            for (var i = 0; i < lista.length; i++) {
                ControlHTML += '<div class="col-md-6 col-xl-2">                                                                                                                                                                                                                ';
                ControlHTML += '    <div class="card m-b-30">                                                                                                                                                                                                                  ';
                ControlHTML += '        <div class="member-card pt-2">                                                                                                                                                                                                         ';
                ControlHTML += '            <div class="thumb-lg member-thumb mx-auto"><img src="' + lista[i].ImagenUrl + '" class="rounded-circle img-thumbnail" alt="profile-image"></div>                                                           ';
                ControlHTML += '                <div class="">                                                                                                                                                                                                                 ';
                ControlHTML += '                    <center>                                                                                                                                                                                                                   ';
                ControlHTML += '                        <h5 style="font-size:12px;font-weight:bold;">' + lista[i].NombreCompleto.toString().toUpperCase() + '</h5>                                                                                                                                             ';
                ControlHTML += '                        <h5 style="font-size:11px;"> Profesor clase grupal</h5>                                                                                                                                                                     ';
                ControlHTML += '                        <p class="text-muted" style="font-size:11px;font-weight:bold;">Nro doc: ' + lista[i].NroDocumento + '</p>                                                                                                                                   ';
                ControlHTML += '                    </center>                                                                                                                                                                                                                  ';
                ControlHTML += '                </div>                                                                                                                                                                                                                         ';
                ControlHTML += '            </div>                                                                                                                                                                                                                             ';
                ControlHTML += '            <ul class="list-group list-group-flush" style="margin-top: -20px;">                                                                                                                                                                ';
                ControlHTML += '                <li class="list-group-item" style="font-size:11px;">                                                                                                                                                                           ';
                ControlHTML += '                    <center class="float-left"><a style="display:' + lista[i].EstadoCelular + '" target="_blank" href="https://api.whatsapp.com/send?phone=' + lista[i].Celular + '"> <img src="/Content/app/img/whatsapp.png" style="height:12px;cursor:pointer;"> </a></center>    ';
                ControlHTML += '                        &nbsp;&nbsp;' + lista[i].Celular + '                                                                                                                                                                                                ';
                ControlHTML += '                    </li>                                                                                                                                                                                                                      ';
                ControlHTML += '                </ul>                                                                                                                                                                                                                          ';
                ControlHTML += '                <div class="card-body">                                                                                                                                                                                                        ';
                ControlHTML += '                    <div class="float-right btn-group btn-group-sm">                                                                                                                                                                           ';
                ControlHTML += '                        <a href="#" onclick="javascript:StartAsistensia(/' + lista[i].NroDocumento + '/);" class="btn btn-primary tooltips" data-placement="top" data-toggle="tooltip" data-original-title="Visualizar el horario del personal"><i class="fa fa-pencil"></i> Ver horario</a>                          ';
                ControlHTML += '                    </div>                                                                                                                                                                                                                     ';
                ControlHTML += '                    <ul class="social-links list-inline mb-0">                                                                                                                                                                                 ';
                ControlHTML += '                    </ul>                                                                                                                                                                                                                      ';
                ControlHTML += '                </div>                                                                                                                                                                                                                         ';
                ControlHTML += '            </div>                                                                                                                                                                                                                             ';
                ControlHTML += '        </div>                                                                                                                                                                                                                                 ';

            }

            $('#divContenedorPersonalClasesGrupales').html(ControlHTML);


            //options.success(lista);
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        },
        error: function (a, b, c) {
            alert(a);
        }
    });


};

function StartAsistensia(NumeroDoc) {

    IniciarMarcacionPersonalGeneral(NumeroDoc);
};

function IniciarMarcacionPersonalGeneral(NroDocumento) {

    NroDocumento = NroDocumento.toString().replace('/', '').replace('/', '');
    $('#txtNroDocumentoPersonalGeneral').val(NroDocumento);

    var entidad = {
        request: {
            NumeroDocumento: NroDocumento
        }
    };

    var metodoCorrecto = function (data) {

        if (data.List.length > 0) {
            $('#divPaso1_contenedorMarcarAsistenciaPersonal').hide('fast');
            $('#divPaso2_contenedorMarcarAsistenciaPersonal').show('fast');
            for (var i = 0; i < data.List.length; i++) {


                if (data.List[i].TipoPersonal == 1) {

                    document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = 'none';
                    document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = 'none';
                    document.getElementById("div_ListaHorarioClases").style.display = '';

                    PoblarDatosAsistenciaProfesores(data.List[i]);

                }
                else if (data.List[i].TipoPersonal == 2) {
                    document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = '';
                    document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = '';
                    document.getElementById("div_ListaHorarioClases").style.display = 'none';

                    if (data.List[i].PersonalAdministrativo != null) {

                        if (data.List[i].PersonalAdministrativo.Vigencia == 'False') {
                            $.bootstrapGrowl('Personal con el DNI ' + $('#txtNroDocumentoPersonalGeneral').val() + ' esta inactivo', { type: 'danger', width: 'auto' });
                        }
                        else {
                            //$('#MarcarAsistenciaPersonalFijo').css('display', 'block');
                            PoblarDatosAsistenciaPersonalFijo(data.List[i]);
                        }

                    }
                }
            }
        } else {
            $('#divPaso1_contenedorMarcarAsistenciaPersonal').show('fast');
            $('#divPaso2_contenedorMarcarAsistenciaPersonal').hide('fast');
            $.bootstrapGrowl("No encotramos información.", { type: 'danger', width: 'auto' });
        }
    };

    var metodoError = function (data) {
        alert(data.responseText);
    };

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: JSON.stringify(entidad),
        type: "POST",
        url: "/gestionce/ListarPersonalAsistenciaGeneralPorNroDocumento",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            document.getElementById('loadMe').style.display = 'block';
            document.getElementById('myModalAsistenciaColaborador').style.display = 'none';

        },
        complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            $('button[type="button"]').attr("disabled", false);
            document.getElementById('myModalAsistenciaColaborador').style.display = 'block';
        },
        success: function (msg) {
            var data = msg;
            metodoCorrecto(data);
        },
        error: function (a) {
            metodoError(a);
        }
    });
}

function PoblarDatosAsistenciaPersonalFijo(entidad) {

    if (entidad != null) {
        if (entidad.PersonalAdministrativo != null) {

            // update image in dom
            $('#imgVerFotoColaborador').attr('src', entidad.PersonalAdministrativo.UrlImagen);
            $('#txtNombreCompletoPersonalGeneral').val(entidad.PersonalAdministrativo.Nombres + ' ' + entidad.PersonalAdministrativo.ApellidoPaterno);
            $('#lblPersonalFijo_Cargo').html(entidad.PersonalAdministrativo.DescripcionCargo);
            var FechaNacimiento = new Date(parseInt(entidad.PersonalAdministrativo.FechaNacimiento.replace('/Date(', '')));
            $('#lblPersonalFijo_FechaNacimiento').html('F. Nacimiento: ' + FechaNacimiento.ddmmyyyy('/'));

            if (!IsUndefinedOrNullOrEmpty(entidad.PersonalAdministrativo.Email)) {
                $('#lblPersonalFijo_Correo').html('Correo: ' + entidad.PersonalAdministrativo.Email);
            } else {
                $('#lblPersonalFijo_Correo').html("sin correo");
            }

            if (!IsUndefinedOrNullOrEmpty(entidad.PersonalAdministrativo.Direccion)) {
                $('#lblPersonalFijo_Direccion').html('Dirección: ' + entidad.PersonalAdministrativo.Direccion);
            } else {
                $('#lblPersonalFijo_Direccion').html("sin dirección");
            }

            if (!IsUndefinedOrNullOrEmpty(entidad.PersonalAdministrativo.Celular)) {
                $('#lblPersonalFijo_Celular').html('Celular: ' + entidad.PersonalAdministrativo.Celular);
            } else {
                $('#lblPersonalFijo_Celular').html("sin celular");
            }

        }

        //MOSTRAR  Y OCULTAR GRID DE ASISTENCIA
        //tipoTurno 0 = SIN HORARIO LABORAL,
        //1 = HORARIO LABORAL SOLO MAÑANA,
        //2 = HORARIO LABORAL SOLO TARDE,
        //3 HORARIO LABORAL MAÑANA Y TARDE
        if (!IsUndefinedOrNullOrEmpty(entidad.tipoTurno)) {
            if (entidad.tipoTurno == 0) {
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = 'none';
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = 'none';
            } else if (entidad.tipoTurno == 1) {
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = '';
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = 'none';
            } else if (entidad.tipoTurno == 2) {
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = 'none';
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = '';
            }
            else if (entidad.tipoTurno == 3) {
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = '';
                document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = '';
            }
        } else {
            document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = 'none';
            document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = 'none';
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.CodigoPersonalAsistencia)) {
            $('#AsistenciaPersonalAdm_NumeroDocumento').attr('data-codigopersonalasistencia', entidad.CodigoPersonalAsistencia);
        }
        else {
            $('#AsistenciaPersonalAdm_NumeroDocumento').attr('data-codigopersonalasistencia', '');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraIngreso)) {

            //var FHoraIngreso = new Date(parseInt(entidad.FechaHoraIngreso.replace('/Date(', '')));
            //alert(FHoraIngreso.format('h:mm:ss a'));
            $('#AsistenciaPersonalAdm_FHoraIngreso').html(entidad.FechaHoraIngresoTexto);
            $('#AsistenciaPersonalAdm_FHoraIngreso').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_FHoraIngreso').attr("disabled", true);
        } else {
            $('#btnAsistenciaPersonalAdm_FHoraIngreso').attr("disabled", false);
            $('#AsistenciaPersonalAdm_FHoraIngreso').css('color', '#000000');
            $('#AsistenciaPersonalAdm_FHoraIngreso').html('0:00');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraRefrigerioSalida)) {
            //var FHoraRefrigerioSalida = new Date(parseInt(entidad.FechaHoraRefrigerioSalida.replace('/Date(', '')));
            $('#AsistenciaPersonalAdm_RefrigerioSalida').html(entidad.FechaHoraRefrigerioSalidaTexto);
            $('#AsistenciaPersonalAdm_RefrigerioSalida').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_RefrigerioSalida').attr("disabled", true);
        } else {
            $('#AsistenciaPersonalAdm_RefrigerioSalida').css('color', '#000000');
            $('#btnAsistenciaPersonalAdm_RefrigerioSalida').attr("disabled", false);
            $('#AsistenciaPersonalAdm_RefrigerioSalida').html('0:00');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraRefrigerioRetorno)) {
            //var FHoraRefrigerioRetorno = new Date(parseInt(entidad.FechaHoraRefrigerioRetorno.replace('/Date(', '')));
            $('#AsistenciaPersonalAdm_RefrigerioRetorno').html(entidad.FechaHoraRefrigerioRetornoTexto);
            $('#AsistenciaPersonalAdm_RefrigerioRetorno').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_RefrigerioRetorno').attr("disabled", true);
        } else {
            $('#AsistenciaPersonalAdm_RefrigerioRetorno').css('color', '#000000');
            $('#btnAsistenciaPersonalAdm_RefrigerioRetorno').attr("disabled", false);
            $('#AsistenciaPersonalAdm_RefrigerioRetorno').html('0:00');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraSalida)) {
            //var FHoraSalida = new Date(parseInt(entidad.FechaHoraSalida.replace('/Date(', '')));
            $('#AsistenciaPersonalAdm_FHoraSalida').html(entidad.FechaHoraSalidaTexto);
            $('#AsistenciaPersonalAdm_FHoraSalida').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_FHoraSalida').attr("disabled", true);
        } else {
            $('#AsistenciaPersonalAdm_FHoraSalida').css('color', '#000000');
            $('#btnAsistenciaPersonalAdm_FHoraSalida').attr("disabled", false);
            $('#AsistenciaPersonalAdm_FHoraSalida').html('0:00');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraIngreso_TurnoTarde)) {
            //var FHoraIngreso = new Date(parseInt(entidad.FechaHoraIngreso_TurnoTarde.replace('/Date(', '')));
            $('#AsistenciaPersonalAdm_FHoraIngreso_TurnoTarde').html(entidad.FechaHoraIngreso_TurnoTardeTexto);
            $('#AsistenciaPersonalAdm_FHoraIngreso_TurnoTarde').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_FHoraIngreso_TurnoTarde').attr("disabled", true);
        } else {
            $('#AsistenciaPersonalAdm_FHoraIngreso_TurnoTarde').css('color', '#000000');
            $('#btnAsistenciaPersonalAdm_FHoraIngreso_TurnoTarde').attr("disabled", false);
            $('#AsistenciaPersonalAdm_FHoraIngreso_TurnoTarde').html('0:00');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraRefrigerioSalida_TurnoTarde)) {
            //var FHoraRefrigerioSalida = new Date(parseInt(entidad.FechaHoraRefrigerioSalida_TurnoTarde.replace('/Date(', '')));
            $('#AsistenciaPersonalAdm_RefrigerioSalida_TurnoTarde').html(entidad.FechaHoraRefrigerioSalida_TurnoTardeTexto);
            $('#AsistenciaPersonalAdm_RefrigerioSalida_TurnoTarde').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_RefrigerioSalida_TurnoTarde').attr("disabled", true);
        } else {
            $('#AsistenciaPersonalAdm_RefrigerioSalida_TurnoTarde').css('color', '#000000');
            $('#btnAsistenciaPersonalAdm_RefrigerioSalida_TurnoTarde').attr("disabled", false);
            $('#AsistenciaPersonalAdm_RefrigerioSalida_TurnoTarde').html('0:00');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraRefrigerioRetorno_TurnoTarde)) {
            //var FHoraRefrigerioRetorno = new Date(parseInt(entidad.FechaHoraRefrigerioRetorno_TurnoTarde.replace('/Date(', '')));
            $('#AsistenciaPersonalAdm_RefrigerioRetorno_TurnoTarde').html(entidad.FechaHoraRefrigerioRetorno_TurnoTardeTexto);
            $('#AsistenciaPersonalAdm_RefrigerioRetorno_TurnoTarde').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_RefrigerioRetorno_TurnoTarde').attr("disabled", true);
        } else {
            $('#AsistenciaPersonalAdm_RefrigerioRetorno_TurnoTarde').css('color', '#000000');
            $('#btnAsistenciaPersonalAdm_RefrigerioRetorno_TurnoTarde').attr("disabled", false);
            $('#AsistenciaPersonalAdm_RefrigerioRetorno_TurnoTarde').html('0:00');
        }

        if (!IsUndefinedOrNullOrEmpty(entidad.FechaHoraSalida_TurnoTarde)) {
            //var FHoraSalida = new Date(parseInt(entidad.FechaHoraSalida_TurnoTarde.replace('/Date(', '')));
            $('#AsistenciaPersonalAdm_FHoraSalida_TurnoTarde').html(entidad.FechaHoraSalida_TurnoTardeTexto);
            $('#AsistenciaPersonalAdm_FHoraSalida_TurnoTarde').css('color', '#0075ff');
            $('#btnAsistenciaPersonalAdm_FHoraSalida_TurnoTarde').attr("disabled", true);
        } else {
            $('#AsistenciaPersonalAdm_FHoraSalida_TurnoTarde').css('color', '#000000');
            $('#btnAsistenciaPersonalAdm_FHoraSalida_TurnoTarde').attr("disabled", false);
            $('#AsistenciaPersonalAdm_FHoraSalida_TurnoTarde').html('0:00');
        }


    }
    else {
        $('#AsistenciaPersonalAdm_NumeroDocumento').val('');
        $('#AsistenciaPersonalAdm_NumeroDocumento').focus();
        document.getElementById("div_gridPersonalFijo_AsistenciaTurno1").style.display = 'none';
        document.getElementById("div_gridPersonalFijo_AsistenciaTurno2").style.display = 'none';
    }
}

function IniciarMarcacionPersonalAdministrativoPorIngreso(NumeroOperacion) {
    var NroDocumento = $('#txtNroDocumentoPersonalGeneral').val();
    var entidad = {
        request: {
            NumeroDocumento: NroDocumento,
            FechaHoraIngreso: new Date(),
            EsMarcacionPorConfig: true,
            OperacionMarcacion: NumeroOperacion
        }
    };

    $.ajax({
        data: JSON.stringify(entidad),
        type: "POST",
        url: "/gestionce/RegistrarAsistenciaPersonalAdministrativoPorConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            document.getElementById('loadMe').style.display = 'block';
            document.getElementById('myModalAsistenciaColaborador').style.display = 'none';
        },
        complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModalAsistenciaColaborador').style.display = 'block';
        },
        success: function (msg) {

            var data = msg;
            if (data.Success) {
                $.bootstrapGrowl('marcacion correcta', { type: 'success', width: 'auto' });
                var NumeroDoc = $('#txtNroDocumentoPersonalGeneral').val();
                IniciarMarcacionPersonalGeneral(NumeroDoc);
                //$('#txtNroDocumentoPersonalGeneral').focus();

            }
            else {
                if (data.MessageList.length > 0) {
                    //$.bootstrapGrowl(data.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
                    alert(data.MessageList[0].Detalle);
                    NuevaMarcacionAsistenciaPersonal();
                }
                document.getElementById('loadMe').style.display = 'none';
            }
        },
        error: function (a) {
            alert(a.responseText);
        }
    });
};

function PoblarDatosAsistenciaProfesores(entidad) {
    if (entidad != null) {
        if (entidad.ProfesionalFitness != null) {
            var FechaNacimiento = new Date(parseInt(entidad.ProfesionalFitness.FechaNacimiento.replace('/Date(', '')));
            $('#imgVerFotoColaborador').attr('src', entidad.ProfesionalFitness.ImagenUrl);
            $('#txtNombreCompletoPersonalGeneral').val(entidad.ProfesionalFitness.Nombres + ' ' + entidad.ProfesionalFitness.Apellidos);

            $('#lblPersonalFijo_Cargo').html('Profesor Eventual');
            $('#lblPersonalFijo_FechaNacimiento').html('F. Nacimiento: ' + FechaNacimiento.ddmmyyyy('/'));
            if (entidad.ProfesionalFitness.Correo == '' || entidad.ProfesionalFitness.Correo == null)
                $('#lblPersonalFijo_Correo').html('sin correo');
            else
                $('#lblPersonalFijo_Correo').html('Correo: ' + entidad.ProfesionalFitness.Correo);

            $('#lblPersonalFijo_Celular').html('Celular: ' + entidad.ProfesionalFitness.Celular);
            $('#lblPersonalFijo_Direccion').html('Dirección: ' + entidad.ProfesionalFitness.Direccion);
            //$('#Image_FotoPersona').attr('src', entidad.ProfesionalFitness.ImagenUrl);


            var Lista = entidad.ProfesionalFitness.ListaHorarioClases;
            for (var i = 0; i < Lista.length; i++) {
                Lista[i].CodigoPersonalAsistencia = Lista[i].CodigoPersonalAsistencia;
                Lista[i].CodigoHorarioClasesConfiguracion = Lista[i].CodigoHorarioClasesConfiguracion;
                Lista[i].CodigoHorarioClasesTiempoReal = Lista[i].CodigoHorarioClasesTiempoReal;
                Lista[i].HoraInicio = kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].HoraInicio), "hh:mm tt");
                Lista[i].HoraFin = kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].HoraFin), "hh:mm tt");
                Lista[i].DesSala = Lista[i].DesSala;
                Lista[i].Disciplina = Lista[i].Disciplina;

                //alert("FechaHoraIngreso: " + kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].FechaHoraIngreso),'yyyy'));
                //alert("FechaHoraSalida: " + kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].FechaHoraSalida), 'yyyy'));

                //if (!IsUndefinedOrNullOrEmpty(Lista[i].FechaHoraIngreso)) {
                if (kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].FechaHoraIngreso), 'yyyy') != '0000') {
                    Lista[i].FechaHoraIngresoAsistencia = kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].FechaHoraIngreso), "HH:mm tt");
                    Lista[i].DisabledIngreso = 'disabled="disabled"';
                }
                else {
                    Lista[i].FechaHoraIngresoAsistencia = '--:--';
                    Lista[i].DisabledIngreso = '';
                }

                if (kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].FechaHoraSalida), 'yyyy') != '0000') {
                    Lista[i].FechaHoraSalidaAsistencia = kendo.toString(ConvertirJsonFechaToDatetime(Lista[i].FechaHoraSalida), "HH:mm tt");
                    Lista[i].DisabledSalida = 'disabled="disabled"';
                }
                else {
                    Lista[i].FechaHoraSalidaAsistencia = '--:--';
                    Lista[i].DisabledSalida = '';
                }

                if (Lista[i].CodigoPersonalAsistencia == null) {
                    Lista[i].DisabledSalida = 'disabled="disabled"';
                }

                //Lista[i].canSelect = false;
            }
            CargarGridListaHorarioClases(Lista);
        }

    }
    else {
        $('#lblPersonalFijo_Cargo').html('');
        $('#lblPersonalFijo_Celular').html('');
        $('#lblPersonalFijo_Correo').html('');
        $('#lblPersonalFijo_FechaNacimiento').html('');
        $('#lblPersonalFijo_Distrito').html('');
        $('#lblPersonalFijo_Direccion').html('');
    }
}

function CargarGridListaHorarioClases(data) {
    $("#ListaHorarioClases").html('');
    //alert("CargarGridListaHorarioClases: " + data.length);
    if (data.length > 0) {
        document.getElementById('div_ListaHorarioClases').style.display = '';
        $("#ListaHorarioClases").kendoGrid({
            dataSource: {
                data: data
            },
            height: 430,
            columns: [
                {
                    title: "<center><b style='color:#fff;'>Clase</b></center>",
                    width: 20,
                    template: '<h5 style="font-size:13px;font-weight:bold;">#: DesSala # - #: Disciplina #</h5><h5 style="font-size:15px;font-weight:bold;" > #: HoraInicio # - #: HoraFin #</h5><h5 style="font-size:15px;font-weight:bold;" > AFORO #: CantidadAsistencias # de #: CapacidadPermitida #</h5>',
                    attributes: {
                        style: "font-size:14px;font-weight:bold;text-align:center;"
                    }
                }, {
                    title: "",
                    width: 22,
                    template: "<button #: DisabledIngreso # onclick='IniciarMarcacionAsistenciaProfesor(\"#: CodigoHorarioClasesConfiguracion #\",\"#: CodigoHorarioClasesTiempoReal #\",\"#: CodigoProfesional #\",\"#: CodigoPersonalAsistencia #\",#: DiaNumero #,1);' class='btn btn-primary' style='width:75%;font-size:12px;'>MARCAR INGRESO</button >"
                }, {
                    field: "FechaHoraIngresoAsistencia",
                    title: "<center><b style='color:#fff;text-align:center;'>Ingreso</b></center>",
                    width: 11,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;text-align:center;"
                    }
                }, {
                    field: "FechaHoraSalidaAsistencia",
                    title: "<center><b style='color:#fff;text-align:center;'>Salida</b></center>",
                    width: 11,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;text-align:center;"
                    }
                }, {
                    title: "",
                    width: 22,
                    template: "<button #: DisabledSalida # onclick='IniciarMarcacionAsistenciaProfesor(\"#: CodigoHorarioClasesConfiguracion #\",\"#: CodigoHorarioClasesTiempoReal #\",\"#: CodigoProfesional #\",\"#: CodigoPersonalAsistencia #\",#: DiaNumero #,2);' class='btn btn-primary' style='width:75%;font-size:12px;'  > MARCAR SALIDA</button >"
                }
            ]
        });
    }
    else {
        $("#ListaHorarioClases").html('<span style="font-size:18px;color:red;font-weight:bold;">NO TIENE CLASE PROGRAMANDA</span>');
    }
};

var IniciarMarcacionAsistenciaProfesor = function (CodigoHorarioClasesConfiguracion, CodigoHorarioClasesTiempoReal, CodigoProfesional, CodigoPersonalAsistencia, DiaNumero, TipoAsistencia) {

    var entidadDTO = {
        request: {
            CodigoHorarioClasesConfiguracion: CodigoHorarioClasesConfiguracion,
            CodigoHorarioClasesTiempoReal: CodigoHorarioClasesTiempoReal,
            CodigoProfesional: CodigoProfesional,
            CodigoPersonalAsistencia: CodigoPersonalAsistencia,
            DiaNumero: DiaNumero,
            TipoAsistencia: TipoAsistencia
        }
    };

    $.ajax({
        data: JSON.stringify(entidadDTO),
        type: "POST",
        url: "/gestionce/RegistrarAsistenciaProfesores",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            document.getElementById('loadMe').style.display = 'block';
            document.getElementById('myModalAsistenciaColaborador').style.display = 'block';
        },
        success: function (msg) {

            var data = msg;
            if (data.Success) {
                $.bootstrapGrowl('marcacion correcta', { type: 'success', width: 'auto' });

                var NumeroDoc = $('#txtNroDocumentoPersonalGeneral').val();
                IniciarMarcacionPersonalGeneral(NumeroDoc)
            }
            else {
                if (data.MessageList.length > 0) {
                    $.bootstrapGrowl(data.MessageList[0].Detalle, { type: 'danger', width: 'auto' });
                }
            }

        },
        complete: function () {
            //StartAsistensia();
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModalAsistenciaColaborador').style.display = 'block';
        }
    });
};


Date.prototype.yyyymmdd = function () {
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
};
Date.prototype.HHmmss = function () {

    var Hora = this.getHours();
    var Minuto = this.getMinutes();
    var Segundo = this.getSeconds();
    if (Hora < 10)
        Hora = "0" + Hora;
    if (Minuto < 10)
        Minuto = "0" + Minuto;
    if (Segundo < 10)
        Segundo = "0" + Segundo;
    return Hora + ':' + Minuto + ':' + Segundo;
};
Date.prototype.ddmmyyyy = function (separador) {
    separador = separador == null ? '/' : separador;
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based
    var dd = this.getDate().toString();
    return (dd[1] ? dd : "0" + dd[0]) + separador + (mm[1] ? mm : "0" + mm[0]) + separador + yyyy;
};


var ConvertirJsonFechaToDatetime = function (v) {
    return new Date(parseInt(v.replace('/Date(', '')));
}

//funciones

function BuscarCodigoSocioPrimerRegistro() {

    $.ajax({
        data: '{"User":"' + User + '"}',
        type: "POST",
        url: "/gestionce/BuscarInformacionPrimerSocioActivo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg != null) {
                $('#txtBuscadorGeneral').val(msg);
            }
            var CodigoSocio = $('#txtBuscadorGeneral').val();
            BuscarInformacionSociosPorCodigo(CodigoSocio);

        },
        error: function (e) {
            alert(e.responseText);
        }
    });
}

function BuscarInformacionSociosPorCodigo(codigo) {

    $.ajax({
        data: '{"codigo":"' + codigo + '"}',
        type: "POST",
        url: "/gestionce/BuscarInformacionSociosPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg == null) {
                $.bootstrapGrowl("El socio no existe.", { type: 'danger', width: 'auto' });
                event_mostrarBienvenida();

            } else {

                event_mostrarDatosSocio();

                alert(msg.ImagenUrlCarnetVacunacion);
                $('#imgFotoCarnetVacunacion').attr('src', msg.ImagenUrlCarnetVacunacion);
                if (msg.ImagenUrl == null) {
                    $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='Agregar Fotos' style='Cursor:pointer;'><img  src='../Imagenes/fitness/PerfilHombre.png' class='contrast' style='width:290px;height: 180px;border-radius: 5px;'></div>");
                    $('#FotoCliente_qrcode').attr('src', '../Imagenes/fitness/PerfilHombre.png');
                    $('#lblCumpleaniosSocios').html("");
                    $('#btnAvisoFoto').show('fast');
                    $('#ModalAvisosImportantes').hide('fast');
                    $('#imgVer_FotoSocio').attr('src', '../Imagenes/fitness/PerfilHombre.png');
                } else if (msg.ImagenUrl == undefined) {
                    $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='Agregar Fotos' style='Cursor:pointer;'><img  src='../Imagenes/fitness/PerfilHombre.png' class='contrast' style='width:290px;height: 180px;border-radius: 5px;'></div>");
                    $('#FotoCliente_qrcode').attr('src', '../Imagenes/fitness/PerfilHombre.png');
                    $('#lblCumpleaniosSocios').html("");
                    $('#btnAvisoFoto').show('fast');
                    $('#ModalAvisosImportantes').hide('fast');
                    $('#imgVer_FotoSocio').attr('src', '../Imagenes/fitness/PerfilHombre.png');
                } else {
                    $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='Agregar Fotos' style='Cursor:pointer;'><img  src='" + msg.ImagenUrl + "' class='contrast' style='width:290px;height: 180px;border-radius: 5px;'></div>");
                    $('#FotoCliente_qrcode').attr('src', msg.ImagenUrl);
                    $('#lblCumpleaniosSocios').html("<div style='font-size:60px;font-family: cursive;color: black;display: " + msg.flagCumpleanios + "'><img src='/Content/appfitplataformapersonafit/assets/images/globofiesta.png' style='width:155px;display: " + msg.flagCumpleanios + "' class='img-circle' title='Feliz Cumpleaños'/>&nbsp Hoy esta de cumpleaños</div>");
                    $('#btnAvisoFoto').hide('fast');
                    if (msg.flagCumpleanios != 'none') {
                        $('#ModalAvisosImportantes').show('fast');
                    }

                    $('#imgVer_FotoSocio').attr('src', msg.ImagenUrl);
                }

                //$('#lblNombreCompleto2').html((msg.Nombres).toUpperCase() + ' ' + (msg.Apellidos).toUpperCase());
                $('#txtBuscadorGeneral').val(msg.CodigoSocio);
                $('#lblNombreCompleto').val((msg.Nombres).toUpperCase() + ' , ' + (msg.Apellidos).toUpperCase());
                $('#lblClienteSuplemento').html((msg.Nombres).toUpperCase() + ' , ' + (msg.Apellidos).toUpperCase());

                $("#lblNombreCompleto").css('font-size', '22px');
                $("#lblNombreCompleto").css('font-weight', 'bold');
                $('#infoDNI').html(msg.DNI);
                $('#infoFechaCreacion').val(msg.DescFechaCreacion);

                $('#infoUsuarioCreacion').val(msg.UsuarioCreacion);

                if (msg.Celular != '') {
                    $('#infoCelular').html(msg.Celular);
                    $('#imginfoCelular').attr('href', 'https://api.whatsapp.com/send?phone=' + msg.Celular);
                    $('#imginfoCelular').css('display', 'block');

                } else {
                    $('#infoCelular').html('sin whatsapp');
                    $('#imginfoCelular').css('display', 'none');
                }

                $('#infoFechaNacimiento').val(msg.DescFechaNacimiento);

                $('#infoEdad').val(msg.Edad);
                $('#infoDireccion').val(msg.Direccion);
                $('#infoDistrito').val(msg.Distrito);

                $('#infoCorreo').val(msg.Correo);
                $('#lblVendedorInfoClienteRepartido').html(msg.UserAsesorVenta);
                if (msg.EstadoCivil == 1) {
                    $('#lblEstadoCivil').val('Soltero');
                } else if (msg.EstadoCivil == 2) {
                    $('#lblEstadoCivil').val('Casado');
                } else {
                    $('#lblEstadoCivil').val('Viudo');
                }

                if (msg.EstadoHijos == 1) {
                    $('#lblHijos').val('Si');
                } else {
                    $('#lblHijos').val('No');
                }
                $('#lblOcupacion').val(msg.Ocupacion);
                $('#lblTelefonoTrabajo').val(msg.TelefonoTrabajo);
                $('#lblLugarTrabajo').val(msg.DireccionTrabajo);

                var face = msg.UrlFacebook;

                if (face != '' && face != null) {
                    //$('#imginfoFacebook').attr('href', msg.UrlFacebook);
                    $('#imginfoFacebook').val(msg.UrlFacebook);
                } else {
                    $('#imginfoFacebook').val('Sin facebook.');

                    //$('#imginfoFacebook').attr('href', 'www.facebook.com');
                }

                $('#infoCodigo').html(msg.CodigoSocio);
                $('#lblSede').val(msg.desSede);

                var url = msg.ImagenUrl == undefined ? "../Imagenes/fitness/anonimoProducto.jpg" : msg.ImagenUrl;
                $('#ImagenSubir').attr('src', url);
                $('#hdURLImagenSubir').val(url);

                $('#hdDeudaFiado').val(msg.DeudaFiado);
                $('#hdDeudaSuplemento').val(msg.DeudaSuplemento);
                $('#hdDeudaRopa').val(msg.DeudaRopa);
                ListarMembresia(msg.CodigoSocio);


            }
        }

    });

}

function BuscarInformacionSociosPorCodigoFiltro(Filtro) {

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        data: '{"Filtro":"' + Filtro + '"}',
        type: "POST",
        url: "/gestionce/BuscarInformacionSociosPorCodigoFiltro",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            event_mostrarBienvenida();

            $("#gridMenbresias").empty();
            $('#lblNombreMembresiaInfo').html('');

            $("#gridDetalleAsistencia").empty();
            $("#gridReservas").empty();
            $("#gridHistorialPagos").empty();
            $("#gridMensajesMenbresia").empty();
            $("#lblCantidadTotalAsistenciaEfectivo").html('0');
            $("#lblPorAsistidos").html('0');
            $("#lblCalificacion").html('0');

            $("#lblInfoMembresia_Cliente").html('');
            $("#lblEstadoCitaNutricional_Cliente").html('');
            document.getElementById("InforMembresias_Cliente").style.display = 'none';

            $('#lblVendedorInfoClienteRepartido').html('');
            $('#lblCantidadListarDetalleAsistenciaSocio').html('');

            if (msg.CodigoSocio == 0) {

                event_mostrarBienvenida();

                $.bootstrapGrowl("El socio no existe.", { type: 'danger', width: 'auto' });
                $('#hdCodigo').val(0);
                $('#lblNombreCompleto').val('');
                $('#lblClienteSuplemento').html('');

                $('#lblClienteSuplemento').val('');

                $('#infoDNI').html('');
                $('#infoFechaCreacion').val('');
                $('#infoCelular').html('');
                $('#infoFechaNacimiento').val('');

                $('#infoEdad').val('');
                $('#infoDireccion').val('');

                $('#infoDistrito').val('');
                $('#infoCorreo').val('');
                $('#lblEstadoCivil').val('');
                $('#lblHijos').val('');

                $('#lblOcupacion').val('');
                $('#lblTelefonoTrabajo').val('');
                $('#lblLugarTrabajo').val('');
                // $('#lblSexo').html('');
                $('#infoFacebook').val('');

                $('#infoCodigo').html('');
                $('#lblSede').val('');
                $('#ImagenSubir').attr('src', '../Imagenes/fitness/anonimoProducto.jpg');
                $('#hdURLImagenSubir').val('../Imagenes/fitness/anonimoProducto.jpg');

                $('#lblVendedorInfoClienteRepartido').html('');
                $('#imgFotoCarnetVacunacion').attr('src', 'https://contenidosappsfit.blob.core.windows.net/carnetvacunacion/carnetvacunacion.png');
            } else {
                event_mostrarDatosSocio();
                event_noescribiendo();
                $('#hdCodigo').val(msg.CodigoSocio);

                //FlagCumpleanios = msg.flagCumpleanios;
                //msg.ImagenUrlCarnetVacunacion
                $('#imgFotoCarnetVacunacion').attr('src', msg.ImagenUrlCarnetVacunacion);
                if (msg.ImagenUrl == null) {
                    $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='Agregar Fotos' style='Cursor:pointer;'><img  src='../Imagenes/fitness/PerfilHombre.png' class='contrast' style='width:250px;height: 144pxborder-radius: 16px;'></div>");
                    $('#FotoCliente_qrcode').attr('src', '../Imagenes/fitness/PerfilHombre.png');

                    $('#lblCumpleaniosSocios').html("");
                    $('#btnAvisoFoto').show('fast');
                    $('#ModalAvisosImportantes').hide('fast');

                    $('#imgVer_FotoSocio').attr('src', '../Imagenes/fitness/PerfilHombre.png');

                } else if (msg.ImagenUrl == undefined) {
                    $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='Agregar Fotos' style='Cursor:pointer;'><img  src='../Imagenes/fitness/PerfilHombre.png' class='contrast'  style='width:290px;height: 180px;border-radius: 5px;'></div>");
                    $('#FotoCliente_qrcode').attr('src', '../Imagenes/fitness/PerfilHombre.png');
                    $('#lblCumpleaniosSocios').html("");
                    $('#btnAvisoFoto').show('fast');
                    $('#ModalAvisosImportantes').hide('fast');

                    $('#imgVer_FotoSocio').attr('src', '../Imagenes/fitness/PerfilHombre.png');

                } else {
                    $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='Agregar Fotos' style='Cursor:pointer;'><img  src='" + msg.ImagenUrl + "' class='contrast'  style='width:290px;height: 180px;border-radius: 5px;'></div>");
                    $('#FotoCliente_qrcode').attr('src', msg.ImagenUrl);
                    $('#lblCumpleaniosSocios').html("<div style='font-size:60px;font-family: cursive;color: black;display: " + msg.flagCumpleanios + "'><img src='/Content/appfitplataformapersonafit/assets/images/globofiesta.png' style='width:155px;display: " + msg.flagCumpleanios + "' class='img-circle' title='Feliz Cumpleaños'/>&nbsp; Hoy esta de cumpleaños</div>");
                    $('#btnAvisoFoto').hide('fast');
                    if (msg.flagCumpleanios != 'none') {
                        $('#ModalAvisosImportantes').show('fast');
                    }

                    $('#imgVer_FotoSocio').attr('src', msg.ImagenUrl);

                }

                //  validar para el cumpleaños
                $('#hdDeudaFiado').val(msg.DeudaFiado);

                $('#hdDeudaSuplemento').val(msg.DeudaSuplemento);
                $('#hdDeudaRopa').val(msg.DeudaRopa);

                $('#lblNombreCompleto').val((msg.Nombres).toUpperCase() + ' , ' + (msg.Apellidos).toUpperCase());
                $('#lblClienteSuplemento').html((msg.Nombres).toUpperCase() + ' , ' + (msg.Apellidos).toUpperCase());

                $('#infoDNI').html(msg.DNI);
                $('#infoFechaCreacion').val(msg.DescFechaCreacion);
                $('#infoUsuarioCreacion').val(msg.UsuarioCreacion);
                if (msg.Celular != '') {
                    $('#infoCelular').html(msg.Celular);
                    $('#imginfoCelular').attr('href', 'https://api.whatsapp.com/send?phone=' + msg.Celular);
                    $('#imginfoCelular').css('display', 'block');

                } else {
                    $('#infoCelular').html('sin whatsapp');
                    $('#imginfoCelular').css('display', 'none');
                }

                $('#infoFechaNacimiento').val(msg.DescFechaNacimiento);

                $('#infoEdad').val(msg.Edad);
                $('#infoDireccion').val(msg.Direccion);

                $('#infoDistrito').val(msg.Distrito);
                $('#infoCorreo').val(msg.Correo);

                if (msg.EstadoCivil == 1) {
                    $('#lblEstadoCivil').val('Soltero');
                } else if (msg.EstadoCivil == 2) {
                    $('#lblEstadoCivil').val('Casado');
                } else {
                    $('#lblEstadoCivil').val('Viudo');
                }

                if (msg.EstadoHijos == 1) {
                    $('#lblHijos').val('Si');
                } else {
                    $('#lblHijos').val('No');
                }
                $('#lblOcupacion').val(msg.Ocupacion);
                $('#lblTelefonoTrabajo').val(msg.TelefonoTrabajo);
                $('#lblLugarTrabajo').val(msg.DireccionTrabajo);
                var face = msg.UrlFacebook;
                if (face != '' && face != null) {
                    $('#imginfoFacebook').val(msg.UrlFacebook);
                } else {
                    $('#imginfoFacebook').val('Sin facebook.');
                }

                $('#infoCodigo').html(msg.CodigoSocio);
                $('#lblSede').val(msg.desSede);

                var url = msg.ImagenUrl == undefined ? "../Imagenes/fitness/anonimoProducto.jpg" : msg.ImagenUrl;
                $('#ImagenSubir').attr('src', url);
                $('#hdURLImagenSubir').val(url);

                $('#lblVendedorInfoClienteRepartido').html(msg.UserAsesorVenta);

                $('#GridDeudas tr').removeClass('k-state-selected');
                $('#GridDeudas').find("tr").eq(0).removeClass('k-state-selected');
                $('#upGrilla1 tr').removeClass('k-state-selected');
                $('#upGrilla1').find("tr").eq(0).removeClass('k-state-selected');

            }
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            var CodigoSocio = $('#hdCodigo').val();

            if (CodigoSocio == 0) {
                event_mostrarDatosSocio();

                $("#gridMenbresias").empty();
                $('#lblNombreMembresiaInfo').html('');

                $("#gridDetalleAsistencia").empty();
                $("#gridReservas").empty();
                $("#gridHistorialPagos").empty();
                $("#gridMensajesMenbresia").empty();
                $("#lblCantidadTotalAsistenciaEfectivo").html('0');
                $("#lblPorAsistidos").html('0');
                $("#lblCalificacion").html('0');

                $("#lblInfoMembresia_Cliente").html('');
                $("#lblEstadoCitaNutricional_Cliente").html('');
                document.getElementById("InforMembresias_Cliente").style.display = 'none';

                $('#lblVendedorInfoClienteRepartido').html('');
                $('#lblCantidadListarDetalleAsistenciaSocio').html('');


            } else {
                ListarMembresia(CodigoSocio);
            }
        }

    });

}

function MarcarAsistencia() {
    var CodigoMenbresia = $('#hdCodigoMembresiaOrigen').val();
    var CodigoPersona = $('#hdCodigo').val();

    ActualizarNroIngreso(CodigoPersona, CodigoMenbresia);
}

function ValidarBuscarDiasHorarioPaquete() {
    //VALIDAR SI LA MEMBRESIA DEL CLIENTE TIENE ACCESO A LA SEDE INGRESADA
    var flagPaqueteSedePermiso = $('#hdflagPaqueteSedePermiso').val();
    if (flagPaqueteSedePermiso == 1) {

        //EL RESULTADO DE ESTE METODO AHORA LO TRAE EN LISTAR MEMBRESIAS, YA NO NECESITO IR AL SERVIDOR
        var ObtenerDisponibilidadHorarioPaquete = $('#hdflagDisponibilidadHorarioPaquete').val();
        if (ObtenerDisponibilidadHorarioPaquete > 0) {

            var CantDiasEfec = $('#hdCantidadTotalAsistenciaEfectivo').val();
            var NroIngresoActual = $('#hdNroIngresoActual').val();

            if (parseInt(CantDiasEfec) < parseInt(NroIngresoActual)) {
                $.bootstrapGrowl("NRO ASISTENCIAS LLEGO A SU LIMITE, REVISA EL NRO DE SESIONES DE LA MEMBRESIA", { type: 'danger', width: 'auto' });
            } else {
                MarcarAsistencia();
            }

        } else if (ObtenerDisponibilidadHorarioPaquete == 0) {

            document.getElementById("tabla_Info_Inicio_Cliente").style.display = '';
            $('#lblMensajeInicio_Cliente').html('HORARIO NO DISPONIBLE');
            $('#lblEstadoCliente').html('SU PLAN NO TIENE ACCESO EN ESTE HORARIO');
            $.bootstrapGrowl("EL PLAN DEL CLIENTE NO TIENE ACCESO PARA INGRESAR EN ESTE HORARIO", { type: 'danger', width: 'auto' });
            $('#btnMarcarAsistencia_Cliente').hide('fast');
            $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
            $('#InforMembresias_Cliente').css('background-color', 'red');
        }

    } else {
        $.bootstrapGrowl("ESTA MEMBRESIA NO TIENE ACCESO PARA ESTA SEDE.", { type: 'danger', width: 'auto' });
    }

    //document.getElementById('loadMe').style.display = 'block';
    //$.ajax({
    //    data: '{"CodigoPaquete":"' + CodigoPaquete + '"}',
    //    type: "POST",
    //    url: "/gestionce/ValidarBuscarDiasHorarioPaquete",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {

    //        if (msg > 0) {
    //            //ValidarIngresoDiaPaquete(codMem);
    //            var CantDiasEfec = $('#hdCantidadTotalAsistenciaEfectivo').val();
    //            var NroIngresoActual = $('#hdNroIngresoActual').val();
    //            var CodigoSocio = $('#hdCodigo').val();

    //            if (CantDiasEfec == NroIngresoActual) {
    //                verAsistencias(codMem);
    //                verReservas(codMem);
    //                BuscarAsistenciaEfectiva(codMem, CodigoSocio);
    //                $.bootstrapGrowl("Cantidad Asistencia llego a si limite, revisa el numero de sesiones de la membresia.", { type: 'danger', width: 'auto' });
    //            } else {
    //                MarcarAsistencia();
    //            }

    //        } else if (msg == 0) {

    //            document.getElementById("tabla_Info_Inicio_Cliente").style.display = '';
    //            $('#lblMensajeInicio_Cliente').html('HORARIO NO DISPONIBLE');
    //            $('#lblEstadoCliente').html('SU PLAN NO TIENE ACCESO EN ESTE HORARIO⛔');
    //            $('#btnMarcarAsistencia_Cliente').hide('fast');
    //            $('#InforMembresias_Cliente').css('background-color', 'red');
    //        }

    //    }, complete: function () {
    //        document.getElementById('loadMe').style.display = 'none';
    //    }
    //});

}

function ValidarBuscarDiasHorarioPaquete_ReservarYMarcarAsistencia(control) {

    var id = $(control).attr('data-id');
    var id_tiemporeal = $(control).attr('data-idtiemporeal');

    //VALIDAR SI LA MEMBRESIA DEL CLIENTE TIENE ACCESO A LA SEDE INGRESADA
    var flagPaqueteSedePermiso = $('#hdflagPaqueteSedePermiso').val();
    if (flagPaqueteSedePermiso == 1) {

        //EL RESULTADO DE ESTE METODO AHORA LO TRAE EN LISTAR MEMBRESIAS, YA NO NECESITO IR AL SERVIDOR
        var ObtenerDisponibilidadHorarioPaquete = $('#hdflagDisponibilidadHorarioPaquete').val();
        if (ObtenerDisponibilidadHorarioPaquete > 0) {

            var CantDiasEfec = $('#hdCantidadTotalAsistenciaEfectivo').val();
            var NroIngresoActual = $('#hdNroIngresoActual').val();

            if (parseInt(CantDiasEfec) < parseInt(NroIngresoActual)) {
                $.bootstrapGrowl("NRO ASISTENCIAS LLEGO A SU LIMITE, REVISA EL NRO DE SESIONES DE LA MEMBRESIA", { type: 'danger', width: 'auto' });
            } else {

                //AQUI PONER FUNCION
                //MarcarAsistencia();
                var CodigoMenbresia = $('#hdCodigoMembresiaOrigen').val();
                var codigosocio = $('#hdCodigo').val();
                UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia(id, id_tiemporeal, codigosocio, CodigoMenbresia)
            }

        } else if (ObtenerDisponibilidadHorarioPaquete == 0) {

            document.getElementById("tabla_Info_Inicio_Cliente").style.display = '';
            $('#lblMensajeInicio_Cliente').html('HORARIO NO DISPONIBLE');
            $('#lblEstadoCliente').html('SU PLAN NO TIENE ACCESO EN ESTE HORARIO');
            $.bootstrapGrowl("EL PLAN DEL CLIENTE NO TIENE ACCESO PARA INGRESAR EN ESTE HORARIO", { type: 'danger', width: 'auto' });
            $('#btnMarcarAsistencia_Cliente').hide('fast');
            $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
            $('#InforMembresias_Cliente').css('background-color', 'red');
        }

    } else {
        $.bootstrapGrowl("ESTA MEMBRESIA NO TIENE ACCESO PARA ESTA SEDE.", { type: 'danger', width: 'auto' });
    }

}

function listardllAsesoresVentas() {

    var dllAsesoresVentas = $("#dllAsesoresVentas").kendoDropDownList({
        optionLabel: "Vendedores",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/listardllAsesoresVentas",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            $('#dllAsesoresVentas').data('kendoDropDownList').value(User);

                        }
                    });
                }
            }
        }, change: function () {

        }
    }).data("kendoDropDownList");
}

function ListarMembresia(codigo) {

    $("#gridMenbresias").empty();
    $("#gridMenbresias").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"codSocio":"' + codigo + '"}',
                        type: "POST",
                        url: "/gestionce/ListarMembresias",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            if (msg == '') {

                                event_mostrarDatosSocio();

                                $("#gridMenbresias").empty();
                                $('#lblNombreMembresiaInfo').html('');

                                $("#gridDetalleAsistencia").empty();
                                $("#gridReservas").empty();

                                $("#gridHistorialPagos").empty();
                                $("#gridMensajesMenbresia").empty();

                                $("#lblInfoMembresia_Cliente").html('');
                                $("#lblEstadoCitaNutricional_Cliente").html('');
                                document.getElementById("InforMembresias_Cliente").style.display = 'none';
                                document.getElementById("divGridHistorialFreezing").style.display = 'none';

                                $('#lblCantidadListarDetalleAsistenciaSocio').html('');
                                $('#btnMarcarAsistencia_Cliente').hide('fast');
                                $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');

                                $('#ModalAvisoNotieneMembresia').show('fast');

                                $('#btnCerrarModalAvisoNotieneMembresia').click(function () {
                                    $('#ModalAvisoNotieneMembresia').hide('fast');
                                });

                            } else {
                                event_mostrarDatosSocio();
                                document.getElementById("divBotonEnviarSocioANuevo").style.display = 'none';
                            }
                        }
                    });
                }
            }
        },
        selectable: "row",
        scrollable: true,
        height: 160,
        columns: [
            {
                template: '<center><div style="height: 17px;width: 17px;margin-left: -4px;"><label style="background-color:#: EstadoColor #;width: 17px;border-radius:25px;height: 17px;"></label></div></center>',
                title: "",
                width: 3
            }, {
                field: "NombrePaquete",
                title: "<center style='color:#fff;'><b>PROMOCION</b></center>",
                width: 9,
                attributes: {
                    style: "font-size:13px;text-align:center;"
                }
            },
            {
                field: "FechaCreacion",
                title: "<center style='color:#fff;'><b>INSCRIPCION</b></center>",
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd'), 'dd/MM/yyyy hh:mm tt') #",
                width: 15,
                attributes: {
                    style: "font-size:13px;text-align:center;"
                }
            },
            {
                field: "DesFechaInicio",
                title: "<center style='color:#fff;'><b>FECHA INICIO</b></center>",
                width: 9,
                attributes: {
                    style: "font-size:13px;text-align:center;"
                }
            },
            {
                field: "DesFechaFin",
                title: "<center style='color:#fff;'><b>FECHA FIN</b></center>",
                width: 9,
                attributes: {
                    style: "font-size:13px;text-align:center;"
                }

            }, {
                field: "Costo",
                title: "<center style='color:#fff;'><b>PRECIO</b></center>",
                width: 6,
                attributes: {
                    style: "font-size:13px;text-align:center;"
                }

            }, {
                field: "MontoTotal",
                title: "<center style='color:#fff;'><b>A CUENTA</b></center>",
                width: 7,
                attributes: {
                    style: "font-size:13px;text-align:center;"
                }

            }, {
                field: "Debe",
                title: "<center style='color:#fff;'><b>SALDO</b></center>",
                width: 7,
                attributes: {
                    style: "font-size:13px;text-align:center;"
                }

            }, {
                field: "CantidadFreezing",
                title: "<center style='color:#fff;'><b>FREEZING</b></center>",
                width: 8,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "CantidadFreezingTomados",
                title: "<center style='color:#fff;'><b>FREEZING TOM</b></center>",
                width: 9,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "FrezenDisponibles",
                title: "<center style='color:#fff;'><b>FREEZ. ACTUAL</b></center>",
                width: 9,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "NroContrato",
                title: "<center style='color:#fff;'><b># CONTRATO</b></center>",
                width: 11,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "AsesorComercial",
                title: "<center style='color:#fff;'><b>RESPONSABLE</b></center>",
                width: 13,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "CodigoSede",
                title: "<center style='color:#fff;'><b>SEDE</b></center>",
                width: 5,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        }, change: function (e) {

            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                var codMem = dataItem.CodigoMenbresia;

                $('#lblCuotaPendiente_Fecha').html(dataItem.strFechaCuota);
                $('#hdflagPaqueteSedePermiso').val(dataItem.flagPaqueteSedePermiso);
                $('#hdflagDisponibilidadHorarioPaquete').val(dataItem.ObtenerDisponibilidadHorarioPaquete);
                $('#hdEstadoDescripcionMembresia').val(dataItem.desEstado);
                $('#hdCodigoTipoIngresoPago').val(dataItem.TipoIngreso);
                $('#hdAsesorComercialPago').val(dataItem.AsesorComercial);
                $('#hdTipoProducto').val(dataItem.tipoProducto);
                $('#hdCodigoCodigoMenbresiaPago').val(dataItem.CodigoMenbresia);
                $('#hdCodigoDescripcionMenbresiaPago').val(dataItem.Descripcion);

                document.getElementById("InforMembresias_Cliente").style.display = '';
                $('#lblNombreMembresiaInfo').html(dataItem.Descripcion);
                $('#lblCuotaPendienteModalDebe').html("S/ " + dataItem.MontoCuota);

                $('#hdDeudaMembresia').val(dataItem.Debe);
                if (parseFloat(dataItem.Debe) > 0) {

                    if ($('#hdPermiso_PagoMembresias').val() == '1') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;">🚨 DEBE ' + parseFloat(dataItem.Debe).toFixed(2) + ' EN MEMBRESÍA 🚨</td><td><div data-toggle="modal" data-target="#myModalverMasPagarDeuda" onclick="verMasPagarDeuda();" style="background-color:white;color:red;border-radius:15px;height:20px;width: 61px;cursor: pointer;">Pagar</div></td></tr></table>');
                    } else if ($('#hdPermiso_PagoMembresias').val() == '0') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;">🚨 DEBE ' + parseFloat(dataItem.Debe).toFixed(2) + ' EN MEMBRESÍA 🚨</td><td></td></tr></table>');
                    }

                    $('#InforMembresias_Cliente').css('background-color', 'red');
                    $('#lblTotal').html(dataItem.Debe);

                } else {
                    $('#lblTotal').html('0.00');
                    $('#lblEstadoCliente').html('PASE 👍 ' + dataItem.ObtenerTiempoVencimiento);

                    $('#InforMembresias_Cliente').css('background-color', '#fff');
                }
                //alert('NroIngreso: ' + dataItem.NroIngreso);
                //alert('NroIngresoActual: ' + dataItem.NroIngresoActual);
                //DEUDA FIADO
                //if (parseFloat($('#hdDeudaFiado').val()) > 0) {
                //    $('#btnPagarFiado').show('fast');
                //    $('#btnPagarFiado').html('Debe S/.' + parseFloat($('#hdDeudaFiado').val()).toFixed(2) + ' Jugueria');
                //    $('#lblEstadoCliente').html('DEBE ' + parseFloat($('#hdDeudaFiado').val()).toFixed(2));
                //    $('#InforMembresias_Cliente').css('background-color', 'red');
                //} else {
                //    $('#btnPagarFiado').hide('fast');
                //}
                //DEUDA SUPLEMENTO
                if ((parseFloat($('#hdDeudaSuplemento').val()) > 0)) {

                    if ($('#hdPermiso_PagoProductos').val() == '1') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;"> DEBE ' + parseFloat($('#hdDeudaSuplemento').val()).toFixed(2) + ' EN PRODUCTOS</td><td><div data-toggle="modal" data-target="#myModalPagarDeudaProductos" onclick="CentroEntrenamiento_uspListarDeudasCliente();" style="background-color:white;color:red;border-radius:15px;height:20px;width: 61px;cursor: pointer;">Pagar</div></td></tr></table>');
                    } else if ($('#hdPermiso_PagoProductos').val() == '0') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;"> DEBE ' + parseFloat($('#hdDeudaSuplemento').val()).toFixed(2) + ' EN PRODUCTOS</td><td></td></tr></table>');
                    }

                    $('#InforMembresias_Cliente').css('background-color', 'red');
                }

                if (dataItem.flagPaqueteSedePermiso == 0) {
                    $('#lblEstadoCliente').html('🚷 NO TIENES ACCESO A ESTA SEDE 🚷');
                    $('#InforMembresias_Cliente').css('background-color', 'rgb(0 117 255)');
                    $('#lblTotal').html(dataItem.Debe);
                    $('#btnPagarFiado').hide('fast');
                }

                $('#hdCodigoMembresiaOrigen').val(codMem);
                $('#hdCodigoPaqueteOrigen').val(dataItem.CodigoPaquete);

                $('#lblInfoMembresia_Cliente').html('TIPO ' + dataItem.desTipoPaquete + ': ' + dataItem.NroIngresoActual + ' ASISTENCIAS de ' + dataItem.NroIngreso + ' SESIONES.');
                //$("#lblInfoMembresia_Cliente").html('&nbsp;&nbsp;' + dataItem.ObtenerTiempoVencimiento + '&nbsp;&nbsp;');
                $("#lblEstadoCitaNutricional_Cliente").html('&nbsp;&nbsp;' + dataItem.ObtenerEstadoCitaNutrional + '&nbsp;&nbsp;');

                $('#hdCantidadTotalAsistenciaEfectivo').val(dataItem.NroIngreso);
                //$('#hdCantidadTotalAsistenciaEfectivo').val(dataItem.NroIngreso + dataItem.CantidadFreezing);
                $('#hdNroIngresoActual').val(dataItem.NroIngresoActual);

                VerMembresia_Cliente(codMem);

            });

        }
    });

}

function VerMembresia_Cliente(codigoMembresia) {
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"codigoMenbresia":"' + codigoMembresia + '"}',
        type: "POST",
        url: "/gestionce/VerInformacionMenbresias",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            ListarHistorialPagos(msg.ListPagosContrato);//ListarHistorialPagos(codMem);
            ListarMensajesMenbresia_view(msg.ListContratoMensaje);
            uspListarClientesMenbresiasCuotas(msg.ListContratoCuota);
            verReservas_view(msg.ListReservas);

            document.getElementById('loadMe').style.display = 'none';

            $('#hdEstadoMembresia').val(msg.Estado);

            if ($('#InforMembresias_Cliente').css('background-color') != 'rgb(255, 0, 0)') {
                $('#InforMembresias_Cliente').css('background', msg.EstadoColor);
            }

            $('#lblCuotaPendiente').html(msg.MontoCuota);

            if (parseFloat(msg.MontoCuota) > 0) {
                document.getElementById("modaldivCuotaPendiente").style.display = 'block';
            } else {
                document.getElementById("modaldivCuotaPendiente").style.display = 'none';
            }

            if (msg.Estado == 3) {  //traspaso
                $('#btnMarcarAsistencia_Cliente').hide('fast');
                $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
            } else if (msg.Estado == 0) {  //freezing
                $('#btnMarcarAsistencia_Cliente').hide('fast');
                $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
            } else if (msg.Estado == 1 && msg.TipoMembresia == 1) {

            } else if (msg.Estado == 1 && msg.TipoMembresia == 2) {
                $('#btnMarcarAsistencia_Cliente').show('fast');
                $('#btnMarcarAsistenciaClaseHorario_Cliente').show('fast');
            }

            if (msg.NroIngreso > msg.NroIngresoActual && msg.Estado != 2 && msg.Estado != 3 && msg.Estado != 0) {
                $('#btnMarcarAsistencia_Cliente').show('fast');
                $('#btnMarcarAsistenciaClaseHorario_Cliente').show('fast');
            } else {
                $('#btnMarcarAsistencia_Cliente').hide('fast');
                $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
            }

            $('#hdflagFechaInicio').val(msg.FechaInicio);
            $('#hdflagFechaHoy').val(msg.Hoy);

            $('#lblFechaInicioConge_Cliente').html(msg.DescFechaCongelacionProgramada);
            $('#lblFechaFinConge_Cliente').html(msg.DescFechaDesCongelacion);
            if (msg.EstadoInfoCogelado == 0) {
                document.getElementById("divDatosCongelamiento_Cliente").style.display = '';
                $('#lblEstadoCliente').html('🛡 SU MEMBRESIA ESTA CONGELADO 🛡');
                $('#lblInforReceptorTraspaso_Cliente').html(msg.MotivoCongelamiento);
                document.getElementById("lblInforReceptorTraspaso_Cliente").style.display = '';
                return;
            } else {
                document.getElementById("divDatosCongelamiento_Cliente").style.display = 'none';

            }

            if (msg.ObservacionTraspaso != '' && msg.ObservacionTraspaso != null) {
                $('#infoTraspasoDeMembresia_Cliente').html(msg.ObservacionTraspaso);
                document.getElementById("tableInfoTraspaso_Cliente").style.display = '';
            } else {
                $('#infoTraspasoDeMembresia_Cliente').html('');
                document.getElementById("tableInfoTraspaso_Cliente").style.display = 'none';
            }

            if (msg.EstadoInfoCogelado != 0) {

                if (msg.Estado == 3) {
                    $('#lblInforReceptorTraspaso_Cliente').html('traspaso a: ' + msg.SocioTraspasoReceptor + '.');
                    document.getElementById("lblInforReceptorTraspaso_Cliente").style.display = '';
                    $('#lblEstadoCliente').html('SU MEMBRESIA SE TRASPASO A ' + msg.SocioTraspasoReceptor);
                } else {
                    $('#lblInforReceptorTraspaso_Cliente').html('');
                    document.getElementById("lblInforReceptorTraspaso_Cliente").style.display = 'none';
                }
            }

            if (parseFloat($('#hdDeudaMembresia').val()) > 0) {

                if ($('#hdPermiso_PagoMembresias').val() == 1) {
                    $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;">🚨 DEBE ' + parseFloat($('#hdDeudaMembresia').val()).toFixed(2) + ' EN MEMBRESÍA 🚨</td><td><div data-toggle="modal" data-target="#myModalverMasPagarDeuda" onclick="verMasPagarDeuda();" style="background-color:white;color:red;border-radius:15px;height:20px;width: 61px;cursor: pointer;">Pagar</div></td></tr></table>');
                } else if ($('#hdPermiso_PagoMembresias').val() == 0) {
                    $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;">🚨 DEBE ' + parseFloat($('#hdDeudaMembresia').val()).toFixed(2) + ' EN MEMBRESÍA 🚨</td><td></td></tr></table>');
                }

                $('#InforMembresias_Cliente').css('background-color', 'red');
                $('#lblTotal').html($('#hdDeudaMembresia').val());
            }

            //FIADO
            //if (parseFloat($('#hdDeudaFiado').val()) > 0) {
            //    $('#btnPagarFiado').show('fast');
            //    $('#btnPagarFiado').html('Debe S/.' + parseFloat($('#hdDeudaFiado').val()).toFixed(2) + ' Jugueria');
            //    $('#lblEstadoCliente').html('DEBE ' + parseFloat($('#hdDeudaFiado').val()).toFixed(2));
            //    $('#InforMembresias_Cliente').css('background-color', 'red');

            //} else {
            //    $('#btnPagarFiado').hide('fast');
            //}
            //DEUDA SUPLEMENTO Y ROPA

            if ((parseFloat($('#hdDeudaSuplemento').val()) > 0)) {

                if ($('#hdPermiso_PagoProductos').val() == '1') {
                    $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;"> DEBE ' + parseFloat($('#hdDeudaSuplemento').val()).toFixed(2) + ' EN PRODUCTOS</td><td><div data-toggle="modal" data-target="#myModalPagarDeudaProductos" onclick="CentroEntrenamiento_uspListarDeudasCliente();" style="background-color:white;color:red;border-radius:15px;height:20px;width: 61px;cursor: pointer;">Pagar</div></td></tr></table>');
                } else if ($('#hdPermiso_PagoProductos').val() == '0') {
                    $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;"> DEBE ' + parseFloat($('#hdDeudaSuplemento').val()).toFixed(2) + ' EN PRODUCTOS</td><td></td></tr></table>');
                }

                $('#InforMembresias_Cliente').css('background-color', 'red');

            }

            if (msg.FechaInicio > msg.Hoy) {
                document.getElementById("tabla_Info_Inicio_Cliente").style.display = '';
                $('#lblMensajeInicio_Cliente').html('SU MEMBRESÍA AÚN NO INICIA');
                $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;width: 80%;font-weight:bold;">📅 SU MEMBRESIA AUN NO INICIA</td><td style="width: 20%;font-weight:bold;"><div data-toggle="modal" data-target="#myModalActivarMembresia" style="background-color:white;color:#0075ff;border-radius:15px;height:20px;width: 61px;cursor: pointer;">ACTIVAR</div></td></tr></table>');

                $('#btnMarcarAsistencia_Cliente').hide('fast');
                $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
                $('#InforMembresias_Cliente').css('background-color', 'rgb(0 117 255)');
            } else {
                document.getElementById("tabla_Info_Inicio_Cliente").style.display = 'none';
                if (msg.Estado == 2) {  //finalizado
                    $('#btnMarcarAsistencia_Cliente').hide('fast');
                    $('#btnMarcarAsistenciaClaseHorario_Cliente').hide('fast');
                    $('#lblEstadoCliente').html('😨 SU MEMBRESÍA FINALIZÓ 👎');
                }

                if (parseFloat($('#hdDeudaMembresia').val()) > 0) {

                    if ($('#hdPermiso_PagoMembresias').val() == '1') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;">🚨 DEBE ' + parseFloat($('#hdDeudaMembresia').val()).toFixed(2) + ' EN MEMBRESÍA 🚨</td><td><div data-toggle="modal" data-target="#myModalverMasPagarDeuda" onclick="verMasPagarDeuda();" style="background-color:white;color:red;border-radius:15px;height:20px;width: 61px;cursor: pointer;">Pagar</div></td></tr></table>');
                    } else if ($('#hdPermiso_PagoMembresias').val() == '0') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;">🚨 DEBE ' + parseFloat($('#hdDeudaMembresia').val()).toFixed(2) + ' EN MEMBRESÍA 🚨</td><td></td></tr></table>');
                    }

                    $('#InforMembresias_Cliente').css('background-color', 'red');
                    $('#lblTotal').html($('#hdDeudaMembresia').val());

                }

                //FIADO
                //if (parseFloat($('#hdDeudaFiado').val()) > 0) {
                //    $('#btnPagarFiado').show('fast');
                //    $('#btnPagarFiado').html('Debe S/.' + parseFloat($('#hdDeudaFiado').val()).toFixed(2) + ' Jugueria');
                //    $('#lblEstadoCliente').html('DEBE ' + parseFloat($('#hdDeudaFiado').val()).toFixed(2));
                //    $('#InforMembresias_Cliente').css('background-color', 'red');

                //} else {
                //    $('#btnPagarFiado').hide('fast');
                //}
                //DEUDA SUPLEMENTO Y ROPA
                if ((parseFloat($('#hdDeudaSuplemento').val()) > 0)) {

                    if ($('#hdPermiso_PagoProductos').val() == '1') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;"> DEBE ' + parseFloat($('#hdDeudaSuplemento').val()).toFixed(2) + ' EN PRODUCTOS</td><td><div data-toggle="modal" data-target="#myModalPagarDeudaProductos" onclick="CentroEntrenamiento_uspListarDeudasCliente();" style="background-color:white;color:red;border-radius:15px;height:20px;width: 61px;cursor: pointer;">Pagar</div></td></tr></table>');
                    } else if ($('#hdPermiso_PagoProductos').val() == '0') {
                        $('#lblEstadoCliente').html('<table style="width:100%;"><tr><td style="font-size: 25px;color: #fff;font-weight:bold;"> DEBE ' + parseFloat($('#hdDeudaSuplemento').val()).toFixed(2) + ' EN PRODUCTOS</td><td></td></tr></table>');
                    }

                    $('#InforMembresias_Cliente').css('background-color', 'red');

                }
            }

        },
        complete: function () {

            var CodigoSocio = $('#hdCodigo').val();
            //validar si esta en el horario del paquete
            var chkMarcadorAutomatico = $('#chkMarcadorAutomatico').is(':checked');
            if (chkMarcadorAutomatico == true) {

                if ($('#hdEstadoDescripcionMembresia').val() == 'Activo') {
                    var flagAsistencia = $('#flagMarcarAsistencia').val();
                    if (flagAsistencia == 1) {
                        ValidarBuscarDiasHorarioPaquete();
                    }
                } else {

                    var flagAsistencia = $('#flagMarcarAsistencia').val();
                    if (flagAsistencia == 1) {

                        $.bootstrapGrowl("No se puede marcar la Asistencia ..! Membresia Inactiva , Traspasado o congelado .. ¡", { type: 'danger', width: 'auto', align: 'center' });
                        $('#txtBuscadorGeneral').val('');
                        verAsistencias(codigoMembresia);
                        BuscarAsistenciaEfectiva(codigoMembresia, CodigoSocio);
                    }
                }

            } else {

                verAsistencias(codigoMembresia);
                BuscarAsistenciaEfectiva(codigoMembresia, CodigoSocio);
                $.bootstrapGrowl("Marcador automatico esta desactivado.", { type: 'danger', width: 'auto' });
            }


        }
    });
}

function verReservas_view(msg) {

    $("#gridReservas").empty();
    $("#gridReservas").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    options.success(msg);
                    verReservasHistorial(msg);
                }
            }
        },
        sortable: true,
        height: 130,
        columns: [{
            field: "Disciplina",
            title: "<center style='color:#fff;font-size:12px;'><b>CLASE</b></center>",
            width: 35,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "DiaSemana",
            title: "<center style='color:#fff;font-size:12px;'><b>DIA</b></center>",
            width: 20,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "FechaHoraInicio",
            title: "<center style='color:#fff;font-size:12px;'><b>FECHA</b></center>",
            width: 25,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraInicio, 'yyyy-MM-dd '), 'dd/MM/yyyy') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "FechaHoraInicio",
            title: "<center style='color:#fff;font-size:12px;'><b>INICIA</b></center>",
            width: 25,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraInicio, 'yyyy-MM-dd '), 'hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "FechaHoraFin",
            title: "<center style='color:#fff;font-size:12px;'><b>FINALIZA</b></center>",
            width: 25,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraFin, 'yyyy-MM-dd '), 'hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><button onclick='Confirmar_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias_Checking(\"#: CodigoHorarioClasesConfiguracion #\",\"#: CodigoHorarioClasesConfiguracionTiempoReal #\",\"#: CodigoHorarioClasesConfiguracionAsistencias #\",#: CodigoSocio #,#: CodigoMembresia #);' type='button' class='btn btn-light btn-sm' title='Marcar Asistencia.' style='font-size:11px;display:#: flagVistaBotonMarcarAsistencia #' >Ingresar</button><div class='far fa-calendar-check' style='display:#: flagVistaImagenAsistio #' title='FECHA INGRESO: #= kendo.toString(kendo.parseDate(FechaHoraAsistio, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') #' ></div></center>",
            title: "",
            width: 23,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }]
    });

}


function event_btnCerrardivCuotaPendiente() {
    document.getElementById('modaldivCuotaPendiente').style.display = 'none';
}

function BuscarAsistenciaEfectiva(CodigoMenbresia, CodigoSocio) {

    $.ajax({
        data: '{"CodigoMenbresia":"' + CodigoMenbresia + '","CodigoSocio":"' + CodigoSocio + '"}',
        type: "POST",
        url: "/gestionce/BuscarAsistenciaEfectiva",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#lblCantidadTotalAsistenciaEfectivo').html(msg.DiasEfectivo);
            $('#lblPorAsistidos').html(msg.desPorAsistido.toFixed(2));
            $('#lblCalificacion').html(msg.desNomEstado);

            $('#lblCantidadListarDetalleAsistenciaSocio_TODO').html(msg.DiasAsistidos);
            $('#lblCantidadTotalAsistenciaEfectivo_TODO').html(msg.DiasEfectivo);
            $('#lblPorAsistidos_TODO').html(msg.desPorAsistido.toFixed(2));
            $('#lblCalificacion_TODO').html(msg.desNomEstado);

            event_ddlPaginacionAsistenciaTodos(msg.DiasAsistidos, 200);
        }
    });
}

function ListarHistoriaFreezingEliminar(codigoMenbresia) {

    $("#gridHistorialFreezingEliminar").empty();
    $("#gridHistorialFreezingEliminar").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"codigo":"' + codigoMenbresia + '"}',
                        type: "POST",
                        url: "/gestionce/ListarHitorialFreezingPorMenbresia",
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
        columns: [{
            field: "FechaInicio",
            title: "<div style='font-size: 12px;color:white;'>Inicio</div>",
            template: "#= kendo.toString(kendo.parseDate(FechaInicio, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
            width: 8,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "FechaFin",
            title: "<div style='font-size: 12px;color:white;'>Fín</div>",
            template: "#= kendo.toString(kendo.parseDate(FechaFin, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
            width: 8,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "NroDias",
            title: "<div style='font-size: 12px;color:white;'>Días</div>",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        }, {
            field: "Motivo",
            title: "<div style='font-size: 12px;color:white;'>Días</div>",
            width: 25,
            attributes: {
                style: "font-size:12px;"
            }
        }, {
            width: 8,
            template: "<button type='button' class='btn btn-primary' onclick='EliminarFreezing(#: Codigo #)'>Eliminar</button>"
        }
        ]
    });
}

function EliminarFreezing(Codigo) {
    document.getElementById('myModalConfirmEliminarFreezing').style.display = 'block';
    document.getElementById('myModalFreezing').style.display = 'none';
    $("#hdCodigoFreezing").val(Codigo);
}

function ListarHistorialPagos(codigoMenbresia) {

    $("#gridHistorialPagos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    options.success(codigoMenbresia);
                    //$.ajax({
                    //    data: '{"codMembresia":"' + codigoMenbresia + '"}',
                    //    type: "POST",
                    //    url: "/gestionce/ListarHistorialPagos",
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    success: function (msg) {
                    //        options.success(msg);
                    //    }
                    //});
                }
            }

        },
        sortable: true,
        height: 150,
        columns: [
            {
                field: "DesEstado",
                title: "<b style='font-size:11px;color:#fff;font-weight:bold;'>ESTADO<b>",
                template: "<b style='font-size:11px;color:#: ColorEstado #;text-transform:uppercase;'>#: DesEstado #</b>",
                width: 4,
                attributes: {
                    style: "font-size:12px;text-transform:lowercase;"
                }
            }, {
                field: "desFechaPago",
                title: "<center style='color:#fff;font-size:12px;'><b>FECHA</b></center>",
                width: 5,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "Monto",
                title: "<center style='color:#fff;font-size:12px;'><b>MONTO</b></center>",
                width: 3,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "NroComprobante",
                title: "<center style='color:#fff;font-size:12px;'><b>COMPROBANTE</b></center>",
                width: 6,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "DesFormaPago",
                title: "<center style='color:#fff;font-size:12px;'><b>F.PAGO</b></center>",
                width: 5,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:#fff;font-size:12px;'><b>CREADOR</b></center>",
                width: 6,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }

        ]
    });

}

function ListarMensajesMenbresia(codigoMenbresia) {

    $("#gridMensajesMenbresia").empty();
    $("#gridMensajesMenbresia").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"codigoMenbresia":"' + codigoMenbresia + '"}',
                        type: "POST",
                        url: "/gestionce/ListarMensajesMenbresia",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }

        },
        height: 145,
        columns: [{
            field: "FechaCreacion",
            title: "<center style='color:#fff;font-size:12px;'><b>FECHA</b></center>",
            template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
            width: 45,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<center style='color:#fff;font-size:12px;'><b>RESPONSABLE</b></center>",
            width: 40,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "Ocurrencia",
            title: "<center style='color:#fff;font-size:12px;'><b>OCURRENCIA</b></center>",
            width: 50,
            attributes: {
                style: "font-size:12px;text-align:center;text-transform: lowercase;"
            }
        }
        ]
    });

}

function ListarMensajesMenbresia_view(data) {

    $("#gridMensajesMenbresia").empty();
    $("#gridMensajesMenbresia").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    options.success(data);
                }
            }

        },
        height: 145,
        columns: [{
            field: "FechaCreacion",
            title: "<center style='color:#fff;font-size:12px;'><b>FECHA</b></center>",
            template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
            width: 45,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "UsuarioCreacion",
            title: "<center style='color:#fff;font-size:12px;'><b>RESPONSABLE</b></center>",
            width: 40,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Ocurrencia",
            title: "<center style='color:#fff;font-size:12px;'><b>OCURRENCIA</b></center>",
            width: 50,
            attributes: {
                style: "font-size:11px;text-align:center;text-transform: lowercase;"
            }
        }
        ]
    });

}

function BuscarListarAsistencia() {
    $('#imgCargandoAsistenciaDiaria').show('fast');
    ListarAsistenciaTodosFiltro();
}

function BuscarListarAsistenciaInvitados() {
    ListarAsistenciaTodosFiltroInvitados();
}

function NuevoCongelarMembresia() {
    event_escribiendo();
    obtenerConfiguracionFreezingMembresia();
    var codigoMembresia = $('#hdCodigoMembresiaOrigen').val();
    listaVendedoresFreezeng();

    $.ajax({
        data: '{"codigoMenbresia":"' + codigoMembresia + '"}',
        type: "POST",
        url: "/gestionce/BuscarMembresia",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#txtCodigoFreezing').val(msg.CodigoMenbresia);
            $('#txtFechaInicioProcFreezing').data('kendoDatePicker').value(msg.FechaInicio);
            $('#txtFechaFinProcFreezing').data('kendoDatePicker').value(msg.FechaFin);
            $('#txtFrezenDisponiblesProcFreezing').val(msg.FrezenDisponibles);
            $('#hdFrezenDisponiblesProcFreezing').val(msg.FrezenDisponibles);
            $('#ddlVendedoresFreezing').data('kendoDropDownList').value(msg.UsuarioCreacion);
            $("#ddlVendedoresFreezing").data("kendoDropDownList").enable(false);
            $('#ddlVendedoresFreezing').data("kendoDropDownList").dataSource.read();
            $('#txtNroDiasCongelarProcFreezing').val('0');
            $('#txtMotivoFreezing').val('');
            if (msg.FechaCongelacionProgramada == null || msg.FechaCongelacionProgramada == undefined) {
                var todayDate1 = new Date();
                $('#txtFechaFreezingProcFreezing').data("kendoDatePicker").value(todayDate1);
                $('#txtFechaFreezingFinProcFreezing').data("kendoDatePicker").value(todayDate1);

            } else {
                var todayDate = new Date();
                $('#txtFechaFreezingProcFreezing').data("kendoDatePicker").value(todayDate);
                $('#txtFechaFreezingFinProcFreezing').data("kendoDatePicker").value(msg.FechaDesCongelacion);
            }

            $('#hdFechaFinOcultoProcFreezing').val(kendo.toString($("#txtFechaFinProcFreezing").data('kendoDatePicker').value(), 'dd/MM/yyyy'));

        }, complete: function () {
            CalcularVecesHacerfreezingSobran();
            uspListarAsignarDiasHorarioPaquete();
        }
    });

}

function listaVendedoresFreezeng() {

    $("#ddlVendedoresFreezing").kendoDropDownList({
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarUsuarioVendedor",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        }
    });

}

function ActualizarConfiguracionFreezing() {

    var flag = $('#chkEstadoProcFreezingConfiguracion').prop('checked') == 1 ? 1 : 0;
    var nro = $('#txtNroDiasDescuentoAutomatico').val() == "" ? 0 : $('#txtNroDiasDescuentoAutomatico').val();

    if (flag == 1 && nro == 0) {
        $('#chkEstadoProcFreezingConfiguracion').removeAttr("checked");
        $('#txtNroDiasDescuentoAutomatico').css("border-color", "#E53935");
        $.bootstrapGrowl("el numero no puede ser igual a cero.", { type: 'danger', width: 'auto' });
        return;
    } else {
        $('#txtNroDiasDescuentoAutomatico').css("border-color", "#009900");
        $.ajax({
            data: '{"flag":"' + flag + '","nro":"' + nro + '"}',
            type: "post",
            url: "/gestionce/GuardarConfiguracionFreezing",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                obtenerConfiguracionFreezingMembresia();
            }, complete: function () {
                CalcularVecesHacerfreezingSobran();
                $.bootstrapGrowl("se guardo correctamente la configuración del freezing.", { type: 'success', width: 'auto' });
            }
        });
    }

}

function obtenerConfiguracionFreezingMembresia() {

    $.ajax({
        type: "POST",
        url: "/gestionce/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.DescontarFreezingDisponiblesFlag == 1) {
                $('#chkEstadoProcFreezingConfiguracion').prop("checked", true);

            } else if (msg.DescontarFreezingDisponiblesFlag == 0) {
                $('#chkEstadoProcFreezingConfiguracion').removeAttr("checked");
            }
            $('#txtNroDiasDescuentoAutomatico').val(msg.DescontarFreezingDisponiblesNumero);
        }
    });

}

function CalcularVecesHacerfreezingSobran() {
    var flag = $('#chkEstadoProcFreezingConfiguracion').prop('checked') == 1 ? 1 : 0;
    var nro = $('#txtNroDiasDescuentoAutomatico').val() == "" ? 0 : parseInt($('#txtNroDiasDescuentoAutomatico').val());
    if (flag == 1) {

        $('#txtFrezenDisponiblesProcFreezing').css("border-color", "#c5c5c5");
        var nroFreezingDisponible = $('#txtFrezenDisponiblesProcFreezing').val() == "" ? 0 : parseInt($('#txtFrezenDisponiblesProcFreezing').val());
        var DescontarFreezingNumero = parseInt(nro);

        var VecesQuedanFreezing = 0;
        if (nroFreezingDisponible < DescontarFreezingNumero) {
            VecesQuedanFreezing = 0;
        } else {
            VecesQuedanFreezing = nroFreezingDisponible / DescontarFreezingNumero;
            VecesQuedanFreezing = VecesQuedanFreezing.toFixed(1);

            if (VecesQuedanFreezing.split('.').length > 0) {
                VecesQuedanFreezing = VecesQuedanFreezing.split('.')[0];
                VecesQuedanFreezing = parseInt(VecesQuedanFreezing);
            }

        }

        if (VecesQuedanFreezing == 0) {
            $('#lblVecesQuedanFreezing').html("No te queda oportunidad para hacer freezing.");
            $('#TableEjecutarFreezing,#btnGuardarFreezing').hide("fast");
        } else if (VecesQuedanFreezing == 1) {
            $('#lblVecesQuedanFreezing').html("Te queda " + VecesQuedanFreezing + " oportunidad para hacer freezing.");
            $('#TableEjecutarFreezing,#btnGuardarFreezing').show("fast");
        } else {
            $('#lblVecesQuedanFreezing').html("Te quedan " + VecesQuedanFreezing + " oportunidades para hacer freezing.");
            $('#TableEjecutarFreezing,#btnGuardarFreezing').show("fast");
        }

        var NroDiasCongelar = $('#txtNroDiasCongelarProcFreezing').val() == "" ? 0 : parseInt($('#txtNroDiasCongelarProcFreezing').val());
        var restante = 0;
        var NroDiasCongelarAutomatico = $('#txtNroDiasDescuentoAutomatico').val();

        if (parseInt(NroDiasCongelarAutomatico) >= parseInt(NroDiasCongelar)) {
            restante = parseInt(nroDisponibles) - parseInt(NroDiasCongelarAutomatico);
        } else if ((parseInt(NroDiasCongelarAutomatico) * 2) >= parseInt(NroDiasCongelar)) {
            restante = parseInt(nroDisponibles) - parseInt(NroDiasCongelarAutomatico * 2);
        } else if ((parseInt(NroDiasCongelarAutomatico) * 3) >= parseInt(NroDiasCongelar)) {
            restante = parseInt(nroDisponibles) - parseInt(NroDiasCongelarAutomatico * 3);
        } else if ((parseInt(NroDiasCongelarAutomatico) * 4) >= parseInt(NroDiasCongelar)) {
            restante = parseInt(nroDisponibles) - parseInt(NroDiasCongelarAutomatico * 4);
        } else if ((parseInt(NroDiasCongelarAutomatico) * 5) >= parseInt(NroDiasCongelar)) {
            restante = parseInt(nroDisponibles) - parseInt(NroDiasCongelarAutomatico * 5);
        }

        if (restante == 0) {
            $('#txtFrezenDisponiblesRestantesProcFreezing').html("ya no le quedaria mas dias freezing disponibles.");
            $('#hdFrezenDisponiblesProcFreezing').val(0);
        }
        else if (restante == 1) {
            $('#txtFrezenDisponiblesRestantesProcFreezing').html("solo le quedaria un dia freezing disponible.");
            $('#hdFrezenDisponiblesProcFreezing').val(1);
        } else if (restante > 1) {
            $('#txtFrezenDisponiblesRestantesProcFreezing').html("Le quedaria " + restante + " dias freezing disponibles.");
            $('#hdFrezenDisponiblesProcFreezing').val(restante);
        }

    } else if (flag == 0) {

        var nroFreezingDisponible = $('#txtFrezenDisponiblesProcFreezing').val() == "" ? 0 : parseInt($('#txtFrezenDisponiblesProcFreezing').val());
        if (nroFreezingDisponible == 0) {
            $('#lblVecesQuedanFreezing').html("No te queda oportunidad para hacer freezing.");
            $('#TableEjecutarFreezing,#btnGuardarFreezing').hide("fast");
        } else if (nroFreezingDisponible == 1) {
            $('#lblVecesQuedanFreezing').html("Te queda " + nroFreezingDisponible + " oportunidad para hacer freezing.");
            $('#TableEjecutarFreezing,#btnGuardarFreezing').show("fast");
        } else {
            $('#lblVecesQuedanFreezing').html("Te quedan " + nroFreezingDisponible + " oportunidades para hacer freezing.");
            $('#TableEjecutarFreezing,#btnGuardarFreezing').show("fast");
        }

        $('#txtFrezenDisponiblesProcFreezing').css("border-color", "#82B1FF");

        var nroDisponibles = $('#txtFrezenDisponiblesProcFreezing').val() == '' ? 0 : $('#txtFrezenDisponiblesProcFreezing').val();
        var NroDiasCongelar = $('#txtNroDiasCongelarProcFreezing').val() == '' ? 0 : parseInt($('#txtNroDiasCongelarProcFreezing').val());
        var restante = parseInt(nroDisponibles) - parseInt(NroDiasCongelar);

        if (restante == 0) {
            $('#txtFrezenDisponiblesRestantesProcFreezing').html("ya no le quedaria mas dias freezing disponibles.");
            $('#hdFrezenDisponiblesProcFreezing').val(0);
        }
        else if (restante == 1) {
            $('#txtFrezenDisponiblesRestantesProcFreezing').html("solo le quedaria un dia freezing disponible.");
            $('#hdFrezenDisponiblesProcFreezing').val(1);
        } else if (restante > 1) {
            $('#txtFrezenDisponiblesRestantesProcFreezing').html("Le quedaria " + restante + " dias freezing disponibles.");
            $('#hdFrezenDisponiblesProcFreezing').val(restante);
        }

    }

}

function GuardarMembresiaCongelamiento() {
    var todayDate = new Date();
    if (kendo.toString($("#txtFechaFreezingProcFreezing").data('kendoDatePicker').value(), 'MM/dd/yyyy') == null) {
        $('#txtFechaFreezingProcFreezing').data("kendoDatePicker").value(todayDate);
    }

    var codigoMembresia = $('#txtCodigoFreezing').val();
    var fechaInicio = kendo.toString($("#txtFechaInicioProcFreezing").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var fechaFin = kendo.toString($("#txtFechaFinProcFreezing").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FrezenDisponibles = $('#hdFrezenDisponiblesProcFreezing').val();
    var NroDiasCongelar = $('#txtNroDiasCongelarProcFreezing').val() == '' ? 0 : $('#txtNroDiasCongelarProcFreezing').val();
    var fechaFreziing = kendo.toString($("#txtFechaFreezingProcFreezing").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var fechaDesFreziing = kendo.toString($("#txtFechaFreezingFinProcFreezing").data('kendoDatePicker').value(), 'MM/dd/yyyy');

    /*--- Para Historial de Freezing ---*/
    var CodigoSocio = $('#hdCodigo').val();
    var NroDias = $('#txtNroDiasCongelarProcFreezing').val() == '' ? 0 : $('#txtNroDiasCongelarProcFreezing').val();
    var NroSolicitud = $('#txtNroSolicitudFreezing').val();
    var Motivo = $('#txtMotivoFreezing').val();
    /*----------------------------------*/

    $('#btnGuardarFreezing').addClass('disabled');
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"codigo":"' + codigoMembresia + '","fechaInicio":"' + fechaInicio + '","fechaFin":"' + fechaFin +
            '","FrezenDisponibles":"' + FrezenDisponibles + '","NroDiasCongelar":"' + NroDiasCongelar +
            '","fechaFreziing":"' + fechaFreziing + '","fechaDesFreziing":"' + fechaDesFreziing +
            '","CodigoSocio":"' + CodigoSocio + '","NroDias":"' + NroDias + '","NroSolicitud":"' + NroSolicitud +
            '","Motivo":"' + Motivo + '"}',
        type: "POST",
        url: "/gestionce/GuardarMembresiaCongelamiento",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg > 0) {
                $('#myModalFreezing').hide('fast');
                $('#btnGuardarFreezing').removeClass('disabled');
                $.bootstrapGrowl("La membresia ha sido congelada correctamente.", { type: 'success', width: 'auto' });
                event_noescribiendo();
            } else {
                $.bootstrapGrowl("No se ha podido guardar correctamente.", { type: 'danger', width: 'auto' });
            }

        }, complete: function (msg) {
            document.getElementById('loadMe').style.display = 'none';
            var Filtro = $('#hdCodigo').val();
            BuscarInformacionSociosPorCodigoFiltro(Filtro);
            event_noescribiendo();
        }
    });

}

function NuevoMensaje() {
    event_escribiendo();
    $('#myModalMensajeMembresia').show('fast');
    $('#txtMensaje').val('');
}

function guardarMensaje() {

    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");

    var CodigoSocio = $('#hdCodigo').val();
    var codigoMembresia = $('#hdCodigoMembresiaOrigen').val();
    var Mensaje = $('#txtMensaje').val();

    if (Mensaje == '') {
        $.bootstrapGrowl("Falta ingresar un mensaje.", { type: 'danger', width: 'auto' });
        return;
    }

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"CodigoSocio":"' + CodigoSocio + '","codigoMembresia":"' + codigoMembresia + '","Mensaje":"' + Mensaje + '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '"}',
        type: "POST",
        url: "/gestionce/GuardarMensaje",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $.bootstrapGrowl("El mensaje se guardo correctamente.", { type: 'success', width: 'auto' });
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModalMensajeMembresia').style.display = 'none';
            ListarMensajesMenbresia(codigoMembresia);
            event_noescribiendo();
        }
    });



}

function SelectChangeDefaultImagen() {
    document.getElementById("imgCargandoAsistenciaDiaria").style.display = "none";
}

function SelectChangeDefaultImagenNew() {
    document.getElementById("imgCargandoAsistenciaDiaria").style.display = "none";
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

function ListarContratoMembresiaImprimir(codigoMembresia) {

    $.ajax({
        data: '{"codigoMenbresia":"' + codigoMembresia + '"}',
        type: "POST",
        url: "/gestionce/ListarContratoMembresia",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            debugger;
            $('#txtcodigo_Membresia').val(msg.codigo_Membresia);
            $('#txtfechaInicio_Membresia').val(msg.fechaInicio_Membresia); //Listo
            $('#txtfechaFin_Membresia').val(msg.fechaFin_Membresia); //Listo
            $('#txtcosto_Membresia').val(msg.costo_Membresia); //Listo
            $('#txtnroContrato_Membresia').val(msg.nroContrato_Membresia); //Listo

            $('#txtcodigo_Socio').val(msg.codigo_Socio);
            $('#txtnombre_Socio').val(msg.nombre_Socio);  //Listo
            $('#txtapellido_Socio').val(msg.apellido_Socio); //Listo
            $('#txtdni_Socio').val(msgni_Socio); //Listo
            $('#txttelefono_Socio').val(msg.telefono_Socio); //Listo
            $('#txtcelular_Socio').val(msg.celular_Socio); //Listo
            $('#txtcorreo_Socio').val(msg.correo_Socio); //Listo
            $('#txtfechaNacimiento_Socio').val(msg.fechaNacimiento_Socio); //Listo
            $('#txtgenero_Socio').val(msg.genero_Socio); //Listo
            $('#txtfacebook_Socio').val(msg.facebook_Socio);

            $('#txtdireccion_Socio').val(msgireccion_Socio);
            $('#txtdistrito_Socio').val(msgistrito_Socio);
            $('#txtocupacion_Socio').val(msg.ocupacion_Socio); //Listo
            $('#txttipo_Socio').val(msg.tipo_Socio);
            $('#txtcodigo_Paquete').val(msg.codigo_Paquete);
            $('#txtnombre_Paquete').val(msg.nombre_Paquete); //Listo
            $('#txtvalorDias_Paquete').val(msg.valorDias_Paquete);  //Listo
            $('#txtdiasFreezing_Paquete').val(msgiasFreezing_Paquete);
            var numeroContrato = $('#txtnroContrato_Membresia').val();
            $('#documentoContrato').html(numeroContrato);

            var documentoNombreSocio = $('#txtnombre_Socio').val();
            $('#documentoNombreSocio').html(documentoNombreSocio);
            var documentoApellidoSocio = $('#txtapellido_Socio').val();
            $('#documentoApellidoSocio').html(documentoApellidoSocio);
            var documentoTelefono = $('#txttelefono_Socio').val();
            $('#documentoTelefono').html(documentoTelefono);
            var documentoCelular = $('#txtcelular_Socio').val();
            $('#documentoCelular').html(documentoCelular);
            var documentoEmail = $('#txtcorreo_Socio').val();
            $('#documentoEmail').html(documentoEmail);
            var documentoDNI = $('#txtdni_Socio').val();
            $('#documentoDNI').html(documentoDNI);
            var lblcontratoCodigoSocio = $('#infoCodigo').html();
            $('#lblcontratoCodigoSocio').html(lblcontratoCodigoSocio);
            var documentoFechaNacimiento = $('#txtfechaNacimiento_Socio').val();
            $('#documentoFechaNacimiento').html(documentoFechaNacimiento);
            var documentoSexo = $('#txtgenero_Socio').val();
            $('#documentoSexo').html(documentoSexo);
            var documentoReferido = $("#txtreferidoPor_Socio option:selected").text()
            $('#documentoReferido').html(documentoReferido);
            var documentoOcupacion = $('#txtocupacion_Socio').val();
            $('#documentoOcupacion').html(documentoOcupacion);
            var documentoNumeroDias = $('#txtvalorDias_Paquete').val();
            $('#documentoNumeroDias').html(documentoNumeroDias);

            $('#documentoVendedor').html(msg.AsesorComercial_Membresia);
            $('#documentoFechaInscripcion').html(msg.fechaInscripcion_Membresia);
            $('#documentoFechaInicio').html(msg.fechaInicio_Membresia);
            $('#documentoFechaFin').html(msg.fechaFin_Membresia);

            var documentoNombreMembresia = $('#txtnombre_Paquete').val();
            $('#documentoNombreMembresia').html(documentoNombreMembresia);

            var documentoCostoMembresia = $('#txtcosto_Membresia').val();
            $('#documentoCostoMembresia').html(documentoCostoMembresia);

            $('#documentoClausulas').val(msg.Clausula);
            if (msg.observacionTraspaso == "") {

                $('#documentoFechaInicio').html(msg.fechaInicio_Membresia);
                $('#documentoFechaFin').html(msg.fechaFin_Membresia);

            } else {
                $('#documentoFechaInicio').html('');
                $('#documentoFechaFin').html('');
            }

            ImprimeDiv();
        }
    });
}

function ImprimeDiv() {
    var ancho = screen.width - 10;
    var alto = screen.height - 75;
    var divToPrint = document.getElementById('DivIdToPrint');
    var newWin = window.open('', 'Print-Window', 'directories=no, border=0,scrollbars=yes,status=yes,toolbar=no,titlebar=no, resizable=no, menubar=no,width=' + ancho + ',height=' + alto + ',top=0,left=0');
    newWin.document.open();
    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML);
    newWin.document.write(document.getElementById('documentoClausulas').value.replace(/\n/gi, '<br>'));
    newWin.document.write('</body></html>');
    newWin.document.close();
    setTimeout(function () { newWin.close(); }, 100000000);
}

function ObtenerInformacionFin_Cliente(codigoMembresia) {

    $.ajax({
        data: '{"codigoMenbresia":"' + codigoMembresia + '"}',
        type: "POST",
        url: "/gestionce/ObtenerInformacionFin",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $("#lblInfoMembresia_Cliente").html('&nbsp;&nbsp;' + msg + '&nbsp;&nbsp;');
        }
    });
}

function ActualizarNroIngreso(CodigoPersona, CodigoMenbresia) {

    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        data: '{"codigoMembresia":"' + CodigoMenbresia + '","codigoSocio":"' + CodigoPersona + '"}',
        type: "POST",
        url: "/gestionce/ActualizarNroIngresoMembresia",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var Codigo = msg.split('|')[0];
            var Mensaje = msg.split('|')[1];
            if (Codigo == 0) {
                //$.bootstrapGrowl("NRO ASISTENCIAS LLEGO A SU LIMITE, REVISA EL NRO DE SESIONES DE LA MEMBRESIA", { type: 'danger', width: 'auto' });
                $.bootstrapGrowl(Mensaje, { type: 'danger', width: 'auto' });
            } else {

                $.bootstrapGrowl("SE MARCO LA ASISTENCIA CORRECTAMENTE", { type: 'success', width: 'auto', align: 'center' });
                verAsistencias(CodigoMenbresia);
                var chkMarcadorAutomatico = $('#chkMarcadorAutomatico').is(':checked');
                if (chkMarcadorAutomatico == true) {
                    $('#txtBuscadorGeneral').val('');
                    $('#txtBuscadorGeneral').focus();
                }

            }

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function GuardarAsistencia(CodigoPersona, TipoPersona, CodigoPaquete, CodigoMenbresia) {

    $.ajax({
        data: '{"CodigoPersona":"' + CodigoPersona + '","TipoPersona":"' + TipoPersona + '","CodigoPaquete":"' + CodigoPaquete + '","CodigoMenbresia":"' + CodigoMenbresia + '"}',
        type: "POST",
        url: "/gestionce/GuardarAsistencia",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            verAsistencias(CodigoMenbresia);
            BuscarAsistenciaEfectiva(CodigoMenbresia, CodigoPersona);
            var chkMarcadorAutomatico = $('#chkMarcadorAutomatico').is(':checked');
            if (chkMarcadorAutomatico == true) {
                $('#txtBuscadorGeneral').val('');
                $('#txtBuscadorGeneral').focus();
            }

        }
    });

}

function ExportarAsistenciasCliente() {
    tableToExcel('gridDetalleAsistencia', 'Asistencias del cliente');
}

function EliminarAsistencia(Control, CodigoAsistencia, CodigoMenbresia) {
    var CodigoHorarioClasesConfiguracionAsistencias = $(Control).attr('data-CodigoHorarioClasesConfiguracionAsistencias');
    $('#hdCodigoAsistencia_CodigoHorarioClasesConfiguracionAsistencias').val(CodigoHorarioClasesConfiguracionAsistencias);
    $('#hdCodigoAsistencia').val(CodigoAsistencia);
    $('#hdCodMenbresiaAsistencia').val(CodigoMenbresia);
    //alert(CodigoHorarioClasesConfiguracionAsistencias);
    //return;
    $('#myModalConfirmMyModalAsistencia').show('fast');
}

function EliminarAsistenciaInvitado(CodigoAsistenciaI, CodigoInvitado) {
    $('#hdCodigoAsistenciaI').val(CodigoAsistenciaI);
    $('#hdCodigoInvitadoEliminarAsistencia').val(CodigoInvitado);
    $('#myModalConfirmAsistenciaInvitado').show('fast');
}

function listaSocios() {

    $("#lblNombreCompleto").kendoAutoComplete({
        dataTextField: "NombreCompleto",
        template: '<table border="0" style="width:100%;font-size: 9px;">' +
            '<tr>' +
            '<td style="width:20%;">' +
            '<img src=\"#:data.ImagenUrl#\" data_ruta=\"#:data.ImagenUrl#\" style="position: relative;width:40px;height:40px;cursor:pointer" class="img-circle img-focus" />' +
            '</td>' +
            '<td style="width:80%;>' +
            '<span class="k-state-default" >' +
            '<div style="font-weight: bold;font-size: 13px;"> ' +
            'Cod:#:data.CodigoSocio# - #:data.Nombres# #:data.Apellidos# Sede: #:data.DescSede#' +
            '</div>' +
            '</span>' +
            '<img src="../Imagenes/fitness/velita.png" style="width:22px;display:#:data.flagCumpleanios#;" class="img-rounded" title="Feliz Cumpleaños"  />' +
            '</td>' +
            '</tr>' +
            '</table>',
        filter: "startswith",
        minLength: 3,
        height: 400,
        cache: false,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    event_escribiendo();
                    var valor = $('#lblNombreCompleto').data('kendoAutoComplete').value();
                    var flag = 1;
                    $.ajax({
                        type: "POST",
                        data: '{"valor":"' + valor + '","flag":"' + flag + '"}',
                        url: "/gestionce/ListaSocios",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                            $("#lblNombreCompleto").css('font-size', '19px');
                            $("#lblNombreCompleto").css('font-weight', 'bold');
                            event_noescribiendo();
                        }
                    });

                }
            }
        },
        select: function (e) {
            event_noescribiendo();
            var dataItem = this.dataItem(e.item.index());
            $('#inner-container').hide('fast');
            $('#primary-nav').css('width', '0px');
            $('#page-sidebar').css('width', '0px');
            $('#page-content').css('margin', '0 0 0 0px');
            $('#txtBuscadorGeneral').val(dataItem.CodigoSocio);
            $('#hdCodigo').val(dataItem.CodigoSocio);
            var Filtro = dataItem.CodigoSocio;
            $('#flagMarcarAsistencia').val(1);
            BuscarInformacionSociosPorCodigoFiltro(Filtro);
            return false;
        }
    });
}

function listaSocios2() {

    event_escribiendo();
    $("#lblNombreCompleto2").kendoAutoComplete({
        dataTextField: "NombreCompleto",
        template: '<table border="0" style="width:100%;font-size: 9px;">' +
            '<tr>' +
            '<td style="width:20%;">' +
            '<img src=\"#:data.ImagenUrl#\" data_ruta=\"#:data.ImagenUrl#\" style="position: relative;width:40px;height:40px;cursor:pointer" class="img-circle img-focus" />' +
            '</td>' +
            '<td style="width:80%;>' +
            '<span class="k-state-default" >' +
            '<div style="font-weight: bold;font-size: 13px;"> ' +
            'Cod:#:data.CodigoSocio# - #:data.Nombres# #:data.Apellidos# Sede: #:data.DescSede#' +
            '</div>' +
            '</span>' +
            '<img src="../Imagenes/fitness/velita.png" style="width:22px;display:#:data.flagCumpleanios#;" class="img-rounded" title="Feliz Cumpleaños"  />' +
            '</td>' +
            '</tr>' +
            '</table>',
        filter: "startswith",
        minLength: 2,
        height: 400,
        cache: false,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    event_escribiendo();

                    var valor = $('#lblNombreCompleto2').data('kendoAutoComplete').value();
                    var flag = 1;
                    $.ajax({
                        type: "POST",
                        data: '{"valor":"' + valor + '","flag":"' + flag + '"}',
                        url: "/gestionce/ListaSocios",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                            $("#lblNombreCompleto2").css('font-size', '19px');
                            $("#lblNombreCompleto2").css('font-weight', 'bold');

                        }
                    });

                }
            }
        },
        select: function (e) {

            event_noescribiendo();

            var dataItem = this.dataItem(e.item.index());
            $('#inner-container').hide('fast');
            $('#primary-nav').css('width', '0px');
            $('#page-sidebar').css('width', '0px');
            $('#page-content').css('margin', '0 0 0 0px');
            $("#lblNombreCompleto2").val('');
            $('#txtBuscadorGeneral').val(dataItem.CodigoSocio);
            $('#hdCodigo').val(dataItem.CodigoSocio);
            var Filtro = dataItem.CodigoSocio;
            $('#flagMarcarAsistencia').val(1);
            BuscarInformacionSociosPorCodigoFiltro(Filtro);

            event_mostrarDatosSocio();
            $('#InforMembresias_Cliente').css('color', '#fff');

            e.preventDefault();
            return false;
        }
    });
}

function seleccionarTextoSocio() {
    $("#lblNombreCompleto").select();
}

function seleccionarTextoInvitado() {
    $("#txtNombreCompletoInvitado").select();
}

function verAsistencias(codMembresia) {

    $('#hdCodigoMembresiaAsistencia').val(codMembresia);

    $("#gridDetalleAsistencia").empty();
    $("#gridDetalleAsistencia").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoMembresia":"' + codMembresia + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarDetalleAsistenciaSocio_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                        }, complete: function () {
                            uspListarDetalleAsistenciaSocio_NumeroRegistros();
                        }
                    });
                }
            }
        },
        sortable: true,
        height: 140,
        columns: [
            {
                field: "FechaCreacion",
                title: "<center style='color:#fff;font-size:12px;'><b>ASISTENCIA<b></center>",
                width: 45,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy') # <div class='far fa-calendar-check' style='display:#: flagVistaImagenAsistioReserva #' ></div>",
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "FechaCreacion",
                title: "<center style='color:#fff;font-size:12px;'><b>HORA</b></center>",
                width: 50,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'hh:mm:ss tt') #",
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "DiaSemana",
                title: "<center style='color:#fff;font-size:12px;'><b>DIA</b></center>",
                width: 40,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:#fff;font-size:12px;'><b>RESPONSABLE</b></center>",
                width: 65,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }]
    });

}

function verAsistencias_VerMasInformacion() {

    var codMembresia = $('#hdCodigoMembresiaAsistencia').val();
    var PageNumber = 1;
    //alert($("#ddlPaginacionAsistenciasTODO").data("kendoDropDownList"));
    if ($("#ddlPaginacionAsistenciasTODO").data("kendoDropDownList") != undefined) {
        PageNumber = $("#ddlPaginacionAsistenciasTODO").data("kendoDropDownList").value();
    }
    document.getElementById('loadMe').style.display = 'block';
    $("#gridAsistenciaTodos").empty();
    $("#gridAsistenciaTodos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoMembresia":"' + codMembresia + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarDetalleAsistenciaSocio_Paginacion_TODOS",
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
        sortable: true,
        height: 350,
        columns: [
            {
                field: "FechaCreacion",
                title: "<center style='color:#fff;font-size:12px;'><b>Fecha<b></center>",
                width: 45,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "FechaCreacion",
                title: "<center style='color:#fff;font-size:12px;'><b>Hora</b></center>",
                width: 50,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'hh:mm:ss tt') #",
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "DiaSemana",
                title: "<center style='color:#fff;font-size:12px;'><b>Día</b></center>",
                width: 40,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:#fff;font-size:12px;'><b>Responsable</b></center>",
                width: 65,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }]
    });

}

function event_ddlPaginacionAsistenciaTodos(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }
    $('#ddlPaginacionAsistenciasTODO').html(htmlOpcion);
    $("#ddlPaginacionAsistenciasTODO").kendoDropDownList({
        change: function () {
            verAsistencias_VerMasInformacion();
        }
    });
}

function verReservas(codMembresia) {
    var CodigoSocio = $('#hdCodigo').val();

    $("#gridReservas").empty();
    $("#gridReservas").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"codSocio":"' + CodigoSocio + '","CodigoMembresia":"' + codMembresia + '"}',
                        type: "POST",
                        url: "/gestionce/CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            verReservasHistorial(msg);

                        }, complete: function () {

                        }
                    });
                }
            }
        },
        sortable: true,
        height: 130,
        columns: [{
            field: "Disciplina",
            title: "<center style='color:#fff;font-size:12px;'><b>CLASE</b></center>",
            width: 35,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        },
        {
            field: "DiaSemana",
            title: "<center style='color:#fff;font-size:12px;'><b>DIA</b></center>",
            width: 20,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "FechaHoraInicio",
            title: "<center style='color:#fff;font-size:12px;'><b>FECHA</b></center>",
            width: 25,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraInicio, 'yyyy-MM-dd '), 'dd/MM/yyyy') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "FechaHoraInicio",
            title: "<center style='color:#fff;font-size:12px;'><b>INICIA</b></center>",
            width: 25,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraInicio, 'yyyy-MM-dd '), 'hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "FechaHoraFin",
            title: "<center style='color:#fff;font-size:12px;'><b>FINALIZA</b></center>",
            width: 25,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraFin, 'yyyy-MM-dd '), 'hh:mm tt') #",
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><button onclick='Confirmar_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias_Checking(\"#: CodigoHorarioClasesConfiguracion #\",\"#: CodigoHorarioClasesConfiguracionTiempoReal #\",\"#: CodigoHorarioClasesConfiguracionAsistencias #\",#: CodigoSocio #,#: CodigoMembresia #);' type='button' class='btn btn-light btn-sm' title='Marcar Asistencia.' style='font-size:11px;display:#: flagVistaBotonMarcarAsistencia #' >Ingresar</button><div class='far fa-calendar-check' style='display:#: flagVistaImagenAsistio #' title='FECHA INGRESO: #= kendo.toString(kendo.parseDate(FechaHoraAsistio, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') #' ></div></center>",
            title: "",
            width: 23,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }]
    });

}

function verReservasHistorial(data) {

    $("#gridReservasHistorial").empty();
    $("#gridReservasHistorial").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    options.success(data);
                }
            }
        },
        sortable: true,
        height: 160,
        columns: [{
            field: "Disciplina",
            title: "<center style='color:#fff;font-size:12px;'><b>Clase</b></center>",
            width: 35,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "DiaSemana",
            title: "<center style='color:#fff;font-size:12px;'><b>Día</b></center>",
            width: 18,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "FechaHoraInicio",
            title: "<center style='color:#fff;font-size:12px;'><b>Fecha</b></center>",
            width: 22,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraInicio, 'yyyy-MM-dd '), 'dd/MM/yyyy') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "FechaHoraInicio",
            title: "<center style='color:#fff;font-size:12px;'><b>Inicia</b></center>",
            width: 22,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraInicio, 'yyyy-MM-dd '), 'hh:mm tt') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "FechaHoraFin",
            title: "<center style='color:#fff;font-size:12px;'><b>Finaliza</b></center>",
            width: 22,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraFin, 'yyyy-MM-dd '), 'hh:mm tt') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "FechaHoraReserva",
            title: "<center style='color:#fff;font-size:12px;'><b>Fecha Reservado</b></center>",
            width: 26,
            template: "#= kendo.toString(kendo.parseDate(FechaHoraReserva, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm tt') #",
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "UsuarioReservacion",
            title: "<center style='color:#fff;font-size:12px;'><b>Responsable</b></center>",
            width: 21,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            template: "<center>#: DesflagAsistio # <div class='far fa-calendar-check' style='display:#: flagVistaImagenAsistio #' title='FECHA INGRESO: #= kendo.toString(kendo.parseDate(FechaHoraAsistio, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') #' ></div></center>",
            field: "DesflagAsistio",
            title: "<center style='color:#fff;font-size:12px;'><b>¿Asistio?</b></center>",
            width: 12,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            template: "<center><button onclick='Consultar_UspActualizarPresencial_DesactivarHorarioClasesAsistencias_Checking(\"#: CodigoHorarioClasesConfiguracion #\",\"#: CodigoHorarioClasesConfiguracionTiempoReal #\",\"#: CodigoHorarioClasesConfiguracionAsistencias #\",#: CodigoSocio #);' type='button' class='btn btn-light btn-sm' title='Cancelar reserva.' style='font-size:11px;' >Cancelar Reserva</button></center>",
            title: "",
            width: 25,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }]
    });

}



function VerModalEliminarAsistencia() {
    $("#myModalEliminarAsistencia").show('fast');
    var CodigoMenbresia = $('#hdCodigoMembresiaOrigen').val();
    verAsistenciasEliminar(CodigoMenbresia);
}

function verAsistenciasEliminar(codMembresia) {

    $("#gridDetalleAsistenciaEliminar").empty();
    $("#gridDetalleAsistenciaEliminar").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoMembresia":"' + codMembresia + '","PageNumber":"' + 1 + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarDetalleAsistenciaSocio_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            uspListarDetalleAsistenciaSocioEliminar_NumeroRegistros();
                        }
                    });
                }
            }
        },
        sortable: true,
        height: 220,
        columns: [
            {
                field: "FechaCreacion",
                title: "<center style='color:rgb(255 255 255);'>Fecha y Hora</center>",
                width: 100,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') #",
                attributes: {

                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:rgb(255 255 255);'>Responsable</center>",
                width: 100,
                attributes: {

                    style: "font-size:11px;text-align:center;"
                }
            }, {
                width: 30,
                template: "<i class='fa fa-trash-alt fa-2x' data-CodigoHorarioClasesConfiguracionAsistencias='#: CodigoHorarioClasesConfiguracionAsistencias #' onclick='EliminarAsistencia(this,#: CodigoAsistencia #,#: CodigoMembresia #)'></i>",
                title: ""
            }]
    });

}

function uspListarDetalleAsistenciaSocioEliminar_NumeroRegistros() {

    var codMembresia = $('#hdCodigoMembresiaOrigen').val();

    $.ajax({
        data: '{"CodigoMembresia":"' + codMembresia + '"}',
        type: "POST",
        url: "/gestionce/uspListarDetalleAsistenciaSocio_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarDetalleAsistenciaSocioEliminar').html(msg.CantTotal);
            ddlPaginacionuspListarDetalleAsistenciaSocioEliminar(msg.CantTotal, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function ddlPaginacionuspListarDetalleAsistenciaSocioEliminar(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarDetalleAsistenciaSocioEliminar').html(htmlOpcion);
    $("#ddlPaginacionuspListarDetalleAsistenciaSocioEliminar").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarDetalleAsistenciaSocioEliminar").data("kendoDropDownList").value();
            var codMembresia = $('#hdCodigoMembresiaOrigen').val();
            verAsistenciasEliminar_ChanguePage(codMembresia, nroPagina);
        }
    });
}

function verAsistenciasEliminar_ChanguePage(codMembresia, PageNumber) {

    $("#gridDetalleAsistenciaEliminar").empty();
    $("#gridDetalleAsistenciaEliminar").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoMembresia":"' + codMembresia + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarDetalleAsistenciaSocio_Paginacion",
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
        sortable: true,
        height: 220,
        columns: [
            {
                field: "FechaCreacion",
                title: "<center style='color:rgb(0 117 255);'>Fecha y Hora</center>",
                width: 100,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') #",
                attributes: {

                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:rgb(0 117 255);'>Responsable</center>",
                width: 100,
                attributes: {

                    style: "font-size:11px;text-align:center;"
                }
            }, {
                width: 30,
                template: "<i class='fa fa-trash-alt fa-2x' onclick='EliminarAsistencia(#: CodigoAsistencia #,#: CodigoMembresia #)'></i>",
                title: ""
            }]
    });

}

function uspListarDetalleAsistenciaInvitados_NumeroRegistros() {
    var CodigoInvitado = $('#hdCodigoAsistenciaInvitado').val();

    $.ajax({
        data: '{"CodigoInvitado":"' + CodigoInvitado + '"}',
        type: "POST",
        url: "/gestionce/uspListarDetalleAsistenciaInvitados_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarDetalleAsistenciaInvitados').html(msg.CantTotal);
            ddlListaPaginasgridListarDetalleAsistenciaInvitados(msg.CantTotal, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function ddlListaPaginasgridListarDetalleAsistenciaInvitados(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }
    $('#ddlListaPaginasgridListarDetalleAsistenciaInvitados').html(htmlOpcion);
    $("#ddlListaPaginasgridListarDetalleAsistenciaInvitados").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlListaPaginasgridListarDetalleAsistenciaInvitados").data("kendoDropDownList").value();
            var CodigoInvitado = $('#hdCodigoAsistenciaInvitado').val();
            verAsistenciasInvitados_ChanguePage(CodigoInvitado, nroPagina);
        }
    });
}

function verAsistencias_ChanguePage(codMembresia, PageNumber) {

    $("#gridDetalleAsistencia").empty();
    $("#gridDetalleAsistencia").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoMembresia":"' + codMembresia + '","PageNumber":"' + PageNumber + '"}',
                        type: "POST",
                        url: "/gestionce/uspListarDetalleAsistenciaSocio_Paginacion",
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
        sortable: true,
        height: 115,
        columns: [
            {
                field: "FechaCreacion",
                title: "<center>ASISTENCIA</center>",
                width: 45,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'dd/MM/yyyy') #",
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "FechaCreacion",
                title: "<center>HORA</center>",
                width: 50,
                template: "#= kendo.toString(kendo.parseDate(FechaCreacion, 'yyyy-MM-dd '), 'hh:mm:ss tt') #",
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "DiaSemana",
                title: "<center>DIA</center>",
                width: 40,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center>RESPONSABILIDAD</center>",
                width: 65,
                attributes: {
                    style: "font-size:12px;text-align:center;"
                }
            }]
    });

}

function ddlListaPaginasgridListarDetalleAsistenciaSocio(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginacionuspListarDetalleAsistenciaSocio').html(htmlOpcion);
    $("#ddlPaginacionuspListarDetalleAsistenciaSocio").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlPaginacionuspListarDetalleAsistenciaSocio").data("kendoDropDownList").value();
            var codMembresia = $('#hdCodigoMembresiaAsistencia').val();
            verAsistencias_ChanguePage(codMembresia, nroPagina);
        }
    });
}

function uspListarDetalleAsistenciaSocio_NumeroRegistros() {

    var codMembresia = $('#hdCodigoMembresiaAsistencia').val();

    $.ajax({
        data: '{"CodigoMembresia":"' + codMembresia + '"}',
        type: "POST",
        url: "/gestionce/uspListarDetalleAsistenciaSocio_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblCantidadListarDetalleAsistenciaSocio').html(msg.CantTotal);
            ddlListaPaginasgridListarDetalleAsistenciaSocio(msg.CantTotal, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function BuscarCuotas() {
    btnDiaSemanaMesElegidoCuotas();
}

function btnDiaSemanaMesElegidoCuotas() {

    var Tipo = $('#hdDiaSemanaMesElegidoCuotas').val();

    var Vendedor = $("#dllAsesoresVentasCuotas").data('kendoDropDownList').value();
    $("#gvListaVerMasCuotasPorCobrar").empty();
    $("#gvListaVerMasCuotasPorCobrar").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"Tipo":"' + Tipo + '","Vendedor":"' + Vendedor + '","PageNumber":"' + 1 + '"}',
                        url: "/gestionce/uspVerMasClientesComprometidosPagosCuotas_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros();
                            ActivarEventoSelectChangeCuotas();
                        }
                    });
                }
            }
        },
        sortable: true,
        height: 135,
        width: 450,
        columns: [{
            field: "CodigoSocio",
            title: "Codigo",
            width: 10,
            attributes: {

                style: "font-size:9px;text-align:center;"
            }
        }, {
            field: "desTiempoPaquete",
            title: "Membresia",
            width: 15,
            attributes: {

                style: "font-size:10px;text-align:center;"
            }

        }, {
            field: "Precio",
            title: "Precio",
            width: 10,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Pago",
            title: "Pagó",
            width: 10,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }, {
            field: "Debe",
            title: "Debe",
            width: 10,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }, {
            field: "FechaCuota",
            title: "Fecha Cuota",
            width: 20,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "MontoComprometido",
            title: "M.Cuota",
            width: 12,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }, {
            field: "Vendedor",
            title: "Vendedor",
            width: 13,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }]

    });
}

function btnDiaSemanaMesElegidoCuotas_ChanguePague(PageNumber) {

    var Tipo = $('#hdDiaSemanaMesElegidoCuotas').val();

    var Vendedor = $("#dllAsesoresVentasCuotas").data('kendoDropDownList').value();
    $("#gvListaVerMasCuotasPorCobrar").empty();
    $("#gvListaVerMasCuotasPorCobrar").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"Tipo":"' + Tipo + '","Vendedor":"' + Vendedor + '","PageNumber":"' + PageNumber + '"}',
                        url: "/gestionce/uspVerMasClientesComprometidosPagosCuotas_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            ActivarEventoSelectChangeCuotas();
                        }
                    });
                }
            }
        },
        sortable: true,
        height: 135,
        width: 450,
        columns: [{
            field: "CodigoSocio",
            title: "Codigo",
            width: 10,
            attributes: {

                style: "font-size:9px;text-align:center;"
            }
        }, {
            field: "desTiempoPaquete",
            title: "Membresia",
            width: 15,
            attributes: {

                style: "font-size:10px;text-align:center;"
            }

        }, {
            field: "Precio",
            title: "Precio",
            width: 10,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Pago",
            title: "Pagó",
            width: 10,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }, {
            field: "Debe",
            title: "Debe",
            width: 10,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }, {
            field: "FechaCuota",
            title: "Fecha Cuota",
            width: 20,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "MontoComprometido",
            title: "M.Cuota",
            width: 12,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }, {
            field: "Vendedor",
            title: "Vendedor",
            width: 13,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }

        }]

    });
}

function ddlListaPaginasuspVerMasClientesComprometidosPagosCuotas(CantidadTotalFilas, TamanioPagina) {
    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }
    $('#ddlListaPaginasuspVerMasClientesComprometidosPagosCuotas').html(htmlOpcion);
    $("#ddlListaPaginasuspVerMasClientesComprometidosPagosCuotas").kendoDropDownList({
        change: function () {
            var nroPagina = $("#ddlListaPaginasuspVerMasClientesComprometidosPagosCuotas").data("kendoDropDownList").value();
            btnDiaSemanaMesElegidoCuotas_ChanguePague(nroPagina);
        }
    });
}

function uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros() {

    var Tipo = $('#hdDiaSemanaMesElegidoCuotas').val();

    var Vendedor = $("#dllAsesoresVentasCuotas").data('kendoDropDownList').value();
    $.ajax({
        data: '{"Tipo":"' + Tipo + '","Vendedor":"' + Vendedor + '"}',
        type: "POST",
        url: "/gestionce/uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            ddlListaPaginasuspVerMasClientesComprometidosPagosCuotas(msg.CantidadTotalFilas, msg.TamanioPagina);
        }, complete: function () {

        }
    });
}

function ListarUsuarioResponsableVerDeudasSuplementos() {

    $("#dllAsesoresVentasSuplementos").empty();
    $("#dllAsesoresVentasSuplementos").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="dllAsesoresVentasSuplementos_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsableSuplementos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            for (var i = 0; i < msg.length; i++) {
                                msg[i].NombreCompleto = msg[i].NombreCompleto.toUpperCase();
                            }
                            options.success(msg);

                            $('#dllAsesoresVentasSuplementos').data("kendoDropDownList").value(User.toUpperCase());
                        }
                    });
                }
            }
        }
    });

}

function uspRegistrarComprobantePago() {
    $('button').attr('disabled', 'disabled');
    document.getElementById('loadMe').style.display = 'block';

    var entidad = {};
    entidad.CodigoCliente = 0;
    //armar los pagos
    entidad.listaDetallePago = new Array();

    var grid = $("#GridDeudasSuplementos").data("kendoGrid");
    var dataSource = grid.dataSource;
    //   Recorre cada registro en una cuadrícula de Kendo
    var validadorPago = 0;
    $.each(grid.items(), function (index, item) {
        var uid = $(item).data("uid");
        var dataItem = dataSource.getByUid(uid);
        // Agrega una ID a cada fila como ejemplo

        var detallePago = {};
        detallePago.CodigoComprobantePago = 0;
        detallePago.CodigoComprobante = dataItem.CodigoComprobante;
        detallePago.CodigoComprobanteDetalle = dataItem.CodigoComprobanteDetalle;
        detallePago.CodigoCuentaBancaria = 0;
        detallePago.CodigoMetodoPago = $('#hdCodigoFormaPago').val();
        detallePago.TipoMoneda = 2;
        detallePago.Monto = $(this).find('td').eq(7).find('input').val();
        detallePago.Total = $(this).find('td').eq(7).find('input').val();
        detallePago.Nota = "";
        entidad.listaDetallePago.push(detallePago);

        if (parseFloat(detallePago.Monto).toFixed(2) > parseFloat(detallePago.Total).toFixed(2)) {
            validadorPago = 1;
        }

        //alert("CodigoComprobante: " + dataItem.CodigoComprobante);
        //alert("CodigoComprobanteDetalle: " + dataItem.CodigoComprobanteDetalle);
        //alert($(this).find('td').eq(7).find('input').val());

    });

    if (validadorPago == 1) {
        document.getElementById('loadMe').style.display = 'none';
        $('button').removeAttr('disabled');
        $.bootstrapGrowl("Error, El monto del pago no puede ser mayor al total.", { type: 'danger', width: 'auto' });
        return;
    }

    var metodoCorrecto = function (msg) {

        if (msg) {
            document.getElementById('loadMe').style.display = 'none';

            var CodigoSocio = $('#hdCodigo').val();

            BuscarInformacionSociosPorCodigoFiltro(CodigoSocio);

            $('#btnCerrar_myModalPagarDeudaProductos').click();
            $.bootstrapGrowl("Se guardo la información correctamente, vuelve a buscar al cliente.", { type: 'success', width: 'auto' });
            //ImprimeTicket();
            //limpiarVenta();
            $('button').removeAttr('disabled');
        }
        else {
            $.bootstrapGrowl("Error, no se pudo guardar la información.", { type: 'danger', width: 'auto' });
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/gestionce/ecommerce_uspRegistrarPagoComprobante", request, metodoCorrecto, metodoError);

}

function selectFormaPago(codigoForma) {
    $('#hdCodigoFormaPago').val(codigoForma);
}

function CentroEntrenamiento_uspListarDeudasCliente() {
    $('#divCerrarmyModalPagarDeudaProductos,#btnCerrar_myModalPagarDeudaProductos').click(function () {
        $('#myModalPagarDeudaProductos').hide('fast');
    });

    var Identificacion = $('#infoDNI').html();

    if (Identificacion != '') {

        document.getElementById('loadMe').style.display = 'block';
        $("#GridDeudasSuplementos").empty();
        $("#GridDeudasSuplementos").kendoGrid({
            dataSource: {
                type: "json",
                transport: {
                    read: function (options) {
                        $.ajax({
                            type: "POST",
                            data: '{"Identificacion":"' + Identificacion + '"}',
                            url: "/gestionce/CentroEntrenamiento_uspListarDeudasCliente",
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
            height: 135,
            columns: [
                {
                    field: "Correlativo",
                    title: "<b style='color:#fff;'>Correlativo</b>",
                    width: 5,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;"
                    }
                }, {
                    field: "Descripcion",
                    title: "<b style='color:#fff;'>Descripcion</b>",
                    width: 10,
                    attributes: {
                        style: "font-size:13px;font-weight:bold;"
                    }
                }, {
                    field: "Precio",
                    title: "<b style='color:#fff;'>Precio</b>",
                    width: 5,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;"
                    }
                }, {
                    field: "Cantidad",
                    title: "<b style='color:#fff;'>Cantidad</b>",
                    width: 5,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;"
                    }
                }, {
                    field: "Total",
                    title: "<b style='color:#fff;'>Total</b>",
                    width: 5,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;"
                    }
                }, {
                    field: "Importe",
                    title: "<b style='color:#fff;'>A cuenta</b>",
                    width: 5,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;"
                    }
                }, {
                    field: "Debe",
                    title: "<b style='color:#fff;'>Debe</b>",
                    width: 5,
                    attributes: {
                        style: "font-size:15px;font-weight:bold;"
                    }
                }, {
                    field: "Debe",
                    title: "<b style='color:#fff;'>Ingresar pago</b>",
                    template: '<input id="txtMonto_deudaproducto_#: CodigoComprobanteDetalle #" style="font-size:15px;font-weight:bold;width:85%;" class="form-control" placeholder="ingresar pago" type="text" value="0">',
                    width: 5
                }]
        });
    }

}

function uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion() {
    $('#imgCargandoVentasDia').css('display', '');

    var FechaInicio = kendo.toString($("#txtFechaInicioAcuentaSuplementos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFechaFinAcuentaSuplementos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var AsesorComercial = $('#dllAsesoresVentasSuplementos').data("kendoDropDownList").value() == 'Todos' ? '' : $('#dllAsesoresVentasSuplementos').data("kendoDropDownList").value();
    $("#GridDeudasSuplementos").empty();
    $("#GridDeudasSuplementos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","AsesorComercial":"' + AsesorComercial + '","PageNumber":"' + 1 + '"}',
                        url: "/gestionce/uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros(CodigoSede, FechaInicio, FechaFin, AsesorComercial);
                            ActivarEventoSelectChangeDeudasSuplementos();
                            $('#imgCargandoVentasDia').css('display', 'none');
                        }
                    });
                }
            }
        },
        sortable: true,
        height: 135,
        columns: [{
            field: "CodigoSocio",
            title: "Codigo",
            width: 3,
            attributes: {

                style: "font-size:9px;"
            }
        }, {
            field: "Descripcion",
            title: "Suplemento",
            width: 12,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            field: "Total",
            title: "Total",
            width: 5,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            field: "Debe",
            title: "Debe",
            width: 5,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            field: "Responsable",
            title: "Responsable",
            width: 5,
            attributes: {

                style: "font-size:10px;"
            }
        }]

    });
}

function uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros(CodigoSede, FechaInicio, FechaFin, AsesorComercial) {

    $.ajax({
        data: '{"FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","AsesorComercial":"' + AsesorComercial + '"}',
        type: "POST",
        url: "/gestionce/uspListarACuentaSuplementosPorFechaRangoFechas_NumeroRegistros",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#lblTodalDebeSuplementos').html(msg.TotalDeuda);
            ddlPaginasuspListarSuplementosSociosAcuenta(msg.CantidadRegistros, msg.TamanioPagina);

        }, complete: function () {

        }
    });

}

function ddlPaginasuspListarSuplementosSociosAcuenta(CantidadTotalFilas, TamanioPagina) {

    var CantidadPaginas = parseInt(CantidadTotalFilas / TamanioPagina) + 1;
    var htmlOpcion = "";
    for (var i = 1; i <= CantidadPaginas; i++) {
        htmlOpcion += "<option value='" + i + "'> " + i + " </option>";
    }

    $('#ddlPaginasuspListarSuplementosSociosAcuenta').html(htmlOpcion);
    $("#ddlPaginasuspListarSuplementosSociosAcuenta").kendoDropDownList({
        change: function () {
            uspListarACuentaSuplementosPorFechaRangoFechas_ChanguePage();
        }
    });
}

function uspListarACuentaSuplementosPorFechaRangoFechas_ChanguePage() {
    $('#imgCargandoVentasDia').css('display', '');


    var FechaInicio = kendo.toString($("#txtFechaInicioAcuentaSuplementos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var FechaFin = kendo.toString($("#txtFechaFinAcuentaSuplementos").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    var AsesorComercial = $('#dllAsesoresVentasSuplementos').data("kendoDropDownList").value() == 'Todos' ? '' : $('#dllAsesoresVentasSuplementos').data("kendoDropDownList").value();
    var PageNumber = $("#ddlPaginasuspListarSuplementosSociosAcuenta").data('kendoDropDownList').value();
    $("#GridDeudasSuplementos").empty();
    $("#GridDeudasSuplementos").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"FechaInicio":"' + FechaInicio + '","FechaFin":"' + FechaFin + '","AsesorComercial":"' + AsesorComercial + '","PageNumber":"' + PageNumber + '"}',
                        url: "/gestionce/uspListarACuentaSuplementosPorFechaRangoFechas_Paginacion",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {
                            $('#imgCargandoVentasDia').css('display', 'none');
                            ActivarEventoSelectChangeDeudasSuplementos();
                        }
                    });
                }
            }
        },
        sortable: true,
        height: 135,
        columns: [{
            field: "CodigoSocio",
            title: "Codigo",
            width: 3,
            attributes: {

                style: "font-size:9px;"
            }
        }, {
            field: "Descripcion",
            title: "Descripcion",
            width: 12,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            field: "Total",
            title: "Total",
            width: 5,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            field: "Debe",
            title: "Debe",
            width: 5,
            attributes: {

                style: "font-size:10px;"
            }
        }, {
            field: "Responsable",
            title: "Responsable",
            width: 5,
            attributes: {

                style: "font-size:10px;"
            }
        }]

    });
}

function listardllAsesoresVentasCuotas() {


    var dllAsesoresVentas = $("#dllAsesoresVentasCuotas").kendoDropDownList({
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({

                        type: "POST",
                        url: "/gestionce/listardllAsesoresVentas",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            $('#dllAsesoresVentasCuotas').data('kendoDropDownList').value(User);

                        }, complete: function () {
                            btnDiaSemanaMesElegidoCuotas();
                        }
                    });
                }
            }
        }, change: function () {

        }
    }).data("kendoDropDownList");
}

function listardllAsesoresVentasInasistencias() {


    var dllAsesoresVentas = $("#dllAsesoresVentasInasistencias").kendoDropDownList({
        optionLabel: "Todos",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            transport: {
                read: function (options) {

                    $.ajax({
                        type: "POST",
                        url: "/gestionce/listardllAsesoresVentas",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);

                            $('#dllAsesoresVentasInasistencias').data('kendoDropDownList').value(User);

                        }, complete: function () {
                            //uspListar_Socios_Inasistencias();
                        }
                    });
                }
            }
        }, change: function () {

        }
    }).data("kendoDropDownList");
}

function uspListarClientesMenbresiasCuotas(msg) {

    $("#gridVerCuotas").empty();
    $("#gridVerCuotas").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {

                    if (msg.length == 0) {
                        event_mostrarPagos();
                    } else {
                        event_mostrarCuotas();
                    }
                    options.success(msg);

                    //$.ajax({
                    //    data: '{"CodigoMenbresia":"' + CodigoMenbresia + '"}',
                    //    type: "POST",
                    //    url: "/gestionce/uspListarClientesMenbresiasCuotas",
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    success: function (msg) {
                    //        if (msg.length == 0) {
                    //            event_mostrarPagos();
                    //        } else {
                    //            event_mostrarCuotas();
                    //        }
                    //        options.success(msg);
                    //    }, complete: function () {

                    //    }
                    //});
                }
            },

        },
        sortable: true,
        height: 150,
        columns: [{
            field: "Fecha",
            title: "<center>Fecha Cuota</center>",
            template: "#= kendo.toString(kendo.parseDate(Fecha, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
            width: 65,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Monto",
            title: "<center>Monto</center>",
            width: 50,
            format: '{0:0.00}',
            attributes: {

                style: "font-size:11px;text-align:center;"
            }
        },
        {
            field: "UsuarioCreacion",
            title: "<center>Responsable</center>",
            width: 80,
            attributes: {

                style: "font-size:11px;text-align:center;"
            }
        }]
    });

}

function cargarMenu() {

    var numCliente = 0;
    var numAgenda = 0;
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

function ActivarEventoSelectChangeCuotas() {
    $('#gvListaVerMasCuotasPorCobrar tr').click(function () {
        $('#gvListaVerMasCuotasPorCobrar tr').removeClass('k-state-selected');
        $(this).addClass('k-state-selected');
        var codigo = $(this).find("td").eq(0).html();
        $('#hdCodigo').val(codigo);
        BuscarInformacionSociosPorCodigo(codigo);

        $('#gvListado tr').removeClass('k-state-selected');
        $('#gvListado').find("tr").eq(0).removeClass('k-state-selected');
        $('#GridDeudas tr').removeClass('k-state-selected');
        $('#GridDeudas').find("tr").eq(0).removeClass('k-state-selected');

        $('#gvListadoInasistencias tr').removeClass('k-state-selected');
        $('#gvListadoInasistencias').find("tr").eq(0).removeClass('k-state-selected');

        $('#gvListaAsistencia tr').removeClass('k-state-selected');
        $('#gvListaAsistencia').find("tr").eq(0).removeClass('k-state-selected');

        $('#GridDeudasSuplementos tr').removeClass('k-state-selected');
        $('#GridDeudasSuplementos').find("tr").eq(0).removeClass('k-state-selected');

        $('#gridCumpleanios tr').removeClass('k-state-selected');
        $('#gridCumpleanios').find("tr").eq(0).removeClass('k-state-selected');

        $('#txtBuscadorGeneral').val('');
        $('#flagMarcarAsistencia').val(0);

    });
}

function SelectChangeDefault() {
    $('#gvListado').find("tr").eq(1).addClass('k-state-selected');
    var codigo = $('#gvListado').find("tr").eq(1).find("td").eq(0).html();
    $('#hdCodigo').val(codigo);
    BuscarInformacionSociosPorCodigo(codigo);

    $('#GridDeudas tr').removeClass('k-state-selected');
    $('#GridDeudas').find("tr").eq(0).removeClass('k-state-selected');
    $('#gvListaVerMasCuotasPorCobrar tr').removeClass('k-state-selected');
    $('#gvListaVerMasCuotasPorCobrar').find("tr").eq(0).removeClass('k-state-selected');

    $('#gvListadoInasistencias tr').removeClass('k-state-selected');
    $('#gvListadoInasistencias').find("tr").eq(0).removeClass('k-state-selected');

    $('#gvListaAsistencia tr').removeClass('k-state-selected');
    $('#gvListaAsistencia').find("tr").eq(0).removeClass('k-state-selected');

    $('#GridDeudasSuplementos tr').removeClass('k-state-selected');
    $('#GridDeudasSuplementos').find("tr").eq(0).removeClass('k-state-selected');

    $('#gridCumpleanios tr').removeClass('k-state-selected');
    $('#gridCumpleanios').find("tr").eq(0).removeClass('k-state-selected');

    $('#txtBuscadorGeneral').val('');
    $('#flagMarcarAsistencia').val(0);

}

function ActivarEventoSelectChangeDeudasSuplementos() {
    $('#GridDeudasSuplementos tr').click(function () {
        $('#GridDeudasSuplementos tr').removeClass('k-state-selected');
        $(this).addClass('k-state-selected');

        var codigo = $(this).find("td").eq(0).html();
        $('#hdCodigo').val(codigo);
        BuscarInformacionSociosPorCodigo(codigo);

        $('#GridDeudas tr').removeClass('k-state-selected');
        $('#GridDeudas tr').find("tr").eq(0).removeClass('k-state-selected');
        $('#gvListado tr').removeClass('k-state-selected');
        $('#gvListado').find("tr").eq(0).removeClass('k-state-selected');
        $('#gvListaVerMasCuotasPorCobrar tr').removeClass('k-state-selected');
        $('#gvListaVerMasCuotasPorCobrar').find("tr").eq(0).removeClass('k-state-selected');
        $('#gvListadoInasistencias tr').removeClass('k-state-selected');
        $('#gvListadoInasistencias').find("tr").eq(0).removeClass('k-state-selected');

        $('#gvListaAsistencia tr').removeClass('k-state-selected');
        $('#gvListaAsistencia').find("tr").eq(0).removeClass('k-state-selected');

        $('#gridCumpleanios tr').removeClass('k-state-selected');
        $('#gridCumpleanios').find("tr").eq(0).removeClass('k-state-selected');

        $('#txtBuscadorGeneral').val('');
        $('#flagMarcarAsistencia').val(0);

    });
}

function ActivarEventoSelectChangeCumpleanios() {
    $('#gridCumpleanios tr').click(function () {
        $('#gridCumpleanios tr').removeClass('k-state-selected');
        $(this).addClass('k-state-selected');

        var codigo = $(this).find("td").eq(0).html();
        $('#hdCodigo').val(codigo);
        BuscarInformacionSociosPorCodigo(codigo);

        $('#GridDeudas tr').removeClass('k-state-selected');
        $('#GridDeudas tr').find("tr").eq(0).removeClass('k-state-selected');
        $('#gvListado tr').removeClass('k-state-selected');
        $('#gvListado').find("tr").eq(0).removeClass('k-state-selected');
        $('#gvListaVerMasCuotasPorCobrar tr').removeClass('k-state-selected');
        $('#gvListaVerMasCuotasPorCobrar').find("tr").eq(0).removeClass('k-state-selected');
        $('#gvListadoInasistencias tr').removeClass('k-state-selected');
        $('#gvListadoInasistencias').find("tr").eq(0).removeClass('k-state-selected');

        $('#gvListaAsistencia tr').removeClass('k-state-selected');
        $('#gvListaAsistencia').find("tr").eq(0).removeClass('k-state-selected');

        $('#GridDeudasSuplementos tr').removeClass('k-state-selected');
        $('#GridDeudasSuplementos').find("tr").eq(0).removeClass('k-state-selected');

        $('#txtBuscadorGeneral').val('');
        $('#flagMarcarAsistencia').val(0);

    });
}

function EliminarSocio(codigoSocio) {
    $('#hdCodigo').val(codigoSocio);
    $('#myModalConfirm').show('fast');
}

function EstaFocus() {
    $('#hdFlagVentanaSocios').val('0');
    $("#txtBuscadorGeneral").css("background-color", "yellow");
    $('#flagMarcarAsistencia').val(1);
}

function SalidaFocus() {
    $("#txtBuscadorGeneral").css("background-color", "White");
    $('#flagMarcarAsistencia').val(0);
    $('#hdFlagVentanaSocios').val('-1');
}

function validarCamposCadena(e) { // 1
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    patron = /[A-Za-z.\s\w\ñ\Ñ]/; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}

function fechaMascara() {

    $("#txtFechaInicioAcuentaSuplementos").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaFinAcuentaSuplementos").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaFreezingProcFreezing").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaFreezingFinProcFreezing").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
    $("#txtFechaSalidaSuplementos_Modal").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });
}

function ImprimirContratolMembresias() {
    var codigoMembresia = $('#hdCodigoMembresiaOrigen').val();
    ListarContratoMembresiaImprimir(codigoMembresia);
}

function verMasInformacionSocio() {
    $('#myModalVerMas').show('fast');
    $('#DivCerrarmyModalVerMas').click(function () {
        $('#myModalVerMas').hide('fast');
    });

    event_uspCentroEntrenamiento_uspConsumoTotalPorCliente();
}

function verMasPagarDeuda() {

    //$('#myModalverMasPagarDeuda').show('fast');
    //$('#DivCerrarmyModalverMasPagarDeuda').click(function () {
    //    $('#myModalverMasPagarDeuda').hide('fast');
    //});

    if ($('#hdFlagVentanaPagos').val() == '0') {

        var todayDate = new Date();
        $("#txtFechaSalida").kendoDatePicker();
        $('#txtFechaSalida').kendoDatePicker({
            value: todayDate,
            change: function () {
                var fechaInicioFiltro = $("#txtFechaSalida").data('kendoDatePicker').value();
                var mmfechaInicioFiltro = fechaInicioFiltro.getDate();

                var hoy = new Date();
                if (hoy < fechaInicioFiltro) {
                    $('#txtFechaSalida').data("kendoDatePicker").value(hoy);
                    $.bootstrapGrowl("La fecha no puede ser mayor al dia actual.", { type: 'primary', width: 'auto' });
                }

            }
        });

        $("#txtFechaSalida").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });

        $('#txtTotalPagado').keyup(function () {
            var total = $('#lblTotal').html();
            var TotalPagando = $('#txtTotalPagado').val();
            var debe = parseFloat(total) - parseFloat(TotalPagando);
            $('#lblTotalDebe').html(debe);
            calcularTotalNeto();
        });

        $('#txtTotalPagado').click(function () {
            $('#txtTotalPagado').select();
        });

        ListaTipoDocumentoContrato();

        $('#lblRealizarPago').click(function () {

            var aporte = $('#txtTotalPagado').val();
            var total = $('#lblTotal').html();
            var totalDeuda = $('#lblTotalDebe').html();
            var Acuenta = $('#hdMontoTal').val();

            var count = 0;
            $('#gridMenbresias tbody tr').each(function () {
                count += 1;
            });
            if (count > 0) {
                if ($('#infoCodigo').html() == '0') {
                    $.bootstrapGrowl(" Debe buscar un socio, es importante para un membresia", { type: 'danger', width: 'auto' });
                } else {
                    if (total > 0) {
                        if (parseFloat(aporte) == parseFloat(total)) {
                            guardarVenta();
                        } else if (parseFloat(aporte) > parseFloat(total)) {
                            $.bootstrapGrowl("El aporte no puede ser mayor al total de la venta.", { type: 'danger', width: 'auto' });
                        } else if (aporte > 0 && parseFloat(aporte) < parseFloat(total)) {
                            guardarVenta();
                        } else {
                            $.bootstrapGrowl("Los datos del pago son incorrectos , verifique el a cuenta a pagar", { type: 'danger', width: 'auto' });
                        }
                    } else {
                        $.bootstrapGrowl("No se puede efectuar el pago de saldo 0", { type: 'danger', width: 'auto' });
                    }
                }
            } else {
                $.bootstrapGrowl("No hay contratos a pagar en la lista.", { type: 'danger', width: 'auto' });
            }
        });

        //if (CodigoUnidadNegocio != 16) {
        //    if (Perfil == 1 || Perfil == 13) {
        //        $('#txtFechaSalida').data("kendoDatePicker").enable();
        //    } else {
        //        $('#txtFechaSalida').data("kendoDatePicker").enable(false);
        //    }
        //}
    }

    $('#hdFlagVentanaPagos').val('1');

}

function listaSubTipoDocumentoSerie(CodTipoDocumento) {

    var ddlSubTipoDocumento = $("#ddlSubTipoDocumento").kendoDropDownList({
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodTipoDocumento":"' + CodTipoDocumento + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSubTipoDocumentosPorTipoDocumento",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        },
                        complete: function () {
                            generarSerieComprobante();
                            ValidarGenerarSerieComprobante();
                        }
                    });
                }
            }
        },
        change: function () {
            ValidarGenerarSerieComprobante();
        }
    }).data("kendoDropDownList");
}

function ValidarGenerarSerieComprobante_Diario() {
    $.ajax({
        type: "POST",
        url: "/gestionce/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.GenerarSerie) {
                generarSerieComprobante_Diario();
            } else {
                $('#txtNroDocumento').removeClass("disabled");
            }
        }
    });
}

function generarSerieComprobante_Diario() {
    var Documento = "";
    $('input[name="orderBoxComprobante_Diario[]"]:checked').each(function () {
        Documento += $(this).val();
    });
    var TipoDocumento = Documento;

    var SubTipoDocumento = $("#ddlSubTipoDocumento_Diario").data('kendoDropDownList').value();

    $.ajax({
        data: '{"tipoDocumento":"' + TipoDocumento + '","subTipoDocumento":"' + SubTipoDocumento + '"}',
        type: "POST",
        url: "/gestionce/ObtenerSerieGenarado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#txtNroDocumento_Diario').val(msg);
            $('#txtNroDocumento_Diario').addClass("disabled");
            $('#txtNroDocumento_Diario').removeAttr("disabled");
            if (msg.length > 1) {
                $('#txtNroDocumento_Diario').attr("disabled", "disabled");
            }
        }
    });
}



function generarSerieComprobante() {
    var Documento = "";
    $('input[name="orderBoxComprobante[]"]:checked').each(function () {
        Documento += $(this).val();
    });
    var TipoDocumento = Documento;
    var SubTipoDocumento = $("#ddlSubTipoDocumento").data('kendoDropDownList').value();

    $.ajax({
        data: '{"tipoDocumento":"' + TipoDocumento + '","subTipoDocumento":"' + SubTipoDocumento + '"}',
        type: "POST",
        url: "/gestionce/ObtenerSerieGenarado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            $('#txtNroDocumento').val(msg);
            $('#txtNroDocumento').addClass("disabled");
            $('#txtNroDocumento').removeAttr("disabled");
            if (msg.length > 1) {
                $('#txtNroDocumento').attr("disabled", "disabled");
            }
        }
    });
}

function ValidarGenerarSerieComprobante() {

    $.ajax({
        type: "POST",
        url: "/gestionce/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.GenerarSerie) {
                generarSerieComprobante();
            } else {
                $('#txtNroDocumento').removeClass("disabled");
            }
        }
    });
}

function ListaTipoDocumentoContrato() {
    var TipoDocumento = "";
    $('input[name="orderBoxComprobante[]"]:checked').each(function () {
        TipoDocumento += $(this).val();
    });
    listaSubTipoDocumentoSerie(TipoDocumento);
    calcularTotalNeto();
}

function calcularTotalNeto() {
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobante[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });
    var SubTotal = 0;
    var igv = 0;
    var total = $('#txtTotalPagado').val();
    if (codigoTipoDocumento == 1) { //factura
        igv = parseFloat(total) * 0.18;
        SubTotal = parseFloat(total) - igv;
        document.getElementById("tdSubtotal").style.display = '';
        document.getElementById("tdIGV").style.display = '';

    } else if (codigoTipoDocumento == 2 || codigoTipoDocumento == 3) { //boleta o otros
        total = total;
        SubTotal = 0.00;
        igv = 0.00;
        document.getElementById("tdSubtotal").style.display = 'none';
        document.getElementById("tdIGV").style.display = 'none';
    }
    $('#txtSubTotalTop').html(SubTotal.toFixed(2));
    $('#txtIGVTop').html(igv.toFixed(2));
}

function guardarVenta() {
    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");

    var index = 0;
    var codigoSalida = 0;
    var codigoCliente = $('#infoCodigo').html() == '' ? 0 : $('#infoCodigo').html();
    var razonSocial_Sr = '';//$('#txtNombre').val() + ' ' + $('#txtApellido').val();
    var nroDNIRUC = '';//$('#txtDni').val();
    var direccion = '';//$('#txtDireccion').val();
    var fechaSalida = kendo.toString($("#txtFechaSalida").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    var codigoSubTipoDocumento = $('#ddlSubTipoDocumento').data("kendoDropDownList").value();
    var nroDocumento = $('#txtNroDocumento').val();
    var subTotal = $('#txtSubTotalTop').html();
    var igv = $('#txtIGVTop').html();
    var total = $('#txtTotalPagado').val();
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobante[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });
    if (codigoTipoDocumento == 1) {
        var subTotal = $('#txtSubTotalTop').html();
        var igv = $('#txtIGVTop').html();
        var total = $('#txtTotalPagado').val();
    } else {
        var subTotal = '0.00';
        var igv = '0.00';
        var total = $('#txtTotalPagado').val();
    }
    var xml = "";
    xml += "<ds>";
    var codigoProducto = $('#hdCodigoCodigoMenbresiaPago').val();
    var tipo = $('#hdTipoProducto').val();
    var CodigoDetalle = 0;
    var CodigoPedido = 0;
    var AsesorComercial = $('#hdAsesorComercialPago').val();
    var TipoIngresoMembre = $('#hdCodigoTipoIngresoPago').val();
    var cantidad = 1;
    var descripcion = $('#hdCodigoDescripcionMenbresiaPago').val();
    var precioUnitario = $('#txtTotalPagado').val();
    var importe = $('#txtTotalPagado').val();
    xml += "<d>";
    xml += "<a>" + codigoProducto + "</a>"; //CodigoProducto
    xml += "<b>" + tipo + "</b>"; //tipo
    xml += "<c>" + cantidad + "</c>"; //Cantidad
    xml += "<e>" + descripcion + "</e>"; //Descripcion
    xml += "<f>" + precioUnitario + "</f>"; //PrecioUnitario
    xml += "<g>" + importe + "</g>"; //Importe
    xml += "<i>" + CodigoDetalle + "</i>"; //Importe
    xml += "<j>" + CodigoPedido + "</j>"; //codigoPedido
    xml += "<k>" + AsesorComercial + "</k>"; //Asesor comercial
    xml += "<n>" + TipoIngresoMembre + "</n>";
    xml += "</d>";
    xml += "</ds>";
    //FORMA DE PAGO
    var xmlFormaPago = "";
    xmlFormaPago += "<ds>";
    var FP_TipoMoneda = 1;
    var FP_Monto = $('#txtTotalPagado').val();
    var FP_TipoCambio = 0;
    var FP_NroBoucher = $('#txtTextoBaucher1').val();
    var FP_FormaPago = "";
    $('input[name="orderBoxFormaPago[]"]:checked').each(function () {
        FP_FormaPago += $(this).val();
    });
    var FP_SubFormaPago = "";
    if (FP_FormaPago == 1) {
        FP_SubFormaPago = 0;
    } else if (FP_FormaPago == 2) {
        $('input[name="orderBoxOpcion1[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 3) {
        $('input[name="orderBoxOpcion1[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 4) {
        $('input[name="orderBoxOpcion2[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 5) {
        FP_SubFormaPago = 0;
    }
    //formaPago1
    xmlFormaPago += "<d>";
    xmlFormaPago += "<a>" + FP_TipoMoneda + "</a>"; //Tipo moneda
    xmlFormaPago += "<b>" + FP_Monto + "</b>"; //Monto
    xmlFormaPago += "<c>" + FP_TipoCambio + "</c>"; //Tipo cambio
    xmlFormaPago += "<e>" + FP_FormaPago + "</e>"; //Forma de pago
    xmlFormaPago += "<f>" + FP_SubFormaPago + "</f>"; //sub Forma de pago
    xmlFormaPago += "<g>" + FP_NroBoucher + "</g>"; //Nro Boucher
    xmlFormaPago += "</d>";
    xmlFormaPago += "</ds>";
    var xmlCuotas = "";

    $.ajax({
        data: '{"codigoSalida":"' + codigoSalida + '","CodigoSocio":"' + codigoCliente + '","RazonSocial_Sr":"' + razonSocial_Sr + '","RUC_DNI":"' + nroDNIRUC + '","Direccion":"' + direccion + '","FechaVenta":"' + fechaSalida +
            '","CodigoTipoComprobante":"' + codigoTipoDocumento + '","CodigoSubTipoComprobante":"' + codigoSubTipoDocumento + '","NroComprobante":"' + nroDocumento +
            '","NroTarjeta":"' + FP_NroBoucher + '","TipoMoneda":"' + 0 + '","FormaPago":"' + FP_FormaPago +
            '","SubTotal":"' + subTotal + '","IGV":"' + igv + '","TotalNeto":"' + total +
            '","tipoCambio":"' + 0 + '","listaDetalle":"' + xml + '","listaFormaPago":"' + xmlFormaPago +
            '","TotalDolares":"' + 0 + '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '","listaCuotas":"' + xmlCuotas + '"}',
        type: "POST",
        url: "/gestionce/GuardarSalida",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            debugger;
            if (msg.split('|')[0] > 0) {

                if (msg.split('|')[1] == 0) {
                    $.bootstrapGrowl("La impresora no está activa.", { type: 'primary', width: 'auto' });
                    ListaTipoDocumentoContrato();

                    $('#txtTotalPagado').val("0.00");
                    $.bootstrapGrowl("El pago se ha realizado correctamente.", { type: 'success', width: 'auto' });
                } else if (msg.split('|')[1] == "1") {
                    $.bootstrapGrowl("Imprimiendo comprobante.", { type: 'success', width: 'auto' });

                    //----Amador 05/01/2023----
                    generateComprobanteGlobal(msg.split('|')[0])

                    ListaTipoDocumentoContrato(msg.split('|')[1]);
                    $('#txtTotalPagado').val("0.00");
                    document.getElementById("BtnPagarDeudaClose").click();
                }
                else if (msg.split('|')[1] == "2") {

                    var urlPDFComprobante = msg.split('|')[2];
                    if (urlPDFComprobante != null) {
                        ImprimirPDFJs(urlPDFComprobante);
                    }
                    else {
                        $.bootstrapGrowl("No se emitio sunafact.", { type: 'success', width: 'auto' });
                    }
                }
            } else {
                $.bootstrapGrowl("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias", { type: 'danger', width: 'auto' });
            }
        }, complete: function () {
            var CodigoSocio = $('#txtBuscadorGeneral').val();
            ListarMembresia(CodigoSocio);
            $('button[type="button"]').attr("disabled", false);
            document.getElementById("btnCerrar_myModalverMasPagarDeuda").click();
            document.getElementById('loadMe').style.display = 'none';
        }
    });
}

function verMasPagarFiado() {
    $('#myModalverMasPagarFiado').show('fast');
    $('#DivCerrarmyModalverMasPagarFiado').click(function () {
        $('#myModalverMasPagarFiado').hide('fast');
    });

    ListarPedidosDelSocio();

    if ($('#hdFlagVentanaFiado').val() == '0') {
        var todayDate = new Date();
        $("#txtFechaSalidaSuplementos").kendoDatePicker();
        $('#txtFechaSalidaSuplementos').data("kendoDatePicker").value(todayDate);
        $("#txtFechaSalidaSuplementos").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });

        $('#txtTotalPagado').keyup(function () {
            var total = $('#lblTotal').html();
            var TotalPagando = $('#txtTotalPagado').val();
            var debe = parseFloat(total) - parseFloat(TotalPagando);
            $('#lblTotalDebe').html(debe);
            calcularTotalNeto();
        });

        $('#txtTotalPagado').click(function () {
            $('#txtTotalPagado').select();
        });

        ListaTipoDocumentoContratoSuplementos();
        SEGListarUsuarioResponsableSuplementos();

        $('#lblRealizarPagoSuplementos').click(function () {

            var count = 0;
            $('#gridSuplementos tbody tr').each(function () {
                count += 1;
            });
            if (count > 0) {
                if ($('#infoCodigo').html() == '0') {
                    $.bootstrapGrowl(" Debe buscar un socio.", { type: 'danger', width: 'auto' });
                } else {
                    guardarPagofiado();
                }
            } else {
                $.bootstrapGrowl("No hay productos a pagar en la lista.", { type: 'danger', width: 'auto' });
            }
        });
    }

    $('#hdFlagVentanaFiado').val('1');

}

function ListaTipoDocumentoContratoSuplementos() {
    var TipoDocumento = "";
    $('input[name="orderBoxComprobanteSuplementos[]"]:checked').each(function () {
        TipoDocumento += $(this).val();
    });
    listaSubTipoDocumentoSerieSuplementos(TipoDocumento);
}

function listaSubTipoDocumentoSerieSuplementos(CodTipoDocumento) {

    var ddlSubTipoDocumento = $("#ddlSubTipoDocumentoSuplementos").kendoDropDownList({
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodTipoDocumento":"' + CodTipoDocumento + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSubTipoDocumentosPorTipoDocumento",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        },
                        complete: function () {
                            generarSerieComprobanteSuplementos();
                            ValidarGenerarSerieComprobanteSuplementos();
                        }
                    });
                }
            }
        },
        change: function () {
            ValidarGenerarSerieComprobanteSuplementos();
        }
    }).data("kendoDropDownList");
}

function generarSerieComprobanteSuplementos() {
    var Documento = "";
    $('input[name="orderBoxComprobanteSuplementos[]"]:checked').each(function () {
        Documento += $(this).val();
    });
    var TipoDocumento = Documento;
    var SubTipoDocumento = $("#ddlSubTipoDocumentoSuplementos").data('kendoDropDownList').value();


    $.ajax({
        data: '{"tipoDocumento":"' + TipoDocumento + '","subTipoDocumento":"' + SubTipoDocumento + '"}',
        type: "POST",
        url: "/gestionce/ObtenerSerieGenarado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtNroDocumentoSuplementos').val(msg);
            $('#txtNroDocumentoSuplementos').addClass("disabled");
            $('#txtNroDocumentoSuplementos').removeAttr("disabled");
            if (msg.length > 1) {
                $('#txtNroDocumentoSuplementos').attr("disabled", "disabled");
            }
        }
    });
}

function ValidarGenerarSerieComprobanteSuplementos() {


    $.ajax({

        type: "POST",
        url: "/gestionce/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.GenerarSerie) {
                generarSerieComprobanteSuplementos();
            } else {
                $('#txtNroDocumentoSuplementos').removeClass("disabled");
            }
        }
    });
}

function SEGListarUsuarioResponsableSuplementos() {


    $("#ddlVendedorSuplementos").kendoDropDownList({
        filter: "startswith",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="ddlVendedorSuplementos_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsableSuplementos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            for (var i = 0; i < msg.length; i++) {
                                msg[i].NombreCompleto = msg[i].NombreCompleto.toUpperCase();
                            }
                            options.success(msg);

                            $('#ddlVendedorSuplementos').data("kendoDropDownList").value(User.toUpperCase());
                        }
                    });
                }
            }
        }
    });
}

function ListarPedidosDelSocio() {
    $('#gridSuplementos').find('tbody').html('');
    var codigoCliente = $('#infoCodigo').html() == '' ? 0 : $('#infoCodigo').html();

    $.ajax({
        data: '{"codSocio":"' + codigoCliente + '"}',
        type: "POST",
        url: "/gestionce/ListarPedidosDelSocio",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            for (var i = 0; i < msg.length; i++) {
                agregarSuplementos(msg[i].Cantidad, msg[i].CodigoProducto, msg[i].Descripcion, msg[i].PrecioUnitario, msg[i].Tipo, msg[i].Codigo, msg[i].Debe);
            }
        }
    });
}

var codigoArray = new Array();
codigoArray.push(0);

function agregarSuplementos(cantidad, codigoProducto, nombreProducto, precioVenta, tipoProducto, CodigoPedido, debe) {

    var indexFila = parseInt(codigoArray.length) + 1;
    codigoArray.push(indexFila);
    var fila = '';
    var classFila = $('#gridSuplementos tbody tr:last').attr('class');
    classFila = classFila == '' || classFila == undefined ? 'k-alt' : '';
    //tipoProducto  1 = Producto, 2 = Membresia, 3 = Libre, 4 = producto elaborado , 5 = eventos , 6 = suplementos
    if (tipoProducto == 6 || tipoProducto == 9) {
        fila = '<tr  id="TRfilaSuplementos_' + indexFila + '" >' +
            '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoPedido + '" ></td>' +
            '<td style="font-size:11px;margin-left: 3px;" >' + nombreProducto + '</td>' +
            '<td><input type="text" disabled id="CantidadSuplementos_' + indexFila + '" class="k-textbox"  value="' + cantidad + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
            '<td><input type="text" disabled data_precioSuplementos="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementos_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:12px;color:blue;"></td>' +
            '<td><input type="text" id="ImporteSuplementos_' + indexFila + '" class="k-textbox" disabled value="' + (parseFloat(precioVenta).toFixed(2) * parseInt(cantidad)).toFixed(2) + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td><input type="text" id="DebeSuplementos_' + indexFila + '" class="k-textbox" disabled value="' + parseFloat(debe).toFixed(2) + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td><input type="text" id="AcuentaSuplementos_' + indexFila + '" class="k-textbox" value="0.00" style="width:68px;font-size:15px;font-weight:bold;" ></input></td>' +
            '</tr>';
    } else if (tipoProducto == 20 || tipoProducto == 30) {
        fila = '<tr  id="TRfilaSuplementos_' + indexFila + '"   >' +
            '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoPedido + '" ></td>' +
            '<td style="font-size:11px;margin-left: 3px;color:rgb(0 117 255);" >' + nombreProducto + ' - deuda</td>' +
            '<td><input type="text" disabled id="CantidadSuplementos_' + indexFila + '" class="k-textbox"  value="' + cantidad + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
            '<td><input type="text" disabled data_precioSuplementos="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementos_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:12px;color:blue;"></td>' +
            '<td><input type="text" id="ImporteSuplementos_' + indexFila + '" class="k-textbox" disabled value="' + (parseFloat(precioVenta).toFixed(2) * parseInt(cantidad)).toFixed(2) + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td><input type="text" id="DebeSuplementos_' + indexFila + '" class="k-textbox" disabled value="' + parseFloat(debe).toFixed(2) + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td><input type="text" id="AcuentaSuplementos_' + indexFila + '" class="k-textbox" value="0.00" style="width:68px;font-size:15px;font-weight:bold;" ></input></td>' +
            '</tr>';
    } else {
        fila = '<tr id="TRfilaSuplementos_' + indexFila + '"  >' +
            '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoPedido + '" ></td>' +
            '<td style="font-size:11px;margin-left: 3px;" >' + nombreProducto + '</td>' +
            '<td><input type="text" disabled id="CantidadSuplementos_' + indexFila + '" class="k-textbox"  value="' + cantidad + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
            '<td><input type="text" disabled data_precioSuplementos="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementos_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:13px;color:blue;"></td>' +
            '<td><input type="text" id="ImporteSuplementos_' + indexFila + '" class="k-textbox" disabled value="' + (parseFloat(precioVenta).toFixed(2) * parseInt(cantidad)).toFixed(2) + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td><input type="text" id="DebeSuplementos_' + indexFila + '" class="k-textbox" disabled value="' + parseFloat(debe).toFixed(2) + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td style="padding:2px;"><input type="text" id="AcuentaSuplementos_' + indexFila + '" class="k-textbox" value="0.00" style="width:68px;font-size:15px;font-weight:bold;" ></input></td>' +
            '</tr>';
    }

    if (fila.length > 0) {
        $('#gridSuplementos').find('tbody').append(fila);

        $("input[id*='AcuentaSuplementos_']").keyup(function () {

            var totalAcuenta = $(this).val();
            var filaDDD = $(this).attr('id').split('_')[1];
            var totalDebe = $('#DebeSuplementos_' + filaDDD).val();

            if (parseFloat(totalDebe) < parseFloat(totalAcuenta)) {
                $.bootstrapGrowl("El a cuenta no puede ser mayor al debe del producto", { type: 'primary', width: 'auto' });
                $(this).val('0.00');
            }

        });
    }
}

function guardarPagofiado() {

    $('button[type="button"]').attr("disabled", true);
    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");

    var index = 0;
    var codigoSalida = 0;
    var codigoCliente = $('#infoCodigo').html() == '' ? 0 : $('#infoCodigo').html();
    var razonSocial_Sr = '';//$('#txtNombre').val() + ' ' + $('#txtApellido').val();
    var nroDNIRUC = '';//$('#txtDni').val();
    var direccion = '';//$('#txtDireccion').val();
    var fechaSalida = kendo.toString($("#txtFechaSalidaSuplementos").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    var codigoSubTipoDocumento = $('#ddlSubTipoDocumentoSuplementos').data("kendoDropDownList").value();
    var nroDocumento = $('#txtNroDocumentoSuplementos').val();
    var subTotal = 0;
    var igv = 0;
    var total = 0;
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobanteSuplementos[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });

    var xml = "";
    xml += "<ds>";
    $('#gridSuplementos tbody tr').each(function () {
        var codigoProducto = $(this).find("td").eq(0).find("input").val().split('|')[0];
        var tipo = $(this).find("td").eq(0).find("input").val().split('|')[1];
        var CodigoPedido = $(this).find("td").eq(0).find("input").val().split('|')[2];
        var cantidad = $(this).find("td").eq(2).find("input").val();
        var descripcion = $(this).find("td").eq(1).html();
        var precioUnitario = $(this).find("td").eq(3).find("input").val();
        var importe = $(this).find("td").eq(4).find("input").val();
        var acuenta = $(this).find("td").eq(6).find("input").val();
        var debe = $(this).find("td").eq(5).find("input").val();
        xml += "<d>";
        xml += "<a>" + codigoProducto + "</a>"; //CodigoProducto
        xml += "<b>" + tipo + "</b>"; //tipo
        xml += "<c>" + cantidad + "</c>"; //Cantidad
        xml += "<e>" + descripcion + "</e>"; //Descripcion
        xml += "<f>" + precioUnitario + "</f>"; //PrecioUnitario
        xml += "<g>" + importe + "</g>"; //Importe
        xml += "<i>" + $('#ddlVendedorSuplementos').data("kendoDropDownList").value() + "</i>"; //CodigoPedido
        xml += "<j>" + CodigoPedido + "</j>"; //CodigoPedido
        xml += "<k>" + acuenta + "</k>"; //acuenta
        xml += "<k>" + debe + "</k>"; //debe
        xml += "</d>";
    });

    xml += "</ds>";

    //FORMA DE PAGO
    var xmlFormaPago = "";
    xmlFormaPago += "<ds>";
    var FP_TipoMoneda = 1;
    var FP_Monto = 0;
    var FP_TipoCambio = 0;
    var FP_NroBoucher = $('#txtTextoBaucher1Suplementos').val();
    var FP_FormaPago = "";
    $('input[name="orderBoxFormaPago[]"]:checked').each(function () {
        FP_FormaPago += $(this).val();
    });
    var FP_SubFormaPago = "";
    if (FP_FormaPago == 1) {
        FP_SubFormaPago = 0;
    } else if (FP_FormaPago == 2) {
        $('input[name="orderBoxOpcion1[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 3) {
        $('input[name="orderBoxOpcion1[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 4) {
        $('input[name="orderBoxOpcion2[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 5) {
        FP_SubFormaPago = 0;
    }
    //formaPago1
    xmlFormaPago += "<d>";
    xmlFormaPago += "<a>" + FP_TipoMoneda + "</a>"; //Tipo moneda
    xmlFormaPago += "<b>" + FP_Monto + "</b>"; //Monto
    xmlFormaPago += "<c>" + FP_TipoCambio + "</c>"; //Tipo cambio
    xmlFormaPago += "<e>" + FP_FormaPago + "</e>"; //Forma de pago
    xmlFormaPago += "<f>" + FP_SubFormaPago + "</f>"; //sub Forma de pago
    xmlFormaPago += "<g>" + FP_NroBoucher + "</g>"; //Nro Boucher
    xmlFormaPago += "</d>";
    xmlFormaPago += "</ds>";
    var xmlCuotas = "";

    if (tk != '' && tkLatitude != '' && tkLongitude != '') {

        $.ajax({
            data: '{"codigoSalida":"' + codigoSalida + '","CodigoSocio":"' + codigoCliente + '","RazonSocial_Sr":"' + razonSocial_Sr + '","RUC_DNI":"' + nroDNIRUC + '","Direccion":"' + direccion + '","FechaVenta":"' + fechaSalida +
                '","CodigoTipoComprobante":"' + codigoTipoDocumento + '","CodigoSubTipoComprobante":"' + codigoSubTipoDocumento + '","NroComprobante":"' + nroDocumento +
                '","NroTarjeta":"' + FP_NroBoucher + '","TipoMoneda":"' + 0 + '","FormaPago":"' + FP_FormaPago +
                '","SubTotal":"' + subTotal + '","IGV":"' + igv + '","TotalNeto":"' + total +
                '","tipoCambio":"' + 0 + '","listaDetalle":"' + xml + '","listaFormaPago":"' + xmlFormaPago +
                '","TotalDolares":"' + 0 + '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '","listaCuotas":"' + xmlCuotas + '"}',
            type: "POST",
            url: "/gestionce/GuardarPagoFiado",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $.bootstrapGrowl("El pago se ha realizado correctamente.", { type: 'success', width: 'auto' });
            }, complete: function () {
                $('button[type="button"]').attr("disabled", false);
                BuscarInformacionSociosPorCodigo(codigoCliente);
                $('#myModalverMasPagarFiado').hide('fast');
            }
        });
    } else {
        alert("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias");
        $(document).empty();
    }
}

function uspActualizarMenbresiasFechaInicio() {

    $('button[type="button"]').attr("disabled", true);
    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");




    var codigoMembresia = $('#hdCodigoMembresiaAsistencia').val();

    if (tk != '' && tkLatitude != '' && tkLongitude != '') {
        $.ajax({
            data: '{"codigoMembresia":"' + codigoMembresia + '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '"}',
            type: "POST",
            url: "/gestionce/uspActualizarMenbresiasFechaInicio",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg > 0) {
                    $.bootstrapGrowl("Los datos se han guardado correctamente.", { type: 'success', width: 'auto' });
                } else {
                    $.bootstrapGrowl("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias", { type: 'danger', width: 'auto' });
                }
            }, complete: function () {
                $('button[type="button"]').attr("disabled", false);
                var codigoCliente = $('#infoCodigo').html() == '' ? 0 : $('#infoCodigo').html();
                BuscarInformacionSociosPorCodigo(codigoCliente);
                $('#myModalConfirmacioUpdateInicio').hide('fast');
            }
        });
    } else {
        alert("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias");
        $(document).empty();
    }
}

function TipoPago() {
    var valor = "";
    $('input[name="orderBoxFormaPago[]"]:checked').each(function () {
        valor += $(this).val();
    });
    if (valor == 1) {
        document.getElementById("divOpcionTarjetas1").style.display = 'none';
        document.getElementById("divOpcionTarjetas2").style.display = 'none';
    } else if (valor == 2) {
        document.getElementById("divOpcionTarjetas1").style.display = '';
        document.getElementById("divOpcionTarjetas2").style.display = 'none';
    } else if (valor == 3) {
        document.getElementById("divOpcionTarjetas1").style.display = '';
        document.getElementById("divOpcionTarjetas2").style.display = 'none';
    } else if (valor == 4) {
        document.getElementById("divOpcionTarjetas1").style.display = 'none';
        document.getElementById("divOpcionTarjetas2").style.display = '';
    } else if (valor == 5) {
        document.getElementById("divOpcionTarjetas1").style.display = 'none';
        document.getElementById("divOpcionTarjetas2").style.display = 'none';
    }
}

//verMasPagarFiado
function verMasPagar_DeudaSuplementoRopa() {
    $('#myModalverMasPagarDeudaSuplementoRopa').show('fast');
    $('#DivCerrarmyModalverMasPagarDeudaSuplementoRopa').click(function () {
        $('#myModalverMasPagarDeudaSuplementoRopa').hide('fast');
    });

    uspListarDeudasSuplementoRopaDelSocio();

    if ($('#hdFlagVentana_DeudaSuplementoRopa').val() == '0') {
        var todayDate = new Date();
        $("#txtFechaSalida_DeudaSuplementoRopa").kendoDatePicker();
        $('#txtFechaSalida_DeudaSuplementoRopa').data("kendoDatePicker").value(todayDate);
        $("#txtFechaSalida_DeudaSuplementoRopa").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });

        //$('#txtTotalPagado').keyup(function () {
        //    var total = $('#lblTotal').html();
        //    var TotalPagando = $('#txtTotalPagado').val();
        //    var debe = parseFloat(total) - parseFloat(TotalPagando);
        //    $('#lblTotalDebe').html(debe);
        //    calcularTotalNeto();
        //});

        //$('#txtTotalPagado').click(function () {
        //    $('#txtTotalPagado').select();
        //});

        ListaTipoDocumentoContrato_DeudaSuplementosRopa();
        SEGListarUsuarioResponsable_DeudaSuplementosRopa();

        $('#lblRealizarPago_DeudaSuplementoRopa').click(function () {

            var count = 0;
            $('#gridDeudaSuplementoRopa tbody tr').each(function () {
                count += 1;
            });
            if (count > 0) {
                if ($('#infoCodigo').html() == '0') {
                    $.bootstrapGrowl(" Debe buscar un socio, es importante para un membresia", { type: 'danger', width: 'auto' });
                } else {
                    //guardarPagofiado();
                }
            } else {
                $.bootstrapGrowl("No hay contratos a pagar en la lista.", { type: 'danger', width: 'auto' });
            }
        });
    }

    $('#hdFlagVentana_DeudaSuplementoRopa').val('1');

}

function SEGListarUsuarioResponsable_DeudaSuplementosRopa() {


    $("#ddlVendedor_DeudaSuplementoRopa").kendoDropDownList({
        filter: "startswith",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="ddlVendedor_DeudaSuplementoRopa_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsableSuplementos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            for (var i = 0; i < msg.length; i++) {
                                msg[i].NombreCompleto = msg[i].NombreCompleto.toUpperCase();
                            }
                            options.success(msg);

                            $('#ddlVendedor_DeudaSuplementoRopa').data("kendoDropDownList").value(User.toUpperCase());
                        }
                    });
                }
            }
        }
    });
}

function ListaTipoDocumentoContrato_DeudaSuplementosRopa() {
    var TipoDocumento = "";
    $('input[name="orderBoxComprobanteSuplementos[]"]:checked').each(function () {
        TipoDocumento += $(this).val();
    });
    listaSubTipoDocumentoSerie_DeudaSuplementosRopa(TipoDocumento);
}

function listaSubTipoDocumentoSerie_DeudaSuplementosRopa(CodTipoDocumento) {


    var ddlSubTipoDocumento = $("#ddlSubTipoDocumento_DeudaSuplementoRopa").kendoDropDownList({
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodTipoDocumento":"' + CodTipoDocumento + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSubTipoDocumentosPorTipoDocumento",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        },
                        complete: function () {
                            generarSerieComprobante_DeudaSuplementoRopa();
                            ValidarGenerarSerieComprobante_DeudaSuplementoRopa();
                        }
                    });
                }
            }
        },
        change: function () {
            ValidarGenerarSerieComprobante_DeudaSuplementoRopa();
        }
    }).data("kendoDropDownList");
}

function generarSerieComprobante_DeudaSuplementoRopa() {
    var Documento = "";
    $('input[name="orderBoxComprobante_DeudaSuplementoRopa[]"]:checked').each(function () {
        Documento += $(this).val();
    });
    var TipoDocumento = Documento;
    var SubTipoDocumento = $("#ddlSubTipoDocumento_DeudaSuplementoRopa").data('kendoDropDownList').value();


    $.ajax({
        data: '{"tipoDocumento":"' + TipoDocumento + '","subTipoDocumento":"' + SubTipoDocumento + '"}',
        type: "POST",
        url: "/gestionce/ObtenerSerieGenarado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtNroDocumento_DeudaSuplementoRopa').val(msg);
            $('#txtNroDocumento_DeudaSuplementoRopa').addClass("disabled");
            $('#txtNroDocumento_DeudaSuplementoRopa').removeAttr("disabled");
            if (msg.length > 1) {
                $('#txtNroDocumento_DeudaSuplementoRopa').attr("disabled", "disabled");
            }
        }
    });
}

function ValidarGenerarSerieComprobante_DeudaSuplementoRopa() {


    $.ajax({

        type: "POST",
        url: "/gestionce/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.GenerarSerie) {
                generarSerieComprobante_DeudaSuplementoRopa();
            } else {
                $('#txtNroDocumento_DeudaSuplementoRopa').removeClass("disabled");
            }
        }
    });
}

function uspListarDeudasSuplementoRopaDelSocio() {
    $('#gridDeudaSuplementoRopa').find('tbody').html('');


    var codigoCliente = $('#infoCodigo').html() == '' ? 0 : $('#infoCodigo').html();
    $.ajax({
        data: '{"CodigoSocio":"' + codigoCliente + '"}',
        type: "POST",
        url: "/gestionce/uspListarDeudasSuplementoRopaDelSocio",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            for (var i = 0; i < msg.length; i++) {
                agregar_DedudasSuplementosRopa(msg[i].CodigoProducto, msg[i].Descripcion, msg[i].PrecioVenta, msg[i].Cantidad, msg[i].Tipo, msg[i].CodigoDetalle);
            }
        }
    });
}

function agregar_DedudasSuplementosRopa(codigoProducto, nombreProducto, precioVenta, cantidad, tipoProducto, CodigoDetalle) {

    var indexFila = parseInt(codigoArray.length) + 1;
    codigoArray.push(indexFila);
    var fila = '';
    var classFila = $('#gridDeudaSuplementoRopa tbody tr:last').attr('class');
    classFila = classFila == '' || classFila == undefined ? 'k-alt' : '';
    //tipoProducto  1 = Producto, 2 = Membresia, 3 = Libre, 4 = producto elaborado , 5 = eventos , 6 = suplementos
    fila = '<tr  id="TRfilaSuplementos_' + indexFila + '" >' +
        '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoDetalle + '" ></td>' +
        '<td style="font-size:11px;margin-left: 3px;color:rgb(0 117 255);" >' + nombreProducto + ' - deuda</td>' +
        '<td><input type="text" id="CantidadSuplementos_' + indexFila + '" class="k-textbox"  value="' + 1 + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
        '<td><input type="text" data_precioSuplementos="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementos_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:12px;color:blue;"></td>' +
        '<td><input type="text" id="ImporteSuplementos_' + indexFila + '" class="k-textbox" disabled value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
        '<td><input type="text" id="AcuentaSuplementos_' + indexFila + '" class="k-textbox" value="0.00" style="width:68px;font-size:13px;font-weight:bold;" ></td>' +
        '</tr>';

    if (fila.length > 0) {
        $('#gridDeudaSuplementoRopa').find('tbody').append(fila);

        $("input[id*='AcuentaSuplementos_']").keyup(function () {

            var totalAcuenta = $(this).val();
            var filaDDD = $(this).attr('id').split('_')[1];
            var totalDebe = $('#ImporteSuplementos_' + filaDDD).val();

            if (parseFloat(totalDebe) < parseFloat(totalAcuenta)) {
                $.bootstrapGrowl("El a cuenta no puede ser mayor al importe del producto", { type: 'primary', width: 'auto' });
                $(this).val('0.00');
            }
        });
    }
}

function GuardarVentaDeudasSuplementosRopa() {
    $('button[type="button"]').attr("disabled", true);
    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");




    var index = 0;
    var codigoSalida = 0;
    var codigoCliente = $('#infoCodigo').html() == '' ? 0 : $('#infoCodigo').html();
    var razonSocial_Sr = '';
    var nroDNIRUC = '';
    var direccion = '';
    var fechaSalida = kendo.toString($("#txtFechaSalida_DeudaSuplementoRopa").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    var codigoSubTipoDocumento = $('#ddlSubTipoDocumento_DeudaSuplementoRopa').data("kendoDropDownList").value();
    var nroDocumento = $('#txtNroDocumento_DeudaSuplementoRopa').val();
    var subTotal = 0;
    var igv = 0;
    var total = 0;
    var TotalAporte = 0;
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobante_DeudaSuplementoRopa[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });
    if (codigoTipoDocumento == 1) {
        var subTotal = '0.00';
        var igv = '0.00';
        var total = '0.00';
    } else {
        var subTotal = '0.00';
        var igv = '0.00';
        var total = '0.00';
    }
    var xml = "";
    xml += "<ds>";
    $('#gridDeudaSuplementoRopa tbody tr').each(function () {
        var codigoProducto = $(this).find("td").eq(0).find("input").val().split('|')[0];
        var tipo = $(this).find("td").eq(0).find("input").val().split('|')[1];
        var CodigoDetalle = $(this).find("td").eq(0).find("input").val().split('|')[2];
        var cantidad = $(this).find("td").eq(2).find("input").val();
        var descripcion = $(this).find("td").eq(1).html();
        var precioUnitario = $(this).find("td").eq(3).find("input").val();
        var importe = $(this).find("td").eq(4).find("input").val();
        var acuenta = $(this).find("td").eq(5).find("input").val();
        xml += "<d>";
        xml += "<a>" + codigoProducto + "</a>"; //CodigoProducto
        xml += "<b>" + tipo + "</b>"; //tipo
        xml += "<c>" + cantidad + "</c>"; //Cantidad
        xml += "<e>" + descripcion + "</e>"; //Descripcion
        xml += "<f>" + precioUnitario + "</f>"; //PrecioUnitario
        xml += "<g>" + importe + "</g>"; //Importe
        xml += "<i>" + CodigoDetalle + "</i>"; //CodigoDetalle
        xml += "<j>" + acuenta + "</j>"; //acuenta
        xml += "</d>";
    });

    xml += "</ds>";
    //FORMA DE PAGO
    var xmlFormaPago = "";
    xmlFormaPago += "<ds>";
    var FP_TipoMoneda = 1;
    var FP_Monto = '0.00';
    var FP_TipoCambio = 0;
    var FP_NroBoucher = $('#txtTextoBaucher1_DeudaSuplementoRopa').val();
    var FP_FormaPago = "";
    $('input[name="orderBoxFormaPago_DeudaSuplementoRopa[]"]:checked').each(function () {
        FP_FormaPago += $(this).val();
    });
    var FP_SubFormaPago = "";
    if (FP_FormaPago == 1) {
        FP_SubFormaPago = 0;
    } else if (FP_FormaPago == 2) {
        $('input[name="orderBoxOpcion1_DeudaSuplementoRopa[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 3) {
        $('input[name="orderBoxOpcion1_DeudaSuplementoRopa[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 4) {
        $('input[name="orderBoxOpcion2_DeudaSuplementoRopa[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 5) {
        FP_SubFormaPago = 0;
    }
    //formaPago1
    xmlFormaPago += "<d>";
    xmlFormaPago += "<a>" + FP_TipoMoneda + "</a>"; //Tipo moneda
    xmlFormaPago += "<b>" + FP_Monto + "</b>"; //Monto
    xmlFormaPago += "<c>" + FP_TipoCambio + "</c>"; //Tipo cambio
    xmlFormaPago += "<e>" + FP_FormaPago + "</e>"; //Forma de pago
    xmlFormaPago += "<f>" + FP_SubFormaPago + "</f>"; //sub Forma de pago
    xmlFormaPago += "<g>" + FP_NroBoucher + "</g>"; //Nro Boucher
    xmlFormaPago += "</d>";
    xmlFormaPago += "</ds>";
    var Vendedor = $('#ddlVendedor_DeudaSuplementoRopa').data("kendoDropDownList").value();

    if (tk != '' && tkLatitude != '' && tkLongitude != '') {
        $.ajax({
            data: '{"codigoSalida":"' + codigoSalida + '","CodigoSocio":"' + codigoCliente + '","RazonSocial_Sr":"' + razonSocial_Sr + '","RUC_DNI":"' + nroDNIRUC + '","Direccion":"' + direccion + '","FechaVenta":"' + fechaSalida +
                '","CodigoTipoComprobante":"' + codigoTipoDocumento + '","CodigoSubTipoComprobante":"' + codigoSubTipoDocumento + '","NroComprobante":"' + nroDocumento +
                '","NroTarjeta":"' + FP_NroBoucher + '","TipoMoneda":"' + 0 + '","FormaPago":"' + FP_FormaPago +
                '","SubTotal":"' + subTotal + '","IGV":"' + igv + '","TotalNeto":"' + total +
                '","tipoCambio":"' + 0 + '","listaDetalle":"' + xml + '","listaFormaPago":"' + xmlFormaPago +
                '","Vendedor":"' + Vendedor +
                '","TotalDolares":"' + 0 + '","TotalAporte":"' + TotalAporte + '","SubFormaPago":"' + FP_SubFormaPago + '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '"}',
            type: "POST",
            url: "/gestionce/GuardarVentaDeudasSuplementosRopa",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.split('|')[0] > 0) {
                    $.bootstrapGrowl("El pagó se realizó correctamente.", { type: 'success', width: 'auto' });
                    $('button[type="button"]').attr("disabled", false);
                    BuscarInformacionSociosPorCodigo(codigoCliente);
                    $('#myModalverMasPagarDeudaSuplementoRopa').hide('fast');

                    $('#gridDeudaSuplementoRopa tbody tr').each(function () {
                        $(this).remove();
                    });
                } else {
                    $.bootstrapGrowl("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias", { type: 'danger', width: 'auto' });
                }
                $('button[type="button"]').attr("disabled", false);
            }, complete: function () {


            }
        });
    } else {
        alert("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias");
        $(document).empty();
    }
}

function NuevoVentaDiario() {

    event_escribiendo();
    $('#myModalPagarDiario').show("fast");
    $('#DivCerrarmyModalPagarDiario').click(function () {
        $('#myModalPagarDiario').hide("fast");
        event_noescribiendo();
    });

    //para la venta de diarios
    var flagExisteCodigoCliente = false;
    //alert($('#hdCodigo').val());
    if ($('#hdCodigo').val() > 0) {
        $('#chkVenderDiarioConCliente').prop('checked', true);
        document.getElementById('divValidadorVentaDiarioConDatosCliente').style.display = '';
        document.getElementById('divValidadorVentaDiarioSinDatosCliente').style.display = 'none';
        $('#txtCliente_VentaDiario_Vista').val('CODIGO: ' + $('#hdCodigo').val() + ' - ' + $('#lblNombreCompleto').val());
    } else {
        $('#chkVenderDiarioConCliente').prop('checked', false);
        document.getElementById('divValidadorVentaDiarioConDatosCliente').style.display = 'none';
        document.getElementById('divValidadorVentaDiarioSinDatosCliente').style.display = '';
    }

    $('#btnRemoverClasediaria').click(function () {
        QuitardelaListaClasediaria();
    });

    if ($('#hdflagVentaDiario').val() == '0') {
        $('#hdflagVentaDiario').val('1');

        $('#chkVenderDiarioConCliente').click(function () {
            if ($('#chkVenderDiarioConCliente').prop('checked')) {
                if ($('#hdCodigo').val() > 0) {
                    $('#chkVenderDiarioConCliente').prop('checked', true);
                    document.getElementById('divValidadorVentaDiarioConDatosCliente').style.display = '';
                    document.getElementById('divValidadorVentaDiarioSinDatosCliente').style.display = 'none';
                    $('#txtCliente_VentaDiario_Vista').val('CODIGO: ' + $('#hdCodigo').val() + ' - ' + $('#lblNombreCompleto').val());
                } else {
                    alert("Falta buscar a un cliente, es obligatorio para vender a un cliente.");
                    $('#chkVenderDiarioConCliente').prop('checked', false);
                }
            } else {
                $('#chkVenderDiarioConCliente').prop('checked', false);
                document.getElementById('divValidadorVentaDiarioConDatosCliente').style.display = 'none';
                document.getElementById('divValidadorVentaDiarioSinDatosCliente').style.display = '';
            }
        });

        uspListarDiarios();

        var todayDate = new Date();
        $("#txtFechaSalida_Diario").kendoDatePicker();
        $('#txtFechaSalida_Diario').kendoDatePicker({
            value: todayDate,
            change: function () {
                var fechaInicioFiltro = $("#txtFechaSalida_Diario").data('kendoDatePicker').value();
                var mmfechaInicioFiltro = fechaInicioFiltro.getDate();

                var hoy = new Date();
                if (hoy < fechaInicioFiltro) {
                    $('#txtFechaSalida_Diario').data("kendoDatePicker").value(hoy);
                    $.bootstrapGrowl("La fecha no puede ser mayor al dia actual.", { type: 'primary', width: 'auto' });
                }

            }
        });

        $("#txtFechaSalida_Diario").mask("99/99/9999", { placeholder: "dd/mm/yyyy" });

        ListaTipoDocumento_Diario();
        SEGListarUsuarioResponsable_Diario();


        $('#lblRealizarPago_Diario').click(function () {

            var total = $('#lblTotal_Diario').html();
            var aporte = $('#txtTotalPagado_Diario').val();
            var count = 0;
            $('#gridDiarios tbody tr').each(function () {
                count += 1;
            });
            if (count > 0) {
                if (parseFloat(total) < parseFloat(aporte)) {
                    $.bootstrapGrowl("El aporte no puede mayor al total de la venta.", { type: 'danger', width: 'auto' });
                } else if (aporte <= 0) {
                    $.bootstrapGrowl("El aporte no puede ser menor o igual 0.", { type: 'danger', width: 'auto' });
                } else {
                    guardarVentaDiario();
                }

            } else {
                $.bootstrapGrowl("No hay productos en la lista.", { type: 'danger', width: 'auto' });
            }
        });
    }
}

function uspListarDiarios() {

    var controlHtml = "";
    $('#divDiarioVenta').html("");
    $.ajax({
        type: "POST",
        url: "/gestionce/uspListarDiarios",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var Contador = 0;
            controlHtml = '<table width="100%" border="0" style="text-align: center;Color:#000;">';
            for (var i = 0; i < msg.length; i++) {

                if (msg[i].Cantidad > 0) {

                    controlHtml += '<tr><td style="font-size:11px; width:25%;"><div id="data_SeleccionarDiarioVenta2_' + i + '" data_SeleccionarDiarioVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarDiario(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 4 + ', ' + 0 + ')"  style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                        '<table width="100%"><tr>' +
                        '<td width="90%" style="font-weight:bold;">' +
                        msg[i].Descripcion.substr(0, 17) +
                        '</td>' +
                        '<td width="10%">' +
                        '<b style="text-align:right;color:rgb(0 117 255);font-size: 20px;">' + parseFloat(msg[i].PrecioVenta).toFixed(2) + ' </b>' +
                        '</td>' +
                        '</tr></table>' +
                        '</div></td>';
                }

            }
            controlHtml += '</tr>';

            $('#divDiarioVenta').append(controlHtml + '</table>');
        }
    });
}

function SeleccionarDiario(indice, codigo, precioVenta, tipo, CodigoDetalle, cantidad) {
    descripcion = $("#data_SeleccionarDiarioVenta2_" + indice).attr('data_SeleccionarDiarioVenta2');
    agregarDiarios(cantidad, codigo, descripcion, precioVenta, tipo, CodigoDetalle);
    $.bootstrapGrowl("Se agrego el producto correctamente", { type: 'success', width: 'auto' });
}

function agregarDiarios(cantidad, codigoProducto, nombreProducto, precioVenta, tipoProducto, CodigoDetalle) {

    var indexFila = parseInt(codigoArray.length) + 1;
    codigoArray.push(indexFila);
    var fila = '';
    var classFila = $('#gridDiarios tbody tr:last').attr('class');
    classFila = classFila == '' || classFila == undefined ? 'k-alt' : '';
    //tipoProducto  1 = Producto, 2 = Membresia, 3 = Libre, 4 = producto elaborado , 5 = eventos , 6 = suplementos
    fila = '<tr  id="TRfilaDiario_' + indexFila + '"  >' +
        '<td><input checked style="margin-left: 2px;" type="checkbox" id="chkProductoDiario_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoDetalle + '" ></td>' +
        '<td style="font-size:11px;margin-left: 3px;" >' + nombreProducto + '</td>' +
        '<td><label id="imgDisminuirDiario_' + indexFila + '" onclick="DisminuirStockDiario(CantidadDiario_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;" >-</label></td>' +
        '<td align="center"><input type="text" id="CantidadDiario_' + indexFila + '" class="k-textbox"  value="' + 1 + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
        '<td><label id="imgAumentarDiario_' + indexFila + '" onclick="AumentarStockDiario(CantidadDiario_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;" >+</label></td>' +
        '<td align="center"><input type="text" data_precioDiario="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioDiario_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;background-color: transparent;font-size:13px;color:blue;"></td>' +
        '<td align="center"><input type="text" id="ImporteDiario_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
        '<td align="center" style="padding:2px;"><input type="text" id="AcuentaDiario_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;font-size:13px;border-color:transparent;background-color: transparent;font-weight:bold;" ></td>' +
        '</tr>';

    if (fila.length > 0) {
        $('#gridDiarios').find('tbody').append(fila);
        CalcularImporteDiario(indexFila);
        $("input[id*='CantidadDiario_']").keyup(function () {
            var numRow = $(this).attr('id').split('_')[1];
            CalcularImporteDiario(numRow);
        });
        $("input[id*='PrecioDiario_']").keyup(function () {
            var numRow = $(this).attr('id').split('_')[1];
            CalcularImporteDiario(numRow);
        });
        $("input[id*='AcuentaDiario_']").keyup(function () {

            var totalAcuentaMax = 0;
            $('#gridDiarios tbody tr').each(function () {
                totalAcuentaMax += parseFloat($(this).find("td").eq(7).find("input").val());
            });

            var totalSaldo = 0;
            var total = parseFloat($('#lblTotal_Diario').html());
            totalSaldo = total - totalAcuentaMax;

            if (totalSaldo < 0) {
                $.bootstrapGrowl("El a cuenta no puede ser mayor al total a pagar del producto", { type: 'primary', width: 'auto' });
            }

            $('#txtTotalPagado_Diario').val(totalAcuentaMax.toFixed(2));
            $('#lblTotalDebe_Diario').html(parseFloat(totalSaldo).toFixed(2));

        });
    }
}

function ListaTipoDocumento_Diario() {
    var TipoDocumento = "";
    $('input[name="orderBoxComprobante_Diario[]"]:checked').each(function () {
        TipoDocumento += $(this).val();
    });
    listaSubTipoDocumentoSerie_Diario(TipoDocumento);
    if (TipoDocumento == 1) {/*Aplica solo a facturas*/
        $('#div_facturacion_nro_contribuyente').css('display', 'block');
        $('#div_facturacion_direccion_contribuyente').css('display', 'block');
    }
    else {
        $('#div_facturacion_nro_contribuyente').css('display', 'none');
        $('#div_facturacion_direccion_contribuyente').css('display', 'none');
    }
    calcularTotalNeto_Diario();
}

function calcularTotalNeto_Diario() {
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobante_Diario[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });
    var SubTotal = 0;
    var igv = 0;
    var total = 0;
    var totalMonto = 0;
    var totalAcuenta = 0;
    var totaSaldo = 0;
    $('#gridDiarios tbody tr').each(function () {
        totalAcuenta += parseFloat($(this).find("td").eq(7).find("input").val());
        totalMonto += parseFloat($(this).find("td").eq(6).find("input").val());
    });

    $('#lblTotal_Diario').html(totalMonto.toFixed(2));
    $('#txtTotalPagado_Diario').val(totalAcuenta.toFixed(2));
    $('#lblTotalDebe_Diario').html(totalAcuenta.toFixed(2) - totalAcuenta.toFixed(2));

    total = $('#txtTotalPagado_Diario').val() == "" ? 0.00 : $('#txtTotalPagado_Diario').val();
    if (codigoTipoDocumento == 1) { //factura
        igv = parseFloat(total) * 0.18;
        SubTotal = parseFloat(total) - igv;

    } else if (codigoTipoDocumento == 2 || codigoTipoDocumento == 3) { //boleta o otros
        total = total;
        SubTotal = 0.00;
        igv = 0.00;

    }
    $('#txtSubTotalTop_Diario').html(SubTotal.toFixed(2));
    $('#txtIGVTop_Diario').html(igv.toFixed(2));
}

function listaSubTipoDocumentoSerie_Diario(CodTipoDocumento) {

    var ddlSubTipoDocumento = $("#ddlSubTipoDocumento_Diario").kendoDropDownList({
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodTipoDocumento":"' + CodTipoDocumento + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSubTipoDocumentosPorTipoDocumento",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        },
                        complete: function () {

                            ValidarGenerarSerieComprobante_Diario();
                        }
                    });
                }
            }
        },
        change: function () {
            ValidarGenerarSerieComprobante_Diario();
        }
    }).data("kendoDropDownList");
}

function SEGListarUsuarioResponsable_Diario() {

    $("#ddlVendedor_Diario").kendoDropDownList({
        filter: "startswith",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="ddlVendedor_Diario_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsableSuplementos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            for (var i = 0; i < msg.length; i++) {
                                msg[i].NombreCompleto = msg[i].NombreCompleto.toUpperCase();
                            }
                            options.success(msg);
                            var User = getCookie('_Usuario_Business');
                            $('#ddlVendedor_Diario').data("kendoDropDownList").value(User.toString().toUpperCase());

                        }
                    });
                }
            }
        }
    });
}

function DisminuirStockDiario(id, index, cantidad) {
    var valor = $(id).val();
    valor = parseInt(valor) - 1;
    if (valor == 0) {
        valor = 1;
    } else {
        $(id).val(valor);
        CalcularImporteDiario(index);
    }
}

function AumentarStockDiario(id, index, cantidad) {
    var valor = $(id).val();
    valor = parseInt(valor) + 1;
    if (valor > cantidad) {
        valor = 1;
        $.bootstrapGrowl("La cantidad ingresada no puede superar al stock actual", { type: 'danger', width: 'auto' });
    } else {
        $(id).val(valor);
        CalcularImporteDiario(index);
    }
}

function CalcularImporteDiario(codigo) {
    var precio = parseFloat($('#PrecioDiario_' + codigo).val());

    var cantidad = $('#CantidadDiario_' + codigo).val() == '' ? '0' : $('#CantidadDiario_' + codigo).val();
    var importe = precio.toFixed(2) * parseInt(cantidad);
    $('#ImporteDiario_' + codigo).val(importe.toFixed(2));
    $('#AcuentaDiario_' + codigo).val(importe.toFixed(2));
    calcularTotalNeto_Diario();
}

function removerDiarios() {

    $('#txtTextoBaucher1_Diario').val('');

    $('#txtTotalPagado_Diario').val('0.00');

    $('#lblTotalDebe_Diario').html('0.00');

    calcularTotalNeto_Diario();

}

function ObtenerUnidadNegocio() {

    $.ajax({
        type: "POST",
        url: "/gestionce/uspSeguridadObtenerUnidadNegocio",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.CodigoUnidadNegocio == null) {
                alert("Comuniquese con Centro de Ayuda");
            } else {
                $('#imgLogoSede').attr('src', msg.Logo);
                $('#modalConfirmarReserva_norma').html(msg.ReservasNormativa);
                $('#modalConfirmarReserva_nota').html(msg.ReservasNotas);
                $('#hdflag_controlarObligatorioMarcarIngresoSalaClase').val(msg.ObligatorioMarcarIngresoSalaClase);

                if (msg.ObligatorioMarcarIngresoSalaClase) {
                    document.getElementById('divControlMarcarAsistenciaNormal').style.display = 'none';
                    document.getElementById('divControlMarcarAsistenciaNormal_CheckAutomatico').style.display = 'none';
                    document.getElementById('divControlMarcarAsistencia_SeleccionarClaseHorario').style.display = 'block';
                    CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy();
                } else {
                    document.getElementById('divControlMarcarAsistenciaNormal').style.display = 'block';
                    document.getElementById('divControlMarcarAsistenciaNormal_CheckAutomatico').style.display = 'block';
                    document.getElementById('divControlMarcarAsistencia_SeleccionarClaseHorario').style.display = 'none';
                }
            }
        }
    });
}

function NuevoVenderProductos() {

    if ($('#hdflagModalVentaSuplemento').val() == '0') {

        SEGListarUsuarioResponsable_Suplementos();
        ListaTipoDocumentoSuplementos_Modal();

        $('#btnRemoverSuplementos').click(function () {
            QuitardelaListaSuplementos();
        });

        $('#lblRealizarFiado_Modal').click(function () {
            $('#myModalConfirmarFiado').show('fast');
        });

        $('#DivCerrarConfirmarFiado,#btnCancelarConfirmarFiado').click(function () {
            $('#myModalConfirmarFiado').hide('fast');
        });

        $('#btnGrabarConfirmarFiado').click(function () {
            var count = 0;
            $('#gridSuplementosCarrito tbody tr').each(function () {
                count += 1;
            });

            if (count > 0) {
                if ($('#hdCodigo').val() == 0) {
                    $.bootstrapGrowl("Debe buscar ó registrar un socio, es importante.", { type: 'danger', width: 'auto' });
                } else {
                    guardarFiadoSuplementos();
                }
            } else {
                $.bootstrapGrowl("No hay productos en la lista.", { type: 'danger', width: 'auto' });
            }
        });

        ClickDivVentaProductosModal(3);
        uspListarProductoBuscadorPorNombre();
    }

    $('#myModalVentaSuplementos').show('fast');

    $('#DivCerrarmyModalVentaSuplementos').click(function () {
        $('#myModalVentaSuplementos').hide('fast');
    });

    $('#hdflagModalVentaSuplemento').val('1');
}

function SEGListarUsuarioResponsable_Suplementos() {

    $("#ddlVendedorSuplementos_Modal").kendoDropDownList({
        filter: "startswith",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var nombre = $('input[aria-owns="ddlVendedorSuplementos_Modal_listbox"]').val();
                    $.ajax({
                        data: '{"filtro":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/SEGListarUsuarioResponsableSuplementos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            for (var i = 0; i < msg.length; i++) {
                                msg[i].NombreCompleto = msg[i].NombreCompleto.toUpperCase();
                            }
                            options.success(msg);

                            $('#ddlVendedorSuplementos_Modal').data("kendoDropDownList").value(User.toUpperCase());
                        }
                    });
                }
            }
        }
    });
}

function ListaTipoDocumentoSuplementos_Modal() {
    var TipoDocumento = "";
    $('input[name="orderBoxComprobanteSuplementos_Modal[]"]:checked').each(function () {
        TipoDocumento += $(this).val();
    });
    listaSubTipoDocumentoSerie_SuplementoModal(TipoDocumento);
    calcularTotalNetoSuplementos();
}

function removerSuplementos_Modal() {

    $('#txtTextoBaucher1Suplementos').val('');

    $('#txtTotalPagadoSuplementos').val('0.00');

    $('#lblTotalDebeSuplementos').html('0.00');

    calcularTotalNetoSuplementos();

}

function listaSubTipoDocumentoSerie_SuplementoModal(CodTipoDocumento) {


    var ddlSubTipoDocumento = $("#ddlSubTipoDocumentoSuplementos_Modal").kendoDropDownList({
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        dataSource: {
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodTipoDocumento":"' + CodTipoDocumento + '"}',
                        type: "POST",
                        url: "/gestionce/ListarSubTipoDocumentosPorTipoDocumento",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        },
                        complete: function () {
                            generarSerieComprobante_SuplementoModal();
                            ValidarGenerarSerieComprobante_SuplementoModal();
                        }
                    });
                }
            }
        },
        change: function () {
            ValidarGenerarSerieComprobante_SuplementoModal();
        }
    }).data("kendoDropDownList");
}

function generarSerieComprobante_SuplementoModal() {
    var Documento = "";
    $('input[name="orderBoxComprobanteSuplementos_Modal[]"]:checked').each(function () {
        Documento += $(this).val();
    });
    var TipoDocumento = Documento;
    var SubTipoDocumento = $("#ddlSubTipoDocumentoSuplementos_Modal").data('kendoDropDownList').value();


    $.ajax({
        data: '{"tipoDocumento":"' + TipoDocumento + '","subTipoDocumento":"' + SubTipoDocumento + '"}',
        type: "POST",
        url: "/gestionce/ObtenerSerieGenarado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $('#txtNroDocumentoSuplementos_Modal').val(msg);
            $('#txtNroDocumentoSuplementos_Modal').addClass("disabled");
            $('#txtNroDocumentoSuplementos_Modal').removeAttr("disabled");
            if (msg.length > 1) {
                $('#txtNroDocumentoSuplementos_Modal').attr("disabled", "disabled");
            }
        }
    });
}

function ValidarGenerarSerieComprobante_SuplementoModal() {


    $.ajax({

        type: "POST",
        url: "/gestionce/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.GenerarSerie) {
                generarSerieComprobante_SuplementoModal();
            } else {
                $('#txtNroDocumentoSuplementos_Modal').removeClass("disabled");
            }
        }
    });
}

function TipoPagoSuplementos_Modal() {
    var valor = "";
    $('input[name="orderBoxFormaPagoSuplementos_Modal[]"]:checked').each(function () {
        valor += $(this).val();
    });
    if (valor == 1) {
        document.getElementById("divOpcionTarjetas1Suplementos_Modal").style.display = 'none';
        document.getElementById("divOpcionTarjetas2Suplementos_Modal").style.display = 'none';
    } else if (valor == 2) {
        document.getElementById("divOpcionTarjetas1Suplementos_Modal").style.display = '';
        document.getElementById("divOpcionTarjetas2Suplementos_Modal").style.display = 'none';
    } else if (valor == 3) {
        document.getElementById("divOpcionTarjetas1Suplementos_Modal").style.display = '';
        document.getElementById("divOpcionTarjetas2Suplementos_Modal").style.display = 'none';
    } else if (valor == 4) {
        document.getElementById("divOpcionTarjetas1Suplementos_Modal").style.display = 'none';
        document.getElementById("divOpcionTarjetas2Suplementos_Modal").style.display = '';
    } else if (valor == 5) {
        document.getElementById("divOpcionTarjetas1Suplementos_Modal").style.display = 'none';
        document.getElementById("divOpcionTarjetas2Suplementos_Modal").style.display = 'none';
    }
}

function ClickDivVentaProductosModal(tipo) {

    if (tipo == 2) {

        document.getElementById("DivProductosVentaModal").style.display = '';
        document.getElementById("DivSuplementoVentaModal").style.display = 'none';
        document.getElementById("DivRopaVentaModal").style.display = 'none';
        uspListarProductoPorCategoriaVenta(0);
    } else if (tipo == 3) {

        document.getElementById("DivProductosVentaModal").style.display = 'none';
        document.getElementById("DivSuplementoVentaModal").style.display = '';
        document.getElementById("DivRopaVentaModal").style.display = 'none';
        uspListarSuplementosVentaPorCategoria(0);
    } else if (tipo == 4) {

        document.getElementById("DivProductosVentaModal").style.display = 'none';
        document.getElementById("DivSuplementoVentaModal").style.display = 'none';
        document.getElementById("DivRopaVentaModal").style.display = '';
        uspListarRopasVentas();
    }

}

function uspListarSuplementosVentaPorCategoria(CodigoCategoria) {

    var controlHtml = "";

    $('#DivSuplementoVentaModal').html("");
    $('#DivSuplementoVentaModal').empty();
    $.ajax({
        data: '{"CodigoCategoria":"' + CodigoCategoria + '"}',
        type: "POST",
        url: "/gestionce/uspListarSuplementosVentasPorCategoria",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            debugger;
            var Contador = 0;
            controlHtml = '<table width="100%" border="0" style="text-align: center;Color:#000;">';
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].Cantidad > 0) {
                    Contador++;
                    if (Contador == 1) {

                        controlHtml += '<tr><td style="font-size:11px; width: 20%;"><div id="data_SeleccionarSuplementosVenta2_' + i + '" data_SeleccionarSuplementosVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoSuplemento + ', ' + msg[i].PrecioVenta + ', ' + 6 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '</table>' +
                            '</div></td>';
                    } else if (Contador == 2) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarSuplementosVenta2_' + i + '" data_SeleccionarSuplementosVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoSuplemento + ', ' + msg[i].PrecioVenta + ', ' + 6 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '</table>' +
                            '</div></td>';
                    } else if (Contador == 3) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarSuplementosVenta2_' + i + '" data_SeleccionarSuplementosVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoSuplemento + ', ' + msg[i].PrecioVenta + ', ' + 6 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +

                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '</table>' +
                            '</div></td>';
                        Contador = 0;
                    } else if (Contador == 4) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarSuplementosVenta2_' + i + '" data_SeleccionarSuplementosVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoSuplemento + ', ' + msg[i].PrecioVenta + ', ' + 6 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '</table>' +
                            '</div></td>';
                    } else if (Contador == 5) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarSuplementosVenta2_' + i + '" data_SeleccionarSuplementosVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoSuplemento + ', ' + msg[i].PrecioVenta + ', ' + 6 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +

                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '</table>' +
                            '</div></td>';

                    }
                }

            }

            if (Contador == 1 || Contador == 2 || Contador == 3 || Contador == 4) {
                controlHtml += '</tr>';
            }

            $('#DivSuplementoVentaModal').html(controlHtml + '</table>');
        }, complete: function () {

        }
    });
}

function SeleccionarSuplementosVenta2(indice, codigo, precioVenta, tipo, CodigoDetalle, cantidad) {
    //var flag = $('#hdflagSuplemento').val();
    //if (flag == 0) {
    var descripcion = '';
    if (tipo == 1) {
        descripcion = $("#data_SeleccionarProductoVenta2_" + indice).attr('data_SeleccionarProductoVenta2');
    } else if (tipo == 6) {
        descripcion = $("#data_SeleccionarSuplementosVenta2_" + indice).attr('data_SeleccionarSuplementosVenta2');
    } else if (tipo == 9) {
        descripcion = $("#data_SeleccionarRopaVenta2_" + indice).attr('data_SeleccionarRopaVenta2');
    } else if (tipo == 4) {
        descripcion = $("#data_SeleccionarDiarioVenta2_" + indice).attr('data_SeleccionarDiarioVenta2');
    }

    agregarSuplementosModal(cantidad, codigo, descripcion, precioVenta, tipo, CodigoDetalle);
    $.bootstrapGrowl("Se agrego el producto correctamente", { type: 'success', width: 'auto' });
    //} else {
    //    $.bootstrapGrowl("seleccione el boton Nueva Venta y luego selecione el suplemento", { type: 'primary', width: 'auto' });
    //    $('#txtTotalPagadoSuplementos').val("0.00");
    //    $('#lblTotalDebeSuplementos').html("0.00");
    //    $('#lblTotalSuplementos').html("0.00");
    //    $('#gridSuplementosDetalle').data('kendoGrid').refresh();
    //}
}

function agregarSuplementosModal(cantidad, codigoProducto, nombreProducto, precioVenta, tipoProducto, CodigoDetalle) {


    var indexFila = parseInt(codigoArray.length) + 1;
    codigoArray.push(indexFila);
    var fila = '';
    var classFila = $('#gridSuplementosCarrito tbody tr:last').attr('class');
    classFila = classFila == '' || classFila == undefined ? 'k-alt' : '';
    //tipoProducto  1 = Producto, 2 = Membresia, 3 = Libre, 4 = producto elaborado , 5 = eventos , 6 = suplementos
    if (tipoProducto == 6 || tipoProducto == 9) {
        fila = '<tr  id="TRfilaSuplementos_' + indexFila + '"  >' +
            '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoDetalle + '" ></td>' +
            '<td style="font-size:11px;margin-left: 3px;" >' + nombreProducto + '</td>' +
            '<td><label id="imgDisminuirSuplementos_' + indexFila + '" onclick="DisminuirStockSuplementos(CantidadSuplementos_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;" >-</label></td>' +
            '<td><input type="text" id="CantidadSuplementosModal_' + indexFila + '" class="k-textbox"  value="' + 1 + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
            '<td><label id="imgAumentarSuplementos_' + indexFila + '" onclick="AumentarStockSuplementos(CantidadSuplementosModal_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;" >+</label></td>' +
            '<td><input type="text" data_precioSuplementosModal="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementosModal_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:12px;color:blue;"></td>' +
            '<td><input type="text" id="ImporteSuplementosModal_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td><input type="text" id="AcuentaSuplementosModal_' + indexFila + '" class="k-textbox" value="0.00" style="width:68px;font-size:13px;font-weight:bold;" ></td>' +
            '</tr>';
    } else if (tipoProducto == 20 || tipoProducto == 30) {
        fila = '<tr  id="TRfilaSuplementos_' + indexFila + '"  >' +
            '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoDetalle + '" ></td>' +
            '<td style="font-size:11px;margin-left: 3px;color:rgb(0 117 255);" >' + nombreProducto + ' - deuda</td>' +
            '<td><label id="imgDisminuirSuplementos_' + indexFila + '" onclick="DisminuirStockSuplementos(CantidadSuplementos_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;" >-</label></td>' +
            '<td><input type="text" id="CantidadSuplementosModal_' + indexFila + '" class="k-textbox"  value="' + 1 + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
            '<td><label id="imgAumentarSuplementos_' + indexFila + '" onclick="AumentarStockSuplementos(CantidadSuplementosModal_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;" >+</label></td>' +
            '<td><input type="text" data_precioSuplementosModal="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementosModal_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:12px;color:blue;"></td>' +
            '<td><input type="text" id="ImporteSuplementosModal_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td><input type="text" id="AcuentaSuplementosModal_' + indexFila + '" class="k-textbox" value="0.00" style="width:68px;font-size:13px;font-weight:bold;" ></td>' +
            '</tr>';
    } else if (tipoProducto == 1) {
        fila = '<tr  id="TRfilaSuplementos_' + indexFila + '"    >' +
            '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoDetalle + '" ></td>' +
            '<td style="font-size:11px;margin-left: 3px;" >' + nombreProducto + '</td>' +
            '<td><label id="imgDisminuirSuplementos_' + indexFila + '" onclick="DisminuirStockSuplementos(CantidadSuplementos_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;"  >-</label></td>' +
            '<td><input type="text" id="CantidadSuplementosModal_' + indexFila + '" class="k-textbox"  value="' + 1 + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
            '<td><label id="imgAumentarSuplementos_' + indexFila + '"  onclick="AumentarStockSuplementos(CantidadSuplementosModal_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;"  >+</label></td>' +
            '<td><input type="text" data_precioSuplementosModal="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementosModal_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:13px;color:blue;"></td>' +
            '<td><input type="text" id="ImporteSuplementosModal_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td style="padding:2px;"><input type="text" id="AcuentaSuplementosModal_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;font-size:13px;border-color:transparent;background-color: transparent;font-weight:bold;" ></td>' +
            '</tr>';
    } else {
        fila = '<tr  id="TRfilaSuplementos_' + indexFila + '"    >' +
            '<td><input style="margin-left: 2px;" type="checkbox" id="chkProductoSuplementos_' + indexFila + '" value="' + codigoProducto + '|' + tipoProducto + '|' + CodigoDetalle + '" ></td>' +
            '<td style="font-size:11px;margin-left: 3px;" >' + nombreProducto + '</td>' +
            '<td><label id="imgDisminuirSuplementos_' + indexFila + '" onclick="DisminuirStockSuplementos(CantidadSuplementos_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;" >-</label></td>' +
            '<td><input type="text" id="CantidadSuplementosModal_' + indexFila + '" class="k-textbox"  value="' + 1 + '" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;margin-left: 2px;" ></td>' +
            '<td><label id="imgAumentarSuplementos_' + indexFila + '" onclick="AumentarStockDiarios(CantidadSuplementosModal_' + indexFila + ',' + indexFila + ',' + cantidad + ')"  style="width:23px;cursor:pointer;margin-left: 2px;"  >+</label></td>' +
            '<td><input type="text" data_precioSuplementosModal="' + parseFloat(precioVenta).toFixed(2) + '" id="PrecioSuplementosModal_' + indexFila + '" class="k-textbox" value="' + parseFloat(precioVenta).toFixed(2) + '" style="width:50px;border-color:transparent;background-color: transparent;font-size:13px;color:blue;"></td>' +
            '<td><input type="text" id="ImporteSuplementosModal_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;border-color:transparent;background-color: transparent;font-size:11px;" ></td>' +
            '<td style="padding:2px;"><input type="text" id="AcuentaSuplementosModal_' + indexFila + '" class="k-textbox" disabled value="0.00" style="width:68px;font-size:13px;border-color:transparent;background-color: transparent;font-weight:bold;" ></td>' +
            '</tr>';
    }

    if (fila.length > 0) {
        $('#gridSuplementosCarrito').find('tbody').append(fila);
        CalcularImporteSuplementos(indexFila);
        $("input[id*='CantidadSuplementosModal_']").keyup(function () {
            var numRow = $(this).attr('id').split('_')[1];
            CalcularImporteSuplementos(numRow);
        });
        $("input[id*='PrecioSuplementosModal_']").keyup(function () {
            var numRow = $(this).attr('id').split('_')[1];
            CalcularImporteSuplementos(numRow);
        });

        $("input[id*='AcuentaSuplementosModal_']").keyup(function () {

            var totalacuentamax = 0;
            $('#gridSuplementosCarrito tbody tr').each(function () {
                totalacuentamax += parseFloat($(this).find("td").eq(7).find("input").val());
            });

            var totalsaldo = 0;
            var total = parseFloat($('#lblTotalSuplementos_Modal').html());
            totalsaldo = total - totalacuentamax;

            if (totalsaldo < 0) {
                $.bootstrapgrowl("el a cuenta no puede ser mayor al total a pagar del producto", { type: 'primary', width: 'auto' });
            }

            $('#txtTotalPagadoSuplementos_Modal').val(totalacuentamax.toFixed(2));
            $('#lblTotalDebeSuplementos_Modal').html(parseFloat(totalsaldo).toFixed(2));
        });
    }
}

function DisminuirStockSuplementos(id, index, cantidad) {
    var valor = $(id).val();
    valor = parseInt(valor) - 1;
    if (valor == 0) {
        valor = 1;
    } else {
        $(id).val(valor);
        CalcularImporteSuplementos(index);
    }
}

function AumentarStockSuplementos(id, index, cantidad) {

    var valor = $(id).val();
    valor = parseInt(valor) + 1;
    if (valor > cantidad) {
        valor = 1;
        $.bootstrapGrowl("La cantidad ingresada no puede superar al stock actual", { type: 'danger', width: 'auto' });
    } else {
        $(id).val(valor);
        CalcularImporteSuplementos(index);
    }
}

function CalcularImporteSuplementos(codigo) {
    var precio = parseFloat($('#PrecioSuplementosModal_' + codigo).val());
    var precioReal = $('#PrecioSuplementosModal_' + codigo).attr('data_precioSuplementosModal');
    if (precio > parseFloat(precioReal)) {
        $('#PrecioSuplementosModal_' + codigo).val(precioReal);
        precio = precioReal;
        $('#ImporteSuplementosModal_' + codigo).val(precio);
        $('#AcuentaSuplementosModal_' + codigo).val(precio);
        calcularTotalNetoSuplementos();
    } else {
        var cantidad = $('#CantidadSuplementosModal_' + codigo).val() == '' ? '0' : $('#CantidadSuplementosModal_' + codigo).val();
        var importe = precio.toFixed(2) * parseInt(cantidad);
        $('#ImporteSuplementosModal_' + codigo).val(importe.toFixed(2));
        $('#AcuentaSuplementosModal_' + codigo).val(importe.toFixed(2));
        calcularTotalNetoSuplementos();
    }
}

function calcularTotalNetoSuplementos() {
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobanteSuplementos_Modal[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });
    var SubTotal = 0;
    var igv = 0;
    var total = 0;
    var totalMonto = 0;
    var totalAcuenta = 0;
    var totaSaldo = 0;
    //var flag = $('#hdflagSuplemento').val();
    //if (flag != 1) {
    $('#gridSuplementosCarrito tbody tr').each(function () {
        totalAcuenta += parseFloat($(this).find("td").eq(7).find("input").val());
        totalMonto += parseFloat($(this).find("td").eq(6).find("input").val());
    });

    $('#lblTotalSuplementos_Modal').html(totalMonto.toFixed(2));
    $('#txtTotalPagadoSuplementos_Modal').val(totalAcuenta.toFixed(2));
    $('#lblTotalDebeSuplementos_Modal').html(totalAcuenta.toFixed(2) - totalAcuenta.toFixed(2));
    //} else {

    //}

    total = $('#txtTotalPagadoSuplementos_Modal').val() == "" ? 0.00 : $('#txtTotalPagadoSuplementos_Modal').val();
    if (codigoTipoDocumento == 1) { //factura
        igv = parseFloat(total) * 0.18;
        SubTotal = parseFloat(total) - igv;
        document.getElementById("tdSubtotalSuplementos_Modal").style.display = '';
        document.getElementById("tdIGVSuplementos_Modal").style.display = '';
    } else if (codigoTipoDocumento == 2 || codigoTipoDocumento == 3) { //boleta o otros
        total = total;
        SubTotal = 0.00;
        igv = 0.00;
        document.getElementById("tdSubtotalSuplementos_Modal").style.display = 'none';
        document.getElementById("tdIGVSuplementos_Modal").style.display = 'none';
    }
    $('#txtSubTotalTopSuplementos_Modal').html(SubTotal.toFixed(2));
    $('#txtIGVTopSuplementos_Modal').html(igv.toFixed(2));
}

function uspListarProductoPorCategoriaVenta(CodigoCategoria) {

    var controlHtml = "";
    $('#DivProductosVentaModal').html("");
    $('#DivProductosVentaModal').empty();
    $.ajax({
        data: '{"CodigoCategoria":"' + CodigoCategoria + '"}',
        type: "POST",
        url: "/gestionce/uspListarProductoPorCategoriaVenta",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var Contador = 0;
            controlHtml = '<table width="100%" border="0" style="text-align: center;Color:#000;">';
            for (var i = 0; i < msg.length; i++) {
                if (msg[i].Cantidad > 0) {

                    Contador++;
                    if (Contador == 1) {

                        controlHtml += '<tr><td style="font-size:11px; width: 20%;"><div id="data_SeleccionarProductoVenta2_' + i + '" data_SeleccionarProductoVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 1 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/producto.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';
                    } else if (Contador == 2) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarProductoVenta2_' + i + '" data_SeleccionarProductoVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 1 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/producto.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';

                    } else if (Contador == 3) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarProductoVenta2_' + i + '" data_SeleccionarProductoVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 1 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/producto.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';
                        Contador = 0;
                    } else if (Contador == 4) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarProductoVenta2_' + i + '" data_SeleccionarProductoVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 1 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/producto.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';
                    } else if (Contador == 5) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarProductoVenta2_' + i + '" data_SeleccionarProductoVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 1 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/producto.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';

                    }
                }
            }
            if (Contador == 1 || Contador == 2 || Contador == 3 || Contador == 4) {
                controlHtml += '</tr>';
            }
            $('#DivProductosVentaModal').html(controlHtml + '</table>');
        }, complete: function () {

        }
    });
}

function uspListarRopasVentas() {


    var controlHtml = "";
    $('#DivRopaVentaModal').html("");
    $('#DivRopaVentaModal').empty();
    $.ajax({

        type: "POST",
        url: "/gestionce/uspListarRopasVentas",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            var Contador = 0;
            controlHtml = '<table width="100%" border="0" style="text-align: center;Color:#000;">';
            for (var i = 0; i < msg.length; i++) {

                if (msg[i].Cantidad > 0) {
                    Contador++;
                    if (Contador == 1) {

                        controlHtml += '<tr><td style="font-size:11px; width: 20%;"><div id="data_SeleccionarRopaVenta2_' + i + '" data_SeleccionarRopaVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 9 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/ropa.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';

                    } else if (Contador == 2) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarRopaVenta2_' + i + '" data_SeleccionarRopaVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 9 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/ropa.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';

                    } else if (Contador == 3) {
                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarRopaVenta2_' + i + '" data_SeleccionarRopaVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 9 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/ropa.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';

                    } else if (Contador == 4) {
                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarRopaVenta2_' + i + '" data_SeleccionarRopaVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 9 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/ropa.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';

                    } else if (Contador == 5) {

                        controlHtml += '<td style="font-size:11px; width: 20%;"><div id="data_SeleccionarRopaVenta2_' + i + '" data_SeleccionarRopaVenta2="' + msg[i].Descripcion + '" onclick="SeleccionarSuplementosVenta2(' + i + ', ' + msg[i].CodigoProducto + ', ' + msg[i].PrecioVenta + ', ' + 9 + ', ' + 0 + ', ' + msg[i].Cantidad + ')" style="width:98%;background-color:#fff;cursor:pointer;text-align:left;padding:2px 2px 0px 10px;margin:3px 3px 3px 3px;font-size: 15px;color:#000;border-radius:6px;border:1px #dddfe2 solid;">' +
                            '<table width="100%" border="0">' +
                            '<tr><td>' +
                            '<table width="100%"><tr>' +
                            '<td width="100%" style="text-align:center;">' +
                            '<label style="color:rgb(0 117 255);font-size: 16px;">' + msg[i].Cantidad + '</label>' +
                            '<img style="height:25px;width:25px;" src="../binfit-imagenes/clientes/ropa.png" >' +
                            '</td>' +
                            '</tr>' +
                            '<tr><td width="100%" style="font-size: 12px;text-align:center;">' +
                            msg[i].Descripcion.substr(0, 19) +
                            '</td>' +
                            '</tr></table>' +
                            '</td></tr>' +
                            '<tr><td>' +
                            '<center>+ Agregar</center>' +
                            '</td></tr></table>' +
                            '</div></td>';

                        Contador = 0;
                    }
                }

            }
            if (Contador == 1 || Contador == 2 || Contador == 3 || Contador == 4) {
                controlHtml += '</tr>';
            }

            $('#DivRopaVentaModal').html(controlHtml + '</table>');
        }
    });
}

function QuitardelaListaSuplementos() {
    $('#gridSuplementosCarrito tbody tr').each(function () {
        if ($(this).find("td").eq(0).find("input").is(':checked')) {
            $(this).remove();
        }
    });
    calcularTotalNetoSuplementos();
}

function QuitardelaListaClasediaria() {
    $('#gridDiarios tbody tr').each(function () {
        if ($(this).find("td").eq(0).find("input").is(':checked')) {
            $(this).remove();
        }
    });
    calcularTotalNeto_Diario();
}

function guardarVentaSuplementosModal() {
    $('button[type="button"]').attr("disabled", true);
    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");





    var index = 0;
    var codigoSalida = 0;
    var codigoCliente = $('#hdCodigo').val() == '' ? 0 : $('#hdCodigo').val();
    var razonSocial_Sr = $('#lblNombreCompleto').val() + ($('#hdCodigo').val() == '' ? 0 : (' (' + $('#hdCodigo').val() + ')'));
    var nroDNIRUC = $('#infoDNI').html();
    var direccion = $('#infoDireccion').val();
    var fechaSalida = kendo.toString($("#txtFechaSalidaSuplementos_Modal").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    var codigoSubTipoDocumento = $('#ddlSubTipoDocumentoSuplementos_Modal').data("kendoDropDownList").value();
    var nroDocumento = $('#txtNroDocumentoSuplementos_Modal').val();
    var subTotal = $('#txtSubTotalTopSuplementos_Modal').html();
    var igv = $('#txtIGVTopSuplementos_Modal').html();
    var total = $('#lblTotalSuplementos_Modal').html();
    var TotalAporte = $('#txtTotalPagadoSuplementos_Modal').val();
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobanteSuplementos_Modal[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });
    if (codigoTipoDocumento == 1) {
        var subTotal = $('#txtSubTotalTopSuplementos_Modal').html();
        var igv = $('#txtIGVTopSuplementos_Modal').html();
        var total = $('#lblTotalSuplementos_Modal').html();
    } else {
        var subTotal = '0.00';
        var igv = '0.00';
        var total = $('#lblTotalSuplementos_Modal').html();
    }
    var xml = "";
    xml += "<ds>";
    $('#gridSuplementosCarrito tbody tr').each(function () {
        var codigoProducto = $(this).find("td").eq(0).find("input").val().split('|')[0];
        var tipo = $(this).find("td").eq(0).find("input").val().split('|')[1];
        var CodigoDetalle = $(this).find("td").eq(0).find("input").val().split('|')[2];
        var cantidad = $(this).find("td").eq(3).find("input").val();
        var descripcion = $(this).find("td").eq(1).html();
        var precioUnitario = $(this).find("td").eq(5).find("input").val();
        var importe = $(this).find("td").eq(6).find("input").val();
        var acuenta = $(this).find("td").eq(7).find("input").val();
        xml += "<d>";
        xml += "<a>" + codigoProducto + "</a>"; //CodigoProducto
        xml += "<b>" + tipo + "</b>"; //tipo
        xml += "<c>" + cantidad + "</c>"; //Cantidad
        xml += "<e>" + descripcion + "</e>"; //Descripcion
        xml += "<f>" + precioUnitario + "</f>"; //PrecioUnitario
        xml += "<g>" + importe + "</g>"; //Importe
        xml += "<i>" + CodigoDetalle + "</i>"; //CodigoDetalle
        xml += "<j>" + acuenta + "</j>"; //acuenta
        xml += "</d>";
    });

    xml += "</ds>";
    //FORMA DE PAGO
    var xmlFormaPago = "";
    xmlFormaPago += "<ds>";
    var FP_TipoMoneda = 1;
    var FP_Monto = $('#txtTotalPagadoSuplementos_Modal').val();
    var FP_TipoCambio = 0;
    var FP_NroBoucher = $('#txtTextoBaucher1Suplementos_Modal').val();
    var FP_FormaPago = "";
    $('input[name="orderBoxFormaPagoSuplementos_Modal[]"]:checked').each(function () {
        FP_FormaPago += $(this).val();
    });
    var FP_SubFormaPago = "";
    if (FP_FormaPago == 1) {
        FP_SubFormaPago = 0;
    } else if (FP_FormaPago == 2) {
        $('input[name="orderBoxOpcion1Suplementos_Modal[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 3) {
        $('input[name="orderBoxOpcion1Suplementos_Modal[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 4) {
        $('input[name="orderBoxOpcion1Suplementos_Modal[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 5) {
        FP_SubFormaPago = 0;
    }
    //formaPago1
    xmlFormaPago += "<d>";
    xmlFormaPago += "<a>" + FP_TipoMoneda + "</a>"; //Tipo moneda
    xmlFormaPago += "<b>" + FP_Monto + "</b>"; //Monto
    xmlFormaPago += "<c>" + FP_TipoCambio + "</c>"; //Tipo cambio
    xmlFormaPago += "<e>" + FP_FormaPago + "</e>"; //Forma de pago
    xmlFormaPago += "<f>" + FP_SubFormaPago + "</f>"; //sub Forma de pago
    xmlFormaPago += "<g>" + FP_NroBoucher + "</g>"; //Nro Boucher
    xmlFormaPago += "</d>";
    xmlFormaPago += "</ds>";
    var Vendedor = $('#ddlVendedorSuplementos_Modal').data("kendoDropDownList").value();

    if (tk != '' && tkLatitude != '' && tkLongitude != '') {
        $.ajax({
            data: '{"codigoSalida":"' + codigoSalida + '","CodigoSocio":"' + codigoCliente + '","RazonSocial_Sr":"' + razonSocial_Sr + '","RUC_DNI":"' + nroDNIRUC + '","Direccion":"' + direccion + '","FechaVenta":"' + fechaSalida +
                '","CodigoTipoComprobante":"' + codigoTipoDocumento + '","CodigoSubTipoComprobante":"' + codigoSubTipoDocumento + '","NroComprobante":"' + nroDocumento +
                '","NroTarjeta":"' + FP_NroBoucher + '","TipoMoneda":"' + 0 + '","FormaPago":"' + FP_FormaPago +
                '","SubTotal":"' + subTotal + '","IGV":"' + igv + '","TotalNeto":"' + total +
                '","tipoCambio":"' + 0 + '","listaDetalle":"' + xml + '","listaFormaPago":"' + xmlFormaPago +
                '","Vendedor":"' + Vendedor +
                '","TotalDolares":"' + 0 + '","TotalAporte":"' + TotalAporte + '","SubFormaPago":"' + FP_SubFormaPago + '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '"}',
            type: "POST",
            url: "/gestionce/GuardarVentaSuplementos",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.split('|')[0] > 0) {

                    if (msg.split('|')[1] == 0) {

                        $.bootstrapGrowl("El pagó se realizó correctamente.", { type: 'success', width: 'auto' });
                        ListaTipoDocumentoSuplementos_Modal();
                        removerSuplementos_Modal();
                        ClickDivVentaProductosModal(3);
                        $('#gridSuplementosCarrito tbody tr').each(function () {
                            $(this).remove();
                        });
                        calcularTotalNetoSuplementos();
                    } else if (msg.split('|')[1] != 0) {
                        $.bootstrapGrowl("Imprimiendo comprobante.", { type: 'success', width: 'auto' });
                        /*obtener valor para impresion*/
                        var lista = new Array();
                        var _row = $('#gridSuplementosCarrito tbody tr');
                        for (var i = 0; i < _row.length; i++) {
                            var _fila = {};
                            _fila.Descripcion = $(_row[i]).find('td:nth-child(2)').text();
                            _fila.Cantidad = $(_row[i]).find('td:nth-child(4)').find('input').val();
                            _fila.PrecioUnitario = $(_row[i]).find('td:nth-child(6)').find('input').val();
                            _fila.Importe = $(_row[i]).find('td:nth-child(7)').find('input').val();
                            _fila.MontoAcuenta = $(_row[i]).find('td:nth-child(8)').find('input').val();
                            lista.push(_fila);
                        }
                        BuscarInformacionGeneralVentaSuplementosPorCodigo(msg.split('|')[0], lista);
                    }
                } else {
                    $.bootstrapGrowl("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias", { type: 'danger', width: 'auto' });
                }
                $('button[type="button"]').attr("disabled", false);
            }, complete: function () {

            }
        });
    } else {
        alert("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias");
        $(document).empty();
    }
}

function uspListarProductoBuscadorPorNombre() {

    $("#lblBuscadorProductos").kendoAutoComplete({
        dataTextField: "Descripcion",
        template: '<table border="0" style="width:100%;">' +
            '<tr>' +
            '<td style="width:100%;>' +
            '<div style="font-weight: bold;font-size: 12px;color:#:data.Color#;"> ' +
            '#:data.Descripcion# ' +
            '</div>' +
            '</td>' +
            '</tr>' +
            '<tr>' +
            '<td style="width:100%;>' +
            '<span class="k-state-default" >' +
            '<div style="font-weight: bold;font-size: 10px;color:#:data.Color#;"> ' +
            'Cantidad: #:data.Cantidad#,  Precio de Venta: #:data.PrecioVenta#, Detalle: #:data.Detalle#' +
            '</div>' +
            '</span>' +
            '</td>' +
            '</tr>' +
            '</table>',
        filter: "startswith",
        minLength: 3,
        height: 400,
        cache: false,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {
                    var valor = $('#lblBuscadorProductos').data('kendoAutoComplete').value();
                    var CodigoSocio = $('#hdCodigo').val() == '' ? 0 : $('#hdCodigo').val();

                    $.ajax({
                        type: "POST",
                        data: '{"CodigoSocio":"' + CodigoSocio + '","Descripcion":"' + valor + '"}',
                        url: "/gestionce/uspListarProductoBuscadorPorNombre",
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
        select: function (e) {
            var dataItem = this.dataItem(e.item.index());

            //agregarSuplementosModal(cantidad, codigoProducto, nombreProducto, precioVenta, tipoProducto, CodigoDetalle)
            //SeleccionarSuplementosVenta2(indice,codigo, precioVenta, tipo, CodigoDetalle,cantidad)
            //SeleccionarSuplementosVenta2(dataItem.CodigoProducto, dataItem.Descripcion, dataItem.PrecioVenta, dataItem.Cantidad, dataItem.Tipo, dataItem.CodigoDetalle);
            agregarSuplementosModal(dataItem.Cantidad, dataItem.CodigoProducto, dataItem.Descripcion, dataItem.PrecioVenta, dataItem.Tipo, dataItem.CodigoDetalle);
            $("#lblBuscadorProductos").val('');
            return false;
        }
    });

    //$("#lblBuscadorProductosCodBarras").keypress(function (e) {

    //    var keycode = (e.keyCode ? e.keyCode : e.which);
    //    if (keycode == '13') {
    //        var valor = $('#lblBuscadorProductosCodBarras').val();
    //        if (ConvertToDecimal(valor) > 0) {
    //            var CodigoSocio = $('#txtBuscarPorCodigoClienteContrato').val() == '' ? 0 : $('#txtBuscarPorCodigoClienteContrato').val();
    //            $.ajax({
    //                type: "POST",
    //                data: '{"CodigoSocio":"' + CodigoSocio + '","Descripcion":"' + valor + '"}',
    //                url: "Main.aspx/uspListarProductoBuscadorPorNombre",
    //                contentType: "application/json; charset=utf-8",
    //                dataType: "json",
    //                success: function (msg) {
    //                    if (msg.length > 0) {
    //                        var dataItem = msg[0];
    //                        SeleccionarSuplementosVenta(dataItem.CodigoProducto, dataItem.Descripcion, dataItem.PrecioVenta, dataItem.Cantidad, dataItem.Tipo, dataItem.CodigoDetalle);
    //                        $("#lblBuscadorProductosCodBarras").val('');
    //                    }
    //                    else {
    //                        $.bootstrapGrowl("no hay productos disponibles", { type: 'danger', width: 'auto' });
    //                        $("#lblBuscadorProductosCodBarras").val('');
    //                    }
    //                }, complete: function () {

    //                }
    //            });
    //        }

    //        e.stopPropagation();
    //        e.preventDefault();
    //        return false;
    //    }
    //});

}

function guardarFiadoSuplementos() {
    $('button[type="button"]').attr("disabled", true);
    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");





    var codigoCliente = $('#hdCodigo').val() == '' ? 0 : $('#hdCodigo').val();
    var fechaSalida = kendo.toString($("#txtFechaSalidaSuplementos_Modal").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    var Vendedor = $('#ddlVendedorSuplementos_Modal').data("kendoDropDownList").value();

    var index = 0;
    var codigoSalida = 0;

    var xml = "";
    xml += "<ds>";
    $('#gridSuplementosCarrito tbody tr').each(function () {
        var codigoProducto = $(this).find("td").eq(0).find("input").val().split('|')[0];
        var tipo = $(this).find("td").eq(0).find("input").val().split('|')[1];
        var CodigoDetalle = $(this).find("td").eq(0).find("input").val().split('|')[2];
        var cantidad = $(this).find("td").eq(3).find("input").val();
        var descripcion = $(this).find("td").eq(1).html();
        var precioUnitario = $(this).find("td").eq(5).find("input").val();
        var importe = $(this).find("td").eq(6).find("input").val();
        var acuenta = $(this).find("td").eq(7).find("input").val();
        xml += "<d>";
        xml += "<a>" + codigoProducto + "</a>"; //CodigoProducto
        xml += "<b>" + tipo + "</b>"; //tipo
        xml += "<c>" + cantidad + "</c>"; //Cantidad
        xml += "<e>" + descripcion + "</e>"; //Descripcion
        xml += "<f>" + precioUnitario + "</f>"; //PrecioUnitario
        xml += "<g>" + importe + "</g>"; //Importe
        xml += "<i>" + CodigoDetalle + "</i>"; //CodigoDetalle
        xml += "<j>" + acuenta + "</j>"; //acuenta
        xml += "</d>";
    });
    xml += "</ds>";

    //FORMA DE PAGO
    var xmlFormaPago = "";
    xmlFormaPago += "<ds>";
    var FP_TipoMoneda = 1;
    var FP_Monto = 0;
    var FP_TipoCambio = 0;
    var FP_NroBoucher = $('#txtTextoBaucher1Suplementos_Modal').val();
    var FP_FormaPago = "";
    $('input[name="orderBoxFormaPagoSuplementos_Modal[]"]:checked').each(function () {
        FP_FormaPago += $(this).val();
    });
    var FP_SubFormaPago = "";
    if (FP_FormaPago == 1) {
        FP_SubFormaPago = 0;
    } else if (FP_FormaPago == 2) {
        $('input[name="orderBoxOpcion1Suplementos_Modal[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 3) {
        $('input[name="orderBoxOpcion1Suplementos_Modal[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 4) {
        $('input[name="orderBoxOpcion1Suplementos_Modal[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 5) {
        FP_SubFormaPago = 0;
    }
    //formaPago1
    xmlFormaPago += "<d>";
    xmlFormaPago += "<a>" + FP_TipoMoneda + "</a>"; //Tipo moneda
    xmlFormaPago += "<b>" + FP_Monto + "</b>"; //Monto
    xmlFormaPago += "<c>" + FP_TipoCambio + "</c>"; //Tipo cambio
    xmlFormaPago += "<e>" + FP_FormaPago + "</e>"; //Forma de pago
    xmlFormaPago += "<f>" + FP_SubFormaPago + "</f>"; //sub Forma de pago
    xmlFormaPago += "<g>" + FP_NroBoucher + "</g>"; //Nro Boucher
    xmlFormaPago += "</d>";
    xmlFormaPago += "</ds>";
    var xmlCuotas = "";

    if (tk != '' && tkLatitude != '' && tkLongitude != '') {
        $.ajax({
            data: '{"codigoSalida":"' + codigoSalida + '","CodigoSocio":"' + codigoCliente + '","FechaVenta":"' + fechaSalida +
                '","listaDetalle":"' + xml + '","listaFormaPago":"' + xmlFormaPago + '","Vendedor":"' + Vendedor +
                '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '"}',
            type: "POST",
            url: "/gestionce/GuardarFiadosSuplementos",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                $.bootstrapGrowl("El Fiado se realizó correctamente.", { type: 'success', width: 'auto' });
                $('button[type="button"]').attr("disabled", false);
            }, complete: function () {

                $('#gridSuplementosCarrito tbody tr').each(function () {
                    $(this).remove();
                });
                $('#myModalConfirmarFiado').hide('fast');
                calcularTotalNetoSuplementos();
            }
        });
    } else {
        alert("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias");
        $(document).empty();
    }
}

function guardarVentaDiario() {

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var tk = getCookie("tkID");
    var tkLatitude = getCookie("tkLatitude");
    var tkLongitude = getCookie("tkLongitude");

    var index = 0;
    var codigoSalida = 0;
    var codigoCliente = 0;
    var razonSocial_Sr = $('#txtCliente_VentaDiario').val();
    if ($('#chkVenderDiarioConCliente').prop('checked')) {
        if ($('#hdCodigo').val() > 0) {
            codigoCliente = $('#hdCodigo').val();
            razonSocial_Sr = $('#lblNombreCompleto').val();
        } else {
            codigoCliente = 0;
            razonSocial_Sr = $('#txtCliente_VentaDiario').val();
        }
    } else {
        codigoCliente = 0;
        razonSocial_Sr = $('#txtCliente_VentaDiario').val();
    }


    var nroDNIRUC = '';
    var direccion = '';
    var fechaSalida = kendo.toString($("#txtFechaSalida_Diario").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    var codigoSubTipoDocumento = $('#ddlSubTipoDocumento_Diario').data("kendoDropDownList").value();
    var nroDocumento = $('#txtNroDocumento_Diario').val();
    var subTotal = $('#txtSubTotalTop_Diario').html();
    var igv = $('#txtIGVTop_Diario').html();
    var total = $('#lblTotal_Diario').html();
    var TotalAporte = $('#txtTotalPagado_Diario').val();
    var codigoTipoDocumento = "";
    $('input[name="orderBoxComprobante_Diario[]"]:checked').each(function () {
        codigoTipoDocumento += $(this).val();
    });
    if (codigoTipoDocumento == 1) {
        var subTotal = $('#txtSubTotalTop_Diario').html();
        var igv = $('#txtIGVTop_Diario').html();
        var total = $('#lblTotal_Diario').html();
    } else {
        var subTotal = '0.00';
        var igv = '0.00';
        var total = $('#lblTotal_Diario').html();
    }
    var xml = "";
    xml += "<ds>";
    $('#gridDiarios tbody tr').each(function () {

        var codigoProducto = $(this).find("td").eq(0).find("input").val().split('|')[0];
        var tipo = $(this).find("td").eq(0).find("input").val().split('|')[1];
        var CodigoDetalle = $(this).find("td").eq(0).find("input").val().split('|')[2];
        var cantidad = $(this).find("td").eq(3).find("input").val();
        var descripcion = $(this).find("td").eq(1).html();
        var precioUnitario = $(this).find("td").eq(5).find("input").val();
        var importe = $(this).find("td").eq(6).find("input").val();
        var acuenta = $(this).find("td").eq(7).find("input").val();
        xml += "<d>";
        xml += "<a>" + codigoProducto + "</a>"; //CodigoProducto
        xml += "<b>" + tipo + "</b>"; //tipo
        xml += "<c>" + cantidad + "</c>"; //Cantidad
        xml += "<e>" + descripcion + "</e>"; //Descripcion
        xml += "<f>" + precioUnitario + "</f>"; //PrecioUnitario
        xml += "<g>" + importe + "</g>"; //Importe
        xml += "<i>" + CodigoDetalle + "</i>"; //CodigoDetalle
        xml += "<j>" + acuenta + "</j>"; //acuenta
        xml += "</d>";
    });

    xml += "</ds>";
    //FORMA DE PAGO
    var xmlFormaPago = "";
    xmlFormaPago += "<ds>";
    var FP_TipoMoneda = 1;
    var FP_Monto = $('#txtTotalPagado_Diario').val();
    var FP_TipoCambio = 0;
    var FP_NroBoucher = $('#txtTextoBaucher1_Diario').val();
    var FP_FormaPago = "";
    $('input[name="orderBoxFormaPago_Diario[]"]:checked').each(function () {
        FP_FormaPago += $(this).val();
    });
    var FP_SubFormaPago = "";
    if (FP_FormaPago == 1) {
        FP_SubFormaPago = 0;
    } else if (FP_FormaPago == 2) {
        $('input[name="orderBoxFormaPago_Diario[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 3) {
        $('input[name="orderBoxFormaPago_Diario[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 4) {
        $('input[name="orderBoxFormaPago_Diario[]"]:checked').each(function () {
            FP_SubFormaPago += $(this).val();
        });
    } else if (FP_FormaPago == 5) {
        FP_SubFormaPago = 0;
    }
    //formaPago1
    xmlFormaPago += "<d>";
    xmlFormaPago += "<a>" + FP_TipoMoneda + "</a>"; //Tipo moneda
    xmlFormaPago += "<b>" + FP_Monto + "</b>"; //Monto
    xmlFormaPago += "<c>" + FP_TipoCambio + "</c>"; //Tipo cambio
    xmlFormaPago += "<e>" + FP_FormaPago + "</e>"; //Forma de pago
    xmlFormaPago += "<f>" + FP_SubFormaPago + "</f>"; //sub Forma de pago
    xmlFormaPago += "<g>" + FP_NroBoucher + "</g>"; //Nro Boucher
    xmlFormaPago += "</d>";
    xmlFormaPago += "</ds>";
    var Vendedor = $('#ddlVendedor_Diario').data("kendoDropDownList").value();

    if (tk != '' && tkLatitude != '' && tkLongitude != '') {
        $.ajax({
            data: '{"codigoSalida":"' + codigoSalida + '","CodigoSocio":"' + codigoCliente + '","RazonSocial_Sr":"' + razonSocial_Sr + '","RUC_DNI":"' + nroDNIRUC + '","Direccion":"' + direccion + '","FechaVenta":"' + fechaSalida +
                '","CodigoTipoComprobante":"' + codigoTipoDocumento + '","CodigoSubTipoComprobante":"' + codigoSubTipoDocumento + '","NroComprobante":"' + nroDocumento +
                '","NroTarjeta":"' + FP_NroBoucher + '","TipoMoneda":"' + 0 + '","FormaPago":"' + FP_FormaPago +
                '","SubTotal":"' + subTotal + '","IGV":"' + igv + '","TotalNeto":"' + total +
                '","tipoCambio":"' + 0 + '","listaDetalle":"' + xml + '","listaFormaPago":"' + xmlFormaPago +
                '","Vendedor":"' + Vendedor +
                '","TotalDolares":"' + 0 + '","TotalAporte":"' + TotalAporte + '","SubFormaPago":"' + FP_SubFormaPago + '","tk":"' + tk + '","latitud":"' + tkLatitude + '","longitud":"' + tkLongitude + '"}',
            type: "POST",
            url: "/gestionce/GuardarVentaDiario",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {

                if (msg.split('|')[0] > 0) {
                    if (msg.split('|')[1] == 0) {
                        $.bootstrapGrowl("La facturas y boletas electronicas no estan habilitadas.", { type: 'success', width: 'auto' });
                        $.bootstrapGrowl("El pago se ha realizado correctamente.", { type: 'success', width: 'auto' });

                        ListaTipoDocumento_Diario();
                        removerDiarios();
                        $('#gridDiarios tbody tr').each(function () {
                            $(this).remove();
                        });
                        $('#txtCliente_VentaDiario').val('');
                    } else if (msg.split('|')[1] == "1") {
                        $.bootstrapGrowl("La facturas y boletas electronicas no estan habilitadas.", { type: 'success', width: 'auto' });
                        $.bootstrapGrowl("Imprimiendo comprobante.....", { type: 'success', width: 'auto' });


                        console.log(msg.split('|')[0]);

                        //Imprimir_BuscarInformacionGeneralVentaRapida(msg.split('|')[0]);


                        var nrcomprobante = document.getElementById("txtNroDocumento_Diario").value;
                        if (nrcomprobante != "") {
                            generatepdfDiario(nrcomprobante);
                        }


                        // ImprimeVentaRapida()

                    }
                    else if (msg.split('|')[1] == "2") {
                        var urlPDFComprobante = msg.split('|')[2];

                        if (urlPDFComprobante != null) {
                            $.bootstrapGrowl("Imprimiendo comprobante.....", { type: 'success', width: 'auto' });
                            ImprimirPDFJs(urlPDFComprobante);
                        }
                        else {
                            $.bootstrapGrowl("No se emitio sunafact.", { type: 'error', width: 'auto' });
                        }

                        //ListaTipoDocumento_Diario();
                        //removerDiarios();
                        //$('#gridDiarios tbody tr').each(function () {
                        //    $(this).remove();
                        //});
                        //$('#txtCliente_VentaDiario').val('');


                        ListaTipoDocumento_Diario();
                        document.getElementById('loadMe').style.display = 'none';
                        removerDiarios();
                        $('#gridDiarios tbody tr').each(function () {
                            $(this).remove();
                        });
                        $('#txtCliente_VentaDiario').val('');
                        calcularTotalNeto_Diario();
                    }
                } else {
                    $.bootstrapGrowl("Error en el sistema", { type: 'danger', width: 'auto' });
                }
                $('button[type="button"]').attr("disabled", false);
            }, complete: function () {
                document.getElementById('loadMe').style.display = 'none';
            }
        });
    } else {
        alert("Su tiempo se agoto vuelva a ingresar al sistema por favor, su ingreso solo dura 24 horas. Gracias");
        $(document).empty();
    }
}



function generatepdfDiario(code) {

    $.ajax({
        data: JSON.stringify({ nrocomprobante: code }),
        type: "POST",
        url: "/gestionce/generatePDFSimple",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg.Status == 0) {

                //document.getElementById("btnVentaDiariaClose").click();

                //document.getElementById("divrenderPdfDiario").src = msg?.Message1;
                //document.getElementById("namepdftcmDiario").value = msg?.Message2;
                //document.getElementById("btnModalPdfDiario").click();


                //print directo
                printJS({
                    printable: msg?.Message1, type: 'pdf', showModal: true, onPrintDialogClose: function () {
                        removePdfDiario(msg?.Message2);
                    }
                })





            } else {
                $.bootstrapGrowl(msg.Message1, { type: 'danger', width: 'auto' });
            }

        }
    });
}

function removePdfDiario(name) {
    document.getElementById("txtCliente_VentaDiario").value = "";
    QuitardelaListaClasediaria();
    removerDiarios();


    if (name.trim().length > 0 && name != "") {
        $.ajax({
            data: JSON.stringify({ name: name }),
            type: "POST",
            url: "/gestionce/removePDF",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                console.log(msg)

            }
        });
    }

    //generate nrcomp
    generarSerieComprobante_Diario()

}

function sendEmailPDFDiario(e) {
    e.disabled = true;
    var to = document.getElementById("txtCorreoPDFDiario").value;
    var namepdf = document.getElementById("namepdftcmDiario").value;
    var data = {
        name: namepdf,
        to: to,
        asunto: "Comprobante"
    }

    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: "/gestionce/sendEmailVentaPdf",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg?.Status == 0) {
                $.bootstrapGrowl(msg?.Message1, { type: 'success', width: 'auto' });
                document.getElementById("txtCorreoPDFDiario").value = "";
                document.getElementById("namepdftcmDiario").value = "";

            } else {
                $.bootstrapGrowl(msg?.Message1, { type: 'danger', width: 'auto' });
            }
            e.disabled = false;

        }
    });
}



function Imprimir_BuscarInformacionGeneralVentaRapida(codigo) {

    $.ajax({
        data: '{"codigo":"' + codigo + '"}',
        type: "POST",
        url: "/gestionce/BuscarInformacionGeneralVentaPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg)

            $('#NombreEmpresaVenta_TicketDiario').html(msg.NombreEmpresa);
            $('#DistritoEmpresaVenta_TicketDiario').html(msg.DistritoEmpresa);
            $('#lblFrase_TicketDiario').html(msg.Frase);

            if (msg.RucEmpresa != '') {
                $('#RucEmpresaVenta_TicketDiario').html('RUC: ' + msg.RucEmpresa);
            } else {
                document.getElementById("RucEmpresaVenta_TicketDiario").style.display = 'none';
            }
            if (msg.TelefonoEmpresa != '') {
                $('#TelefonoEmpresaVenta_TicketDiario').html('Telf.: ' + msg.TelefonoEmpresa);
            } else {
                document.getElementById("TelefonoEmpresaVenta_TicketDiario").style.display = 'none';
            }
            if (msg.CorreoEmpresa != '') {
                $('#EmailEmpresaVenta_TicketDiario').html('Email: ' + msg.CorreoEmpresa);
            } else {
                document.getElementById("EmailEmpresaVenta_TicketDiario").style.display = 'none';
            }
            $('#FechaDeLaVentaVenta_TicketDiario').html(msg.DescFechaVenta);

            var NombreCliente = $('#txtCliente_VentaDiario').val();

            if ($('#chkVenderDiarioConCliente').prop('checked')) {
                if ($('#hdCodigo').val() > 0) {
                    codigoCliente = $('#hdCodigo').val();
                    razonSocial_Sr = $('#lblNombreCompleto').val();
                    $('#NombreCliente_TicketDiario').html('CODIGO: ' + codigoCliente + ' - ' + razonSocial_Sr);
                } else {
                    if (NombreCliente != '') {
                        $('#NombreCliente_TicketDiario').html(NombreCliente);
                    } else {
                        $('#NombreCliente_TicketDiario').html('____________________________');
                    }
                }
            } else {
                if (NombreCliente != '') {
                    $('#NombreCliente_TicketDiario').html(NombreCliente);
                } else {
                    $('#NombreCliente_TicketDiario').html('____________________________');
                }
            }



            $('#UsuarioCreacion_TicketDiario').html(msg.UsuarioCreacion.toUpperCase());

            var total = 0.00;
            var importe = 0.00;
            $('#gridDiarios tbody tr').each(function () {
                total += parseFloat($(this).find("td").eq(6).find("input").val());
                importe += parseFloat($(this).find("td").eq(7).find("input").val());
            });

            var igv = total * 0.18;
            var subTotal = (total - igv);

            $('#NroComprobanteVenta_TicketDiario').html(msg.NroComprobante);
            $('#SubTotalVentaVenta_TicketDiario').html(subTotal.toFixed(2));
            $('#IgvVentaVenta_TicketDiario').html(igv.toFixed(2));
            $('#TotalVenta_TicketDiario').html(total.toFixed(2));
            $('#ImporteVenta_TicketDiario').html(importe.toFixed(2));

            var FP_FormaPago = "";
            $('input[name="orderBoxFormaPago_Diario[]"]:checked').each(function () {
                FP_FormaPago += $(this).val();
            });
            if (FP_FormaPago == 1) {
                FP_FormaPago = "Total: efectivo";
            } else if (FP_FormaPago == 2) {
                FP_FormaPago = "Total: tarjeta debito";
            } else if (FP_FormaPago == 3) {
                FP_FormaPago = "Total: tarjeta credito";
            } else if (FP_FormaPago == 4) {
                FP_FormaPago = "Total: deposito";
            } else if (FP_FormaPago == 5) {
                FP_FormaPago = "Total: web";
            }

            $('#FormaPago_TicketDiario').html(FP_FormaPago);
            var tipoDocumento = "";
            $('input[name="orderBoxComprobante_Diario[]"]:checked').each(function () {
                tipoDocumento += $(this).val();
            });
            if (tipoDocumento == 1) {
                document.getElementById("ticketSubtotal_TicketDiario").style.display = '';
                document.getElementById("ticketIgv_TicketDiario").style.display = '';
            } else {
                document.getElementById("ticketSubtotal_TicketDiario").style.display = 'none';
                document.getElementById("ticketIgv_TicketDiario").style.display = 'none';
            }

            Imprimir_BuscarInformacionDetalleVentaRapida();
        }
    });
}

function Imprimir_BuscarInformacionDetalleVentaRapida() {

    $('#ProductosDeVenta_TicketDiario').html('');
    var controlHtml = "";
    $('#gridDiarios tbody tr').each(function () {

        var cantidad = $(this).find("td").eq(3).find("input").val();
        var descripcion = $(this).find("td").eq(1).html();
        var preciounit = $(this).find("td").eq(5).find("input").val();
        var importe = $(this).find("td").eq(6).find("input").val();

        controlHtml += '<tr>' +
            '<td style="text-align: center;font-size:medium;">' + cantidad + '</td>' +
            '<td style="text-align: left;font-size:medium;">' + descripcion + '</td>' +
            '<td style="text-align: center;font-size:medium;">' + preciounit + '</td>' +
            '<td style="text-align: center;font-size:medium;">' + importe + '</td>' +
            '</tr>';
    });

    $('#ProductosDeVenta_TicketDiario').append(controlHtml);
    ImprimeVentaRapida();
}




function ImprimeVentaRapida() {
    var ancho = screen.width - 10;
    var alto = screen.height - 75;
    var divToPrint = document.getElementById('divTicketVenta_TicketDiario');

    var newWin = window.open('', 'Print-Window', 'directories=no, border=0,scrollbars=yes,status=yes,toolbar=no,titlebar=no, resizable=no, menubar=no,width=' + ancho + ',height=' + alto + ',top=0,left=0');
    newWin.document.open();
    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
    newWin.document.close();
    setTimeout(function () { newWin.close(); }, 100);

    $.bootstrapGrowl("El pagó se realizó correctamente.", { type: 'success', width: 'auto' });
    ListaTipoDocumento_Diario();
    document.getElementById('loadMe').style.display = 'none';
    removerDiarios();
    $('#gridDiarios tbody tr').each(function () {
        $(this).remove();
    });
    $('#txtCliente_VentaDiario').val('');
    calcularTotalNeto_Diario();
}

function BuscarInformacionGeneralVentaSuplementosPorCodigo(codigo, listaItems) {

    $.ajax({
        data: '{"codigo":"' + codigo + '"}',
        type: "POST",
        url: "/gestionce/BuscarInformacionGeneralVentaPorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var Confi_Tipo_Comprobante = msg.Tipo_Conf_Comprobante;
            if (Confi_Tipo_Comprobante == 1) {
                $('#NombreEmpresaVenta_TicketProductos').html(msg.NombreEmpresa);
                $('#DistritoEmpresaVenta_TicketProductos').html(msg.DistritoEmpresa);
                $('#lblFraseVenta_TicketProductos').html(msg.Frase);
                $('#NombreEmpresaVenta_TicketProductos').html(msg.NombreEmpresa);
                if (msg.RucEmpresa != '') {
                    $('#RucEmpresaVenta_TicketProductos').html('RUC: ' + msg.RucEmpresa);
                } else {
                    document.getElementById("RucEmpresaVenta_TicketProductos").style.display = 'none';
                }
                if (msg.TelefonoEmpresa != '') {
                    $('#TelefonoEmpresaVenta_TicketProductos').html('Telf.: ' + msg.TelefonoEmpresa);
                } else {
                    document.getElementById("TelefonoEmpresaVenta_TicketProductos").style.display = 'none';
                }
                if (msg.CorreoEmpresa != '') {
                    $('#EmailEmpresaVenta_TicketProductos').html('Email: ' + msg.CorreoEmpresa);
                } else {
                    document.getElementById("EmailEmpresaVenta_TicketProductos").style.display = 'none';
                }
                $('#FechaDeLaVentaVenta_TicketProductos').html(msg.DescFechaVenta);
                //if (msg.DireccionDistritoCliente != '') {
                //    $('#DireccionDistritoCliente_TicketProductos').html(msg.DireccionDistritoCliente);
                //} else {
                //    $('#DireccionDistritoClienteTicketSuplementos').html('____________________________');
                //}
                //var rucDni = $('#txtDni').val();
                //if (rucDni != '') {
                //    $('#RucCliente_TicketProductos').html(rucDni);
                //} else {
                //    $('#RucCliente_TicketProductos').html('____________________________');
                //}

                if (msg.NombreCliente != '') {
                    $('#NombreCliente_TicketProductos').html(msg.NombreCliente);
                } else {
                    $('#NombreCliente_TicketProductos').html('____________________________');
                }
                $('#UsuarioCreacion_TicketProductos').html(msg.UsuarioCreacion);

                if (listaItems != null) {
                    var total = 0;
                    for (var i = 0; i < listaItems.length; i++) {
                        var monto = ConvertToDecimal(listaItems[i].Importe);
                        total += monto;
                    }

                    var IGV = total * 0.18;
                    var subTotal = (total - IGV);

                    $('#TotalVenta_TicketProductos').html(total.toFixed(2));
                    $('#NroComprobanteVenta_TicketProductos').html(msg.NroComprobante);
                    $('#SubTotalVentaVenta_TicketProductos').html(subTotal.toFixed(2));
                    $('#IgvVentaVenta_TicketProductos').html(IGV.toFixed(2));

                }

                var tipoDocumento = "";
                $('input[name="orderBoxComprobanteSuplementos_Modal[]"]:checked').each(function () {
                    tipoDocumento += $(this).val();
                });
                if (tipoDocumento == 1) {
                    document.getElementById("ticketSubtotal_TicketProductos").style.display = '';
                    document.getElementById("ticketIgv_TicketProductos").style.display = '';
                } else {
                    document.getElementById("ticketSubtotal_TicketProductos").style.display = 'none';
                    document.getElementById("ticketIgv_TicketProductos").style.display = 'none';
                }

                BuscarInformacionDetalleDeVentaSuplementosPorCodigo(codigo, listaItems);

            }
        }
    });
}

function BuscarInformacionDetalleDeVentaSuplementosPorCodigo(codigo, listaItems) {

    $('#ProductosDeVenta_TicketProductos').html('');
    var controlHtml = "";

    for (var i = 0; i < listaItems.length; i++) {
        controlHtml += '<tr>' +
            '<td style="text-align: center;font-size:medium;">' + listaItems[i].Cantidad + '</td>' +
            '<td style="text-align: left;font-size:medium;">' + listaItems[i].Descripcion + '</td>' +
            '<td style="text-align: center;font-size:medium;">' + listaItems[i].PrecioUnitario + '</td>' +
            '<td style="text-align: center;font-size:medium;">' + listaItems[i].Importe + '</td>' +
            '</tr>';
    }

    var formaPago = "";
    $('input[name="orderBoxFormaPagoSuplementos_Modal[]"]:checked').each(function () {
        formaPago += $(this).val();
    });

    if (formaPago == 1) {
        formaPago = 'Efectivo';
    } else if (formaPago == 2) {
        formaPago = 'Tarjeta Debito';
    } else if (formaPago == 3) {
        formaPago = 'Tarjeta Credito';
    } else if (formaPago == 4) {
        formaPago = 'Deposito';
    } else if (formaPago == 5) {
        formaPago = 'Web';
    }

    $('#FormaPago_TicketProductos').html(formaPago);
    $('#Acuenta_TicketProductos').html($('#txtTotalPagadoSuplementos_Modal').val());
    $('#Saldo_TicketProductos').html($('#lblTotalDebeSuplementos_Modal').html());

    $('#ProductosDeVenta_TicketProductos').append(controlHtml);
    ImprimeVentaSuplementosSocio();
}

function ImprimeVentaSuplementosSocio() {
    var ancho = screen.width - 10;
    var alto = screen.height - 75;
    var divToPrint = document.getElementById('divTicketVenta_TicketProductos');
    var newWin = window.open('', 'Print-Window', 'directories=no, border=0,scrollbars=yes,status=yes,toolbar=no,titlebar=no, resizable=no, menubar=no,width=' + ancho + ',height=' + alto + ',top=0,left=0');
    newWin.document.open();
    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
    newWin.document.close();
    setTimeout(function () { newWin.close(); }, 100);

    $('#gridSuplementosCarrito tbody tr').each(function () {
        $(this).remove();
    });
    calcularTotalNetoSuplementos();
}

function uspListarAsignarDiasHorarioPaquete() {

    var CodigoPaquete = $('#hdCodigoPaqueteOrigen').val();

    $("#griDiasDisponiblesPaquete").empty();
    $("#griDiasDisponiblesPaquete").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"CodigoPaquete":"' + CodigoPaquete + '"}',
                        url: "/gestionce/uspListarAsignarDiasHorarioPaquete",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }
                    });
                }
            }
        },
        height: 325,
        sortable: true,
        columns: [
            {
                template: "<input id='txtCodigoDia_#: Dia #' style='width:21px;height:21px;' disabled type='checkbox' #: Check # ><input type='text' value='#: Dia #' class='k-textbox' style='width:50px;display:none;'/>",
                title: "",
                width: 5
            }, {
                field: "desDia",
                title: "Día",
                width: 18,
                attributes: {
                    style: "font-size:11px;"
                }
            }]
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

function cuentaFindes() {

    var diasCongelamiento = $('#txtNroDiasCongelarProcFreezing').val();
    var fechaInicioFreezing = $('#txtFechaFreezingProcFreezing').val();
    var inicio = new Date(fechaInicioFreezing.split('/')[2], fechaInicioFreezing.split('/')[1] - 1, fechaInicioFreezing.split('/')[0]);//$("#txtFechaFreezingProcFreezing").data('kendoDatePicker').value();
    var fin = sumaFechaDia(parseInt(diasCongelamiento), fechaInicioFreezing);
    fin = new Date(fin.split('/')[2], fin.split('/')[1] - 1, fin.split('/')[0]);

    var countDiasNoAcceso = 0; //nro de dias que no tiene acceso por el paquete que adquirio

    while (inicio <= fin) {

        if (inicio.getDay() == 0 && $('#txtCodigoDia_7').prop('checked') == false) {
            countDiasNoAcceso++;
        }
        //lunes
        if (inicio.getDay() == 1 && $('#txtCodigoDia_1').prop('checked') == false) {
            countDiasNoAcceso++;
        }
        //martes
        if (inicio.getDay() == 2 && $('#txtCodigoDia_2').prop('checked') == false) {
            countDiasNoAcceso++;
        }
        //miercoles
        if (inicio.getDay() == 3 && $('#txtCodigoDia_3').prop('checked') == false) {
            countDiasNoAcceso++;
        }

        //jueves
        if (inicio.getDay() == 4 && $('#txtCodigoDia_4').prop('checked') == false) {
            countDiasNoAcceso++;
        }
        //viernes
        if (inicio.getDay() == 5 && $('#txtCodigoDia_5').prop('checked') == false) {
            countDiasNoAcceso++;
        }
        //sabado
        if (inicio.getDay() == 6 && $('#txtCodigoDia_6').prop('checked') == false) {
            countDiasNoAcceso++;
        }

        inicio.setDate(inicio.getDate() + 1);
    }

    inicio = $("#txtFechaFreezingProcFreezing").data('kendoDatePicker').value();
    $('#txtFrezenNroDiasNoAcceso').val(countDiasNoAcceso);
    return countDiasNoAcceso;

}

function event_confirmarActivarMembresia() {

    $('button[type="button"]').attr("disabled", true);
    var CodigoMenbresia = $('#hdCodigoMembresiaOrigen').val();
    var CodigoSocio = $('#hdCodigo').val();

    $.ajax({
        data: '{"CodigoMenbresia":"' + CodigoMenbresia + '"}',
        type: "POST",
        url: "/gestionce/uspActualizarMenbresiasFechaInicio",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg > 0) {

                $.bootstrapGrowl("La membresia se activo e inicia a partir de hoy.", { type: 'success', width: 'auto' });
                document.getElementById('myModalActivarMembresia').style.display = 'none';
                ListarMembresia(CodigoSocio);

            } else {
                $.bootstrapGrowl("Error, vuelve a intentarlo de nuevo", { type: 'danger', width: 'auto' });
            }
            $('button[type="button"]').attr("disabled", false);
        }
    });

}

function AgregarFotos() {
    document.getElementById('myModalTomarFoto').style.display = 'block';
    CargarCamaraOnline();
}

function AgregarFotosCarnet() {
    document.getElementById('myModalTomarFotoCarnet').style.display = 'block';
}

var ReadImage = function (file) {

    var reader = new FileReader;
    var image = new Image;

    reader.readAsDataURL(file);
    reader.onload = function (_file) {

        image.src = _file.target.result;
        image.onload = function () {
            var height = this.height;
            var width = this.width;
            var type = file.type;
            var size = ~~(file.size / 1024) + "KB";

            $("#imgVistaImagen").attr('src', _file.target.result);
            $("#txtDetalle").val("Size:" + size + ", " + height + " X " + width + ", " + type + "");
            $("#hdheight_imagen").val(height);
            $("#hdwidth_imagen").val(width);
            if (parseInt(height) > 1800) {
                $.bootstrapGrowl("El alto de la imagen no puede ser mayor 1800, seleccione otra imagen.", { type: 'danger', width: 'auto' });
            } else if (parseInt(width) > 1800) {
                $.bootstrapGrowl("El ancho de la imagen no puede ser mayor 1800, seleccione otra imagen.", { type: 'danger', width: 'auto' });
            }
        }

    }
}

var ReadImageCarnetVacunacion = function (file) {

    var reader = new FileReader;
    var image = new Image;

    reader.readAsDataURL(file);
    reader.onload = function (_file) {

        image.src = _file.target.result;
        image.onload = function () {
            var height = this.height;
            var width = this.width;
            var type = file.type;
            var size = ~~(file.size / 1024) + "KB";

            $("#imgVistaImagenCarnet").attr('src', _file.target.result);
            $("#txtDetalleFotoCarnet").val("Size:" + size + ", " + height + " X " + width + ", " + type + "");
            $("#hdheight_imagenCarnet").val(height);
            $("#hdwidth_imagenCarnet").val(width);
            if (parseInt(height) > 1800) {
                $.bootstrapGrowl("El alto de la imagen no puede ser mayor 1800, seleccione otra imagen.", { type: 'danger', width: 'auto' });
            } else if (parseInt(width) > 1800) {
                $.bootstrapGrowl("El ancho de la imagen no puede ser mayor 1800, seleccione otra imagen.", { type: 'danger', width: 'auto' });
            }
        }

    }
}

var ActualizarFoto = function () {

    var SubDominio = getCookie("_urlCorporativo_SubDominio");
    var CodigoSocio = $('#hdCodigo').val();
    var file = $("#buscarImagen").get(0).files;

    var detalle = $("#txtDetalle").val();
    var height = $("#hdheight_imagen").val();
    var width = $("#hdwidth_imagen").val();

    if (SubDominio == '') {
        $.bootstrapGrowl("Error, Falta el nombre del SubDominio.", { type: 'danger', width: 'auto' });
        return false;
    } else if (CodigoSocio == '') {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return false;
    } else if (detalle == '') {
        $.bootstrapGrowl("Falta seleccionar una nueva imagen.", { type: 'danger', width: 'auto' });
        return false;
    } else if (parseInt(height) > 1800) {
        $.bootstrapGrowl("El alto de la imagen no puede ser mayor 1800.", { type: 'danger', width: 'auto' });
        return false;
    } else if (parseInt(width) > 1800) {
        $.bootstrapGrowl("El ancho de la imagen no puede ser mayor 1800.", { type: 'danger', width: 'auto' });
        return false;
    } else if (file == undefined || file == null) {
        $.bootstrapGrowl("Falta seleccionar una nueva imagen.", { type: 'danger', width: 'auto' });
        return false;
    }

    $('button[type="button"]').attr("disabled", true);
    var data = new FormData;
    data.append("ImageFile", file[0]);
    data.append("SubDominio", SubDominio);
    data.append("CodigoSocio", CodigoSocio);

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        type: "Post",
        url: "/gestionce/GuardarFotoCliente",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {

            $('button[type="button"]').attr("disabled", false);
            $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='fotos' style='Cursor:pointer;'><img  src='" + response + "' class='contrast' style='width:290px;height: 180px;border-radius: 5px;'></div>");
            $('#FotoCliente_qrcode').attr('src', response);
            $.bootstrapGrowl("La imagen ha sido guardada correctamente.", { type: 'success', width: 'auto' });
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModalTomarFoto').style.display = 'none';
        }
    })

}

var ActualizarFotoCarnet = function () {

    var SubDominio = getCookie("_urlCorporativo_SubDominio");
    var CodigoSocio = $('#hdCodigo').val();
    var file = $("#buscarImagenCarnet").get(0).files;

    var detalle = $("#txtDetalleFotoCarnet").val();
    var height = $("#hdheight_imagenCarnet").val();
    var width = $("#hdwidth_imagenCarnet").val();

    if (SubDominio == '') {
        $.bootstrapGrowl("Error, Falta el nombre del SubDominio.", { type: 'danger', width: 'auto' });
        return false;
    } else if (CodigoSocio == '') {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return false;
    } else if (detalle == '') {
        $.bootstrapGrowl("Falta seleccionar una nueva imagen.", { type: 'danger', width: 'auto' });
        return false;
    } else if (parseInt(height) > 1800) {
        $.bootstrapGrowl("El alto de la imagen no puede ser mayor 1800.", { type: 'danger', width: 'auto' });
        return false;
    } else if (parseInt(width) > 1800) {
        $.bootstrapGrowl("El ancho de la imagen no puede ser mayor 1800.", { type: 'danger', width: 'auto' });
        return false;
    } else if (file == undefined || file == null) {
        $.bootstrapGrowl("Falta seleccionar una nueva imagen.", { type: 'danger', width: 'auto' });
        return false;
    }

    $('button[type="button"]').attr("disabled", true);
    var data = new FormData;
    data.append("ImageFile", file[0]);
    data.append("SubDominio", SubDominio);
    data.append("CodigoSocio", CodigoSocio);

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        type: "Post",
        url: "/gestionce/GuardarFotoCarnetVacunacionCliente",
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {

            $('button[type="button"]').attr("disabled", false);
            //$('#imgFotoCarnetVacunacion').html("<div onclick='AgregarFotos()' title='fotos' style='Cursor:pointer;'><img  src='" + response + "' class='contrast' style='width:290px;height: 180px;border-radius: 5px;'></div>");

            $('#imgFotoCarnetVacunacion').attr('src', response);
            $.bootstrapGrowl("La imagen ha sido guardada correctamente.", { type: 'success', width: 'auto' });

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModalTomarFotoCarnet').style.display = 'none';
        }
    })

}


function CargarCamaraOnline() {

    // Grab elements, create settings, etc.
    var video = document.getElementById('video');

    // Get access to the camera!
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        // Not adding `{ audio: true }` since we only want video now
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            //video.src = window.URL.createObjectURL(stream);
            video.srcObject = stream;
            video.play();
        });
    }

}

function event_TomarFoto() {
    var canvas = document.getElementById('canvas');
    var context = canvas.getContext('2d');
    var video = document.getElementById('video');

    context.drawImage(video, 0, 0, 250, 200);
}

function event_GuardarFotoCamaraWeb() {
    var image = document.getElementById("canvas").toDataURL("image/png");
    image = image.replace('data:image/png;base64,', '');

    var SubDominio = getCookie("_urlCorporativo_SubDominio");
    var CodigoSocio = $('#hdCodigo').val();

    if (SubDominio == '') {
        $.bootstrapGrowl("Error, Falta el nombre del SubDominio.", { type: 'danger', width: 'auto' });
        return false;
    } else if (CodigoSocio == '') {
        $.bootstrapGrowl("Falta buscar un cliente.", { type: 'danger', width: 'auto' });
        return false;
    } else if (image == undefined || image == null) {
        $.bootstrapGrowl("Falta tomar una foto.", { type: 'danger', width: 'auto' });
        return false;
    } else if (image == '') {
        $.bootstrapGrowl("Falta tomar una foto.", { type: 'danger', width: 'auto' });
        return false;
    }

    $('button[type="button"]').attr("disabled", true);
    var data = new FormData;
    data.append("ImageFile_texto", image);
    data.append("SubDominio", SubDominio);
    data.append("CodigoSocio", CodigoSocio);

    document.getElementById('loadMe').style.display = 'block';
    $.ajax({
        type: "Post",
        url: "/gestionce/GuardarFotoCliente_WebCam",
        // data: '{"CodigoMenbresia":"' + CodigoMenbresia + '"}',
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {

            $('button[type="button"]').attr("disabled", false);
            $('#lblFotoSocio').html("<div onclick='AgregarFotos()' title='fotos' style='Cursor:pointer;'><img  src='" + response + "' class='contrast' style='width:290px;height: 180px;border-radius: 5px;'></div>");
            $('#FotoCliente_qrcode').attr('src', response);
            $.bootstrapGrowl("La imagen ha sido guardada correctamente.", { type: 'success', width: 'auto' });

        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('myModalTomarFoto').style.display = 'none';

        }, error: function (jqXHR, textStatus, errorThrown) {
            alert(errorThrown);

        }
    })

}

function limpiarTotalesMembresias(nombre) {
    //Membresias //Nutricion //Personalizado
    //$('#lblMontoEfectivo' + nombre).html('0.00');
    //$('#lblMontoDebito' + nombre).html('0.00');
    //$('#lblMontoCredito' + nombre).html('0.00');
    //$('#lblMontoDeposito' + nombre).html('0.00');
    //$('#lblMontoWeb' + nombre).html('0.00');
    //$('#lblTotalVenta' + nombre).html('0.00');

    $('#lbl' + nombre + '_FormaPago_1').html('0.00');
    $('#lbl' + nombre + '_FormaPago_2').html('0.00');
    $('#lbl' + nombre + '_FormaPago_3').html('0.00');
    $('#lbl' + nombre + '_FormaPago_4').html('0.00');
    $('#lbl' + nombre + '_FormaPago_5').html('0.00');
    $('#lbl' + nombre + '_Total').html('0.00');
    //lblMembresias_Total
    //lblMembresias_FormaPago_1
}

function event_uspCentroEntrenamiento_uspConsumoTotalPorCliente() {
    //2 Membresias,7 Nutricion, 8 Personalizado

    if ($('#hdCodigo').val() > 0) {
        document.getElementById('loadMe').style.display = 'block';

        limpiarTotalesMembresias('Membresias');
        limpiarTotalesMembresias('Diario');
        limpiarTotalesMembresias('Productos');
        limpiarTotalesMembresias('Suplementos');
        limpiarTotalesMembresias('Nutricion');
        limpiarTotalesMembresias('Personalizado');
        limpiarTotalesMembresias('Ropas');
        limpiarTotalesMembresias('Accesorios');
        limpiarTotalesMembresias('Totales');

        var CodigoSocio = $('#hdCodigo').val();
        var DNI = $('#infoDNI').html();

        $.ajax({
            data: '{"CodigoSocio":"' + CodigoSocio + '","DNI":"' + DNI + '"}',
            type: "POST",
            url: "/gestionce/uspCentroEntrenamiento_uspConsumoTotalPorCliente",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {

                $('#lblMembresia_Deudas').html('');
                var Total_final = 0;
                var Totalefectivo_final = 0;
                var Totaldebito_final = 0;
                var Totalcredito_final = 0;
                var Totaldeposito_final = 0;
                var Totalweb_final = 0;
                //limpiarTotalesMembresias('Membresias');
                //limpiarTotalesMembresias('Nutricion');
                //limpiarTotalesMembresias('Personalizado');

                //diario ----- 4 DIARIOS para no repetir con ACCESORIOS desde la base de datos se traera 5
                //membresias-----2 Membresias, 7 = NUTRICION, 8 = PERSONALIZADO, 4 = SERVICIOS
                //productos -------- 1 = SUPLEMENTOS, 2 = JUGUERIA, 3 = ROPA FITNESS, 4 = ACCESORIOS
                if (msg.length > 0) {
                    //2 Membresias
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 2) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblMembresias_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblMembresias_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblMembresias_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblMembresias_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblMembresias_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblMembresias_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);

                    //7 Nutricion
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 7) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblNutricion_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblNutricion_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblNutricion_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblNutricion_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblNutricion_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblNutricion_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);
                    //8 Personalizado
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 8) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblPersonalizado_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblPersonalizado_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblPersonalizado_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblPersonalizado_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblPersonalizado_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblPersonalizado_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);
                    //2 jugueria
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 5) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblProductos_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblProductos_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblProductos_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblProductos_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblProductos_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblProductos_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);
                    //1 suplementos
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 1) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblSuplementos_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblSuplementos_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblSuplementos_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblSuplementos_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblSuplementos_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblSuplementos_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);
                    //3 ropas fitness
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 3) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblRopas_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblRopas_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblRopas_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblRopas_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblRopas_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblRopas_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);
                    //4 accesorios
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 4) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblAccesorios_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblAccesorios_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblAccesorios_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblAccesorios_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblAccesorios_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblAccesorios_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);
                    //6 diarios
                    var MontoTotal = 0;
                    for (var i = 0; i < msg.length; i++) {
                        if (msg[i].Tipo == 6) {

                            MontoTotal = MontoTotal + msg[i].MontoTotalSalida;
                            if (msg[i].FormaPago == 1) {
                                $('#lblDiario_FormaPago_1').html(msg[i].MontoTotalSalida);
                                Totalefectivo_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 2) {
                                $('#lblDiario_FormaPago_2').html(msg[i].MontoTotalSalida);
                                Totaldebito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 3) {
                                $('#lblDiario_FormaPago_3').html(msg[i].MontoTotalSalida);
                                Totalcredito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 4) {
                                $('#lblDiario_FormaPago_4').html(msg[i].MontoTotalSalida);
                                Totaldeposito_final += parseFloat(msg[i].MontoTotalSalida);
                            }
                            if (msg[i].FormaPago == 5) {
                                $('#lblDiario_FormaPago_5').html(msg[i].MontoTotalSalida);
                                Totalweb_final += parseFloat(msg[i].MontoTotalSalida);
                            }

                        }
                    }
                    $('#lblDiario_Total').html(MontoTotal.toFixed(2));
                    Total_final += parseFloat(MontoTotal);

                    //TOTAL FINALES
                    $('#lblTotales_Total').html(Total_final.toFixed(2));
                    $('#lblTotales_FormaPago_1').html(Totalefectivo_final.toFixed(2));
                    $('#lblTotales_FormaPago_2').html(Totaldebito_final.toFixed(2));
                    $('#lblTotales_FormaPago_3').html(Totalcredito_final.toFixed(2));
                    $('#lblTotales_FormaPago_4').html(Totaldeposito_final.toFixed(2));
                    $('#lblTotales_FormaPago_5').html(Totalweb_final.toFixed(2));
                }

            }, complete: function () {
                document.getElementById('loadMe').style.display = 'none';
                event_uspCentroEntrenamiento_uspConsumoDetalladoPorCliente();
            }
        });
    } else {
        alert("Es obligatorio buscar un cliente.");
    }


}

function event_uspCentroEntrenamiento_uspConsumoDetalladoPorCliente() {

    document.getElementById('loadMe').style.display = 'block';

    var CodigoSocio = $('#hdCodigo').val();
    var DNI = $('#infoDNI').html();
    $("#gvListadoConsumoDetalladoCliente").empty();
    $("#gvListadoConsumoDetalladoCliente").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: '{"CodigoSocio":"' + CodigoSocio + '","DNI":"' + DNI + '"}',
                        type: "POST",
                        url: "/gestionce/uspCentroEntrenamiento_uspConsumoDetalladoPorCliente",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        selectable: "row",
        scrollable: true,
        height: 750,
        columns: [
            {
                field: "DescTipo",
                title: "<center style='color:#fff;'><b>Tipo</b></center>",
                width: 9,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Fecha",
                title: "<center style='color:#fff;'><b>Fecha Venta</b></center>",
                template: "#= kendo.toString(kendo.parseDate(Fecha, 'yyyy-MM-dd'), 'dd/MM/yyyy hh:mm tt') #",
                width: 10,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            },
            {
                field: "Descripcion",
                title: "<center style='color:#fff;'><b>Descripción</b></center>",
                width: 15,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Costo",
                title: "<center style='color:#fff;'><b>Precio</b></center>",
                width: 6,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }

            }, {
                field: "Total",
                title: "<center style='color:#fff;'><b>Importe</b></center>",
                width: 7,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }

            }, {
                field: "FormaPago",
                title: "<center style='color:#fff;'><b>Forma Pago</b></center>",
                width: 7,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }

            }, {
                field: "NroComprobante",
                title: "<center style='color:#fff;'><b>Comprobante</b></center>",
                width: 8,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "UsuarioCreacion",
                title: "<center style='color:#fff;'><b>Responsable</b></center>",
                width: 10,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }, {
                field: "Vendedor",
                title: "<center style='color:#fff;'><b>Vendedor</b></center>",
                width: 10,
                attributes: {
                    style: "font-size:11px;text-align:center;"
                }
            }],
        dataBound: function (e) {
            this.element.find('tbody tr:first').addClass('k-state-selected');
            this.select(this.tbody.find('>tr:first'));
        }, change: function (e) {

            var grid = this;
            grid.select().each(function () {
                var dataItem = grid.dataItem($(this));
                //var codMem = dataItem.CodigoMenbresia;
            });

        }
    });

}

function event_makeCodeQR_Cliente() {

    if ($('#hdCodigo').val() > 0) {

        $("#qrcode").html('');
        $("#qrcode").qrcode({
            //render:"table"
            width: 235,
            height: 235,
            text: $('#hdCodigo').val()
        });

        $('#lblqrcode_Nombres').html('CODIGO: ' + $('#hdCodigo').val() + ' - ' + $('#lblNombreCompleto').val());
        $('#modaldiv_QRCliente').show('fast');

    } else {
        alert("Debes buscar un cliente.");
    }

}

function event_btnCerrarmodaldiv_QRCliente() {
    $('#modaldiv_QRCliente').hide('fast');
}

function CargarCamaraLectorQR() {

    // Grab elements, create settings, etc.
    var video = document.getElementById('reader');

    // Get access to the camera!
    if (navigator.mediaDevices && navigator.mediaDevices.getUserMedia) {
        // Not adding `{ audio: true }` since we only want video now
        navigator.mediaDevices.getUserMedia({ video: true }).then(function (stream) {
            //video.src = window.URL.createObjectURL(stream);
            video.srcObject = stream;
            video.play();
        });
    }

}

function SEGListarPerfilMenu() {    

    document.getElementById('loadMe').style.display = 'block';
    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {

        $('button[type="button"]').attr("disabled", false);
        document.getElementById('loadMe').style.display = 'none';

        for (var i = 0; i < msg.length; i++) {

            if (msg[i].Estado) {
                $('[data-controlmodulo="' + msg[i].CodigoMenu + '"]').show('fast');

                if (msg[i].CodigoMenu == '01d9d907-b8a6-4e2f-976a-ac34cb696d62') {
                    $('#hdPermiso_PagoMembresias').val('1');
                }
                if (msg[i].CodigoMenu == '6ad39c4c-cfca-4dc3-a5ce-44963884cbf7') {
                    $('#hdPermiso_PagoProductos').val('1');
                }
                if (msg[i].CodigoMenu == '7f43381c-5c82-45a1-8e52-0a00244cc377') {

                    //RESERVAR CLASES CON RESERVAS EN TIEMPO REAL
                    //document.getElementById('divClasesTiempoReal').style.display = '';
                    uspListarPresencial_HorarioClasesConfiguracionChecking();
                    //RESERVAS DE CLASES MANUALES
                    uspListarSalas();
                    //LISTAR SALAS DE CLASES GRUPALES
                    uspListarSala_Presencial();
                }

            }
        }

        if (msg.length == 0) {
            $('[data-controlmodulo="fb8a3d7d-68a8-4a9b-ab9c-9bb049c24c75"]').show('fast'); //activar el menu permiso

        }

        uspUpdateEstadoMembresias();


    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {

    };
    LlamarAJAX("/gestionce/SEGListarPerfilMenu", request, metodoCorrecto, metodoError);

}

//PARA VER LAS SALAS DE LAS CLASES GRUPALES AL INICIO
function uspListarSala_Presencial() {

    document.getElementById('loadMe').style.display = 'block';

    var metodoCorrecto = function (data) {
        var content_Salas = new Array();

        for (var i = 0; i < data.length; i++) {

            if (i == 0) {
                $('#hdCodigoSala').val(data[i].CodigoSala);
                uspListarPresencial_HorarioClasesConfiguracionCalendario(data[i].CodigoSala);

                content_Salas.push('<li onclick="uspListarPresencial_HorarioClasesConfiguracionCalendario(' + data[i].CodigoSala + ')" class="nav-item active"><a style="color:#000;" class="nav-link active" data-toggle="tab" href="#" role="tab">');
                content_Salas.push(data[i].Descripcion);
                content_Salas.push('</a></li>');
            } else {
                content_Salas.push('<li onclick="uspListarPresencial_HorarioClasesConfiguracionCalendario(' + data[i].CodigoSala + ')" class="nav-item"><a style="color:#000;" class="nav-link" data-toggle="tab" href="#" role="tab">');
                content_Salas.push(data[i].Descripcion);
                content_Salas.push('</a></li>');
            }
        }
        //uspListarPresencial_HorarioClasesConfiguracionCalendario

        $('#ulSalas').html(content_Salas.join(' '));

        document.getElementById('loadMe').style.display = 'none';

    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
    };

    LlamarAJAX('/reservapresencial/uspListarSala_Presencial', request, metodoCorrecto, metodoError);
}

function uspListarPresencial_HorarioClasesConfiguracionCalendario(CodigoSala) {
    $('#hdCodigoSala').val(CodigoSala);
    document.getElementById('loadMe').style.display = 'block';
    $('#calendario').fullCalendar('destroy');
    var entidad = {
        request: {
            CodigoSala: ConvertToInt32(CodigoSala)
        }
    };
    $.ajax({
        type: "POST",
        data: JSON.stringify(entidad),
        url: "/reservapresencial/uspListarPresencial_HorarioClasesConfiguracionCalendarioChecking",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //uspListarDisciplinaSala_Presencial();
            ListarCalendario(msg);
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

    //IniciarSalaHorarios(CodigoSala)
};

function ListarCalendario(data) {

    var alto = $('#divCalendarioClasesHorario').height();
    alto = window.innerHeight - 120;
    $('#calendario').fullCalendar({
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
        header: {
            right: 'myCustomButton',
            center: 'agendaWeek',
            left: 'today'
        },
        customButtons: {
            estatus: {
                text: '°',
                //type:'html',
                click: function () {
                    alert('clicked the custom button!');
                },
                //html:'<button style="color:red;border:solid 1px #aaa;">Update</button>'
            }
        },
        axisFormat: 'h(:mm)a',
        height: alto,
        defaultView: 'agendaWeek',
        lang: 'es',
        locale: 'es-us',
        aultDate: new Date(),
        schedulerLicenseKey: 'GPL-My-Project-Is-Open-Source',
        aultDate: new Date(),
        selectable: true,
        allDaySlot: false,
        minTime: '05:00:00',
        maxTime: '23:59:00',
        eventClick: function (event, calEvent, jsEvent, view) {
            //BUSCAR
            //uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(event.id);
            if (parseInt(event.CantidadAsistencias) > 0) {
                var _hora = kendo.toString(event.start._d, "hh:mm tt") + " - " + kendo.toString(event.end._d, "hh:mm tt");

                $('#modal-large-title-modalAsistencias').html('CLASE: ' + event.title + ' DE ' + _hora + ' CON ' + event.nombreProfesional);
                uspListarPresencial_HorarioClasesAsistenciasGestion(event.id);
            } else {
                $.bootstrapGrowl("No se encontro reservas en esta clase.", { type: 'danger', width: 'auto' });
            }
            
        },
        eventLimit: true,
        eventResize: function (event, delta, revertFunc) {
            //if (confirm("¿Desea actualizar la fecha de reserva?")) {
            //    var dataupdate = {
            //        CodigoWellnessConfiguracion: event.resourceId,
            //        CodigoWellnessServicioReserva: event.id,
            //        FechaInicio: event.start._d.addHours(5).yyyymmdd(),
            //        FechaFin: event.end._d.addHours(5).yyyymmdd(),
            //        TipoWellnessTm: 2
            //    };
            //    ActualizarFechasReserva(dataupdate);
            //    toastr.success("Actualizando los datos de la reserva", 'Resultado!');
            //}
            //else {
            //    toastr.error("Revertiendo los cambios de reserva", 'Resultado!');
            //    revertFunc();
            //}
        },
        eventDrop: function (event, delta, revertFunc) {
            //debugger;
            //if (confirm('¿Desea mover la reserva?')) {
            //    var dataupdate = {
            //        CodigoWellnessConfiguracion: event.resourceId,
            //        CodigoWellnessServicioReserva: event.id,
            //        FechaInicio: event.start._d.addHours(5).yyyymmdd(),
            //        FechaFin: event.end._d.addHours(5).yyyymmdd(),
            //        TipoWellnessTm: 2
            //    };
            //    ActualizarFechasReserva(dataupdate);
            //    toastr.success("Actualizando los datos de la reserva", 'Resultado!');
            //}
            //else {
            //    toastr.error("Revertiendo los cambios de reserva", 'Resultado!');
            //    revertFunc();
            //}
        },
        select: function (startDate, endDate) {

            //$('#hdCodigoHorarioClasesConfiguracion').val('');
            //document.getElementById('btnDesactivarClase').style.display = 'none';
            //$('#txtClase_Dia').removeAttr('disabled');

            //var fecha = new Date(startDate);
            //fecha.setHours(fecha.getHours() + 5);

            //var timepickerInicio = $("#txtClase_HoraInicio").data("kendoTimePicker");
            //timepickerInicio.value(fecha);

            //var fecha = new Date(startDate);
            //fecha.setHours(fecha.getHours() + 6);

            //var timepickerFin = $("#txtClase_HoraFin").data("kendoTimePicker");
            //timepickerFin.value(fecha);

            //var dia = fecha.getDay();

            //dia = ObtenerDiaSemanaSQLSERVER(dia);

            //$('select[id="txtClase_Dia"]').val(dia);
            //$("#txtClase_HoraInicio").data("kendoTimePicker").enable();
            //$("#txtClase_HoraFin").data("kendoTimePicker").enable();

            //event_nuevoProfesor();

            //document.getElementById("btnAbrirModalClase").click();
        },
        loading: function (isLoading, view) {
            if (isLoading) {
                document.getElementById('loadMe').style.display = 'block';
            }
            else {
                document.getElementById('loadMe').style.display = 'none';
            }
        },
        events: $.map(data, function (item, i) {
            var event = {};

            event.id = item.CodigoHorarioClasesConfiguracion;
            event.start = new Date(parseInt(item.HoraInicio.replace('/Date(', '')));
            event.end = new Date(parseInt(item.HoraFin.replace('/Date(', '')));
            event.title = item.Disciplina;
            if (item.Color != null && item.Color != '') {
                event.backgroundColor = item.Color;
            }
            else {
                event.backgroundColor = "#9501fc";
            }
            event.borderColor = "#000";
            event.CapacidadPermitida = item.CapacidadPermitida;
            event.CantidadAsistencias = item.CantidadAsistencias;
            event.urlPhoto = item.PhotoProfesionalFitness;
            event.dni = item.DNIProfesionalFitness;
            event.nombreProfesional = item.NombreProfesionalFitness;
            event.nombreCorto = item.NombreProfesionalFitness;//.split(' ').length > 0 ? item.NombreProfesionalFitness.split(' ')[0] : '';
            return event;
        }),
        eventRender: function (event, element, view) {
            if (true) {

                var el = element.html();
                var ancho = $(element).width();
                var alto = $(element).height();
                if (event.urlPhoto != null && event.urlPhoto != '') {
                    var detalle = new Array();
                    //alert(event.end._d);
                    var _hora = kendo.toString(event.start._d, "hh:mm tt") + " - " + kendo.toString(event.end._d, "hh:mm tt");

                    detalle.push('<div class="estilohorariocalendar pb-card" style="width: 100%;height: 100%;background-color:#fff;padding:3px;border-left-width: thick;border-left-color: ' + event.backgroundColor + ';color:#000;border-left-style: solid;">');
                    detalle.push('<div style="text-align:left;">');
                    if (parseInt(event.CantidadAsistencias) > 0) {
                        detalle.push('<span class="badge rounded-pill bg-success dark__bg-dark" style="position: absolute;right: 13px;font-size: 8px;">' + event.CantidadAsistencias + '/' + event.CapacidadPermitida + '</span>');

                    } else {
                        detalle.push('<span class="badge rounded-pill bg-dark dark__bg-dark" style="position: absolute;right: 13px;font-size: 8px;">' + event.CantidadAsistencias + '/' + event.CapacidadPermitida + '</span>');
                    }
                    
                    detalle.push('<h5 class="card-title mg-b-5" style="color:#000;font-size:12px;font-weight: bold;">' + event.title + '</h5>');
                    detalle.push('<p class="card-subtitle" style="color:#000;font-size:11px;margin-top:-12px;">' + event.nombreCorto + '</p>');
                    detalle.push('<p style="color:#000;font-size:13px;font-weight: bold;margin-top:-12px;">' + _hora + '</p>');
                    detalle.push('</div>');
                    detalle.push('</div>');

                    element.html(detalle.join(' '));
                }
                else {
                    element.html(el);
                }
            }

        },
        dayclick: function (event, allday, jsevent, view) {

            var fecha = new Date(event._d);
            fecha.setHours(fecha.getHours() + 5);

        }
    });

}

function uspListarPresencial_HorarioClasesAsistenciasGestion(CodigoHorarioClasesConfiguracion) {

    document.getElementById('loadMe').style.display = 'block';
    var codigo = CodigoHorarioClasesConfiguracion;

    $.ajax({
        type: "POST",
        data: '{"CodigoHorarioClasesConfiguracion":"' + codigo + '"}',
        url: "/gestionce/uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //options.success(msg);
            if (msg.length > 0) {
                document.getElementById('modalAsistencias').style.display = 'block';
                var controlHtml = '';
                //RECORRER LAS FILAS
                for (var i = 0; i < msg.length; i++) {

                    controlHtml += '<div class="col-12 p-card" style="border: 1px #ccc solid;">';
                    controlHtml += '    <div class="row">';
                    controlHtml += '        <div class="col-sm-3 col-md-3">';
                    controlHtml += '            <div class="position-relative h-sm-100">';
                    controlHtml += '                <img style="height: 120px;width: 120px;" class="fit-cover rounded-1 absolute-sm-centered" src="' + msg[i].ImagenUrl + '" alt="">';                    
                    controlHtml += '            </div>';
                    controlHtml += '        </div>';
                    controlHtml += '        <div class="col-sm-9 col-md-9">';
                    controlHtml += '            <div class="row">';
                    controlHtml += '                <div class="col-lg-8">';
                    controlHtml += '                    <h5 class="mt-3 mt-sm-0">' + msg[i].Nombres + ' ' + msg[i].Apellidos + '</h5>';

                    controlHtml += '                    <ul class="list-unstyled d-lg-block">';
                    controlHtml += '                        <li><span style="color:#000;font-size:13px;">Codigo: ' + msg[i].CodigoSocio + '</span></li>';
                    controlHtml += '                        <li><span style="color:#000;font-size:13px;">Nro documento: ' + msg[i].DNI + '</span></li>';
                    controlHtml += '                        <li><span style="color:#000;font-size:13px;">Celular: ' + msg[i].Celular + '</span></li>';
                    controlHtml += '                        <li><span style="color:#000;font-size:13px;">Correo: ' + msg[i].Correo + '</span></li>';                    
                    controlHtml += '                        <li><span style="color:#000;font-size:13px;"><strong class="text-success">Fecha reserva: ' + kendo.toString(kendo.parseDate(msg[i].FechaHoraReserva, 'yyyy-MM-dd'), 'dd/MM/yyyy HH:mm tt') + '</strong></span></li>';                    
                    controlHtml += '                    </ul>';
                    controlHtml += '                </div>';
                    controlHtml += '                <div class="col-lg-4 d-flex justify-content-between flex-column">';
                    controlHtml += '                    <div>';
                    controlHtml += '                        <h4 style="display:none;" class="fs-1 fs-md-2 text-warning mb-0">$1199.5</h4>';
                    controlHtml += '                        <h5 style="display:none;" class="fs--1 text-500 mb-0 mt-1">';
                    controlHtml += '                            <del>$2399 </del><span class="ms-1">-50%</span>';
                    controlHtml += '                        </h5>';
                    controlHtml += '                        <div class="d-lg-block">';
                    controlHtml += '                            <p class="fs--1 mb-1"><strong>' + msg[i].PlanMembresia + '</strong></p>';
                    controlHtml += '                            <p class="fs--1 mb-1">Inicio: <strong>' + kendo.toString(kendo.parseDate(msg[i].FechaInicio, 'yyyy-MM-dd'), 'dd/MM/yyyy') + '</strong></p>';
                    controlHtml += '                            <p class="fs--1 mb-1">Finaliza: <strong>' + kendo.toString(kendo.parseDate(msg[i].FechaFin, 'yyyy-MM-dd'), 'dd/MM/yyyy') + '</strong></p>';
                    controlHtml += '                            <p class="fs--1 mb-1">';
                    controlHtml += '                                <strong class="text-success">' + msg[i].ObtenerTiempoVencimiento + '</strong>';
                    controlHtml += '                            </p>';
                    controlHtml += '                        </div>';
                    controlHtml += '                    </div>';
                    controlHtml += '                    <div class="mt-2">';
                    controlHtml += '                        <button onclick="Confirmar_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias_CheckingMasivo(/' + msg[i].CodigoHorarioClasesConfiguracion + '/,/' + msg[i].CodigoHorarioClasesConfiguracionTiempoReal + '/,/' + msg[i].CodigoHorarioClasesConfiguracionAsistencias + '/,' + msg[i].CodigoSocio + ',' + msg[i].CodigoMembresia + ',this);" class="btn btn-primary btn-sm me-1 mb-1" type="button" style="display:' + msg[i].flagVistaBotonMarcarAsistencia + '">';
                    controlHtml += '                            <i class="fa-solid fa-check"></i>&nbsp;Marcar Asistencia';
                    controlHtml += '                        </button>';
                    controlHtml += '                        <div style="display:' + msg[i].flagVistaImagenAsistio + '"><i class="fa-solid fa-check"></i>&nbsp;Asistencia: &nbsp;' + kendo.toString(kendo.parseDate(msg[i].FechaHoraAsistio, 'yyyy-MM-dd '), 'dd/MM/yyyy hh:mm:ss tt') + '</div>';
                    controlHtml += '                    </div> ';
                    controlHtml += '                </div>';
                    controlHtml += '            </div>';
                    controlHtml += '        </div>';
                    controlHtml += '    </div>';
                    controlHtml += '</div> ';

                }
                $("#gvListaAsistencias").empty();
                $("#gvListaAsistencias").html(controlHtml);

            } else {
                $.bootstrapGrowl("No se encontro reservas en esta clase.", { type: 'danger', width: 'auto' });
                document.getElementById('modalAsistencias').style.display = 'none';
            }
        }, complete: function () {
            document.getElementById('loadMe').style.display = 'none';
        }
    });

    return;

    //$("#gvListaAsistencias").empty();
    //$("#gvListaAsistencias").kendoGrid({
    //    dataSource: {
    //        type: "json",
    //        transport: {
    //            read: function (options) {
    //                $.ajax({
    //                    type: "POST",
    //                    data: '{"CodigoHorarioClasesConfiguracion":"' + codigo + '"}',
    //                    url: "/gestionce/uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking",
    //                    contentType: "application/json; charset=utf-8",
    //                    dataType: "json",
    //                    success: function (msg) {
    //                        options.success(msg);
    //                        if (msg.length > 0) {
    //                            document.getElementById('modalAsistencias').style.display = 'block';

                                
    //                        } else {
    //                            $.bootstrapGrowl("No se encontro reservas en esta clase.", { type: 'danger', width: 'auto' });
    //                            document.getElementById('modalAsistencias').style.display = 'none';
    //                        }
    //                    }, complete: function () {
    //                        document.getElementById('loadMe').style.display = 'none';
    //                    }
    //                });
    //            }
    //        }
    //    },
    //    selectable: "row",
    //    sortable: true,
    //    height: 400,
    //    width: 900,
    //    columns: [{
    //        template: "<img style='margin-top: 0px; height: 36px; width: 36px; border-radius: 20px;' src='#: ImagenUrl #' alt='Cliente'>",
    //        title: "<center style='color:#fff;font-weight:bold'>Foto</center>",
    //        width: 5,
    //        attributes: {
    //            style: "font-size:10px;text-align:center;"
    //        }
    //    }, {
    //        field: "CodigoSocio",
    //        title: "<center style='color:#fff;font-weight:bold'>Codigo</center>",
    //        width: 6,
    //        attributes: {
    //            style: "font-size:10px;text-align:center;"
    //        }
    //    }, {
    //        field: "Nombres",
    //        title: "<center style='color:#fff;font-weight:bold'>Nombres</center>",
    //        width: 10,
    //        attributes: {
    //            style: "font-size:10px;text-align:center;"
    //        }
    //    }, {
    //        field: "Apellidos",
    //        title: "<center style='color:#fff;font-weight:bold'>Apellidos</center>",
    //        width: 10,
    //        attributes: {
    //            style: "font-size:10px;text-align:center;"
    //        }
    //    }, {
    //        field: "DNI",
    //        title: "<center style='color:#fff;font-weight:bold'>Nro Doc.</center>",
    //        width: 8,
    //        attributes: {
    //            style: "font-size:10px;text-align:center;"
    //        }
    //    }, {
    //        field: "Celular",
    //        title: "<center style='color:#fff;font-weight:bold'>Celular</center>",
    //        width: 8,
    //        attributes: {
    //            style: "font-size:10px;text-align:center;"
    //        }
    //    }, {
    //        field: "PlanMembresia",
    //        title: "<center style='color:#fff;font-weight:bold'>Membresia</center>",
    //        width: 10,
    //        attributes: {
    //            style: "font-size:10px;text-align:center;text-transform: uppercase;"
    //        }
    //    }, {
    //        field: "FechaInicio",
    //        title: "<center style='color:#fff;'><b>Fecha inicio</b></center>",
    //        template: "#= kendo.toString(kendo.parseDate(FechaInicio, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
    //        width: 10,
    //        attributes: {
    //            style: "font-size:12px;text-align:center;"
    //        }
    //    }, {
    //        field: "FechaFin",
    //        title: "<center style='color:#fff;'><b>Fecha fin</b></center>",
    //        template: "#= kendo.toString(kendo.parseDate(FechaFin, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
    //        width: 10,
    //        attributes: {
    //            style: "font-size:12px;text-align:center;"
    //        }
    //    }, {
    //        field: "FechaHoraReserva",
    //        title: "<center style='color:#fff;'><b>Fecha reserva</b></center>",
    //        template: "#= kendo.toString(kendo.parseDate(FechaHoraReserva, 'yyyy-MM-dd'), 'dd/MM/yyyy HH:mm tt') #",
    //        width: 10,
    //        attributes: {
    //            style: "font-size:12px;text-align:center;"
    //        }
    //    }]
    //});

}

function event_cerrarModalAsistencias() {
    document.getElementById('modalAsistencias').style.display = 'none';
}


function uspUpdateEstadoMembresias() {

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';

    $.ajax({
        type: "POST",
        url: "/gestionce/uspUpdateEstadoMembresias",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            $.bootstrapGrowl("Se reviso el estado de membresias correctamente.", { type: 'success', width: 'auto' });

            $('button[type="button"]').attr("disabled", false);
            document.getElementById('loadMe').style.display = 'none';
        }
    });

}


function uspValidarPagosClientes_AppFitnes() {

    $.ajax({
        type: "POST",
        url: "/gestionce/uspValidarPagosClientes_AdFitness",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg)
            let monto = msg?.MontoMensualidad;
            let description = msg?.PlanEmpresa;
            let clientP = msg?.ClientIdPaypal;
            let secretP = msg?.SecretIdPaypal;
            let entornoP = msg?.StatuProdPaypal;

            if (msg.CodigoUnidadNegocio == 183) {//URSULA TELLES de jhonatan
                msg.NroCuenta = '';
            }

            if (msg.NroCuenta == '') {
                document.getElementById('MyModalMembresiaCulminada').style.display = 'none';
                document.getElementById("btnAvisoCobranza_AppsFit").style.display = 'none';
                SEGListarPerfilMenu();
            } else {
                if (msg.Existe == 1) { //muestra el modal con boton de cerrar el mensaje
                    document.getElementById('MyModalMembresiaCulminada').style.display = 'block';
                    document.getElementById("btnAvisoCobranza_AppsFit").style.display = 'block';

                    document.getElementById("MyModalMembresiaCulminadaPaypal").style.display = 'none';
                    document.getElementById("MyModalMembresiaCulminadaBody").style.display = '';
                    document.getElementById("bodymculmModal").style.background = "";

                    $('#lblFechaPago_AppsFit').html(msg.FechaPagoTexto);
                    $('#lblFechaVence_AppsFit').html(msg.FechaVencimientoPagoTexto);
                    $('#lblMonto_AppsFit').html(msg.TipoMoneda + '' + msg.MontoMensualidad);
                    $('#lblBanco_AppsFit').html(msg.EntidadBancaria);
                    $('#lblNroCuenta_AppsFit').html(msg.NroCuenta);
                    $('#lblNombre_AppsFit').html(msg.ResponsableCuenta);
                    $('#lblCCI_AppsFit').html(msg.CCI);

                    $('#lblMensaje1_AppsFit').attr('href', 'https://api.whatsapp.com/send?phone=51' + msg.CelularEnviarVoucher + '&text=hola,%20soy%20la%20empresa%20' + msg.RazonSocial);
                    $('#lblMensaje2_AppsFit').attr('href', 'https://api.whatsapp.com/send?phone=51' + msg.CelularEnviarVoucher + '&text=hola,%20soy%20la%20empresa%20' + msg.RazonSocial);
                    $('#btnAvisoCobranza_AppsFit').click(function () {
                        document.getElementById('MyModalMembresiaCulminada').style.display = 'none';
                    });


                    //show modal paypal $
                    if (msg?.TipoMoneda == "$" && msg.MontoMensualidad > 0 && msg?.ClientIdPaypal != "") {
                        document.getElementById("MyModalMembresiaCulminadaPaypal").style.display = '';
                        document.getElementById("MyModalMembresiaCulminadaBody").style.display = 'none';

                        document.getElementById("amountTotalPay").innerText = msg?.MontoMensualidad;
                        document.getElementById("amountTotalPayItem").innerText = msg?.MontoMensualidad;
                        document.getElementById("bodymculmModal").style.background = "#D9D9D9";

                        document.getElementById("closeModalPaypalActiveShowDiv").style.display = "";

                        document.getElementById("fpbuyp").innerText = msg?.FechaPagoTexto;
                        document.getElementById("fpbuypv").innerText = msg?.FechaVencimientoPagoTexto;
                        document.getElementById("buypaldescription").innerText = `${msg?.PlanEmpresa}`;
                        //*********************************************** USD **************************************/
                        //paypal 

                      
                        var script = document.createElement('script');
                        script.type = 'text/javascript';
                        script.src = `https://www.paypal.com/sdk/js?client-id=${msg?.ClientIdPaypal}&components=buttons&currency=USD`;
                        document.head.appendChild(script);
                        setTimeout(() => {
                            try {
                                paypal.Buttons({
                                    style: {
                                        layout: 'vertical',
                                        color: 'white',
                                        shape: 'pill',
                                        label: 'paypal',
                                        tagline: false,
                                    },
                                    createOrder: async function () {
                                        let data = {
                                            monto, description, clientP, secretP, entornoP
                                        };
                                        const resp = await axios({
                                            method: "post",
                                            url: "/pasarela/PaypalOrderBusiness",
                                            data: data,
                                            headers: { "Content-Type": "application/json" },
                                        });

                                        return resp?.data?.Message1;

                                    },
                                    onApprove: async function (data) {

                                        let order = data?.orderID;
                                        let token = data?.facilitatorAccessToken;
                                        let business = true;
                                        let datax = {
                                            token, order, business, monto, entornoP
                                        };
                                        const capture = await axios({
                                            method: "post",
                                            url: "/pasarela/CaptureOrder",
                                            data: datax,
                                            headers: { "Content-Type": "application/json" },
                                        });

                                        if (capture.data?.Success) {
                                            document.getElementById('MyModalMembresiaCulminada').style.display = 'none';
                                            document.getElementById("BtnModalBuyPal").style.display = "block";
                                        } else {

                                            $.bootstrapGrowl(capture.data?.Message1, { type: 'danger', width: 'auto' });
                                        }
                                        return true;
                                    }, onCancel: async function () {

                                    }, onError: async function (err) {
                                        console.log("Log Paypal:", err)
                                    },

                                }).render('#paypal-button-container-buy');

                            } catch (e) {
                                location.reload()
                            }
                        }, 300);
                    }
                    //*********************************************** END USD **************************************/


                    SEGListarPerfilMenu();
                } else if (msg.Existe == 2) {//muestra el modal sin boton de cerrar el mensaje

                    document.getElementById('MyModalMembresiaCulminada').style.display = 'block';
                    document.getElementById("btnAvisoCobranza_AppsFit").style.display = 'none';

                    document.getElementById("MyModalMembresiaCulminadaPaypal").style.display = 'none';
                    document.getElementById("MyModalMembresiaCulminadaBody").style.display = '';
                    document.getElementById("bodymculmModal").style.background = "";


                    $('#lblFechaPago_AppsFit').html(msg.FechaPagoTexto);
                    $('#lblFechaVence_AppsFit').html(msg.FechaVencimientoPagoTexto);
                    $('#lblMonto_AppsFit').html(msg.MontoMensualidad);
                    $('#lblBanco_AppsFit').html(msg.EntidadBancaria);
                    $('#lblNroCuenta_AppsFit').html(msg.NroCuenta);
                    $('#lblNombre_AppsFit').html(msg.ResponsableCuenta);
                    $('#lblCCI_AppsFit').html(msg.CCI);

                    $('#lblMensaje1_AppsFit').attr('href', 'https://api.whatsapp.com/send?phone=51' + msg.CelularEnviarVoucher + '&text=hola,%20soy%20la%20empresa%20' + msg.RazonSocial);
                    $('#lblMensaje2_AppsFit').attr('href', 'https://api.whatsapp.com/send?phone=51' + msg.CelularEnviarVoucher + '&text=hola,%20soy%20la%20empresa%20' + msg.RazonSocial);

                    //show modal paypal $
                    if (msg?.TipoMoneda == "$" && msg.MontoMensualidad > 0 && msg?.ClientIdPaypal != "") {
                        document.getElementById("MyModalMembresiaCulminadaPaypal").style.display = '';
                        document.getElementById("MyModalMembresiaCulminadaBody").style.display = 'none';

                        document.getElementById("closeModalPaypalActiveShowDiv").style.display = "none";

                        document.getElementById("amountTotalPay").innerText = msg?.MontoMensualidad;
                        document.getElementById("amountTotalPayItem").innerText = msg?.MontoMensualidad;
                        document.getElementById("bodymculmModal").style.background = "#D9D9D9";

                        document.getElementById("fpbuyp").innerText = msg?.FechaPagoTexto;
                        document.getElementById("fpbuypv").innerText = msg?.FechaVencimientoPagoTexto;
                        document.getElementById("buypaldescription").innerText = `${msg?.PlanEmpresa}`;
                        //*********************************************** USD **************************************/
                        //paypal 
                       
                        var script = document.createElement('script');
                        script.type = 'text/javascript';
                        script.src = `https://www.paypal.com/sdk/js?client-id=${msg?.ClientIdPaypal}&components=buttons&currency=USD`;
                        document.head.appendChild(script);

                        setTimeout(() => {
                            try {
                                paypal.Buttons({
                                    style: {
                                        layout: 'vertical',
                                        color: 'white',
                                        shape: 'pill',
                                        label: 'paypal',
                                        tagline: false,
                                    },
                                    createOrder: async function () {
                                        let data = {
                                            monto, description, clientP, secretP, entornoP
                                        };
                                        const resp = await axios({
                                            method: "post",
                                            url: "/pasarela/PaypalOrderBusiness",
                                            data: data,
                                            headers: { "Content-Type": "application/json" },
                                        });
                                        console.log(`Order Id: ${resp?.data?.Message1}`)
                                        return resp?.data?.Message1;

                                    },
                                    onApprove: async function (data) {

                                        let order = data?.orderID;
                                        let token = data?.facilitatorAccessToken;
                                        let business = true;
                                        let datax = {
                                            token, order, business, monto
                                        };
                                        const capture = await axios({
                                            method: "post",
                                            url: "/pasarela/CaptureOrder",
                                            data: datax,
                                            headers: { "Content-Type": "application/json" },
                                        });

                                        if (capture.data?.Success) {
                                            document.getElementById('MyModalMembresiaCulminada').style.display = 'none';
                                            document.getElementById("BtnModalBuyPal").style.display = "block";
                                        } else {

                                            $.bootstrapGrowl(capture.data?.Message1, { type: 'danger', width: 'auto' });
                                        }
                                        return true;
                                    }, onCancel: async function () {

                                    }, onError: async function (err) {
                                        console.log("Log Paypal:", err)
                                    },

                                }).render('#paypal-button-container-buy');
                            } catch (e) {
                                location.reload()
                            }

                        }, 300);
                    }
                    //*********************************************** END USD **************************************/


                } else if (msg.Existe == 0) { //ES DEMOSTRACION
                    document.getElementById('MyModalMembresiaCulminada').style.display = 'none';
                    document.getElementById("btnAvisoCobranza_AppsFit").style.display = 'none';

                    if (msg.Estado == 2) {

                        document.getElementById('MyModalDemostracion').style.display = 'block';
                        document.getElementById("btnAvisoDemo_AppsFit").style.display = msg.EstadoFinPrueba;

                        $('#demo_lblFechaPago_AppsFit').html(msg.FechaVencimientoDemoTexto);
                        $('#demo_lblFechaVence_AppsFit').html(msg.FechaVencimientoDemoTexto);
                        $('#demo_lblPlan_AppsFit').html(msg.PlanEmpresa);
                        $('#demo_lblMonto_AppsFit').html(msg.TipoMoneda + '' + msg.MontoMensualidad);
                        $('#demo_lblBanco_AppsFit').html(msg.EntidadBancaria);
                        $('#demo_lblNroCuenta_AppsFit').html(msg.NroCuenta);
                        $('#demo_lblNombre_AppsFit').html(msg.ResponsableCuenta);
                        $('#demo_lblCCI_AppsFit').html(msg.CCI);


                        $('#btnAvisoDemo_AppsFit').click(function () {
                            document.getElementById('MyModalDemostracion').style.display = 'none';
                        });


                        if (msg?.TipoMoneda == "$" && msg.MontoMensualidad > 0 && msg?.ClientIdPaypal != "") {
                            document.getElementById('MyModalDemostracion').style.display = 'none';
                            document.getElementById('ModalByPaypalCustom').style.display = 'block';


                            document.getElementById("amountTotalPayC").innerText = msg?.MontoMensualidad;
                            document.getElementById("amountTotalPayItemC").innerText = msg?.MontoMensualidad;


                            //**************** start validate close button************/
                            if (msg?.EstadoFinPrueba == 'none') {
                                document.getElementById("ModalByPaypalCustomClose").style.display = "none";
                            } else {
                                document.getElementById("ModalByPaypalCustomClose").style.display = "";
                            }
                            //**************** end validate close button************/


                            //document.getElementById("fpbuypC").innerText = msg?.FechaPagoTexto;
                            document.getElementById("fpbuypvC").innerText = msg?.FechaVencimientoDemoTexto;
                            document.getElementById("buypaldescriptionC").innerText = `${msg?.PlanEmpresa}`;
                            //*********************************************** USD **************************************/
                            //paypal 
                           
                            var script = document.createElement('script');
                            script.type = 'text/javascript';
                            script.src = `https://www.paypal.com/sdk/js?client-id=${msg?.ClientIdPaypal}&components=buttons&currency=USD`;
                            document.head.appendChild(script);

                            setTimeout(() => {
                                try {
                                    paypal.Buttons({
                                        style: {
                                            layout: 'vertical',
                                            color: 'white',
                                            shape: 'pill',
                                            label: 'paypal',
                                            tagline: false,
                                        },
                                        createOrder: async function () {
                                            let data = {
                                                monto, description, clientP, secretP, entornoP
                                            };
                                            const resp = await axios({
                                                method: "post",
                                                url: "/pasarela/PaypalOrderBusiness",
                                                data: data,
                                                headers: { "Content-Type": "application/json" },
                                            });
                                            console.log(`Order Id: ${resp?.data?.Message1}`)
                                            return resp?.data?.Message1;

                                        },
                                        onApprove: async function (data) {

                                            let order = data?.orderID;
                                            let token = data?.facilitatorAccessToken;
                                            let business = true;
                                            let datax = {
                                                token, order, business, monto
                                            };
                                            const capture = await axios({
                                                method: "post",
                                                url: "/pasarela/CaptureOrder",
                                                data: datax,
                                                headers: { "Content-Type": "application/json" },
                                            });

                                            if (capture.data?.Success) {
                                                document.getElementById('ModalByPaypalCustom').style.display = 'none';
                                                document.getElementById("BtnModalBuyPal").style.display = "block";
                                            } else {

                                                $.bootstrapGrowl(capture.data?.Message1, { type: 'danger', width: 'auto' });
                                            }
                                            return true;
                                        }, onCancel: async function () {

                                        }, onError: async function (err) {
                                            console.log("Log Paypal:", err)
                                        },

                                    }).render('#paypal-button-container-buy-custom');
                                } catch (e) {
                                    location.reload()
                                }

                            }, 300);

                            //*********************************************** END USD **************************************/
                        }
                    }

                    SEGListarPerfilMenu();
                } else if (msg.Existe == 4) { //ES RETIRADO

                    document.getElementById('MyModalMembresiaCulminada').style.display = 'none';
                    document.getElementById("btnAvisoCobranza_AppsFit").style.display = 'none';
                    document.getElementById('MyModalDemostracion').style.display = 'none';
                    document.getElementById('MyModalLineaRetirado').style.display = 'block';

                    $('#retirado_lblFechaPago_AppsFit').html(msg.FechaPagoTexto);
                    $('#retirado_lblPlan_AppsFit').html(msg.PlanEmpresa);
                    $('#retirado_lblMonto_AppsFit').html(msg.TipoMoneda + '' + msg.MontoMensualidad);
                    $('#retirado_lblBanco_AppsFit').html(msg.EntidadBancaria);
                    $('#retirado_lblNroCuenta_AppsFit').html(msg.NroCuenta);
                    $('#retirado_lblNombre_AppsFit').html(msg.ResponsableCuenta);
                    $('#retirado_lblCCI_AppsFit').html(msg.CCI);

                }

            }

        }
    });
}

function showTogleBuySuccessClose() {
    document.getElementById("BtnModalBuyPal").style.display = 'none';
    location.reload();
}
function closeModalPaypalActiveShow() {
    document.getElementById("MyModalMembresiaCulminada").style.display = 'none'
}
function ModalByPaypalCustomClose() {
    document.getElementById("ModalByPaypalCustom").style.display = 'none'

}

//---Amador 05/01/2023----
//funciones globales

function generateComprobanteGlobal(code) {

    $.ajax({
        data: JSON.stringify({ codigo: code }),
        type: "POST",
        url: "/gestionce/generatePDF",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            console.log(msg)
            if (msg.Status == 0) {
                document.getElementById("btnModalPdfGeneral").click();
                document.getElementById("divrenderPdfGeneral").src = msg?.Message1;
                //document.getElementById("txtCorreoPDFGeneral").value =  'demo@gmail.com';
                document.getElementById("namepdftcmGeneral").value = msg?.Message2;

            } else {
                $.bootstrapGrowl(msg.Message1, { type: 'danger', width: 'auto' });
            }

        }
    });
    //generarSerieComprobante_Diario()
    ValidarGenerarSerieComprobante()
}

function sendEmailPDFGeneral(e) {
    e.disabled = true;
    var to = document.getElementById("txtCorreoPDFGeneral").value;
    var namepdf = document.getElementById("namepdftcmGeneral").value;
    var data = {
        name: namepdf,
        to: to,
        asunto: "Comprobante"
    }

    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: "/gestionce/sendEmailVentaPdf",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            if (msg?.Status == 0) {
                $.bootstrapGrowl(msg?.Message1, { type: 'success', width: 'auto' });

                document.getElementById("txtCorreoPDFGeneral").value = "";
                document.getElementById("namepdftcmGeneral").value = "";
            } else {
                $.bootstrapGrowl(msg?.Message1, { type: 'danger', width: 'auto' });
            }
            e.disabled = false;

        }
    });


}

function removePdfGeneral() {
    var name = document.getElementById("namepdftcmGeneral").value;
    if (name.trim().length > 0 && name != "") {
        $.ajax({
            data: JSON.stringify({ name: name }),
            type: "POST",
            url: "/gestionce/removePDF",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
            }
        });
    }
}

window.onafterprint = function () {
    console.log("sdfsdfsdfdsf")
}

