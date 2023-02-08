
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
	//-------------------------------------------------------------------
	//Archivo     : AsistenciaInvitadosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 04/05/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class AsistenciaInvitadosLogic: IDisposable
	{
		AsistenciaInvitadosData oAsistenciaInvitadosData = null;
		public AsistenciaInvitadosLogic()
		{
			oAsistenciaInvitadosData = new AsistenciaInvitadosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AsistenciaInvitadosGetList
		//Objetivo: Retorna una colección de registros de tipo AsistenciaInvitadosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListAsistenciaInvitadosDTO AsistenciaInvitadosGetList(ReqFilterAsistenciaInvitadosDTO oReqFilterAsistenciaInvitadosDTO)
		{
		
			RespListAsistenciaInvitadosDTO oRespListAsistenciaInvitadosDTO = new RespListAsistenciaInvitadosDTO();
		
			oRespListAsistenciaInvitadosDTO.List = new List<AsistenciaInvitadosDTO>();
			oRespListAsistenciaInvitadosDTO.User = oReqFilterAsistenciaInvitadosDTO.User;
			oRespListAsistenciaInvitadosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterAsistenciaInvitadosDTO.User))
            {
                oRespListAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AsistenciaInvitados no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterAsistenciaInvitadosDTO.Paging == null)
            {
                oRespListAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListAsistenciaInvitadosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    List<AsistenciaInvitadosDTO> AsistenciaInvitadosDTOList = new List<AsistenciaInvitadosDTO>();

                    switch (oReqFilterAsistenciaInvitadosDTO.FilterCase)
                    {

                        case filterCaseAsistenciaInvitados.uspListarDetalleAsistenciaInvitados_Paginacion:
                            {
                                if (!oReqFilterAsistenciaInvitadosDTO.Paging.All && oReqFilterAsistenciaInvitadosDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAsistenciaInvitadosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDetalleAsistenciaInvitados_Paginacion"]);
                                }

                                AsistenciaInvitadosDTOList = oAsistenciaInvitadosData.uspListarDetalleAsistenciaInvitados_Paginacion(oReqFilterAsistenciaInvitadosDTO.Item, oReqFilterAsistenciaInvitadosDTO.Paging);
                            }
                            break;
                        case filterCaseAsistenciaInvitados.ListarAsistenciaTodosFiltroInvitados_Paginacion:
                            {
                                if (!oReqFilterAsistenciaInvitadosDTO.Paging.All && oReqFilterAsistenciaInvitadosDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterAsistenciaInvitadosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarAsistenciaTodosInvitados_Paginacion"]);
                                }

                                AsistenciaInvitadosDTOList = oAsistenciaInvitadosData.uspListarAsistenciaTodosFiltroInvitados_Paginacion(oReqFilterAsistenciaInvitadosDTO.Item, oReqFilterAsistenciaInvitadosDTO.Paging);
                            }
                            break;
                            
                    }

                    oRespListAsistenciaInvitadosDTO.List = AsistenciaInvitadosDTOList;
                    oRespListAsistenciaInvitadosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListAsistenciaInvitadosDTO.Success = false;
                    oRespListAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListAsistenciaInvitadosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	AsistenciaInvitadosGetItem
		//Objetivo: Retorna un registro de tipo AsistenciaInvitadosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemAsistenciaInvitadosDTO AsistenciaInvitadosGetItem(ReqFilterAsistenciaInvitadosDTO oReqFilterAsistenciaInvitadosDTO)
		{
			RespItemAsistenciaInvitadosDTO oRespItemAsistenciaInvitadosDTO = new RespItemAsistenciaInvitadosDTO();

            oRespItemAsistenciaInvitadosDTO.Success = false;
            oRespItemAsistenciaInvitadosDTO.Item = null;
            oRespItemAsistenciaInvitadosDTO.User = oReqFilterAsistenciaInvitadosDTO.User;
            oRespItemAsistenciaInvitadosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterAsistenciaInvitadosDTO.User))
            {
                oRespItemAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AsistenciaInvitados no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemAsistenciaInvitadosDTO.MessageList.Count == 0)
            {
                AsistenciaInvitadosDTO oAsistenciaInvitadosDTO = null;
                try
                {
                    switch (oReqFilterAsistenciaInvitadosDTO.FilterCase)
                    {
                       
                      
                        case filterCaseAsistenciaInvitados.uspListarDetalleAsistenciaInvitados_NumeroRegistros:
                            {
                                oAsistenciaInvitadosDTO = new AsistenciaInvitadosDTO();
                                oAsistenciaInvitadosDTO = oAsistenciaInvitadosData.uspListarDetalleAsistenciaInvitados_NumeroRegistros(oReqFilterAsistenciaInvitadosDTO.Item);
                            }
                            break;

                        case filterCaseAsistenciaInvitados.uspListarAsistenciaTodosFiltroInvitados_NumeroRegistros:
                            {
                                oAsistenciaInvitadosDTO = new AsistenciaInvitadosDTO();
                                oAsistenciaInvitadosDTO = oAsistenciaInvitadosData.uspListarAsistenciaTodosFiltroInvitados_NumeroRegistros(oReqFilterAsistenciaInvitadosDTO.Item);
                            }
                            break;

                        default:
                            {
                                oAsistenciaInvitadosDTO = new AsistenciaInvitadosDTO();
                            }
                            break;
                    }

                    oRespItemAsistenciaInvitadosDTO.Item = new AsistenciaInvitadosDTO();
                    oRespItemAsistenciaInvitadosDTO.Item = oAsistenciaInvitadosDTO;
                    oRespItemAsistenciaInvitadosDTO.Success = true;
                    oRespItemAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemAsistenciaInvitadosDTO.Success = false;
                    oRespItemAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemAsistenciaInvitadosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo AsistenciaInvitadosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespAsistenciaInvitadosDTO ExecuteTransac(ReqAsistenciaInvitadosDTO oReqAsistenciaInvitadosDTO)
		{
			RespAsistenciaInvitadosDTO oRespAsistenciaInvitadosDTO = new RespAsistenciaInvitadosDTO();

            oRespAsistenciaInvitadosDTO.MessageList = new List<Mensaje>();
            oRespAsistenciaInvitadosDTO.User = oReqAsistenciaInvitadosDTO.User;
            
            if (String.IsNullOrEmpty(oReqAsistenciaInvitadosDTO.User))
            {
                oRespAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de AsistenciaInvitados no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespAsistenciaInvitadosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (AsistenciaInvitadosDTO item in oReqAsistenciaInvitadosDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oAsistenciaInvitadosData.Registrar(item);
                                    break;
                                case Operation.uspActualizarAsistenciaInvitadoPorCodigoInvitado:
                                    oAsistenciaInvitadosData.uspActualizarAsistenciaInvitadoPorCodigoInvitado(item);
                                    break;

                                case Operation.Delete:
                                    oAsistenciaInvitadosData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespAsistenciaInvitadosDTO.Success = true;
                        oRespAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespAsistenciaInvitadosDTO.Success = false;
                        oRespAsistenciaInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespAsistenciaInvitadosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
