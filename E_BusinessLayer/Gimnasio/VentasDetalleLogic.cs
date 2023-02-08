
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
	
	public class VentasDetalleLogic: IDisposable
	{
		VentasDetalleData oVentasDetalleData = null;
		public VentasDetalleLogic()
		{
			oVentasDetalleData = new VentasDetalleData();
		}
		
		public RespListVentasDetalleDTO VentasDetalleGetList(ReqFilterVentasDetalleDTO oReqFilterVentasDetalleDTO)
		{
			RespListVentasDetalleDTO oRespListVentasDetalleDTO = new RespListVentasDetalleDTO();
		
			oRespListVentasDetalleDTO.List = new List<VentasDetalleDTO>();
			oRespListVentasDetalleDTO.User = oReqFilterVentasDetalleDTO.User;
			oRespListVentasDetalleDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterVentasDetalleDTO.User))
            {
                oRespListVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlDetalleSalida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterVentasDetalleDTO.Paging == null)
            {
                oRespListVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListVentasDetalleDTO.MessageList.Count == 0)
            {
                
                try
                {
                    List<VentasDetalleDTO> VentasDetalleDTOList = new List<VentasDetalleDTO>();
                    switch (oReqFilterVentasDetalleDTO.FilterCase)
                    {

                        case filterCaseVentasDetalle.uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion:

                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }

                            int NumeroRegistros = 0;
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging, out NumeroRegistros);

                            oRespListVentasDetalleDTO.Paging = new Paging {
                                TotalRecord = Convert.ToUInt32(NumeroRegistros),
                                PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"])
                            };

                            break;

                        case filterCaseVentasDetalle.uspReporteVentasRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                       case filterCaseVentasDetalle.uspReporteVentasNutricionRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasNutricionRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasNutricionRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                       case filterCaseVentasDetalle.uspReporteVentasPersonalizadoRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasPersonalizadoRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasPersonalizadoRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasProductosRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasProductosRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                         case filterCaseVentasDetalle.uspReporteVentasSuplementosTotalesRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasSuplementosTotalesRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasSuplementosTotalesRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasRopasTotalesRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRopasTotalesRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasRopasTotalesRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                        case filterCaseVentasDetalle.uspListarSuplementosPagosPorFechaAnular_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarSuplementosPagosPorFechaAnular_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspListarSuplementosPagosPorFechaAnular_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                        case filterCaseVentasDetalle.uspListarRopasPagosPorFechaAnular_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarRopasPagosPorFechaAnular_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspListarRopasPagosPorFechaAnular_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasCafeteriaRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasCafeteriaRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasServiciosRangoFechas_Paginacion:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasServiciosRangoFechas_Paginacion(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                        case filterCaseVentasDetalle.uspListarDetalleVentasSuplementos:
                            {
                                VentasDetalleDTOList = oVentasDetalleData.uspListarDetalleVentasSuplementos(oReqFilterVentasDetalleDTO.Item);
                            }
                         break;

                        case filterCaseVentasDetalle.uspListarDetalleVentasRopas:
                            {
                                VentasDetalleDTOList = oVentasDetalleData.uspListarDetalleVentasRopas(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;




                        case filterCaseVentasDetalle.uspReporteVentasMembresuasRangoFechas_PaginacionExcel:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasMembresiasRangoFechas_PaginacionExcel(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                        case filterCaseVentasDetalle.uspReporteVentasServiciosRangoFechas_PaginacionExcel:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasServiciosRangoFechas_PaginacionExcel(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                        case filterCaseVentasDetalle.uspReporteVentasProductosRangoFechas_PaginacionExcel:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasProductosRangoFechas_PaginacionExcel(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                        case filterCaseVentasDetalle.uspReporteVentasSuplementosTotalesRangoFechas_PaginacionExcel:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasSuplementosTotalesRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasSuplementosTotalesRangoFechas_PaginacionExcel(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                        case filterCaseVentasDetalle.uspReporteVentasNutricionRangoFechas_PaginacionExcel:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasNutricionRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasNutricionRangoFechas_PaginacionExcel(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                        case filterCaseVentasDetalle.uspReporteVentasPersonalizadoRangoFechas_PaginacionExcel:
                           
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasPersonalizadoRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasPersonalizadoRangoFechas_PaginacionExcel(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                        case filterCaseVentasDetalle.uspReporteVentasRopasTotalesRangoFechas_PaginacionExcel:
                            
                            if (!oReqFilterVentasDetalleDTO.Paging.All && oReqFilterVentasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterVentasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRopasTotalesRangoFechas_Paginacion"]);
                            }
                            VentasDetalleDTOList = oVentasDetalleData.uspReporteVentasRopasTotalesRangoFechas_PaginacionExcel(oReqFilterVentasDetalleDTO.Item, oReqFilterVentasDetalleDTO.Paging);
                            break;
                            
                        default:
                            VentasDetalleDTOList = oVentasDetalleData.Listar(oReqFilterVentasDetalleDTO.Item);
                            break;
                    }
                  
                    oRespListVentasDetalleDTO.List = VentasDetalleDTOList;
                    oRespListVentasDetalleDTO.Success = true;
                    

                }
                catch (Exception ex)
                {
                    oRespListVentasDetalleDTO.Success = false;
                    oRespListVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListVentasDetalleDTO;	
           
		}
		
		public RespItemVentasDetalleDTO VentasDetalleGetItem(ReqFilterVentasDetalleDTO oReqFilterVentasDetalleDTO)
		{
			RespItemVentasDetalleDTO oRespItemVentasDetalleDTO = new RespItemVentasDetalleDTO();

            oRespItemVentasDetalleDTO.Success = false;
            oRespItemVentasDetalleDTO.Item = null;
            oRespItemVentasDetalleDTO.User = oReqFilterVentasDetalleDTO.User;
            oRespItemVentasDetalleDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterVentasDetalleDTO.User))
            {
                oRespItemVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlDetalleSalida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemVentasDetalleDTO.MessageList.Count == 0)
            {
                VentasDetalleDTO oVentasDetalleDTO = null;
                try
                {
                    switch (oReqFilterVentasDetalleDTO.FilterCase)
                    {
                        case filterCaseVentasDetalle.uspReporteVentasRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;
                       case filterCaseVentasDetalle.uspReporteVentasNutricionRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasNutricionRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasProductosRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasProductosRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspListarSuplementosPagosPorFechaAnular_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspListarSuplementosPagosPorFechaAnular_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspListarRopasPagosPorFechaAnular_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspListarRopasPagosPorFechaAnular_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasCafeteriaRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasCafeteriaRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;

                        case filterCaseVentasDetalle.uspReporteVentasServiciosRangoFechas_NumeroRegistros:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                                oVentasDetalleDTO = oVentasDetalleData.uspReporteVentasServiciosRangoFechas_NumeroRegistros(oReqFilterVentasDetalleDTO.Item);
                            }
                            break;


                        default:
                            {
                                oVentasDetalleDTO = new VentasDetalleDTO();
                            }
                            break;
                    }

                    oRespItemVentasDetalleDTO.Item = new VentasDetalleDTO();
                    oRespItemVentasDetalleDTO.Item = oVentasDetalleDTO;
                    oRespItemVentasDetalleDTO.Success = true;
                    oRespItemVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemVentasDetalleDTO.Success = false;
                    oRespItemVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemVentasDetalleDTO;
		}
	
		public RespVentasDetalleDTO ExecuteTransac(ReqVentasDetalleDTO oReqVentasDetalleDTO)
		{
			RespVentasDetalleDTO oRespVentasDetalleDTO = new RespVentasDetalleDTO();

            oRespVentasDetalleDTO.MessageList = new List<Mensaje>();
            oRespVentasDetalleDTO.User = oReqVentasDetalleDTO.User;
            
            if (String.IsNullOrEmpty(oReqVentasDetalleDTO.User))
            {
                oRespVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ControlDetalleSalida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespVentasDetalleDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (VentasDetalleDTO item in oReqVentasDetalleDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oVentasDetalleData.Registrar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespVentasDetalleDTO.Success = true;
                        oRespVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespVentasDetalleDTO.Success = false;
                        oRespVentasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespVentasDetalleDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
