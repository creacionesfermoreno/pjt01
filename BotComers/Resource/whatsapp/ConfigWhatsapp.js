
const durationZindexModalC = 50

//get list config whatsapp accounts
function getListConfigWhatsapp() {
    $.ajax({
        data: JSON.stringify({}),
        type: "POST",
        url: "/whatsapp/ConfigAll",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            document.getElementById("content_menus_by_acountphone").style.display = "none";

            var html = "";
            let d_account_content_ = document.getElementById("d_account_content_");


            msg?.forEach((item, index) => {
                //html += `<label class="labl">
                //                                <input type="radio" onclick="selectedDevicesRadio('${item?.CodigoWhatsappConfiguracion}')" name="radiodeviceslist" value="${item?.CodigoWhatsappConfiguracion}"  />
                //                                <div class="btnNumbers">
                //                                    <div class="mr-1">
                //                                        <i class="fa fa-whatsapp fa-2x" aria-hidden="true"></i>
                //                                    </div>
                //                                    <div class="btnNumbers_item">
                //                                        <div>
                //                                            Número ${index + 1} 

                //                                        </div>
                //                                        <div>
                //                                            ${item?.NumberPhone}
                //                                        </div>
                //                                    </div>
                //                                        <div class="btnNumbers_item ml-2">
                //                                        <div>
                //                                           <i class="fa fa-trash" aria-hidden="true" onclick="destroyWhatsappConfigByCode('${item?.CodigoWhatsappConfiguracion}')"></i>

                //                                        </div>
                //                                        <div>
                //                                          <i class="fa fa-pencil-square-o" data-toggle="modal" data-target="#modalAddWConfig" onclick="getListWhatsappConfigByCode('${item?.CodigoWhatsappConfiguracion}')"></i>
                //                                        </div>
                //                                    </div>
                //                                </div>
                //                            </label>`;




                html += `<div class="content-phones_ ml-2" id="dkeys${index}">
                             <label class="labl_">
                                <input type="radio" onclick="selectedDevicesRadio('${index}','${item?.CodigoWhatsappConfiguracion}')" name="radiodeviceslist" value="${item?.CodigoWhatsappConfiguracion}">
                                <div class="btnNumbers_">
                                    <div class="mr-1">
                                        <i class="fa fa-whatsapp fa-2x" aria-hidden="true"></i>
                                    </div>
                                    <div class="btnNumbers_item_ ml-1">
                                          <div>
                                            Número ${index + 1} 
                                          </div>
                                         <div>
                                          ${item?.NumberPhone}
                                         </div>
                                    </div>
                                </div>
                            </label>
                        <div class="btnNumbers_item_ ml-2">
                                <div>
                                  <i class="fa fa-trash" aria-hidden="true" onclick="destroyWhatsappConfigByCode('${item?.CodigoWhatsappConfiguracion}')"></i>
                                </div>
                                 <div>
                                      <i class="fa fa-pencil-square-o" data-toggle="modal" data-target="#modalAddWConfig" onclick="getListWhatsappConfigByCode('${item?.CodigoWhatsappConfiguracion}')" aria-hidden="true"></i>
                                 </div>
                            </div>
                        </div>`;
            });

            html += ` <label class="labl" >
                                                <input type="radio"  name="radiodeviceslist" data-toggle="modal" data-target="#modalAddWConfig" onclick="showModalConfigWs()"  />
                                                <div class="btnNumbers">
                                                    <div class="mr-1">
                                                       <i class="fa fa-plus-circle" aria-hidden="true"></i>
                                                    </div>
                                                    <div class="btnNumbers_item">
                                                        Nuevo
                                                    </div>
                                                </div>
                                            </label >`;
            d_account_content_.innerHTML = html;
        },
        complete: function (data) {
        }
    });
}


//config:register
function registerWhatsappConfig(e) {
    e.disabled = true;
    var token = document.getElementById("whconfig_token").value;
    var sdk = "--";
    var idapp = document.getElementById("whconfig_idapp").value;
    var idaccount = document.getElementById("whconfig_idAccount").value;
    var idphone = document.getElementById("whconfig_idphone").value;
    var numberphone = document.getElementById("whconfig_number").value;
    var data = {
        idapp, idphone, idaccount, numberphone,
        token, sdk
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: "/whatsapp/registerConfig",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg?.Status == 0) {
                $.bootstrapGrowl(msg?.Message1, { type: 'success', width: 'auto' });
                getListConfigWhatsapp()
                document.getElementById("whconfig_idapp").value = "";
                document.getElementById("whconfig_token").value = "";

                document.getElementById("whconfig_idAccount").value = "";
                document.getElementById("whconfig_idphone").value = "";
                document.getElementById("whconfig_number").value = "";
                selectedSet()
            } else {

                $.bootstrapGrowl(msg?.Message1, { type: 'danger', width: 'auto' });
            }
        },
        complete: function (data) {
        }
    });
    e.disabled = false;
}

