
function evemt_changeApp(e, type) {
    document.querySelectorAll(".ItemNotis").forEach((item, index) => {
        item.style.display = 'none';
    });
    document.querySelectorAll(".item_menu_sd").forEach((item, index) => {
        item.classList.remove("active")
    });
    switch (type) {
        case 1:
            document.getElementById("dlistadodenotificacionhref").classList.add("active");
            document.getElementById("dlistadodenotificacion").style.display = 'block';
            listNotiapp();
            break;
        default:
    }

}

//*********************************************** START CRUD ********************************************/



function listNotiapp(PageNumber = 1) {
    $("#kgrid_list_notispush").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            transport: {
                read: function (options) {
                    $.ajax({
                        data: JSON.stringify({ PageNumber }),
                        type: "POST",
                        url: "/notiapp/ListPagination",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            let data = msg?.List;
                            options.success(data);
                            if (data.lengh > 0) {
                                var CantidadPaginas = parseInt(msg?.Paging.TotalRecord / msg?.Paging.PageRecords);
                                if (PageNumber == 1 && CantidadPaginas > 0) {
                                    var htmlOpcion = "";
                                    for (var i = 1; i <= CantidadPaginas; i++) {
                                        htmlOpcion += `<option value=${i}>${i} </option>`;
                                    }
                                    document.getElementById("kgrid_list_notispush_page").innerHTML = htmlOpcion;
                                }
                            }

                        }, complete: function () {

                        }
                    });
                }
            }
        },
        //selectable: "row",
        //sortable: true,
        height: 770,
        columns: [
            {
                field: "Title",
                title: "<span class='kendo_camp_header_text' >Título</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",

                }
            }, {
                field: "Body",
                title: "<span class='kendo_camp_header_text' >Detalle</span>",
                width: 10,
                attributes: {
                    style: "font-size:11px;",

                }
            },
            {
                title: "<span class='kendo_camp_header_text'>Estado</span>",
                template: function (item) {
                    let html = "";
                    if (item?.Send == true) {
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
            }, {

                title: "<span class='kendo_camp_header_text'>Recurrente</span>",
                template: function (item) {
                    let html = "";
                    if (item?.Recurrent == true) {
                        html = `Si`;
                    } else {
                        html = `No`;
                    }
                    return html;
                },
                width: 5,
                attributes: {
                    style: "font-size:11px;"
                }
            },
            {
                title: "<span class='kendo_camp_header_text'>Fecha Creación</span>",
                field: "DescFechaCreacion",
                width: 10,
                attributes: {
                    style: "font-size:11px;"
                }
            },

            {
                title: "<span class='kendo_camp_header_text'>Acciones</span>",
                width: 10,
                template: function (dataItem) {
                    var html = '<div class="p-1">'
                    html += ` <button type="button"  class=" " data-toggle="modal"  data-target="#ModalNotisPush" onclick="ShowGetItem('${dataItem?.CodigoNotificacionesApp}')" >
                          
                                <i class="fa fa-eye icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                        </button>`;

                    if (dataItem.Send == true) {
                        html += `<button type="button" class=" ml-1" data-toggle="modal" data-target="#ModalNotisAddress" onclick="AddressNotiShow('${dataItem?.CodigoNotificacionesApp}')" >
                            <i class="fa fa-info icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                        </button>`;
                    }
                    if (dataItem.Send == false) {
                        html += `<button type="button" class="ml-1" onclick="DestroyNotis('${dataItem?.CodigoNotificacionesApp}')" >
                            <i class="fa fa-trash icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                          </button>`;
                        html += `  <button type="button" class=""  onclick="SendNoty('${dataItem?.CodigoNotificacionesApp}')" >
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

        ]
    });
}



function paginateChangeNtis() {
    let page = document.getElementById("kgrid_list_notispush_page").value;
    listNotiapp(parseInt(page));
}


//register
async function RegisterNotisApp(e) {

    var group = document.getElementById('ntis_group').value;
    var type = document.getElementById('ntis_type').value;
    var title = document.getElementById("ntis_title").value;
    var body = document.getElementById("ntis_body").value;
    var image = document.getElementById("ntis_urlimage").value;
    var recurrent = document.getElementById("ntis_recurrente").checked;
    var description = document.getElementById("kendo_editor_notis").value; // $("#kendo_editor_notis").data("kendoEditor").value();
    var optionMode = document.getElementById("toption_ntis").value;

    var data = {
        group, type, title, body, image, recurrent, description, optionMode

    }
    e.disabled = true;
    const resp = await axios({
        method: "post",
        url: "/notiapp/RegisterNotiApp",
        data: data,
        headers: { "Content-Type": "application/json" },
    });
    if (resp.data?.Success) {
        listNotiapp();
        document.getElementById("btnCloseNotis").click();
        cleanNotiRegister();
        Swal.fire({
            title: '<strong>Aviso</strong>',
            icon: 'success',
            html: resp.data?.Message1,
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
        });
    } else {
        let html = "";
        html += `<div class="d-flex justify-content-center">`
        html += `<ul class="text-start">`
        resp.data.Date.forEach((item, index) => {
            html += `<li>${index + 1}.- ${item}</li>`;
        })
        html += `</ul>`
        html += `</div>`
        Swal.fire({
            title: '<strong>Validaciones</strong>',
            icon: 'error',
            html: html,
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
        });
    }
    e.disabled = false;
}

//clearData
function cleanNotiRegister() {
    document.getElementById('ntis_icon').style.display = 'none';
    document.getElementById('ntis_image').value = "";
    document.getElementById('ntis_group').value = "";
    document.getElementById('ntis_type').value = "";
    document.getElementById("ntis_title").value = "";
    document.getElementById("ntis_body").value = "";
    document.getElementById("ntis_urlimage").value = "";
    document.getElementById("ntis_recurrente").checked = false;
    //$("#kendo_editor_notis").data("kendoEditor").value("");
    document.getElementById("kendo_editor_notis").value = "";
    document.getElementById("toption_ntis").value = 1;
    ModeSendOptionNtis(document.getElementById("opsennowNtis"), 1);

    document.getElementById("mbodyNotisApp").classList.remove("disabledDiv");
    document.getElementById("NotisAppFooter").style.display = '';
}


//show modal add
function showModalNtis() {
    HideBackdrop();
    cleanNotiRegister();
}


//send notify
async function SendNoty(code) {
    document.getElementById("loadMe").style.display = "block";
    try {
        const resp = await axios({
            method: "post",
            url: "/notiapp/SendNotiApp",
            data: { code },
            headers: { "Content-Type": "application/json" },
        });
        if (resp.data.Success) {
            listNotiapp()
            $.bootstrapGrowl(resp.data.Message1, { type: 'success', width: 'auto' });
        } else {
            $.bootstrapGrowl(resp.data.Message1, { type: 'danger', width: 'auto' });
        }
    } catch (e) {
        $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
    }
    document.getElementById("loadMe").style.display = "none";
}



//show get item
async function ShowGetItem(code) {
    HideBackdrop();
    document.getElementById("mbodyNotisApp").classList.add("disabledDiv");
    document.getElementById("NotisAppFooter").style.display = 'none';
    try {
        const resp = await axios({
            method: "post",
            url: "/notiapp/getItemNotiApp",
            data: { code },
            headers: { "Content-Type": "application/json" },
        });

        if (resp.data.Success) {
            let item = resp.data?.Date;
            document.getElementById('ntis_group').value = item?.Group;

            document.getElementById('ntis_type').value = item?.TipeNoty;
            document.getElementById("ntis_title").value = item?.Title;
            document.getElementById("ntis_body").value = item?.Body;
            document.getElementById("ntis_urlimage").value = item?.UrlImage;
            document.getElementById("ntis_recurrente").checked = item?.Recurrent;
           // $("#kendo_editor_notis").data("kendoEditor").value(item?.DescriptionHtml);
            document.getElementById("kendo_editor_notis").value = item?.DescriptionHtml;

            document.getElementById("toption_ntis").value = item?.Send;

            if (item.Send) {
                ModeSendOptionNtis(document.getElementById("opsennowNtis"), 1);
            } else {
                ModeSendOptionNtis(document.getElementById("opsaveNtis"), 2);
            }
        }
    } catch (e) {
        $.bootstrapGrowl(e, { type: 'danger', width: 'auto' });
    }
}


//detail show address
async function AddressNotiShow(code) {
    HideBackdrop();
    ListNotisPushAddress(code)
    document.getElementById("codeNotiPuhs").value = code;
   
}


//destroy
function DestroyNotis(code) {
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
                    url: "/notiapp/DestroyNotiApp",
                    data: { code },
                    headers: { "Content-Type": "application/json" },
                });
                if (resp?.data?.Success) {
                    listNotiapp();
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

//*********************************************** END CRUD ********************************************/




//*******************************  DETAIL ADDRESS ***********************************/

function ListNotisPushAddress(code, PageNumber = 1) {
    $("#dkendo_body_notisAddress").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            transport: {
                read: function (options) {
                    $.ajax({
                        data: JSON.stringify({ code, PageNumber }),
                        type: "POST",
                        url: "/notiapp/ListPaginationAddress",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            let data = msg?.List;
                            options.success(data);

                            if (data.length > 0) {
                                var CantidadPaginas = parseInt(msg?.Paging.TotalRecord / msg?.Paging.PageRecords);
                                if (PageNumber == 1 && CantidadPaginas > 0) {
                                    var htmlOpcion = "";
                                    for (var i = 1; i <= CantidadPaginas; i++) {
                                        htmlOpcion += `<option value=${i}>${i} </option>`;
                                    }
                                    document.getElementById("kgrid_noti_address_page_detail").innerHTML = htmlOpcion;

                                }
                                document.getElementById("totalnotiaddress").innerText = msg?.Paging.TotalRecord;
                            }
                            document.getElementById("totalnotiaddress").innerText = msg?.Paging.TotalRecord;
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
            field: "FullName",
            title: "<span class='kendo_camp_header_text' >Nombre y apellidos</span>",
            width: 10,
            attributes: {
                style: "font-size:11px;",

            }
        },
        {
            title: "<span class='kendo_camp_header_text'>Estado</span>",
            template: function (item) {
                let html = "";
                if (item?.Send == true) {
                    html = `enviado`;
                } else {
                    html = `fallido`;
                }
                return html;
            },
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        },{
            title: "<span class='kendo_camp_header_text'>Leido</span>",
            template: function (item) {
                let html = "";
                if (item?.Read == true) {
                    html = `Si`;
                } else {
                    html = `No`;
                }
                return html;
            },
            width: 5,
            attributes: {
                style: "font-size:11px;"
            }
        },
        ]
    });
}

function pChangeDetailNotisAddress() {
    let page = document.getElementById("kgrid_noti_address_page_detail").value;
    let code = document.getElementById("codeNotiPuhs").value
    ListNotisPushAddress(code, parseInt(page));
}

//*******************************  DETAIL ADDRESS ***********************************/







//******************************************************* START IMAGE *************************************************/
const typeFilesNoti = ["image/png", "image/jpg", "image/jpeg"];
let sizePerN = 524288; //0.5mb
let sizeFileN = parseFloat(sizePerN / 1048576); //bytes

async function uploadImageNotis(e) {
    let fileInput = document.getElementById("ntis_image");
    let v = e.files[0]

    if (v != undefined) {
        if (
            v.size < sizePerN &&
            typeFilesNoti.includes(v.type)
        ) {
            //ok
            document.getElementById("ntis_urlimage").value = "";
            document.getElementById("ntis_icon").style.display = "none";
            var data = new FormData;
            data.append("image", v);
            const resp = await axios({
                method: "post",
                url: "/home/uploadFile",
                data: data,
                headers: { "Content-Type": "multipart/form-data" },
            });
            if (resp.data?.Success) {
                document.getElementById("ntis_urlimage").value = resp.data?.Message1;
                document.getElementById("ntis_icon").style.display = "";
            }

        } else {
            fileInput.value = ""
            document.getElementById("ntis_urlimage").value = "";
            document.getElementById("ntis_icon").style.display = "none";

            Swal.fire({
                title: '<strong>Validaciones</strong>',
                icon: 'error',
                html: `Tamaño de archivo max ${sizeFileN.toFixed(1)} mb, ${typeFilesNoti.toString()}`,
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
            });
        }
    } else {
        fileInput.value = "",
            document.getElementById("ntis_urlimage").value = "";
        document.getElementById("ntis_icon").style.display = "none";
    }

}


//******************************************************* END IMAGE *************************************************/


//******************************************************* START MODE SEND *************************************************/
function ModeSendOptionNtis(e, value) {
    document.querySelectorAll(".item-option-ecamp").forEach((item, index) => {
        item.classList.remove("item-option-prog-active");
    });
    e.classList.add("item-option-prog-active")
    switch (value) {
        case 1:
            //document.getElementById("dcontent_programation").style.display = 'none';
            document.getElementById("toption_ntis").value = 1;
            document.getElementById("BtnSaveNotis").innerText = "Enviar ahora";
            break;
        case 2:
            // document.getElementById("dcontent_programation").style.display = 'none';
            document.getElementById("toption_ntis").value = 0;
            document.getElementById("BtnSaveNotis").innerText = "Guardar";
            break;
        case 3:
            // document.getElementById("dcontent_programation").style.display = '';
            document.getElementById("toption_ntis").value = 2;
            document.getElementById("BtnSaveNotis").innerText = "Guardar programación";
            break;
        default:
    }
}
//******************************************************* END MODE SEND *************************************************/


//******************************************************* RENDER EDITOR*************************************************/


//var editor = $("#kendo_editor_notis").kendoEditor({
//    stylesheets: [
//        "../content/shared/styles/editor.css",
//    ], imageBrowser: {
//        messages: {
//            dropFilesHere: "Drop files here"
//        },
//        transport: {
//            read: "~/Content/appsfit_img/",
//            destroy: {
//                url: "/kendo-ui/service/ImageBrowser/Destroy",
//                type: "POST"
//            },
//            create: {
//                url: "/kendo-ui/service/ImageBrowser/Create",
//                type: "POST"
//            },
//            thumbnailUrl: "/kendo-ui/service/ImageBrowser/Thumbnail",
//            uploadUrl: "/kendo-ui/service/ImageBrowser/Upload",
//            imageUrl: "/kendo-ui/service/ImageBrowser/Image?path={0}"
//        }
//    },
//    tools: [
//        "bold",
//        "italic",
//        "underline",
//        "undo",
//        "redo",
//        "strikethrough",
//        "justifyLeft",
//        "justifyCenter",
//        "justifyRight",
//        "justifyFull",
//        "insertUnorderedList",
//        "insertOrderedList",
//        "insertUpperRomanList",
//        "insertLowerRomanList",
//        "indent",
//        "outdent",
//        "createLink",
//        "unlink",
//        "insertImage",
//        "insertFile",
//        "subscript",
//        "superscript",
//        "tableWizard",
//        "createTable",
//        "addRowAbove",
//        "addRowBelow",
//        "addColumnLeft",
//        "addColumnRight",
//        "deleteRow",
//        "deleteColumn",
//        "mergeCellsHorizontally",
//        "mergeCellsVertically",
//        "splitCellHorizontally",
//        "splitCellVertically",
//        "tableAlignLeft",
//        "tableAlignCenter",
//        "tableAlignRight",
//        "viewHtml",
//        "cleanFormatting",
//        "copyFormat",
//        "applyFormat",
//        "fontName",
//        "fontSize",
//        "foreColor",
//        "backColor",
//        "print",
//        "formatting"
//    ]
//});


//******************************************************* RENDER EDITOR*************************************************//