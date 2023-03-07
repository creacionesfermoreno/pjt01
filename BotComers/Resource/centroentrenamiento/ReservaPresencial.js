//CAMBIAR NOMBRE DE SALA

function event_CambiarNombreSala(CodigoSala) {
    document.getElementById('modalActualizarNombreSala').style.display = 'block';
    $('#hdCodigoSala_gestion').val(CodigoSala);
}

function event_cancelarNombreSala() {
    document.getElementById('modalActualizarNombreSala').style.display = 'none';
    $('#hdCodigoSala_gestion').val('0');
}

//SALAS

function event_DesactivarSala(CodigoSala) {
    document.getElementById('modaldesactivarsala').style.display = 'block';
    $('#hdCodigoSala_gestion').val(CodigoSala);
}

function event_cancelarDesactivarSala() {
    document.getElementById('modaldesactivarsala').style.display = 'none';
    $('#hdCodigoSala_gestion').val('0');
}

function event_cerrarAvisoSalaconClases() {
    document.getElementById('modalavisosalaconclases').style.display = 'none';   
}


function ListarSalaGrupalesGrid() {
    
    document.getElementById('loadMe').style.display = 'block';
    
    $("#gridSalasGrupales").empty();
    $("#gridSalasGrupales").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",                        
                        url: "/gestionce/uspListarSala_Presencial",
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
        height: 450,
        width: 450,
        columns: [{
            field: "CodigoSala",
            title: "<center style='color:#fff;font-weight:bold'>Codigo</center>",
            width: 5,
            attributes: {
                style: "font-size:10px;text-align:center;"
            }
        }, {
            field: "Descripcion",
            title: "<center style='color:#fff;font-weight:bold'>Descripcion</center>",
            width: 15,
            attributes: {
                style: "font-size:11px;text-align:center;"
            }
        }, {
            template: "<center><button onclick='event_CambiarNombreSala(\"#: CodigoSala #\");' type='button' class='btn btn-light btn-sm' title='Modificar sala.' >Modificar nombre</button></center>",
            title: "",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }, {
            template: "<center><button onclick='event_DesactivarSala(\"#: CodigoSala #\");' type='button' class='btn btn-light btn-sm' title='Eliminar sala.' >Eliminar</button></center>",
            title: "",
            width: 10,
            attributes: {
                style: "font-size:12px;text-align:center;"
            }
        }]
    });

}

function uspListarSala_Presencial() {

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


        uspListarDisciplinaSala_Presencial();
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
    };

    LlamarAJAX('/reservapresencial/uspListarSala_Presencial', request, metodoCorrecto, metodoError);
}

