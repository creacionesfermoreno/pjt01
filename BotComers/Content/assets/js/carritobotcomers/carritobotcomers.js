
$(document).ready(function () {
  
    ListarProductosLocalStorage();
});

function comprarProducto(obj) {
    //var CargarDatosLocal();
    //e.preventDefault();
    // alert('hola mundo');
    debugger;
    if ($(obj).hasClass('agregar-carrito')) {
        var producto = $(obj).parent();
        //obtener valores del producto
        var infoProducto = {
            id_producto: $(producto).find('input[name="item.ProductoID"]').val(),
            imagen: $(producto).find('input[name="item.UrlImage"]').val(),
            titulo: $(producto).find('input[name="item.Title"]').val(),
            descripcion: $(producto).find('input[name="item.Descripcion"]').val(),
            nombreproducto: $(producto).find('input[name="item.NombreProducto"]').val(),
            precioactual: $(producto).find('input[name="item.PrecioActual"]').val(),
            preciooriginal: $(producto).find('input[name="item.PrecioOriginal"]').val(),
            cantidad: 1
        }
        ////EliminarProductosLocalStorage(infoproducto)
        ////ActulizarProductosLocalStorage(inf)

        //agregar items a local storage

        //ActulizarProductosLocalStorage(infoProducto)

        AgregarProductosLocalStorage(infoProducto)
        ListarProductosLocalStorage(infoProducto)

        //$('#ProductoTitulo').html(infoProducto.nombreproducto);
        //$('#Image').html(infoProducto.imagen);

        $('#ProductoTitulo').html(infoProducto.precioactual);
    }
}

function AgregarProductosLocalStorage(infoproducto) {

    var lista = localStorage.getItem('productos');
    var lista2 = new Array();

    if (lista != null) {
        lista2 = JSON.parse(lista);
        if (ValidarProductoLocalStorange(infoproducto, lista2)) {
            alert('el producto ya existe');
        }
        else {
            alert('se agrego correctamente');
            lista2.push(infoproducto);
        }
    } else {
        //lista = new Array();
        //lista.push(infoproducto);

        alert('se agrego nuevo producto');
        lista2.push(infoproducto);
    }

    localStorage.setItem('productos', JSON.stringify(lista2));
}

function EliminarProductosLocalStorage(id) {  
    
    var lista = localStorage.getItem('productos');
    var lista2 = new Array();
    var listafinal = new Array();
    if (lista != null) {
        lista2 = JSON.parse(lista);
        debugger;
        if (confirm('Desea eliminar el producto')) {
            for (i = 0; i < lista2.length; i++) {
                debugger;
                if (lista2[i].id_producto == id) {

                }
                else {
                    listafinal.push(lista2[i]);
                }
            }
            localStorage.setItem("productos", JSON.stringify(listafinal));
        }
    }
    ListarProductosLocalStorage();
}

function ListarProductosLocalStorage() {

    var ListaProducto = localStorage.getItem('productos');
    var lista2 = JSON.parse(ListaProducto);
    var htmlli = new Array();
    var montoTotal = 0;
    var cantidad = 0;
    for (var i = 0; i < lista2.length; i++) {
        montoTotal += parseFloat(lista2[i].precioactual);
        cantidad += parseInt(lista2[i].cantidad);
        htmlli.push('<li>');
        htmlli.push('  <a href="DetalleProducto/' + lista2[i].id_producto + '" >');
        htmlli.push('    <img class="minicart-product-image" src="' + lista2[i].imagen + '" alt="Li>');
        htmlli.push('  </a>');
        htmlli.push('  <div class="minicart-product-details">');
        htmlli.push('    <h6><a href="DetalleProducto/' + lista2[i].id_producto + '" style="font-size:11px;">' + lista2[i].descripcion +'</a></h6>');
        htmlli.push('    <span>' + lista2[i].precioactual + ' </span>');
        htmlli.push('  </div>');
        htmlli.push('  <button class="close" onclick="EliminarProductosLocalStorage(' + lista2[i].id_producto + ');" title="Remove">');
        htmlli.push('    <i class="fa fa-close"></i>');
        htmlli.push('  </button>');
        htmlli.push('</li>');
    }

    $('#carrito').find('.item-text').html(montoTotal + '<span class="cart-item-count">' + lista2.length + '</span>');
    $('#ul_Listacarrito').html(htmlli.join(' '));
    $('#ul_Listacarrito').parent().find('.minicart-total').html('TOTAL: ' + '<span>S/. ' + (montoTotal).toFixed(2) + '</span>');

}

function ActulizarProductosLocalStorage(infoproducto) {

    //var actulizarproducto = localStorage.getItem('productos')
    //var actulizar2 = new.Array();
    //for (var i = 0; i < actulizar2.length; i++) {
    //    var id_producto = $(id_producto[i].html);
    //    var descripcion = $(descripcion[i].html);
    //    var nombre = $(nombre[i].html);
    //    var precioactual = $(precioactual[i].html);
    //    var preciooriginal = $(preciooriginal[i].html);
    //    cantidad: 1;
    //}


}

//<!--Validadndo los datos
function ValidarProductoLocalStorange(producto, lista) {

    var resultado = false;
    for (i = 0; i < lista.length; i++) {
        if (lista[i].id_producto == producto.id_producto) {
            resultado = true;
            lista[i].cantidad += 1;
            break;

        }
    }
    return resultado;
}

































