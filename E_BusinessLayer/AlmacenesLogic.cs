using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer;
using E_DataModel;
using E_DataModel.Common;

namespace E_BusinessLayer
{
    public class AlmacenesLogic : IDisposable
    {
        AlmacenesData oAlmacenesData = null;
        public AlmacenesLogic()
        {
            oAlmacenesData = new AlmacenesData();
        }

        public RespListAlmacenesDTO AlmacenesGetList(ReqFilterAlmacenesDTO oReqFilter)
        {

            RespListAlmacenesDTO oRespList = new RespListAlmacenesDTO();

            oRespList.List = new List<AlmacenesDTO>();
            oRespList.User = oReqFilter.User;
            oRespList.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilter.User))
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Categoria no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilter.Paging == null)
            {
                oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }


            if (oRespList.MessageList.Count == 0)
            {
                try
                {

                    if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                    {
                        oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<AlmacenesDTO> CategoriaDTOList = new List<AlmacenesDTO>();

                    switch (oReqFilter.FilterCase)
                    {
                        case filterCaseAlmacenes.ecommerce_uspListarAlmacenes_Paginacion:

                            if (!oReqFilter.Paging.All && oReqFilter.Paging.PageRecords == 0)
                            {
                                oReqFilter.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            CategoriaDTOList = oAlmacenesData.ecommerce_uspListarAlmacenes_Paginacion(oReqFilter.Item,oReqFilter.Paging);
                            break;
                        default:
                            {
                                // CategoriaDTOList = oAlmacenesData.uspListarSocios_PorVendedor_Paginacion(oReqFilter.Paging);
                            }
                            break;
                    }

                    oRespList.List = CategoriaDTOList;
                    oRespList.Success = true;

                }
                catch (Exception ex)
                {
                    oRespList.Success = false;
                    oRespList.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespList;

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
