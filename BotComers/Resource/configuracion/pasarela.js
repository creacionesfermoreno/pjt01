
//get types pasarela
async function getTypesPasarela() {
    try {
        const resp = await axios({
            method: "post",
            url: "/pasarela/getTypePasarelas",
            data: {},
            headers: { "Content-Type": "application/json" },
        });
        let html = "";
        resp?.data?.forEach((item, index) => {
            console.log(item)
            html += `<div>
                <label class="cursor-pointer" id="key_ppt_${index}">
                    <input hidden type="radio" value="${item?.CodigoPlantillaFormaPago}" name="radio_types_pasarelas" id="key_ppt_${index}" />
                    <div class="flex flex-col text-center dpp" style="" onclick="activeRadioButton(this,'${item?.Descripcion}')">
                        <img src="https://img.freepik.com/premium-vector/key-icon-image_188544-4745.jpg?w=2000" width="50px" />
                        <p class="p-0 m-0">${item?.Descripcion}</p>
                    </div>
                </label>
            </div>`;
        });
        document.getElementById("content_pasarelaoption").innerHTML = html;
    } catch (e) {
        console.log(e)
    }
}

//modal show PEM
async function showModalPEM() {
    document.getElementById("colpasarelas").style.display = "";
    document.getElementById("registerPasarelaEmpresaBtn").style.display = "";
    document.getElementById("updatePasarelaEmpresaBtn").style.display = "none";
    document.getElementById("title_modal_PEM").innerText = "Nueva pasarela de pago"
    await getTypesPasarela()
    clearCulqui();
}


//get list pasarela empresa
function getListPEmpresa() {
    document.getElementById('loadMe').style.display = 'block';
    $("#k_list_pasarelaEmpresa").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: JSON.stringify({}),
                        type: "POST",
                        url: "/pasarela/getAllPasarelaEM",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            console.log(msg)
                            options.success(msg);
                        }, complete: function () {
                            document.getElementById('loadMe').style.display = 'none';
                        }
                    });
                }
            }
        },
        height: 400,
        columns: [

            {
                field: "DesFormaPago",
                title: "<span class='kendo_camp_header_text' >Forma Pago</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",
                }
            }, {
                field: "Valor1",
                title: "<span class='kendo_camp_header_text' >Key publico</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",
                }
            },
            {
                title: "<span class='kendo_camp_header_text'>Key privado</span>",
                field: "Valor2",
                width: 10,
                attributes: {
                    style: "font-size:11px;"
                }
            },
            {
                title: "<span class='kendo_camp_header_text'>Estado</span>",
                template: function (item) {
                    let html = "";
                    if (item?.Estado == true) {
                        html = `<div class="px-2.5 py-0.5 bg-green-500 rounded-full text-white text-center text-xs">activo</div>`;
                    } else {
                        html = `<div class="px-2.5 py-0.5 bg-red-500 rounded-full text-white text-center text-xs">deshabilitado</div>`;
                    }
                    return html;
                },
                width: 3,
                attributes: {
                    style: "font-size:11px;"
                }
            },
            {
                title: "<span class='kendo_camp_header_text'>Fecha creación</span>",
                field: "DesFechaCreacion",
                width: 5,
                attributes: {
                    style: "font-size:11px;"
                }
            },

            {
                title: "<span class='kendo_camp_header_text'>Acciones</span>",
                width: 10,
                template: function (item) {
                    var html = '<div class="p-1">'
                    html += `<button type="button" class="" onclick="deletePasarelaEm('${item?.CodigoPlantillaFormaPago}')" >
                                 <i class="fa fa-trash icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                              </button>

                                <button type="button"  class="" data-toggle="modal" data-target="#modalPasarela" onclick="editPasarelaEm('${item?.CodigoPlantillaFormaPago}')" >
                                     <i class="fa fa-pencil-square-o icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                                </button>
                                
                                `;
                    if (item?.EstadoProduccion == false) {
                        html += `<button type="button"  class="" data-toggle="modal" data-target="#modalCardDemo" onclick="showModalPay('${item?.CodigoPlantillaFormaPago}','${item?.DesFormaPago}','${item?.Valor1}')" >
                                    <i class="fa fa-credit-card icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                                </button>`;
                    }
                    html += '</div>'
                    return html;
                },
                attributes: {
                    style: "font-size:11px;"
                }
            },

        ],
    });
}







