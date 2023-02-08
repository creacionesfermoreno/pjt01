using System.Web.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;


using E_BusinessLayer.Corporativo;
using E_DataModel.Common;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;

using AppsfitWebApi.Models;
using System;

using AppsfitWebApi.Repository;

namespace AppsfitWebApi.Controllers
{

    [RoutePrefix("api/estate")]
    public class EstateController : ApiController
    {

        [HttpPost]
        [Route("listarperfil")]
        public HttpResponseMessage CentroEntrenamiento_uspBuscarAspNetUsers_imprimirticket_DefaultKey(CentroEntrenamiento_AspNetUsersDTO oItem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oItem.DefaultKeyUser == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro DefaultKeyUser.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            CentroEntrenamiento_AspNetUsersDTO oCentroEntrenamiento_AspNetUsersDTO = new CentroEntrenamiento_AspNetUsersDTO();
            oCentroEntrenamiento_AspNetUsersDTO.DefaultKeyUser = oItem.DefaultKeyUser;
            
            ReqFilterCentroEntrenamiento_AspNetUsersDTO oReq = new ReqFilterCentroEntrenamiento_AspNetUsersDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_AspNetUsers.CentroEntrenamiento_uspBuscarAspNetUsers_imprimirticket_DefaultKey,
                User = "appsfit",
                Item = oCentroEntrenamiento_AspNetUsersDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_AspNetUsersDTO oResp = null;

            using (CentroEntrenamiento_AspNetUsersLogic oMenbresiasLogic = new CentroEntrenamiento_AspNetUsersLogic())
            {
                oResp = oMenbresiasLogic.ResponseGetItem(oReq);
            }

            if (oResp.Success)
            {
                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Consulta realizada correctamente.";
                _objResponseModel.Message2 = "";
                _objResponseModel.Date = oResp.Item;
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Message2 = "";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpPost]
        [Route("listarmembresiassocio")]
        public HttpResponseMessage appsfit_uspListarMembresiasSocios(ContratoDTO oItem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oItem.DefaultKeyUser == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro DefaultKeyUser.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.DefaultKeyEmpresa == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro DefaultKeyEmpresa.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.DefaultKeyUser = oItem.DefaultKeyUser;
            oContratoDTO.DefaultKeyEmpresa = oItem.DefaultKeyEmpresa;            

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.appsfit_uspListarMembresiasSocios,
                User = "appsfit",
                Item = oContratoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {                
                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Consulta realizada correctamente.";
                _objResponseModel.Message2 = "";
                _objResponseModel.Date = oResp.List;
            }
            else
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Message2 = "";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpPost]
        [Route("listarmedidas")]
        public HttpResponseMessage uspListarControlMedidas_Paginacion(ContratoDTO oItem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oItem.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.CodigoSocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }

            ControlMedidasDTO oControlMedidasDTO = new ControlMedidasDTO();
            oControlMedidasDTO.CodigoUnidadNegocio = oItem.CodigoUnidadNegocio;
            oControlMedidasDTO.CodigoSede = oItem.CodigoSede;
            oControlMedidasDTO.CodigoCliente = oItem.CodigoSocio;

            ReqFilterControlMedidasDTO oReq = new ReqFilterControlMedidasDTO()
            {
                FilterCase = filterCaseControlMedidas.uspListarControlMedidas_Paginacion,
                Item = oControlMedidasDTO,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageRecords = 0,
                    PageNumber = System.Convert.ToUInt32(1)
                }
            };

            RespListControlMedidasDTO oResp = null;

            using (ControlMedidasLogic oControlMedidasLogic = new ControlMedidasLogic())
            {
                oResp = oControlMedidasLogic.ControlMedidasGetList(oReq);
            }

