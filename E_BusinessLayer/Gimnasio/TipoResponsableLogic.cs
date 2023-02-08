
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
	//Archivo     : TipoResponsableLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 02/08/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TipoResponsableLogic: IDisposable
	{
		TipoResponsableData oTipoResponsableData = null;
		public TipoResponsableLogic()
		{
			oTipoResponsableData = new TipoResponsableData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoResponsableGetList
		//Objetivo: Retorna una colección de registros de tipo TipoResponsableDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTipoResponsableDTO TipoResponsableGetList(ReqFilterTipoResponsableDTO oReqFilterTipoResponsableDTO)
		{
		
			RespListTipoResponsableDTO oRespListTipoResponsableDTO = new RespListTipoResponsableDTO();
		
			oRespListTipoResponsableDTO.List = new List<TipoResponsableDTO>();
			oRespListTipoResponsableDTO.User = oReqFilterTipoResponsableDTO.User;
			oRespListTipoResponsableDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTipoResponsableDTO.User))
            {
                oRespListTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoResponsable no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTipoResponsableDTO.Paging == null)
            {
                oRespListTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTipoResponsableDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterTipoResponsableDTO.Paging.All && oReqFilterTipoResponsableDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTipoResponsableDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TipoResponsableDTO> TipoResponsableDTOList = new List<TipoResponsableDTO>();

                    switch (oReqFilterTipoResponsableDTO.FilterCase)
                    {
                        default:
                            {
                                TipoResponsableDTOList = oTipoResponsableData.Listar(oReqFilterTipoResponsableDTO.Item);
                            }
                            break;
                    }

                    oRespListTipoResponsableDTO.List = TipoResponsableDTOList;
                    oRespListTipoResponsableDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTipoResponsableDTO.Success = false;
                    oRespListTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTipoResponsableDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoResponsableGetItem
		//Objetivo: Retorna un registro de tipo TipoResponsableDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTipoResponsableDTO TipoResponsableGetItem(ReqFilterTipoResponsableDTO oReqFilterTipoResponsableDTO)
		{
			RespItemTipoResponsableDTO oRespItemTipoResponsableDTO = new RespItemTipoResponsableDTO();

            oRespItemTipoResponsableDTO.Success = false;
            oRespItemTipoResponsableDTO.Item = null;
            oRespItemTipoResponsableDTO.User = oReqFilterTipoResponsableDTO.User;
            oRespItemTipoResponsableDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTipoResponsableDTO.User))
            {
                oRespItemTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoResponsable no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTipoResponsableDTO.MessageList.Count == 0)
            {
                TipoResponsableDTO oTipoResponsableDTO = null;
                try
                {
                    switch (oReqFilterTipoResponsableDTO.FilterCase)
                    {
                       
                        case filterCaseTipoResponsable.porCodigo:
                            {
                                oTipoResponsableDTO = new TipoResponsableDTO();
                                oTipoResponsableDTO = oTipoResponsableData.BuscarPorCodigoTipoResponsable(oReqFilterTipoResponsableDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTipoResponsableDTO = new TipoResponsableDTO();
                            }
                            break;
                    }

                    oRespItemTipoResponsableDTO.Item = new TipoResponsableDTO();
                    oRespItemTipoResponsableDTO.Item = oTipoResponsableDTO;
                    oRespItemTipoResponsableDTO.Success = true;
                    oRespItemTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTipoResponsableDTO.Success = false;
                    oRespItemTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTipoResponsableDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TipoResponsableDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTipoResponsableDTO ExecuteTransac(ReqTipoResponsableDTO oReqTipoResponsableDTO)
		{
			RespTipoResponsableDTO oRespTipoResponsableDTO = new RespTipoResponsableDTO();

            oRespTipoResponsableDTO.MessageList = new List<Mensaje>();
            oRespTipoResponsableDTO.User = oReqTipoResponsableDTO.User;
            
            if (String.IsNullOrEmpty(oReqTipoResponsableDTO.User))
            {
                oRespTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoResponsable no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTipoResponsableDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TipoResponsableDTO item in oReqTipoResponsableDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTipoResponsableData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oTipoResponsableData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oTipoResponsableData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTipoResponsableDTO.Success = true;
                        oRespTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTipoResponsableDTO.Success = false;
                        oRespTipoResponsableDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTipoResponsableDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
