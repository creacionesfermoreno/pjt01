var index_row_griddi = 1;
var SubTotal1 = 0;
var DescuentoTotal = 0;
var SubTotal2 = 0;
var IGV = 0;
var Total = 0;

function buscarHistorialInvoice() {

    var entidad = {};
    entidad.PageNumber = 1;
    entidad.b_FechaEmisionInicio = kendo.toString($("#txtInvoice_FechaInicio").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    entidad.b_FechaEmisionFin = kendo.toString($("#txtInvoice_FechaFin").data('kendoDatePicker').value(), 'MM/dd/yyyy');
    entidad.CodigoEstadoEntrega = ConvertToStringFromObject($('select[id="ddlEstadoEntrega"] option:selected').val());

    $('#gridInvoice tbody').html('');
    var metodoCorrecto = function (data) {

        var Total = 0.0;
        var Cobrado = 0.0;
        var PorCobrar = 0.0;

        var content_A = new Array();
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                
                content_A.push('<tr>');
                content_A.push('<td>');
                content_A.push(data[i].Correlativo);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].NombresCliente);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].DesFechaEmision);
                content_A.push('</td>');              
                content_A.push('<td>');
                content_A.push(data[i].Total);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].TotalCobrado);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].TotalPorCobrar);
                content_A.push('</td>');
                content_A.push('<td style="color:' + data[i].ColorEstado + ';">');
                content_A.push(data[i].DesEstado);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].DesEstadoEntrega);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push('<i class="fa fa-eye tx-18" style="cursor:pointer;"></i>');
                
                content_A.push('<o style="display:' + (data[i].Estado == 4 ? "block" : "none") +'">');
                content_A.push('<i class="icon ion-unlocked tx-18" style="cursor:pointer;" title="Facturar y emitir"></i>');
                content_A.push('</o>');

                content_A.push('<o style="display:' + (data[i].TotalPorCobrar > 0 ? "block" : "none") + '">');
                content_A.push('<a href="/invoice/transaction?idc=' + data[i].CodigoCliente + '&idco=' + data[i].CodigoComprobante + '" title="Pagar"><i class="fa fa-money tx-18" style="cursor:pointer;"></i></a>');
                content_A.push('</o>');

                content_A.push('<o style="display:' + (data[i].Estado == 1 ? "block" : "none") + '">');
                content_A.push('<a href="" title="Anular Factura" onclick="SeleccionarFilaItemsVenta(' + data[i].CodigoComprobante +');"><i class="fa fa-close tx-18" style="cursor:pointer;"></i></a>');
                content_A.push('</o >');

                content_A.push('<o style="display:' + (data[i].Estado == 4 ? "block" : "none") + '">');
                content_A.push('<a href="" title="Eliminar" onclick="SeleccionarFilaItemsVenta(' + data[i].CodigoComprobante + ');"><i class="fa fa-close tx-18" style="cursor:pointer;"></i></a>');
                content_A.push('</o >');

                content_A.push('</td>');
                content_A.push('</tr>');

                Total += data[i].Total;
                Cobrado += data[i].TotalCobrado;
                PorCobrar += data[i].TotalPorCobrar;
            }
            
            $('#gridInvoice tbody').html(content_A.join(' '));
            var content_tfoot = new Array();
            content_tfoot.push('    <tr>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td style="text-align:right;">Totales:</td>');
            content_tfoot.push('        <td>' + Total + '</td>');
            content_tfoot.push('        <td>' + Cobrado +'</td>');
            content_tfoot.push('        <td>' + PorCobrar +'</td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('    </tr>');            
            $('#gridInvoice tfoot').html(content_tfoot.join(' '));
        } else {
            $('#gridInvoice tbody').html('');
            var content_tfoot = new Array();           
            content_tfoot.push('    <tr>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td style="text-align:right;">Totales:</td>');
            content_tfoot.push('        <td>0.00</td>');
            content_tfoot.push('        <td>0.00</td>');
            content_tfoot.push('        <td>0.00</td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('        <td></td>');
            content_tfoot.push('    </tr>');
            $('#gridInvoice tfoot').html(content_tfoot.join(' '));
        }

    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/invoice/ListarComprobante_Paginacion', request, metodoCorrecto, metodoError);
}

