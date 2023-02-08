
var index_row_griddi = 1;

function change_txtItemsventa_rdioTipoItem(value) {
    
    if (value == 1) { //Simple
        $('#lbl_ckboxInventariable').show('fast');
        $('#div_txtItemsventa_Almacen').hide('fast');
        if ($('#txtItemsventa_ckboxInventariable').prop('checked')) {
            document.getElementById('divDetalleInventario').style.display = '';
        } else {
            document.getElementById('divDetalleInventario').style.display = 'none';
        }
        document.getElementById('divItemsIncluidos').style.display = 'none';
    } else if (value == 2) { //Kit
        $('#lbl_ckboxInventariable').hide('fast');
        $('#div_txtItemsventa_Almacen').show('fast');
        document.getElementById('divDetalleInventario').style.display = 'none';
        document.getElementById('divItemsIncluidos').style.display = '';
    }
}

function click_ckboxInventariable() {
    
    if ($('#txtItemsventa_ckboxInventariable').prop('checked')) {
        document.getElementById('divDetalleInventario').style.display = '';
        document.getElementById('div_txtItemsventa_TipoUnidadMedida').style.display = 'none';
    } else {
        document.getElementById('divDetalleInventario').style.display = 'none';
        document.getElementById('div_txtItemsventa_TipoUnidadMedida').style.display = '';
    }
    
    if (index_row_griddi == 1) {
        Agregar_NuevaBodega();
    }
}

function ListarAlmacenes() {
    
    var metodoCorrecto = function (data) {
        var content_Almacenes = new Array();
        
        for (var i = 0; i < data.length; i++) {
            content_Almacenes.push('<option value="' + data[i].CodigoAlmacen + '">' + data[i].Descripcion + '</option>');
        }
        $('#txtItemsventa_ddlAlmacenes').html(content_Almacenes.join(' '));


        $('#txtItemsventa_ckboxInventariable').click();
        click_ckboxInventariable();

    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {

    };
    
    LlamarAJAX('/itemsventa/ListarAlmacenes', request, metodoCorrecto, metodoError);
}

function ListarCategorias() {

    var metodoCorrecto = function (data) {
        var content_Categorias = new Array();
        
        for (var i = 0; i < data.length; i++) {

            if (data[i].CodigoMenuSuperior == 0) {

                content_Categorias.push('<optgroup label="' + data[i].Descripcion +'">');

                for (var e = 0; e < data.length; e++) {
                    if (data[i].CodigoMenu == data[e].CodigoMenuSuperior) {
                        content_Categorias.push('<option value="' + data[e].CodigoMenu + '">' + data[e].Descripcion +'</option> ');                        
                    }
                }                   
                content_Categorias.push('</optgroup>');
            }

            //content_Categorias.push('<option value="' + data[i].CodigoMenu + '">' + data[i].Descripcion + '</option>');
        }
        $('#txtItemsventa_CodigoCategoria').html(content_Categorias.join(' '));
    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {

    };

    LlamarAJAX('/itemsventa/uspListarCategorias', request, metodoCorrecto, metodoError);
}

function Agregar_NuevaBodega() {
    
    var html_ddlAlmacenes = $('#txtItemsventa_ddlAlmacenes').html();
    
    var content_NBodega = new Array();

    if (index_row_griddi == 1) {
        content_NBodega.push('<tr id="rowdi_' + index_row_griddi + '">');
        content_NBodega.push('<td>');
        content_NBodega.push('<select disabled id="rowdi_ddlUnidadMedida_' + index_row_griddi + '" class="form-control form-control-sm show-tick selectpicker" data-style="btn-primary">');
        content_NBodega.push('<optgroup label="Unidad">');
        content_NBodega.push('<option value="1">Unidad</option>');
        content_NBodega.push('<option value="2">Servicio</option>');
        content_NBodega.push('<option value="3">Pieza</option>');
        content_NBodega.push('<option value="4">Millar</option>');
        content_NBodega.push('<option value="5">Kit</option>');
        content_NBodega.push('</optgroup>');
        content_NBodega.push('<optgroup label="Longitud">');
        content_NBodega.push('<option value="6">Centímetro (cm)</option>');
        content_NBodega.push('<option value="7">Metro (m)</option>');
        content_NBodega.push('<option value="8">Pulgada</option>');
        content_NBodega.push('</optgroup>');
        content_NBodega.push('<optgroup label="Área">');
        content_NBodega.push('<option value="9">Centímetro cuadrado (cm2)</option>');
        content_NBodega.push('<option value="10">Metro cuadrado (m2)</option>');
        content_NBodega.push('<option value="11">Pulgada cuadrada</option>');
        content_NBodega.push('</optgroup>');
        content_NBodega.push('<optgroup label="Volumen">');
        content_NBodega.push('<option value="12">Milimetro (mL)</option>');
        content_NBodega.push('<option value="13">Litro (L)</option>');
        content_NBodega.push('<option value="14">Galón</option>');
        content_NBodega.push('</optgroup>');
        content_NBodega.push('<optgroup label="Peso">');
        content_NBodega.push('<option value="15">Gramo (g)</option>');
        content_NBodega.push('<option value="16">Kilogramo (Kg)</option>');
        content_NBodega.push('<option value="17">Tonelada</option>');
        content_NBodega.push('<option value="18">Libra</option>');
        content_NBodega.push('</optgroup>');
        content_NBodega.push('</select>');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<input id="rowdi_txtCostoUnidad_' + index_row_griddi + '" class="form-control form-control-sm" placeholder="Input box" type="text" value="0" >');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<select id="rowdi_ddlAlmacenes_' + index_row_griddi + '" class="form-control form-control-sm" data-placeholder="">');
        content_NBodega.push(html_ddlAlmacenes);
        content_NBodega.push('</select>');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<input id="rowdi_txtCantidadInicial_' + index_row_griddi + '" class="form-control form-control-sm" placeholder="Input box" type="text" value="1">');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<input id="rowdi_txtCantidadMinima_' + index_row_griddi + '" class="form-control form-control-sm" placeholder="Input box" type="text" value="0">');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<input id="rowdi_txtCantidadMaxima_' + index_row_griddi + '" class="form-control form-control-sm" placeholder="Input box" type="text" value="0">');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('</td>');
        content_NBodega.push('</tr>');

    } else {
        content_NBodega.push('<tr id="rowdi_' + index_row_griddi + '">');
        content_NBodega.push('<td>');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<select id="rowdi_ddlAlmacenes_' + index_row_griddi + '" class="form-control form-control-sm" data-placeholder="">');
        content_NBodega.push(html_ddlAlmacenes);
        content_NBodega.push('</select>');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<input id="rowdi_txtCantidadInicial_' + index_row_griddi + '" class="form-control form-control-sm" placeholder="Input box" type="text">');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<input id="rowdi_txtCantidadMinima_' + index_row_griddi + '" class="form-control form-control-sm" placeholder="Input box" type="text">');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<input id="rowdi_txtCantidadMaxima_' + index_row_griddi + '" class="form-control form-control-sm" placeholder="Input box" type="text">');
        content_NBodega.push('</td>');
        content_NBodega.push('<td>');
        content_NBodega.push('<i class="fa fa-close tx-22" onclick="jacascript:Quitar_Bodega(' + index_row_griddi+')" ></i>');
        content_NBodega.push('</td>');
        content_NBodega.push('</tr>');
    }

    $('#gridDetalleInventario tbody').append(content_NBodega.join(' '));
    index_row_griddi++;
}

