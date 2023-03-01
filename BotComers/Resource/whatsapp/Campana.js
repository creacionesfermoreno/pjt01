
//globals const
const durationRetro = 1000
const durationZindexModal = 5


//****************************************** STAR CRUD*************************************************/

//get list campaigns
//async function getCampaigns() {

//    let body = document.getElementById("whatsapp_campanas_body");
//    let codeconfig = document.getElementById("CodigoWhatsappConfiguracion").value;
//    let data = {
//        codeconfig,
//        start: document.getElementById("filter_date_start_camp").value ?? null,
//        end: document.getElementById("filter_date_end_camp").value ?? null
//    };
//    console.log(data)
//    try {
//        const resp = await axios({
//            method: "post",
//            url: "/whatsapp/Campaigns",
//            data: data,
//            headers: { "Content-Type": "application/json" },
//        });
//        console.log(resp?.data)
//        if (resp?.data) {
//            let html = "";
//            resp?.data?.forEach((item, index) => {
//                html += ` <tr class="border-bottom border-200">
//                              <td class="" style="">${index + 1} </td>
//                              <td class="" style="">${item?.NombreWhatsappCampania}</td>
//                              <td class="" style="">${item?.DescFechaHoraProgramado}</td>
//                              <td class="" style="">${statusCampaings(item?.EstadoWhatsappCampania)}</td>
//                              <td class=""  style=""> 



//                            <button type="button" class="btn-remove" onclick="deleteCampaign('${item?.CodigoWhatsappCampania}')" >
//                                <i class="fa fa-trash" aria-hidden="true"></i>
//                            </button>`;

//                if (item?.EstadoWhatsappCampania == false) {
//                    html += `<button type="button" data-toggle="modal" data-target="#modalCampanaWh" class="btn-edit ml-1" onclick="getCampaignItem('${item?.CodigoWhatsappCampania}','${item?.NameTemplate}')" >
//                        <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
//                    </button>`;
//                }


//                html += `  <button type="button" class="btn-edit" data-toggle="modal" data-target="#modalCampanaWh" onclick="duplicateCampaign('${item?.CodigoWhatsappCampania}','${item?.NameTemplate}')" >
//                                       <i class="fa fa-files-o" aria-hidden="true"></i>
//                             </button>`;


//                if (item?.EstadoWhatsappCampania == true) {
//                    html += ` <button type="button" class="btn-send_custom" data-toggle="modal" data-target="#modalDetailCampaign" onclick="getListCampaignDetails('${item?.CodigoWhatsappCampania}')" >
//                            <i class="fa fa-info" aria-hidden="true"></i>
//                            </button>`;

//                }
//                if (item?.EstadoWhatsappCampania == false) {
//                    html += `<button type="button" class="btn-send_custom ml-1" onclick="sendCampaign('${item?.CodigoWhatsappCampania}')" >
//                        <i class="fa fa-paper-plane" aria-hidden="true"></i>
//                    </button>`;
//                }


//                html += `</td>
//                         </tr>`;

//            });
//            body.innerHTML = html;
//        }
//    } catch (e) {
//        console.log(e)
//    }
//}


//list with kendo grid

