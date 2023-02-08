$(document).ready(function () {

    leerLocalStorage();
    calcularTotal();
});

//Añadir producto al carrito
function comprarProducto(control) {
    
    var imagen = $(control).attr('data-imagen');
    var titulo = $(control).attr('data-titulo');
    var precio = $(control).attr('data-precio');
    var id = $(control).attr('data-id');
    
    //Enviamos el producto seleccionado para tomar sus datos
    leerDatosProducto(imagen, titulo, precio, id);        
}

//Añadir producto al carrito
function comprarProductoDetalle(control) {

    var imagen = $(control).attr('data-imagen');
    var titulo = $(control).attr('data-titulo');
    var precio = $(control).attr('data-precio');
    var id = $(control).attr('data-id');
    var cantidad = $('#txtCantidadCompra').val();   
    
    //Enviamos el producto seleccionado para tomar sus datos
    leerDatosProductoDetalle(imagen, titulo, precio, id, cantidad);
}

//Leer datos del producto
function leerDatosProducto(imagen, titulo, precio, id) {
    
    const infoProducto = {
        imagen: imagen,
        titulo: titulo,
        precio: precio,
        id: id,
        cantidad: 1
    }
    let productosLS; let cantidadActual = 0;
    productosLS = obtenerProductosLocalStorage();
    productosLS.forEach(function (productoLS) {
        if (productoLS.id === infoProducto.id) {
            cantidadActual = productoLS.cantidad;
            productosLS = productoLS.id;
            eliminarProductoLocalStorage(infoProducto.id);
          
        }
    });
    
    if (productosLS === infoProducto.id) {
        const infoProducto2 = {
            imagen: imagen,
            titulo: titulo,
            precio: precio,
            id: id,
            cantidad: (cantidadActual + 1)
        }
        insertarCarrito(infoProducto2);       
    }
    else {
        insertarCarrito(infoProducto);       
    }

    leerLocalStorage();
    calcularTotal();            
}

//Leer datos del producto
function leerDatosProductoDetalle(imagen, titulo, precio, id, cantidad) {

    const infoProducto = {
        imagen: imagen,
        titulo: titulo,
        precio: precio,
        id: id,
        cantidad: cantidad
    }
    let productosLS; let cantidadActual = 0;
    productosLS = obtenerProductosLocalStorage();
    productosLS.forEach(function (productoLS) {
        if (productoLS.id === infoProducto.id) {
            cantidadActual = productoLS.cantidad;
            productosLS = productoLS.id;
            eliminarProductoLocalStorage(infoProducto.id);
        }
    });

    if (productosLS === infoProducto.id) {
        const infoProducto2 = {
            imagen: imagen,
            titulo: titulo,
            precio: precio,
            id: id,
            cantidad: (parseInt(cantidadActual) + parseInt(cantidad))
        }
        insertarCarrito(infoProducto2);
        leerLocalStorage();
        calcularTotal();
    }
    else {
        insertarCarrito(infoProducto);
        leerLocalStorage();
        calcularTotal();
    }

}

function leerDatosDescuento(valor, tipo, codigo, nrocupon) {
    const infoDescuento = {
        valor: valor,
        tipo: tipo,
        codigo: codigo,
        nrocupon: nrocupon
    }
    guardarDescuentoLocalStorage(infoDescuento);    
}

function leerDatosEnvio(PrecioEnvio, TipoTiempoEntrega, TiempoEntrega) {
    const infoEnvio = {
        costoenvio: PrecioEnvio,
        tipotiempoentrega: TipoTiempoEntrega,
        tiempoentrega: TiempoEntrega
    }
    guardarEnvioLocalStorage(infoEnvio);
}

function leerDatosEnvioGratis(Valor) {
    const infoEnvioGratis = {
        valor: Valor
    }
    guardarEnvioGratisLocalStorage(infoEnvioGratis);
}

//muestra producto seleccionado en carrito
function insertarCarrito(producto) {
    
    const row = document.createElement('li');              
    row.innerHTML = `    
                <a href="product-detail-1.html">
                    <figure><img src="${producto.imagen}" data-src="${producto.imagen}" alt="" width="50" height="50" class="lazy"></figure>
                    <strong><span>${producto.titulo} x${producto.cantidad}</span>S/. ${producto.precio}</strong>
                </a>
                <a href="#0" class="action" ><i class="ti-trash" data-id="${producto.id}"  onclick="javascript:eliminarProducto(this);" ></i></a>`;

    const listaProductos = document.querySelector('#lista-carrito');
    listaProductos.appendChild(row);
    guardarProductosLocalStorage(producto);

}

//Eliminar el producto del carrito en el DOM
function eliminarProducto(control) {
        
    let productoID;   
    productoID = $(control).attr('data-id');    
    eliminarProductoLocalStorage(productoID);
    leerLocalStorage();
    calcularTotal();

}

//Elimina todos los productos
function vaciarCarrito(e){
    e.preventDefault();
    while (listaProductos.firstChild) {
        listaProductos.removeChild(listaProductos.firstChild);
    }
    vaciarLocalStorage();

    return false;
}

//Almacenar en el LS
function guardarProductosLocalStorage(producto){
    let productos;
    //Toma valor de un arreglo con datos del LS
    productos = obtenerProductosLocalStorage();
    //Agregar el producto al carrito
    productos.push(producto);
    //Agregamos al LS
    localStorage.setItem('productos', JSON.stringify(productos));
}

