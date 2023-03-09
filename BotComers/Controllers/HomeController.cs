using BotComers.Helpers;
using BotComers.Models;
using BotComers.Repository;
using BotComers.Repository.PasarelaEmpresaServices;
using BotComers.ViewModels;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using MercadoPago;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BotComers.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }



        //upload files
        public ActionResult uploadFile()
        {
            ResponseModel response = new ResponseModel();

            HomeRepository repo = new HomeRepository();
            HttpFileCollectionBase files = Request.Files;

            if (files.Count > 0)
            {
                ClienteViewModel fi = new ClienteViewModel();
                fi.ImageFile = (HttpPostedFileWrapper)files[0];

                var upload = repo.uploadFileRepo(fi, "notisimages");
                if (!upload.Success)
                {
                    response.Success = false;
                    response.Message1 = "No se pudo subir la imagen";
                }
                else
                {
                    response.Success = true;
                    response.Message1 = upload.Message1;
                }
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }


        //********************************************************** PLANES ********************************************

        //list planes for pasarelas
        public async Task<ActionResult> getPlanesPasarela(string code)
        {
            ResponseModel response = new ResponseModel();
            PasarelaRepository pasarepo = new PasarelaRepository();

            var account = pasarepo.getItemAccount(code);

            if (!string.IsNullOrEmpty(account.Valor1) && !string.IsNullOrEmpty(account.Valor2))
            {
                string type = account.DesFormaPago.ToUpper();
                PasarelaEmpresaService pemserv = new PasarelaEmpresaService();
                List<PlanHome> plans = new List<PlanHome>();
                switch (type)
                {
                    case "CULQI":
                        var data = await pemserv.PlanesCulqiServ(account.Valor2);
                        var plansData = (List<Datum>)data.Date;
                        foreach (var itemx in plansData)
                        {
                            plans.Add(new PlanHome() { Id = itemx.id, Name = itemx.name, Price = itemx.amount });
                        }
                        response.Date = plans;
                        response.Success = true;
                        break;
                    default:
                        break;
                }


            }
            else
            {
                response.Success = false;
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }



        //register plan
        public ActionResult RegisterPlanPasarela(int paquete = 0, string codepago = "", string idplan = "",string descripcion = "")
        {
            ResponseModel response = new ResponseModel();
            if (paquete == 0 || string.IsNullOrEmpty(codepago) || string.IsNullOrEmpty(idplan))
            {
                response.Message1 = "Campos requeridos";
                response.Status = 1;
                response.Success = false;
            }
            else
            {
                List<PlanesDTO> list = new List<PlanesDTO>();

                list.Add(new PlanesDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    UsuarioCreacion = Commun.Usuario,
                    CodigoPaquete = paquete,
                    CodigoPaqueteSuscripcion = "0",
                    CodigoPlantillaFormaPago = codepago,
                    IdSuscripcionPasarela = idplan,
                    Descripcion = descripcion,
                    Operation = E_DataModel.Common.Operation.PlanSuscriptionRegister,
                }); ;
                ReqPlanesDTO oReq = new ReqPlanesDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespPlanesDTO oResp = null;
                using (PlanesLogic logic = new PlanesLogic())
                {
                    oResp = logic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    response.Message1 = oResp.MessageList[0].Detalle;
                    response.Status = 0;
                    response.Success = true;
                }
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }



        //list plan by paquete
        public ActionResult GetPlanPasarelaPaquete(int paquete = 0)
        {
            List<PlanesDTO> lista = new List<PlanesDTO>();

            PlanesDTO oDto = new PlanesDTO();
            oDto.CodigoUnidadNegocio = Commun.CodigoUnidadNegocio;
            oDto.CodigoSede = Commun.CodigoSede;
            oDto.CodigoPaquete= paquete;

            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()

            {
                FilterCase = filterCasePlanes.ListPlanPasarelaByPaquete,
                Item = oDto,
                User = Commun.Usuario,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 99999
                }
            };

            RespListPlanesDTO oResp = null;
            using (PlanesLogic oLogic = new PlanesLogic())
            {
                oResp = oLogic.PlanesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }



        //destroy plan by paquete
        public ActionResult DestroyPlanByPaquete(int paquete = 0,string codesuscripcion = "")
        {
            ResponseModel _objResponseModel = new ResponseModel();
            if (paquete == 0 || string.IsNullOrEmpty(codesuscripcion))
            {
                _objResponseModel.Success = false;
                _objResponseModel.Message1 = "Campos requeridos";
            }
            else
            {
                List<PlanesDTO> list = new List<PlanesDTO>();

                list.Add(new PlanesDTO()
                {
                    CodigoUnidadNegocio = Commun.CodigoUnidadNegocio,
                    CodigoSede = Commun.CodigoSede,
                    CodigoPaquete = paquete,
                    CodigoPaqueteSuscripcion = codesuscripcion,
                    Operation = Operation.PlanSuscriptionDestroy,
                });
                ReqPlanesDTO oReq = new ReqPlanesDTO()
                {
                    List = list,
                    User = Commun.Usuario
                };

                RespPlanesDTO oResp = null;
                using (PlanesLogic logic = new PlanesLogic())
                {
                    oResp = logic.ExecuteTransac(oReq);
                }
                if (oResp.Success)
                {
                    _objResponseModel.Message1 = oResp.MessageList[0].Detalle;
                    _objResponseModel.Status = 0;
                    _objResponseModel.Success = true;
                }
            }
            return Json(_objResponseModel, JsonRequestBehavior.AllowGet);
        }

        //********************************************************** PLANES ********************************************


    }
}