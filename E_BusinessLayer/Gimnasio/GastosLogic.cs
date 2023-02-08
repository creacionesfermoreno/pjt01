
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;

namespace E_BusinessLayer.Gimnasio
{	
	public class GastosLogic: IDisposable
	{
		GastosData oEgresosData = null;
		public GastosLogic()
		{
			oEgresosData = new GastosData();
		}
			
		public RespListGastosDTO GastosGetList(ReqFilterGastosDTO oReqFilterGastosDTO)
		{
			RespListGastosDTO oRespListGastosDTO = new RespListGastosDTO();
		    
			oRespListGastosDTO.List = new List<GastosDTO>();
			oRespListGastosDTO.User = oReqFilterGastosDTO.User;
			oRespListGastosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterGastosDTO.User))
            {
                oRespListGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Egresos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterGastosDTO.Paging == null)
            {
                oRespListGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListGastosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    List<GastosDTO> EgresosDTOList = new List<GastosDTO>();

                    switch (oReqFilterGastosDTO.FilterCase)
                    {
                        case filterCaseGastos.ListarDetalleEgresosCaja:
                            EgresosDTOList = oEgresosData.ListarDetalleEgresosCaja(oReqFilterGastosDTO.Item);
                            break;
                        case filterCaseGastos.uspReporteEgresoRangoFechas:
                           
                            EgresosDTOList = oEgresosData.uspReporteEgresoRangoFechas(oReqFilterGastosDTO.Item);
                            break;
                        case filterCaseGastos.uspReporteEgresoRangoFechas_Paginacion:

                            if (!oReqFilterGastosDTO.Paging.All && oReqFilterGastosDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterGastosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            EgresosDTOList = oEgresosData.uspReporteEgresoRangoFechas_Paginacion(oReqFilterGastosDTO.Item, oReqFilterGastosDTO.Paging);
                            break;
                        case filterCaseGastos.uspReporteEgresoRangoFechas_ExportarExcel:

                            if (!oReqFilterGastosDTO.Paging.All && oReqFilterGastosDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterGastosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            EgresosDTOList = oEgresosData.uspReporteEgresoRangoFechas_ExportarExcel(oReqFilterGastosDTO.Item, oReqFilterGastosDTO.Paging);
                            break;
                        case filterCaseGastos.uspReporteEgresoRangoFechas_PaginacionExcel:
                            
                            if (!oReqFilterGastosDTO.Paging.All && oReqFilterGastosDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterGastosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            EgresosDTOList = oEgresosData.uspReporteEgresoRangoFechas_PaginacionExcel(oReqFilterGastosDTO.Item, oReqFilterGastosDTO.Paging);
                            break;
                        case filterCaseGastos.ListarEgresosTotal:
                            
                            EgresosDTOList = oEgresosData.ListarEgresosTotal(oReqFilterGastosDTO.Item);
                            break;
                        default:
                            EgresosDTOList = oEgresosData.Listar(oReqFilterGastosDTO.Item);
                        break;
                    }

                    oRespListGastosDTO.List = EgresosDTOList;
                    oRespListGastosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListGastosDTO.Success = false;
                    oRespListGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListGastosDTO;	
           
		}
		
		public RespItemGastosDTO GastosGetItem(ReqFilterGastosDTO oReqFilterGastosDTO)
		{
			RespItemGastosDTO oRespItemGastosDTO = new RespItemGastosDTO();

            oRespItemGastosDTO.Success = false;
            oRespItemGastosDTO.Item = null;
            oRespItemGastosDTO.User = oReqFilterGastosDTO.User;
            oRespItemGastosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterGastosDTO.User))
            {
                oRespItemGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Egresos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemGastosDTO.MessageList.Count == 0)
            {
                GastosDTO oGastosDTO = null;
                try
                {
                    switch (oReqFilterGastosDTO.FilterCase)
                    {
                       
                        case filterCaseGastos.porCodigo:
                            {
                                oGastosDTO = new GastosDTO();
                                oGastosDTO = oEgresosData.BuscarPorCodigoEgresos(oReqFilterGastosDTO.Item);
                            }
                            break;
                        case filterCaseGastos.uspReporteEgresoRangoFechas_NumeroRegistros:
                            {
                                oGastosDTO = new GastosDTO();
                                oGastosDTO = oEgresosData.uspReporteEgresoRangoFechas_NumeroRegistros(oReqFilterGastosDTO.Item);
                            }
                            break;
                        default:
                            {
                                oGastosDTO = new GastosDTO();
                            }
                            break;
                    }

                    oRespItemGastosDTO.Item = new GastosDTO();
                    oRespItemGastosDTO.Item = oGastosDTO;
                    oRespItemGastosDTO.Success = true;
                    oRespItemGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemGastosDTO.Success = false;
                    oRespItemGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemGastosDTO;
		}
	
		public RespGastosDTO ExecuteTransac(ReqGastosDTO oReqGastosDTO)
		{
			RespGastosDTO oRespGastosDTO = new RespGastosDTO();

            oRespGastosDTO.MessageList = new List<Mensaje>();
            oRespGastosDTO.User = oReqGastosDTO.User;
            
            if (String.IsNullOrEmpty(oReqGastosDTO.User))
            {
                oRespGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Egresos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespGastosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (GastosDTO item in oReqGastosDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oEgresosData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oEgresosData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oEgresosData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespGastosDTO.Success = true;
                        oRespGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespGastosDTO.Success = false;
                        oRespGastosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespGastosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
