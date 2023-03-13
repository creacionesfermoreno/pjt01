using AppsfitWebApi.Models;
using AppsfitWebApi.Repository.Services;
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Corporativo;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AppsfitWebApi.Repository
{
    public class HomeRepository
    {

        //validate Pasarela
        public async Task<ResponseApi> ValidPasarelaRepo(string DefaultKeyEmpresa, string CodigoPlantillaFormaPago)
        {
            ResponseApi responseModel = new ResponseApi();
            PaypalService paypalService = new PaypalService();

            //search acount pasarela
            var pasarela = GetItemAccountPaymentsRepo(DefaultKeyEmpresa, CodigoPlantillaFormaPago);
            if (string.IsNullOrEmpty(pasarela.CodigoPlantillaFormaPago) || pasarela.CodigoPlantillaFormaPago == "0" || string.IsNullOrEmpty(pasarela.DesFormaPago))
            {
                responseModel.Message1 = "No tiene una cuenta de pasarela registrada";
                responseModel.Status = 1;
                responseModel.Success = false;
            }
            else
            {
                string type = pasarela?.DesFormaPago;
                responseModel.Message3 = type;
                switch (type.ToUpper())
                {
                    case "PAYPAL":
                        responseModel = await paypalService.PaypalTokenService(pasarela?.Valor1, pasarela?.Valor2, pasarela.EstadoProduccion);
                        responseModel.Production = pasarela.EstadoProduccion;
                        break;

                    case "CULQI":
                        responseModel.Message1 = pasarela.Valor1;
                        responseModel.Message2 = pasarela.Valor2;
                        responseModel.Success = true;
                        break;

                    case "MERCADO PAGO":
                        responseModel.Message1 = pasarela?.Valor2;
                        responseModel.Message2 = pasarela?.Valor3;
                        responseModel.Success = true;
                        break;

                    default:
                        break;
                }
            }
            return responseModel;
        }


        //list pasarela by keyempresa
        public ResponseApi AccountPaymentsRepo(string DefaultKeyEmpresa)
        {
            ResponseApi responseApi = new ResponseApi();
            try
            {
                List<PasarelaEmpresaDTO> list = new List<PasarelaEmpresaDTO>();
                PasarelaEmpresaDTO oPasarelaEmpresaDTO = new PasarelaEmpresaDTO();
                oPasarelaEmpresaDTO.DefaultKeyEmpresa = DefaultKeyEmpresa;
                ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()
                {
                    FilterCase = FilterCasePasarelaEmpresa.ListApi,
                    Item = oPasarelaEmpresaDTO,
                    User = "Admin",
                    Paging = new E_DataModel.Common.Paging()
                    {
                        All = true,
                        PageNumber = 0,
                        PageRecords = 9999
                    }
                };
                RespListPasarelaEmpresaDTO oResp = null;
                using (PasarelaEmpresaLogic ologic = new PasarelaEmpresaLogic())
                {
                    oResp = ologic.GetList(oReq);
                }
                if (oResp.Success)
                {
                    responseApi.Date = oResp.List;
                    responseApi.Success = true;
                }
            }
            catch (Exception ex)
            {
                responseApi.Status = 1;
                responseApi.Message1 = ex.Message;
            }
            return responseApi;
        }



        //get account payment by code
        public PasarelaEmpresaDTO GetItemAccountPaymentsRepo(string DefaultKeyEmpresa, string CodigoPlantillaFormaPago)
        {
            PasarelaEmpresaDTO oEmpresaDTO = new PasarelaEmpresaDTO();
            oEmpresaDTO.DefaultKeyEmpresa = DefaultKeyEmpresa;
            oEmpresaDTO.CodigoPlantillaFormaPago = CodigoPlantillaFormaPago;

            ReqFilterPasarelaEmpresaDTO oReq = new ReqFilterPasarelaEmpresaDTO()
            {
                FilterCase = FilterCasePasarelaEmpresa.SearchCodeApi,
                Item = oEmpresaDTO,
                User = "Admin",
            };
            RespItemPasarelaEmpresaDTO oResp = null;
            using (PasarelaEmpresaLogic oLogic = new PasarelaEmpresaLogic())
            {
                oResp = oLogic.GetItem(oReq);
            }
            if (oResp.Success)
            {
                oEmpresaDTO = oResp.Item;
            }
            return oEmpresaDTO;
        }


        //register table suscription

        public bool RegisterSuscriptionMembresia(PlanesDTO item)
        {
            bool register = false;
            List<PlanesDTO> list = new List<PlanesDTO>();

            list.Add(new PlanesDTO()
            {
                CodigoMembresiasSuscripcion = "0",
                CodigoUnidadNegocio = item.CodigoUnidadNegocio,
                CodigoSede = item.CodigoSede,
                DefaultKeyEmpresa = item.DefaultKeyEmpresa,
                DefaultKeyUser = item.DefaultKeyUser,
                CodigoPlantillaFormaPago = item.CodigoPlantillaFormaPago,
                IdClientePasarela = item.IdClientePasarela,
                IdSuscripcionPasarela = item.IdSuscripcionPasarela,
                DataJsonPasarela = item.DataJsonPasarela,
                CodigoSocio = item.CodigoSocio,
                NroDocumento = item.NroDocumento,
                CodigoPaquete = item.CodigoPaquete,
                UsuarioCreacion = "appsFit",
                Operation = Operation.RegisterMembSucription,
            });
            ReqPlanesDTO oReq = new ReqPlanesDTO()
            {
                List = list,
                User = "app",
            };
            RespPlanesDTO oResp = null;
            using (PlanesLogic oLogic = new PlanesLogic())
            {
                oResp = oLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                register = true;
            }
            return register;
        }



        //list suscription membresia by defaultkeyuser
        public List<PlanesDTO> ListMembSuscriptionDefaultKUserRepo(string DefaultKeyUser)
        {
            List<PlanesDTO> lista = null;
            PlanesDTO oPlanDto = new PlanesDTO();
            oPlanDto.DefaultKeyUser = DefaultKeyUser;


            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.ListMemSuscriptionDkUser,
                Item = oPlanDto,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin",
            };

            RespListPlanesDTO oResp = null;
            using (PlanesLogic oLogic = new PlanesLogic())
            {
                oResp = oLogic.PlanesGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = new List<PlanesDTO>();
                lista = oResp.List;
            }
            return lista;
        }



        //Membresia suscription by idsuscription

        public PlanesDTO getMembresiaSuscriptionByIdSuscription(string idSubscription)
        {
            PlanesDTO oDto = new PlanesDTO();
            oDto.IdSuscripcionPasarela = idSubscription;

            ReqFilterPlanesDTO oReq = new ReqFilterPlanesDTO()
            {
                FilterCase = filterCasePlanes.getItemMemSucriptionByIdSucription,
                Item = oDto,
                User = "Admin",
            };
            RespItemPlanesDTO oResp = null;
            using (PlanesLogic oLogic = new PlanesLogic())
            {
                oResp = oLogic.PlanesGetItem(oReq);
            }
            if (oResp.Success)
            {
                oDto = oResp.Item;
            }
            return oDto;
        }


    }
}