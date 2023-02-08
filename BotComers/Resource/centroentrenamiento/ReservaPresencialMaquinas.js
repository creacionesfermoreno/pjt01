


//CONFIGURACION SALA MAQUINAS

function uspListarPresencial_ConfiguracionSalaFitness(CodigoSala) {
    
    var entidad = {};
    entidad.CodigoSala = CodigoSala;
    $('#hdCodigoSala_vista').val(CodigoSala);
    $('#hdCodigoSala').val(CodigoSala);
    var metodoCorrecto = function (data) {
        
        if (data.length > 0) {
            
            for (var i = 0; i < data.length; i++) {
                $("#txtDia_" + data[i].DiaNumero + "_HoraInicio").data("kendoTimePicker").value(data[i].HoraInicio);
                $("#txtDia_" + data[i].DiaNumero + "_HoraFin").data("kendoTimePicker").value(data[i].HoraFin);
                $("#txtDia_" + data[i].DiaNumero + "_Tiempo").val(data[i].Tiempo);
                $("#txtDia_" + data[i].DiaNumero + "_Plaza").val(data[i].CapacidadPermitida);
                $("#txtDia_" + data[i].DiaNumero + "_Minutos").val(data[i].Minutos);
                $("#txtDia_" + data[i].DiaNumero + "_CodigoConfiguracionSalaFitness").val(data[i].CodigoConfiguracionSalaFitness);
                $("#txtDia_" + data[i].DiaNumero + "_Horarios").val(data[i].NroHorarios);
                $("#txtDia_" + data[i].DiaNumero + "_AforoxHorarios").val(data[i].AforoxHorario);
            }

        } else {

            for (var i = 1; i < 8; i++) {
                $("#txtDia_" + i + "_HoraInicio").data("kendoTimePicker").value('06:00 AM');
                $("#txtDia_" + i + "_HoraFin").data("kendoTimePicker").value('09:00 PM');
                $("#txtDia_" + i + "_Tiempo").val('');
                $("#txtDia_" + i + "_Plaza").val('');
                $("#txtDia_" + i + "_Minutos").val('');
                $("#txtDia_" + i + "_CodigoConfiguracionSalaFitness").val('');
                $("#txtDia_" + i + "_Horarios").val('');
                $("#txtDia_" + i + "_AforoxHorarios").val('');
            }

            $.bootstrapGrowl("No se encontro alguna configuración de Horario de la sala de maquinas.", { type: 'success', width: 'auto' });
        }
        
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/reservapresencial/uspListarPresencial_ConfiguracionSalaFitness', request, metodoCorrecto, metodoError);
}

function EliminarHorarioXHorasTemporal() {
    
    var entidad = {};
    entidad.CodigoSala = $('#hdCodigoSala_vista').val();
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        
        if (msg == 0) {
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Eliminando los horarios de la sala de maquinas....", { type: 'success', width: 'auto' });
            uspRegistrarPresencial_ConfiguracionSalaFitness();
        } else if (msg > 0) {
            $('button[type="button"]').attr("disabled", false);
            uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE();
            $.bootstrapGrowl("tienes horarios en maquinas que tienen reservas, no podemos registrar los nuevos horarios que estas creando.", { type: 'danger', width: 'auto' });
        }
      
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/CentroEntrenamiento_uspEliminarPresencial_SalaMaquinas_HorarioTemporal", request, metodoCorrecto, metodoError);

}

function event_cerrarmMdalClasesMaquinas() {
    document.getElementById('modalClasesMaquinas').style.display = 'none';
    document.getElementById('modalConfirmarIngresoHorarios').style.display = 'none';
}

function uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE() {

    var CodigoSala = $('#hdCodigoSala_vista').val();

    document.getElementById('loadMe').style.display = 'block';

    $("#gvListaClasesMaquinas").empty();
    $("#gvListaClasesMaquinas").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        data: '{"CodigoSala":"' + CodigoSala + '"}',
                        url: "/reservapresencial/uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            if (msg.length > 0) {
                                document.getElementById('modalClasesMaquinas').style.display = 'block';
                            } else {
                                document.getElementById('modalClasesMaquinas').style.display = 'none';
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
        height: 300,
        width: 900,
        columns: [{
            field: "DiaNombre",
            title: "<center style='color:#fff;font-weight:bold'>Día</center>",
            width: 8,
            attributes: {
                style: "font-size:10px;text-align:center;"
            }
        }, {
            field: "HoraInicio",
            title: "<center style='color:#fff;'><b>Fecha Clase</b></center>",
            template: "#= kendo.toString(kendo.parseDate(HoraInicio, 'yyyy-MM-dd'), 'dd/MM/yyyy') #",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "HoraInicio",
            title: "<center style='color:#fff;'><b>Inicia</b></center>",
            template: "#= kendo.toString(kendo.parseDate(HoraInicio, 'yyyy-MM-dd'), 'hh:mm tt') #",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "HoraFin",
            title: "<center style='color:#fff;'><b>Termina</b></center>",
            template: "#= kendo.toString(kendo.parseDate(HoraFin, 'yyyy-MM-dd'), 'hh:mm tt') #",
            width: 8,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesSala",
            title: "<center style='color:#fff;font-weight:bold'>Sala</center>",
            width: 8,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "Disciplina",
            title: "<center style='color:#fff;font-weight:bold'>Disciplina</center>",
            width: 12,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            template: "<b style='color:black;font-size:11px;text-align:center;' >#: CantidadAsistencias # de #: CapacidadPermitida #</b>",
            title: "<center style='color:#fff;font-weight:bold'>Asist.</center>",
            width: 6,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            field: "CantidadPlazas",
            title: "<center style='color:#fff;font-weight:bold'>Aforo</center>",
            width: 4,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            field: "DesEstado",
            title: "<center style='color:#fff;font-weight:bold'>Estado</center>",
            width: 5,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }]
    });
}

