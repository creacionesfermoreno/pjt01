
const STRING_ECAMP = "campo requerido";
const _isStringNotNull_ecamp = { mask: /[\S\s]+[\S]+/ };

const validCustomerEcamp = (row, element, errors) => {
    const COLUMN = "correo";

    if (!element[COLUMN]) {
        let rowError = row + 2;
        errors.push({
            row: rowError,
            column: COLUMN,
            type: STRING_ECAMP,
            value: element[COLUMN]
        });
    } else {
        let validCustomerEcamp = _isStringNotNull_ecamp.mask.test(element[COLUMN]);
        if (!validCustomerEcamp) {
            let rowError = row + 2;
            errors.push({
                row: rowError,
                column: COLUMN,
                type: STRING_ECAMP,
                value: element[COLUMN]
            });
        }
    }
};

const validateExcelECamp = (rows) => {
    let errors = [];
    for (let i = 0; i < rows.length; i++) {
        let element = rows[i];
        validCustomerEcamp(i, element, errors);
    }

    return errors;
};

async function uploadfileExecelECamp(e) {
    let inputFile = document.getElementById("ecamp_execel");
    const selectedFile = e.files?.[0];
    let data = [{
        "name": "jayanth",
        "data": "scd",
        "abc": "sdef"
    }]

    XLSX.utils.json_to_sheet(data, 'out.xlsx');
    if (selectedFile?.type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
        $.bootstrapGrowl("Tipo de archivo no permitido", { type: 'danger', width: 'auto' });
        inputFile.value = ""
        return;
    }

    if (selectedFile) {
        let fileReader = new FileReader();
        fileReader.readAsBinaryString(selectedFile);
        fileReader.onload = (event) => {
            let data = event.target.result;
            let workbook = XLSX.read(data, { type: "binary" });
            workbook.SheetNames.forEach(async (sheet, index) => {
                let rowObject = XLSX.utils.sheet_to_row_object_array(workbook.Sheets[sheet]);
                let errors = validateExcelECamp(rowObject);

                if (errors.length == 0) {

                } else {
                    inputFile.value = "";
                    let errorHtml = "";
                    errors?.forEach((item) => {
                        errorHtml += `<span>Columna : ${item.column} ${item.type} en la fila ${item.row}</span></br>`
                    })
                    Swal.fire({
                        title: '<strong>Validaciones</strong>',
                        icon: 'error',
                        html: errorHtml,
                        showCloseButton: true,
                        showCancelButton: false,
                        focusConfirm: false,
                    })
                }
            });
        }
    } else {
        inputFile.value = ""
    }
}
//******************************************************* END EXECL VALIDATE *************************************************/



