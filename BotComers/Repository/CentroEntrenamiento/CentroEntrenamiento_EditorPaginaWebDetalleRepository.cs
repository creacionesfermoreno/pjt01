using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_EditorPaginaWebDetalleRepository : IDisposable
    {

        public CentroEntrenamiento_EditorPaginaWebDetalleDTO ecommerce_uspBuscarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            CentroEntrenamiento_EditorPaginaWebDetalleDTO oItemViewModel = null;

            CentroEntrenamiento_EditorPaginaWebDetalleDTO oCentroEntrenamiento_EditorPaginaWebDetalleDTO = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
            oCentroEntrenamiento_EditorPaginaWebDetalleDTO.CodigoUnidadNegocio = request.CodigoUnidadNegocio;
            oCentroEntrenamiento_EditorPaginaWebDetalleDTO.CodigoSede = request.CodigoSede;
            oCentroEntrenamiento_EditorPaginaWebDetalleDTO.Codigo = request.Codigo;

            ReqFilterCentroEntrenamiento_EditorPaginaWebDetalleDTO oReq = new ReqFilterCentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                FilterCase = filterCaseCentroEntrenamiento_EditorPaginaWebDetalle.ecommerce_uspBuscarEdicionPaginaWebDetalle,
                Item = oCentroEntrenamiento_EditorPaginaWebDetalleDTO,
                User = "appsfit"
            };
            RespItemCentroEntrenamiento_EditorPaginaWebDetalleDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebDetalleLogic oCentroEntrenamiento_EditorPaginaWebDetalleLogic = new CentroEntrenamiento_EditorPaginaWebDetalleLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebDetalleLogic.ResponseGetItem(oReq);
            }
            if (oResp.Success)
            {
                oItemViewModel = new CentroEntrenamiento_EditorPaginaWebDetalleDTO();
                oItemViewModel = oResp.Item;
            }

            return oItemViewModel;
        }

        public List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> ecommerce_uspListarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> lista = null;

            ReqFilterCentroEntrenamiento_EditorPaginaWebDetalleDTO oReq = new ReqFilterCentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                Item = new CentroEntrenamiento_EditorPaginaWebDetalleDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Tipo = request.Tipo
                },
                FilterCase = filterCaseCentroEntrenamiento_EditorPaginaWebDetalle.ecommerce_uspListarEdicionPaginaWebDetalle,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_EditorPaginaWebDetalleDTO oResp = null;

            using (CentroEntrenamiento_EditorPaginaWebDetalleLogic oCentroEntrenamiento_EditorPaginaWebDetalleLogic = new CentroEntrenamiento_EditorPaginaWebDetalleLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebDetalleLogic.ResponseGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_EditorPaginaWebDetalleDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public string ecommerce_uspRegistrarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            string mensaje = string.Empty;

            List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> list = new List<CentroEntrenamiento_EditorPaginaWebDetalleDTO>();

            list.Add(new CentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                Codigo = request.Codigo,
                Tipo = request.Tipo,
                Titulo = request.Titulo,
                SubTitulo = request.SubTitulo,
                LinkPago = request.LinkPago == null ? string.Empty : request.LinkPago,
                UrlUmagen = request.UrlUmagen,
                Operation = Operation.Create
            });

            ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO oReq = new ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_EditorPaginaWebDetalleDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebDetalleLogic oCentroEntrenamiento_EditorPaginaWebDetalleLogic = new CentroEntrenamiento_EditorPaginaWebDetalleLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebDetalleLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }

            return mensaje;
        }

        public string ecommerce_uspActualizarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            string mensaje = string.Empty;

            List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> list = new List<CentroEntrenamiento_EditorPaginaWebDetalleDTO>();

            list.Add(new CentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                Codigo = request.Codigo,
                Tipo = request.Tipo,
                Titulo = request.Titulo,
                SubTitulo = request.SubTitulo,
                UrlUmagen = request.UrlUmagen,
                LinkPago = request.LinkPago == null ? string.Empty : request.LinkPago,
                Operation = Operation.Update
            });

            ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO oReq = new ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_EditorPaginaWebDetalleDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebDetalleLogic oCentroEntrenamiento_EditorPaginaWebDetalleLogic = new CentroEntrenamiento_EditorPaginaWebDetalleLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebDetalleLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }

            return mensaje;
        }


        public int ecommerce_uspEliminarEdicionPaginaWebDetalle(CentroEntrenamiento_EditorPaginaWebDetalleDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_EditorPaginaWebDetalleDTO> list = new List<CentroEntrenamiento_EditorPaginaWebDetalleDTO>();

            list.Add(new CentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                Codigo = request.Codigo,
                Operation = Operation.Delete
            });

            ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO oReq = new ReqCentroEntrenamiento_EditorPaginaWebDetalleDTO()
            {
                List = list,
                User = "appsfit"
            };
            RespCentroEntrenamiento_EditorPaginaWebDetalleDTO oResp = null;
            using (CentroEntrenamiento_EditorPaginaWebDetalleLogic oCentroEntrenamiento_EditorPaginaWebDetalleLogic = new CentroEntrenamiento_EditorPaginaWebDetalleLogic())
            {
                oResp = oCentroEntrenamiento_EditorPaginaWebDetalleLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }
            return mensaje;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}