function Quitar_Bodega(index_row_griddi) {
    $('#rowdi_' + index_row_griddi).remove();
}

function RegistrarItemsVenta() {
    
        var Accion = $('input[id="hdAccionItemVenta"]').val();

        if (Accion != 'E') {
            $('input[id="hdCodigoItemVenta"]').val('0');
        }

        var entidad = {};
        entidad.CodigoItemVenta = $('input[id="hdCodigoItemVenta"]').val() == '' ? 0 : $('input[id="hdCodigoItemVenta"]').val();
        entidad.Nombre = ConvertToStringFromObject($('input[id="txtItemsventa_Nombre"]').val());
        entidad.PrecioVenta = ConvertToStringFromObject($('input[id="txtItemsventa_PrecioVenta"]').val());
        entidad.PrecioTotal = ConvertToStringFromObject($('input[id="txtItemsventa_PrecioVenta"]').val());
        entidad.CodigoTipoImpuesto = ConvertToStringFromObject($('select[id="txtItemsventa_TipoImpuesto"] option:selected').val());
        entidad.CodigoUnidadMedida = ConvertToStringFromObject($('select[id="txtItemsventa_TipoUnidadMedida"] option:selected').val());
        entidad.CodigoTipoItem = $('input:radio[name=txtItemsventa_rdioTipoItem]:checked').val();
        entidad.CodigoAlmacen = ConvertToStringFromObject($('select[id="txtItemsventa_ddlAlmacenes"] option:selected').val());
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
                if ($('input[id="rowdi_txtCantidadInicial_' + index + '"]').val() != '' && $('input[id="rowdi_txtCantidadMinima_' + index + '"]').val() != '' && $('input[id="rowdi_txtCantidadMaxima_' + index + '"]').val() != '') {
                    entidad.CodigoUnidadMedida = $('select[id="rowdi_ddlUnidadMedida_1"] option:selected').val();
                    detalleInventario.CostoUnidad = $('input[id="rowdi_txtCostoUnidad_1"]').val();
                    detalleInventario.CodigoAlmacen = $('select[id="rowdi_ddlAlmacenes_' + index + '"] option:selected').val();
                    detalleInventario.CantidadInicial = $('input[id="rowdi_txtCantidadInicial_' + index + '"]').val();
                    detalleInventario.CantidadMinima = $('input[id="rowdi_txtCantidadMinima_' + index + '"]').val();
                    detalleInventario.CantidadMaxima = $('input[id="rowdi_txtCantidadMaxima_' + index + '"]').val();
                    entidad.lista_ItemsVentaInventarioDTO.push(detalleInventario);
                }
               
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
        document.getElementById('loadMe').style.display = 'block';
        $('button').attr('disabled', 'disabled');
        var metodoCorrecto = function (msg) {
            document.getElementById('loadMe').style.display = 'none';
            //msg.Success
            if (msg) {

                //MostrarMensaje("Informacion", "Registro Guardado Correctamente");
                $('button').removeAttr('disabled');
                $('.alert-success').show();
                $('.alert-success').delay(4000).hide(600);

                $('input[id="txtItemsventa_Nombre"]').val('');
                $('input[id="txtItemsventa_PrecioVenta"]').val('');
                $('input[id="txtItemsventa_TipoImpuesto"]').val('');
                $('select[id="txtItemsventa_TipoImpuesto"]').val('');
                $('select[id="txtItemsventa_TipoUnidadMedida"]').val('');
                //$('input:radio[name=txtItemsventa_rdioTipoItem]:checked').val();
                $('select[id="txtItemsventa_ddlAlmacenes"]').val(0);
                //$('#txtItemsventa_ckboxInventariable').prop('checked') == true ? 1 : 0;
                $('input[id="txtItemsventa_Referencia"]').val('');
                $('textarea[id="txtItemsventa_Descripcion"]').val('');
                $('select[id="txtItemsventa_CodigoCuentaContable"]').val('');
                $('input[id="txtItemsventa_CodigoProductoSUNAT"]').val('');
                $('#txtItemsventa_ckboxInventariable').prop('checked', false);
                index_row_griddi = 1;
                $('#gridDetalleInventario tbody').html('');
            }
            else {
                alert("Error, vuelva a intentar nuevamente!");
            }
        };
        var metodoError = function (msg) {
            alert(msg);
        };
        var request = {
            request: entidad
        };
        LlamarAJAX("/itemsventafit/RegistrarItemsVenta", request, metodoCorrecto, metodoError);

}