//******************************************************* START FILES *************************************************/
const typesFile = ["image/png", "image/jpg", "image/jpeg", "application/pdf", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"];
let sizePer = 5242880; //5mb
let sizeFile = parseInt(sizePer / 1048576); //bytes


let filesECamp = []
function addFileEmailCamp(e) {
    let fileInput = document.getElementById("ecamp_execel_files");
    let v = e.files[0]
    console.log(v)

    if (v != undefined) {
        if (
            v.size < sizePer &&
            typesFile.includes(v.type)
        ) {
            filesECamp.push(v);
            renderFilesEcamp()
        } else {
            fileInput.value = ""

            Swal.fire({
                title: '<strong>Validaciones</strong>',
                icon: 'error',
                html: `Tamaño de archivo max ${sizeFile} mb, ${typesFile.toString()}`,
                showCloseButton: true,
                showCancelButton: false,
                focusConfirm: false,
            });
        }
    } else { fileInput.value = "" }

}

function removeFileEmailCamp(index) {
    filesECamp = filesECamp.filter((item, ind) => ind != index);
    renderFilesEcamp()
}

function renderFilesEcamp() {
    let html = '';
    console.log(filesECamp)
    filesECamp.forEach(function (element, index) {
        let kb = Number.parseFloat(element?.size * 0.000977).toFixed(2);
        html += `<div class="col-12 col-md-6 align-self-center">
                            <div class="form-group border-1 px-2 py-1 d-flex justify-content-around">
                                <div><i class="fa fa-paperclip text-secondary" aria-hidden="true"></i> ${element.name} (${kb}) </div><div onclick="removeFileEmailCamp(${index})" class="col text-end px-0"><i class="fa fa-times-circle fa-1x cursor-pointer" aria-hidden="true"></i></div>
                            </div>
                 </div>`;
    });
    document.getElementById("DRenderFilesECamp").innerHTML = html;
}

//******************************************************* END FILES *************************************************/


//******************************************************* START MODE SEND *************************************************/
function ModeSendOption(e, value) {
    document.querySelectorAll(".item-option-ecamp").forEach((item, index) => {
        item.classList.remove("item-option-prog-active");
    });
    e.classList.add("item-option-prog-active")
    switch (value) {
        case 0:
            document.getElementById("dcontent_programation").style.display = 'none';
            document.getElementById("toption_ecamp").value = 0;
            document.getElementById("BtnSaveECamp").innerText = "Enviar ahora";
            break;
        case 1:
            document.getElementById("dcontent_programation").style.display = 'none';
            document.getElementById("toption_ecamp").value = 1;
            document.getElementById("BtnSaveECamp").innerText = "Guardar";
            break;
        case 2:
            document.getElementById("dcontent_programation").style.display = '';
            document.getElementById("toption_ecamp").value = 2;
            document.getElementById("BtnSaveECamp").innerText = "Guardar programación";
            break;
        default:
    }
}
//******************************************************* END MODE SEND *************************************************/



//******************************************************* START CRUD *************************************************/

//list

function listECamp(PageNumber = 1) {
    $("#kgrid_email_campanas_body").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            transport: {
                read: function (options) {
                    $.ajax({
                        data: JSON.stringify({ PageNumber }),
                        type: "POST",
                        url: "/emailcampaing/ListCampaign",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            let data = msg?.List;
                            options.success(data);
                          
                            var CantidadPaginas = parseInt(msg?.Paging.TotalRecord / msg?.Paging.PageRecords);
                            if (PageNumber == 1 && CantidadPaginas > 0) {
                                var htmlOpcion = "";
                                for (var i = 1; i <= CantidadPaginas; i++) {
                                    htmlOpcion += `<option value=${i}>${i} </option>`;
                                }
                                document.getElementById("kgrid_email_campanas_page").innerHTML = htmlOpcion;
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
        columns: [{
            field: "NombreCorreoCampania",
            title: "<span class='kendo_camp_header_text' >Nombre de campaña</span>",
            width: 10,
            attributes: {
                style: "font-size:11px;",

            }
        },
        {

            title: "<span class='kendo_camp_header_text'>Estado</span>",
            template: function (item) {
                let html = "";
                if (item?.SendCorreo == true) {
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
                html += ` <button type="button"  class=" " data-toggle="modal"  data-target="#ModalECamp" onclick="ShowECamp('${dataItem?.CodigoCorreoCampania}')" >
                          
                                <i class="fa fa-eye icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                        </button>`;


                //html += `<button type="button" class=" ml-1" data-toggle="modal" data-target="#modalCampanaWh"  onclick="duplicateCampaign('${dataItem?.CodigoCorreoCampania}','${dataItem?.NombreCorreoCampania}')" >
                //                       <i class="fa fa-files-o icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                //       </button>`;
                if (dataItem.SendCorreo == true) {
                    html += `<button type="button" class=" ml-1" data-toggle="modal" data-target="#ModalECampEmails" onclick="DetailsECampEmails('${dataItem?.CodigoCorreoCampania}')" >
                            <i class="fa fa-info icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                        </button>`;
                }
                if (dataItem.SendCorreo == false) {
                    html += `<button type="button" class="ml-1" onclick="DestroyECamp('${dataItem?.CodigoCorreoCampania}')" >
                            <i class="fa fa-trash icon-kendo_ icon-btn-accion" aria-hidden="true"></i>
                          </button>`;
                    html += `  <button type="button" class=""  onclick="SendECamp('${dataItem?.CodigoCorreoCampania}')" >
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

listECamp();


function paginateChange() {
    let page = document.getElementById("kgrid_email_campanas_page").value;
    listECamp(parseInt(page));
}


//--register
async function registerECamp(e) {

    let name = document.getElementById("ecamp_name").value;
    let excel = document.getElementById("ecamp_execel");

    var body = $("#ecamp_editor").data("kendoEditor").value();
    let mode = document.getElementById("toption_ecamp").value;



    const formData = new FormData();
    formData.append("name", name);
    formData.append("content", body);
    formData.append("mode", mode);
    formData.append("excel", excel.files?.[0]);

    filesECamp.forEach(function (element, index) {
        formData.append(`files[${index}]`, element);
    });
    e.disabled = true;
    const resp = await axios.post("/emailcampaing/RegisterEmailCampaing", formData, {
        headers: {
            "Content-Type": "multipart/form-data",
        },
    });
    if (resp?.data?.Success) {
        listECamp();
        cleanEcampaign();
        Swal.fire({
            title: '<strong>Mensaje</strong>',
            icon: 'success',
            html: resp?.data.Message1,
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
        });
    } else {
        let html = "<ul>";
        resp?.data?.Date.forEach((item, index) => {
            html += `<li>${item?.name}</li>`
        });
        html += '<ul>'
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


//clean data register
function cleanEcampaign() {
    document.getElementById("ecamp_name").value = "";
    document.getElementById("ecamp_execel").value = "";
    document.getElementById("ecamp_execel").value = "";
    document.getElementById("ecamp_execel_files").value = "";
    $("#ecamp_editor").data("kendoEditor").value("");
    filesECamp = [];
    renderFilesEcamp();
}

//delete 
function DestroyECamp(code) {
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
                    url: "/emailcampaing/DestroyECamp",
                    data: { code },
                    headers: { "Content-Type": "application/json" },
                });
                if (resp?.data?.Status == 0) {
                    listECamp();
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


//show
async function ShowECamp(code) {
    document.getElementById("footerECamp").style.display = 'none'
    HideBackdrop();
    const resp = await axios({
        method: "post",
        url: "/emailcampaing/ShowECamp",
        data: { code },
        headers: { "Content-Type": "application/json" },
    });
    if (resp.data) {
        let item = resp?.data;
        document.getElementById("ecamp_name").value = item?.NombreCorreoCampania;
        //document.getElementById("ecamp_execel").value = item?.UrlDestinatarios;

        $("#ecamp_editor").data("kendoEditor").value(item?.Content_html);
        if (item?.SendCorreo) {
            let now = document.getElementById("opsennowEcamp");
            ModeSendOption(now, 0)
        } else {
            let sav = document.getElementById("opsaveEcamp");
            ModeSendOption(sav, 1)
        }

    }
    const respFiles = await axios({
        method: "post",
        url: "/emailcampaing/AllFiles",
        data: { code },
        headers: { "Content-Type": "application/json" },
    });
    renderFilesEcampShow(respFiles.data)

}

function renderFilesEcampShow(data) {
    let html = '';
    data.forEach(function (element, index) {
        let name = element?.UrlArchivosAdjunto.split("_")
        html += `<div class="col-12 col-md-6 align-self-center">
                            <div class="form-group border-1 px-2 py-1 d-flex justify-content-around">
                                
                               <a href="${element?.UrlArchivosAdjunto}" class="text-secondary" target="_blank">${name[1]}</a>

                            </div>
                 </div>`;
    });
    document.getElementById("DRenderFilesECamp").innerHTML = html;
}

//******************************************************* END CRUD *************************************************/

function showModalEcamp() {
    HideBackdrop();
    cleanEcampaign();
    document.getElementById("footerECamp").style.display = ''
}

async function DetailsECampEmails(code) {
    HideBackdrop();
    document.getElementById("codeECamp_").value = code;
    listECampDetailEmails(code, 1)
}

//send
async function SendECamp(code) {

    document.getElementById("loadMe").style.display = 'block';
   

    const resp = await axios({
        method: "post",
        url: "/emailcampaing/SendECamp",
        data: { code },
        headers: { "Content-Type": "application/json" },
    });

    if (resp?.data?.Success) {
        listECamp();
        Swal.fire({
            title: '<strong>Mensaje</strong>',
            icon: 'success',
            html: resp?.data.Message1,
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
        });

    } else {
        Swal.fire({
            title: '<strong>Validaciones</strong>',
            icon: 'error',
            html: resp?.data.Message1,
            showCloseButton: true,
            showCancelButton: false,
            focusConfirm: false,
        });
    }
    document.getElementById("loadMe").style.display = 'none';
}

//********************************************************* DETAIL EMAILS ***********************************************/

function listECampDetailEmails(code, PageNumber = 1) {
    $("#dkendoEmailsDetailECamp").kendoGrid({
        dataSource: {
            type: "json",
            serverPaging: true,
            transport: {
                read: function (options) {
                    $.ajax({
                        data: JSON.stringify({ code, PageNumber }),
                        type: "POST",
                        url: "/emailcampaing/ListCampaignDetails",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            let data = msg?.List;
                            options.success(data);
                            console.log(msg?.Paging)

                            if (data.length > 0) {
                                var CantidadPaginas = parseInt(msg?.Paging.TotalRecord / msg?.Paging.PageRecords);
                                if (PageNumber == 1 && CantidadPaginas > 0) {
                                    var htmlOpcion = "";
                                    for (var i = 1; i <= CantidadPaginas; i++) {
                                        htmlOpcion += `<option value=${i}>${i} </option>`;
                                    }
                                    document.getElementById("kgrid_email_campanas_page_detail").innerHTML = htmlOpcion;

                                }
                                document.getElementById("totalEcampDetail").innerText = msg?.Paging.TotalRecord;
                            }
                            document.getElementById("totalEcampDetail").innerText = msg?.Paging.TotalRecord;
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
            field: "Destinatario",
            title: "<span class='kendo_camp_header_text' >Destinatario</span>",
            width: 10,
            attributes: {
                style: "font-size:11px;",

            }
        },
        {
            title: "<span class='kendo_camp_header_text'>Estado</span>",
            template: function (item) {
                let html = "";
                if (item?.EstadoCorreoCampania == true) {
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
        },
        {
            title: "<span class='kendo_camp_header_text'>Fecha Creación</span>",
            field: "DescFechaCreacion",
            width: 10,
            attributes: {
                style: "font-size:11px;"
            }
        },
        ]
    });
}

function paginateChangeDetail() {
    let page = document.getElementById("kgrid_email_campanas_page_detail").value;
    let code = document.getElementById("codeECamp_").value;
    listECampDetailEmails(code, parseInt(page));
}

//********************************************************* DETAIL EMAILS ***********************************************/




//******************************************************* RENDER EDITOR*************************************************/
var editor = $("#ecamp_editor").kendoEditor({
    stylesheets: [
        "../content/shared/styles/editor.css",
    ], imageBrowser: {
        messages: {
            dropFilesHere: "Drop files here"
        },
        transport: {
            read: "~/Content/appsfit_img/",
            destroy: {
                url: "/kendo-ui/service/ImageBrowser/Destroy",
                type: "POST"
            },
            create: {
                url: "/kendo-ui/service/ImageBrowser/Create",
                type: "POST"
            },
            thumbnailUrl: "/kendo-ui/service/ImageBrowser/Thumbnail",
            uploadUrl: "/kendo-ui/service/ImageBrowser/Upload",
            imageUrl: "/kendo-ui/service/ImageBrowser/Image?path={0}"
        }
    },
    tools: [
        "bold",
        "italic",
        "underline",
        "undo",
        "redo",
        "strikethrough",
        "justifyLeft",
        "justifyCenter",
        "justifyRight",
        "justifyFull",
        "insertUnorderedList",
        "insertOrderedList",
        "insertUpperRomanList",
        "insertLowerRomanList",
        "indent",
        "outdent",
        "createLink",
        "unlink",
        "insertImage",
        "insertFile",
        "subscript",
        "superscript",
        "tableWizard",
        "createTable",
        "addRowAbove",
        "addRowBelow",
        "addColumnLeft",
        "addColumnRight",
        "deleteRow",
        "deleteColumn",
        "mergeCellsHorizontally",
        "mergeCellsVertically",
        "splitCellHorizontally",
        "splitCellVertically",
        "tableAlignLeft",
        "tableAlignCenter",
        "tableAlignRight",
        "viewHtml",
        "cleanFormatting",
        "copyFormat",
        "applyFormat",
        "fontName",
        "fontSize",
        "foreColor",
        "backColor",
        "print",
        "formatting"
    ]
});
//******************************************************* RENDER EDITOR*************************************************//