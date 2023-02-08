using AppsfitWebApi.Models;
using AppsfitWebApi.ViewModels;
using E_BusinessLayer.Corporativo;
using E_DataModel.Common;
using E_DataModel.Corporativo;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace AppsfitWebApi.Controllers
{
    [RoutePrefix("api/home")]
    public class HomeController : ApiController
    {
        /// <summary>
        /// BUSCA ALGUN CENTRO FITNESS AFILIADO A APPSFIT, MUESTRA UNA LISTA.
        /// </summary>
        /// <param name="AspNetUsersModel"></param>
        /// <returns>
        /// {
        ///     Status
        ///     Message1
        ///     Message2
        ///     Date
        /// }
        /// </returns>
        [HttpPost]
        [Route("buscarcentrosfitness")]
        public HttpResponseMessage ecommerce_uspObtenerEmpresaPorDominio_AppFitness(AspNetUsersModel oitem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oitem.DefaultKey == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro DefaultKey.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.SubDominio == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro SubDominio.";
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

            List<EmpresaViewEditModel> oItemViewModel = null;

            EmpresaDTO oEmpresaDTO = new EmpresaDTO();
            oEmpresaDTO.DefaultKeyUser = oitem.DefaultKey;
            oEmpresaDTO.SubDominio = oitem.SubDominio;

            ReqFilterEmpresaDTO oReq = new ReqFilterEmpresaDTO()
            {
                FilterCase = filterCaseEmpresa.ecommerce_uspObtenerEmpresaPorDominio_AppFitness,
                Item = oEmpresaDTO,
                User = "appsfit",
                Paging = new Paging()
                {
                    All = true,
                    PageRecords = 5,
                    PageNumber = 1
                }
            };
            RespListEmpresaDTO oResp = new RespListEmpresaDTO();
            using (EmpresaLogic oEmpresaLogic = new EmpresaLogic())
            {
                oResp = oEmpresaLogic.EmpresaGetList(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new List<EmpresaViewEditModel>();

                if (oResp.List.Count > 0)
                {
                    foreach (EmpresaDTO item in oResp.List)
                    {
                        oItemViewModel.Add(new EmpresaViewEditModel()
                        {
                            CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                            CodigoSede = item.CodigoSede,
                            DefaultKeyEmpresa = item.DefaultKeyEmpresa,
                            DefaultKeyUser = oitem.DefaultKey,
                            NombreComercialEmpresa = item.NombreComercialEmpresa,
                            LogoTipo = item.LogoTipo,
                            ColorEmpresa = item.ColorEmpresa,
                            Ubigeo = item.Ubigeo,
                            TieneFacturacionElectronica = item.TieneFacturacionElectronica,
                            TiendaAplicacion = item.TiendaAplicacion,
                            AplicacionDisponible = item.AplicacionDisponible,
                            RutinasAplicacion = item.RutinasAplicacion,
                        });
                    }

                    _objResponseModel.Status = 0;
                    _objResponseModel.Message1 = "Búsqueda realizada correctamente.";
                    _objResponseModel.Message2 = "";
                    _objResponseModel.Date = oItemViewModel;
                }
                else
                {
                    _objResponseModel.Status = 1;
                    _objResponseModel.Message1 = "No hemos encontrado el centro fitness.";
                    _objResponseModel.Message2 = "Vuelve a intentarlo con otro nombre.";
                }
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        /// <summary>
        /// LISTAR CENTRO FITNESS FAVORITO, MUESTRA UNA LISTA.
        /// </summary>
        /// <param name="EmpresaDTO"></param>
        /// <returns>
        /// {
        ///     Status
        ///     Message1
        ///     Message2
        ///     Date
        /// }
        /// </returns>
        [HttpPost]
        [Route("listarcentrosfitnessfavoritos")]
        public HttpResponseMessage appsfit_uspAspNetUsersCentroFit_Listar(EmpresaDTO oitem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oitem.DefaultKeyUser == string.Empty)
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
            List<EmpresaViewEditModel> oItemViewModel = null;

            EmpresaDTO oEmpresaDTO = new EmpresaDTO();
            oEmpresaDTO.DefaultKeyUser = oitem.DefaultKeyUser;

            ReqFilterEmpresaDTO oReq = new ReqFilterEmpresaDTO()
            {
                FilterCase = filterCaseEmpresa.appsfit_uspAspNetUsersCentroFit_Listar,
                Item = oEmpresaDTO,
                User = "appsfit",
                Paging = new Paging()
                {
                    All = true,
                    PageRecords = 5,
                    PageNumber = 1
                }
            };
            RespListEmpresaDTO oResp = new RespListEmpresaDTO();
            using (EmpresaLogic oEmpresaLogic = new EmpresaLogic())
            {
                oResp = oEmpresaLogic.EmpresaGetList(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new List<EmpresaViewEditModel>();

                if (oResp.List.Count > 0)
                {
                    foreach (EmpresaDTO item in oResp.List)
                    {
                        oItemViewModel.Add(new EmpresaViewEditModel()
                        {
                            CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                            CodigoSede = item.CodigoSede,
                            DefaultKeyEmpresa = item.DefaultKeyEmpresa,
                            IdUser = oitem.IdUser,
                            NombreComercialEmpresa = item.NombreComercialEmpresa,
                            LogoTipo = item.LogoTipo,
                            Ubigeo = item.Ubigeo,

                            TiendaAplicacion = item.TiendaAplicacion,
                            AplicacionDisponible = item.AplicacionDisponible,
                            RutinasAplicacion = item.RutinasAplicacion,

                        });
                    }

                    _objResponseModel.Status = 0;
                    _objResponseModel.Message1 = "Proceso realizado correctamente.";
                    _objResponseModel.Message2 = "";
                    _objResponseModel.Date = oItemViewModel;
                }
                else
                {
                    _objResponseModel.Status = 1;
                    _objResponseModel.Message1 = "No hemos encontrado algún centro fitness favorito.";
                    _objResponseModel.Message2 = "Vuelve a intentarlo con otro nombre.";
                }
            }
            else
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpPost]
        [Route("centrosfitness_agregarfavorito")]
        public HttpResponseMessage appsfit_uspAspNetUsersCentroFit_AgregarFavorito(EmpresaDTO oitem)
        {
            ResponseModel _objResponseModel = new ResponseModel();
            bool validadorParametros = true;
            if (oitem.DefaultKeyUser == string.Empty)
            {
                _objResponseModel.Status = 2;
                _objResponseModel.Message1 = "Es obligatorio enviar el parametro DefaultKeyUser.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
                validadorParametros = false;
            }
            else if (oitem.DefaultKeyEmpresa == string.Empty)
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

            List<EmpresaDTO> list = new List<EmpresaDTO>();
            list.Add(new EmpresaDTO()
            {
                DefaultKeyUser = oitem.DefaultKeyUser,
                DefaultKeyEmpresa = oitem.DefaultKeyEmpresa,
                Estado = oitem.Estado,
                Operation = Operation.appsfit_uspAspNetUsersCentroFit_AgregarFavorito
            });

            ReqEmpresaDTO oReq = new ReqEmpresaDTO()
            {
                List = list,
                User = "admin"
            };

            RespEmpresaDTO oResp = null;
            using (EmpresaLogic oEmpresaLogic = new EmpresaLogic())
            {
                oResp = oEmpresaLogic.ExecuteTransac(oReq);
            }

            if (oResp.Success)
            {
                if (oResp.MessageList[0].Codigo == 0)
                {
                    _objResponseModel.Status = 0;
                    if (oitem.Estado == 1)
                    {
                        _objResponseModel.Message1 = "Se agrego a la lista de favoritos.";
                        _objResponseModel.Message2 = "";
                    }
                    else
                    {
                        _objResponseModel.Message1 = "Se borro de la lista de favoritos.";
                        _objResponseModel.Message2 = "";
                    }
                }
                else if (oResp.MessageList[0].Codigo == 1)
                {
                    _objResponseModel.Status = 1;
                    _objResponseModel.Message1 = "DefaultKeyUser incorrecto";
                    _objResponseModel.Message2 = "Debe volver a iniciar sesión.";
                }
                else if (oResp.MessageList[0].Codigo == 2)
                {
                    _objResponseModel.Status = 2;
                    _objResponseModel.Message1 = "No hemos encontrado el centro fitness.";
                    _objResponseModel.Message2 = "Vuelve a intentarlo.";
                }
            }
            else
            {
                _objResponseModel.Status = 3;
                _objResponseModel.Message1 = "Tenemos problemas para traer información, verifica tu conexión con internet.";
                _objResponseModel.Message2 = "Vuelve a intentarlo más tarde.";
            }

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(_objResponseModel));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }
    }
}