//********************************************************QULQUI*******************************/
//register
async function registerPasarelaEmpresa(e) {
    let keypublic = document.getElementById("ppbusiness_v_one").value;
    let keyprivate = document.getElementById("ppbusiness_v_two").value;
    let code = document.querySelector('input[name="radio_types_pasarelas"]:checked') ? document.querySelector('input[name="radio_types_pasarelas"]:checked').value : "";
    let status = document.querySelector('input[name="rd_status_pem"]:checked').value;
    let entorProd = document.querySelector('input[name="rd_status_pem_entorn"]:checked').value;
    let type = document.querySelector('#PayMethodType').value;
    let current = document.querySelector('#select_current').value;
    let created = true;
    let data = {
        keypublic, keyprivate, code, status, type, created, entorProd, current
    };

    //*************************** only Mpago*****************/
    if (type == "MERCADO PAGO") {
        if (current == "" || current == null) {
            $.bootstrapGrowl("Selecione tipo de moneda", { type: 'danger', width: 'auto' });
            return;
        }
    }

    e.disabled = true;
    try {
        const resp = await axios({
            method: "post",
            url: "/pasarela/registerPasarela",
            data: data,
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Status == 0) {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'success', width: 'auto' });
            clearCulqui();
            getListPEmpresa();
        } else {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'danger', width: 'auto' });
        }

    } catch (e) {
        $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
    }
    e.disabled = false;
}

function clearCulqui() {
    document.getElementById("ppbusiness_v_one").value = "";
    document.getElementById("ppbusiness_v_two").value = "";
    document.getElementsByName("radio_types_pasarelas").forEach(item => item.checked = false);
    document.getElementById("closeModalPEmpresa").click();
    document.getElementById("registerPasarelaEmpresaBtn").style.display = "";
    document.getElementById("updatePasarelaEmpresaBtn").style.display = "none";
    document.getElementsByName("rd_status_pem").forEach((item, index) => {
        if (index == 0) {
            item.checked = true;
        } else {
            item.checked = false
        }
    })
}
//********************************************************END QULQUI*******************************/