function ListarAlmacenes() {

    var metodoCorrecto = function (data) {
        var content_Almacenes = new Array();

        for (var i = 0; i < data.length; i++) {
            content_Almacenes.push('<option value="' + data[i].CodigoAlmacen + '">' + data[i].Descripcion + '</option>');
        }
        $('#txtInvoice_ddlAlmacenes').html(content_Almacenes.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
    };

    LlamarAJAX('/invoice/ListarAlmacenes', request, metodoCorrecto, metodoError);
}

function ListarTipoComprobante() {

    var entidad = {};
    entidad.CodigoTipoDocumentoEmpresa = $('#hdCodigoTipoComprobante').val();
    
    var metodoCorrecto = function (data) {
        var content_TipoComprobante = new Array();

        for (var i = 0; i < data.length; i++) {
            if (i == 0) {
                $('#txtInvoice_Correlativo').val(data[i].Serie.split('_')[1]);
            }
            content_TipoComprobante.push('<option value="' + data[i].Serie + '">' + data[i].Nombre + '</option>');
        }
        $('#txtInvoice_ddlTipoComprobante').html(content_TipoComprobante.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX('/invoice/ListarTipoComprobante', request, metodoCorrecto, metodoError);
}

function Agregar_NuevoItem() {

    var content_NBodega = new Array();

    content_NBodega.push('<tr id="rowdi_' + index_row_griddi + '">');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_ddlProducto_' + index_row_griddi + '" style="width:99%;" class="form-control form-control-sm wd-200" type="text" onclick="click_idbuscador(this);" onkeypress="click_idbuscador(this);" >');
    content_NBodega.push('<input value="0" id="rowdi_hdCodigoProducto_' + index_row_griddi + '" type="hidden" />');
    content_NBodega.push('<input value="0" id="rowdi_hdCantidadActual_' + index_row_griddi + '" type="hidden" />');
    content_NBodega.push('</td>');
    
    content_NBodega.push('<td>');
    content_NBodega.push('<select id="rowdi_ddlTipoImpuesto_' + index_row_griddi + '" class="form-control form-control-sm wd-150" data-style="btn-primary" disabled>');
    content_NBodega.push('<option value="1">Ninguno - (0%)</option>');
    content_NBodega.push('<option value="2">IGV - (18%)</option>');
    content_NBodega.push('<option value="3">Exonerado - (0%)</option>');
    content_NBodega.push('</select>');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtDescripcion_' + index_row_griddi + '" class="form-control form-control-sm wd-80" type="text">');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtPrecio_' + index_row_griddi + '" class="form-control form-control-sm wd-80 tx-center" type="text" onkeypress="change_cantidad(this)" onkeydown="change_cantidad(this)" onkeyup="change_cantidad(this)" onclick="change_cantidad(this)" >');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtDescuento_' + index_row_griddi + '" class="form-control form-control-sm wd-80" type="text" value="0" disabled >');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtCantidad_' + index_row_griddi + '" class="form-control form-control-sm wd-80 tx-right" type="number" value="1" min="0" onkeypress="change_cantidad(this)" onkeydown="change_cantidad(this)" onkeyup="change_cantidad(this)" onclick="change_cantidad(this)" >');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtTotal_' + index_row_griddi + '" class="form-control form-control-sm wd-80 tx-right" type="text" disabled>');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<i class="fa fa-close tx-22 wd-20" onclick="jacascript:Quitar_ItemVenta(' + index_row_griddi + ')" ></i>');
    content_NBodega.push('</td>');
    content_NBodega.push('</tr>');

    $('#gridInvoice tbody').append(content_NBodega.join(' '));

    $("#rowdi_ddlProducto_" + index_row_griddi).kendoAutoComplete({
        dataTextField: "Nombre",
        template: '<table border="0" style="width:100%;font-size: 9px;">' +
            '<tr>' +
            '<td style="width:20%;">' +
            '<img src=\"#:data.UrlImagen#\" data_ruta=\"#:data.UrlImagen#\" style="position: relative;width:40px;height:40px;cursor:pointer" class="img-circle img-focus" />' +
            '</td>' +
            '<td style="width:80%;>' +
            '<span class="k-state-default" >' +
            '<div style="font-weight: bold;font-size: 11px;"> ' +
            '#:data.Nombre# stock #:data.d_CantidadActual#' +
            '</div>' +
            '</span>' +
            '</td>' +
            '</tr>' +
            '</table>',
        filter: "startswith",
        minLength: 3,
        height: 250,
        cache: false,
        dataSource: {
            serverFiltering: true,
            transport: {
                read: function (options) {

                    var entidad = {};
                    entidad.CodigoAlmacen = ConvertToStringFromObject($('select[id="txtInvoice_ddlAlmacenes"] option:selected').val());
                    entidad.Nombre = $('#' + $('#hdIDBuscadorItemsVenta').val()).val();

                    $.ajax({
                        async: true,
                        type: 'POST',
                        contentType: 'application/json;charset=utf-8',
                        url: '/invoice/BuscadorItemsVentaInventariable',
                        data: JSON.stringify(entidad),
                        dataType: 'json',
                        success: function (message) {
                            options.success(message);
                        },
                        error: function (message, error, cod) {

                        },
                        complete: function () {

                        }
                    });

                }
            }
        },
        select: function (e) {
            
            var dataItem = this.dataItem(e.item.index());
            var index = $('#hdIDBuscadorItemsVenta').val().split('_')[2];
           
            $('#rowdi_hdCodigoProducto_' + index).val(dataItem.CodigoItemVenta);
            $('#rowdi_hdCantidadActual_' + index).val(dataItem.d_CantidadActual);
            //$('#rowdi_txtReferencia_' + index).val(dataItem.Referencia);
            $('#rowdi_txtDescripcion_' + index).val(dataItem.Descripcion);
            $('select[id="rowdi_ddlTipoImpuesto_' + index + '"]').val(dataItem.CodigoTipoImpuesto);
            $('#rowdi_txtPrecio_' + index).val(dataItem.PrecioTotal);

            var cantidadActual = $('#rowdi_hdCantidadActual_' + index).val() == '' ? 0 : parseFloat($('#rowdi_hdCantidadActual_' + index).val()).toFixed(0);
            var cantidad = $('#rowdi_txtCantidad_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCantidad_' + index).val()).toFixed(0);
            var precio = $('#rowdi_txtPrecio_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtPrecio_' + index).val()).toFixed(2);
           
            //Calcular Total
            var total = cantidad * precio;
            $('#rowdi_txtTotal_' + index).val(parseFloat(total).toFixed(2));

            calcular_total();
            return false;
        }
    });

    index_row_griddi++;
}

