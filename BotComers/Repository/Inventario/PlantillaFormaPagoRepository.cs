using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class PlantillaFormaPagoRepository : IDisposable
    {
        public List<PlantillaFormaPagoDTO> ecommerce_uspListarAdminFormaPago(PlantillaFormaPagoDTO request)
        {
            List<PlantillaFormaPagoDTO> lista = new List<PlantillaFormaPagoDTO>();

            ReqFilterPlantillaFormaPagoDTO oReq = new ReqFilterPlantillaFormaPagoDTO()
            {
                Item = new PlantillaFormaPagoDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede
                },
                FilterCase = filterCasePlantillaFormaPago.ecommerce_uspListarAdminFormaPago,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListPlantillaFormaPagoDTO oResp = null;

            using (PlantillaFormaPagoLogic oPlantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = oPlantillaFormaPagoLogic.PlantillaFormaPagoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public PlantillaFormaPagoDTO ecommerce_uspBuscarPlantillaFormaPago(PlantillaFormaPagoDTO request)
        {
            PlantillaFormaPagoDTO oItemViewModel = null;

            PlantillaFormaPagoDTO oPlantillaFormaPagoDTO = new PlantillaFormaPagoDTO();
            oPlantillaFormaPagoDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oPlantillaFormaPagoDTO.CodigoSede = request.CodigoSede;

            ReqFilterPlantillaFormaPagoDTO oReq = new ReqFilterPlantillaFormaPagoDTO()
            {
                FilterCase = filterCasePlantillaFormaPago.ecommerce_uspBuscarFormaPago_MercadoPago,
                Item = oPlantillaFormaPagoDTO,
                User = "admin"
            };
            RespItemPlantillaFormaPagoDTO oResp = null;
            using (PlantillaFormaPagoLogic oPlantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = oPlantillaFormaPagoLogic.PlantillaFormaPagoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new PlantillaFormaPagoDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;

        }

        public string ecommerce_uspRegistrarFormaPago_MercadoPago(PlantillaFormaPagoDTO oItem)
        {
            string mensaje = string.Empty;

            List<PlantillaFormaPagoDTO> list = new List<PlantillaFormaPagoDTO>();

            list.Add(new PlantillaFormaPagoDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                MercadoPago_Publickey = oItem.MercadoPago_Publickey,
                MercadoPago_Accesstoken = oItem.MercadoPago_Accesstoken,
                MercadoPago_Estado = oItem.MercadoPago_Estado,
                Operation = Operation.ecommerce_uspRegistrarFormaPago_MercadoPago
            });

            ReqPlantillaFormaPagoDTO oReq = new ReqPlantillaFormaPagoDTO()
            {
                List = list,
                User = "admin"
            };
            RespPlantillaFormaPagoDTO oResp = null;
            using (PlantillaFormaPagoLogic oPlantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = oPlantillaFormaPagoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }

        public PlantillaFormaPagoDTO ecommerce_uspBuscarFormaPago_Yape(PlantillaFormaPagoDTO request)
        {
            PlantillaFormaPagoDTO oItemViewModel = null;

            PlantillaFormaPagoDTO oPlantillaFormaPagoDTO = new PlantillaFormaPagoDTO();
            oPlantillaFormaPagoDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oPlantillaFormaPagoDTO.CodigoSede = request.CodigoSede;

            ReqFilterPlantillaFormaPagoDTO oReq = new ReqFilterPlantillaFormaPagoDTO()
            {
                FilterCase = filterCasePlantillaFormaPago.ecommerce_uspBuscarFormaPago_Yape,
                Item = oPlantillaFormaPagoDTO,
                User = "appsfit"
            };
            RespItemPlantillaFormaPagoDTO oResp = null;
            using (PlantillaFormaPagoLogic oPlantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = oPlantillaFormaPagoLogic.PlantillaFormaPagoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new PlantillaFormaPagoDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;

        }

        public string ecommerce_uspRegistrarFormaPago_Yape(PlantillaFormaPagoDTO oItem)
        {
            string mensaje = string.Empty;

            List<PlantillaFormaPagoDTO> list = new List<PlantillaFormaPagoDTO>();

            list.Add(new PlantillaFormaPagoDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                Yape_NroCelular = oItem.Yape_NroCelular,
                Yape_CodigoQR = oItem.Yape_CodigoQR,
                Yape_Estado = oItem.Yape_Estado,
                Operation = Operation.ecommerce_uspRegistrarFormaPago_Yape
            });

            ReqPlantillaFormaPagoDTO oReq = new ReqPlantillaFormaPagoDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespPlantillaFormaPagoDTO oResp = null;
            using (PlantillaFormaPagoLogic oPlantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = oPlantillaFormaPagoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }


        public PlantillaFormaPagoDTO ecommerce_uspBuscarFormaPago_ContraEntrega(PlantillaFormaPagoDTO request)
        {
            PlantillaFormaPagoDTO oItemViewModel = null;

            PlantillaFormaPagoDTO oPlantillaFormaPagoDTO = new PlantillaFormaPagoDTO();
            oPlantillaFormaPagoDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oPlantillaFormaPagoDTO.CodigoSede = request.CodigoSede;

            ReqFilterPlantillaFormaPagoDTO oReq = new ReqFilterPlantillaFormaPagoDTO()
            {
                FilterCase = filterCasePlantillaFormaPago.ecommerce_uspBuscarFormaPago_ContraEntrega,
                Item = oPlantillaFormaPagoDTO,
                User = "appsfit"
            };
            RespItemPlantillaFormaPagoDTO oResp = null;
            using (PlantillaFormaPagoLogic oPlantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = oPlantillaFormaPagoLogic.PlantillaFormaPagoGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new PlantillaFormaPagoDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;

        }

        public string ecommerce_uspRegistrarFormaPago_ContraEntrega(PlantillaFormaPagoDTO oItem)
        {
            string mensaje = string.Empty;

            List<PlantillaFormaPagoDTO> list = new List<PlantillaFormaPagoDTO>();

            list.Add(new PlantillaFormaPagoDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                ContraEntrega_InstruccionesCorreo = oItem.ContraEntrega_InstruccionesCorreo,
                ContraEntrega_InstruccionesCheckout = oItem.ContraEntrega_InstruccionesCheckout,
                ContraEntrega_Estado = oItem.ContraEntrega_Estado,
                Operation = Operation.ecommerce_uspRegistrarFormaPago_ContraEntrega
            });

            ReqPlantillaFormaPagoDTO oReq = new ReqPlantillaFormaPagoDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespPlantillaFormaPagoDTO oResp = null;
            using (PlantillaFormaPagoLogic oPlantillaFormaPagoLogic = new PlantillaFormaPagoLogic())
            {
                oResp = oPlantillaFormaPagoLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Datos Guardados Correctamente";
            }

            return mensaje;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}