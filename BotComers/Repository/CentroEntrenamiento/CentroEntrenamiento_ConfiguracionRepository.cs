using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_ConfiguracionRepository : IDisposable
    {

        public ConfiguracionDTO CentroEntrenamiento_uspBuscarEmpresa_imprimirticket(ConfiguracionDTO request)
        {
            ConfiguracionDTO oItemViewModel = null;

            ConfiguracionDTO oConfiguracionDTO = new ConfiguracionDTO();
            oConfiguracionDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oConfiguracionDTO.CodigoSede = request.CodigoSede;

            ReqFilterConfiguracionDTO oReq = new ReqFilterConfiguracionDTO()
            {
                FilterCase = filterCaseConfiguracion.CentroEntrenamiento_uspBuscarEmpresa_imprimirticket,
                Item = oConfiguracionDTO,
                User = "appsfit"
            };
            RespItemConfiguracionDTO oResp = null;
            using (ConfiguracionLogic oConfiguracionLogic = new ConfiguracionLogic())
            {
                oResp = oConfiguracionLogic.ConfiguracionGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new ConfiguracionDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}