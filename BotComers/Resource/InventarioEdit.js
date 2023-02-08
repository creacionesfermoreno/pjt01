

function Agregar_NuevaBodega_Edit() {

    var index_row_griddi_edit = 1;
    $('#gridDetalleInventario tbody tr').each(function () {
        index_row_griddi_edit++;
    });

    var html_ddlAlmacenes = $('#Itemsventa_ddlAlmacenes').html();
    
    var content_NBodega_Edit = new Array();

    content_NBodega_Edit.push('<tr id="rowdi_' + index_row_griddi_edit + '">');
    content_NBodega_Edit.push('<td>');
    content_NBodega_Edit.push('</td>');
    content_NBodega_Edit.push('<td>');    
    content_NBodega_Edit.push('</td>');
    content_NBodega_Edit.push('<td>');
    content_NBodega_Edit.push('<input id="rowdi_hdCodigoItemsVentaInventario_' + index_row_griddi_edit + '" class="form-control form-control-sm" type="hidden" name="" value="0">');
    content_NBodega_Edit.push('</td>');    
    content_NBodega_Edit.push('<td>');
    content_NBodega_Edit.push('<select id="rowdi_ddlAlmacenes_' + index_row_griddi_edit + '" class="form-control form-control-sm" data-placeholder="">');
    content_NBodega_Edit.push(html_ddlAlmacenes);
    content_NBodega_Edit.push('</select>');
    content_NBodega_Edit.push('</td>');
    content_NBodega_Edit.push('<td>');
    content_NBodega_Edit.push('<input id="rowdi_txtCantidadInicial_' + index_row_griddi_edit + '" class="form-control form-control-sm" placeholder="Input box" type="text">');
    content_NBodega_Edit.push('</td>');
    content_NBodega_Edit.push('<td>');
    content_NBodega_Edit.push('<input id="rowdi_txtCantidadMinima_' + index_row_griddi_edit + '" class="form-control form-control-sm" placeholder="Input box" type="text">');
    content_NBodega_Edit.push('</td>');
    content_NBodega_Edit.push('<td>');
    content_NBodega_Edit.push('<input id="rowdi_txtCantidadMaxima_' + index_row_griddi_edit + '" class="form-control form-control-sm" placeholder="Input box" type="text">');
    content_NBodega_Edit.push('</td>');
    content_NBodega_Edit.push('<td>');
    content_NBodega_Edit.push('<i class="fa fa-close tx-22" onclick="jacascript:Quitar_Bodega(' + index_row_griddi_edit + ')" ></i>');
    content_NBodega_Edit.push('</td>');
    content_NBodega_Edit.push('</tr>');

    $('#gridDetalleInventario tbody').append(content_NBodega_Edit.join(' '));
    index_row_griddi_edit++;
}

function Quitar_Bodega(index_row_griddi) {
    $('#rowdi_' + index_row_griddi).remove();
}