function getCampaigns() {
    let codeconfig = document.getElementById("CodigoWhatsappConfiguracion").value;
    let data = {
        codeconfig: codeconfig,
        start: document.getElementById("filter_date_start_camp").value ?? null,
        end: document.getElementById("filter_date_end_camp").value ?? null
    };

    $("#kgrid_whatsapp_campanas_body").kendoGrid({
        dataSource: {
            type: "json",
            transport: {
                read: function (options) {
                    $.ajax({
                        data: JSON.stringify(data),
                        type: "POST",
                        url: "/whatsapp/Campaigns",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            options.success(msg);
                        }, complete: function () {

                        }
                    });
                }
            }
        },
        //selectable: "row",
        //sortable: true,
        height: 770,
        columns: [{
            field: "NombreWhatsappCampania",
            title: "<span class='kendo_camp_header_text' >Nombre de campaña</span>",
            width: 10,
            attributes: {
                style: "font-size:11px;",

            }
        },
        {
            title: "<span class='kendo_camp_header_text'>Cola destino</span>",
            field: "DesColaDestino",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }

        },

        {

            title: "<span class='kendo_camp_header_text'>Estado</span>",
            template: function (item) {
                let html = "";
                if (item?.EstadoWhatsappCampania == true) {
                    html = `enviado`;
                } else {
                    html = `pendiente`;
                }
                return html;
            },
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }

        },
        {
            title: "<span class='kendo_camp_header_text'>Total Números</span>",
            field: "Total",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }

        },
        {
            title: "<span class='kendo_camp_header_text'>Validos</span>",
            field: "TotalEnviado",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }

        }, {
            title: "<span class='kendo_camp_header_text'>Inválidos</span>",
            field: "TotalError",
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }

        },
        {
            title: "<span class='kendo_camp_header_text'>Fecha Creación</span>",
            field: "DateGlobalization",
            width: 10,
            //template: function (data) {
            //    return 10;
            //},
            attributes: {
                style: "font-size:11px;"
            }

        },
        //{
        //    field: "DescFechaHoraProgramado",
        //    title: "<center style='color:#fff;font-weight:bold'>Fecha y Hora Programado</center>",
        //    width: 10,
        //    attributes: {
        //        style: "font-size:11px;text-align:center;"
        //    }

        //},

        {
            title: "<span class='kendo_camp_header_text'>Acciones</span>",
            width: 10,
            template: function (dataItem) {
                var html = '<div class="p-1">'
                html += `<button type="button" class="" onclick="deleteCampaign('${dataItem?.CodigoWhatsappCampania}')" >
                        <i class="fa fa-trash icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                      </button>`;


                if (dataItem.EstadoWhatsappCampania == false) {
                    html += ` <button type="button"  class=" " data-toggle="modal" data-target="#modalCampanaWh" onclick="getCampaignItem('${dataItem?.CodigoWhatsappCampania}','${dataItem?.NameTemplate}')" >
                        <i class="fa fa-pencil-square-o icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                    </button>`;

                }
                html += `<button type="button" class=" ml-1" data-toggle="modal" data-target="#modalCampanaWh"  onclick="duplicateCampaign('${dataItem?.CodigoWhatsappCampania}','${dataItem?.NameTemplate}')" >
                                       <i class="fa fa-files-o icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                       </button>`;
                if (dataItem.EstadoWhatsappCampania == true) {
                    html += `<button type="button" class=" ml-1" data-toggle="modal" data-target="#modalDetailCampaign" onclick="getListCampaignDetails('${dataItem?.CodigoWhatsappCampania}')" >
                        <i class="fa fa-info icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                    </button>`;
                }
                if (dataItem.EstadoWhatsappCampania == false) {
                    html += `  <button type="button" class=""  onclick="sendCampaign('${dataItem?.CodigoWhatsappCampania}')" >
                        <i class="fa fa-paper-plane icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
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
        //dataBound: function (e) {

        //    this.select(this.tbody.find('>tr:first'));
        //},
        //change: function () {
        //    var text = "";
        //    var grid = this;
        //    grid.select().each(function (e) {
        //        var dataItem = grid.dataItem($(this));

        //    });
        //}
    });

}



function sdaaaaaa(s) {

}



function statusCampaings(val) {
    switch (val) {
        case false:
            return "Pendiente";
            break;
        case true:
            return "Enviado";
            break;
        default:
    }
}

function clearModalZindex() {
    let timer;
    window.clearTimeout(timer);
    timer = window.setTimeout(() => {
        if (document.querySelector(".modal-backdrop")) {
            document.querySelector(".modal-backdrop").classList.remove("modal-backdrop");
        }
    }, durationZindexModal);
}

function showModalTemplatesSelectes() {
    clearModalZindex()
    document.getElementById("btncloseModalCampaign_close").click();

    //document.getElementById("BtnModalAddCampanaWs").click();
}

//get item campaign --Edit
async function getCampaignItem(code, name) {
    document.getElementById("TituloTextCampaign").innerText = "Editar campaña";

    document.getElementById("BtnRegistrarCampaigns").style.display = "none";
    document.getElementById("BtnUpdateCampaigns").style.display = "";
    clearModalZindex();

    //get template
    await getItemTemplate(name);

    //get campaign
    try {
        const resp = await axios({
            method: "post",
            url: "/whatsapp/CampaignsByCode",
            data: { code }
            ,
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Date) {

            let item = resp?.data?.Date;
            document.getElementById("key_campaign_").value = item?.CodigoWhatsappCampania;
            document.getElementById("template_seleted_value").value = item?.NameTemplate;
            document.getElementById("btnselectedtemplate_new").disabled = true;
            document.getElementById("name_campana").value = item?.NombreWhatsappCampania;

            document.getElementById("url_execel_campana").value = item?.UrlDestinatarios;
            document.getElementById("canpana_select_cola").value = item?.ColaDestino;
            document.getElementById("canpana_select_weather").value = item?.TiempoRespuesta;

            let date = item?.DescFechaHoraProgramado;
            let dateSlice = date.split(" ");
            document.getElementById("date_campana").value = dateSlice[0];
            document.getElementById("hour_campana").value = dateSlice[1];


            if (item?.TypeHeader == "IMAGE") {
                document.getElementById("cam_type_header_value").value = item?.ParametersHeader;
                document.getElementById("imgprevHeaderImage").src = item?.ParametersHeader;
            }
            else if (item?.TypeHeader == "VIDEO") {
                document.getElementById("cam_type_header_value").value = item?.ParametersHeader;
                document.getElementById("videoprevHeaderVideo").src = item?.ParametersHeader;
            }
            else if (item?.TypeHeader == "DOCUMENT") {
                document.getElementById("cam_type_header_value").value = item?.ParametersHeader;
                document.getElementById("embedprevHeaderDoc").src = item?.ParametersHeader;

            } else if (item?.TypeHeader == "TEXT") {
                var timerH;
                if (item?.ParametersHeader != null && item?.ParametersHeader != "") {
                    window.clearTimeout(timerH);
                    timer = window.setTimeout(function () {
                        document.getElementById("para_select_header_text").value = item?.ParametersHeader;

                    }, durationRetro);
                }

            } else {

            }

            if (item?.ParametersBody) {
                var timer;
                if (item?.ParametersBody != null || item?.ParametersBody != "") {
                    //is parameters body
                    window.clearTimeout(timer);
                    timer = window.setTimeout(function () {
                        let dataArray = item?.ParametersBody.split("|");
                        let bodyp = document.querySelectorAll(".para_select_body_text").forEach((select, index) => {
                            select.value = dataArray[index]
                        });
                    }, durationRetro);

                }
            }

        }

    } catch (e) {

    }
}
//duplicate  campaign
async function duplicateCampaign(code, name) {
    document.getElementById("TituloTextCampaign").innerText = "Campaña duplicada";

    document.getElementById("BtnRegistrarCampaigns").style.display = "";
    document.getElementById("BtnUpdateCampaigns").style.display = "none";
    clearModalZindex();


    //get template
    await getItemTemplate(name);

    //get campaign
    try {
        const resp = await axios({
            method: "post",
            url: "/whatsapp/CampaignsByCode",
            data: { code }
            ,
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Date) {

            let item = resp?.data?.Date;
            
            document.getElementById("key_campaign_").value = item?.CodigoWhatsappCampania;
            document.getElementById("template_seleted_value").value = item?.NameTemplate;
            document.getElementById("text_select_template").innerText = item?.NameTemplate;
            document.getElementById("btnselectedtemplate_new").disabled = true;
            document.getElementById("name_campana").value = "";
            document.getElementById("url_execel_campana").value = "";

            document.getElementById("date_campana").value = "";
            document.getElementById("hour_campana").value = "";
            if (item?.TypeHeader == "IMAGE") {
                document.getElementById("cam_type_header_value").value = item?.ParametersHeader;
                document.getElementById("imgprevHeaderImage").src = item?.ParametersHeader;
                document.getElementById("dheader_Image").style.display = 'none';
            }
            else if (item?.TypeHeader == "VIDEO") {
                document.getElementById("cam_type_header_value").value = item?.ParametersHeader;
                document.getElementById("videoprevHeaderVideo").src = item?.ParametersHeader;
            }
            else if (item?.TypeHeader == "DOCUMENT") {
                document.getElementById("cam_type_header_value").value = item?.ParametersHeader;
                document.getElementById("embedprevHeaderDoc").src = item?.ParametersHeader;

            } else if (item?.TypeHeader == "TEXT") {
                var timerH;
                if (item?.ParametersHeader != null && item?.ParametersHeader != "") {
                    window.clearTimeout(timerH);
                    timer = window.setTimeout(function () {
                        document.getElementById("para_select_header_text").value = item?.ParametersHeader;
                        document.getElementById("para_select_header_text").disabled = true;
                    }, durationRetro);
                }
            } else {

            }

            if (item?.ParametersBody) {
                var timer;
                if (item?.ParametersBody != null || item?.ParametersBody != "") {
                    //is parameters body

                    window.clearTimeout(timer);
                    timer = window.setTimeout(function () {
                        let dataArray = item?.ParametersBody.split("|");
                        let bodyp = document.querySelectorAll(".para_select_body_text").forEach((select, index) => {
                            select.disabled = true;
                            select.value = dataArray[index]
                        });

                    }, durationRetro);

                }
            }

        }

    } catch (e) {

    }
}

//register
async function RegistrarCampaigns(e) {

    let template = document.getElementById("template_seleted_value").value;
    let campaign = document.getElementById("name_campana").value;
    let dat = document.getElementById("date_campana").value;
    let hour = document.getElementById("hour_campana").value;
    let excel = document.getElementById("url_execel_campana").value;


    let type = document.getElementById("cam_type_header").value;
    let url = document.getElementById("cam_type_header_value").value;
    let text_is_param = document.getElementById("header_is_param").value;
    let text = document.getElementById("para_select_header_text") ? document.getElementById("para_select_header_text").value : "";

    let typeprogration = document.getElementById("type_programtion_selected").value;
    let language = document.getElementById("language_template").value;

    let date = null;
    if (template == "") {
        $.bootstrapGrowl("Selecione un template", { type: 'danger', width: 'auto' });
        return true;
    }

    if (language == "") {
        $.bootstrapGrowl("lenguaje requerido", { type: 'danger', width: 'auto' });
        return true;
    }

    if (campaign == "") {
        $.bootstrapGrowl("campo nombre de campaña requerido", { type: 'danger', width: 'auto' });
        return true;
    }
    if (typeprogration == "2") {
        if (dat == "") {
            $.bootstrapGrowl("campo fecha requerido", { type: 'danger', width: 'auto' });
            return true;
        }
        if (hour == "") {
            $.bootstrapGrowl("campo hora requerido", { type: 'danger', width: 'auto' });
            return true;
        }
        date = dat + " " + hour;
    }


    if (type == "IMAGEN" || type == "VIDEO" || type == "DOCUMENT") {
        if (url == "") {
            $.bootstrapGrowl("campo medios requerido", { type: 'danger', width: 'auto' });
            return true;
        }
    }


    if (excel == "") {
        $.bootstrapGrowl("campo destinatarios requerido", { type: 'danger', width: 'auto' });
        return true;
    }


    let bparams = "";
    let bpramsTotal = 0;
    let bpramsTotalV = document.getElementsByName("para_select_body_text").length;
    document.getElementsByName("para_select_body_text").forEach((item, index) => {
        if (item.value) {
            bparams += item.value + "|";
            bpramsTotal += 1;
        }

    });

    if (text_is_param == '1') {
        if (text == "") {
            $.bootstrapGrowl("parametro del header es requerido", { type: 'danger', width: 'auto' });
            return true;
        }
    }

    if (bpramsTotal != bpramsTotalV) {
        $.bootstrapGrowl("parametros del body incompletos", { type: 'danger', width: 'auto' });
        return true;
    }

    let header = {
        type, url, text
    };

    let codeconfig = document.querySelector('input[name="radiodeviceslist"]:checked').value;
    let cola = document.getElementById("canpana_select_cola").value;
    let trespuesta = document.getElementById("canpana_select_weather").value;



    let data = {
        codeconfig,
        cola,
        trespuesta,
        template,
        language,
        campaign,
        date: date,
        excel,
        header,
        bparams,
    };
    e.disabled = true;
    try {
        const resp = await axios({
            method: "post",
            url: "/whatsapp/CampaignsRegister",
            data: data,
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Status == 0) {

            if (typeprogration == '1') {
                document.getElementById("btnCloseModalCampaign").click();
                //send
                await sendCampaign(resp?.data?.Message2);
            }
            clearCampaign();
            getCampaigns();
            document.getElementById("btnCloseModalCampaign").click();
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'success', width: 'auto' });

        } else {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'danger', width: 'auto' });
        }

    } catch (e) {
        $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
    }
    e.disabled = false;
}

//update
async function UpdateCampaigns(e) {
    let template = document.getElementById("template_seleted_value").value;
    let campaign = document.getElementById("name_campana").value;
    let dat = document.getElementById("date_campana").value;
    let hour = document.getElementById("hour_campana").value;
    let excel = document.getElementById("url_execel_campana").value;


    let type = document.getElementById("cam_type_header").value;
    let url = document.getElementById("cam_type_header_value").value;
    let text_is_param = document.getElementById("header_is_param").value;
    let text = document.getElementById("para_select_header_text") ? document.getElementById("para_select_header_text").value : "";
    let typeprogration = document.getElementById("type_programtion_selected").value;

    if (template == "") {
        $.bootstrapGrowl("Selecione un template", { type: 'danger', width: 'auto' });
        return true;
    }
    if (campaign == "") {
        $.bootstrapGrowl("campo nombre de campaña requerido", { type: 'danger', width: 'auto' });
        return true;
    }
    date = null;
    if (typeprogration == "2") {
        if (dat == "") {
            $.bootstrapGrowl("campo fecha requerido", { type: 'danger', width: 'auto' });
            return true;
        }
        if (hour == "") {
            $.bootstrapGrowl("campo hora requerido", { type: 'danger', width: 'auto' });
            return true;
        }
        date = dat + " " + hour;
    }

    if (excel == "") {
        $.bootstrapGrowl("campo destinatarios requerido", { type: 'danger', width: 'auto' });
        return true;
    }


    let bparams = "";
    let bpramsTotal = 0;
    let bpramsTotalV = document.getElementsByName("para_select_body_text").length;
    document.getElementsByName("para_select_body_text").forEach((item, index) => {
        if (item.value) {
            bparams += item.value + "|";
            bpramsTotal += 1;
        }

    });

    if (text_is_param == '1') {
        if (text == "") {
            $.bootstrapGrowl("parametro del header es requerido", { type: 'danger', width: 'auto' });
            return true;
        }
    }

    if (bpramsTotal != bpramsTotalV) {
        $.bootstrapGrowl("parametros del body incompletos", { type: 'danger', width: 'auto' });
        return true;
    }

    let header = {
        type, url, text
    };

    let code = document.getElementById("key_campaign_").value;

    let data = {
        code,
        campaign,
        date: date,
        excel,
        header,
        bparams,
    };
    e.disabled = true;

    try {
        const resp = await axios({
            method: "post",
            url: "/whatsapp/CampaignsUpdate",
            data: data,
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Status == 0) {
            clearCampaign();
            getCampaigns();
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'success', width: 'auto' });
        } else {
            $.bootstrapGrowl(resp?.data?.Message1, { type: 'danger', width: 'auto' });
        }

    } catch (e) {
        $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
    }
    e.disabled = false;
}


//clear
function clearCampaign() {
    document.getElementById("key_campaign_").value = '';
    document.getElementById("btnselectedtemplate_new").disabled = false;
    document.getElementById("template_seleted_value").value = "";
    document.getElementById("name_campana").value = "";
    document.getElementById("hour_campana").value = "";
    document.getElementById("date_campana").value = "";

    document.getElementById("execeltext_cam").innerText = "";


    document.getElementById("cam_type_header").value = "";
    document.getElementById("cam_type_header_value").value = "";
    document.getElementById("url_execel_campana").value = "";
    document.getElementById("header_is_param").value = "0";



    document.getElementById("dHeader").style.display = "none";
    document.getElementById("dBodyCamp").style.display = "none";
    document.getElementById("dFooterCamp").style.display = "none";


    document.getElementById("dprevHeaderText").innerText = "";
    document.getElementById("imgprevHeaderImage").src = "";


    document.getElementById("dprevHeaderVideo").style.display = "none";
    document.getElementById("videoprevHeaderVideo").src = "";

    document.getElementById("dprevCampbodyText").innerText = "";
    document.getElementById("prevCampFooterText").innerText = "";

    document.getElementById("rowdqrepliespreviewcom").innerText = "";
    document.getElementById("dacctionprevcam").style.display = "none";

    document.getElementById("smallimage_file_campana_icon").innerHTML = "";
    document.getElementById("execeltext_cam").innerHTML = "";
    document.getElementById("smallvideo_file_campana_icon").innerHTML = '';
    document.getElementById("smalldoc_file_campana_icon").innerHTML = "";
    document.getElementById("canpana_select_cola").value = "0";
    document.getElementById("canpana_select_weather").value = "0";


    //steps
    document.querySelectorAll(".step-item").forEach((item, index) => {
        item.classList.remove("active");
    });
    document.querySelectorAll(".step-contents").forEach((item, index) => {
        item.style.display = "none";
    });
    document.getElementById("step_one").style.display = "";

    document.getElementById("step1").classList.add("active");
    document.getElementById("mfooter_one").style.display = "";

    document.getElementById("mfooter_two").style.display = "none";
    document.getElementById("mfooter_tree").style.display = "none";
    document.getElementById("mfooter_for").style.display = "none";


    //
    document.getElementById("btnselectedtemplate_new").value = "";
    document.getElementById("text_select_template").value = "sin plantilla";
    document.querySelectorAll(".radio_template_select").forEach((sl, index) => {
        sl.checke = false;
    });

    document.querySelectorAll(".template-selected").forEach((rv, index) => {
        rv.classList.remove("template-selected");
    });

}

//send template
async function sendCampaign(code) {

    document.getElementById("loadMe").style.display = "block";
    try {
        const resp = await axios({
            method: "post",
            url: "/whatsapp/SendCampaign",
            data: { code },
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data?.Success == true) {
            Swal.fire({
                icon: 'success',
                title: resp?.data?.Message1,
                showConfirmButton: false,
                timer: 1500
            });
            getCampaigns();
        } else {
            Swal.fire({
                icon: 'error',
                title: resp?.data?.Message1,
                showConfirmButton: false,
                timer: 1500
            })
        }
        
    } catch (e) {
        Swal.fire({
            icon: 'error',
            title: e,
            showConfirmButton: false,
            timer: 1500
        })
    }
    document.getElementById("loadMe").style.display = "none";
    

}

//delete campaign
function deleteCampaign(code) {
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
                    url: "/whatsapp/DestroyCampaign",
                    data: { code },
                    headers: { "Content-Type": "application/json" },
                });
                if (resp?.data?.Status == 0) {
                    getCampaigns();
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


//get list detail campaigns
async function getListCampaignDetails(code) {

    setTimeout(() => {
        if (document.querySelector(".modal-backdrop")) {
            document.querySelector(".modal-backdrop").classList.remove("modal-backdrop");
        }
    }, 5);

    let body = document.getElementById("body_campaign_detail");
    try {
        const resp = await axios({
            method: "post",
            url: "/whatsapp/CampaignsDetail",
            data: { code },
            headers: { "Content-Type": "application/json" },
        });
        if (resp?.data) {
            let html = "";
            resp?.data?.forEach((item, index) => {
                html += ` <tr class="border-bottom border-200">
                              <td class="" style="">${index + 1}</td>
                              <td class="" style="">${item?.Destinatario}</td>
                              <td class="" style="">${item?.Phone}</td>
                              <td class="" style=""> ${statusRenderHtml(item?.EstadoWhatsappCampania)}</td>
                              <td class="" style="">${item?.DescFechaCreacion}</td>
                         </tr>`;

            });
            body.innerHTML = html;
        }
    } catch (e) {
    }

}

function statusRenderHtml(value) {
    if (value == true) {
        return '<span class="badge badge-success">exitoso</span>';

    } else {
        return '<span class="badge badge-danger">fallido</span>';
    }

}






//****************************************** END CRUD*************************************************/


function changeSelectedTemplate(e) {
    document.querySelectorAll(".template-selected").forEach(x => x.classList.remove("template-selected-active"));
    e.classList.add("template-selected-active")
}

async function setTemplateSelected() {
    if (document.querySelector('input[name="radio_template_select"]:checked')) {
        document.getElementById("btnclose_template_selected_footer").click();
        let template = document.querySelector('input[name="radio_template_select"]:checked').value;

        document.getElementById("template_seleted_value").value = template;
        document.getElementById("text_select_template").innerText = template;
        
        getItemTemplate(template);
        document.getElementById("cam_type_header_value").value="";
       

    } else {
        $.bootstrapGrowl("Selecione una plantilla", { type: 'danger', width: 'auto' });
    }
   
    
}

//btn close seleted modal template
function closeModalSelectedTemplateDis() {
    document.getElementById("BtnModalAddCampanaWs").click();
}


function selectListTemplate(data) {
    let html = "";



    //html += ' <option value=""></option>';
    //data.forEach((item, index) => {
    //    if (item?.status == "APPROVED") {
    //        html += ` <option value="${item?.name}">${item?.name} </option>`;
    //    }

    //});
    //document.getElementById("template_canpana_select").innerHTML = html;
    data.forEach((item, index) => {

        


        if (item?.status == "APPROVED") {

            html += `<div class="col-3 template-selected" style="overflow: scroll;" onclick="changeSelectedTemplate(this)">`;
            html += `   <label for="radio_template_select_${index}" style="width: 100%;cursor:pointer" >`;
            html += `   <div><input type="radio" name="radio_template_select" value="${item?.name}" id="radio_template_select_${index}" hidden></div>`;
            html += `      <div>`;

            html += `<div class="card" style="width: 100%;" >
                            <div class="card-header card-header_">
                              <div class="row justify-content-between">
                                <div class="col"><strong style="font-size: 11px;">${item?.name}</strong></div>
                               </div>
                             </div>
                        <div class="card-body p-0 m-0">

                        <div class="" >
                            <div class="talk-bubble_c tri-right left-top">
                                <div class="talktext ">`;

            item?.components.forEach((com, indx) => {
                if (com?.type == "HEADER") {
                    html += `<div id="dheaderPreview_list">`;
                    if (com?.format == "TEXT") {
                        html += `<strong id="previewText_list" style="font-size: 12px;font-weight:600;">${com?.text}</strong>`;
                    } else if (com?.format == "IMAGE") {
                        html += `<div id="previewImagen_list" class="dpreview_">
                                            <i class="fa fa-picture-o fa-5x " aria-hidden="true"></i>
                                        </div>`;
                    } else if (com?.format == "VIDEO") {
                        html += `<div id="previewVideo_list" class="dpreview_">
                                            <i class="fa fa-play-circle fa-5x " aria-hidden="true"></i>
                                        </div>`;
                    } else if (com?.format == "DOCUMENT") {
                        html += `<div id="previewDocument_list"  class="dpreview_">
                                            <i class="fa fa-file-text fa-5x " aria-hidden="true"></i>
                                        </div>`;
                    } else {

                    }
                    html += ` </div>`;
                }

                if (com?.type == "BODY") {
                    html += `<div id="previewBody_list" style=" font-size: 10px;margin-top:2px; color: #362d2d">${com?.text}</div>`;
                }
                if (com?.type == "FOOTER") {
                    html += `<small id="previewFooter_list" style="color: #a7a4a4">${com?.text}</small>`;
                }
                if (com?.type == "BUTTONS") {

                    let quicks = com?.buttons.filter(q => q.type != "QUICK_REPLY");
                    if (quicks.length > 0) {
                        html += `<div class="line-divider"></div>`;
                        html += `<div id="previewAcction" style="margin-top: 5px; display: block;">`;
                        let phone = com?.buttons.filter(q => q.type == "PHONE_NUMBER");
                        let web = com?.buttons.filter(q => q.type == "URL");
                        if (phone.length > 0) {
                            html += ` <div class="" style="text-align: center; display: block;" id="dcallTextPreview">
                                                <i class="fa fa-phone" style="color: #2383f5" aria-hidden="true"></i>
                                                <span id="callTextPreview" style="color: #0493f9">${phone[0].text}</span>
                                            </div>`;
                        }
                        if (web.length > 0) {
                            html += `<div class="" style="text-align: center; display: block;" id="dlinkTextPreview">
                                                <i class="fa fa-external-link" style="color: #2383f5" aria-hidden="true"></i>
                                                <a href="${web[0].url}" target="_blank"> <span id="linkTextPreview" style="color: #0493f9">${web[0].text}</span></a>
                                             </div>`;
                        }

                        html += `</div>`;
                    }

                }

            });

            html += `</div></div>`;
            item?.components.forEach((com, indx) => {
                if (com?.type == "BUTTONS") {
                    html += `<div class="talk-qreply_c mt-4" id="dqrepliespreview_list">`;
                    html += `<div class="row justify-content-around m-1">`;
                    let quicks = com?.buttons.filter(q => q.type == "QUICK_REPLY");
                    quicks.forEach((btns, key) => {
                        html += ` <div class="col-12 mb-2 btn-qreplies " id="previewFirtButtonQreplies_one_list">${btns?.text}</div>`;
                    });
                    html += `</div>`;
                    html += `</div>`;
                }
            });
            html += `</div>`;

            html += `</div>`;





            html += `</div>`;





            html += `   </div>`;
            html += `  </label>`;
            html += `   </div>`


        }

    });


    document.getElementById("template_canpana_select_content").innerHTML = html;
}

//const template_canpana_select = document.getElementById('template_canpana_select');
//template_canpana_select.addEventListener('change', function handleChange(event) {
//    if (event.target.value != "") {
//        getItemTemplate(event.target.value);
//    }
//});

function ModalBackdropRemove() {
    document.getElementById("TituloTextCampaign").innerText = "Nueva campaña";
    document.getElementById("BtnRegistrarCampaigns").style.display = "";
    document.getElementById("BtnUpdateCampaigns").style.display = "none";
    clearModalZindex();
}

//reset data open modal create
const BtnModalAddCampanaWsHandleClick = document.getElementById('BtnModalAddCampanaWs');
BtnModalAddCampanaWsHandleClick.addEventListener('click', function handleChange(event) {
    //clearCampaign()
});



//get template selected validation data 
function getItemTemplate(name) {

    let code = document.getElementById("CodigoWhatsappConfiguracion").value;
    let data = {
        code, name
    };

    if (code != "" && name != "") {
        $.ajax({
            data: JSON.stringify(data),
            type: "POST",
            url: "/whatsapp/getItemTemplate",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (resp) {
                document.getElementById("header_is_param").value = "0"
                document.getElementById("cam_type_header").value = "NINGUNA"

                document.getElementById("dHeader").style.display = "none";

                document.getElementById("dprevHeaderText").innerText = "";

                document.getElementById("dFooterCamp").style.display = "none"
                document.getElementById("dprevCampbodyText").innerText = "";
                document.getElementById("prevCampFooterText").innerText = "";

                document.getElementById("dprevHeaderImage").style.display = "none";
                document.getElementById("dheader_Image").style.display = "none";
                document.getElementById("dheader_Video").style.display = "none";
                document.getElementById("dprevHeaderVideo").style.display = "none";
                document.getElementById("dheader_Document").style.display = "none";
                document.getElementById("dprevHeaderDoc").style.display = "none";

                document.getElementById("dheader_text").style.display = "none";
                document.getElementById("txt_header_text").innerText = "";
                document.getElementById("d_params_texts").innerHTML = "";

                document.getElementById("dBodyCamp").style.display = "none";
                document.getElementById("txt_body_camp").innerText = "";
                document.getElementById("d_params_body").innerHTML = "";

                document.getElementById("rowdqrepliespreviewcom").innerHTML = "";
                document.getElementById("dacctionprevcam").style.display = "none";

                document.getElementById("language_template").value = resp?.Date?.language;
                resp?.Date?.components.forEach((item, index) => {
                    if (item.type == "HEADER") {
                        document.getElementById("dHeader").style.display = "";
                        document.getElementById("cam_type_header").value = item?.format;
                        if (item.format == "TEXT") {
                            document.getElementById("dheader_text").style.display = "";
                            document.getElementById("txt_header_text").innerText = item?.text;
                            document.getElementById("dprevHeaderText").innerText = item?.text;
                            let count = countParms(item.text);
                            if (count > 0) {
                                generateInputHeaderText()
                            }

                        } if (item.format == "IMAGE") {
                            document.getElementById("txt_header_text").innerText = "";
                            document.getElementById("dprevHeaderText").innerText = "";


                            document.getElementById("dheader_Image").style.display = "";
                            document.getElementById("dprevHeaderImage").style.display = "";

                        } if (item.format == "VIDEO") {
                            document.getElementById("txt_header_text").innerText = "";
                            document.getElementById("dprevHeaderText").innerText = "";

                            document.getElementById("dheader_Video").style.display = "";
                            document.getElementById("dprevHeaderVideo").style.display = "";

                        } if (item.format == "DOCUMENT") {
                            document.getElementById("txt_header_text").innerText = "";
                            document.getElementById("dprevHeaderText").innerText = "";

                            document.getElementById("dheader_Document").style.display = "";
                            document.getElementById("dprevHeaderDoc").style.display = "";

                        }

                    }
                    if (item.type == "BODY") {
                        document.getElementById("dBodyCamp").style.display = "";
                        document.getElementById("txt_body_camp").innerText = item.text;
                        document.getElementById("dprevCampbodyText").innerText = item.text;
                        document.getElementById("dprevCampbodyText").style.display = "";
                        let count = countParms(item.text);
                        if (count > 0) {
                            document.getElementById("d_params_body").style.display = "";
                            generateInput(count)
                        }
                    }

                    if (item.type == "FOOTER") {
                        document.getElementById("dFooterCamp").style.display = ""
                        document.getElementById("txt_footer_camp").innerText = item.text;

                        document.getElementById("prevCampFooterText").innerText = item.text;
                        document.getElementById("prevCampFooterText").style.display = "";
                    }

                    if (item.type == "BUTTONS") {
                        let quickReply = item.buttons.filter(q => q.type == "QUICK_REPLY");

                        if (quickReply.length > 0) {
                            document.getElementById("rowdqrepliespreviewcom").style.display = "";
                            let htmlqr = "";
                            item.buttons.forEach((b, index) => {
                                htmlqr += `<div class="col-12 mb-2 btn-qreplies ">${b?.text}</div>`;
                            })
                            document.getElementById("rowdqrepliespreviewcom").innerHTML = htmlqr;
                        } else {

                            document.getElementById("dacctionprevcam").style.display = ""
                            let acctionCall = item.buttons.filter(q => q.type == "PHONE_NUMBER");
                            if (acctionCall.length > 0) {
                                document.getElementById("dcallTextPreviewcam").style.display = "",
                                    document.getElementById("callTextPreviewcam").innerText = acctionCall?.[0].text;
                            }
                            let acctionLink = item.buttons.filter(q => q.type == "URL");
                            if (acctionLink.length > 0) {
                                document.getElementById("dlinkTextPreviewcam").style.display = "",
                                    document.getElementById("linkTextPreviewcam").innerText = acctionLink?.[0].text;
                            }
                        }
                    }

                })
            },
            complete: function (data) {

            }
        });
    }
}


function countParms(str) {
    var ch = '{{';
    return count = str.split(ch).length - 1;
}




function generateInput(total) {
    let container = document.getElementById("d_params_body");
    html = "";
    for (var i = 0; i < total; i++) {
        html += `<div class="mb-2">
        <span class="field-icon">{{${i + 1}}}</span>
        <select class="form-control para_select_body_text" name="para_select_body_text">
          <option value=""></option>
            <option value="0">codigocliente</option>
            <option value="1">nombrescliente</option>
            <option value="2">apellidoscliente</option>
            <option value="3">celular</option>
            <option value="4">correo</option>
            <option value="5">nrodocumentocliente</option>
            <option value="6">nombremembresia</option>
            <option value="7">nrocontrato</option>
            <option value="8">fechainicio</option>
            <option value="9">fechafin</option>
        </select>
    </div>`;

        //let div = document.createElement("div");
        //let span = document.createElement("span");
        //let input = document.createElement("input");

        //span.setAttribute("class", "field-icon");
        //span.innerText = `{{${i + 1}}}`;

        //input.setAttribute("class","form-control mb-2");
        //input.setAttribute("id",`idyn${i+1}`);
        //input.setAttribute("name",`input_dinamyc_body`);

        //div.appendChild(span);
        //div.appendChild(input);
        //container.appendChild(div)
    }
    container.innerHTML = html
}
function generateInputHeaderText() {
    let container = document.getElementById("d_params_texts");

    //let div = document.createElement("div");
    //let span = document.createElement("span");
    //let input = document.createElement("input");

    //span.setAttribute("class", "field-icon");
    //span.innerText = `{{1}}`;

    //input.setAttribute("class", "form-control mb-2");
    //input.setAttribute("id", `idheadertext_dynamic`);
    //input.setAttribute("name", `input_dinamyc_header`);

    //div.appendChild(span);
    //div.appendChild(input);
    //container.appendChild(div)
    let html = "";
    html += `<div class="mb-2">
        <span class="field-icon">{{1}}</span>
        <select class="form-control para_select_body_text" id="para_select_header_text">
          <option value=""></option>
            <option value="0">codigocliente</option>
            <option value="1">nombrescliente</option>
            <option value="2">apellidoscliente</option>
            <option value="3">celular</option>
            <option value="4">correo</option>
            <option value="5">nrodocumentocliente</option>
            <option value="6">nombremembresia</option>
            <option value="7">nrocontrato</option>
            <option value="8">fechainicio</option>
            <option value="9">fechafin</option>
        </select>
    </div>`;
    document.getElementById("header_is_param").value = "1"
    container.innerHTML = html;

}

//upload file campana
async function uploadfileImage(e) {
    if (e) {
        if (e.files?.[0]) {
            e.disabled = true;
            document.getElementById("smallimage_file_campana").innerText = "subiendo...";
            document.getElementById("smallimage_file_campana_icon").innerHTML = "";
            let url = await uploadAzurePatch(e.files[0])
            if (url != "") {
                $.bootstrapGrowl("Imagen subida correctamente", { type: 'success', width: 'auto' });
                document.getElementById("cam_type_header_value").value = url;
                document.getElementById("imgprevHeaderImage").src = url;

            } else {
                $.bootstrapGrowl("No se pudo subir la imagen", { type: 'danger', width: 'auto' });
                document.getElementById("cam_type_header_value").value = "";
                document.getElementById("imgprevHeaderImage").src = "";
            }
            e.disabled = false;
            document.getElementById("smallimage_file_campana").innerText = "";
            document.getElementById("smallimage_file_campana_icon").innerHTML = '<span><i class="fa fa-paperclip" aria-hidden="true"></i> subido correctamente</span>';
        }
    }
}
async function uploadfilePDF(e) {
    if (e) {
        if (e.files?.[0]) {
            document.getElementById("smalldoc_file_campana").innerText = "subiendo...";
            document.getElementById("smalldoc_file_campana_icon").innerHTML = "";
            let url = await uploadAzurePatch(e.files[0])
            if (url != "") {
                $.bootstrapGrowl("PDF subida correctamente", { type: 'success', width: 'auto' });
                document.getElementById("cam_type_header_value").value = url;
                document.getElementById("embedprevHeaderDoc").src = url;

            } else {
                $.bootstrapGrowl("No se pudo subir la PDF", { type: 'danger', width: 'auto' });
                document.getElementById("cam_type_header_value").value = "";
                document.getElementById("embedprevHeaderDoc").src = "";

            }
            document.getElementById("smalldoc_file_campana").innerText = "";
            document.getElementById("smalldoc_file_campana_icon").innerHTML = '<span><i class="fa fa-paperclip" aria-hidden="true"></i> subido correctamente</span>';
        }
    }
}

async function uploadfileVideo(e) {
    if (e) {
        if (e.files?.[0]) {
            document.getElementById("smallvideo_file_campana").innerText = "subiendo...";
            document.getElementById("smallvideo_file_campana_icon").innerHTML = '';

            let url = await uploadAzurePatch(e.files[0])
            if (url != "") {
                $.bootstrapGrowl("Video subida correctamente", { type: 'success', width: 'auto' });
                document.getElementById("cam_type_header_value").value = url;
                document.getElementById("videoprevHeaderVideo").src = url;
            } else {
                $.bootstrapGrowl("No se pudo subir la video", { type: 'danger', width: 'auto' });
                document.getElementById("cam_type_header_value").value = '';
                document.getElementById("videoprevHeaderVideo").src = '';

            }
            document.getElementById("smallvideo_file_campana").innerText = "";
            document.getElementById("smallvideo_file_campana_icon").innerHTML = '<span><i class="fa fa-paperclip" aria-hidden="true"></i> subido correctamente</span>';
        }
    }
}







//-------------------------------- validate excel --------------------------------------------

const INTEGER = "campo requerido/tipo número";
const _isInteger = { mask: /^[0-9]*$/ };

const STRING = "campo requerido";
const _isStringNotNull = { mask: /[\S\s]+[\S]+/ };



const PHONE = "A standar  10 digit phone number (I.e. 123-456-7890)";
const _isPhone = { mask: /^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$/ };

const DECIMAL = "A decimal format (I.e. 1234.10 or 1234)";
const _isDecimal = { mask: /^\d*(\.\d+)?$/ };



const validCodeCustomer = (row, element, errors) => {
    const COLUMN = "codigocliente";
    let validCodeCustomer = _isInteger.mask.test(element[COLUMN]);
    if (!validCodeCustomer) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: INTEGER,
            value: element[COLUMN]
        });
    }
};

const validCustomer = (row, element, errors) => {
    const COLUMN = "nombrecliente";

    if (!element[COLUMN]) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    } else {
        let validCustomer = _isStringNotNull.mask.test(element[COLUMN]);
        if (!validCustomer) {
            let rowError = row + 2;
            errors.push({
                row: rowError,
                column: COLUMN,
                type: STRING,
                value: element[COLUMN]
            });
        }
    }


};

const validCustomerSurname = (row, element, errors) => {
    const COLUMN = "apellidoscliente";
    if (!element[COLUMN]) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    } else {
        let validCustomerSurname = _isStringNotNull.mask.test(element[COLUMN]);
        if (!validCustomerSurname) {
            let rowError = row + 2;
            errors.push({
                row: rowError,
                column: COLUMN,
                type: STRING,
                value: element[COLUMN]
            });
        }
    }

};

const validPhone = (row, element, errors) => {
    const COLUMN = "celular";
    let validPhone = _isInteger.mask.test(element[COLUMN]);
    if (!validPhone) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: INTEGER,
            value: element[COLUMN]
        });
    }
};

const validCorreo = (row, element, errors) => {
    const COLUMN = "correo";
    let validCorreo = _isStringNotNull.mask.test(element[COLUMN]);
    if (!validCorreo) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    }
};

const validNumberDocument = (row, element, errors) => {
    const COLUMN = "nrodocumentocliente";
    let validNumberDocument = _isStringNotNull.mask.test(element[COLUMN]);
    if (!validNumberDocument) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    }
};


const validMembresia = (row, element, errors) => {
    const COLUMN = "nombremembresia";
    if (!element[COLUMN]) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    } else {
        let validMembresia = _isStringNotNull.mask.test(element[COLUMN]);
        if (!validMembresia) {
            let rowError = row + 2;
            errors.push({
                row: rowError,
                column: COLUMN,
                type: STRING,
                value: element[COLUMN]
            });
        }
    }

};


const validContrato = (row, element, errors) => {
    const COLUMN = "nrocontrato";
    let validContrato = _isStringNotNull.mask.test(element[COLUMN]);
    if (!validContrato) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    }
};

const validDateStart = (row, element, errors) => {
    const COLUMN = "fechainicio";

    if (!element[COLUMN]) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    } else {
        let validDateStart = _isStringNotNull.mask.test(element[COLUMN]);
        if (!validDateStart) {
            let rowError = row + 2;
            errors.push({
                row: rowError,
                column: COLUMN,
                type: STRING,
                value: element[COLUMN]
            });
        }
    }


};

const validDateEnd = (row, element, errors) => {
    const COLUMN = "fechafin";
    if (!element[COLUMN]) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING,
            value: element[COLUMN]
        });
    } else {
        let validDateEnd = _isStringNotNull.mask.test(element[COLUMN]);
        if (!validDateEnd) {
            let rowError = row + 2;
            errors.push({
                row: rowError,
                column: COLUMN,
                type: STRING,
                value: element[COLUMN]
            });
        }
    }

};

const validateExcel = (rows) => {
    let errors = [];
    for (let i = 0; i < rows.length; i++) {
        let element = rows[i];
        validCodeCustomer(i, element, errors);
        validCustomer(i, element, errors);
        validCustomerSurname(i, element, errors);
        validPhone(i, element, errors);
        validCorreo(i, element, errors);
        validNumberDocument(i, element, errors);
        validMembresia(i, element, errors);
        validContrato(i, element, errors);
        validDateStart(i, element, errors);
        validDateEnd(i, element, errors);
    }

    return errors;
};




async function uploadfileExecel(e) {
    let inputFile = document.getElementById("execel_file_campana");
    const selectedFile = e.files?.[0];
    let data = [{
        "name": "jayanth",
        "data": "scd",
        "abc": "sdef"
    }]

    XLSX.utils.json_to_sheet(data, 'out.xlsx');
    document.getElementById("execeltext_cam").innerHTML = '';
    document.getElementById("errors_excel_validate").innerHTML = ''

    if (selectedFile?.type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
        $.bootstrapGrowl("Tipo de archivo no permitido", { type: 'danger', width: 'auto' });
        document.getElementById("url_execel_campana").value = "";
        return;
    }
    //application/vnd.ms-excel

    if (selectedFile) {
        let fileReader = new FileReader();
        fileReader.readAsBinaryString(selectedFile);
        fileReader.onload = (event) => {
            let data = event.target.result;
            let workbook = XLSX.read(data, { type: "binary" });
            workbook.SheetNames.forEach(async (sheet, index) => {
                let rowObject = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheet]);
                let errors = validateExcel(rowObject);
                if (errors.length == 0) {
                    document.getElementById("smallexecel_file_campana").innerText = "subiendo...";
                    let url = await uploadAzurePatch(selectedFile)
                    if (url != "") {
                        inputFile.value = ""
                        document.getElementById("execeltext_cam").innerHTML = '<span><i class="fa fa-check-circle-o text-success" aria-hidden="true"></i> subido correctamente</span>';
                        document.getElementById("url_execel_campana").value = url;
                        //document.getElementById("execeltext_cam").innerText = selectedFile?.name
                    } else {
                        $.bootstrapGrowl("No se pudo subir  destinatario", { type: 'danger', width: 'auto' });
                        document.getElementById("url_execel_campana").value = '';

                    }
                    document.getElementById("smallexecel_file_campana").innerText = "";


                } else {
                    inputFile.value = ""
                    //hay errores
                    let html = "";
                    errors.forEach((item, index) => {
                        html += `<li class="list-group-item p-0"><span style="font-weight: 600;font-size: 13px;color:#ab4a4a">${item?.column}:</span> <small style="font-size: 12px;color:#db3737">${item?.type}(fila:${item?.row})</small></li>`;
                    });
                    document.getElementById("errors_excel_validate").innerHTML = html;
                }

            });
        }
    } else {
        inputFile.value = ""
    }








}
//-------------------------------- validate excel --------------------------------------------

async function uploadAzurePatch(file) {
    try {
        if (file) {
            var data = new FormData;
            data.append("ImageFile", file);
            const resp = await axios({
                method: "post",
                url: "/whatsapp/uploadFile",
                data: data,
                headers: { "Content-Type": "multipart/form-data" },
            });
            if (resp?.data?.Status == 0) {
                return resp?.data?.Message1;
            } else {
                return "";
            }
        }
    } catch (ex) {
        return "";
    }

}

//----------------------------------------------------------------------
function showOptionProgramacion(e, value) {
    document.querySelectorAll(".item-option-prog").forEach((item, index) => {
        item.classList.remove("item-option-prog-active");
    });
    e.classList.add("item-option-prog-active")
    switch (value) {
        case 0:
            document.getElementById("dcontent_programation").style.display = 'none';
            document.getElementById("type_programtion_selected").value = "0";
            document.getElementById("BtnRegistrarCampaigns").innerText = "Guardar";
            break;
        case 1:
            document.getElementById("dcontent_programation").style.display = 'none';
            document.getElementById("type_programtion_selected").value = "1";
            document.getElementById("BtnRegistrarCampaigns").innerText = "Enviar ahora";
            break;
        case 2:
            document.getElementById("dcontent_programation").style.display = '';
            document.getElementById("type_programtion_selected").value = "2";
            document.getElementById("BtnRegistrarCampaigns").innerText = "Guardar programación";
            break;

        default:
    }

}

function closeModalCampaign() {
    clearCampaign();
}

//functions steps
function nextStep(val) {
    switch (val) {
        case 2:
            if (document.getElementById("name_campana").value == "") {
                document.getElementById("name_campana").focus();
                return;
            }
            let cola = document.getElementById("canpana_select_cola").value;
            if (parseInt(cola) == 0) {
                $.bootstrapGrowl("seleccione cola de destino", { type: 'danger', width: 'auto' });
                return;
            }
            let tresp = document.getElementById("canpana_select_weather").value;
            if (parseInt(tresp) == 0) {
                $.bootstrapGrowl("seleccione el tiempo máximo de respuesta", { type: 'danger', width: 'auto' });
                return;
            }

            document.getElementById("step_one").style.display = "none";
            document.getElementById("step_two").style.display = "";
            document.getElementById("step2").classList.add("active");

            document.getElementById("mfooter_one").style.display = "none";
            document.getElementById("mfooter_two").style.display = "";

            break;


        case 3:
            if (document.getElementById("template_seleted_value").value == "") {
                $.bootstrapGrowl("seleccione una plantilla", { type: 'danger', width: 'auto' });
                return;
            }
            let typeH = document.getElementById("cam_type_header").value;
            if (typeH == "IMAGE" || typeH == "VIDEO" || typeH == "DOCUMENT") {
                if (document.getElementById("cam_type_header_value").value == "") {
                    $.bootstrapGrowl("archivo adjunto requerido", { type: 'danger', width: 'auto' });
                    return;
                }
            }

            let text_is_param = document.getElementById("header_is_param").value;
            let text = document.getElementById("para_select_header_text") ? document.getElementById("para_select_header_text").value : "";
            let bparams = "";
            let bpramsTotal = 0;
            let bpramsTotalV = document.getElementsByName("para_select_body_text").length;
            document.getElementsByName("para_select_body_text").forEach((item, index) => {
                if (item.value) {
                    bparams += item.value + "|";
                    bpramsTotal += 1;
                }
            });

            if (text_is_param == '1') {
                if (text == "") {
                    $.bootstrapGrowl("parametro del header es requerido", { type: 'danger', width: 'auto' });
                    return;
                }
            }
            if (bpramsTotal != bpramsTotalV) {
                $.bootstrapGrowl("parametros del body incompletos", { type: 'danger', width: 'auto' });
                return;
            }

            document.getElementById("step_two").style.display = 'none';
            document.getElementById("step_tree").style.display = '';
            document.getElementById("step3").classList.add("active");

            document.getElementById("mfooter_tree").style.display = "";
            document.getElementById("mfooter_two").style.display = "none";

            break;

        case 4:
            if (document.getElementById("url_execel_campana").value == "") {
                $.bootstrapGrowl("archivo adjunto requerido", { type: 'danger', width: 'auto' });
                return;
            }

            document.getElementById("step_two").style.display = 'none';
            document.getElementById("step_tree").style.display = 'none';
            document.getElementById("step_for").style.display = '';
            document.getElementById("step4").classList.add("active");

            document.getElementById("mfooter_one").style.display = "none";
            document.getElementById("mfooter_tree").style.display = "none";
            document.getElementById("mfooter_two").style.display = "none";
            document.getElementById("mfooter_for").style.display = "";
            break;
        default:
            break;
    }
}
function prevStep(val) {
    document.querySelectorAll(".step-item").forEach((item, index) => {
        item.classList.remove("active");
    })
    switch (val) {
        case 1:
            document.getElementById("step_one").style.display = "";
            document.getElementById("step_two").style.display = "none";
            document.getElementById("step1").classList.add("active");

            document.getElementById("mfooter_one").style.display = "";
            document.getElementById("mfooter_two").style.display = "none";
            break;

        case 2:
            document.getElementById("step_one").style.display = "none";
            document.getElementById("step_tree").style.display = "none";
            document.getElementById("step_two").style.display = "";
            document.getElementById("step1").classList.add("active");
            document.getElementById("step2").classList.add("active");

            document.getElementById("mfooter_one").style.display = "none";
            document.getElementById("mfooter_two").style.display = "";
            document.getElementById("mfooter_tree").style.display = "none";
            break;
        case 3:
            document.getElementById("step_one").style.display = "none";
            document.getElementById("step_tree").style.display = "";
            document.getElementById("step_two").style.display = "none";
            document.getElementById("step_for").style.display = "none";
            document.getElementById("step1").classList.add("active");
            document.getElementById("step2").classList.add("active");
            document.getElementById("step3").classList.add("active");


            document.getElementById("mfooter_one").style.display = "none";
            document.getElementById("mfooter_two").style.display = "none";
            document.getElementById("mfooter_for").style.display = "none";
            document.getElementById("mfooter_tree").style.display = "";
            break;
        default:
    }
}

