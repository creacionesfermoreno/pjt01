

function getTemplates() {
    var code = document.getElementById("CodigoWhatsappConfiguracion").value;

    document.getElementById("loadMe").style.display = "block"
    $.ajax({
        data: JSON.stringify({ code: code }),
        type: "POST",
        url: "/whatsapp/getTemplates",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            console.log(resp)
            if (resp?.Status == 0) {
                html = "";
                resp?.Date?.data.forEach((item, index) => {
                    html += `<div class="col-md-3">

                       <div class="card">
                            <div class="card-header card-header_">
                              <div class="row justify-content-between">
                                <div class="col"><strong>${item?.name}</strong></div>
                               </div>
                             </div>
                        <div class="card-body">

                        <div class="bg-prev-whatsapp p-3 content-preview-list-template" >
                            <div class="talk-bubble_c tri-right left-top">
                                <div class="talktext ">`;

                    item?.components.forEach((com, indx) => {
                        if (com?.type == "HEADER") {
                            html += `<div id="dheaderPreview_list">`;
                            if (com?.format == "TEXT") {
                                html += `<strong id="previewText_list" style="font-size: 16px;font-weight:600;">${com?.text}</strong>`;
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
                            html += `<div id="previewBody_list" style=" font-size: 15px;margin-top:2px; color: #362d2d">${com?.text}</div>`;
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

                    html += `<div class="card-footer">`;
                    html += `<div class="row align-items-center justify-content-between">`;

                    html += `<div class="col-5">`;

                    if (item?.status == "REJECTED") {
                        html += ` <span class="badge    btn-status-template btn-status-template-danger"><i class="fa fa-times-circle mr-1" aria-hidden="true"></i> Rechazado</span>`;
                    } else if (item?.status == "APPROVED") {
                        html += ` <span class="badge btn-status-template btn-status-template-success"> <i class="fa fa-check-circle-o mr-1" aria-hidden="true"></i> Aprovado</span>`;
                    } else {
                        html += ` <span class="badge  btn-status-template btn-status-template-warning"> <i class="fa fa-question-circle mr-1" aria-hidden="true"></i> En revisión</span>`;
                    }
                    html += ` </div>`;
                    html += `<div class="col-5 text-end"><button type="button" class="btn-custom btn-custom-primary" onclick="destroyTemplate('${item?.name}')">
                                        <i class="fa fa-trash icon-kendo_" aria-hidden="true"></i> Eliminar
                                    </button></div>`;

                    html+=` </div>`;
                    html += ` </div>`;

                    html += `</div>`;
                    html += `</div>`;



                });
                //document.getElementById("whatsapp_templates_body").innerHTML = html;
                document.getElementById("content_list_template_prev").innerHTML = html;
                selectListTemplate(resp?.Date?.data)
            } else {
                $.bootstrapGrowl(resp?.Message1, { type: 'danger', width: 'auto' });
            }
        },
        complete: function (data) {
            document.getElementById("loadMe").style.display = "none"
        }
    });

}




//----------------------------------------swiths----------------------------------------------

const selectTypeHeader = document.getElementById('template_typeHeader');
selectTypeHeader.addEventListener('change', function handleChange(event) {
    showTypeHeader(event.target.value);
});

function showTypeHeader(type) {
    document.getElementById("previewText").style.display = 'block';
    switch (type) {
        case "0":
            document.getElementById("dheaderTextTemplate").style.display = "none";
            document.getElementById("dheaderMediosTemplate").style.display = "none";
            selectForPreviewTypeHeader(0)
            document.getElementById("previewText").style.display = 'none'

            break;
        case "1":
            document.getElementById("dheaderTextTemplate").style.display = "block";
            document.getElementById("dheaderMediosTemplate").style.display = "none";
            selectForPreviewTypeHeader(0)
            break;
        case "2":

            document.getElementById("medio_header_template_imagen").checked = true;

            selectForPreviewTypeHeader(1)
            document.getElementById("dheaderTextTemplate").style.display = "none";
            document.getElementById("dheaderMediosTemplate").style.display = "block";
            changeSelectMediosForPreview()
            document.getElementById("previewText").style.display = 'none'
            break;
        default:
            break;
    }

}


const selectTypeButtons = document.getElementById('template_typeBottom');
selectTypeButtons.addEventListener('change', function handleChange(event) {
    showTypeButtons(event.target.value);
});

function showTypeButtons(type) {
    let btnMore = document.getElementById("moreBtnAnswer");
    switch (type) {
        case "0":
            document.getElementById("dcallaccion").style.display = "none";
            document.getElementById("dfastAnswer").style.display = "none";
            document.getElementById("contentButtons").display = "none";
            removeRowButtonQreplies(2)
            removeRowButtonQreplies(3)
            btnMore.disabled = false;
            document.getElementById("previewAcction").style.display = "none";
            document.getElementById("dqrepliespreview").style.display = "none";
            break;
        case "1":
            document.getElementById("dcallaccion").style.display = "block";
            document.getElementById("dfastAnswer").style.display = "none";
            document.getElementById("contentButtons").display = "none";
            removeRowButtonQreplies(2)
            removeRowButtonQreplies(3)
            btnMore.disabled = false;
            document.getElementById("previewAcction").style.display = "block";
            document.getElementById("dqrepliespreview").style.display = "none";
            break;
        case "2":
            document.getElementById("dcallaccion").style.display = "none";
            document.getElementById("dfastAnswer").style.display = "block";
            btnMore.disabled = false;
            document.getElementById("previewAcction").style.display = "none";
            document.getElementById("dqrepliespreview").style.display = "block";
            break;
        default:
            break;
    }

}



function showCaseAddButton() {
    let two = document.getElementById("dtxt_fastAnswerTwo").style.display;
    let tree = document.getElementById("dtxt_fastAnswerTree").style.display;
    if (two == 'none') {
        document.getElementById("dtxt_fastAnswerTwo").style.display = '';
        document.getElementById("previewFirtButtonQreplies_two").style.display = '';
    }
    if (tree == 'none' && two != "none") {
        document.getElementById("dtxt_fastAnswerTree").style.display = '';
        document.getElementById("previewFirtButtonQreplies_tree").style.display = '';
    }

}
function removeRowButtonQreplies(num) {
    switch (num) {
        case 2:
            document.getElementById("dtxt_fastAnswerTwo").style.display = 'none';
            document.getElementById("txt_fastAnswerTwo").value = '';
            document.getElementById("previewFirtButtonQreplies_two").innerHTML = '';
            document.getElementById("previewFirtButtonQreplies_two").style.display = 'none';
            break;
        case 3:
            document.getElementById("dtxt_fastAnswerTree").style.display = 'none';
            document.getElementById("txt_fastAnswerTree").value = '';
            document.getElementById("previewFirtButtonQreplies_tree").innerHTML = '';
            document.getElementById("previewFirtButtonQreplies_tree").style.display = 'none';
            break;

        default:
            break;
    }
}

//-----------------------------------------swiths----------------------------------------------



//--------------------------------------- preview ---------------------------------
function selectForPreviewTypeHeader(value) {
    switch (value) {
        case 0:
            document.getElementById("previewDocument").style.display = "none";
            document.getElementById("previewImagen").style.display = "none";
            document.getElementById("previewVideo").style.display = "none";
            break
        case 1:
            //image
            document.getElementById("previewDocument").style.display = "none";
            document.getElementById("previewImagen").style.display = "block";
            document.getElementById("previewVideo").style.display = "none";
            break
        case 2:
            //video
            document.getElementById("previewDocument").style.display = "none";
            document.getElementById("previewImagen").style.display = "none";
            document.getElementById("previewVideo").style.display = "block";
            break
        case 3:
            //document
            document.getElementById("previewDocument").style.display = "block";
            document.getElementById("previewImagen").style.display = "none";
            document.getElementById("previewVideo").style.display = "none";
            break
        default:
            break;
    }
}


function changeSelectMediosForPreview() {
    if (document.querySelector('input[name="medio_header_template"]:checked')) {
        let value = document.querySelector('input[name="medio_header_template"]:checked').value;
        if (value == "video") {
            selectForPreviewTypeHeader(2)
        } else if (value == "image") {
            selectForPreviewTypeHeader(1)
        } else {
            selectForPreviewTypeHeader(3)
        }

    }
}

//*****keypress******

//text
const txtHeader = document.getElementById('txt_header_template');
txtHeader.addEventListener('keyup', function handleChange(event) {
    document.getElementById("previewText").innerHTML = event.target.value;
});

txtHeader.addEventListener('keydown', function handleChange(event) {
    document.getElementById("previewText").innerHTML = event.target.value;
});

//body
const txtBody = document.getElementById('text_body_template');
txtBody.addEventListener('keyup', function handleChange(event) {
    console.log(event.target.value)
    document.getElementById("previewBody").innerHTML = event.target.value;
});

txtBody.addEventListener('keydown', function handleChange(event) {

    document.getElementById("previewBody").innerHTML = event.target.value;
});

//footer
const txtFooter = document.getElementById('text_footer_template');
txtFooter.addEventListener('keyup', function handleChange(event) {
    document.getElementById("previewFooter").innerHTML = event.target.value;
});

txtFooter.addEventListener('keydown', function handleChange(event) {
    document.getElementById("previewFooter").innerHTML = event.target.value;
});

//text call button
const txt_button_accionbprev = document.getElementById('txt_button_accion');
txt_button_accionbprev.addEventListener('keyup', function handleChange(event) {
    document.getElementById("callTextPreview").innerHTML = event.target.value;
    if (event.target.value.toString().length == 0) {
        document.getElementById("dcallTextPreview").style.display = 'none';
    } else {
        document.getElementById("dcallTextPreview").style.display = 'block';
    }
});

txt_button_accionbprev.addEventListener('keydown', function handleChange(event) {
    document.getElementById("callTextPreview").innerHTML = event.target.value;
    if (event.target.value.toString().length == 0) {
        document.getElementById("dcallTextPreview").style.display = 'none';
    } else {
        document.getElementById("dcallTextPreview").style.display = 'block';
    }
});

//text link button 
const txt_button_accion_webprev = document.getElementById('txt_button_accion_web');
txt_button_accion_webprev.addEventListener('keyup', function handleChange(event) {
    document.getElementById("linkTextPreview").innerHTML = event.target.value;
    if (event.target.value.toString().length == 0) {
        document.getElementById("dlinkTextPreview").style.display = 'none';
    } else {
        document.getElementById("dlinkTextPreview").style.display = 'block';
    }
});

txt_button_accion_webprev.addEventListener('keydown', function handleChange(event) {
    document.getElementById("linkTextPreview").innerHTML = event.target.value;
    if (event.target.value.toString().length == 0) {
        document.getElementById("dlinkTextPreview").style.display = 'none';
    } else {
        document.getElementById("dlinkTextPreview").style.display = 'block';
    }
});


//qreplies one
const txt_fastAnswerOneprev = document.getElementById('txt_fastAnswerOne');
txt_fastAnswerOneprev.addEventListener('keyup', function handleChange(event) {
    document.getElementById("dqrepliespreview").style.display = ''
    document.getElementById("previewFirtButtonQreplies_one").style.display = ''
    document.getElementById("previewFirtButtonQreplies_one").innerHTML = event.target.value;

});

txt_fastAnswerOneprev.addEventListener('keydown', function handleChange(event) {
    document.getElementById("dqrepliespreview").style.display = ''
    document.getElementById("previewFirtButtonQreplies_one").style.display = ''
    document.getElementById("previewFirtButtonQreplies_one").innerHTML = event.target.value;
});
//qreplies two
const txt_fastAnswerTwoprev = document.getElementById('txt_fastAnswerTwo');
txt_fastAnswerTwoprev.addEventListener('keyup', function handleChange(event) {
    document.getElementById("dqrepliespreview").style.display = ''
    document.getElementById("previewFirtButtonQreplies_two").style.display = ''
    document.getElementById("previewFirtButtonQreplies_two").innerHTML = event.target.value;

});

txt_fastAnswerTwoprev.addEventListener('keydown', function handleChange(event) {
    document.getElementById("dqrepliespreview").style.display = ''
    document.getElementById("previewFirtButtonQreplies_two").style.display = ''
    document.getElementById("previewFirtButtonQreplies_two").innerHTML = event.target.value;
});


//qreplies tree
const txt_fastAnswerTreeprev = document.getElementById('txt_fastAnswerTree');
txt_fastAnswerTreeprev.addEventListener('keyup', function handleChange(event) {
    document.getElementById("dqrepliespreview").style.display = ''
    document.getElementById("previewFirtButtonQreplies_tree").style.display = ''
    document.getElementById("previewFirtButtonQreplies_tree").innerHTML = event.target.value;

});

txt_fastAnswerTreeprev.addEventListener('keydown', function handleChange(event) {
    document.getElementById("dqrepliespreview").style.display = ''
    document.getElementById("previewFirtButtonQreplies_tree").style.display = ''
    document.getElementById("previewFirtButtonQreplies_tree").innerHTML = event.target.value;
});

//*****keypress******
//--------------------------------------- preview ---------------------------------
function abcd() {

    setTimeout(() => {
        if (document.querySelector(".modal-backdrop")) {
            document.querySelector(".modal-backdrop").classList.remove("modal-backdrop");
        }
    }, 5);
}

//generate buttons

function removeItembutton(code) {
    document.getElementById(code).remove();
    let btn = document.getElementById("moreBtnAnswer");
    if (document.getElementsByName("txt_fastAnswer").length > 2) {
        btn.disabled = true;

    } else {
        btn.disabled = false;
    }
}

function generateButtons(e) {
    //if (document.getElementsByName("txt_fastAnswer").length > 2) {
    //    e.disabled = true;
    //    return true;
    //}
    showCaseAddButton();
    return;
    let code = uuidv4();
    let codeI = uuidv4();
    const row = document.createElement("div");
    const col = document.createElement("div");
    const col_ = document.createElement("div");
    const icon = document.createElement("i");
    const form = document.createElement("div");
    const label = document.createElement("label");
    const div = document.createElement("div");
    const span = document.createElement("span");
    const input = document.createElement("input");

    row.setAttribute("class", "row");
    row.setAttribute("id", code)
    col.setAttribute("class", "col-6");
    col_.setAttribute("class", "col-1 align-self-center");
    icon.setAttribute("class", "fa fa-times cursor-pointer");
    icon.setAttribute("aria-hidden", "true");
    icon.setAttribute("onclick", `removeItembutton('${code}')`);

    form.setAttribute("class", "form-group");
    label.textContent = "Texto del botón";
    span.setAttribute("class", "field-icon");
    span.textContent = "25";
    input.setAttribute("class", "form-control");
    input.setAttribute("name", "txt_fastAnswer");
    input.setAttribute("oninput", "validateQrB()");

    div.appendChild(span);
    div.appendChild(input);
    form.appendChild(label)
    form.appendChild(div)
    col.appendChild(form);
    col_.appendChild(icon);
    row.appendChild(col);
    row.appendChild(col_);
    document.getElementById("contentButtons").appendChild(row);
}







function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}












//-------------------------------------- validates ---------------------------------

const template_name = document.getElementById('template_name');
template_name.addEventListener('input', function handleChange(event) {
    maxLengh(event, 60);
});
const txt_header_template = document.getElementById('txt_header_template');
txt_header_template.addEventListener('input', function handleChange(event) {
    maxLengh(event, 60);
});
const text_body_template = document.getElementById('text_body_template');
text_body_template.addEventListener('input', function handleChange(event) {
    maxLengh(event, 1024);
});
const text_footer_template = document.getElementById('text_footer_template');
text_footer_template.addEventListener('input', function handleChange(event) {
    maxLengh(event, 60);
});


const txt_button_accionVLength = document.getElementById('txt_button_accion');
txt_button_accionVLength.addEventListener('input', function handleChange(event) {
    maxLengh(event, 25);
});
const txt_button_accion_webVLength = document.getElementById('txt_button_accion_web');
txt_button_accion_webVLength.addEventListener('input', function handleChange(event) {
    maxLengh(event, 25);

});

const txt_button_accion_call_numberVLength = document.getElementById('txt_button_accion_call_number');
txt_button_accion_call_numberVLength.addEventListener('input', function handleChange(event) {
    maxLengh(event, 20);
});
const txt_button_accion_call_urlVLength = document.getElementById('txt_button_accion_call_url');
txt_button_accion_call_urlVLength.addEventListener('input', function handleChange(event) {
    maxLengh(event, 2000);
});




function maxLengh(value, number) {
    var value = event.target.value;
    if (value.length > number)
        return event.target.value = value.slice(0, number);
}

let vtemplate_namek = document.getElementById("template_name");
vtemplate_namek.addEventListener('keyup', function (e) {
    vtemplate_namek.value = e.target.value.replace(/[^a-z?_?]+/g, '');
});

function ValidateText() {
    
}


//-------------------------------------- validates ---------------------------------








//-------------------------------------------- REGISTER TEMPLATE -------------------------------------
async function registerTemplate(e) {
    e.disabled = true;
    let name = document.getElementById("template_name").value;
    let language = document.getElementById("template_languaje").value;
    let category = document.getElementById("template_category").value;
    let body = document.getElementById("text_body_template").value;

    let typeHeader = document.getElementById("template_typeHeader").value;
    let textHeaderValue = document.getElementById("txt_header_template").value;

    let haderMedioValue = document.querySelector('input[name="medio_header_template"]:checked') ? document.querySelector('input[name="medio_header_template"]:checked').value : '';
    let bottomSelected = document.getElementById("template_typeBottom").value;

    let header = {
        type: typeHeader,
        value: textHeaderValue,
        medio: haderMedioValue
    };


    let phone_text = document.getElementById("txt_button_accion").value;
    let phonevalue = document.getElementById("txt_button_accion_call_number").value;
    let codeCountry = document.getElementById("txt_button_accion_call_code").value;

    let url_text = document.getElementById("txt_button_accion_web").value;
    let url_link = document.getElementById("txt_button_accion_call_url").value;

    let valueQuick = []
    let valueQr = document.getElementsByName("txt_fastAnswer").forEach((item, index) => {
        if (item.value) {
            valueQuick.push(item.value)
        }
    })

    let bottons = {
        type: bottomSelected,
        data: {
            phone_text: phone_text,
            phonevalue: codeCountry + phonevalue,
            url_text: url_text,
            url_link: url_link,
        },
        valueQuick: valueQuick
    };


    let code = document.getElementById("CodigoWhatsappConfiguracion").value;
    let footer = document.getElementById("text_footer_template").value;



    let data = {
        code, name, language, category, header, footer, body, bottons
    };
    console.log(data)
    
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: "/whatsapp/registerTemplate",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (resp) {
            if (resp?.Status == 0) {
                document.getElementById("closeModalTemplate").click();
                $.bootstrapGrowl("Template registrado correctamente!", { type: 'success', width: 'auto' });
                closeModalRegisterTemplate()
                getTemplates()
            } else {
                $.bootstrapGrowl(resp?.Message1, { type: 'danger', width: 'auto' });
                $.bootstrapGrowl(resp?.Message2, { type: 'danger', width: 'auto' });
            }
        },
        complete: function (data) {
            e.disabled = false;
           
        }
    });
}

