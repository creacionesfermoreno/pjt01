using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class TarifasEnvioRepository : IDisposable
    {
        public List<TarifasEnvioDTO> ecommerce_uspListar_TarifasEnvio(TarifasEnvioDTO request)
        {
            List<TarifasEnvioDTO> lista = new List<TarifasEnvioDTO>();

            ReqFilterTarifasEnvioDTO oReq = new ReqFilterTarifasEnvioDTO()
            {
                Item = new TarifasEnvioDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    TipoUbigeo = request.TipoUbigeo,
                    Ubigeo = request.Ubigeo
                },
                FilterCase = filterCaseTarifasEnvio.ecommerce_uspListarAdminTarifasEnvio,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListTarifasEnvioDTO oResp = null;

            using (TarifasEnvioLogic oTarifasEnvioLogic = new TarifasEnvioLogic())
            {
                oResp = oTarifasEnvioLogic.TarifasEnvioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public string ecommerce_uspRegistrarAdminTarifasEnvio(TarifasEnvioDTO oItem)
        {
            string mensaje = string.Empty;

            List<TarifasEnvioDTO> list = new List<TarifasEnvioDTO>();

            list.Add(new TarifasEnvioDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                CodigoTarifaEnvio = oItem.CodigoTarifaEnvio,
                CodigoUbigeo = oItem.CodigoUbigeo,
                Ubigeo = oItem.Ubigeo,
                PrecioEnvio = oItem.PrecioEnvio,
                TiempoEntrega = oItem.TiempoEntrega,
                TipoTiempoEntrega = oItem.TipoTiempoEntrega,
                Estado = oItem.Estado,
                UsuarioCreacion = oItem.UsuarioCreacion,
                Operation = oItem.Accion == "N" ? Operation.Create : Operation.Update
            });

            ReqTarifasEnvioDTO oReq = new ReqTarifasEnvioDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespTarifasEnvioDTO oResp = null;
            using (TarifasEnvioLogic oTarifasEnvioLogic = new TarifasEnvioLogic())
            {
                oResp = oTarifasEnvioLogic.ExecuteTransac(oReq);
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