function ActualizarItemsVenta() {

    var Accion = $('input[id="hdAccionItemVenta"]').val();
    
    var entidad = {};
    
    entidad.CodigoItemVenta = $('input[id="hdCodigoItemVenta"]').val();
    entidad.Nombre = ConvertToStringFromObject($('input[id="txtItemsventa_Nombre"]').val());
    entidad.PrecioVenta = ConvertToStringFromObject($('input[id="txtItemsventa_PrecioVenta"]').val());
    entidad.PrecioTotal = ConvertToStringFromObject($('input[id="txtItemsventa_PrecioVenta"]').val());
    entidad.CodigoTipoImpuesto = ConvertToStringFromObject($('select[id="txtItemsventa_TipoImpuesto"] option:selected').val());
    entidad.CodigoUnidadMedida = ConvertToStringFromObject($('select[id="txtItemsventa_TipoUnidadMedida"] option:selected').val());
    entidad.CodigoTipoItem = $('input:radio[name=txtItemsventa_rdioTipoItem]:checked').val();
    entidad.CodigoAlmacen = ConvertToStringFromObject($('select[id="txtItemsventa_ddlAlmacenes"] option:selected').val()) == '' ? 0 : ConvertToStringFromObject($('select[id="txtItemsventa_ddlAlmacenes"] option:selected').val());
    entidad.ItemInventariable = $('#txtItemsventa_ckboxInventariable').prop('checked') == true ? 1 : 0;
    entidad.Referencia = ConvertToStringFromObject($('input[id="txtItemsventa_Referencia"]').val());
    entidad.Descripcion = ConvertToStringFromObject($('textarea[id="txtItemsventa_Descripcion"]').val());
    entidad.CodigoCategoriaItem = ConvertToStringFromObject($('select[id="txtItemsventa_CodigoCategoria"] option:selected').val());
    entidad.CodigoProductoSUNAT = ConvertToStringFromObject($('input[id="txtItemsventa_CodigoProductoSUNAT"]').val());
    entidad.CodigoCuentaContable = ConvertToStringFromObject($('input[id="txtItemsventa_CodigoCuentaContable"]').val());
    entidad.UrlImagen = '';
    entidad.Estado = 1;
    entidad.VisualizarTiendaVirtual = $('#txtItemsventa_ckboxVisualizarTiendaVirtual').prop('checked') == true ? 1 : 0;
    entidad.Accion = Accion;

    //Recorrer el inventario del itemsVenta
    if (entidad.CodigoTipoItem == 1 && entidad.ItemInventariable == 1) {
        entidad.lista_ItemsVentaInventarioDTO = new Array();
       
        $('#gridDetalleInventario tbody tr').each(function () {
            var index = $(this).prop('id').split('_')[1];
            var detalleInventario = {};
            
            detalleInventario.CodigoItemVenta = $('input[id="hdCodigoItemVenta"]').val();
            detalleInventario.CodigoItemsVentaInventario = $('input[id="rowdi_hdCodigoItemsVentaInventario_' + index + '"]').val();
            entidad.CodigoUnidadMedida = $('select[id="rowdi_ddlUnidadMedida_1"] option:selected').val();
            detalleInventario.CostoUnidad = $('input[id="rowdi_txtCostoUnidad_1"]').val();
            detalleInventario.CodigoAlmacen = $('select[id="rowdi_ddlAlmacenes_' + index + '"] option:selected').val() == undefined ? 0 : $('select[id="rowdi_ddlAlmacenes_' + index + '"] option:selected').val();
            detalleInventario.CantidadInicial = $('input[id="rowdi_txtCantidadInicial_' + index + '"]').val();
            detalleInventario.CantidadMinima = $('input[id="rowdi_txtCantidadMinima_' + index + '"]').val();
            detalleInventario.CantidadMaxima = $('input[id="rowdi_txtCantidadMaxima_' + index + '"]').val();
            entidad.lista_ItemsVentaInventarioDTO.push(detalleInventario);

        });

    }
    
    if (IsUndefinedOrNullOrEmpty(entidad.Nombre)) {
        $('#txtItemsventa_Nombre_validation').show();
        $('#txtItemsventa_Nombre_validation').delay(4000).hide(600);
        return;
    }
    else if ($('input[id="txtItemsventa_PrecioVenta"]').val() == '') {
        $('#txtItemsventa_PrecioVenta_validation').show();
        $('#txtItemsventa_PrecioVenta_validation').delay(4000).hide(600);
        return;
    }

    $('button').attr('disabled', 'disabled');
    var metodoCorrecto = function (msg) {
        //msg.Success
        $('button').removeAttr('disabled');
        $('.alert-success').show();
        $('.alert-success').delay(4000).hide(600);
        
    };
    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        request: entidad
    };

    LlamarAJAX("/itemsventa/RegistrarItemsVenta", request, metodoCorrecto, metodoError);

}