function destroyTemplate(name) {
    let code = document.getElementById("CodigoWhatsappConfiguracion").value;
    let data = {
        code, name
    };

    Swal.fire({
        title: 'Estas segura(o)?',
        text: "¡No podrás revertir esto!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'Cancelar',
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                data: JSON.stringify(data),
                type: "POST",
                url: "/whatsapp/destroyTemplate",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resp) {
                    if (resp?.Status == 0) {
                        $.bootstrapGrowl("Template eliminado correctamente!", { type: 'success', width: 'auto' });
                        getTemplates()

                    } else {
                        $.bootstrapGrowl(resp?.Message1, { type: 'danger', width: 'auto' });
                        $.bootstrapGrowl(resp?.Message2, { type: 'danger', width: 'auto' });
                    }
                },
                complete: function (data) {
                   
                }
            });
        }
    })

}

function closeModalRegisterTemplate() {
    document.getElementById("template_languaje").value = "";
    document.getElementById("template_name").value = "";
    document.getElementById("template_category").value = "";

    document.getElementById("template_typeHeader").value = "0";
    document.getElementById("txt_header_template").value = "";
    showTypeHeader("0");

    document.getElementById("text_body_template").value = "";
    document.getElementById("previewBody").innerHTML = "";
    document.getElementById("text_footer_template").value = "";
    document.getElementById("previewFooter").innerHTML = "";

    showTypeButtons("0");
    document.getElementById("template_typeBottom").value = "0";
}

//selected emojis*********************
     new EmojiPicker({
        trigger: [
            {
                selector: '.triger_btn',
                insertInto: ['.text_area_custom_body']
            },
        ],
        closeButton: true,
        specialButtons: 'orange',
     });

const elemx = document.querySelector('#text_body_template');
elemx.addEventListener('focus', (event) => {
    let conten = document.getElementById("text_body_template").value;
    document.getElementById("previewBody").innerText = conten;
});



