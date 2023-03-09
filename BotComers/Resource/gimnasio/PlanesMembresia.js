//******************************************************* IMAGEN PLAN ***************************************************/
const typeFilesPlanes = ["image/png", "image/jpg", "image/jpeg"];
let sizePerPl = 1048576; //1mb
let sizeFilePl = parseFloat(sizePerPl / 1048576); //bytes

async function uploadImagePl(e) {
    let fileInput = document.getElementById("fileImagenPlan");
    let v = e.files[0]

    if (v != undefined) {
        if (
            v.size < sizePerPl &&
            typeFilesPlanes.includes(v.type)
        ) {
            //ok
            var data = new FormData;
            data.append("image", v);
            const resp = await axios({
                method: "post",
                url: "/home/uploadFile",
                data: data,
                headers: { "Content-Type": "multipart/form-data" },
            });
            if (resp.data?.Success) {
                document.getElementById("fileImagenPlanUrl").value = resp.data?.Message1;
                document.getElementById("fileImagenPlanUrlRender").src = resp.data?.Message1;
            }

        } else {
            fileInput.value = ""
            document.getElementById("fileImagenPlanUrl").value = "";
            Swal.fire({
                title: '<strong>Validaciones</strong>',
                icon: 'error',
                html: `Tamaño de archivo max ${sizeFilePl.toFixed(1)} mb, ${typeFilesPlanes.toString()}`,
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
            });
        }
    } else {
        fileInput.value = ""
        document.getElementById("fileImagenPlanUrl").value = "";
    }
}
//******************************************************* IMAGEN PLAN ***************************************************/

async function showModalSuscription() {
    getPasarelas();
    let paquete = document.getElementById("txtCodigoPaquete").value;
    getPlansByPaquete(paquete);



}



//get plans by codigo paquete
async function getPlansByPaquete(paquete) {

    $("#kendogridPlanbyPaquetebody").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            transport: {
                read: function (options) {
                    $.ajax({
                        data: JSON.stringify({ paquete }),
                        type: "POST",
                        url: "/home/GetPlanPasarelaPaquete",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            console.log(msg)
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        },
        //selectable: "row",
        //sortable: true,
        height: 250,
        columns: [
            {
                field: "CodigoPaquete",
                title: "<span class='kendo_camp_header_text' >Codigo paquete</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",

                }
            }, {
                field: "DesPasarelaPago",
                title: "<span class='kendo_camp_header_text' >Pasarela pago</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",

                }
            },
            {
                field: "IdSuscripcionPasarela",
                title: "<span class='kendo_camp_header_text' >Id plan</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",

                }
            },{
                field: "DesSuscripcionPlan",
                title: "<span class='kendo_camp_header_text' >Plan</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",

                }
            },
            {
                title: "<span class='kendo_camp_header_text'>Acciones</span>",
                width: 10,
                template: function (dataItem) {
                    var html = '<div class="p-1">'
                    html += `<button type="button" class="ml-1" onclick="DestroyByPaquete('${dataItem?.CodigoPaqueteSuscripcion}')" >
                            <i class="fa fa-trash icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                          </button>`;
                    html += '</div>'
                    return html;
                },
                attributes: {
                    style: "font-size:11px;"
                }
            },
        ]
    });

}


async function getPasarelas() {
    const resp = await axios({
        method: "post",
        url: "/pasarela/getAllPasarelaEM",
        data: {},
        headers: { "Content-Type": "application/json" },
    });

    if (resp.data) {
        let html = "";
        html += `<option value="">------</option>`;
        resp.data?.forEach((item, index) => {
            html += `<option value="${item?.CodigoPlantillaFormaPago}">${item?.DesFormaPago}</option>`;
        });

        document.getElementById("selectedSuscriptionPa").innerHTML = html;
    }
}


async function changeSelectedPago(e) {
    document.getElementById("selectedPlanList").innerHTML = "";
    if (e.value) {
        let code = e.value;
        document.getElementById("selectedPlanList").innerHTML = "";

        const resp = await axios({
            method: "post",
            url: "/home/getPlanesPasarela",
            data: { code },
            headers: { "Content-Type": "application/json" },
        });

        if (resp.data.Success) {
            let html = "";
            html += `<option value="">------</option>`;
            resp.data?.Date.forEach((item, index) => {
                html += `<option value="${item?.Id}">${item?.Name}</option>`;
            });

            document.getElementById("selectedPlanList").innerHTML = html;
        }
    }
}




const selectxx = document.getElementById('selectedPlanList');
selectxx.addEventListener('change', function handleChange(event) {
    document.getElementById("selectedPlanListName").value = selectxx.options[selectxx.selectedIndex].text;
});

async function registerPlanPasarela(e) {
    e.disabled = true;
    let paquete = document.getElementById("txtCodigoPaquete").value;
    let codepago = document.getElementById("selectedSuscriptionPa").value;
    let idplan = document.getElementById("selectedPlanList").value;
    let descripcion = document.getElementById("selectedPlanListName").value;

    if (paquete == "") {
        $.bootstrapGrowl("Codigo paquete requerido", { type: 'danger', width: 'auto' });
        return;
    }

    if (codepago == "") {
        $.bootstrapGrowl("Codigo forma de pago requerido", { type: 'danger', width: 'auto' });
        return;
    }

    if (idplan == "") {
        $.bootstrapGrowl("Id Plan requerido", { type: 'danger', width: 'auto' });
        return;
    }
    
    let data = {
        paquete, codepago, idplan, descripcion
    };
   
    const resp = await axios({
        method: "post",
        url: "/home/RegisterPlanPasarela",
        data: data,
        headers: { "Content-Type": "application/json" },
    });
    if (resp.data.Success) {
        document.getElementById("btnClosePlPasa").click();
        $.bootstrapGrowl(resp.data?.Message1, { type: 'success', width: 'auto' });
    } else {
        $.bootstrapGrowl(resp.data?.Message1, { type: 'danger', width: 'auto' });
    }
    e.disabled = false;
}



//destroy by paquete

async function DestroyByPaquete(codesuscripcion) {

    let paquete = document.getElementById("txtCodigoPaquete").value;

    let data = {
        paquete, codesuscripcion
    };
    Swal.fire({
        title: 'Estas seguro(a)?',
        text: "¡No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Eliminar',
        cancelButtonText: 'Cancelar',
    }).then(async (result) => {
        if (result.isConfirmed) {
            try {
                const resp = await axios({
                    method: "post",
                    url: "/home/DestroyPlanByPaquete",
                    data: data,
                    headers: { "Content-Type": "application/json" },
                });
                if (resp?.data?.Status == 0) {
                    getPlansByPaquete(paquete);
                    $.bootstrapGrowl(resp?.data?.Message1, { type: 'success', width: 'auto' });
                } else {
                    $.bootstrapGrowl(resp?.data?.Message1, { type: 'danger', width: 'auto' });
                }

            } catch (e) {
                $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
            }
        }
    })


}