function uspRegistrarSala_Presencial() {

    var Accion = "N";
    
    var entidad = {};
    
    entidad.Descripcion = ConvertToStringFromObject($('input[id="txtSala_Descripcion"]').val());
    entidad.NroSala = 0;
    entidad.Color = "";
    entidad.Accion = Accion;
    
    if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
        alert("Falta ingresar nombre de la sala.");
        return;
    }

    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {
        //msg.Success
        if (msg) {
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se guardo correctamente los datos.", { type: 'success', width: 'auto' });
            document.getElementById("btnCerrarmodalSala").click();
            $('input[id="txtSala_Descripcion"]').val('');
            uspListarSala_Presencial();
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
    LlamarAJAX("/reservapresencial/CentroEntrenamiento_uspRegistrarSala_Presencial", request, metodoCorrecto, metodoError);

}

function uspEliminarSala_Presencial() {
    
    var entidad = {};

    entidad.CodigoSala = $('input[id="hdCodigoSala_gestion"]').val();
 
    if (IsUndefinedOrNullOrEmpty(entidad.CodigoSala)) {
        alert("Falta seleccionar una sala.");
        return;
    }

    if (!$('#chkConfirmarEliminarSala').prop('checked')) {
        $.bootstrapGrowl("Hola, te falta leer y confirmar el acuerdo.", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {
       
        if (msg == 0) {
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Se elimino correctamente esta sala.", { type: 'success', width: 'auto' });
            document.getElementById('modaldesactivarsala').style.display = 'none';
            $('input[id="hdCodigoSala_gestion"]').val('0');
            uspListarSala_Presencial();
            ListarSalaGrupalesGrid();
        }
        else {
            $('button[type="button"]').attr("disabled", false);
            document.getElementById('modaldesactivarsala').style.display = 'none';
            document.getElementById('modalavisosalaconclases').style.display = 'block';
        }
        
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/CentroEntrenamiento_uspEliminarSala_Presencial", request, metodoCorrecto, metodoError);

}

function uspActualizarSala_Presencial() {

    var entidad = {};
    entidad.CodigoSala = $('input[id="hdCodigoSala_gestion"]').val();
    entidad.Descripcion = $('input[id="txtSala_NuevoNombre"]').val();

    if (IsUndefinedOrNullOrEmpty(entidad.CodigoSala)) {
        alert("Falta seleccionar una sala.");
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Descripcion)) {
        alert("Falta ingresar el nuevo nombre.");
        return;
    }
   
    $('button[type="button"]').attr("disabled", true);
    var metodoCorrecto = function (msg) {

        $('button[type="button"]').attr("disabled", false);
        $.bootstrapGrowl("Se actualizo correctamente el nombre de esta sala.", { type: 'success', width: 'auto' });
        document.getElementById('modalActualizarNombreSala').style.display = 'none';
        $('input[id="hdCodigoSala_gestion"]').val('0');
        uspListarSala_Presencial();
        ListarSalaGrupalesGrid();

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/CentroEntrenamiento_uspEditarSala_Presencial", request, metodoCorrecto, metodoError);

}


//DISCIPLINAS X SALAS

function uspListarDisciplinaSala_Presencial() {

    var entidad = {};
    entidad.CodigoSala = $('input[id="hdCodigoSala"]').val() == '' ? 1 : $('input[id="hdCodigoSala"]').val();

    var metodoCorrecto = function (data) {
        var content_disciplina = new Array();
        content_disciplina.push('<option value="0">SELECCIONE DISCIPLINA</option>');
        for (var i = 0; i < data.length; i++) {
            content_disciplina.push('<option value="' + data[i].CodigoDisciplinaSala + '|' + data[i].Capacidad + '">' + data[i].Disciplina + '</option>');
        }       
        $('#txtClase_Disciplina').html(content_disciplina.join(' '));
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
    if (NroPlaza == '0') {
        $('#txtClase_NroPlaza').val('0');
        $('#hdCodigoDisciplinaSala').val('0');
    } else {
        $('#txtClase_NroPlaza').val(NroPlaza.split('|')[1]);
        $('#hdCodigoDisciplinaSala').val(NroPlaza.split('|')[0]);
    }
    
}

function uspRegistrarDisciplinaSala_Presencial() {

    document.getElementById('loadMe').style.display = 'block';

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
        document.getElementById('loadMe').style.display = 'none';

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

//PROFESOR

function event_nuevoProfesor() {
    $('#txtProfesor_Accion').val("N");
    //$('button[id="btnNuevoProfesor"]').attr("disabled", true);
    //$('button[id="btnGuardarProfesor"]').attr("disabled", false);
    $('input[id="txtProfesor_NroDocumento"]').attr("disabled", false);

    $('input[id="txtProfesor_CodigoProfesional"]').val('');
    $('input[id="txtProfesor_Nombres"]').val('');
    $('input[id="txtProfesor_Apellidos"]').val('');
    $('input[id="txtProfesor_NroDocumento"]').val('');
    $('input[id="txtProfesor_Celular"]').val('');
    $('input[id="txtProfesor_Telefono"]').val('');
    $('input[id="txtProfesor_Correo"]').val('');
    var todayDate = new Date();
    $("#txtProfesor_FechaNacimiento").data('kendoDatePicker').value(todayDate);
   
    $('input[id="txtProfesor_Direccion"]').val('');
    $('input[id="txtProfesor_Distrito"]').val('');

    $('#txtProfesorBuscador_Nombres').val('');
    $('#txtProfesorBuscador_Apellidos').val('');
    $('#txtProfesorBuscador_NroDocumento').val('');

    $('button[id="txtProfesor_NroDocumento"]').attr("disabled", false);
}

function uspRegistrarProfesor() {

    var Accion = $('input[id="txtProfesor_CodigoProfesional"]').val() == '' ? 'N' : $('#txtProfesor_Accion').val();

    var entidad = {};    
    entidad.CodigoProfesional = $('input[id="txtProfesor_CodigoProfesional"]').val();
    entidad.Nombres = $('input[id="txtProfesor_Nombres"]').val();
    entidad.Apellidos = $('input[id="txtProfesor_Apellidos"]').val();
    entidad.TipoDocumento = ConvertToStringFromObject($('select[id="txtProfesor_TipoDocumento"] option:selected').val()); 
    entidad.NroDocumento = $('input[id="txtProfesor_NroDocumento"]').val();
    entidad.Telefono = $('input[id="txtProfesor_Telefono"]').val();
    entidad.Celular = $('input[id="txtProfesor_Celular"]').val();
    entidad.Correo = $('input[id="txtProfesor_Correo"]').val();
    entidad.FechaNacimiento = kendo.toString($("#txtProfesor_FechaNacimiento").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    entidad.ImagenUrl = "";
    entidad.Genero = ConvertToStringFromObject($('select[id="txtProfesor_Genero"] option:selected').val()); 
    entidad.CostoPorClase = $('input[id="txtProfesor_CostoPorClase"]').val();
    entidad.DescuentoPorminuto = $('input[id="txtProfesor_DescuentoPorminuto"]').val();
    entidad.Facebook = "";
    entidad.Ubigeo = "";
    entidad.Direccion = $('input[id="txtProfesor_Direccion"]').val();
    entidad.Distrito = $('input[id="txtProfesor_Distrito"]').val();
    entidad.Accion = Accion;
    
    if (IsUndefinedOrNullOrEmpty(entidad.Nombres)) {
        $.bootstrapGrowl("Falta ingresar el nombre", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Apellidos)) {
        $.bootstrapGrowl("Falta ingresar el apellido", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.NroDocumento)) {
        $.bootstrapGrowl("Falta ingresar el nro documento", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.Celular)) {
        $.bootstrapGrowl("Falta ingresar el nro de celular", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CostoPorClase)) {
        $.bootstrapGrowl("Falta ingresar el costo que cobra por clase", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.DescuentoPorminuto)) {
        $.bootstrapGrowl("Falta ingresar el monto a descontar por cada minuto de tardanza", { type: 'danger', width: 'auto' });
        return;
    } 

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        
        if (Accion == "N") {
            $('button[type="button"]').attr("disabled", false);          
            $.bootstrapGrowl("Se guardo correctamente los datos.", { type: 'success', width: 'auto' });
            $('input[id="txtProfesor_CodigoProfesional"]').val(msg);

        } else if (Accion == "E") {

            $('button[type="button"]').attr("disabled", false);        
            $.bootstrapGrowl("Se actualizo  correctamente los datos.", { type: 'success', width: 'auto' });
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
    LlamarAJAX("/reservapresencial/uspRegistrarProfesor", request, metodoCorrecto, metodoError);

}

function uspBuscarProfesorPorNombres() {

    document.getElementById('loadMe').style.display = 'block';
    var entidad = {};
    
    entidad.Nombres = $('input[id="txtProfesorBuscador_Nombres"]').val();
    entidad.Apellidos = $('input[id="txtProfesorBuscador_Apellidos"]').val();
    entidad.NroDocumento = $('input[id="txtProfesorBuscador_NroDocumento"]').val();
    
    if (IsUndefinedOrNullOrEmpty(entidad.Nombres) && IsUndefinedOrNullOrEmpty(entidad.Apellidos) && IsUndefinedOrNullOrEmpty(entidad.NroDocumento)) {
        $.bootstrapGrowl("Falta ingresar un buscador", { type: 'danger', width: 'auto' });
        return;
    }
    if (!IsUndefinedOrNullOrEmpty(entidad.Nombres) && !IsUndefinedOrNullOrEmpty(entidad.Apellidos) && !IsUndefinedOrNullOrEmpty(entidad.NroDocumento)) {
        $.bootstrapGrowl("solo puede ingresar un buscador", { type: 'danger', width: 'auto' });
    }
    if (entidad.Nombres != "" && entidad.Apellidos != "") {
        $.bootstrapGrowl("solo puede ingresar un buscador", { type: 'danger', width: 'auto' });
    }
    if (entidad.Nombres != "" && entidad.NroDocumento != "") {
        $.bootstrapGrowl("solo puede ingresar un buscador", { type: 'danger', width: 'auto' });
    }
    if (entidad.Apellidos != "" && entidad.NroDocumento != "") {
        $.bootstrapGrowl("solo puede ingresar un buscador", { type: 'danger', width: 'auto' });
    }

    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {
        document.getElementById('loadMe').style.display = 'none';
        if (msg.validacionBusqueda != "vacio") {
            $('button[type="button"]').attr("disabled", false);

            $('input[id="txtProfesor_Accion"]').val("E");
            $('input[id="txtProfesor_CodigoProfesional"]').val(msg.CodigoProfesional);
            $('input[id="txtProfesor_Nombres"]').val(msg.Nombres);
            $('input[id="txtProfesor_Apellidos"]').val(msg.Apellidos);
            $('select[id="txtProfesor_TipoDocumento"]').val(msg.TipoDocumento);
            $('input[id="txtProfesor_NroDocumento"]').val(msg.NroDocumento);
            $('input[id="txtProfesor_Celular"]').val(msg.Telefono);
            $('input[id="txtProfesor_Telefono"]').val(msg.Celular);
            $('input[id="txtProfesor_Correo"]').val(msg.Correo);
            $("#txtProfesor_FechaNacimiento").data('kendoDatePicker').value(msg.FechaNacimiento); 
           // entidad.ImagenUrl = "";
            $('select[id="txtProfesor_Genero"]').val(msg.Genero);             
            $('input[id="txtProfesor_Direccion"]').val(msg.Direccion);
            $('input[id="txtProfesor_Distrito"]').val(msg.Distrito);
            $('input[id="txtClase_CostoPorClase"]').val(msg.CostoPorClase);
            $('input[id="txtClase_DescuentoPorminuto"]').val(msg.DescuentoPorminuto);
            
            $.bootstrapGrowl("Se encontro datos.", { type: 'success', width: 'auto' });
        }
        else {

            $('button[type="button"]').attr("disabled", false);

            $('input[id="txtProfesor_Accion"]').val("N");
            $('input[id="txtProfesor_CodigoProfesional"]').val('');
            $('input[id="txtProfesor_Nombres"]').val('');
            $('input[id="txtProfesor_Apellidos"]').val('');
            $('input[id="txtProfesor_NroDocumento"]').val('');
            $('input[id="txtProfesor_Celular"]').val('');
            $('input[id="txtProfesor_Telefono"]').val('');
            $('input[id="txtProfesor_Correo"]').val('');
            var todayDate = new Date();
            $("#txtProfesor_FechaNacimiento").data('kendoDatePicker').value(todayDate);
            // entidad.ImagenUrl = "";
            $('input[id="txtProfesor_Direccion"]').val('');
            $('input[id="txtProfesor_Distrito"]').val('');
            $('input[id="txtClase_CostoPorClase"]').val('0');
            $('input[id="txtClase_DescuentoPorminuto"]').val('0');
            $.bootstrapGrowl("No se encontro un profesor", { type: 'danger', width: 'auto' });
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/uspBuscarProfesorPorNombres", request, metodoCorrecto, metodoError);

}

//CONFIGURACION HORARIOS

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
        url: "/reservapresencial/uspListarPresencial_HorarioClasesConfiguracionCalendario",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {   
            uspListarDisciplinaSala_Presencial();
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

            $('#hdCodigoHorarioClasesConfiguracion').val('');
            document.getElementById('btnDesactivarClase').style.display = 'none';
            $('#txtClase_Dia').removeAttr('disabled');

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
            $("#txtClase_HoraInicio").data("kendoTimePicker").enable();
            $("#txtClase_HoraFin").data("kendoTimePicker").enable();

            event_nuevoProfesor();
            
            document.getElementById("btnAbrirModalClase").click();
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
                    detalle.push('<h5 class="card-title mg-b-5" style="color:#000;font-size:13px;font-weight: bold;">' + event.title + '</h5>');
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

function event_nuevaclaseBotoNuevo() {

    $('#hdCodigoHorarioClasesConfiguracion').val('');
    $('#txtClase_Dia').removeAttr('disabled');

    var fecha = new Date();
    fecha.setHours(fecha.getHours());

    var timepickerInicio = $("#txtClase_HoraInicio").data("kendoTimePicker");
    timepickerInicio.value(fecha);

    var fecha = new Date();
    fecha.setHours(fecha.getHours() + 1);

    var timepickerFin = $("#txtClase_HoraFin").data("kendoTimePicker");
    timepickerFin.value(fecha);

    var dia = fecha.getDay();

    dia = ObtenerDiaSemanaSQLSERVER(dia);

    $('select[id="txtClase_Dia"]').val(dia);
    $("#txtClase_HoraInicio").data("kendoTimePicker").enable();
    $("#txtClase_HoraFin").data("kendoTimePicker").enable();

    event_nuevoProfesor();
    event_nuevaclase();
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

function uspRegistrarPresencial_HorarioClasesConfiguracion() {

    var Accion = 'N';
    var entidad = {};
    
    if ($('#hdCodigoHorarioClasesConfiguracion').val() != '') {
        uspActualizarPresencial_HorarioClasesConfiguracion();
    } else {

        entidad.CodigoHorarioClasesConfiguracion = $('#hdCodigoHorarioClasesConfiguracion').val();
        entidad.CodigoDisciplinaSala = $('input[id="hdCodigoDisciplinaSala"]').val();
        entidad.CodigoProfesional = $('input[id="txtProfesor_CodigoProfesional"]').val();
        entidad.CodigoSala = $('input[id="hdCodigoSala"]').val();
        entidad.HoraInicio = $('input[id="txtClase_HoraInicio"]').val();
        entidad.HoraFin = $('input[id="txtClase_HoraFin"]').val();
        entidad.CapacidadPermitida = $('input[id="txtClase_NroPlaza"]').val();

        entidad.DiaNombre = '';
        $('.csschkDiaSemanaClase:checked').each(
            function () {
                //alert("El checkbox con valor " + $(this).val() + " está seleccionado");
                entidad.DiaNombre += $(this).val() + '|';  //entidad.DiaNombre + $(this).val() + '|';
                
            }
        );
        
        //alert(entidad.DiaNombre);
        //return;

        //entidad.DiaNumero = ConvertToStringFromObject($('select[id="txtClase_Dia"] option:selected').val());
        //entidad.DiaNombre = ConvertToStringFromObject($('select[id="txtClase_Dia"] option:selected').text());

        entidad.CostoPorClase = $('input[id="txtClase_CostoPorClase"]').val();
        entidad.DescuentoPorminuto = $('input[id="txtClase_DescuentoPorminuto"]').val();
        entidad.CompartirLinkSala = $('#chkCompartirLinkSala').prop('checked');
        entidad.LinkSala = $('input[id="txtClase_linkSala"]').val();
        entidad.Accion = Accion;

        var horaInicio = moment(entidad.HoraInicio, 'h:mma');
        var horaFin = moment(entidad.HoraFin, 'h:mma');

        if (entidad.CodigoDisciplinaSala == '0') {
            $.bootstrapGrowl("Falta seleccionar una disciplina", { type: 'danger', width: 'auto' });
            return;
        } if (IsUndefinedOrNullOrEmpty(entidad.CodigoDisciplinaSala)) {
            $.bootstrapGrowl("Falta seleccionar una disciplina", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoProfesional)) {
            $.bootstrapGrowl("Falta seleccionar el profesor", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoSala)) {
            $.bootstrapGrowl("Falta seleccionar una sala", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.HoraInicio)) {
            $.bootstrapGrowl("Falta seleccionar la hora de inicio", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.HoraFin)) {
            $.bootstrapGrowl("Falta seleccionar la hora de fin", { type: 'danger', width: 'auto' });
            return;
        } else if (horaFin.isBefore(horaInicio)) {
            $.bootstrapGrowl("La hora fin no puede ser menor a la hora inicio", { type: 'danger', width: 'auto' });
            return;
        } else if (entidad.HoraInicio == entidad.HoraFin) {
            $.bootstrapGrowl("aviso, la hora fin no puede ser igual a la hora inicio", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.CapacidadPermitida)) {
            $.bootstrapGrowl("Falta ingresar el nro de plaza", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.DiaNombre)) {
            $.bootstrapGrowl("Falta seleccionar el día", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.CostoPorClase)) {
            $.bootstrapGrowl("Falta ingresar el costo de la clase", { type: 'danger', width: 'auto' });
            return;
        } else if (IsUndefinedOrNullOrEmpty(entidad.DescuentoPorminuto)) {
            $.bootstrapGrowl("Falta ingresar el monto de descuento por minuto de tardanza", { type: 'danger', width: 'auto' });
            return;
        }

        $('button[type="button"]').attr("disabled", true);
        document.getElementById('loadMe').style.display = 'block';
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
                $('button[type="button"]').attr("disabled", false);
                $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
            }

            document.getElementById('loadMe').style.display = 'none';
            document.getElementById('modalClase').style.display = 'none';

        };
        var metodoError = function (msg) {
            alert(msg);
        };
        var request = {
            request: entidad
        };
        LlamarAJAX("/reservapresencial/uspRegistrarPresencial_HorarioClasesConfiguracion", request, metodoCorrecto, metodoError);

    }
    
}


function uspActualizarPresencial_HorarioClasesConfiguracion() {

    var Accion = 'E';
    var entidad = {};   

    entidad.CodigoHorarioClasesConfiguracion = $('#hdCodigoHorarioClasesConfiguracion').val();
    entidad.CodigoDisciplinaSala = $('input[id="hdCodigoDisciplinaSala"]').val();
    entidad.CodigoProfesional = $('input[id="txtProfesor_CodigoProfesional"]').val();
    entidad.CodigoSala = $('input[id="hdCodigoSala"]').val();
    entidad.HoraInicio = $('input[id="txtClase_HoraInicio"]').val();
    entidad.HoraFin = $('input[id="txtClase_HoraFin"]').val();
    entidad.CapacidadPermitida = $('input[id="txtClase_NroPlaza"]').val();
    entidad.DiaNumero = ConvertToStringFromObject($('select[id="txtClase_Dia"] option:selected').val());
    entidad.DiaNombre = ConvertToStringFromObject($('select[id="txtClase_Dia"] option:selected').text());
    entidad.CostoPorClase = $('input[id="txtClase_CostoPorClase"]').val();
    entidad.DescuentoPorminuto = $('input[id="txtClase_DescuentoPorminuto"]').val();
    entidad.CompartirLinkSala = $('#chkCompartirLinkSala').prop('checked');
    entidad.LinkSala = $('input[id="txtClase_linkSala"]').val();
    entidad.Accion = Accion;

    var horaInicio = moment(entidad.HoraInicio, 'h:mma');
    var horaFin = moment(entidad.HoraFin, 'h:mma');

    if (entidad.CodigoDisciplinaSala == '0') {
        $.bootstrapGrowl("Falta seleccionar una disciplina", { type: 'danger', width: 'auto' });
        return;
    } if (IsUndefinedOrNullOrEmpty(entidad.CodigoDisciplinaSala)) {
        $.bootstrapGrowl("Falta seleccionar una disciplina", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoProfesional)) {
        $.bootstrapGrowl("Falta seleccionar el profesor", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CodigoSala)) {
        $.bootstrapGrowl("Falta seleccionar una sala", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.HoraInicio)) {
        $.bootstrapGrowl("Falta seleccionar la hora de inicio", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.HoraFin)) {
        $.bootstrapGrowl("Falta seleccionar la hora de fin", { type: 'danger', width: 'auto' });
        return;
    } else if (horaFin.isBefore(horaInicio)) {
        $.bootstrapGrowl("La hora fin no puede ser menor a la hora inicio", { type: 'danger', width: 'auto' });
        return;
    } else if (entidad.HoraInicio == entidad.HoraFin) {
        $.bootstrapGrowl("aviso, la hora fin no puede ser igual a la hora inicio", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CapacidadPermitida)) {
        $.bootstrapGrowl("Falta ingresar el nro de plaza", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.DiaNumero)) {
        $.bootstrapGrowl("Falta seleccionar el día", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.CostoPorClase)) {
        $.bootstrapGrowl("Falta ingresar el costo de la clase", { type: 'danger', width: 'auto' });
        return;
    } else if (IsUndefinedOrNullOrEmpty(entidad.DescuentoPorminuto)) {
        $.bootstrapGrowl("Falta ingresar el monto de descuento por minuto de tardanza", { type: 'danger', width: 'auto' });
        return;
    }

    $('button[type="button"]').attr("disabled", true);
    document.getElementById('loadMe').style.display = 'block';
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
            $('button[type="button"]').attr("disabled", false);
            $.bootstrapGrowl("Error, vuelva a intentar nuevamente!", { type: 'danger', width: 'auto' });
        }

        document.getElementById('loadMe').style.display = 'none';
        document.getElementById('modalClase').style.display = 'none';

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/uspActualizarPresencial_HorarioClasesConfiguracion", request, metodoCorrecto, metodoError);

}

function event_confirmarDesactivarClase() {
    document.getElementById('modaldesactivarclase').style.display = 'block';
}

function event_cancelarDesactivarClase() {
    document.getElementById('modaldesactivarclase').style.display = 'none';
}

function uspDesactivarPresencial_HorarioClasesConfiguracion() {
    
    var entidad = {};
 
    if ($('#hdCodigoHorarioClasesConfiguracion').val() == '') {
        alert("Falta seleccionar una clase.");
        return;
    }

    entidad.CodigoHorarioClasesConfiguracion = $('#hdCodigoHorarioClasesConfiguracion').val();
    entidad.CodigoSala = $('input[id="hdCodigoSala"]').val();

    if (entidad.CodigoHorarioClasesConfiguracion == '') {
        $.bootstrapGrowl("Falta seleccionar una clase.", { type: 'danger', width: 'auto' });
        return;
    }

    if (!$('#chkConfirmarEliminarClaseGrupal').prop('checked')) {
        $.bootstrapGrowl("Hola, te falta leer y confirmar el acuerdo.", { type: 'danger', width: 'auto' });
        return;
    }
   
    $('button[type="button"]').attr("disabled", true);

    var metodoCorrecto = function (msg) {

        $('button[type="button"]').attr("disabled", false);
        $('input[id="hdCodigoHorarioClasesConfiguracion"]').val('');
        $.bootstrapGrowl("Se desactivo correctamente la clase.", { type: 'success', width: 'auto' });
        uspListarPresencial_HorarioClasesConfiguracionCalendario(entidad.CodigoSala);
        document.getElementById('modalClase').style.display = 'none';
        document.getElementById('modaldesactivarclase').style.display = 'none';

    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/reservapresencial/CentroEntrenamiento_uspDesactivarPresencial_HorarioClasesConfiguracion", request, metodoCorrecto, metodoError);

}


function uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(CodigoHorarioClasesConfiguracion) {

    document.getElementById('loadMe').style.display = 'block';

    var CodigoSala = $('input[id="hdCodigoSala"]').val();
   
    var entidad = {
        request: {
            CodigoHorarioClasesConfiguracion: CodigoHorarioClasesConfiguracion,
            CodigoSala: CodigoSala
        }
    };

    $('input[id="txtProfesor_CodigoProfesional"]').val('');
    $('input[id="txtProfesor_Nombres"]').val('');
    $('input[id="txtProfesor_Apellidos"]').val('');
    $('input[id="txtProfesor_NroDocumento"]').val('');
    $('input[id="txtProfesor_Celular"]').val('');
    $('input[id="txtProfesor_Telefono"]').val('');
    $('input[id="txtProfesor_Correo"]').val('');
    
    var todayDate = new Date();
    $("#txtProfesor_FechaNacimiento").data('kendoDatePicker').value(todayDate);

    $('input[id="txtProfesor_Direccion"]').val('');
    $('input[id="txtProfesor_Distrito"]').val('');

    $('#txtProfesorBuscador_Nombres').val('');
    $('#txtProfesorBuscador_Apellidos').val('');
    $('#txtProfesorBuscador_NroDocumento').val('');

    $("#chkCompartirLinkSala").prop('checked', false);
    $("#txtClase_linkSala").attr('disabled', 'disabled');

    $.ajax({
        data: JSON.stringify(entidad),
        type: "POST",
        url: "/reservapresencial/uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            var item = msg;
         
            if (item != null) {

                event_editarclase();
                document.getElementById('btnDesactivarClase').style.display = 'block';
                
                $('#txtProfesor_Accion').val("E");
                $('#txtProfesor_CodigoProfesional').val(item.ProfesionalFitness.CodigoProfesional);
                $('#txtProfesor_NroDocumento').val(item.ProfesionalFitness.DNI);
                $('#txtProfesor_NroDocumento').attr('disabled', 'disabled');
                $('#txtProfesor_Nombres').val(item.ProfesionalFitness.Nombres);
                $('#txtProfesor_Apellidos').val(item.ProfesionalFitness.Apellidos);
                $('select[id="txtProfesor_Genero"]').val(item.ProfesionalFitness.Genero);      

                $('select[id="txtProfesor_TipoDocumento"]').val(item.ProfesionalFitness.TipoDocumento);   
                $('#txtProfesor_NroDocumento').val(item.ProfesionalFitness.NroDocumento);

                $('#txtProfesor_Direccion').val(item.ProfesionalFitness.Direccion);
                $('#txtProfesor_Distrito').val(item.ProfesionalFitness.Distrito);

                $('#txtProfesor_Telefono').val(item.ProfesionalFitness.Telefono);
                $('#txtProfesor_Celular').val(item.ProfesionalFitness.Celular);
                $('#txtProfesor_Correo').val(item.ProfesionalFitness.Correo);
                $('#txtProfesor_CostoPorClase').val(item.ProfesionalFitness.CostoPorClase);
                $('#txtProfesor_DescuentoPorminuto').val(item.ProfesionalFitness.DescuentoPorminuto);

                $('#txtProfesor_FechaNacimiento').data("kendoDatePicker").value(item.ProfesionalFitness.FechaNacimiento);
                //if (item.ProfesionalFitness.ImagenUrl != null && item.ProfesionalFitness.ImagenUrl != '') {
                //    $('#imgFotoProfesor').attr('src', item.ProfesionalFitness.ImagenUrl);
                //}
               //TipoDocumentoTelefono
                $('#hdCodigoHorarioClasesConfiguracion').val(item.CodigoHorarioClasesConfiguracion); 

                //alert(item.CodigoDisciplinaSala + '|' + item.CapacidadPermitida);

                $('select[id="txtClase_Disciplina"]').val(item.CodigoDisciplinaSala + '|' + item.CapacidadPermitida);               
                $('select[id="txtClase_Dia"]').val(item.DiaNumero);  

                $("#hdCodigoDisciplinaSala").val(item.CodigoDisciplinaSala);
                $("#txtClase_NroPlaza").val(item.CapacidadPermitida);
              
          
               
                var HoraInicio = convertToDateTimeFromJson(item.HoraInicio);
                var HoraFin = convertToDateTimeFromJson(item.HoraFin);

                $("#txtClase_HoraInicio").data("kendoTimePicker").value(HoraInicio);
                $("#txtClase_HoraFin").data("kendoTimePicker").value(HoraFin);

                $("#txtClase_HoraInicio").data("kendoTimePicker").enable(false);
                $("#txtClase_HoraFin").data("kendoTimePicker").enable(false);
               
                $('#txtClase_Dia').attr('disabled', 'disabled');

                $("#txtClase_CostoPorClase").val(item.CostoPorClase);
                $("#txtClase_DescuentoPorminuto").val(item.DescuentoPorminuto);

                $("#txtClase_linkSala").val(item.LinkSala);
                $("#chkCompartirLinkSala").prop('checked', item.CompartirLinkSala);
                if (item.CompartirLinkSala) {
                    $("#txtClase_linkSala").removeAttr('disabled');
                } else {
                    $("#txtClase_linkSala").attr('disabled', 'disabled');
                }
                
                //$('button[id="btnNuevoProfesor"]').attr("disabled", true);
                //$('button[id="btnGuardarProfesor"]').attr("disabled", false);

                $.bootstrapGrowl("Busqueda términada.", { type: 'success', width: 'auto' });

            }
            else {
                document.getElementById('btnDesactivarClase').style.display = 'none';
                $.bootstrapGrowl("libre para registrar.", { type: 'success', width: 'auto' });
            }
        },
        error: function (e, d) {
            
            alert(e.responseText);
        },
        complete: function () {

            document.getElementById('loadMe').style.display = 'none';
            document.getElementById("btnAbrirModalClase").click();
        }
    });

};

function event_cerrarModalClase() {
    document.getElementById('modalClase').style.display = 'none';

}

function event_nuevaclase() {
    document.getElementById('form-check-diasemana-nuevo').style.display = '';
    document.getElementById('form-check-diasemana-editar').style.display = 'none';
}

function event_editarclase() {
    document.getElementById('form-check-diasemana-nuevo').style.display = 'none';
    document.getElementById('form-check-diasemana-editar').style.display = '';
}
