/****************************** menus emails ****************************/
function evemt_changeMenu(e, num) {
    switch (num) {
        case 1:
            var d = document.getElementById("Configuracion_EnvioCorreos");
            d.style.display = 'block';

            document.querySelectorAll(".item_menu_sd").forEach((item, index) => {
                item.classList.remove("active")
            })
            e.classList.add("active");
            document.getElementById("Dconfiguration").style.display = 'none';
            break;
        case 2:
            var d = document.getElementById("Dconfiguration");
            d.style.display = 'block';
            document.querySelectorAll(".item_menu_sd").forEach((item, index) => {
                item.classList.remove("active")
            })
            e.classList.add("active");
            document.getElementById("Configuracion_EnvioCorreos").style.display = 'none'
            break;
        default:
            break;
    }


}