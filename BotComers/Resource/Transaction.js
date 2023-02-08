

function BuscadorClientes() {


    $("#txtTransaction_BuscadorCliente").kendoAutoComplete({
        dataTextField: "NombreCompleto",
        template: '<table border="0" style="width:100%;font-size: 9px;">' +
            '<tr>' +
            '<td style="width:20%;">' +
            '</td>' +
            '<td style="width:80%;>' +
            '<span class="k-state-default" >' +
            '<div style="font-weight: bold;font-size: 11px;"> ' +
            '#:data.Nombres# #:data.Apellidos#' +
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
                    entidad.Nombre = $('#txtTransaction_BuscadorCliente').val();
                    $.ajax({
                        async: true,
                        type: 'POST',
                        contentType: 'application/json;charset=utf-8',
                        url: '/invoice/BuscadorClientes',
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
            $('input[id="txtTransaction_CodigoCliente"]').val(dataItem.CodigoCliente);
            $('input[id="txtTransaction_hdCodigoCliente"]').val(dataItem.CodigoCliente);

            
            return false;
        }
    });

}

function RegistrarComprobantePago() {

    var Accion = 'N';//$('input[id="hdAccionItemVenta"]').val();
    var entidad = {};  
    entidad.Accion = Accion;

    //Recorrer el detalle del comprobante
    entidad.listaDetallePago = new Array();
    $('#gridComprobantes tbody tr').each(function () {
        var index = $(this).prop('id').split('_')[1];
        var detallePago = {};
        if ($('input[id="rowdi_Monto_' + index + '"]').val() != '') {
            detallePago.CodigoComprobantePago = 0;
            detallePago.CodigoComprobante = $('input[id="rowdi_CodigoComprobante_' + index + '"]').val();
            detallePago.CodigoCuentaBancaria = 0;
            detallePago.CodigoMetodoPago = $('select[id="txtTransaction_MetodoPago"] option:selected').val().split('_')[0];
            detallePago.TipoMoneda = $('select[id="txtTransaction_TipoMoneda"] option:selected').val().split('_')[0];
            detallePago.Monto = $('input[id="rowdi_Monto_' + index + '"]').val();
            detallePago.Nota = document.getElementById("txtItemsventa_Descripcion").value;
            detallePago.Estado = 1;
            detallePago.Accion = Accion;

            entidad.listaDetallePago.push(detallePago);
        }
       
    });

    $('button').attr('disabled', 'disabled');
    var metodoCorrecto = function (msg) {

        if (msg) {

            $('button').removeAttr('disabled');
            $('.alert-success').show();
            $('.alert-success').delay(4000).hide(600);
            window.location.href = "/invoice/index";
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
    LlamarAJAX("/invoice/RegistrarComprobantePago", request, metodoCorrecto, metodoError);

}