//********************************************************CRUD*******************************/
//delete campaign
function deletePasarelaEm(code) {
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
                    url: "/pasarela/deletePasarelaEM",
                    data: { code },
                    headers: { "Content-Type": "application/json" },
                });
                if (resp?.data?.Status == 0) {
                    getListPEmpresa();
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

//edit pasarelaEm
async function editPasarelaEm(code) {

    document.getElementById("registerPasarelaEmpresaBtn").style.display = "none";
    document.getElementById("updatePasarelaEmpresaBtn").style.display = "";
    try {
        const resp = await axios({
            method: "post",
            url: "/pasarela/getItemPasarelaEm",
            data: { code },
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data) {
            let data = await resp?.data?.Date;
            console.log(data)
            document.getElementById("title_modal_PEM").innerText = "Editar pasarela de pago " + data?.DesFormaPago

            document.getElementById("colpasarelas").style.display = "none";

            document.getElementById("ppbusiness_v_one").value = data?.Valor1;
            document.getElementById("ppbusiness_v_two").value = data?.Valor2;
            document.getElementById("CodigoPlantillaFormaPago").value = data?.CodigoPlantillaFormaPago;
            document.getElementById("PayMethodType").value = data?.DesFormaPago.toUpperCase();

            document.getElementsByName("rd_status_pem").forEach(item => {
                if (item.value == data?.Estado) {
                    item.checked = true;
                }
            });

            document.getElementsByName("rd_status_pem_entorn").forEach(itemx => {

                if (itemx.value == data?.EstadoProduccion) {
                    itemx.checked = true;
                }
                console.log(itemx.value == data?.EstadoProduccion)
            });

            //****************************************shows************************************ */
            document.getElementById("dtxt_currents").style.display = "none";
            document.getElementById("select_current").value = "";
            let meth = data?.DesFormaPago;
            switch (meth.toUpperCase()) {
                case "CULQI":
                    document.getElementById("label_public").innerHTML = "Ingrese key público";
                    document.getElementById("label_private").innerHTML = "Ingrese key privado";
                    break;
                case "PAYPAL":
                    document.getElementById("label_public").innerHTML = "Ingrese client id";
                    document.getElementById("label_private").innerHTML = "Ingrese client secret";
                    break;
                case "MERCADO PAGO":
                    document.getElementById("dtxt_currents").style.display = "";
                    document.getElementById("select_current").value = data?.Valor3;
                    document.getElementById("label_public").innerHTML = "Ingrese key público";
                    document.getElementById("label_private").innerHTML = "Ingrese token";
                    break;
                default:
                    break;
            }
            //****************************************shows************************************ */
        }
    } catch (e) {
    }
}

async function getItemPasarelaEmActive(code) {

    try {
        const resp = await axios({
            method: "post",
            url: "/pasarela/getItemPasarelaEmActive",
            data: { code },
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data) {
            console.log(resp?.data)
        }
    } catch (e) {
    }
}
getItemPasarelaEmActive()
//update pararelaEm
async function updatePasarelaEmpresa(e) {

    let keypublic = document.getElementById("ppbusiness_v_one").value;
    let keyprivate = document.getElementById("ppbusiness_v_two").value;
    let code = document.getElementById("CodigoPlantillaFormaPago").value;
    let status = document.querySelector('input[name="rd_status_pem"]:checked').value;
    //let code = document.querySelector('input[name="radio_types_pasarelas"]:checked') ? document.querySelector('input[name="radio_types_pasarelas"]:checked').value : "";
    let type = document.querySelector('#PayMethodType').value;
    let entorProd = document.querySelector('input[name="rd_status_pem_entorn"]:checked').value;
    let current = document.querySelector('#select_current').value;


    //*************************** only Mpago*****************/
    if (type == "MERCADO PAGO") {
        if (current == "" || current == null) {
            $.bootstrapGrowl("Selecione tipo de moneda", { type: 'danger', width: 'auto' });
            return;
        }
    }

    let created = false;
    let data = {
        keypublic, keyprivate, code, status, type, created, entorProd, current
    };

    e.disabled = true;
    try {
        const resp = await axios({
            method: "post",
            url: "/pasarela/registerPasarela",
            data: data,
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Status == 0) {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'success', width: 'auto' });
            clearCulqui();
            getListPEmpresa();
        } else {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'danger', width: 'auto' });
        }

    } catch (e) {
        $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
    }
    e.disabled = false;
}

//********************************************************END CRUD*******************************/


//show modal

async function showModalPay(code, type, valor) {
    document.querySelector('#PayMethodType').value = type.toUpperCase();
    document.getElementById("CodigoPlantillaFormaPago").value = code;

    document.querySelectorAll(".demorows").forEach((item, index) => {
        item.style.display = "none";
    });

    let paymet = type.toUpperCase();

    switch (paymet) {
        case "CULQI":
            document.getElementById("PaymetCulqiDemo").style.display = "";
            break;

        case "PAYPAL":
            document.getElementById("PaymetPaypalDemo").style.display = "";

            var script = document.createElement('script');
            script.type = 'text/javascript';
            script.id = "payDemoScript";
            script.src = `https://www.paypal.com/sdk/js?client-id=${valor}&components=buttons&currency=USD`;
            document.head.appendChild(script);
            renderPaypal();

            //clearTimeout(setTime);
            break;
        case "MERCADO PAGO":
            document.getElementById("PaymetMPagoDemo").style.display = "";
            //document.getElementById("cho-container").innerHTML = "";

            //const mp = new MercadoPago(valor, {
            //    locale: "es-PE"
            //});
            const resp = await axios({
                method: "post",
                url: "/pasarela/DemoPayCard",
                data: { code: code, type: paymet },
                headers: { "Content-Type": "application/json" },
            });

            document.getElementById("cho_container_link").href = resp?.data?.Message2;
            //if (resp.data?.Success) {
            //    console.log("preferenceId :", resp?.data?.Message1)
            //    mp.checkout({
            //        preference: {
            //            id: resp?.data?.Message1
            //        },
            //        render: {
            //            container: '.cho-container',
            //            label: 'Pagar',
            //        }
            //    });
            //}

            break;
        default:
    }
}

