$(document).ready(function () {

    var todayDate = new Date();

    $('#txtFechaVentasHechaTotales').kendoDatePicker({
        value: todayDate,
        change: function () {
        }
    });

    $('#txtFechaVentaFinTotales').kendoDatePicker({
        value: todayDate,
        change: function () {
        }
    });

    listaTurnosTotales();
    ListarCountersTotales();
    ListaTiempoMembresiaPaqueteTotales();
    ListarAsesoresComercialesTotales();
    ListarTipoIngresoTotales();
    //cargarMenu();

    $('input[name="opcionExportar"]').click(function (e) {
        var valor = $(this).prop('checked');

        var inputs = $('input[name="opcionExportar"]');
        var checkedss = false;
        var validos = 0;
        for (var i = 0; i < inputs.length; i++) {
            var itemcheck = $(inputs[i]).prop('checked');
            if (itemcheck) {
                checkedss = itemcheck;
                validos += 1;
            }
        }

        if (checkedss) {
            if (validos == inputs.length) {
                $('#chkOpcionesExportar').prop('checked', true);
            }
            else {
                $('#chkOpcionesExportar').prop('checked', false);
            }

        }
        else {
            $('#chkOpcionesExportar').prop('checked', false);
        }
    });

    $('#chkOpcionesExportar').click(function (e) {
        var valor = $('#chkOpcionesExportar').prop('checked');
        if (valor) {
            $('input[name="opcionExportar"]').prop('checked', true);
        }
        else {
            $('input[name="opcionExportar"]').prop('checked', false);
        }
    });
    
});


function ListarCountersTotales() {
  
    var Counters = $("#txtCountersTotales").kendoDropDownList({
        optionLabel: "Todos Counter",
        dataTextField: "NombreCompleto",
        dataValueField: "NombreCompleto",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarCounters",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                            $('#hdCountersTotales').val(Counters.value());
                        }, complete: function () {
                          
                        }
                    });
                }
            }
        }, change: function () {
            $('#hdCountersTotales').val(Counters.value());
        }
    }).data("kendoDropDownList");
}

function ListarAsesoresComercialesTotales() {
     
    var AsesoresComerciales = $("#txtAsesoresComercialesTotales").kendoDropDownList({
        optionLabel: "Vendedores",
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
                            $('#hdAsesoresComercialesTotales').val(AsesoresComerciales.value());
                            $('#hdNombreVendedorTotales').val(AsesoresComerciales.value())
                        }, complete: function () {
                          
                        }
                    });
                }
            }
        }, change: function () {
            $('#hdAsesoresComercialesTotales').val(AsesoresComerciales.value());
            $('#hdNombreVendedorTotales').val(AsesoresComerciales.value())
        }
    }).data("kendoDropDownList");
}

function listaTurnosTotales() {
     
    var Turnos = $("#ddlTurnosEmpresaTotales").kendoDropDownList({
        optionLabel: "turnos",
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarTurnos",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {                            
                            options.success(msg);
                            $('#hdTurnosEmpresa').val(Turnos.value());
                        }, complete: function () {
                           
                        }
                    });
                }
            }
        }, change: function () {
            $('#hdTurnosEmpresaTotales').val(Turnos.value());
        }
    }).data("kendoDropDownList");
}