function SeleccionarFilaItemsVenta(id) {
    $('input[id="hdCodigoItemVenta"]').val(id);
}

function EliminarItemsVenta() {

    var entidad = {};    
    //entidad.CodigoUnidadNegocio = CodigoUnidadNegocio;
    //entidad.CodigoSede = 0;
    entidad.CodigoItemVenta = $('input[id="hdCodigoItemVenta"]').val() == '' ? 0 : $('input[id="hdCodigoItemVenta"]').val();

    $('button').attr('disabled', 'disabled');
    var metodoCorrecto = function (msg) {
        //msg.Success
        if (msg) {

            $('button').removeAttr('disabled');
            location.reload();
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
    LlamarAJAX("/itemsventa/EliminarItemsVenta", request, metodoCorrecto, metodoError);
}

function ListarItemsVenta() {

    var Buscador = $("#txtItemsVenta_Buscador").val();
    
    var metodoCorrecto = function (data) {
        var content_A = new Array();
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {

                content_A.push('<tr>');
                content_A.push('<td>');
                content_A.push(data[i].CodigoItemVenta);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push('<img src="' + data[i].UrlImagen + '" style="width:40px;" alt="">');
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].Nombre);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].Referencia);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].PrecioVenta);
                content_A.push('</td>');
                content_A.push('<td>');
                content_A.push(data[i].Descripcion);
                content_A.push('</td>');             
                content_A.push('<td>');

                content_A.push('<a class="btn btn-primary btn-sm" href="/itemsventafit/edit/' + data[i].CodigoItemVenta + '" >Editar</a >');
                content_A.push('<a class="btn btn-primary btn-sm" href="" data-toggle="modal" data-target="#modalConfirmarEliminacion" onclick="SeleccionarFilaItemsVenta(' + data[i].CodigoItemVenta + ');">Eliminar</a>');

                //content_A.push('<i class="fa fa-eye tx-18" style="cursor:pointer;"></i>');
                //content_A.push('<a href="/itemsventafit/edit/' + data[i].CodigoItemVenta + '"><i class="fa fa-pencil tx-18" style="cursor:pointer;"></i></a>');
                //content_A.push('<i class="fa fa-plug tx-18" style="cursor:pointer;"></i>');
                //content_A.push('<a href="" data-toggle="modal" data-target="#modalConfirmarEliminacion" onclick="SeleccionarFilaItemsVenta(' + data[i].CodigoItemVenta + ');"><i class="fa fa-close tx-18" style="cursor:pointer;"></i></a>');

                content_A.push('</td>');
                content_A.push('</tr>');

            }
            $('#gridEmpresa tbody').html(content_A.join(' '));
        } else {
            $('#gridEmpresa tbody').html('');
        }

    };

    var metodoError = function (msg) {
        alert(msg);
    };
    var request = {
        Buscador: Buscador      
    };

    LlamarAJAX('/itemsventa/ListarItemsVenta', request, metodoCorrecto, metodoError);
}