            if (oResp.Success)
            {
                List<MedidasCliente> clienteList = new List<MedidasCliente>();
                for (int i = 0; i < oResp.List.Count; i++)
                {
                    MedidasCliente itemPlantilla = new MedidasCliente();
                    itemPlantilla.nombre = "Objetivo: " + oResp.List[i].Observacion;
                    itemPlantilla.valor = "Evaluación: " + oResp.List[i].strFechaIngreso;
                    itemPlantilla.texto_color = "#000";
                    itemPlantilla.texto_negrita = true;
                    itemPlantilla.list = new List<MedidasCliente>();

                    itemPlantilla.list.Add(new MedidasCliente() { 
                        nombre = "Diagnostico",
                        valor = oResp.List[i].Comentario,
                        texto_color = "#000",
                        texto_negrita = true
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Antecedentes Medicos",
                        valor = oResp.List[i].AntecedentesMedicos,
                        texto_color = "#000",
                        texto_negrita = true
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Experiencia Entrenando",
                        valor = oResp.List[i].ExpEntrenamiento,
                        texto_color = "#000",
                        texto_negrita = true
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Objetivos",
                        valor = oResp.List[i].Observacion,
                        texto_color = "#000",
                        texto_negrita = true
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Edad",
                        valor = oResp.List[i].Edad.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Estatura",
                        valor = oResp.List[i].Estatura.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Peso Corporal",
                        valor = oResp.List[i].PesoCorporal.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Peso Graso",
                        valor = oResp.List[i].PesoGraso.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "% Grasa",
                        valor = oResp.List[i].PorcentajeGrasa.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "% Agua",
                        valor = oResp.List[i].PorcentajeAgua.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Grasa Visceral",
                        valor = oResp.List[i].GrasaVisceral.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "IMC",
                        valor = oResp.List[i].IMC.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Cuello",
                        valor = oResp.List[i].Cuello.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Circunferencia del Torax",
                        valor = oResp.List[i].CirdelTorax.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Cintura",
                        valor = oResp.List[i].Cintura.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Cintura baja",
                        valor = oResp.List[i].CadA.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Cadera",
                        valor = oResp.List[i].CadB.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Muslo",
                        valor = oResp.List[i].MusloSuperior.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Pantorrilla",
                        valor = oResp.List[i].MusloBajo.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Brazo Relajado",
                        valor = oResp.List[i].BrazoNormal.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Brazo Contraído",
                        valor = oResp.List[i].BrazoFlexionado.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Muñeca",
                        valor = oResp.List[i].Munieca.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    itemPlantilla.list.Add(new MedidasCliente()
                    {
                        nombre = "Gluteos",
                        valor = oResp.List[i].Gluteos.ToString(),
                        texto_color = "#000",
                        texto_negrita = false
                    });
                    clienteList.Add(itemPlantilla); 

                }

                _objResponseModel.Status = 0;
                _objResponseModel.Message1 = "Consulta realizada correctamente.";
                _objResponseModel.Message2 = "";
                _objResponseModel.Date = clienteList;
            }
            else
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                _objResponseModel.Message2 = "";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpPost]
        [Route("registerfreezing")]
        public HttpResponseMessage registerfreezing(FreezingModel oItem)
        {
            ResponseModel _objResponseModel = new ResponseModel();

            bool validadorParametros = true;
            if (oItem.CodigoUnidadNegocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoUnidadNegocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.CodigoSede == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSede.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.CodigoSocio == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro CodigoSocio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.User == "")
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Usuario.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            else if (oItem.fechaInicio == null)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro fechaInicio.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.fechaFin == null)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro fechaFin.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.FrezenDisponibles == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro FrezenDisponibles.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.NroDiasCongelar == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro NroDiasCongelar.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.fechaFreziing == null)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro fechaFreziing.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.fechaDesFreziing == null)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro fechaDesFreziing.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.NroDias == 0)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro NroDias.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oItem.Motivo == "")
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro Motivo.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }

            if (!validadorParametros)
            {
                var response_validador = new HttpResponseMessage(HttpStatusCode.OK);
                response_validador.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
                response_validador.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response_validador;
            }
            try
            {
                using (CheckingRepository oRepository = new CheckingRepository())
                {
                    var res = oRepository.GuardarMembresiaCongelamiento(oItem.CodigoUnidadNegocio, oItem.codigo, oItem.fechaInicio, oItem.fechaFin, oItem.FrezenDisponibles, oItem.NroDiasCongelar, oItem.fechaFreziing, oItem.fechaDesFreziing, oItem.CodigoSede, oItem.CodigoSocio, oItem.NroDias, oItem.NroSolicitud, oItem.Motivo, oItem.User);
                   
                    if (res > 0)
                    {
                        _objResponseModel.Status = 0;
                        _objResponseModel.Message1 = "La membresia ha sido congelada correctamente.";

                    }
                    else
                    {
                        _objResponseModel.Status = 1;
                        _objResponseModel.Message1 = "No se ha podido guardar correctamente";
                    }
                    
                }
            }
            catch(Exception ex)
            {
                _objResponseModel.Status = 1;
                _objResponseModel.Message1 = ex.Message.ToString();
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

    }


    public class FreezingModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int codigo { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }

        public int FrezenDisponibles { get; set; }
        public int NroDiasCongelar { get; set; }

        public DateTime fechaFreziing { get; set; }
        public DateTime fechaDesFreziing { get; set; }

        public int CodigoSocio { get; set; }
        public int NroDias { get; set; }
        public string NroSolicitud { get; set; }
        public string Motivo { get; set; }
        public string User { get; set; }
    }


    public class MedidasCliente
    {
        public string nombre { get; set; }
        public string valor { get; set; }        
        public string texto_color { get; set; }
        public bool texto_negrita { get; set; }
        public List<MedidasCliente> list { get; set; }

    }


}