function guardarDescuentoLocalStorage(descuento) {
    localStorage.removeItem('descuento');
    //Agregamos al LS
    localStorage.setItem('descuento', JSON.stringify(descuento));
}

function guardarEnvioLocalStorage(envio) {
    localStorage.removeItem('envio');
    //Agregamos al LS
    localStorage.setItem('envio', JSON.stringify(envio));
}

function guardarEnvioGratisLocalStorage(envio) {
    localStorage.removeItem('enviogratis');
    //Agregamos al LS
    localStorage.setItem('enviogratis', JSON.stringify(envio));
}

function obtenerDescuentoLocalStorage() {
    let descuentoLS;

    //Comprobar si hay algo en LS
    if (localStorage.getItem('descuento') === null) {
        descuentoLS = [];
    }
    else {
        descuentoLS = JSON.parse(localStorage.getItem('descuento'));
    }
    
    return descuentoLS;
}

function obtenerEnvioLocalStorage() {
    let envioLS;

    //Comprobar si hay algo en LS
    if (localStorage.getItem('envio') === null) {
        envioLS = [];
    }
    else {
        envioLS = JSON.parse(localStorage.getItem('envio'));
    }

    return envioLS;
}

function obtenerEnvioGratisLocalStorage() {
    let enviogratisLS;

    //Comprobar si hay algo en LS
    if (localStorage.getItem('enviogratis') === null) {
        enviogratisLS = [];
    }
    else {
        enviogratisLS = JSON.parse(localStorage.getItem('enviogratis'));
    }

    return enviogratisLS;
}

//Comprobar que hay elementos en el LS
function obtenerProductosLocalStorage(){
    let productoLS;

    //Comprobar si hay algo en LS
    if (localStorage.getItem('productos') === null) {
        productoLS = [];
    }
    else {
        productoLS = JSON.parse(localStorage.getItem('productos'));
    }
    return productoLS;
}

//Mostrar los productos guardados en el LS
function leerLocalStorage() {
    const listaProductos = document.querySelector('#lista-carrito');
    $('#lista-carrito').empty();
    let productosLS;
    let cantidad = 0;
    productosLS = obtenerProductosLocalStorage();
    productosLS.forEach(function (producto) {
        //Construir plantilla      
        const row = document.createElement('li');
        row.innerHTML = `    
                <a href="product-detail-1.html">
                    <figure><img src="${producto.imagen}" data-src="${producto.imagen}" alt="" width="50" height="50" class="lazy"></figure>
                    <strong><span>${producto.titulo} x${producto.cantidad}</span>S/. ${producto.precio}</strong>
                </a>
                <a href="#0" class="action" ><i class="ti-trash" data-id="${producto.id}" onclick="javascript:eliminarProducto(this);" ></i></a>`;
        
        cantidad += parseInt(producto.cantidad);
        
        listaProductos.appendChild(row);
    });
    
    $('#lblCantidadCarrito').html(cantidad);
}

//Mostrar los productos guardados en el LS en compra.html
function leerLocalStorageCompra(){
    let productosLS;
    productosLS = obtenerProductosLocalStorage();
    productosLS.forEach(function (producto) {
        const row = document.createElement('tr');
        row.innerHTML = `
                <td>
                    <img src="${producto.imagen}" width=100>
                </td>
                <td>${producto.titulo}</td>
                <td>${producto.precio}</td>
                <td>
                    <input type="number" class="form-control cantidad" min="1" value=${producto.cantidad}>
                </td>
                <td id='subtotales'>${producto.precio * producto.cantidad}</td>
                <td>
                    <a href="#" class="borrar-producto fas fa-times-circle" style="font-size:30px" data-id="${producto.id}"></a>
                </td>
            `;
        listaCompra.appendChild(row);
    });
}

//Eliminar producto por ID del LS
function eliminarProductoLocalStorage(productoID){
    let productosLS;
    //Obtenemos el arreglo de productos
    productosLS = obtenerProductosLocalStorage();
    //Comparar el id del producto borrado con LS
    productosLS.forEach(function (productoLS, index) {
        if (productoLS.id === productoID) {
            productosLS.splice(index, 1);
        }
    });

    //Añadimos el arreglo actual al LS
    localStorage.setItem('productos', JSON.stringify(productosLS));
}

//Eliminar todos los datos del LS
function vaciarLocalStorage(){
    localStorage.clear();
}

//Procesar pedido
function procesarPedido(e){
    e.preventDefault();

    if (obtenerProductosLocalStorage().length === 0) {
        Swal.fire({
            type: 'error',
            title: 'Oops...',
            text: 'El carrito está vacío, agrega algún producto',
            showConfirmButton: false,
            timer: 2000
        })
    }
    else {
        location.href = "compra.html";
    }
}

//Calcular montos
function calcularTotal(){
    let productosLS;
    let total = 0, igv = 0, subtotal = 0;
    productosLS = obtenerProductosLocalStorage();
    for (let i = 0; i < productosLS.length; i++) {
        let element = Number(productosLS[i].precio * productosLS[i].cantidad);
        total = total + element;

    }

    igv = parseFloat(total * 0.18).toFixed(2);
    subtotal = parseFloat(total - igv).toFixed(2);

    //document.getElementById('subtotal').innerHTML = "S/. " + subtotal;
    //document.getElementById('igv').innerHTML = "S/. " + igv;
    document.getElementById('total').innerHTML = "S/. " + total.toFixed(2);
}