function Quitar_ItemVenta(index_row_griddi) {
    $('#rowdi_' + index_row_griddi).remove();
}

function click_idbuscador(control) {
    var ID = $(control).prop('id');
    $('#hdIDBuscadorItemsVenta').val(ID);
}

function change_cantidad(control) {
    var ID = $(control).prop('id');
    var index = ID.split('_')[2];

    var cantidadActual = $('#rowdi_hdCantidadActual_' + index).val() == '' ? 0 : parseFloat($('#rowdi_hdCantidadActual_' + index).val()).toFixed(0);
    var cantidad = $('#rowdi_txtCantidad_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCantidad_' + index).val()).toFixed(0);
    var precio = $('#rowdi_txtPrecio_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtPrecio_' + index).val()).toFixed(2);
       
    //Calcular Total
    var total = cantidad * precio;
    $('#rowdi_txtTotal_' + index).val(parseFloat(total).toFixed(2));
    calcular_total();
}

function calcular_total() {
    SubTotal1 = 0;
    DescuentoTotal = 0;
    SubTotal2 = 0;
    IGV = 0;
    Total = 0;
    $('#gridInvoice tbody tr').each(function () {
        var index = $(this).prop('id').split('_')[1];
        var row_total = $('input[id="rowdi_txtTotal_' + index + '"]').val() == '' ? 0 : $('input[id="rowdi_txtTotal_' + index + '"]').val();
        SubTotal1 = SubTotal1 + parseFloat(row_total);
    });
    SubTotal2 = parseFloat(SubTotal1) - parseFloat(DescuentoTotal);
    IGV = SubTotal2 * 0.18;
    Total = SubTotal2;

    $('#tdSubTotal1').html(parseFloat(SubTotal1).toFixed(2));
    $('#tdDescuento').html(parseFloat(DescuentoTotal).toFixed(2));
    $('#tdSubTotal2').html(parseFloat(SubTotal2).toFixed(2));
    $('#tdIGV').html(parseFloat(IGV).toFixed(2));
    $('#tdTotal').html(parseFloat(Total).toFixed(2));
    
}