//ELIMINAR LOS HORARIOS EN TIEMPO REAL Y RESERVAS DE DE LA SALA MAQUINAS 
function uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL() {
    
    if ($('#chkConfirmarEliminarHorariosMaquinas').prop('checked')) {
        var entidad = {};
        $('button[type="button"]').attr("disabled", true);

        var metodoCorrecto = function (msg) {

            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("los horarios se han eliminado correctamente, ahora pueden guardar tu nuevo horario de la sala de maquinas.", { type: 'success', width: 'auto' });
            document.getElementById('modalClasesMaquinas').style.display = 'none';
            document.getElementById('modalConfirmarIngresoHorarios').style.display = 'none';
            uspListarPresencial_HorarioClasesConfiguracionCalendario(100);

        };
        var metodoError = function (msg) {
            alert(msg);
        };
        var request = {
            request: entidad
        };
        LlamarAJAX("/reservapresencial/CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL", request, metodoCorrecto, metodoError);

    } else {
        $.bootstrapGrowl("Hola, te falta leer y confirmar el acuerdo.", { type: 'danger', width: 'auto' });
    }
    
}

function uspRegistrarPresencial_ConfiguracionSalaFitness() {

    var Accion = "N";
    var entidad = {};   
    entidad.Accion = Accion;
    entidad.lista = new Array();

    for (var i = 1; i <= 7; i++) {
        var detalle = {};

        detalle.CodigoSala = $('#hdCodigoSala_vista').val();
        detalle.DiaNumero = $('select[id="txtDia_' + i + '_Dia"] option:selected').val();
        detalle.DiaNombre = $('select[id="txtDia_' + i + '_Dia"] option:selected').text();
        detalle.HoraInicio = $("#txtDia_" + i + "_HoraInicio").data("kendoTimePicker").value();
        detalle.HoraFin = $("#txtDia_" + i + "_HoraFin").data("kendoTimePicker").value();      
        detalle.Tiempo = $("#txtDia_" + i + "_Tiempo").val();
        detalle.CapacidadPermitida = $("#txtDia_" + i + "_Plaza").val();
        detalle.Minutos = $("#txtDia_" + i + "_Minutos").val();
        detalle.CodigoConfiguracionSalaFitness = $("#txtDia_" + i + "_CodigoConfiguracionSalaFitness").val();
        if (detalle.CodigoConfiguracionSalaFitness != '' ) {
            Accion = "E";
        }
        if (detalle.HoraInicio == '' || detalle.HoraInicio == null) {
            $.bootstrapGrowl("Falta seleccionar hora inicio", { type: 'danger', width: 'auto' });
            return;
        } else if (detalle.HoraFin == '' || detalle.HoraFin == null) {
            $.bootstrapGrowl("Falta seleccionar hora fin", { type: 'danger', width: 'auto' });
            return;
        } else if (detalle.Tiempo == '' || detalle.Tiempo == null) {
            $.bootstrapGrowl("Falta ingresar tiempo", { type: 'danger', width: 'auto' });
            return;
        } else if (detalle.Minutos == '' || detalle.Minutos == null) {
            $.bootstrapGrowl("Falta ingresar minutos", { type: 'danger', width: 'auto' });
            return;
        } else if (detalle.CapacidadPermitida == '' || detalle.CapacidadPermitida == null) {
            $.bootstrapGrowl("Falta ingresar Capacidad Permitida", { type: 'danger', width: 'auto' });
            return;
        }

        entidad.lista.push(detalle);
        
    }
 
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
      
        $('button[type="button"]').attr("disabled", false);
        $.bootstrapGrowl("Se guardo correctamente los datos.", { type: 'success', width: 'auto' });

        document.getElementById('modalConfirmarIngresoHorarios').style.display = 'none';

        var CodigoSala = $('#hdCodigoSala_vista').val();
        uspListarPresencial_ConfiguracionSalaFitness(CodigoSala);

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/uspRegistrarPresencial_ConfiguracionSalaFitness", request, metodoCorrecto, metodoError);

}


