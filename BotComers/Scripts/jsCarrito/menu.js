$(document).ready(function () {

   
});

//Leer datos del producto
function leerDatosMenu(CodigoMenu,CodigoMenuSuperior,Descripcion, CodigoImagen, UrlImagen) {

    const infoMenu = {
        CodigoMenu: CodigoMenu,
        CodigoMenuSuperior: CodigoMenuSuperior,
        Descripcion: Descripcion,
        CodigoImagen: CodigoImagen,
        UrlImagen: UrlImagen
    }
  
    guardarMenuLocalStorage(infoMenu);
    
}

//Almacenar en el LS
function guardarMenuLocalStorage(infoMenu) {
    let menu;
    //Toma valor de un arreglo con datos del LS
    menu = obtenerMenuLocalStorage();
    //Agregar el producto al carrito
    menu.push(infoMenu);
    //Agregamos al LS
    localStorage.setItem('menu', JSON.stringify(menu));
}


//Comprobar que hay elementos en el LS
function obtenerMenuLocalStorage() {
    let menuLS;

    //Comprobar si hay algo en LS
    if (localStorage.getItem('menu') === null) {
        menuLS = [];
    }
    else {
        menuLS = JSON.parse(localStorage.getItem('menu'));
    }
    return menuLS;
}


//Eliminar producto por ID del LS
function eliminarMenuLocalStorage(MenuID) {
    let menuLS;
    //Obtenemos el arreglo de productos
    menuLS = obtenerMenuLocalStorage();
    //Comparar el id del producto borrado con LS
    menuLS.forEach(function (menuLS, index) {
        if (menuLS.CodigoMenu === MenuID) {
            menuLS.splice(index, 1);
        }
    });

    //Añadimos el arreglo actual al LS
    localStorage.setItem('menu', JSON.stringify(menuLS));
}

function eliminarlocalStoragexkey(keyName) {
    localStorage.removeItem(keyName);
}