
//LISTAR SALAS DE MAQUINAS

function event_salamaquinasMENU(CodigoSala) {
    $('#hdCodigoSala_vista').val(CodigoSala);

    CargarFechaHorarioSalaMaquinas();
}

function uspListarSalas() {

    var entidad = {};

    var metodoCorrecto = function (data) {

        var content_salamaquinas = new Array();
        if (data.length > 0) {

            $('#hdflag_consalamaquinas').val('1');
            for (var i = 0; i < data.length; i++) {

                if (i == 0) {
                    content_salamaquinas.push('<a href="#activity_maquinas" onclick="event_salamaquinasMENU(' + data[i].CodigoSala +')" class="active" data-toggle="tab" role="tab" aria-controls="activity_maquinas" aria-selected="true">' + data[i].Descripcion + '</a>');                  
                    $('#hdCodigoSala_vista').val(data[i].CodigoSala);
                    //uspListarPresencial_ConfiguracionSalaFitness(data[i].CodigoSala);

                } else {
                    content_salamaquinas.push('<a href="#activity_maquinas" onclick="event_salamaquinasMENU(' + data[i].CodigoSala +')" data-toggle="tab" role="tab" aria-selected="false">' + data[i].Descripcion + '</a>');
                }

                //uspListarPresencial_HorarioClasesConfiguracionCalendario
            }

            content_salamaquinas.push('<a href="#activity_grupales" data-toggle="tab" role="tab" aria-selected="false">SALAS GRUPALES</a>');

            content_salamaquinas.push('<a href="" data-toggle="tab" role="tab" aria-selected="false">');
            content_salamaquinas.push('    <center>');
            content_salamaquinas.push('        <div class="row row-sm" id="modalLoadingMaquinas" style="display:none;">');
            content_salamaquinas.push('            <div class="loader loader-primary"></div>');
            content_salamaquinas.push('        </div>');
            content_salamaquinas.push('    </center>');
            content_salamaquinas.push('</a>');

        } else {
            $('#hdflag_consalamaquinas').val('0');
            content_salamaquinas.push('<a href="#activity_grupales" class="active" data-toggle="tab" role="tab" aria-controls="activity_maquinas" aria-selected="true" >SALAS GRUPALES</a>');
            content_salamaquinas.push('<a href="" data-toggle="tab" role="tab" aria-selected="false">');
            content_salamaquinas.push('    <center>');
            content_salamaquinas.push('        <div class="row row-sm" id="modalLoadingMaquinas" style="display:none;">');
            content_salamaquinas.push('            <div class="loader loader-primary"></div>');
            content_salamaquinas.push('        </div>');
            content_salamaquinas.push('    </center>');
            content_salamaquinas.push('</a>');

            $('#activity_maquinas').removeClass('active');
            $('#activity_maquinas').removeClass('show');

            $('#activity_grupales').addClass('active');
            $('#activity_grupales').addClass('show');
        }
       
        $('#divSalas').html(content_salamaquinas.join(' '));

        Obtener3FechasTodo();
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/pg/uspListarSalaMaquinas_Presencial', request, metodoCorrecto, metodoError);
}

//SALA CLASES GRUPALES
function uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(control, DiaNumero, fecha, validacionTieneReserva) {
    document.getElementById('loadMe').style.display = 'block';
    if (control != undefined) {

        $("#modalLoadingMaquinas").show("fast");

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

    var metodoCorrecto = function (data) {
        document.getElementById('loadMe').style.display = 'none';
        $("#modalLoadingMaquinas").hide("fast");
        
        if (data.length > 0) {

            //alert("data.length: " + data.length + " control: " + control + " DiaNumero: " + DiaNumero + " fecha: " + fecha + " validacionTieneReserva: " + validacionTieneReserva);

            var validacionTieneReservaFecha = validacionTieneReserva; 
            var content_clase = new Array();
            for (var i = 0; i < data.length; i++) {
                
                content_clase.push('<div class="col-sm-3 col-lg-3 mg-t-30" style="background-color: #e9ebee;padding-bottom: 10px;">');
                content_clase.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 10px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;padding-top: 12px;border-radius: 12px;border-bottom-right-radius: 0;border-bottom-left-radius: 0;">');
                //VERIFICAMOS SI TIENEN RESERVA 
                if (data[i].CodigoHorarioClasesConfiguracionAsistencias != '' && data[i].CodigoHorarioClasesConfiguracionAsistencias != null) {
                    //VERIFICAMOS SI TIENE LINK ACTIVO COMPARTIDO
                    if (data[i].CompartirLinkSala) {

                        if (data[i].LinkSala == '') {
                            content_clase.push('<div style="background-color:#efeff4;color:#000;font-weight: bold;text-align:center;padding:10px;border-radius: 12px;"> ');
                            content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">SIN ACCESO</p>');
                            content_clase.push('</div>');
                        } else {
                            content_clase.push('<a href="' + data[i].LinkSala + '" target="_blank"><div style="background-color:#0075ff;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                            content_clase.push('<p class="tx-center" style="font-size:23px;padding-top: 12px;">👉🔗INGRESAR🔗</p>');
                            content_clase.push('</div></a>');
                        }
                     
                        content_clase.push('<p class="tx-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 26px;">' + data[i].HoraInicioTexto + ' - ' + data[i].HoraFinTexto + '</p>');
                        content_clase.push('<p class="tx-center" style="font-weight: bold;">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + data[i].NombreProfesionalFitness + '</p>');
                        content_clase.push('</div>');
                       
                    } else {
                        content_clase.push('<p class="tx-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 26px;">' + data[i].HoraInicioTexto + ' - ' + data[i].HoraFinTexto + '</p>');
                        content_clase.push('<p class="tx-center" style="font-weight: bold;">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + data[i].NombreProfesionalFitness + '</p>');
                        content_clase.push('</div>');
                        content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;font-weight: bold;">');
                        content_clase.push('<p class="tx-center" style="font-size:22px;margin-top: 10px;">AFORO: ' + data[i].CantidadPlazas + '</p>');
                        content_clase.push('</div>');
                    }
                } else {
                    content_clase.push('<p class="tx-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 26px;">' + data[i].HoraInicioTexto + ' - ' + data[i].HoraFinTexto + '</p>');
                    content_clase.push('<p class="tx-center" style="font-weight: bold;">' + data[i].Disciplina + ' - ' + data[i].DesSala + ' - ' + data[i].NombreProfesionalFitness + '</p>');
                    content_clase.push('</div>');
                    content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;font-weight: bold;">');
                    content_clase.push('<p class="tx-center" style="font-size:22px;margin-top: 10px;">AFORO: ' + data[i].CantidadPlazas + '</p>');
                    content_clase.push('</div>');
                }
             
                
                if (data[i].CodigoHorarioClasesConfiguracionAsistencias != '' && data[i].CodigoHorarioClasesConfiguracionAsistencias != null) {
                    
                    if (data[i].validacionCancelarCita == 1) { //1= puede cancelar la cita
                        content_clase.push('<a href="#inicio"><div data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data[i].CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data[i].CodigoSocio + '" data-fecha="' + fecha +'"  onclick="javascript:CancelarCitaClaseGrupal(this);" style="background-color:red;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                        content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">CANCELAR</p>');
                        content_clase.push('</div></a>');
                    } else if (data[i].validacionCancelarCita == 2){//2 = NO puede cancelar la cita
                        content_clase.push('<div style="background-color:#0075ff;color:#000;text-align:center;padding:10px;border-radius: 12px;"> ');
                        content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">RESERVADO</p>');
                        content_clase.push('</div>');
                    }
                      
                } else {
                     //VALIDAMOS SI TIENE RESERVA NO PUEDE RESERVAR OTROS HORARIOS DURANTE EL DIA
                    if (validacionTieneReservaFecha > 0) {
                        content_clase.push('<a href="#inicio"><div onclick="mostrarAviso();" disabled style="background-color:#EFEFF4;font-weight: bold;color:#000;text-align:center;padding:10px;border-radius: 12px;"> ');
                        content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;font-weight: bold;">RESERVAR</p>');
                            content_clase.push('</div></a>');
                    } else {
                        if (data[i].CantidadPlazas <= 0) {
                            content_clase.push('<div disabled style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                            content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;font-weight: bold;">' + data[i].EstadoReserva + '</p>');
                            content_clase.push('</div>');
                        } else {
                            content_clase.push('<a href="#inicio"><div data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" data-horainicio="' + data[i].HoraInicioTexto + '" data-horafin="' + data[i].HoraFinTexto + '"  data-disciplina="' + data[i].Disciplina + '" data-fecha="' + fecha + '" onclick="javascript:visualizarCuposDisponibles(this);" style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                            content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;font-weight: bold;">' + data[i].EstadoReserva + '</p>');
                            content_clase.push('</div></a>');
                        }
                    }
                   
                }
                  
                content_clase.push('</div>');
                content_clase.push('</div>');
            }
            $('#divClases').html(content_clase.join(' '));

        } else {

            //$('#modal-info').show('fast');

            var content_clase = new Array();            
            content_clase.push('<div style="padding: 13px;margin-top:30px;" class="alert alert-solid alert-primary" role="alert">');                
            content_clase.push('<strong>Upps!</strong> Lo sentimos, se terminó las clases grupales en este día.');
            content_clase.push('</div>');
            $('#divClases').html(content_clase.join(' '));

        }
       
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/pg/uspListarPresencial_HorarioClasesConfiguracionPaginaWeb', request, metodoCorrecto, metodoError);
}

function visualizarCuposDisponibles(control) {
    var id = $(control).attr('data-id');
    var id_tiemporeal = $(control).attr('data-idtiemporeal');
    var dia = $(control).attr('data-dianumero');
    var horainicio = $(control).attr('data-horainicio');
    var horafin = $(control).attr('data-horafin');
    var disciplina = $(control).attr('data-disciplina');
    var fecha = $(control).attr('data-fecha');

    UspRegistrarPresencial_HorarioClasesAsistencias(id, id_tiemporeal, dia, disciplina, horainicio, horafin, fecha,'G');
}

function CancelarCitaClaseGrupal(control) {
    var id = $(control).attr('data-id');
    var id_tiemporeal = $(control).attr('data-idtiemporeal');
    var id_asistencia = $(control).attr('data-idasistencias');
    var codigosocio = $(control).attr('data-codigosocio');
    var dia = $(control).attr('data-dianumero');
    var fecha = $(control).attr('data-fecha');

    UspActualizarPresencial_DesactivarHorarioClasesAsistencias(id, id_tiemporeal, id_asistencia, codigosocio, dia, fecha,'G');
}

function CancelarCitaMaquinas(control) {
    var id = $(control).attr('data-id');
    var id_tiemporeal = $(control).attr('data-idtiemporeal');
    var id_asistencia = $(control).attr('data-idasistencias');
    var codigosocio = $(control).attr('data-codigosocio');
    var dia = $(control).attr('data-dianumero');
    var fecha = $(control).attr('data-fecha');

    UspActualizarPresencial_DesactivarHorarioClasesAsistencias(id, id_tiemporeal, id_asistencia, codigosocio, dia, fecha,'M');
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
    
    if (entidad.CodigoHorarioClasesConfiguracion == '') {
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
            var ReservasNormativa = getCookie('_ReservasNormativa_PersonaFit');
            var ReservasNotas = getCookie('_ReservasNotas_PersonaFit');
            $('#modalConfirmarReserva_norma').html(ReservasNormativa);
            $('#modalConfirmarReserva_nota').html(ReservasNotas);
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

        verReservas();
        $("#modalLoadingMaquinas").hide("fast");
    };
    var metodoError = function (msg) {
        alert("Error: " + msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/pg/UspRegistrarPresencial_HorarioClasesAsistencias", request, metodoCorrecto, metodoError);

}

function UspActualizarPresencial_DesactivarHorarioClasesAsistencias(id, id_tiemporeal, id_asistencia, codigosocio, dia,fecha, tipo) {
    $("#modalLoadingMaquinas").show("fast");
    var entidad = {};
    entidad.CodigoHorarioClasesConfiguracion = id;
    entidad.CodigoHorarioClasesConfiguracionTiempoReal = id_tiemporeal;
    entidad.CodigoHorarioClasesConfiguracionAsistencias = id_asistencia;
    entidad.CodigoSocio = codigosocio;

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

            verReservas();
        } 
        else {
            $.bootstrapGrowl("Error, No se ha podido cancelar la reserva!", { type: 'danger', width: 'auto' });
        }

        $("#modalLoadingMaquinas").hide("fast");
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/pg/CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias", request, metodoCorrecto, metodoError);

}

//SALA MAQUINAS
function uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_MAQUINAS(control, DiaNumero, fecha, validacionTieneReserva) {

    document.getElementById('loadMe').style.display = 'block';

    document.getElementById("divHorariSalaMaquinas").style.display = 'none';
    document.getElementById("divRegresar2").style.display = '';
    document.getElementById("divClasesMaquinas").style.display = '';  

    if (control != undefined) {
        fecha = $(control).attr('data-fecha');
        validacionTieneReserva = $(control).attr('data-validacionTieneReserva');
    } 
    //fecha = $(control).attr('data-fecha');  
    //validacionTieneReserva = $(control).attr('data-validacionTieneReserva');

    var horainicio = '06:00 AM';//$(control).attr('data-horainicio');
    var horafin = '06:00 AM';//$(control).attr('data-horafin');

    var entidad = {};
    entidad.DiaNumero = DiaNumero;
    entidad.CodigoSala = $('#hdCodigoSala_vista').val();
    entidad.FechaHoraReserva = fecha;
    entidad.HoraInicio = fecha + ' ' + horainicio;
    entidad.HoraFin = fecha + ' ' + horafin;
    
    var metodoCorrecto = function (data) {

        document.getElementById('loadMe').style.display = 'none';
        if (data.length > 0) {

            $('#divTituloBuscadorSalaMaquinas').html('👇selecciona un horario disponible:👇');
            var validacionTieneReservaFecha = validacionTieneReserva; 
            
            var content_clase = new Array();
            for (var i = 0; i < data.length; i++) {

                content_clase.push('<div class="col-md-3 col-lg-3 mg-t-30" style="background-color: #e9ebee;padding-bottom: 10px;" >');
                content_clase.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 10px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;border-radius: 12px;">');
                content_clase.push('<p class="tx-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 25px;">' + data[i].HoraInicioTexto + ' - ' + data[i].HoraFinTexto + '</p>');
                content_clase.push('</div>');                
                content_clase.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;">');
                content_clase.push('<p class="tx-center" style="font-size:24px;margin-top: 10px;font-weight:bold;">AFORO: ' + data[i].CantidadPlazas + '</p>');
                content_clase.push('</div>');

                if (data[i].CodigoHorarioClasesConfiguracionAsistencias != '' && data[i].CodigoHorarioClasesConfiguracionAsistencias != null) {

                    if (data[i].validacionCancelarCita == 1) { //1= puede cancelar la cita
                        content_clase.push('<a href="#inicio"><div data-id="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data[i].CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data[i].CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data[i].CodigoSocio + '" data-fecha="' + fecha + '" onclick="javascript:CancelarCitaMaquinas(this);" style="background-color:red;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                        content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">CANCELAR</p>');
                        content_clase.push('</div></a>');
                    } else if (data[i].validacionCancelarCita == 2) {//2 = NO puede cancelar la cita
                        content_clase.push('<div style="background-color:#000;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                        content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">RESERVADO</p>');
                        content_clase.push('</div>');
                    }
                    
                } else {
                    //VALIDAMOS SI TIENE RESERVA NO PUEDE RESERVAR OTROS HORARIOS DURANTE EL DIA
                    if (validacionTieneReservaFecha > 0) {
                        content_clase.push('<a href="#inicio"><div onclick="mostrarAviso();" disabled style="background-color:#EFEFF4;font-weight: bold;color:#000;text-align:center;padding:10px;border-radius: 12px;"> ');
                        content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;font-weight: bold;">RESERVAR</p>');
                        content_clase.push('</div></a>');
                    } else {
                        if (data[i].CantidadPlazas <= 0) {
                            content_clase.push('<div disabled style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                            content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;font-weight: bold;">' + data[i].EstadoReserva + '</p>');
                            content_clase.push('</div>');
                        } else {
                            content_clase.push('<a href="#inicio"><div data-id-maquinas="' + data[i].CodigoHorarioClasesConfiguracion + '" data-dianumero-maquinas="' + DiaNumero + '" data-idtiemporeal-maquinas="' + data[i].CodigoHorarioClasesTiempoReal + '" data-horainicio-maquinas="' + data[i].HoraInicioTexto + '" data-horafin-maquinas="' + data[i].HoraFinTexto + '"  data-disciplina-maquinas="' + data[i].Disciplina + '" data-fecha="' + fecha + '" onclick="javascript:visualizarCuposDisponiblesMaquinas(this);" style="background-color:' + data[i].ColorReserva + ';color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                            content_clase.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;font-weight: bold;">' + data[i].EstadoReserva + '</p>');
                            content_clase.push('</div></a>');
                        }
                    }
                   
                }
               
                content_clase.push('</div>');
                content_clase.push('</div>');
                
            }
            $('#divClasesMaquinas').html(content_clase.join(' '));

           
        } else {

            $('#modal-info').show('fast');
            var content_clase = new Array();
           
            content_clase.push('<div style="padding: 13px;margin-top:30px;" class="alert alert-solid alert-primary" role="alert">');
            content_clase.push('<strong>Upps!</strong> Lo sentimos, no encontramos horarios disponibles en este día.');
            content_clase.push('</div>');

            $('#divClasesMaquinas').html(content_clase.join(' '));
            
        }

        $("#modalLoadingMaquinas").hide("fast");
        
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/pg/uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS', request, metodoCorrecto, metodoError);
}

function uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(DiaNumero, fecha) {
    document.getElementById('divqrcode').style.display = 'none';
    $('#qrcode').html('');
    document.getElementById('loadMe').style.display = 'block';
    
    var entidad = {};
    entidad.DiaNumero = DiaNumero;
    entidad.FechaHoraReserva = fecha;
    entidad.CodigoSala = $('#hdCodigoSala_vista').val();

    var metodoCorrecto = function (data) {        
        document.getElementById('loadMe').style.display = 'none';
        
        if (data.validacionCancelarCita != 0) {
            document.getElementById('divqrcode').style.display = 'block';
            if (data.TipoSala == 1) { //TIPO SALA GRUPALES
                $('#hdCodigoClaseQR').val(data.CodigoHorarioClasesConfiguracionAsistencias);
                $('#lblEstadoTieneReserva_texto').html('Felicidades, ya tienes una reserva en la sala grupal, muestra tu CodigoQR al ingresar al centro.');
                             
                var content_clasereservada = new Array();
                content_clasereservada.push('<br /><div class="col-sm-3 col-lg-3 mg-t-30" style="background-color: #e9ebee;padding-bottom: 10px;">');
                content_clasereservada.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 10px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;padding-top: 12px;border-radius: 12px;border-bottom-right-radius: 0;border-bottom-left-radius: 0;">');
                content_clasereservada.push('<p class="tx-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 25px;">' + data.HoraInicioTexto + ' - ' + data.HoraFinTexto + '</p>');
                content_clasereservada.push('<p class="tx-center" style="font-weight: bold;">' + data.Disciplina + ' - ' + data.DesSala + '</p>');
                content_clasereservada.push('</div>');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;font-weight: bold;">');
                content_clasereservada.push('<p class="tx-center" style="font-size:22px;margin-top: 10px;">AFORO: ' + data.CantidadPlazas + '</p>');
                content_clasereservada.push('</div>');
                
                if (data.validacionCancelarCita == 1) { //1= puede cancelar la cita
                    content_clasereservada.push('<a href="#inicio"><div data-id="' + data.CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data.CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data.CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data.CodigoSocio + '" data-fecha="' + fecha + '"  onclick="javascript:CancelarCitaClaseGrupal(this);" style="background-color:red;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                    content_clasereservada.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">CANCELAR</p>');
                    content_clasereservada.push('</div></a>');
                } else if (data.validacionCancelarCita == 2) {//2 = NO puede cancelar la cita
                    content_clasereservada.push('<div style="background-color:#ccc;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                    content_clasereservada.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">RESERVADO</p>');
                    content_clasereservada.push('</div>');
                }

                content_clasereservada.push('</div>');
                content_clasereservada.push('</div>');
                $('#divClasesMaquinas_Reservada').html(content_clasereservada.join(' '));

            } else if (data.TipoSala == 2) { //TIPO SALA MAQUINAS
                $('#hdCodigoClaseQR').val(data.CodigoHorarioClasesConfiguracionAsistencias);
                $('#lblEstadoTieneReserva_texto').html('Felicidades, ya tienes una reserva en la sala de maquinas, muestra tu Codigo QR al ingresar al centro.');
                //agregamos al div para que el usuario pueda cancelar su reserva muy facil                    
                var content_clasereservada = new Array();
                content_clasereservada.push('<br /><div class="col-md-3 col-lg-3 mg-t-30" style="background-color: #e9ebee;padding-bottom: 10px;" >');
                content_clasereservada.push('<div class="card-contact pd-t-3 pd-r-3 pd-b-3 pd-l-3 clase" style="background-color: #fff;border-radius: 12px;padding: 10px;box-shadow: 0 3px 6px rgba(0,0,0,0.16), 0 3px 6px rgba(0,0,0,0.23);">');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;border-radius: 12px;">');
                content_clasereservada.push('<p class="tx-center tx-18" style="margin-top: 8px;font-weight:bold;font-size: 25px;">' + data.HoraInicioTexto + ' - ' + data.HoraFinTexto + '</p>');
                content_clasereservada.push('</div>');
                content_clasereservada.push('<div style="background-color:#fff;color:#000;text-align:center;padding:3px;">');
                content_clasereservada.push('<p class="tx-center" style="font-size:24px;margin-top: 10px;font-weight:bold;">AFORO: ' + data.CantidadPlazas + '</p>');
                content_clasereservada.push('</div>');

                if (data.validacionCancelarCita == 1) { //1= puede cancelar la cita
                    content_clasereservada.push('<a href="#inicio"><div data-id="' + data.CodigoHorarioClasesConfiguracion + '" data-dianumero="' + DiaNumero + '" data-idtiemporeal="' + data.CodigoHorarioClasesTiempoReal + '" data-idasistencias="' + data.CodigoHorarioClasesConfiguracionAsistencias + '" data-codigosocio="' + data.CodigoSocio + '" data-fecha="' + fecha + '" onclick="javascript:CancelarCitaMaquinas(this);" style="background-color:red;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                    content_clasereservada.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">CANCELAR</p>');
                    content_clasereservada.push('</div></a>');
                } else if (data.validacionCancelarCita == 2) {//2 = NO puede cancelar la cita
                    content_clasereservada.push('<div style="background-color:#ccc;color:#fff;text-align:center;padding:10px;border-radius: 12px;"> ');
                    content_clasereservada.push('<p class="tx-center" style="font-size:25px;padding-top: 12px;">RESERVADO</p>');
                    content_clasereservada.push('</div>');
                }

                content_clasereservada.push('</div>');
                content_clasereservada.push('</div>');
                $('#divClasesMaquinas_Reservada').html(content_clasereservada.join(' '));

            }

            makeCode();
            
        } else {
            $('#lblEstadoTieneReserva_texto').html('aún no tienes una reserva en esta fecha');
            $('#divClasesMaquinas_Reservada').html('');
            document.getElementById('divqrcode').style.display = 'none';
        }
       
        
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/pg/CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS', request, metodoCorrecto, metodoError);

}

function visualizarCuposDisponiblesMaquinas(control) {
    var id = $(control).attr('data-id-maquinas');
    var id_tiemporeal = $(control).attr('data-idtiemporeal-maquinas');
    var dia = $(control).attr('data-dianumero-maquinas');
    var horainicio = $(control).attr('data-horainicio-maquinas');
    var horafin = $(control).attr('data-horafin-maquinas');
    var disciplina = $(control).attr('data-disciplina-maquinas');
    var fecha = $(control).attr('data-fecha');

    UspRegistrarPresencial_HorarioClasesAsistencias(id, id_tiemporeal, dia, disciplina, horainicio, horafin, fecha,'M');
}

function mostrarAviso() {  
    $('#modalAviso').show('fast');
}

function ocultarAviso() {
    $('#modalAviso').hide('fast');
    $('#modalConfirmarReserva').hide('fast');
    $('#modal-info').hide('fast');
  
}

//_ColorEmpresa_PersonaFit
function makeCode() {

    var qrCode = new QRCodeStyling({
        width: 150,
        height: 150,
        data: document.getElementById("hdCodigoClaseQR").value,
        
        dotsOptions: {
            color: getCookie('_ColorEmpresa_PersonaFit'),
            type: "rounded"
        },
        backgroundOptions: {
            color: "#fff",
        },
        imageOptions: {
            crossOrigin: "anonymous"
        }
    });

    qrCode.append(document.getElementById("qrcode"));
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
