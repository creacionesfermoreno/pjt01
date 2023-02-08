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
	//Archivo     : PerfilLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 11/25/2014
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class PerfilLogic: IDisposable
	{
		PerfilData oPerfilData = null;
		public PerfilLogic()
		{
			oPerfilData = new PerfilData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PerfilGetList
		//Objetivo: Retorna una colección de registros de tipo PerfilDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListPerfilDTO PerfilGetList(ReqFilterPerfilDTO oReqFilterPerfilDTO)
		{
		
			RespListPerfilDTO oRespListPerfilDTO = new RespListPerfilDTO();
		
			oRespListPerfilDTO.List = new List<PerfilDTO>();
			oRespListPerfilDTO.User = oReqFilterPerfilDTO.User;
			oRespListPerfilDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPerfilDTO.User))
            {
                oRespListPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Perfil no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPerfilDTO.Paging == null)
            {
                oRespListPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPerfilDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    
                    if (!oReqFilterPerfilDTO.Paging.All && oReqFilterPerfilDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPerfilDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PerfilDTO> PerfilDTOList = new List<PerfilDTO>();

                    switch (oReqFilterPerfilDTO.FilterCase)
                    {
                        
                        case filterCasePerfil.filter_uspListarConfiguracionPerfil:
                            {
                                PerfilDTOList = oPerfilData.uspListarConfiguracionPerfil();
                            }
                        break;

                        default:
                            {
                                PerfilDTOList = oPerfilData.Listar();
                            }
                            break;
                    }

                    oRespListPerfilDTO.List = PerfilDTOList;
                    oRespListPerfilDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPerfilDTO.Success = false;
                    oRespListPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPerfilDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PerfilGetItem
		//Objetivo: Retorna un registro de tipo PerfilDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemPerfilDTO PerfilGetItem(ReqFilterPerfilDTO oReqFilterPerfilDTO)
		{
			RespItemPerfilDTO oRespItemPerfilDTO = new RespItemPerfilDTO();

            oRespItemPerfilDTO.Success = false;
            oRespItemPerfilDTO.Item = null;
            oRespItemPerfilDTO.User = oReqFilterPerfilDTO.User;
            oRespItemPerfilDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPerfilDTO.User))
            {
                oRespItemPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Perfil no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPerfilDTO.MessageList.Count == 0)
            {
                PerfilDTO oPerfilDTO = null;
                try
                {
                    switch (oReqFilterPerfilDTO.FilterCase)
                    {
                       
                        case filterCasePerfil.porCodigo:
                            {
                                oPerfilDTO = new PerfilDTO();
                                oPerfilDTO = oPerfilData.BuscarPorCodigoPerfil(oReqFilterPerfilDTO.Item);
                            }
                            break;
                        default:
                            {
                                oPerfilDTO = new PerfilDTO();
                            }
                            break;
                    }

                    oRespItemPerfilDTO.Item = new PerfilDTO();
                    oRespItemPerfilDTO.Item = oPerfilDTO;
                    oRespItemPerfilDTO.Success = true;
                    oRespItemPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPerfilDTO.Success = false;
                    oRespItemPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPerfilDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo PerfilDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespPerfilDTO ExecuteTransac(ReqPerfilDTO oReqPerfilDTO)
		{
			RespPerfilDTO oRespPerfilDTO = new RespPerfilDTO();

            oRespPerfilDTO.MessageList = new List<Mensaje>();
            oRespPerfilDTO.User = oReqPerfilDTO.User;
            
            if (String.IsNullOrEmpty(oReqPerfilDTO.User))
            {
                oRespPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Perfil no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPerfilDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (PerfilDTO item in oReqPerfilDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oPerfilData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oPerfilData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oPerfilData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespPerfilDTO.Success = true;
                        oRespPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPerfilDTO.Success = false;
                        oRespPerfilDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPerfilDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
