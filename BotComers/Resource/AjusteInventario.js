
var index_row_griddi = 1;
var totalAjustado = 0;

function ListarAlmacenes() {
    
    var metodoCorrecto = function (data) {
        var content_Almacenes = new Array();

        for (var i = 0; i < data.length; i++) {
            content_Almacenes.push('<option value="' + data[i].CodigoAlmacen + '">' + data[i].Descripcion + '</option>');
        }
        $('#txtInventarioAjuste_ddlAlmacenes').html(content_Almacenes.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
    };

    LlamarAJAX('/inventarioajuste/ListarAlmacenes', request, metodoCorrecto, metodoError);
}

function Agregar_NuevoItem() {
    
    var content_NBodega = new Array();

    content_NBodega.push('<tr id="rowdi_' + index_row_griddi + '">');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_ddlProducto_' + index_row_griddi + '" class="form-control form-control-sm wd-300" type="text" onclick="click_idbuscador(this);" onkeypress="click_idbuscador(this);" >');    
    content_NBodega.push('<input value="0" id="rowdi_hdCodigoProducto_' + index_row_griddi + '" type="hidden" />');
    content_NBodega.push('</td>');  
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtCantidadActual_' + index_row_griddi + '" class="form-control form-control-sm wd-60" type="text" disabled>');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<select id="rowdi_ddlTipoAjuste_' + index_row_griddi + '" onchange="change_cantidadAjuste(this)" class="form-control form-control-sm wd-150" data-style="btn-primary">');   
    content_NBodega.push('<option value="1">Incremento</option>');
    content_NBodega.push('<option value="2">Disminución</option>');
    content_NBodega.push('</select>');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtCantidad_' + index_row_griddi + '" class="form-control form-control-sm wd-60" type="number" value="0" min="0" onkeypress="change_cantidadAjuste(this)" onkeydown="change_cantidadAjuste(this)" onkeyup="change_cantidadAjuste(this)" oncuechange="change_cantidadAjuste(this)" onclick="change_cantidadAjuste(this)" >');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtCantidadFinal_' + index_row_griddi + '" class="form-control form-control-sm wd-60" type="text" disabled>');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtCostoUnitario_' + index_row_griddi + '" class="form-control form-control-sm wd-60" type="number" value="0" onkeypress="change_cantidadAjuste(this)" onkeydown="change_cantidadAjuste(this)" onkeyup="change_cantidadAjuste(this)" oncuechange="change_cantidadAjuste(this)" onclick="change_cantidadAjuste(this)" >');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<input id="rowdi_txtTotalAjustado_' + index_row_griddi + '" class="form-control form-control-sm wd-60" type="text" disabled>');
    content_NBodega.push('</td>');
    content_NBodega.push('<td>');
    content_NBodega.push('<i style="color:#0075ff;" class="fa fa-trash-alt tx-22 wd-20" onclick="jacascript:Quitar_ItemVenta(' + index_row_griddi + ')" ></i>');
    content_NBodega.push('</td>');
    content_NBodega.push('</tr>');

    $('#gridAjusteInventario tbody').append(content_NBodega.join(' '));
   
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
            '#:data.Nombre#  #:data.Descripcion#' +
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
                    entidad.CodigoAlmacen = ConvertToStringFromObject($('select[id="txtInventarioAjuste_ddlAlmacenes"] option:selected').val());
                    entidad.Nombre = $('#' + $('#hdIDBuscadorItemsVenta').val()).val();

                    $.ajax({
                        async: true,
                        type: 'POST',
                        contentType: 'application/json;charset=utf-8',
                        url: '/inventarioajuste/BuscadorItemsVentaInventariable',
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
            //alert($('#hdIDBuscadorItemsVenta').val());
            var dataItem = this.dataItem(e.item.index());
            var index = $('#hdIDBuscadorItemsVenta').val().split('_')[2];
            
            $('#rowdi_hdCodigoProducto_' + index).val(dataItem.CodigoItemVenta);
            $('#rowdi_txtCantidadActual_' + index).val(dataItem.d_CantidadActual);
            $('#rowdi_txtCostoUnitario_' + index).val(dataItem.d_CostoUnidad);

            //Calcular cantidad final
            var cantidadActual = $('#rowdi_txtCantidadActual_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCantidadActual_' + index).val()).toFixed(2);
            var cantidad = $('#rowdi_txtCantidad_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCantidad_' + index).val()).toFixed(2);
            var tipoAjuste = ConvertToStringFromObject($('select[id="rowdi_ddlTipoAjuste_' + index + '"] option:selected').val());
            var cantidadFinal = 0;
            if (tipoAjuste == 1) { //incremento
                cantidadFinal = cantidadActual + cantidad;
            } else { //disminucion
                cantidadFinal = cantidadActual - cantidad;
            }
            $('#rowdi_txtCantidadFinal_' + index).val(parseFloat(cantidadFinal).toFixed(2));
            //Calcular Total Ajustado
            var costounitario = $('#rowdi_txtCostoUnitario_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCostoUnitario_' + index).val()).toFixed(2);
            var totalajustado = cantidad * costounitario;
            $('#rowdi_txtTotalAjustado_' + index).val(parseFloat(totalajustado).toFixed(2));   
                
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