function Click_TipoComprobante(valor) {
    $('#hdCodigoTipoComprobante').val(valor);
    if (valor == 1) {
        $('#btnTipoComprobanteFactura').addClass('active');
        $('#btnTipoComprobanteBoleta').removeClass('active');
        $('#btnTipoComprobanteRecibo').removeClass('active');
    } else if (valor == 2) {
        $('#btnTipoComprobanteBoleta').addClass('active');
        $('#btnTipoComprobanteFactura').removeClass('active');
        $('#btnTipoComprobanteRecibo').removeClass('active');
    } else if (valor == 5) {
        $('#btnTipoComprobanteRecibo').addClass('active');
        $('#btnTipoComprobanteBoleta').removeClass('active');
        $('#btnTipoComprobanteFactura').removeClass('active');
    }

    ListarTipoComprobante();
}

function change_TipoComprobante() {
    var serie = $('select[id="txtInvoice_ddlTipoComprobante"] option:selected').val().split('_')[1];
    $('#txtInvoice_Correlativo').val(serie);
}

function RegistrarComprobante() {

    var Accion = 'N';//$('input[id="hdAccionItemVenta"]').val();

    var entidad = {};
    
    entidad.CodigoTipoComprobante = $('select[id="txtInvoice_ddlTipoComprobante"] option:selected').val().split('_')[0];
    entidad.TipoMoneda = ConvertToStringFromObject($('select[id="txtInvoice_TipoMoneda"] option:selected').val());
    entidad.CodigoAlmacen = ConvertToStringFromObject($('select[id="txtInvoice_ddlAlmacenes"] option:selected').val());
    entidad.Correlativo = $('#txtInvoice_Correlativo').val();
    entidad.CodigoCliente = $('#txtInvoice_CodigoCliente').val();
    entidad.CodigoVendedor = 0;
    entidad.FechaEmision = kendo.toString($("#txtInvoice_FechaFactura").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    entidad.CodigoPlazoPago = ConvertToStringFromObject($('select[id="txtInvoice_PlazoPago"] option:selected').val());
    entidad.FechaVencimiento = kendo.toString($("#txtInvoice_Vencimiento").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
    entidad.TerminosCondiciones = document.getElementById("txtInvoice_Terminos").value;
    entidad.Notas = document.getElementById("txtInvoice_Notas").value;
    entidad.Comentarios = "";
    entidad.SubTotal = $('#tdSubTotal1').html();
    entidad.Descuento = $('#tdDescuento').html();
    entidad.SubTotal2 = $('#tdSubTotal2').html();
    entidad.IGV = $('#tdIGV').html();
    entidad.Total = $('#tdTotal').html();
    entidad.Estado = 1;
    entidad.Accion = Accion;
    
    //Recorrer el detalle del comprobante
    entidad.listaDetalle = new Array();
    $('#gridInvoice tbody tr').each(function () {
        var index = $(this).prop('id').split('_')[1];
        var detalleAjuste = {};

        detalleAjuste.CodigoItemsVenta = $('input[id="rowdi_hdCodigoProducto_' + index + '"]').val();
        detalleAjuste.Referencia = "";//$('input[id="rowdi_txtReferencia_' + index + '"]').val();
        detalleAjuste.Precio = $('input[id="rowdi_txtPrecio_' + index + '"]').val();
        detalleAjuste.Descuento = $('input[id="rowdi_txtDescuento_' + index + '"]').val();
        detalleAjuste.CodigoTipoImpuesto = $('select[id="rowdi_ddlTipoImpuesto_' + index + '"] option:selected').val();
        detalleAjuste.Descripcion = $('input[id="rowdi_txtDescripcion_' + index + '"]').val();
        detalleAjuste.Cantidad = $('input[id="rowdi_txtCantidad_' + index + '"]').val();
        detalleAjuste.Total = $('input[id="rowdi_txtTotal_' + index + '"]').val();
        
        entidad.listaDetalle.push(detalleAjuste);
    });

    $('button').attr('disabled', 'disabled');
    var metodoCorrecto = function (msg) {

        if (msg) {

            $('button').removeAttr('disabled');
            $('.alert-success').show();
            $('.alert-success').delay(4000).hide(600);
            
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
    LlamarAJAX("/invoice/RegistrarComprobante", request, metodoCorrecto, metodoError);

}