function ListarTipoIngresoTotales() {
  
    var dllTipoIngreso = $("#dllTipoIngresoTotales").kendoDropDownList({
        optionLabel: "Tipo Contrato",
        dataTextField: "Descripcion",
        dataValueField: "Codigo",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {
                    $.ajax({
                        type: "POST",
                        url: "/gestionce/ListarTipoIngreso",
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

function ListaTiempoMembresiaPaqueteTotales() {
   
    var ddlTiempoMembresiaPaqueteTotales = $("#ddlTiempoMembresiaPaqueteTotales").kendoDropDownList({
        filter: "startswith",
        optionLabel: "Tiempo",
        dataTextField: "Descripcion",
        dataValueField: "CodigoTiempo",
        dataSource: {
            serverFiltering: false,
            transport: {
                read: function (options) {

                    var nombre = $('input[aria-owns="ddlTiempoMembresia_listbox"]').val() == 'undefined' ? '' : $('input[aria-owns="ddlTiempoMembresia_listbox"]').val();

                    $.ajax({
                        data: '{"nombre":"' + nombre + '"}',
                        type: "POST",
                        url: "/gestionce/ListaTiempoMembresia",
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

function EnviarExcel_EventoTotales() {

    /*if ($('#hdTipoProduto').val()== 2) {
        tableToExcel('divExportarTodoReporteTotales', 'Membresias');
    }else if($('#hdTipoProduto').val()== 1) {
        tableToExcel('divExportarTodoReporteTotales', 'Productos vendidos');
    } else if ($('#hdTipoProduto').val() == 4) {
        tableToExcel('divExportarTodoReporteTotales', 'Servicios vendidos');
    }else if ($('#hdTipoProduto').val() == 6) {
        tableToExcel('divExportarTodoReporteTotales', 'Suplementos');
    } else if ($('#hdTipoProduto').val() == 7) {
        tableToExcel('divExportarTodoReporteTotales', 'Nutricion');
    } else if ($('#hdTipoProduto').val() == 8) {
        tableToExcel('divExportarTodoReporteTotales', 'Personalizado');
    }
    */

    $('#myModalExportarExcel').modal('show');


    //var CodSede = $(location).attr('href').split('?')[1].split('&')[2].split('=')[1].replace('#', ' ');
    //var CodigoUnidadNegocio = $(location).attr('href').split('?')[1].split('&')[3].split('=')[1].replace('#', ' ');
    //var NombreComercial = $(location).attr('href').split('?')[1].split('&')[4].split('=')[1].replace('#', ' ');
    //$.ajax({
    //    data: '{"CodigoUnidadNegocio":"' + CodigoUnidadNegocio + '","CodSede":"' + CodSede + '"}',
    //    type: "POST",
    //    url: "../Main.aspx/BuscarConfiguracion",
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "json",
    //    success: function (msg) {
    //        if (msg != null) {
    //            ExportarExcelHandlerFacturacion(msg.Ruc, msg.RazonSocial);
    //        }
    //    }
    //});
    //function ExportarExcelHandler(RUC, NombreComercial) {
    //    var data = new FormData();
    //    var Fecha = '2018-09-25';
    //    data.append('NombreGeneral', "INFORME GENERAL VENTAS " + new Date(Fecha).toLocaleDateString('es-es', { month: "long" }).toUpperCase() + " " + RUC + " - " + NombreComercial);
    //    data.append('CodigoUnidadNegocio', CodigoUnidadNegocio);
    //    data.append('CodigoSede', CodSede);
    //    data.append('Fecha', Fecha);
    //    var xhr = new XMLHttpRequest();
    //    xhr.open('POST', 'HttpHandler/ExportarExcelFacturacion.ashx', true);
    //    xhr.responseType = 'blob';
    //    //$.each(SERVER.authorization(), function (k, v) {
    //    //    xhr.setRequestHeader(k, v);
    //    //});
    //    //xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    //    xhr.onload = function (e) {
    //        if (this.status == 200) {
    //            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
    //            var downloadUrl = URL.createObjectURL(blob);
    //            var a = document.createElement("a");
    //            a.href = downloadUrl;
    //            a.download = "data.xls";
    //            a.style.display = "none";
    //            document.body.appendChild(a);
    //            a.click();
    //        } else {
    //            alert('Unable to download excel.')
    //        }
    //    };
    //    xhr.send(data);
    //};
    //function ExportarExcelHandlerFacturacion(RUC, NombreComercial) {
    //    var lista = new Array();
    //    var data = new FormData();
    //    lista.push({
    //        DescripcionCategoria: 'Membresias',
    //        Total: ConvertToDecimal($('#lblTotalVentaMembresias').html().toString().replace('S/.', '').trim()),
    //        Efectivo: ConvertToDecimal($('#lblMontoEfectivoMembresias').html().toString().replace('S/.', '').trim()),
    //        TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoMembresias').html().toString().replace('S/.', '').trim()),
    //        TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoMembresias').html().toString().replace('S/.', '').trim()),
    //        Deposito: ConvertToDecimal($('#lblMontoDepositoMembresias').html().toString().replace('S/.', '').trim()),
    //        Web: ConvertToDecimal($('#lblMontoWebMembresias').html().toString().replace('S/.', '').trim()),
    //        Deuda: ConvertToDecimal($('#lblDeudasMembresiasTotal').html().toString().replace('S/.', '').trim())
    //    });
    //    lista.push({
    //        DescripcionCategoria: 'Diario',
    //        Total: ConvertToDecimal($('#lblTotalVentaServicios').html().toString().replace('S/.', '').trim()),
    //        Efectivo: ConvertToDecimal($('#lblMontoEfectivoServicios').html().toString().replace('S/.', '').trim()),
    //        TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoServicios').html().toString().replace('S/.', '').trim()),
    //        TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoServicios').html().toString().replace('S/.', '').trim()),
    //        Deposito: ConvertToDecimal($('#lblMontoDepositoServicios').html().toString().replace('S/.', '').trim()),
    //        Web: ConvertToDecimal($('#lblMontoWebServicios').html().toString().replace('S/.', '').trim()),
    //        Deuda: '',
    //    });
    //    lista.push({
    //        DescripcionCategoria: 'Jugueria',
    //        Total: ConvertToDecimal($('#lblTotalVentaProductos').html().toString().replace('S/.', '').trim()),
    //        Efectivo: ConvertToDecimal($('#lblMontoEfectivoProductos').html().toString().replace('S/.', '').trim()),
    //        TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoProductos').html().toString().replace('S/.', '').trim()),
    //        TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoProductos').html().toString().replace('S/.', '').trim()),
    //        Deposito: ConvertToDecimal($('#lblMontoDepositoProductos').html().toString().replace('S/.', '').trim()),
    //        Web: ConvertToDecimal($('#lblMontoWebProductos').html().toString().replace('S/.', '').trim()),
    //        Deuda: ''
    //    });
    //    lista.push({
    //        DescripcionCategoria: 'Suplementos',
    //        Total: ConvertToDecimal($('#lblTotalVentaSuplementos').html().toString().replace('S/.', '').trim()),
    //        Efectivo: ConvertToDecimal($('#lblMontoEfectivoSuplementos').html().toString().replace('S/.', '').trim()),
    //        TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoSuplementos').html().toString().replace('S/.', '').trim()),
    //        TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoSuplementos').html().toString().replace('S/.', '').trim()),
    //        Deposito: ConvertToDecimal($('#lblMontoDepositoSuplementos').html().toString().replace('S/.', '').trim()),
    //        Web: ConvertToDecimal($('#lblMontoWebSuplementos').html().toString().replace('S/.', '').trim()),
    //        Deuda: ConvertToDecimal($('#lblDeudasSuplementosTotal').html().toString().replace('S/.', '').trim())
    //    });
    //    lista.push({
    //        DescripcionCategoria: 'Nutrición',
    //        Total: ConvertToDecimal($('#lblTotalVentaNutricion').html().toString().replace('S/.', '').trim()),
    //        Efectivo: ConvertToDecimal($('#lblMontoEfectivoNutricion').html().toString().replace('S/.', '').trim()),
    //        TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoNutricion').html().toString().replace('S/.', '').trim()),
    //        TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoNutricion').html().toString().replace('S/.', '').trim()),
    //        Deposito: ConvertToDecimal($('#lblMontoDepositoNutricion').html().toString().replace('S/.', '').trim()),
    //        Web: ConvertToDecimal($('#lblMontoWebNutricion').html().toString().replace('S/.', '').trim()),
    //        Deuda: ConvertToDecimal($('#lblDeudasNutricionTotal').html().toString().replace('S/.', '').trim())
    //    });
    //    lista.push({
    //        DescripcionCategoria: 'Personalizado',
    //        Total: ConvertToDecimal($('#lblTotalVentaPersonalizado').html().toString().replace('S/.', '').trim()),
    //        Efectivo: ConvertToDecimal($('#lblMontoEfectivoPersonalizado').html().toString().replace('S/.', '').trim()),
    //        TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoPersonalizado').html().toString().replace('S/.', '').trim()),
    //        TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoPersonalizado').html().toString().replace('S/.', '').trim()),
    //        Deposito: ConvertToDecimal($('#lblMontoDepositoPersonalizado').html().toString().replace('S/.', '').trim()),
    //        Web: ConvertToDecimal($('#lblMontoWebPersonalizado').html().toString().replace('S/.', '').trim()),
    //        Deuda: ConvertToDecimal($('#lblDeudasPersonalizadoTotal').html().toString().replace('S/.', '').trim())
    //    });
    //    lista.push({
    //        DescripcionCategoria: 'Ropas',
    //        Total: ConvertToDecimal($('#lblTotalVentaRopas').html().toString().replace('S/.', '').trim()),
    //        Efectivo: ConvertToDecimal($('#lblMontoEfectivoRopas').html().toString().replace('S/.', '').trim()),
    //        TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoRopas').html().toString().replace('S/.', '').trim()),
    //        TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoRopas').html().toString().replace('S/.', '').trim()),
    //        Deposito: ConvertToDecimal($('#lblMontoDepositoRopas').html().toString().replace('S/.', '').trim()),
    //        Web: ConvertToDecimal($('#lblMontoWebRopas').html().toString().replace('S/.', '').trim()),
    //        Deuda: ConvertToDecimal($('#lblDeudasRopasTotal').html().toString().replace('S/.', '').trim())
    //    });
    //    data.append('lista', JSON.stringify(lista));
    //    data.append('RUC', RUC);
    //    data.append('NombreComercial', NombreComercial);
    //    var xhr = new XMLHttpRequest();
    //    xhr.open('POST', 'HttpHandler/ExportarFacturacionResumen.ashx', true);
    //    xhr.responseType = 'blob';
    //    //$.each(SERVER.authorization(), function (k, v) {
    //    //    xhr.setRequestHeader(k, v);
    //    //});
    //    //xhr.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    //    xhr.onload = function (e) {
    //        if (this.status == 200) {
    //            var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
    //            var downloadUrl = URL.createObjectURL(blob);
    //            var a = document.createElement("a");
    //            a.href = downloadUrl;
    //            a.download = "ResumenFacturacion.xls";
    //            a.style.display = "none";
    //            document.body.appendChild(a);
    //            a.click();
    //        } else {
    //            alert('Unable to download excel.')
    //        }
    //    };
    //    xhr.send(data);
    //};
}

function ExportarGeneralFacturacion() {
    var CodSede = $(location).attr('href').split('?')[1].split('&')[2].split('=')[1].replace('#', ' ');
    var CodigoUnidadNegocio = $(location).attr('href').split('?')[1].split('&')[3].split('=')[1].replace('#', ' ');
    var NombreComercial = $(location).attr('href').split('?')[1].split('&')[4].split('=')[1].replace('#', ' ');
    $('#btnGrabarSalaGeneralModal').attr('disabled', 'disabled');

    $.ajax({
        data: '{"CodigoUnidadNegocio":"' + CodigoUnidadNegocio + '","CodSede":"' + CodSede + '"}',
        type: "POST",
        url: "../Main.aspx/BuscarConfiguracion",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg != null) {
                ExportarExcelFacturacionDetalle(msg.Ruc, msg.RazonSocial);
            }
        },
        error: function (a, b, c) {
            $('#btnGrabarSalaGeneralModal').removeAttr('disabled');
        }
    });

    var ExportarExcelFacturacionDetalle = function (RUC, RazonSocial) {

        var ValorGeneral = $('#hdTipoProduto').val();
        var data = new FormData();
        var FechaInicio = kendo.toString($("#txtFechaVentasHechaTotales").data('kendoDatePicker').value(), 'MM/dd/yyyy');
        var FechaFin = kendo.toString($("#txtFechaVentaFinTotales").data('kendoDatePicker').value(), 'MM/dd/yyyy');
        var Vendedor = '';
        var CodigoSede = $(location).attr('href').split('?')[1].split('&')[2].split('=')[1].replace('#', ' ');
        var Turno = $('#hdTurnosEmpresaTotales').val() == '' ? '0' : $('#hdTurnosEmpresaTotales').val();
        var FormaPago = $('#hdModoDePagoTotales').val() == '' ? '0' : $('#hdModoDePagoTotales').val();
        var TipoIngresoMembresia = $("#dllTipoIngresoTotales").data('kendoDropDownList').value();
        var TipoCliente = $('#hdTipoClienteTotales').val() == '' ? '0' : $('#hdTipoClienteTotales').val();
        var Counter = $('#hdCountersTotales').val() == 'Todos Counter' ? '' : $('#hdCountersTotales').val();
        var AsesorComercial = $('#hdAsesoresComercialesTotales').val() == 'Todos Vendedores' ? '' : $('#hdAsesoresComercialesTotales').val();
        var CodigoTiempoPaquete = $("#ddlTiempoMembresiaPaqueteTotales").data('kendoDropDownList').value() == '' ? '0' : $("#ddlTiempoMembresiaPaqueteTotales").data('kendoDropDownList').value();
        var CodigoUnidadNegocio = $(location).attr('href').split('?')[1].split('&')[3].split('=')[1].replace('#', ' ');

        var checksExcel = $('input[name="opcionExportar"]');
        var tipoArray = new Array();
        for (var i = 0; i < checksExcel.length; i++) {
            var itemcheck = $(checksExcel[i]).prop('checked');
            if (itemcheck) {
                tipoArray.push($(checksExcel[i]).attr('data-valor'));
            }
        }


        var entidad = {
            CodigoUnidadNegocio: CodigoUnidadNegocio,
            CodigoSede: CodigoSede,
            FechaInicio: FechaInicio,
            FechaFin: FechaFin,
            Vendedor: Vendedor == "Vendedores" ? "" : Vendedor,
            Turno: Turno,
            FormaPago: FormaPago,
            TipoIngresoMembresia: TipoIngresoMembresia,
            TipoCliente: TipoCliente,
            Counter: Counter,
            AsesorComercial: AsesorComercial == "Vendedores" ? "" : AsesorComercial,
            CodigoTiempoPaquete: CodigoTiempoPaquete
        };

        var lista = new Array();
        var listaCajaGastoUtilidad = new Array();
        lista.push({
            DescripcionCategoria: 'Membresias',
            Total: ConvertToDecimal($('#lblTotalVentaMembresias').html().toString().replace('S/.', '').trim()),
            Efectivo: ConvertToDecimal($('#lblMontoEfectivoMembresias').html().toString().replace('S/.', '').trim()),
            TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoMembresias').html().toString().replace('S/.', '').trim()),
            TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoMembresias').html().toString().replace('S/.', '').trim()),
            Deposito: ConvertToDecimal($('#lblMontoDepositoMembresias').html().toString().replace('S/.', '').trim()),
            Web: ConvertToDecimal($('#lblMontoWebMembresias').html().toString().replace('S/.', '').trim()),
            Deuda: ConvertToDecimal($('#lblDeudasMembresiasTotal').html().toString().replace('S/.', '').trim())
        });
        lista.push({
            DescripcionCategoria: 'Diario',
            Total: ConvertToDecimal($('#lblTotalVentaServicios').html().toString().replace('S/.', '').trim()),
            Efectivo: ConvertToDecimal($('#lblMontoEfectivoServicios').html().toString().replace('S/.', '').trim()),
            TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoServicios').html().toString().replace('S/.', '').trim()),
            TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoServicios').html().toString().replace('S/.', '').trim()),
            Deposito: ConvertToDecimal($('#lblMontoDepositoServicios').html().toString().replace('S/.', '').trim()),
            Web: ConvertToDecimal($('#lblMontoWebServicios').html().toString().replace('S/.', '').trim()),
            Deuda: '',
        });
        lista.push({
            DescripcionCategoria: 'Jugueria',
            Total: ConvertToDecimal($('#lblTotalVentaProductos').html().toString().replace('S/.', '').trim()),
            Efectivo: ConvertToDecimal($('#lblMontoEfectivoProductos').html().toString().replace('S/.', '').trim()),
            TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoProductos').html().toString().replace('S/.', '').trim()),
            TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoProductos').html().toString().replace('S/.', '').trim()),
            Deposito: ConvertToDecimal($('#lblMontoDepositoProductos').html().toString().replace('S/.', '').trim()),
            Web: ConvertToDecimal($('#lblMontoWebProductos').html().toString().replace('S/.', '').trim()),
            Deuda: ''
        });
        lista.push({
            DescripcionCategoria: 'Suplementos',
            Total: ConvertToDecimal($('#lblTotalVentaSuplementos').html().toString().replace('S/.', '').trim()),
            Efectivo: ConvertToDecimal($('#lblMontoEfectivoSuplementos').html().toString().replace('S/.', '').trim()),
            TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoSuplementos').html().toString().replace('S/.', '').trim()),
            TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoSuplementos').html().toString().replace('S/.', '').trim()),
            Deposito: ConvertToDecimal($('#lblMontoDepositoSuplementos').html().toString().replace('S/.', '').trim()),
            Web: ConvertToDecimal($('#lblMontoWebSuplementos').html().toString().replace('S/.', '').trim()),
            Deuda: ConvertToDecimal($('#lblDeudasSuplementosTotal').html().toString().replace('S/.', '').trim())
        });
        lista.push({
            DescripcionCategoria: 'Nutrición',
            Total: ConvertToDecimal($('#lblTotalVentaNutricion').html().toString().replace('S/.', '').trim()),
            Efectivo: ConvertToDecimal($('#lblMontoEfectivoNutricion').html().toString().replace('S/.', '').trim()),
            TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoNutricion').html().toString().replace('S/.', '').trim()),
            TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoNutricion').html().toString().replace('S/.', '').trim()),
            Deposito: ConvertToDecimal($('#lblMontoDepositoNutricion').html().toString().replace('S/.', '').trim()),
            Web: ConvertToDecimal($('#lblMontoWebNutricion').html().toString().replace('S/.', '').trim()),
            Deuda: ConvertToDecimal($('#lblDeudasNutricionTotal').html().toString().replace('S/.', '').trim())
        });
        lista.push({
            DescripcionCategoria: 'Personalizado',
            Total: ConvertToDecimal($('#lblTotalVentaPersonalizado').html().toString().replace('S/.', '').trim()),
            Efectivo: ConvertToDecimal($('#lblMontoEfectivoPersonalizado').html().toString().replace('S/.', '').trim()),
            TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoPersonalizado').html().toString().replace('S/.', '').trim()),
            TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoPersonalizado').html().toString().replace('S/.', '').trim()),
            Deposito: ConvertToDecimal($('#lblMontoDepositoPersonalizado').html().toString().replace('S/.', '').trim()),
            Web: ConvertToDecimal($('#lblMontoWebPersonalizado').html().toString().replace('S/.', '').trim()),
            Deuda: ConvertToDecimal($('#lblDeudasPersonalizadoTotal').html().toString().replace('S/.', '').trim())
        });
        lista.push({
            DescripcionCategoria: 'Ropas',
            Total: ConvertToDecimal($('#lblTotalVentaRopas').html().toString().replace('S/.', '').trim()),
            Efectivo: ConvertToDecimal($('#lblMontoEfectivoRopas').html().toString().replace('S/.', '').trim()),
            TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoRopas').html().toString().replace('S/.', '').trim()),
            TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoRopas').html().toString().replace('S/.', '').trim()),
            Deposito: ConvertToDecimal($('#lblMontoDepositoRopas').html().toString().replace('S/.', '').trim()),
            Web: ConvertToDecimal($('#lblMontoWebRopas').html().toString().replace('S/.', '').trim()),
            Deuda: ConvertToDecimal($('#lblDeudasRopasTotal').html().toString().replace('S/.', '').trim())
        });
        listaCajaGastoUtilidad.push({
            DescripcionCategoria: 'Gastos Caja',
            Total: ConvertToDecimal($('#lblTotalGasto').html().toString().replace('S/.', '').trim())
            //Efectivo: ConvertToDecimal($('#lblMontoEfectivoRopas').html().toString().replace('S/.', '').trim()),
            //TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoRopas').html().toString().replace('S/.', '').trim()),
            //TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoRopas').html().toString().replace('S/.', '').trim()),
            //Deposito: ConvertToDecimal($('#lblMontoDepositoRopas').html().toString().replace('S/.', '').trim()),
            //Web: ConvertToDecimal($('#lblMontoWebRopas').html().toString().replace('S/.', '').trim()),
            //Deuda: ConvertToDecimal($('#lblDeudasRopasTotal').html().toString().replace('S/.', '').trim())
        });
        listaCajaGastoUtilidad.push({
            DescripcionCategoria: 'Utilidad',
            Total: ConvertToDecimal($('#lblTotalUtilidad').html().toString().replace('S/.', '').trim())
            //Efectivo: ConvertToDecimal($('#lblMontoEfectivoRopas').html().toString().replace('S/.', '').trim()),
            //TarjetaDebito: ConvertToDecimal($('#lblMontoDebitoRopas').html().toString().replace('S/.', '').trim()),
            //TarjetaCredito: ConvertToDecimal($('#lblMontoCreditoRopas').html().toString().replace('S/.', '').trim()),
            //Deposito: ConvertToDecimal($('#lblMontoDepositoRopas').html().toString().replace('S/.', '').trim()),
            //Web: ConvertToDecimal($('#lblMontoWebRopas').html().toString().replace('S/.', '').trim()),
            //Deuda: ConvertToDecimal($('#lblDeudasRopasTotal').html().toString().replace('S/.', '').trim())
        });

        data.append('lista', JSON.stringify(lista));
        data.append('listaCaja', JSON.stringify(listaCajaGastoUtilidad));
        data.append('request', JSON.stringify(entidad));
        data.append('RUC', RUC);
        data.append('NombreComercial', RazonSocial);
        data.append('Tipo', tipoArray.join('|'));
        //data.append('Tipo', '0|2|4|1|5|6|7|8|9');

        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'HttpHandler/ExportarExcelFacturacionDetalle.ashx', true);
        xhr.responseType = 'blob';
        xhr.onload = function (e) {
            if (this.status == 200) {
                var blob = new Blob([this.response], { type: 'application/vnd.ms-excel' });
                var downloadUrl = URL.createObjectURL(blob);
                var a = document.createElement("a");
                a.href = downloadUrl;
                a.download = "ResumenFacturacion.xls";
                a.style.display = "none";
                document.body.appendChild(a);
                a.click();
            } else {
                alert('Unable to download excel.')
            }

            $('#btnGrabarSalaGeneralModal').removeAttr('disabled');
        };
        xhr.send(data);

    };
};