using BotComers.Helpers;
using E_BusinessLayer.Gimnasio;
using E_DataModel;
using E_DataModel.Gimnasio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace BotComers.Helpers
{
    public class NubefactService : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Respuesta EjecutarWebService(VentasDTO request, ConfiguracionDTO configuracion)
        {
            ///CREAMOS EL JSON
            Invoice invoice = new Invoice();
            invoice.operacion = "generar_comprobante";
            invoice.tipo_de_comprobante = request.CodigoTipoComprobante; //request.TipoComprobante == "" ? 1 : 2;
            invoice.serie = request.SerieComprobante;
            invoice.numero = 0;
            invoice.sunat_transaction = 1;

            if (invoice.tipo_de_comprobante == 1) /*Parameters.TipoComprobanteFacturacion.FacturaElectronica*/
            {
                invoice.cliente_tipo_de_documento = "6";//request.CodigoTipoDocumentoSocio.ToString();  //(Parameters.TipoDocumentoFacturacion.RUC).ToString();
                invoice.cliente_numero_de_documento = request.RucContribuyente;
                invoice.cliente_denominacion = request.RazonSocialContribuyente;
                invoice.cliente_direccion = request.DireccionFiscalEmpresa;
                invoice.cliente_email = request.CorreoEmpresa;
            }
            else
            {
                if (request.CodigoTipoDocumentoSocio ==2)/*PASAPORTE APPSFIT*/
                {
                    invoice.cliente_tipo_de_documento = "7"; /*CODIGO PASAPORTE SUNAFACT*/
                }
                else if (request.CodigoTipoDocumentoSocio == 3)
                {
                    invoice.cliente_tipo_de_documento = "4"; /*CARNET EXTRANJERIA*/
                }
                else
                    invoice.cliente_tipo_de_documento = request.CodigoTipoDocumentoSocio ==0 ? "-": request.CodigoTipoDocumentoSocio.ToString();
                
                invoice.cliente_numero_de_documento = request.CodigoTipoDocumentoSocio == 0?"VARIOS": request.RUC_DNI;
                invoice.cliente_denominacion = request.CodigoTipoDocumentoSocio == 0 ? "VARIOS" : request.RazonSocial_Sr??"";
                invoice.cliente_direccion = request.CodigoTipoDocumentoSocio == 0 ? "" : request.Direccion??"";
                invoice.cliente_email = request.CodigoTipoDocumentoSocio == 0 ? "" : request.CorreoEmpresa??"";
            }


            invoice.cliente_email_1 = "";
            invoice.cliente_email_2 = "";
            invoice.fecha_de_emision = DateTime.Now;
            invoice.fecha_de_vencimiento = DateTime.Now;
            invoice.moneda = 1;
            invoice.tipo_de_cambio = "";
            invoice.porcentaje_de_igv = 18.00;//Commun.TaxImpuesto;
            Double igv_calculador = (/*Commun.TaxImpuesto*/ 18.00 / 100) + 1;
            invoice.descuento_global = "";
            invoice.total_descuento = "";
            invoice.total_anticipo = "";
            /*600.0;*/
            invoice.total_gravada = Math.Round(Convert.ToDouble(request.TotalNeto) / (igv_calculador), 2);
            invoice.total_inafecta = "";
            invoice.total_exonerada = "";
            /*108;*/
            invoice.total_igv = Math.Round(Convert.ToDouble(request.TotalNeto - (request.TotalNeto / Convert.ToDecimal(igv_calculador))), 2);
            invoice.total_gratuita = "";
            invoice.total_otros_cargos = "";
            /*708*/
            invoice.total = Math.Round(Convert.ToDouble(request.TotalNeto), 2);
            invoice.percepcion_tipo = "";
            invoice.percepcion_base_imponible = "";
            invoice.total_percepcion = "";
            invoice.detraccion = false;
            invoice.observaciones = request.Comentario;
            invoice.documento_que_se_modifica_tipo = "";
            invoice.documento_que_se_modifica_serie = "";
            invoice.documento_que_se_modifica_numero = "";
            invoice.tipo_de_nota_de_credito = "";
            invoice.tipo_de_nota_de_debito = "";
            invoice.enviar_automaticamente_a_la_sunat = false;
            invoice.enviar_automaticamente_al_cliente = false;
            invoice.codigo_unico = "";

            invoice.condiciones_de_pago = "";
            foreach (var item in request.ListaFormaPago)
            {
                switch (item.FormaPago)
                {
                    case 1:
                        invoice.condiciones_de_pago = "EFECTIVO";
                        break;
                    case 2:
                        invoice.condiciones_de_pago = "VISA";
                        break;
                    case 3:
                        invoice.condiciones_de_pago = "MASTERCARD";
                        break;
                    default:
                        break;
                }
            }
            invoice.medio_de_pago = "";
            invoice.placa_vehiculo = "";
            invoice.orden_compra_servicio = "";
            invoice.tabla_personalizada_codigo = "";
            invoice.formato_de_pdf = "TICKET";
            invoice.items = new List<Items>();
            foreach (var item in request.ListaDetalle)
            {
                var item_row = new Items();

                item_row.unidad_de_medida = "ZZ"; //item.TipoConsumoTm == 2 ? "NIU" : "ZZ";
                item_row.codigo =""; // "001";
                item_row.descripcion = item.Descripcion;
                item_row.cantidad = item.Cantidad;
                item_row.valor_unitario = Math.Round(Convert.ToDouble(item.PrecioUnitario / Convert.ToDecimal(igv_calculador)), 10); //500,
                item_row.precio_unitario = Math.Round(Convert.ToDouble(item.PrecioUnitario), 10);
                item_row.descuento = "";
                item_row.subtotal = Math.Round(Convert.ToDouble(item.Importe / Convert.ToDecimal(igv_calculador)), 10);  //500,
                item_row.tipo_de_igv = 1;
                item_row.igv = Math.Round(Convert.ToDouble(item.Importe) - Convert.ToDouble(item.Importe / Convert.ToDecimal(igv_calculador)), 10); //90
                item_row.total = Math.Round(Convert.ToDouble(item.Importe), 10);  //590
                item_row.anticipo_regularizacion = false;
                item_row.anticipo_comprobante_serie = "";
                item_row.anticipo_comprobante_numero = "";

                invoice.items.Add(item_row);
            }

            string json = JsonConvert.SerializeObject(invoice, Formatting.Indented);
            Console.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            string json_en_utf_8 = Encoding.UTF8.GetString(bytes);

            /// #########################################################
            /// #### PASO 3: ENVIAR EL ARCHIVO A NUBEFACT ####
            /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++
            /// # SI ESTÁS TRABAJANDO CON ARCHIVO JSON
            /// # - Debes enviar en el HEADER de tu solicitud la siguiente lo siguiente:
            /// # Authorization = Token token="8d19d8c7c1f6402687720eab85cd57a54f5a7a3fa163476bbcf381ee2b5e0c69"
            /// # Content-Type = application/json
            /// # - Adjuntar en el CUERPO o BODY el archivo JSON o TXT
            /// # SI ESTÁS TRABAJANDO CON ARCHIVO TXT
            /// # - Debes enviar en el HEADER de tu solicitud la siguiente lo siguiente:
            /// # Authorization = Token token="8d19d8c7c1f6402687720eab85cd57a54f5a7a3fa163476bbcf381ee2b5e0c69"
            /// # Content-Type = text/plain
            /// # - Adjuntar en el CUERPO o BODY el archivo JSON o TXT
            /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++

  
            string json_de_respuesta = SendJson(configuracion.UrlAPISunafact, json_en_utf_8, configuracion.TokenSunafact);
            dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
            dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);

            ///#########################################################
            ///#### PASO 4: LEER RESPUESTA DE NUBEFACT ####
            ///+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ///# Recibirás una respuesta de NUBEFACT inmediatamente lo cual se debe leer, verificando que no haya errores.
            ///# Debes guardar en la base de datos la respuesta que te devolveremos.
            ///# Escríbenos a soporte@nubefact.com o llámanos al teléfono: 01 468 3535 (opción 2) o celular (WhatsApp) 955 598762
            ///# Puedes imprimir el PDF que nosotros generamos como también generar tu propia representación impresa previa coordinación con nosotros.
            ///# La impresión del documento seguirá haciéndose desde tu sistema. Enviaremos el documento por email a tu cliente si así lo indicas en el archivo JSON o TXT.
            ///+++++++++++++++++++++++++++++++++++++++++++++++++++++++

            dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            if (leer_respuesta.errors == null)
            {
                Console.WriteLine(json_r_in);
                Console.WriteLine("TIPO: " + leer_respuesta.tipo);
                Console.WriteLine("SERIE: " + leer_respuesta.serie);
                Console.WriteLine("NUMERO: " + leer_respuesta.numero);
                Console.WriteLine("URL: " + leer_respuesta.url);
                Console.WriteLine("ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat);
                Console.WriteLine("DESCRIPCION SUNAT: " + leer_respuesta.sunat_description);
                Console.WriteLine("NOTA SUNAT: " + leer_respuesta.sunat_note);
                Console.WriteLine("CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode);
                Console.WriteLine("SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error);
                Console.WriteLine("PDF EN BASE64: " + leer_respuesta.pdf_zip_base64);
                Console.WriteLine("XML EN BASE64: " + leer_respuesta.xml_zip_base64);
                Console.WriteLine("CDR EN BASE64: " + leer_respuesta.cdr_zip_base64);
                Console.WriteLine("CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr);
                Console.WriteLine("CODIGO HASH: " + leer_respuesta.codigo_hash);
                Console.WriteLine("CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras);
            }
            else
            {
                Console.WriteLine("ERRORES: " + leer_respuesta.errors);
            }

            return leer_respuesta;
        }

        private string SendJson(string ruta, string json, string token)
        {
            try
            {
              

                using (var client = new WebClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    /// ESPECIFICAMOS EL TIPO DE DOCUMENTO EN EL ENCABEZADO

                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                    /// ASI COMO EL TOKEN UNICO
                    client.Headers[HttpRequestHeader.Authorization] = "Token token=" + token;
                    ///ASIGNAR ENCONDING PARA UTF8
                    client.Encoding = System.Text.Encoding.UTF8;
                    /// OBTENEMOS LA RESPUESTA
                    string respuesta = client.UploadString(ruta, "POST", json);
                    /// Y LA 'RETORNAMOS'
                    return respuesta;
                }
            }
            catch (WebException ex)
            {
                /// EN CASO EXISTA ALGUN ERROR, LO TOMAMOS
                var respuesta = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                /// Y LO 'RETORNAMOS'
                return respuesta;
            }
        }

        public Respuesta EjecutarWebServicePuntoVenta(ComprobanteDTO request, ConfiguracionDTO configuracion)
        {
            ///CREAMOS EL JSON
            Invoice invoice = new Invoice();
            invoice.operacion = "generar_comprobante";
            invoice.tipo_de_comprobante = request.CodigoTipoComprobante;
            invoice.serie = request.Correlativo.Split('-')[0];
            invoice.numero = 0;
            invoice.sunat_transaction = 1;

            if (invoice.tipo_de_comprobante == 1) /*Parameters.TipoComprobanteFacturacion.FacturaElectronica*/
            {
                invoice.cliente_tipo_de_documento = request.CodigoTipoDocumentoSocio.ToString();  //(Parameters.TipoDocumentoFacturacion.RUC).ToString();
                invoice.cliente_numero_de_documento = request.RucEmpresa;
                invoice.cliente_denominacion = request.RazonSocial_Sr;
                invoice.cliente_direccion = request.Direccion;
                //invoice.cliente_email = request.CorreoEmpresa;
            }
            else
            {
                invoice.cliente_tipo_de_documento = request.CodigoTipoDocumentoSocio == 0 ? "-" : request.CodigoTipoDocumentoSocio.ToString();
                invoice.cliente_numero_de_documento = request.CodigoTipoDocumentoSocio == 0 ? "VARIOS" : request.RUC_DNI;
                invoice.cliente_denominacion = request.CodigoTipoDocumentoSocio == 0 ? "VARIOS" : request.RazonSocial_Sr ?? "";
                invoice.cliente_direccion = request.CodigoTipoDocumentoSocio == 0 ? "" : request.Direccion ?? "";
                invoice.cliente_email = request.CodigoTipoDocumentoSocio == 0 ? "" : "";
            }

            invoice.cliente_email_1 = "";
            invoice.cliente_email_2 = "";
            invoice.fecha_de_emision = DateTime.Now;
            invoice.fecha_de_vencimiento = DateTime.Now;
            invoice.moneda = 1;
            invoice.tipo_de_cambio = "";
            invoice.porcentaje_de_igv = 18.00;
            Double igv_calculador = (/*Commun.TaxImpuesto*/ 18.00 / 100) + 1;
            invoice.descuento_global = "";
            invoice.total_descuento = "";
            invoice.total_anticipo = "";
            /*600.0;*/
            invoice.total_gravada = Math.Round(Convert.ToDouble(request.Total) / (igv_calculador), 10);
            invoice.total_inafecta = "";
            invoice.total_exonerada = "";
            /*108;*/
            invoice.total_igv = Math.Round(Convert.ToDouble(request.Total - (request.Total / Convert.ToDecimal(igv_calculador))), 10);
            invoice.total_gratuita = "";
            invoice.total_otros_cargos = "";
            /*708*/
            invoice.total = Math.Round(Convert.ToDouble(request.Total), 10);
            invoice.percepcion_tipo = "";
            invoice.percepcion_base_imponible = "";
            invoice.total_percepcion = "";
            invoice.detraccion = false;
            invoice.observaciones = "";
            invoice.documento_que_se_modifica_tipo = "";
            invoice.documento_que_se_modifica_serie = "";
            invoice.documento_que_se_modifica_numero = "";
            invoice.tipo_de_nota_de_credito = "";
            invoice.tipo_de_nota_de_debito = "";
            invoice.enviar_automaticamente_a_la_sunat = false;
            invoice.enviar_automaticamente_al_cliente = false;
            invoice.codigo_unico = "";

            invoice.condiciones_de_pago = "";
            foreach (var item in request.listaDetallePago)
            {
                switch (item.CodigoMetodoPago)
                {
                    case 1:
                        invoice.condiciones_de_pago = "EFECTIVO";
                        break;
                    case 2:
                        invoice.condiciones_de_pago = "VISA";
                        break;
                    case 3:
                        invoice.condiciones_de_pago = "MASTERCARD";
                        break;
                    default:
                        break;
                }
            }
            invoice.medio_de_pago = "";
            invoice.placa_vehiculo = "";
            invoice.orden_compra_servicio = "";
            invoice.tabla_personalizada_codigo = "";
            invoice.formato_de_pdf = "TICKET";
            invoice.items = new List<Items>();
            foreach (var item in request.listaDetalle)
            {
                var item_row = new Items();

                //item_row.unidad_de_medida = "NIU"; //item.TipoConsumoTm == 2 ? "NIU" : "ZZ";
                //item_row.codigo = ""; // "001";
                //item_row.descripcion = item.Descripcion;
                //item_row.cantidad = Convert.ToDouble(item.Cantidad);
                //item_row.valor_unitario = Math.Round(Convert.ToDouble(item.Precio / Convert.ToDecimal(igv_calculador)), 10); //500,
                //item_row.precio_unitario = Math.Round(Convert.ToDouble(item.Precio), 10);
                //item_row.descuento = "";
                //item_row.subtotal = Math.Round(Convert.ToDouble(item.Importe / Convert.ToDecimal(igv_calculador)), 10);  //500,
                //item_row.tipo_de_igv = 1;
                //item_row.igv = Math.Round(Convert.ToDouble(item.Importe) - Convert.ToDouble(item.Importe / Convert.ToDecimal(igv_calculador)), 10); //90
                //item_row.total = Math.Round(Convert.ToDouble(item.Importe), 10);  //590
                //item_row.anticipo_regularizacion = false;
                //item_row.anticipo_comprobante_serie = "";
                //item_row.anticipo_comprobante_numero = "";
                //invoice.items.Add(item_row);
                //var item_row = new Items();

                item_row.unidad_de_medida = "NIU"; //item.TipoConsumoTm == 2 ? "NIU" : "ZZ";
                item_row.codigo = ""; // "001";
                item_row.descripcion = item.Descripcion;
                item_row.cantidad = Convert.ToInt32(item.Cantidad);
                item_row.valor_unitario = Math.Round(Convert.ToDouble(item.Precio / Convert.ToDecimal(igv_calculador)), 10); //500,
                item_row.precio_unitario = Math.Round(Convert.ToDouble(item.Precio ), 10);
                item_row.descuento = "";
                item_row.subtotal = Math.Round(Convert.ToDouble(item.Total / Convert.ToDecimal(igv_calculador)), 10);  //500,
                item_row.tipo_de_igv = 1;
                item_row.igv = Math.Round(Convert.ToDouble(item.Total) - Convert.ToDouble(item.Total / Convert.ToDecimal(igv_calculador)), 10); //90
                item_row.total = Math.Round(Convert.ToDouble(item.Total), 10);  //590
                item_row.anticipo_regularizacion = false;
                item_row.anticipo_comprobante_serie = "";
                item_row.anticipo_comprobante_numero = "";
                invoice.items.Add(item_row);
            }

            string json = JsonConvert.SerializeObject(invoice, Formatting.Indented);
            Console.WriteLine(json);
            byte[] bytes = Encoding.Default.GetBytes(json);
            string json_en_utf_8 = Encoding.UTF8.GetString(bytes);

            /// #########################################################
            /// #### PASO 3: ENVIAR EL ARCHIVO A NUBEFACT ####
            /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++
            /// # SI ESTÁS TRABAJANDO CON ARCHIVO JSON
            /// # - Debes enviar en el HEADER de tu solicitud la siguiente lo siguiente:
            /// # Authorization = Token token="8d19d8c7c1f6402687720eab85cd57a54f5a7a3fa163476bbcf381ee2b5e0c69"
            /// # Content-Type = application/json
            /// # - Adjuntar en el CUERPO o BODY el archivo JSON o TXT
            /// # SI ESTÁS TRABAJANDO CON ARCHIVO TXT
            /// # - Debes enviar en el HEADER de tu solicitud la siguiente lo siguiente:
            /// # Authorization = Token token="8d19d8c7c1f6402687720eab85cd57a54f5a7a3fa163476bbcf381ee2b5e0c69"
            /// # Content-Type = text/plain
            /// # - Adjuntar en el CUERPO o BODY el archivo JSON o TXT
            /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++
            
            string json_de_respuesta = SendJson(configuracion.UrlAPISunafact, json_en_utf_8, configuracion.TokenSunafact);
            dynamic r = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            string r2 = JsonConvert.SerializeObject(r, Formatting.Indented);
            dynamic json_r_in = JsonConvert.DeserializeObject<Respuesta>(r2);

            ///#########################################################
            ///#### PASO 4: LEER RESPUESTA DE NUBEFACT ####
            ///+++++++++++++++++++++++++++++++++++++++++++++++++++++++
            ///# Recibirás una respuesta de NUBEFACT inmediatamente lo cual se debe leer, verificando que no haya errores.
            ///# Debes guardar en la base de datos la respuesta que te devolveremos.
            ///# Escríbenos a soporte@nubefact.com o llámanos al teléfono: 01 468 3535 (opción 2) o celular (WhatsApp) 955 598762
            ///# Puedes imprimir el PDF que nosotros generamos como también generar tu propia representación impresa previa coordinación con nosotros.
            ///# La impresión del documento seguirá haciéndose desde tu sistema. Enviaremos el documento por email a tu cliente si así lo indicas en el archivo JSON o TXT.
            ///+++++++++++++++++++++++++++++++++++++++++++++++++++++++

            dynamic leer_respuesta = JsonConvert.DeserializeObject<Respuesta>(json_de_respuesta);
            if (leer_respuesta.errors == null)
            {
                Console.WriteLine(json_r_in);
                Console.WriteLine("TIPO: " + leer_respuesta.tipo);
                Console.WriteLine("SERIE: " + leer_respuesta.serie);
                Console.WriteLine("NUMERO: " + leer_respuesta.numero);
                Console.WriteLine("URL: " + leer_respuesta.url);
                Console.WriteLine("ACEPTADA_POR_SUNAT: " + leer_respuesta.aceptada_por_sunat);
                Console.WriteLine("DESCRIPCION SUNAT: " + leer_respuesta.sunat_description);
                Console.WriteLine("NOTA SUNAT: " + leer_respuesta.sunat_note);
                Console.WriteLine("CODIGO RESPUESTA SUNAT: " + leer_respuesta.sunat_responsecode);
                Console.WriteLine("SUNAT ERROR SOAP: " + leer_respuesta.sunat_soap_error);
                Console.WriteLine("PDF EN BASE64: " + leer_respuesta.pdf_zip_base64);
                Console.WriteLine("XML EN BASE64: " + leer_respuesta.xml_zip_base64);
                Console.WriteLine("CDR EN BASE64: " + leer_respuesta.cdr_zip_base64);
                Console.WriteLine("CODIGO QR: " + leer_respuesta.cadena_para_codigo_qr);
                Console.WriteLine("CODIGO HASH: " + leer_respuesta.codigo_hash);
                Console.WriteLine("CODIGO DE BARRAS: " + leer_respuesta.codigo_de_barras);
            }
            else
            {
                Console.WriteLine("ERRORES: " + leer_respuesta.errors);
            }

            return leer_respuesta;
        }

    }

    public class Invoice
    {
        public string operacion { get; set; }
        public int tipo_de_comprobante { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public int sunat_transaction { get; set; }
        public string cliente_tipo_de_documento { get; set; }
        public string cliente_numero_de_documento { get; set; }
        public string cliente_denominacion { get; set; }
        public string cliente_direccion { get; set; }
        public string cliente_email { get; set; }
        public string cliente_email_1 { get; set; }
        public string cliente_email_2 { get; set; }
        public DateTime fecha_de_emision { get; set; }
        public DateTime fecha_de_vencimiento { get; set; }
        public int moneda { get; set; }
        public dynamic tipo_de_cambio { get; set; } 
        public double porcentaje_de_igv { get; set; }
        public dynamic descuento_global { get; set; }
        public dynamic total_descuento { get; set; }
        public dynamic total_anticipo { get; set; }
        public dynamic total_gravada { get; set; }
        public dynamic total_inafecta { get; set; }
        public dynamic total_exonerada { get; set; }
        public double total_igv { get; set; }
        public dynamic total_gratuita { get; set; }
        public dynamic total_otros_cargos { get; set; }
        public double total { get; set; }
        public dynamic percepcion_tipo { get; set; }
        public dynamic percepcion_base_imponible { get; set; }
        public dynamic total_percepcion { get; set; }
        public dynamic total_incluido_percepcion { get; set; }
        public bool detraccion { get; set; }
        public string observaciones { get; set; }
        public string motivo { get; set; }
        public dynamic documento_que_se_modifica_tipo { get; set; }
        public string documento_que_se_modifica_serie { get; set; }
        public dynamic documento_que_se_modifica_numero { get; set; }
        public dynamic tipo_de_nota_de_credito { get; set; }
        public dynamic tipo_de_nota_de_debito { get; set; }
        public bool enviar_automaticamente_a_la_sunat { get; set; }
        public bool enviar_automaticamente_al_cliente { get; set; }
        public string codigo_unico { get; set; }
        public string condiciones_de_pago { get; set; }
        public string medio_de_pago { get; set; }
        public string placa_vehiculo { get; set; }
        public string orden_compra_servicio { get; set; }
        public string tabla_personalizada_codigo { get; set; }
        public string formato_de_pdf { get; set; }
        public List<Items> items { get; set; }
        public List<Guias> guias { get; set; }
    }

    public class Items
    {
        public string unidad_de_medida { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public double valor_unitario { get; set; }
        public double precio_unitario { get; set; }
        public dynamic descuento { get; set; }
        public double subtotal { get; set; }
        public int tipo_de_igv { get; set; }
        public double igv { get; set; }
        public double total { get; set; }
        public bool anticipo_regularizacion { get; set; }
        public dynamic anticipo_comprobante_serie { get; set; }
        public dynamic anticipo_comprobante_numero { get; set; }
    }

    public class Guias
    {
        public int guia_tipo { get; set; }
        public string guia_serie_numero { get; set; }
    }

    public class Respuesta
    {
        public string errors { get; set; }
        public int tipo { get; set; }
        public string serie { get; set; }
        public int numero { get; set; }
        public string url { get; set; }
        public bool aceptada_por_sunat { get; set; }
        public string sunat_description { get; set; }
        public string sunat_note { get; set; }
        public string sunat_responsecode { get; set; }
        public string sunat_soap_error { get; set; }
        public string pdf_zip_base64 { get; set; }
        public string xml_zip_base64 { get; set; }
        public string cdr_zip_base64 { get; set; }
        public string cadena_para_codigo_qr { get; set; }
        public string codigo_hash { get; set; }
        public string codigo_de_barras { get; set; }
        public string enlace_del_pdf { get; set; }
    }
}