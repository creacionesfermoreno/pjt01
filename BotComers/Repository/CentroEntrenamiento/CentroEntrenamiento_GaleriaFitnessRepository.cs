using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_GaleriaFitnessRepository : IDisposable
    {

        public List<CentroEntrenamiento_GaleriaFitnessDTO> CentroEntrenamiento_uspListarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO request)
        {
            List<CentroEntrenamiento_GaleriaFitnessDTO> lista = null;

            ReqFilterCentroEntrenamiento_GaleriaFitnessDTO oReq = new ReqFilterCentroEntrenamiento_GaleriaFitnessDTO()
            {
                Item = new CentroEntrenamiento_GaleriaFitnessDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Privacidad = request.Privacidad,
                    Tipo = request.Tipo
                },
                FilterCase = filterCaseCentroEntrenamiento_GaleriaFitness.CentroEntrenamiento_uspListarGaleriaFitness,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_GaleriaFitnessDTO oResp = null;

            using (CentroEntrenamiento_GaleriaFitnessLogic oCentroEntrenamiento_GaleriaFitnessLogic = new CentroEntrenamiento_GaleriaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_GaleriaFitnessLogic.ResponseGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_GaleriaFitnessDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public string CentroEntrenamiento_uspRegistrarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO request)
        {
            string mensaje = string.Empty;

            List<CentroEntrenamiento_GaleriaFitnessDTO> list = new List<CentroEntrenamiento_GaleriaFitnessDTO>();

            list.Add(new CentroEntrenamiento_GaleriaFitnessDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                Codigo = request.Codigo,
                Tipo = request.Tipo,
                Privacidad = request.Privacidad,
                UrlImagen = request.UrlImagen,
                Estado = request.Estado,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = Operation.Create
            });

            ReqCentroEntrenamiento_GaleriaFitnessDTO oReq = new ReqCentroEntrenamiento_GaleriaFitnessDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_GaleriaFitnessDTO oResp = null;
            using (CentroEntrenamiento_GaleriaFitnessLogic oCentroEntrenamiento_GaleriaFitnessLogic = new CentroEntrenamiento_GaleriaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_GaleriaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }

            return mensaje;
        }

        public void CentroEntrenamiento_uspActualizarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO request)
        {
            List<CentroEntrenamiento_GaleriaFitnessDTO> list = new List<CentroEntrenamiento_GaleriaFitnessDTO>();

            list.Add(new CentroEntrenamiento_GaleriaFitnessDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                Codigo = request.Codigo,
                UrlImagen = request.UrlImagen,
                Operation = Operation.Update
            });

            ReqCentroEntrenamiento_GaleriaFitnessDTO oReq = new ReqCentroEntrenamiento_GaleriaFitnessDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_GaleriaFitnessDTO oResp = null;
            using (CentroEntrenamiento_GaleriaFitnessLogic oCentroEntrenamiento_GaleriaFitnessLogic = new CentroEntrenamiento_GaleriaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_GaleriaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {

            }
        }

        public string CentroEntrenamiento_uspEliminarGaleriaFitness(CentroEntrenamiento_GaleriaFitnessDTO request)
        {
            string mensaje = string.Empty;

            List<CentroEntrenamiento_GaleriaFitnessDTO> list = new List<CentroEntrenamiento_GaleriaFitnessDTO>();

            list.Add(new CentroEntrenamiento_GaleriaFitnessDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                Codigo = request.Codigo,
                UrlImagen = request.UrlImagen,
                Operation = Operation.Delete
            });

            ReqCentroEntrenamiento_GaleriaFitnessDTO oReq = new ReqCentroEntrenamiento_GaleriaFitnessDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_GaleriaFitnessDTO oResp = null;
            using (CentroEntrenamiento_GaleriaFitnessLogic oCentroEntrenamiento_GaleriaFitnessLogic = new CentroEntrenamiento_GaleriaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_GaleriaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "Excelente";
            }

            return mensaje;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}