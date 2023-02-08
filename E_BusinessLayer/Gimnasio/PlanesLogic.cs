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

	public class PlanesLogic: IDisposable
	{
		PlanesData oPaquetesData = null;
		public PlanesLogic()
		{
			oPaquetesData = new PlanesData();
		}
		
		public RespListPlanesDTO PlanesGetList(ReqFilterPlanesDTO oReqFilterPlanesDTO)
		{
		
			RespListPlanesDTO oRespListPlanesDTO = new RespListPlanesDTO();
		
			oRespListPlanesDTO.List = new List<PlanesDTO>();
			oRespListPlanesDTO.User = oReqFilterPlanesDTO.User;
			oRespListPlanesDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPlanesDTO.User))
            {
                oRespListPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Paquetes no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPlanesDTO.Paging == null)
            {
                oRespListPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPlanesDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterPlanesDTO.Paging.All && oReqFilterPlanesDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPlanesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarPaquetesMenbresiasCursos_Paginacion"]);
                    }

                    List<PlanesDTO> PlanesDTOList = new List<PlanesDTO>();

                    switch (oReqFilterPlanesDTO.FilterCase)
                    {
                        case filterCasePlanes.uspListarTotalesPaquetesPorMes:
                            {
                                PlanesDTOList = oPaquetesData.uspListarTotalesPaquetesPorMes(oReqFilterPlanesDTO.Item);
                            }
                            break;
                  
                        case filterCasePlanes.filter_uspListarPaquetesPorProfesor:
                            {
                                PlanesDTOList = oPaquetesData.uspListarPaquetesPorProfesor(oReqFilterPlanesDTO.Item);
                            }
                            break;

                        case filterCasePlanes.ListarPaquetesBusquedaFiltroSocio:
                            {
                                PlanesDTOList = oPaquetesData.ListarPaquetesBusquedaFiltroSocio(oReqFilterPlanesDTO.Item);
                            }
                            break;
                        case filterCasePlanes.ListarPaquetesTablaProspectos:
                            {
                                PlanesDTOList = oPaquetesData.ListarPaquetesTablaProspectos(oReqFilterPlanesDTO.Item);
                            }
                            break;
                        case filterCasePlanes.uspListarPaquetesMenbresiasCursos_Paginacion:
                            {
                                PlanesDTOList = oPaquetesData.uspListarPaquetesMenbresiasCursos_Paginacion(oReqFilterPlanesDTO.Item, oReqFilterPlanesDTO.Paging, ref recordCount);
                            }
                            break; 
                        
                        case filterCasePlanes.listApp:
                            {
                                PlanesDTOList = oPaquetesData.ListarPaquetesApp(oReqFilterPlanesDTO.Item);
                            }
                            break;
                      
                    }

                    oRespListPlanesDTO.List = PlanesDTOList;
                    oRespListPlanesDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPlanesDTO.Success = false;
                    oRespListPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPlanesDTO;	
           
		}
		
		public RespItemPlanesDTO PlanesGetItem(ReqFilterPlanesDTO oReqFilterPlanesDTO)
		{
			RespItemPlanesDTO oRespItemPlanesDTO = new RespItemPlanesDTO();

            oRespItemPlanesDTO.Success = false;
            oRespItemPlanesDTO.Item = null;
            oRespItemPlanesDTO.User = oReqFilterPlanesDTO.User;
            oRespItemPlanesDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPlanesDTO.User))
            {
                oRespItemPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Paquetes no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPlanesDTO.MessageList.Count == 0)
            {
                PlanesDTO oPaquetesDTO = null;
                try
                {
                    switch (oReqFilterPlanesDTO.FilterCase)
                    {
                       
                        case filterCasePlanes.porCodigo:
                            {
                                oPaquetesDTO = new PlanesDTO();
                                oPaquetesDTO = oPaquetesData.BuscarPorCodigoPaquetes(oReqFilterPlanesDTO.Item);
                            }
                            break;
                        case filterCasePlanes.BuscarCantidadCupoPaquetesPorCodigo:
                            {
                                oPaquetesDTO = new PlanesDTO();
                                oPaquetesDTO = oPaquetesData.BuscarCantidadCupoPaquetesPorCodigo(oReqFilterPlanesDTO.Item);
                            }
                            break;
                        case filterCasePlanes.uspListarPaquetesMenbresiasCursos_NumeroRegistros:
                            {
                                oPaquetesDTO = new PlanesDTO();
                                oPaquetesDTO = oPaquetesData.uspListarPaquetesMenbresiasCursos_NumeroRegistros(oReqFilterPlanesDTO.Item);
                            }
                            break;
                        default:
                            {
                                oPaquetesDTO = new PlanesDTO();
                            }
                            break;
                    }

                    oRespItemPlanesDTO.Item = new PlanesDTO();
                    oRespItemPlanesDTO.Item = oPaquetesDTO;
                    oRespItemPlanesDTO.Success = true;
                    oRespItemPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPlanesDTO.Success = false;
                    oRespItemPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPlanesDTO;
		}
	
		public RespPlanesDTO ExecuteTransac(ReqPlanesDTO oReqPlanesDTO)
		{
			RespPlanesDTO oRespPlanesDTO = new RespPlanesDTO();

            oRespPlanesDTO.MessageList = new List<Mensaje>();
            oRespPlanesDTO.User = oReqPlanesDTO.User;
            
            if (String.IsNullOrEmpty(oReqPlanesDTO.User))
            {
                oRespPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Paquetes no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPlanesDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int CodigoIngreso = 0;
                        foreach (PlanesDTO item in oReqPlanesDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:

                                    CodigoIngreso = 100;
                                    oPaquetesData.Registrar(item);

                                    break;
                                case Operation.Update:

                                    CodigoIngreso = 100;
                                    oPaquetesData.Actualizar(item);

                                    break;
                                case Operation.Delete:

                                    CodigoIngreso = 100;
                                    int cantidad = oPaquetesData.Eliminar(item);
                                    if (cantidad > 0)
                                    {
                                        tx.Dispose();
                                        oRespPlanesDTO.Success = false;
                                        oRespPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                        {
                                            Codigo = 16,
                                            Detalle = "No ha podido eliminar porque el cliente tiene membresias.",
                                            Tipo = TipoMensaje.Informacion
                                        });
                                    }
                                    
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespPlanesDTO.Success = true;
                        oRespPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoIngreso,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPlanesDTO.Success = false;
                        oRespPlanesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPlanesDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int ValidarBuscarDiasHorarioPaquete(int CodigoUnidadNegocio, int CodigoPaquete)
        {
            int flag = 0;
            flag = oPaquetesData.ValidarBuscarDiasHorarioPaquete(CodigoUnidadNegocio,CodigoPaquete);
            return flag;
        }

	}
}
