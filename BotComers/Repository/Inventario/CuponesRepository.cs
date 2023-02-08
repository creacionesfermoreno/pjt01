using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class CuponesRepository : IDisposable
    {
        public List<CuponesDTO> ecommerce_uspListar_Cupones(CuponesDTO request)
        {
            List<CuponesDTO> lista = new List<CuponesDTO>();

            ReqFilterCuponesDTO oReq = new ReqFilterCuponesDTO()
            {
                Item = new CuponesDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede
                },
                FilterCase = filterCaseCupones.ecommerce_uspListar_Cupones,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCuponesDTO oResp = null;

            using (CuponesLogic oCuponesLogic = new CuponesLogic())
            {
                oResp = oCuponesLogic.CuponesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public CuponesDTO ecommerce_uspBuscar_Cupones(CuponesDTO request)
        {
            CuponesDTO oItem = new CuponesDTO();

            ReqFilterCuponesDTO oReq = new ReqFilterCuponesDTO()
            {
                Item = new CuponesDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoCupon = request.CodigoCupon
                },
                FilterCase = filterCaseCupones.ecommerce_uspBuscar_Cupones,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCuponesDTO oResp = null;

            using (CuponesLogic oCuponesLogic = new CuponesLogic())
            {
                oResp = oCuponesLogic.CuponesGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return oItem;
        }

        public string ecommerce_uspBuscar_CuponesXCodigoPromocion(CuponesDTO request)
        {
            string resultado = string.Empty;

            CuponesDTO oItem = new CuponesDTO();

            ReqFilterCuponesDTO oReq = new ReqFilterCuponesDTO()
            {
                Item = new CuponesDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoPromocion = request.CodigoPromocion
                },
                FilterCase = filterCaseCupones.ecommerce_uspBuscar_CuponesXCodigoPromocion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCuponesDTO oResp = null;

            using (CuponesLogic oCuponesLogic = new CuponesLogic())
            {
                oResp = oCuponesLogic.CuponesGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
                if (oResp.Item != null)
                {
                    resultado = oResp.Item.Valor + "|" + oResp.Item.TipoCupon + "|" + oResp.Item.CodigoCupon;
                }
                else
                {
                    resultado = "sin resultados";
                }
            }
            return resultado;
        }

        public string ecommerce_uspRegistrar_Cupones(CuponesDTO oItem)
        {
            string mensaje = string.Empty;

            List<CuponesDTO> list = new List<CuponesDTO>();

            list.Add(new CuponesDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoCupon = oItem.CodigoCupon == null ? string.Empty : oItem.CodigoCupon,
                CodigoPromocion = oItem.CodigoPromocion,
                TipoCupon = oItem.TipoCupon,
                Valor = oItem.Valor,
                FechaInicio = oItem.FechaInicio,
                FechaFin = oItem.FechaFin,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update
            });

            ReqCuponesDTO oReq = new ReqCuponesDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCuponesDTO oResp = null;
            using (CuponesLogic oCuponesLogic = new CuponesLogic())
            {
                oResp = oCuponesLogic.ExecuteTransac(oReq);
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