function updateWhatsappConfig(e) {
    e.disabled = true;
    document.getElementById("loadingupd_configw").style.display = "";

    var code = document.getElementById("_idwhconfig").value;
    var token = document.getElementById("whconfig_token").value;
    var sdk = "--"
    var idaccount = document.getElementById("whconfig_idAccount").value;
    var idphone = document.getElementById("whconfig_idphone").value;
    var numberphone = document.getElementById("whconfig_number").value;
    var data = {
        code, idphone, idaccount, numberphone,
        token, sdk
    };
    $.ajax({
        data: JSON.stringify(data),
        type: "POST",
        url: "/whatsapp/updateConfig",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg?.Status == 0) {
                document.getElementById("closeModalWhatsapconfig").click();
                $.bootstrapGrowl(msg?.Message1, { type: 'success', width: 'auto' });
                getListConfigWhatsapp();
            } else {
                $.bootstrapGrowl(msg?.Message1, { type: 'danger', width: 'auto' });
            }
        },
        complete: function (data) {
            e.disabled = false;
            document.getElementById("loadingupd_configw").style.display = "none";
        }
    });
}

//delete config ws
function destroyWhatsappConfigByCode(code) {
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
                data: JSON.stringify({ code: code }),
                type: "POST",
                url: "/whatsapp/DestroyConfigByCode",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {

                    if (msg?.Status == 0) {
                        $.bootstrapGrowl(msg?.Message1, { type: 'success', width: 'auto' });
                        getListConfigWhatsapp()
                        selectedSet();
                    } else {
                        $.bootstrapGrowl(msg?.Message1, { type: 'danger', width: 'auto' });
                    }
                },
                complete: function (data) {
                    e.disabled = false;
                }
            });
        }
    })


}

function cancelWhatsappConfig() {
    document.getElementById("updateWhatsappConfigBtn").style.display = "none";
    document.getElementById("registerWhatsappConfigBtn").style.display = "block";

    document.getElementById("whconfig_token").value = "";

    document.getElementById("whconfig_idAccount").value = "";
    document.getElementById("whconfig_idphone").value = "";
    document.getElementById("whconfig_number").value = "";
    document.getElementById("_idwhconfig").value = "";
}


//get item edit
function getListWhatsappConfigByCode(code) {
    clearModalZindexConfig()
    $.ajax({
        data: JSON.stringify({ code: code }),
        type: "POST",
        url: "/whatsapp/ConfigByCode",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg?.Status == 0) {
                document.getElementById("whconfig_token").value = msg?.Date?.Token;

                document.getElementById("whconfig_idapp").value = msg?.Date?.IdentificadorApp;
                document.getElementById("whconfig_idAccount").value = msg?.Date?.IdAccount;
                document.getElementById("whconfig_idphone").value = msg?.Date?.IdPhone;
                document.getElementById("whconfig_number").value = msg?.Date?.NumberPhone;
                document.getElementById("_idwhconfig").value = code;

                document.getElementById("updateWhatsappConfigBtn").style.display = "flex"
                document.getElementById("registerWhatsappConfigBtn").style.display = "none"

            }
        },
        complete: function (data) { }
    });
}


//select radio devices
function selectedDevicesRadio(idclass, code) {

    console.log(idclass)
    if (code !== "") {
        let classid = `dkeys${idclass}`;
        console.log(classid)

        document.querySelectorAll(".content-phones_").forEach(item => {
            item.classList.remove("content-phones-active")
        });
        document.getElementById(classid).classList.add("content-phones-active")

        document.getElementById("contentOptionsWDevices").style.display = "block";
        var licampana = document.getElementById("li_campanawts");
        evemt_changeWhat(licampana, 1);
        document.getElementById("CodigoWhatsappConfiguracion").value = code;
        document.getElementById("content_menus_by_acountphone").style.display = "";
        getTemplates();
        getCampaigns();
    } else {
        //contents menus
        document.getElementById("content_menus_by_acountphone").style.display = "none";
        document.getElementById("contentCampanaW").style.display = "none";
        document.getElementById("contentTemplatesW").style.display = "none";
        document.getElementById("contentTestIndividualW").style.display = "none";

        //filters
        document.getElementById("filter_date_start_camp").value = "";
        document.getElementById("filter_date_end_camp").value = "";

        document.getElementById("contentOptionsWDevices").style.display = "none";
        document.getElementById("CodigoWhatsappConfiguracion").value = "";
    }
}

function showModalConfigWs() {
    document.getElementById("TituloTextCampaign").innerText = "Nueva campaña";
    clearModalZindexConfig();
    document.getElementById("contentOptionsWDevices").style.display = "none";
    document.getElementById("contentCampanaW").style.display = "none";
    document.getElementById("contentTemplatesW").style.display = "none";
    document.getElementById("contentTestIndividualW").style.display = "none";

    document.querySelectorAll(".content-phones_").forEach(item => {
        item.classList.remove("content-phones-active")
    });

}

//globals**
function clearModalZindexConfig() {
    let timer;
    window.clearTimeout(timer);
    timer = window.setTimeout(() => {
        if (document.querySelector(".modal-backdrop")) {
            document.querySelector(".modal-backdrop").classList.remove("modal-backdrop");
        }
    }, durationZindexModalC);
}