function renderPaypal() {
    let setTime = setTimeout(() => {
        paypal.Buttons({
            style: {
                layout: 'vertical',
                color: 'white',
                shape: 'pill',
                label: 'paypal',
                tagline: false,
            },
            createOrder: async function () {
                let type = document.querySelector('#PayMethodType').value;
                let code = document.getElementById("CodigoPlantillaFormaPago").value;
                let data = {
                    code, type
                };
                const resp = await axios({
                    method: "post",
                    url: "/pasarela/DemoPayCard",
                    data: data,
                    headers: { "Content-Type": "application/json" },
                });

                return resp?.data?.Message1;
            },
            onApprove: async function (data) {
                let order = data?.orderID;
                let token = data?.facilitatorAccessToken;
                let datax = {
                    token, order
                };
                const capture = await axios({
                    method: "post",
                    url: "/pasarela/CaptureOrder",
                    data: datax,
                    headers: { "Content-Type": "application/json" },
                });
                if (capture.data?.Success) {

                    $.bootstrapGrowl(capture.data?.Message1, { type: 'success', width: 'auto' });
                } else {

                    $.bootstrapGrowl(capture.data?.Message1, { type: 'danger', width: 'auto' });
                }
                return true;
            }, onCancel: async function () {

            }, onError: async function (err) {
                console.log("Log Paypal:", err)
            },

        }).render('#paypal-button-container');

    }, 250)


}



async function DemoPayed(e) {
    e.disabled = true;
    let type = document.querySelector('#PayMethodType').value;
    let code = document.getElementById("CodigoPlantillaFormaPago").value;
    document.getElementById("loadingCard").style.display = "";
    let data = {
        code, type,
    };

    try {
        const resp = await axios({
            method: "post",
            url: "/pasarela/DemoPayCard",
            data: data,
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Success) {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'success', width: 'auto' });

        } else {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'danger', width: 'auto' });
        }

    } catch (e) {
        $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
    }
    document.getElementById("loadingCard").style.display = "none";
    e.disabled = false;

}


//*************************generales***********************
//active selecte types pasarela 
function activeRadioButton(e, method) {
    document.querySelectorAll(".dpp").forEach(item => item.classList.remove("dpp-active"));
    e.classList.add("dpp-active");
    document.getElementById("dtxt_currents").style.display = "none"

    let p = document.querySelector("#label_public");
    let pr = document.querySelector("#label_private");
    let type = document.querySelector('#PayMethodType');
    let meth = method.toUpperCase();

    switch (meth) {
        case "CULQI":
            p.innerHTML = "Ingrese key público";
            pr.innerHTML = "Ingrese key privado";
            type.value = meth;
            break;
        case "PAYPAL":
            p.innerHTML = "Ingrese client id";
            pr.innerHTML = "Ingrese client secret";
            type.value = meth;
            break;

        case "MERCADO PAGO":
            p.innerHTML = "Ingrese  key público";
            pr.innerHTML = "Ingrese  token";
            type.value = meth;
            document.getElementById("dtxt_currents").style.display = ""
            break;
        default:
            type.value = "";
            break;
    }
}