//DISCIPLINAS X SALAS

function uspListarDisciplinaSala_Presencial() {

    var entidad = {};
    entidad.CodigoSala = $('input[id="hdCodigoSala"]').val() == '' ? 1 : $('input[id="hdCodigoSala"]').val();

    var metodoCorrecto = function (data) {
        var content_disciplina = new Array();
        for (var i = 0; i < data.length; i++) {
            content_disciplina.push('<option value="' + data[i].CodigoDisciplinaSala + '|' + data[i].Capacidad + '">' + data[i].Disciplina + '</option>');
        }
        $('#txtClase_Disciplina').html(content_disciplina.join(' '));

        eventChange_DisciplinaSala_Presencial();
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/reservapresencial/uspListarDisciplinaSala_Presencial', request, metodoCorrecto, metodoError);
}

function eventChange_DisciplinaSala_Presencial() {
    var NroPlaza = $('select[id="txtClase_Disciplina"] option:selected').val(); //capacidad
    $('#txtClase_NroPlaza').val(NroPlaza.split('|')[1]);
    $('#hdCodigoDisciplinaSala').val(NroPlaza.split('|')[0]);
}

function uspRegistrarDisciplinaSala_Presencial() {

    var Accion = "N";

    var entidad = {};
    entidad.CodigoSala = ConvertToStringFromObject($('input[id="hdCodigoSala"]').val());
    entidad.CodigoDisciplinaSala = 0;
    entidad.Disciplina = $('input[id="txtDisciplina_Nombre"]').val();
    entidad.Capacidad = $('input[id="txtDisciplina_Plazas"]').val();
    entidad.Color = $('input[id="txtDisciplina_Color"]').val();
    entidad.Accion = Accion;

    if (IsUndefinedOrNullOrEmpty(entidad.Disciplina)) {
        $.bootstrapGrowl("Falta ingresar el nombre de la disciplina", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Capacidad)) {
        $.bootstrapGrowl("Falta ingresar el nro de plazas", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Color)) {
        $.bootstrapGrowl("Falta elegir un color para la disciplina", { type: 'danger', width: 'auto' });
        return;
    } else if ($('input[id="hdCodigoSala"]').val() == '') {
        $.bootstrapGrowl("Falta seleccionar una sala", { type: 'danger', width: 'auto' });
        return;
    } else if ($('input[id="hdCodigoSala"]').val() == 0) {
        $.bootstrapGrowl("Falta seleccionar una sala", { type: 'danger', width: 'auto' });
        return;
    }
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        //msg.Success
        if (msg) {
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se guardo  correctamente los datos.", { type: 'success', width: 'auto' });

            $('input[id="txtDisciplina_Nombre"]').val('');
            $('input[id="txtDisciplina_Plazas"]').val('');
            uspListarDisciplinaSala_Presencial();
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
    LlamarAJAX("/reservapresencial/uspRegistrarDisciplinaSala_Presencial", request, metodoCorrecto, metodoError);

}


//CONFIGURACION HORARIOS
function uspListarPresencial_HorarioClasesConfiguracionCalendario(CodigoSala) {
    $('#hdCodigoSala').val(CodigoSala);

    $('#calendario').fullCalendar('destroy');
    var entidad = {
        request: {
            CodigoSala: ConvertToInt32(CodigoSala)
        }
    };
    $.ajax({
        type: "POST",
        data: JSON.stringify(entidad),
        url: "/reservapresencial/uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {

            ListarCalendario(msg);
        }
    });

};

function ListarCalendario(data) {
  
    var alto = $('#divCalendarioClasesHorario').height();
    alto = window.innerHeight - 120;
    $('#calendario').fullCalendar({
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
        maxTime: '23:30:00',
        eventClick: function (event, calEvent, jsEvent, view) {

            uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(event.id);
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
       
            var fecha = new Date(startDate);
            fecha.setHours(fecha.getHours() + 5);

            var timepickerInicio = $("#txtClase_HoraInicio").data("kendoTimePicker");
            timepickerInicio.value(fecha);

            var fecha = new Date(startDate);
            fecha.setHours(fecha.getHours() + 6);

            var timepickerFin = $("#txtClase_HoraFin").data("kendoTimePicker");
            timepickerFin.value(fecha);

            var dia = fecha.getDay();
            dia = ObtenerDiaSemanaSQLSERVER(dia);
            $('select[id="txtClase_Dia"]').val(dia);
            
            $("#modalClase").modal('show');
        },
        loading: function (isLoading, view) {
            //if (isLoading) {
            //    waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
            //}
            //else {
            //    waitingDialog.hide();
            //}
        },
        events: $.map(data, function (item, i) {
            var event = {};

            event.id = item.CodigoHorarioClasesConfiguracion;
            event.start = new Date(parseInt(item.HoraInicio.replace('/Date(', '')));
            event.end = new Date(parseInt(item.HoraFin.replace('/Date(', '')));
            event.title = item.Disciplina;
            event.CantidadPlazas = item.CantidadPlazas;
            if (item.Color != null && item.Color != '') {
                event.backgroundColor = item.Color;
            }
            else {
                event.backgroundColor = "#9501fc";
            }
            event.borderColor = "#000";
          
            return event;
        }),
        eventRender: function (event, element, view) {
            if (true) {

                var el = element.html();
     
                var detalle = new Array();

                var _hora = kendo.toString(event.start._d, "hh:mm tt") + " - " + kendo.toString(event.end._d, "hh:mm tt");

                detalle.push('<div style="padding:2px;width: calc(100 % - 30px);background-color: ' + event.backgroundColor + ';color:#fff;">');
                detalle.push('<div style="text-align:left;">');
                detalle.push('<h5 class="card-title tx-11 mg-b-5" style="color:#fff;">Nro Plazas: ' + event.CantidadPlazas + '</h5>');
                detalle.push('<p class="tx-normal tx-12">' + _hora + '</p>');
                detalle.push('</div>');
                detalle.push('</div>');

                element.html(detalle.join(' '));
            }

        },
        dayclick: function (event, allday, jsevent, view) {

            var fecha = new Date(event._d);
            fecha.setHours(fecha.getHours() + 5);

            //alert('current view: ' + kendo.toString(fecha, "hh:mm tt") + ' - ' + fecha.getDay());

            //var timepicker = $("#txtHoraInicioClases").data("kendoTimePicker");
            //timepicker.value(fecha);

            //var dia = fecha.getDay();
            //dia = dia == 0 ? 7 : dia;
            //$('#cboDiaHorarioClases').val(dia);
            //$('#myModalDetalleConfiguracion').modal('show');

        }
    });

}

function ObtenerDiaSemanaSQLSERVER(dia) {
    switch (dia) {
        case 1: dia = 2; break;
        case 2: dia = 3; break;
        case 3: dia = 4; break;
        case 4: dia = 5; break;
        case 5: dia = 6; break;
        case 6: dia = 7; break;
        case 0: dia = 1; break;
        default: dia = 0; break;
    }

    return dia;
}

//HORARIO CONFIGURACION
function uspRegistrarPresencial_HorarioClasesConfiguracion() {

    var Accion = 'N';
    var entidad = {};

    entidad.CodigoHorarioClasesConfiguracion = "";
    entidad.CodigoDisciplinaSala = $('input[id="hdCodigoDisciplinaSala"]').val();
    entidad.CodigoProfesional = "049413E9-0762-4151-AC0B-0079BA0C2F94";
    entidad.CodigoSala = $('input[id="hdCodigoSala"]').val();
    entidad.HoraInicio = $('input[id="txtClase_HoraInicio"]').val();
    entidad.HoraFin = $('input[id="txtClase_HoraFin"]').val();
    entidad.CapacidadPermitida = $('input[id="txtClase_NroPlaza"]').val();
    entidad.DiaNumero = ConvertToStringFromObject($('select[id="txtClase_Dia"] option:selected').val());
    entidad.DiaNombre = ConvertToStringFromObject($('select[id="txtClase_Dia"] option:selected').text());
    entidad.Accion = Accion;

    if (entidad.CodigoDisciplinaSala == '') {
        $.bootstrapGrowl("Falta seleccionar una disciplina", { type: 'danger', width: 'auto' });
        return;
    }  else if (entidad.CodigoSala == '') {
        $.bootstrapGrowl("Falta seleccionar una sala", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.HoraInicio == '') {
        $.bootstrapGrowl("Falta seleccionar la hora de inicio", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.HoraFin == '') {
        $.bootstrapGrowl("Falta seleccionar la hora de fin", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.CapacidadPermitida == '') {
        $.bootstrapGrowl("Falta ingresar el nro de plaza", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.DiaNumero == '') {
        $.bootstrapGrowl("Falta seleccionar el día", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {

        if (Accion == "N") {
            $('button[type="button"]').attr("disabled", false);
            $('input[id="hdCodigoHorarioClasesConfiguracion"]').val(msg);
            $.bootstrapGrowl("Se guardo correctamente una nueva clase.", { type: 'success', width: 'auto' });
            uspListarPresencial_HorarioClasesConfiguracionCalendario(entidad.CodigoSala);
        } else if (Accion == "E") {
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se actualizo correctamente la clase.", { type: 'success', width: 'auto' });
            uspListarPresencial_HorarioClasesConfiguracionCalendario(entidad.CodigoSala);
        }
        else {
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }
        $("#modalClase").modal('hide');

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/uspRegistrarPresencial_HorarioClasesConfiguracion", request, metodoCorrecto, metodoError);

}

function uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(CodigoHorarioClasesConfiguracion) {

    var CodigoSala = $('input[id="hdCodigoSala"]').val();

    var entidad = {
        request: {
            CodigoHorarioClasesConfiguracion: CodigoHorarioClasesConfiguracion,
            CodigoSala: CodigoSala
        }
    };
    
    $.ajax({
        data: JSON.stringify(entidad),
        type: "POST",
        url: "/reservapresencial/uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var item = msg;

            if (item != null) {
               
                $('#hdCodigoHorarioClasesConfiguracion').val(item.CodigoHorarioClasesConfiguracion);

                $('select[id="txtClase_Disciplina"]').val(item.CodigoDisciplinaSala + '|' + item.CapacidadPermitida);
                $('select[id="txtClase_Dia"]').val(item.DiaNumero);
                $("#txtClase_NroPlaza").val(item.CapacidadPermitida);


                $('#txtClase_Dia').attr('disabled', 'disabled');

                var HoraInicio = convertToDateTimeFromJson(item.HoraInicio);
                var HoraFin = convertToDateTimeFromJson(item.HoraFin);

                $("#txtClase_HoraInicio").data("kendoTimePicker").value(HoraInicio);
                $("#txtClase_HoraFin").data("kendoTimePicker").value(HoraFin);
                
                $.bootstrapGrowl("Busqueda términada.", { type: 'success', width: 'auto' });

            }
            else {

                $.bootstrapGrowl("libre para registrar.", { type: 'success', width: 'auto' });
            }
        },
        error: function (e, d) {

            alert(e.responseText);
        },
        complete: function () {

            document.getElementById("btnAbrirModalClase").click();
        }
    });
};
