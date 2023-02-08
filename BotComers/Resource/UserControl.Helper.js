/*mensajes generales*/
var mensajeErrorConexion = "La conexión Wi-fi y los datos móviles No se encuentran disponibles";
var OnFocusInput = function (obj) {
    $(obj).select();
};
function solorLetras(f, e) { // 1
    var tecla = e.getCharCode(); // 2
    if (tecla == 8) return true; // 3
    else if (tecla == 0) return true;
    else if (tecla == 9) return true;
    var patron = /^[a-zA-Z_áéíóúñ.'\s]*$/;
    var paraTest = String.fromCharCode(tecla); // 5
    if (!patron.test(paraTest)) { e.stopEvent(); } // 6
}
function solorNumerosLetras(f, e) { // 1
    var tecla = e.getCharCode(); // 2
    if (tecla == 8) return true; // 3
    else if (tecla == 0) return true;
    else if (tecla == 9) return true;
    var patron = /^[ 0-9-A-z_.'\s]*$/;
    var paraTest = String.fromCharCode(tecla); // 5
    if (!patron.test(paraTest)) { e.stopEvent(); } // 6
}
function soloNumeros(f, e) { // 1
    var tecla = e.charcode; // 2
    if (tecla == 8) return true; // 3
    else if (tecla == 0) return true;
    else if (tecla == 9) return true;
    var patron = /^[ 0-9]*$/;
    var paraTest = String.fromCharCode(tecla); // 5
    if (!patron.test(paraTest)) {
        return false;
    }
    else {
        return true;
    }
}
function solorNumerosMenorTres(f, e) { // 1
    var tecla = e.charcode; // 2
    if (tecla == 8) return true; // 3
    else if (tecla == 1) return true;
    else if (tecla == 3) return true;
    var patron = /^[ 1-3]*$/;
    var paraTest = String.fromCharCode(tecla); // 5
    if (!patron.test(paraTest)) { e.stopEvent(); } // 6
}
function soloNumerosDecimales(f, e) { // 1
    var tecla = e.charcode; // 2
    if (tecla == 8) return true; // 3
    else if (tecla == 0) return true;
    else if (tecla == 9) return true;
    var patron;
    var paraTest;
    if (tecla > 47 && tecla < 58) {
        if (f.value == '') return true;
        patron = /[0-9].[0-9]{1}$/;
        paraTest = String.fromCharCode(tecla); // 5
        if (!patron.test(paraTest)) { return true; } // 6
    }
    if (tecla == 46) {
        if (f.value == '') {
            //e.stopEvent();
            return false;
        };
        patron = /^[0-9]+$/;
        paraTest = String.fromCharCode(tecla); // 5
        if (!patron.test(paraTest) && f.value.indexOf(paraTest) == -1) { return true; } // 6
    }
    //e.stopEvent();
    return false;
}

function MaskMoney(event) {
    var regex = new RegExp("^[.0-9]+\.?[0-9]*$");
    //var regex = new RegExp("^[0-9]+([,.][0-9]+)?$"); 
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function Sololetras(event) {
    var regex = new RegExp("^[-_ a-zñáéíóúüA-ZÑÁÉÍÓÚ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function SoloAlphaNumerico(event) {
    var regex = new RegExp("^[-_ a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

function SoloNumerico(event) {
    var regex = new RegExp("^[0-9\b]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}
function SoloMoneda(event) {
    var regex = new RegExp("^[.0-9]+\.?[0-9]*$");
    //var regex = new RegExp("^[0-9]+([,.][0-9]+)?$"); 
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
}

$('.SoloMoneda').keypress(function (event) {
    var regex = new RegExp("^[.0-9]+\.?[0-9]*$");
    //var regex = new RegExp("^[0-9]+([,.][0-9]+)?$"); 
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

$('.SoloLetras').keypress(function (event) {
    var regex = new RegExp("^[.-_ a-zñáéíóúüA-ZÑÁÉÍÓÚ]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

$('.SoloLetrasGeneral').keypress(function (event) {
    var regex = new RegExp("^[-.,@_ a-zñáéíóúüA-ZÑÁÉÍÓÚ0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

$('.SoloAlphaNumerico').keypress(function (event) {
    var regex = new RegExp("^[-_ a-zA-Z0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

/*solo numeros por clase*/
$(".soloNumeros").keypress(function (event) {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

function IsJsonString(str) {
    try {
        JSON.parse(str);
    } catch (e) {
        return false;
    }
    return true;
}

function ConvertirFechaJson(Fecha) {
    var d = new Date(Fecha);
    if (isNaN(d)) return null;
    return '\/Date(' + d.getTime() + '-0000)\/';
}

/* LLAMADAS PARA SERVICIOS WCF */
var LlamarWCF = function (urlMetodo, objectoAEnviar, metodoCorrecto, metodoError) {
    var URL = URLPrincipalWebService + urlMetodo;
    $.support.cors = true;
    $.ajax({
        crossDomain: true,
        cache: true,
        async: true,
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        url: URL,
        data: JSON.stringify(objectoAEnviar),
        dataType: 'json',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message) {
            metodoError(message);
        },
    });
};
var LlamarSyncWCF = function (urlMetodo, objectoAEnviar, metodoCorrecto, metodoError) {
    var URL = URLPrincipalWebService + urlMetodo;
    $.support.cors = true;
    $.ajax({
        crossDomain: true,
        cache: true,
        async: false,
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        url: URL,
        data: JSON.stringify(objectoAEnviar),
        dataType: 'json',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message) {
            metodoError(message);
        },
    });
};

/* LLAMADAS PARA APIS */
var LlamarAJAX = function (urlAccion, objetoAEnviar, metodoCorrecto, metodoError, Mask, metodoFinalize) {
    if (!window.navigator.onLine) {
        metodoError('La conexión Wi-fi y los datos móviles No se encuentran disponibles');
        return;
    }

    //if ((Mask == null || Mask == false) && ( $('.modal_hide_local').css('display') ==null || $('.modal_hide_local').css('display') == 'none')) {
    //    waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
    //}    

    if (Mask == null || Mask == false) {
        waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
    }

    $.support.cors = true;
    $.ajax({
        async: true,
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        url: urlAccion,
        data: JSON.stringify(objetoAEnviar),
        dataType: 'json',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message, error, cod) {
         
            if (IsJsonString(message.responseText)) {
                var errorDTO = JSON.parse(message.responseText);
                metodoError(errorDTO.Message);
            } else {
                document.write(message.responseText);
                //var mensajeErrorHtml = message.responseText.trim();
                //WindowDialogError.show(mensajeErrorHtml);
            }
        },
        complete: function () {

            if (Mask == null || Mask == false) {
                waitingDialog.hide();
            }

            //if ($('.modal_hide_local').css('display') == null || $('.modal_hide_local').css('display') == 'block') {
            //    waitingDialog.hide();
            //}     

            if (typeof metodoFinalize === 'function') {
                metodoFinalize();
            }
        }
    });
}

var LlamarAJAXSinMask = function (urlAccion, objetoAEnviar, metodoCorrecto, metodoError, metodoComplete) {
    if (!window.navigator.onLine) {
        metodoError('La conexión Wi-fi y los datos móviles No se encuentran disponibles');
        return;
    }
    $.support.cors = true;
    $.ajax({
        async: true,
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        url: urlAccion,
        data: JSON.stringify(objetoAEnviar),
        dataType: 'json',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message, error, cod) {
            if (IsJsonString(message.responseText)) {
                var errorDTO = JSON.parse(message.responseText);
                metodoError(errorDTO.Message);
            } else {
                document.write(message.responseText);
            }
        },
        complete: function () {
            metodoComplete();
        }
    });
}

var LlamarSyncAJAX = function (urlAccion, objetoAEnviar, metodoCorrecto, metodoError) {
    if (!window.navigator.onLine) {
        metodoError('La conexión Wi-fi y los datos móviles No se encuentran disponibles');
        return;
    }
    $.support.cors = true;
    $.ajax({
        crossDomain: true,
        cache: true,
        async: false,
        type: 'POST',
        contentType: 'application/json;charset=utf-8',
        url: urlAccion,
        data: JSON.stringify(objetoAEnviar),
        dataType: 'json',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message) {
            metodoError(message);
        },
    });
}

var LlamarHtmlAJAX = function (urlAccion, objetoAEnviar, metodoCorrecto, metodoError, Mask) {
    if (!window.navigator.onLine) {
        metodoError('La conexión Wi-fi y los datos móviles No se encuentran disponibles');
        return;
    }
    //if ((Mask == null || Mask == false) && ( $('.modal_hide_local').css('display') ==null || $('.modal_hide_local').css('display') == 'none')) {
    //    waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
    //}    

    if (Mask == null || Mask == false) {
        waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
    }

    $.support.cors = true;
    $.ajax({
        async: true,
        type: 'GET',
        contentType: 'application/html;charset=utf-8',
        url: urlAccion,
        data: JSON.stringify(objetoAEnviar),
        //dataType: 'html',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message, error, cod) {
            if (IsJsonString(message.responseText)) {
                var errorDTO = JSON.parse(message.responseText);
                metodoError(errorDTO.Message);
            } else {
                document.write(message.responseText);
            }
        },
        complete: function () {

            if (Mask == null || Mask == false) {
                waitingDialog.hide();
            }

            //if ($('.modal_hide_local').css('display') == null || $('.modal_hide_local').css('display') == 'block') {
            //    waitingDialog.hide();
            //}            
        }
    });
}


var WaitingShow = function () {
    waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
};

var WaitingHide = function () {
    waitingDialog.hide();
};



var GetJsonAjax = function (urlAccion, objetoAEnviar, metodoCorrecto, metodoError) {
    if (!window.navigator.onLine) {
        metodoError('La conexión Wi-fi y los datos móviles No se encuentran disponibles');
        return;
    }

    waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });

    $.support.cors = true;
    $.ajax({
        async: true,
        type: 'GET',
        contentType: 'application/json;charset=utf-8',
        url: urlAccion,
        data: objetoAEnviar,
        dataType: 'json',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message, error, cod) {
            debugger;
            if (IsJsonString(message.responseText)) {
                var errorDTO = JSON.parse(message.responseText);
                metodoError(errorDTO.Message);
            } else {
                document.write(message.responseText);
            }
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

var GetHtmlAjax = function (urlAccion, objetoAEnviar, metodoCorrecto, metodoError) {
    if (!window.navigator.onLine) {
        metodoError('La conexión Wi-fi y los datos móviles No se encuentran disponibles');
        return;
    }
    waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
    $.support.cors = true;
    $.ajax({
        async: true,
        type: 'GET',
        contentType: 'application/html;charset=utf-8',
        url: urlAccion,
        data: JSON.stringify(objetoAEnviar),
        dataType: 'html',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message, error, cod) {
            debugger;
            if (IsJsonString(message.responseText)) {
                var errorDTO = JSON.parse(message.responseText);
                metodoError(errorDTO.Message);
            } else {
                document.write(message.responseText);
            }
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}

var PostHtmlAjax = function (urlAccion, objetoAEnviar, metodoCorrecto, metodoError) {
    if (!window.navigator.onLine) {
        metodoError('La conexión Wi-fi y los datos móviles No se encuentran disponibles');
        return;
    }
    waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
    $.support.cors = true;
    $.ajax({
        async: true,
        type: 'POST',
        contentType: 'application/html;charset=utf-8',
        url: urlAccion,
        data: JSON.stringify(objetoAEnviar),
        dataType: 'html',
        success: function (message) {
            metodoCorrecto(message);
        },
        error: function (message, error, cod) {
            debugger;
            if (IsJsonString(message.responseText)) {
                var errorDTO = JSON.parse(message.responseText);
                metodoError(errorDTO.Message);
            } else {
                document.write(message.responseText);
            }
        },
        complete: function () {
            waitingDialog.hide();
        }
    });
}


var MostrarError = function (titulo, texto) {
    //bootbox.alert(texto, function () { });
    toastr.error(texto, titulo);
};

var MostrarNotificacion = function (Tipo, Message) {
    //$('.top-right').notify({
    //    message: { text: Message.toLowerCase() },
    //    type: Tipo
    //}).show();
};

var MostrarMensaje = function (titulo, texto) {
    //    Ext.MessageBox.show({
    //        title: titulo,
    //        msg: texto,
    //        buttons: Ext.MessageBox.OK
    //    });
    //alert(texto);
    if (titulo == 'Error') {
        toastr.error(texto, titulo);
    }
    else {
        toastr.success(texto, titulo);
    }

};
var IsUndefinedOrEmpty = function (text) {
    if (text === undefined) {
        return true;
    }
    if (text == '') {
        return true;
    }
    if (/^\s+$/.test(text)) {
        return true;
    }
    return false;
};
var IsUndefinedOrNullOrEmpty = function (text) {
    if (text === undefined) {
        return true;
    }
    if (text == null) {
        return true;
    }
    if (text === '') {
        return true;
    }
    if (/^\s+$/.test(text)) {
        return true;
    }
    return false;
};
var IsUndefinedOrNullOrEmptyOrNaN = function (text) {
    if (text === undefined) {
        return true;
    }
    if (text == null) {
        return true;
    }
    if (text === '') {
        return true;
    }
    if (text === 'NaN') {
        return true;
    }
    if (/^\s+$/.test(text)) {
        return true;
    }
    return false;
};
var IsUndefinedOrObject = function (text) {
    //if (typeof (text) === "object") {
    //    return true;
    //}
    if (typeof (text) === "undefined") {
        return true;
    }
    if (/^\s+$/.test(text)) {
        return true;
    }
}
var DiferenciaEntreMeses = function (d1, d2) {
    var months;
    months = (d2.getFullYear() - d1.getFullYear()) * 12;
    months -= d1.getMonth() + 1;
    months += d2.getMonth();
    return (months <= 0 ? 0 : months) + 1;
};
Array.maxProp = function (array, prop) {
    var values = array.map(function (el) {
        return el[prop];
    });
    return Math.max.apply(Math, values);
};
var ConvertirJsonFechaToDatetime = function (v) {
    return new Date(parseInt(v.replace('/Date(', '')));
}
var ConvertToInt32 = function (string) {
    return IsUndefinedOrNullOrEmptyOrNaN(string) ? 0 : parseInt(string);
};
var ConvertToDecimal = function (string) {
    return IsUndefinedOrNullOrEmptyOrNaN(string) ? 0.00 : parseFloat(parseFloat(string).toFixed(2));
};
var ConvertToBoolean = function (string) {
    if (IsUndefinedOrNullOrEmptyOrNaN(string)) {
        return false;
    } else if (ConvertToInt32(string) == 1) {
        return true;
    } else {
        return false;
    }
};
var ConvertToDecimalFixed = function (string, fixed) {
    return IsUndefinedOrNullOrEmptyOrNaN(string) ? 0.00 : parseFloat(string).toFixed(fixed);
};
var ConvertToDatetime = function (date) {
    return IsUndefinedOrNullOrEmptyOrNaN(date) ? null : '\/Date(' + date.getTime() + '-0000)\/';
};

var ConvertStringToDateTime = function (value) {
    if (!isNaN(Date.parse(value))) {
        return value.trim().toDateTime();
    }
    else {
        return null;
    }
}
var convertToDateTimeFromJson = function (v) {
    var valor = ToDatetimeEspecial(v);
    var fechaFin;
    if (valor == 1) {
        fechaFin = new Date(parseInt(v.replace('/Date(', '')));
    } else {
        fechaFin = v;
    }
    if (fechaFin.getFullYear() > 1900) {
        return fechaFin;
    } else {
        return null;
    }
}
var ConvertToStringFromObject = function (value) {
    return value == null ? '' : value.toString().toUpperCase();
}


function ToDatetimeEspecial(input) {
    var d = new Date(input);
    if (isNaN(d)) return 1;
    return 0;
}

function calcular_edad(fecha) {
    var fechaActual = new Date();
    var diaActual = fechaActual.getDate();
    var mmActual = fechaActual.getMonth() + 1;
    var yyyyActual = fechaActual.getFullYear();
    FechaNac = fecha.split("/");
    var diaCumple = FechaNac[0];
    var mmCumple = FechaNac[1];
    var yyyyCumple = FechaNac[2];
    //retiramos el primer cero de la izquierda
    if (mmCumple.substr(0, 1) == 0) {
        mmCumple = mmCumple.substring(1, 2);
    }
    //retiramos el primer cero de la izquierda
    if (diaCumple.substr(0, 1) == 0) {
        diaCumple = diaCumple.substring(1, 2);
    }
    var edad = yyyyActual - yyyyCumple;

    //validamos si el mes de cumpleaños es menor al actual
    //o si el mes de cumpleaños es igual al actual
    //y el dia actual es menor al del nacimiento
    //De ser asi, se resta un año
    if ((mmActual < mmCumple) || (mmActual == mmCumple && diaActual < diaCumple)) {
        edad--;
    }
    return edad;
};

Date.prototype.formatDate = function (format) {
    var date = this,
        day = date.getDate(),
        month = date.getMonth() + 1,
        year = date.getFullYear(),
        hours = date.getHours(),
        minutes = date.getMinutes(),
        seconds = date.getSeconds();

    if (!format) {
        format = "MM/dd/yyyy";
    }

    format = format.replace("MM", month.toString().replace(/^(\d)$/, '0$1'));

    if (format.indexOf("yyyy") > -1) {
        format = format.replace("yyyy", year.toString());
    } else if (format.indexOf("yy") > -1) {
        format = format.replace("yy", year.toString().substr(2, 2));
    }

    format = format.replace("dd", day.toString().replace(/^(\d)$/, '0$1'));

    if (format.indexOf("t") > -1) {
        if (hours > 11) {
            format = format.replace("t", "pm");
        } else {
            format = format.replace("t", "am");
        }
    }

    if (format.indexOf("HH") > -1) {
        format = format.replace("HH", hours.toString().replace(/^(\d)$/, '0$1'));
    }

    if (format.indexOf("hh") > -1) {
        if (hours > 12) {
            hours -= 12;
        }

        if (hours === 0) {
            hours = 12;
        }
        format = format.replace("hh", hours.toString().replace(/^(\d)$/, '0$1'));
    }

    if (format.indexOf("mm") > -1) {
        format = format.replace("mm", minutes.toString().replace(/^(\d)$/, '0$1'));
    }

    if (format.indexOf("ss") > -1) {
        format = format.replace("ss", seconds.toString().replace(/^(\d)$/, '0$1'));
    }
    return format;
};
Date.prototype.addDays = function (days) {
    var dat = new Date(this.valueOf());
    dat.setDate(dat.getDate() + days);
    return dat;
}
Date.prototype.addHours = function (h) {
    this.setTime(this.getTime() + (h * 60 * 60 * 1000));
    return this;
}
var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
        timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
        timezoneClip = /[^-+\dA-Z]/g,
        pad = function (val, len) {
            val = String(val);
            len = len || 2;
            while (val.length < len) val = "0" + val;
            return val;
        };

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
            d = date[_ + "Date"](),
            D = date[_ + "Day"](),
            m = date[_ + "Month"](),
            y = date[_ + "FullYear"](),
            H = date[_ + "Hours"](),
            M = date[_ + "Minutes"](),
            s = date[_ + "Seconds"](),
            L = date[_ + "Milliseconds"](),
            o = utc ? 0 : date.getTimezoneOffset(),
            flags = {
                d: d,
                dd: pad(d),
                ddd: dF.i18n.dayNames[D],
                dddd: dF.i18n.dayNames[D + 7],
                m: m + 1,
                mm: pad(m + 1),
                mmm: dF.i18n.monthNames[m],
                mmmm: dF.i18n.monthNames[m + 12],
                yy: String(y).slice(2),
                yyyy: y,
                h: H % 12 || 12,
                hh: pad(H % 12 || 12),
                H: H,
                HH: pad(H),
                M: M,
                MM: pad(M),
                s: s,
                ss: pad(s),
                l: pad(L, 3),
                L: pad(L > 99 ? Math.round(L / 10) : L),
                t: H < 12 ? "a" : "p",
                tt: H < 12 ? "am" : "pm",
                T: H < 12 ? "A" : "P",
                TT: H < 12 ? "AM" : "PM",
                Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
                o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
                S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
            };

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

function trim(cadena) {
    cadena2 = "";
    len = cadena.length;
    for (var i = 0; i <= len; i++) if (cadena.charAt(i) != " ") { cadena2 += cadena.charAt(i); }
    return cadena2;
}
function esnumero(campo) { return (!(isNaN(campo))); }

var ValidarRucValor = function (valor) {
    valor = trim(valor)
    if (esnumero(valor)) {
        if (valor.length == 8) {
            suma = 0
            for (i = 0; i < valor.length - 1; i++) {
                digito = valor.charAt(i) - '0';
                if (i == 0) suma += (digito * 2)
                else suma += (digito * (valor.length - i))
            }
            resto = suma % 11;
            if (resto == 1) resto = 11;
            if (resto + (valor.charAt(valor.length - 1) - '0') == 11) {
                return true
            }
        }

        else if (valor.length == 11) {
            suma = 0
            x = 6
            for (i = 0; i < valor.length - 1; i++) {
                if (i == 4) x = 8
                digito = valor.charAt(i) - '0';
                x--
                if (i == 0) suma += (digito * x)
                else suma += (digito * x)
            }
            resto = suma % 11;
            resto = 11 - resto
            if (resto >= 10) resto = resto - 10;
            if (resto == valor.charAt(valor.length - 1) - '0') {
                return true
            }
        }
    }
    return false
}

var OnBeginAjax = function () {
    if (!window.navigator.onLine) {
        alert(mensajeErrorConexion);
        return false;
    }
    else {
        waitingDialog.show('procesando...', { dialogSize: 'sm', progressType: 'info' });
        return true;
    }
}


// For convenience...
//Date.prototype.format = function (mask, utc) {
//    return dateFormat(this, mask, utc);
//};
Date.prototype.yyyymmdd = function () {

    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based         
    var dd = this.getDate().toString();
    if (this.getFullYear() > 1900) {
        return yyyy + '-' + (mm[1] ? mm : "0" + mm[0]) + '-' + (dd[1] ? dd : "0" + dd[0]);
    }
    else {
        return "";
    }

};
Date.prototype.ddmmyyyy = function (separador) {
    separador = separador == null ? '/' : separador;
    var yyyy = this.getFullYear().toString();
    var mm = (this.getMonth() + 1).toString(); // getMonth() is zero-based         
    var dd = this.getDate().toString();
    if (this.getFullYear() > 1900) {
        return (dd[1] ? dd : "0" + dd[0]) + separador + (mm[1] ? mm : "0" + mm[0]) + separador + yyyy;
    }
    else {
        return "";
    }
};

String.prototype.toDateTime = function () {
    var dateStr = this;
    if (dateStr.length == 10) {
        var indice = -1;
        var separador = '';
        var fecha = null;
        if (dateStr.lastIndexOf('-') > 1) {
            indice = dateStr.lastIndexOf('-');
            separador = '-';
        }
        else if (dateStr.lastIndexOf('/') > 1) {
            indice = dateStr.lastIndexOf('/');
            separador = '/';
        }
        if (indice > 0 && separador != '') {
            var array = dateStr.split(separador);
            var mes = parseInt(array[1]) - 1;

            if (indice == 7) {
                fecha = new Date(array[0], mes, array[2]);
            }
            else if (indice == 5) {
                fecha = new Date(array[2], mes, array[0]);
            }
        }

        return fecha;
    }
    else {
        return null;
    }
};
var toDateTime = function () {

    if (dateStr.length == 10) {
        var indice = -1;
        var separador = '';
        var fecha = null;
        if (dateStr.lastIndexOf('-') > 1) {
            indice = dateStr.lastIndexOf('-');
            separador = '-';
        }
        else if (dateStr.lastIndexOf('/') > 1) {
            indice = dateStr.lastIndexOf('/');
            separador = '/';
        }
        if (indice > 0 && separador != '') {
            var array = dateStr.split(separador);
            var mes = parseInt(array[1]) - 1;

            if (indice == 7) {
                fecha = new Date(array[0], mes, array[2]);
            }
            else if (indice == 5) {
                fecha = new Date(array[2], mes, array[0]);
            }
        }

        return fecha;
    }
    else {
        return null;
    }
}

/*Impresion comprobante*/
var imprimirComprobante = function (data) {

    var htmlItem = new Array();
    htmlItem.push('<html xmlns="http://www.w3.org/1999/xhtml">');
    htmlItem.push('<head>');
    htmlItem.push('<meta name="viewport" content="width=device-width" />');
    htmlItem.push('<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />');
    htmlItem.push('<link href="/Content/email.css?e=123" media="all" rel="stylesheet" type="text/css" />');
    htmlItem.push('</head>');

    htmlItem.push('<body onload="window.print();">');
    htmlItem.push('<table class="body-wrap">');
    htmlItem.push('<tr><td></td>');
    htmlItem.push('    <td class="container" width="350">');
    htmlItem.push('        <div class="content">');
    htmlItem.push('            <table class="main" width="100%" cellpadding="0" cellspacing="0">');
    htmlItem.push('                <tr>');
    htmlItem.push('                    <td class="content-wrap aligncenter">');
    htmlItem.push('                        <table width="100%" cellpadding="0" cellspacing="0">');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block"><h2 style="margin: 1px 0 0;">' + data.NombreComercial + '</h2> <h6>' + data.Rubro + '</h6></td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">' + data.RazonSocial + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">RUC: ' + data.RUC + ' - Tel: ' + data.TelefonoUnidadNegocio + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">' + data.DireccionLocalNegocio + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">Domicilio Fiscal: ' + data.DireccionFiscalNegocio + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">COMPROBANTE : ' + data.DescripcionTipoComprobante + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">Numero : ' + data.NumeroComprobante + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">Serie : ' + data.SerieEquipoImpresion + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">Nro Autorizacion : ' + data.CodigoAutorizacionSunat + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">');
    htmlItem.push('                                    <table class="invoice">');
    htmlItem.push('                                        <tr>');
    htmlItem.push('                                            <td>');
    htmlItem.push('                                                <b>CLIENTE  </b> : ' + data.NombrePersona + '<br />');
    htmlItem.push('                                                <b>RUC/DNI  </b> : ' + data.NumeroDocumentoPersona + '<br />');
    htmlItem.push('                                                <b>Dirección</b> : ----------<br />');
    htmlItem.push('                                                <b>Emitido  </b> : ' + ConvertirJsonFechaToDatetime(data.FechaPago).toLocaleString() + '<br/>');
    htmlItem.push('                                            </td>');
    htmlItem.push('                                        </tr>');
    htmlItem.push('                                        <tr>');
    htmlItem.push('                                            <td>');
    htmlItem.push('                                                <table class="invoice-items" cellpadding="0" cellspacing="0">');
    htmlItem.push('                                                    <tr>');
    htmlItem.push('                                                        <th width="60%">Descripción</th>');
    htmlItem.push('                                                        <th width="10%">Cant.</th>');
    htmlItem.push('                                                        <th width="10%">P.U.</th>');
    htmlItem.push('                                                        <th class="alignright" width="20%">Imp.</th>');
    htmlItem.push('                                                    </tr>');
    for (var i = 0; i < data.DetalleReciboList.length; i++) {
        htmlItem.push('   <tr>')
        htmlItem.push('     <td width="60%">' + data.DetalleReciboList[i].Descripcion + '</td>');
        htmlItem.push('     <td width="10%">' + data.DetalleReciboList[i].Cantidad + '</td>');
        htmlItem.push('     <td width="10%">' + data.DetalleReciboList[i].PrecioUnitario + '</td>');
        htmlItem.push('     <td class="alignright" width="20%">' + data.DetalleReciboList[i].Importe + '</td>');
        htmlItem.push('   </tr>');
    }
    htmlItem.push('                                                    <tr class="total">');
    htmlItem.push('                                                        <td colspan="4">');
    if (data.IdTipoComprobante == 2 || data.IdTipoComprobante == 3) {

        htmlItem.push('                                                            <div style="width:100%">');
        htmlItem.push('                                                                <div style="width:80%; float:left; text-align:right;">Sub Total</div>');
        htmlItem.push('                                                                <div style="width:20%;float:right; text-align:right;">' + data.SubTotal + '</div>');
        htmlItem.push('                                                            </div>');
        htmlItem.push('                                                            <div style="width:100%">');
        htmlItem.push('                                                                <div style="width:80%; float:left; text-align:right;">IGV</div>');
        htmlItem.push('                                                                <div style="width:20%;float:right; text-align:right;">' + data.IGV + '</div>');
        htmlItem.push('                                                            </div>');
    }
    htmlItem.push('                                                            <div style="width:100%">');
    htmlItem.push('                                                                <div style="width:80%; float:left; text-align:right;">Total</div>');
    htmlItem.push('                                                                <div style="width:20%;float:right; text-align:right;">' + data.MontoTotal + '</div>');
    htmlItem.push('                                                            </div>');
    htmlItem.push('                                                        </td>');
    htmlItem.push('                                                    </tr>');
    htmlItem.push('                                                </table>');
    htmlItem.push('                                            </td>');
    htmlItem.push('                                        </tr>');
    htmlItem.push('                                    </table>');
    htmlItem.push('                                </td>');
    htmlItem.push('                            </tr>');
    if (data.DetalleFormaPagoList.length > 0) {
        htmlItem.push('                            <tr>');
        htmlItem.push('                            <td>');
        htmlItem.push('                               <div style="width:40%; float:left; text-align:left;">FORMA DE PAGO </div>');
        for (var i = 0; i < data.DetalleFormaPagoList.length; i++) {
            htmlItem.push('<div style="width:58%; float:right; text-align:right;padding:0px 5px 0px 0px">' + data.DetalleFormaPagoList[i].Descripcion + '   :  ' + data.DetalleFormaPagoList[i].Monto + '</div>');
        }
        htmlItem.push('                            <br/>');
        htmlItem.push('                            <br/>');
        htmlItem.push('                            </td>');
        htmlItem.push('                            </tr>');
    }
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">');
    htmlItem.push('                                    Gracias por su preferencia no se aceptan cambios ni devoluciones');
    htmlItem.push('                                </td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                        </table>');
    htmlItem.push('                    </td>');
    htmlItem.push('                </tr>');
    htmlItem.push('            </table>');
    htmlItem.push('        </div>');
    htmlItem.push('    </td>');
    htmlItem.push('    <td></td>');
    htmlItem.push(' </tr>');
    htmlItem.push('</table>');

    htmlItem.push('</body></html>');

    var ancho = screen.width - 10;
    var alto = screen.height - 75;

    var newWin = window.open('', 'Prin-Window', 'directories=no, border=0,scrollbars=yes,status=yes,toolbar=no,width=' + ancho + ',height=' + alto + ',top=0,left=0');
    newWin.document.open();
    newWin.document.write(htmlItem.join(' '));
    newWin.document.close();
    window.setTimeout(function () {
        newWin.close();
    }, 3000);
};
/*Imprimir Politicas del Ingreso huesped*/
var imprimirPoliticaIngresoHuesped = function (data) {
    var htmlItem = new Array();
    htmlItem.push('<html xmlns="http://www.w3.org/1999/xhtml">');
    htmlItem.push('<head>');
    htmlItem.push('<meta name="viewport" content="width=device-width" />');
    htmlItem.push('<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />');
    htmlItem.push('<link href="/Content/email.css?e=123222" media="all" rel="stylesheet" type="text/css" />');
    htmlItem.push('</head>');

    htmlItem.push('<body onload="window.print();">');
    htmlItem.push('<table class="body-wrap">');
    htmlItem.push('<tr><td></td>');
    htmlItem.push('    <td class="container" width="350">');
    htmlItem.push('        <div class="content">');
    htmlItem.push('            <table class="main" width="100%" cellpadding="0" cellspacing="0">');
    htmlItem.push('                <tr>');
    htmlItem.push('                    <td class="content-wrap aligncenter">');
    htmlItem.push('                        <table width="100%" cellpadding="0" cellspacing="0">');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block"><h2 style="margin: 1px 0 0;font-weight: bold;">' + data.NombreComercial + '</h2></td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="font-family: calibri;">ESTIMADO(A): ' + data.NombrePersona + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="font-family: calibri;font-size: 15px;">FECHA Y HORA DE INICIO : ' + data.FechaHoraInicio + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="font-family: calibri;font-size: 15px;">FECHA Y HORA FINALIZA : ' + data.FechaHoraFin + '</td>');
    htmlItem.push('                            </tr>');

    //htmlItem.push('                            <tr>');
    //htmlItem.push('                                <td class="content-block" style="font-family: calibri;">ESTIMADO(A): DIANA SU TURNO INICIA EL 12/10/2019 22:45 HRS Y TERMINA EL 12/10/2019 A LAS 23:59 HRS</td>');
    //htmlItem.push('                            </tr>');

    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="color: #000;font-weight: bold;font-size: 16px;font-family: arial;">TIEMPO TOTAL ALQUILADO: ' + data.Tiempo + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="font-size: 12px;"> ' + data.Advertencia + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="font-size: 26px;font-weight: bold;font-family: calibri;">' + data.TipoWellness + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" ><span style="color: #000;font-size: 130px;font-weight: bold;font-family:\'Arial Black\';">' + data.Numero + '</span></td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="font-family: calibri;">' + data.CuerpoMensaje + '</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="padding: 5px 30px;"><div style="height: 90px;border-bottom: solid 1px #000;"></div></td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block">FIRMA DE ACEPTACION</td>');
    htmlItem.push('                            </tr>');
    htmlItem.push('                            <tr>');
    htmlItem.push('                                <td class="content-block" style="font-family: calibri;font-size: 18px;font-weight: bold;"></td>');
    htmlItem.push('                            </tr>');

    if (!IsUndefinedOrEmpty(data.PlacaVehiculo)) {
        htmlItem.push('                            <tr>');
        htmlItem.push('                                <td class="content-block" style="font-family: calibri;font-size: 18px;font-weight: bold;"><div style="height: 50px;border-bottom: dotted 1px #000;"></div></td>');
        htmlItem.push('                            </tr>');

        //htmlItem.push('                            <tr>');
        //htmlItem.push('                                <td class="content-block" style="font-family: calibri;font-size: 18px;">NOMBRES : ' + data.NombrePersona + '</td>');
        //htmlItem.push('                            </tr>');
        htmlItem.push('                            <tr>');
        htmlItem.push('                                <td class="content-block" style="font-family: calibri;font-size: 18px;">PLACA VEHICULO : ' + data.PlacaVehiculo + ' . . . . . . . .</td>');
        htmlItem.push('                            </tr>');
        htmlItem.push('                            <tr>');
        htmlItem.push('                                <td class="content-block" style="font-family: calibri;font-size: 18px;">OBSERVACIONES : . . . . . . . . . . . . . .  </td>');
        htmlItem.push('                            </tr>');
    }


    htmlItem.push('                        </table>');
    htmlItem.push('                    </td>');
    htmlItem.push('                </tr>');
    htmlItem.push('            </table>');
    htmlItem.push('        </div>');
    htmlItem.push('    </td>');
    htmlItem.push('    <td></td>');
    htmlItem.push(' </tr>');
    htmlItem.push('</table>');

    htmlItem.push('</body></html>');

    var ancho = screen.width - 10;
    var alto = screen.height - 75;

    var newWin = window.open('', 'Prin-Window', 'directories=no, border=0,scrollbars=yes,status=yes,toolbar=no,width=' + ancho + ',height=' + alto + ',top=0,left=0');
    newWin.document.open();
    newWin.document.write(htmlItem.join(' '));
    newWin.document.close();
    window.setTimeout(function () {
        newWin.close();
    }, 3000);
};

var imprimirComprobantePDFUrl = function (UrlPDF) {

    var htmlItem = new Array();
    htmlItem.push('<html xmlns="http://www.w3.org/1999/xhtml">');
    htmlItem.push('<head>');
    htmlItem.push('<meta name="viewport" content="width=device-width" />');
    htmlItem.push('<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />');
    htmlItem.push('<link href="/Content/email.css?e=123" media="all" rel="stylesheet" type="text/css" />');
    htmlItem.push('</head>');
    htmlItem.push('<body onload="window.print();">');
    htmlItem.push('<embed width="100%" height="100%" name="plugin" id="plugin" src="' + UrlPDF + '" type="application/pdf">');
    htmlItem.push('</body></html>');
    var ancho = screen.width - 10;
    var alto = screen.height - 75;
    var newWin = window.open('', 'Prin-Window', 'directories=no, border=0,scrollbars=yes,status=yes,toolbar=no,width=' + ancho + ',height=' + alto + ',top=0,left=0');
    newWin.document.open();
    newWin.document.write(htmlItem.join(' '));
    newWin.document.close();
    window.setTimeout(function () {
        //newWin.close();
    }, 3000);
};

/**
 * Module for displaying "Waiting for..." dialog using Bootstrap
 *
 * @author Eugene Maslovich <ehpc@em42.ru>
 */

var waitingDialog = waitingDialog || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
        '<div  class="modal modal_hide_local" data-backdrop="false" data-keyboard="true" tabindex="-1" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-m">' +
        '<div class="modal-content">' +
        '<div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close" style="background:#aaa;"><span aria-hidden="true">&times;</span></button><h3 style="margin:0;"></h3></div>' +
        '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
        '</div>' +
        '</div></div></div>');

    return {
        /**
		 * Opens our dialog
		 * @param message Custom message
		 * @param options Custom options:
		 * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
		 * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
		 */
        show: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            // Opening dialog            
            //$dialog.modal();
        },
        /**
		 * Closes dialog
		 */
        hide: function () {
            $dialog.modal('hide');
            //$dialog.remove();
        }
    };

})(jQuery);

var waitingDialog2 = waitingDialog2 || (function ($) {
    'use strict';

    // Creating modal dialog's DOM
    var $dialog = $(
        '<div  class="modal fade modal_hide_local" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        '<div class="modal-dialog modal-m">' +
        '<div class="modal-content">' +
        '<div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 style="margin:0;"></h3></div>' +
        '<div class="modal-body">' +
        '<div class="progress progress-striped active" style="margin-bottom:0;"><div class="progress-bar" style="width: 100%"></div></div>' +
        '</div>' +
        '</div></div></div>');

    return {
        /**
		 * Opens our dialog
		 * @param message Custom message
		 * @param options Custom options:
		 * 				  options.dialogSize - bootstrap postfix for dialog size, e.g. "sm", "m";
		 * 				  options.progressType - bootstrap postfix for progress bar type, e.g. "success", "warning".
		 */
        show: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text(message);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    settings.onHide.call($dialog);
                });
            }
            // Opening dialog            
            $dialog.modal();
        },
        /**
		 * Closes dialog
		 */
        hide: function () {
            $dialog.modal('hide');
            //$dialog.remove();
        }
    };

})(jQuery);

var WindowDialogError = WindowDialogError || (function ($) {
    'use strict';
    // Creating modal dialog's DOM
    var $dialog = $(
        '<div  class="modal fade modal_hide_local" data-backdrop="static" data-keyboard="false" tabindex="-1" aria-hidden="true" style="padding-top:15%; overflow-y:visible;">' +
        ' <div class="modal-dialog modal-m">' +
        '  <div class="modal-content">' +
        '     <div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h3 style="margin:0;"></h3></div>' +
        '     <div class="modal-body">' +
        //'        <div class="progress progress-striped active" style="margin-bottom:0;">' +
        //'          <div class="progress-bar" style="width: 100%"></div>' +
        //'        </div>' +
        '     </div>' +

        //'     <div class="modal-footer">' +
        //'       <button type="button" class="btn btn-sm btn-primary" onclick="WindowDialogError.hide();">Cerrar</button>' +
        //'     </div>' +

        '  </div>'+
        ' </div>' +
        '</div>');

    return {
        show: function (message, options) {
            // Assigning defaults
            if (typeof options === 'undefined') {
                options = {};
            }
            if (typeof message === 'undefined') {
                message = 'Loading';
            }
            var settings = $.extend({
                dialogSize: 'm',
                progressType: '',
                onHide: null // This callback runs after the dialog was hidden
            }, options);

            // Configuring dialog
            $dialog.find('.modal-dialog').attr('class', 'modal-dialog').addClass('modal-' + settings.dialogSize);
            $dialog.find('.progress-bar').attr('class', 'progress-bar');
            if (settings.progressType) {
                $dialog.find('.progress-bar').addClass('progress-bar-' + settings.progressType);
            }
            $dialog.find('h3').text('ERROR SERVER LOAD');
            $('iframe[name="frame_error"]').remove();
            
            var iframe = document.createElement('iframe');
            iframe.style.width = '100%';
            iframe.name = "frame_error";
            iframe.src = 'data:text/html;charset=utf-8,' + encodeURI(message);

            //$dialog.find('.modal-body').html('<iframe srcdoc="' + message + '');
            $dialog.find('.modal-body').append(iframe);
            // Adding callbacks
            if (typeof settings.onHide === 'function') {
                $dialog.off('hidden.bs.modal').on('hidden.bs.modal', function (e) {
                    e.stopPropagation();
                    alert('cerrando ventana');
                    $('body').css('padding-right', '');
                    settings.onHide.call($dialog);
                });
            }

            $dialog.on('shown.bs.modal', function () {
                e.stopPropagation();
                alert('cerrando ventana');
                $("body.modal-open").removeAttr("style");
            });

            // Opening dialog                  
            $dialog.modal();
           
        },
        /**
		 * Closes dialog
		 */
        hide: function () {
            debugger;
            //$dialog.modal('hide');
            $dialog.remove();
        }
    };

})(jQuery);