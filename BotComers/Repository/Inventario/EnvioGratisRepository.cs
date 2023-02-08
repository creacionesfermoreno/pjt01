using E_BusinessLayer;
using E_DataModel;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.Inventario
{
    public class EnvioGratisRepository : IDisposable
    {
        public EnvioGratisDTO ecommerce_uspBuscarEnvioGratis(EnvioGratisDTO request)
        {
            EnvioGratisDTO oItemViewModel = null;

            EnvioGratisDTO oEnvioGratisDTO = new EnvioGratisDTO();
            oEnvioGratisDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oEnvioGratisDTO.CodigoSede = request.CodigoSede;

            ReqFilterEnvioGratisDTO oReq = new ReqFilterEnvioGratisDTO()
            {
                FilterCase = filterCaseEnvioGratis.ecommerce_uspBuscarEnvioGratis,
                Item = oEnvioGratisDTO,
                User = "appsfit"
            };
            RespItemEnvioGratisDTO oResp = null;
            using (EnvioGratisLogic oEnvioGratisLogic = new EnvioGratisLogic())
            {
                oResp = oEnvioGratisLogic.EnvioGratisGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new EnvioGratisDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;

        }

        public EnvioGratisDTO ecommerce_uspBuscarEnvioGratisXtienda(EnvioGratisDTO request)
        {
            EnvioGratisDTO oItemViewModel = null;

            EnvioGratisDTO oEnvioGratisDTO = new EnvioGratisDTO();
            oEnvioGratisDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oEnvioGratisDTO.CodigoSede = request.CodigoSede;

            ReqFilterEnvioGratisDTO oReq = new ReqFilterEnvioGratisDTO()
            {
                FilterCase = filterCaseEnvioGratis.ecommerce_uspBuscarEnvioGratisXtienda,
                Item = oEnvioGratisDTO,
                User = "appsfit"
            };
            RespItemEnvioGratisDTO oResp = null;
            using (EnvioGratisLogic oEnvioGratisLogic = new EnvioGratisLogic())
            {
                oResp = oEnvioGratisLogic.EnvioGratisGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new EnvioGratisDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;

        }

        public string ecommerce_uspRegistrarEnvioGratis(EnvioGratisDTO oItem)
        {
            string mensaje = string.Empty;

            List<EnvioGratisDTO> list = new List<EnvioGratisDTO>();

            list.Add(new EnvioGratisDTO()
            {
                CodigoUnidadNegocio = oItem.CodigoUnidadNegocio,
                CodigoSede = oItem.CodigoSede,
                Valor = oItem.Valor,
                FechaInicio = oItem.FechaInicio,
                FechaFin = oItem.FechaFin,
                Estado = oItem.Estado,
                Operation = Operation.Create
            });

            ReqEnvioGratisDTO oReq = new ReqEnvioGratisDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespEnvioGratisDTO oResp = null;
            using (EnvioGratisLogic oEnvioGratisLogic = new EnvioGratisLogic())
            {
                oResp = oEnvioGratisLogic.ExecuteTransac(oReq);
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