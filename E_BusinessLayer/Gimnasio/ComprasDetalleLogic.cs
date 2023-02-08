using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.IO;

namespace E_BusinessLayer.Gimnasio
{
	
	public class ComprasDetalleLogic: IDisposable
	{
		ComprasDetalleData oComprasDetalleData = null;
		public ComprasDetalleLogic()
		{
			oComprasDetalleData = new ComprasDetalleData();
		}
        
		public RespListComprasDetalleDTO ComprasDetalleGetList(ReqFilterComprasDetalleDTO oReqFilterComprasDetalleDTO)
		{
		
			RespListComprasDetalleDTO oRespListComprasDetalleDTO = new RespListComprasDetalleDTO();
		
			oRespListComprasDetalleDTO.List = new List<ComprasDetalleDTO>();
			oRespListComprasDetalleDTO.User = oReqFilterComprasDetalleDTO.User;
			oRespListComprasDetalleDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterComprasDetalleDTO.User))
            {
                oRespListComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de DetalleCIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterComprasDetalleDTO.Paging == null)
            {
                oRespListComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

			if (oRespListComprasDetalleDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    List<ComprasDetalleDTO> ComprasDetalleDTOList = new List<ComprasDetalleDTO>();
                    switch (oReqFilterComprasDetalleDTO.FilterCase)
                    {
                        case filterCaseComprasDetalle.ListarComprasEditar:
                            ComprasDetalleDTOList = oComprasDetalleData.Listar(oReqFilterComprasDetalleDTO.Item);
                            break;
                       case filterCaseComprasDetalle.uspListarControlDetalleCIngresosRangoFechas:
                            ComprasDetalleDTOList = oComprasDetalleData.uspListarControlDetalleCIngresosRangoFechas(oReqFilterComprasDetalleDTO.Item);
                            break;
                       case filterCaseComprasDetalle.uspListarComprasProductos_Paginacion:
                            if (!oReqFilterComprasDetalleDTO.Paging.All && oReqFilterComprasDetalleDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterComprasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarComprasProductos_NumeroRegistros"]);
                                ComprasDetalleDTOList = oComprasDetalleData.uspListarComprasProductos_Paginacion(oReqFilterComprasDetalleDTO.Item, oReqFilterComprasDetalleDTO.Paging);
                            }
                           
                            break;
                        default:
                          
                            break;
                    }
                   
                    oRespListComprasDetalleDTO.List = ComprasDetalleDTOList;
                    oRespListComprasDetalleDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListComprasDetalleDTO.Success = false;
                    oRespListComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListComprasDetalleDTO;	
           
		}
		
		public RespItemComprasDetalleDTO ComprasDetalleGetItem(ReqFilterComprasDetalleDTO oReqFilterComprasDetalleDTO)
		{
			RespItemComprasDetalleDTO oRespItemComprasDetalleDTO = new RespItemComprasDetalleDTO();

            oRespItemComprasDetalleDTO.Success = false;
            oRespItemComprasDetalleDTO.Item = null;
            oRespItemComprasDetalleDTO.User = oReqFilterComprasDetalleDTO.User;
            oRespItemComprasDetalleDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterComprasDetalleDTO.User))
            {
                oRespItemComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de DetalleCIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

          
            if (oRespItemComprasDetalleDTO.MessageList.Count == 0)
            {
                ComprasDetalleDTO oComprasDetalleDTO = null;
                try
                {
                    switch (oReqFilterComprasDetalleDTO.FilterCase)
                    {                       
                        case filterCaseComprasDetalle.uspListarComprasProductos_NumeroRegistros:
                            {
                                oComprasDetalleDTO = new ComprasDetalleDTO();
                                oComprasDetalleDTO = oComprasDetalleData.uspListarComprasProductos_NumeroRegistros(oReqFilterComprasDetalleDTO.Item);
                            }
                            break;
                        default:
                            {
                                oComprasDetalleDTO = new ComprasDetalleDTO();
                            }
                            break;
                    }

                    oRespItemComprasDetalleDTO.Item = new ComprasDetalleDTO();
                    oRespItemComprasDetalleDTO.Item = oComprasDetalleDTO;
                    oRespItemComprasDetalleDTO.Success = true;
                    oRespItemComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemComprasDetalleDTO.Success = false;
                    oRespItemComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemComprasDetalleDTO;
		}
	
		public RespComprasDetalleDTO ExecuteTransac(ReqComprasDetalleDTO oReqComprasDetalleDTO)
		{
			RespComprasDetalleDTO oRespComprasDetalleDTO = new RespComprasDetalleDTO();

            oRespComprasDetalleDTO.MessageList = new List<Mensaje>();
            oRespComprasDetalleDTO.User = oReqComprasDetalleDTO.User;
            
            if (String.IsNullOrEmpty(oReqComprasDetalleDTO.User))
            {
                oRespComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de DetalleCIngreso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespComprasDetalleDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (ComprasDetalleDTO item in oReqComprasDetalleDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oComprasDetalleData.Registrar(item);
                                    break;
                                case Operation.Delete:
                                    UsuariosIngresosData oUsuariosIngresosDataDelete = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTODelete = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTODelete.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTODelete.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTODelete.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTODelete.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTODelete.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTODelete.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTODelete = oUsuariosIngresosDataDelete.uspValidarAccesoSistema(oUsuariosIngresosDTODelete);
                                    if (oUsuariosIngresosDTODelete.CodigoValidacion == 3)
                                    {
                                        Codigo = 999999999;
                                        oComprasDetalleData.Eliminar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }
                                   


                                    break;
                            }
                        }
                        tx.Complete();
                        oRespComprasDetalleDTO.Success = true;
                        oRespComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespComprasDetalleDTO.Success = false;
                        oRespComprasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespComprasDetalleDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