function change_cantidadAjuste(control) {
    var ID = $(control).prop('id');
    var index = ID.split('_')[2];

    //Calcular cantidad final
    var cantidadActual = $('#rowdi_txtCantidadActual_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCantidadActual_' + index).val()).toFixed(2);
    var cantidad = $('#rowdi_txtCantidad_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCantidad_' + index).val()).toFixed(2);
    var tipoAjuste = ConvertToStringFromObject($('select[id="rowdi_ddlTipoAjuste_' + index +'"] option:selected').val());
    var cantidadFinal = 0;
    if (tipoAjuste == 1) { //incremento
        cantidadFinal = parseFloat(cantidadActual) + parseFloat(cantidad);
    } else if (tipoAjuste == 2) { //disminucion
        cantidadFinal = parseFloat(cantidadActual) - parseFloat(cantidad);
    }
   // alert('cantidadActual: ' + cantidadActual + ' cantidad: ' + cantidad + ' cantidadFinal: ' + cantidadFinal);
    $('#rowdi_txtCantidadFinal_' + index).val(parseFloat(cantidadFinal).toFixed(2));

    //Calcular Total Ajustado
    var costounitario = $('#rowdi_txtCostoUnitario_' + index).val() == '' ? 0 : parseFloat($('#rowdi_txtCostoUnitario_' + index).val()).toFixed(2);
    var totalajustado = cantidad * costounitario;
    $('#rowdi_txtTotalAjustado_' + index).val(parseFloat(totalajustado).toFixed(2));
    calcular_totalajustado();
}

function calcular_totalajustado() {
    totalAjustado = 0;
    $('#gridAjusteInventario tbody tr').each(function () {
        var index = $(this).prop('id').split('_')[1];
        var row_totalajustado = $('input[id="rowdi_txtTotalAjustado_' + index + '"]').val() == '' ? 0 : $('input[id="rowdi_txtTotalAjustado_' + index + '"]').val();
        totalAjustado = totalAjustado + parseFloat(row_totalajustado);     
    });
    $('#txtInventarioAjuste_TotalAjuste').html(parseFloat(totalAjustado).toFixed(2));
}

function RegistrarAjusteInventario() {

    var Accion = 'N';//$('input[id="hdAccionItemVenta"]').val();
    
    var entidad = {};
   
    entidad.CodigoItemsVentaAjusteInventario = 0;
    entidad.CodigoAlmacen = ConvertToStringFromObject($('select[id="txtInventarioAjuste_ddlAlmacenes"] option:selected').val());
    entidad.FechaAjuste = kendo.toString($("#txtInventarioAjuste_FechaRealizacion").data('kendoDatePicker').value(), 'MM/dd/yyyy hh:mm:ss tt');
   
    entidad.Observaciones = document.getElementById("txtInventarioAjuste_Observaciones").value;
    entidad.TotalAjuste = $('#txtInventarioAjuste_TotalAjuste').html();
    entidad.Estado = 1;
    entidad.Accion = Accion;

    if (entidad.Observaciones == '') {
        alert("Falta ingresar una observación del ajuste.");
        return;
    }
    //else if (entidad.TotalAjuste == '' || entidad.TotalAjuste == 0) {
    //    alert("Falta agregar un producto para el ajuste.");
    //    return;
    //}

    //Recorrer el inventario del items Venta Ajuste Inventario
    entidad.listaDetalle = new Array();
    $('#gridAjusteInventario tbody tr').each(function () {
        var index = $(this).prop('id').split('_')[1];
        var detalleAjuste = {};
        
        detalleAjuste.CodigoItemsVentaAjusteInventario = 0;
        detalleAjuste.CodigoItemsVentaAjusteInventarioDetalle = 0;
        detalleAjuste.CodigoItemVenta = $('input[id="rowdi_hdCodigoProducto_' + index + '"]').val();
        detalleAjuste.CantidadActual = $('input[id="rowdi_txtCantidadActual_' + index + '"]').val();
        detalleAjuste.CodigoTipoAjuste = $('select[id="rowdi_ddlTipoAjuste_' + index + '"] option:selected').val();
        detalleAjuste.CantidadAjuste = $('input[id="rowdi_txtCantidad_' + index + '"]').val();
        detalleAjuste.CantidadFinal = $('input[id="rowdi_txtCantidadFinal_' + index + '"]').val();
        detalleAjuste.CostoUnidad = $('input[id="rowdi_txtCostoUnitario_' + index + '"]').val();
        detalleAjuste.TotalAjuste = $('input[id="rowdi_txtTotalAjustado_' + index + '"]').val();

        if (detalleAjuste.CantidadActual == '') {
            alert("Falta seleccionar un producto");
            return;
        } else if (detalleAjuste.CantidadAjuste == '' || detalleAjuste.CantidadAjuste == 0) {
            alert("Falta ingresar la cantidad");
            return;
        }
        //else if (detalleAjuste.CostoUnidad == '' || detalleAjuste.CostoUnidad == 0) {
        //    alert("Falta ingresar el costo unitario");
        //    return;
        //}

        entidad.listaDetalle.push(detalleAjuste);
    });
    
    $('button').attr('disabled', 'disabled');
    var metodoCorrecto = function (msg) {
        
        if (msg) {
          
            $('button').removeAttr('disabled');
            $('.alert-success').show();
            $('.alert-success').delay(4000).hide(600);

            $('input[id="txtInventarioAjuste_Observaciones"]').val('');
            $('input[id="txtInventarioAjuste_TotalAjuste"]').html('0.00');

            document.getElementById("btnRegresar").click();
            $('#gridAjusteInventario tbody tr').each(function () {
                $(this).remove();               
            });
            
        }
        else {
            alert("vuelva a intentar nuevamente!");            
        }
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };
    LlamarAJAX("/inventarioajuste/RegistrarAjusteInventario", request, metodoCorrecto, metodoError);

}


