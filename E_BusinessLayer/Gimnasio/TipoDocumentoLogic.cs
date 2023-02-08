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
	//-------------------------------------------------------------------
	//Archivo     : TipoDocumentoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 17/09/2014
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TipoDocumentoLogic: IDisposable
	{
		TipoDocumentoData oTipoDocumentoData = null;
		public TipoDocumentoLogic()
		{
			oTipoDocumentoData = new TipoDocumentoData();
		}
        
		//-------------------------------------------------------------------
		//Nombre:	TipoDocumentoGetList
		//Objetivo: Retorna una colección de registros de tipo TipoDocumentoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTipoDocumentoDTO TipoDocumentoGetList(ReqFilterTipoDocumentoDTO oReqFilterTipoDocumentoDTO)
		{
		
			RespListTipoDocumentoDTO oRespListTipoDocumentoDTO = new RespListTipoDocumentoDTO();
		
			oRespListTipoDocumentoDTO.List = new List<TipoDocumentoDTO>();
			oRespListTipoDocumentoDTO.User = oReqFilterTipoDocumentoDTO.User;
			oRespListTipoDocumentoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTipoDocumentoDTO.User))
            {
                oRespListTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoDocumento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTipoDocumentoDTO.Paging == null)
            {
                oRespListTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            
			if (oRespListTipoDocumentoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterTipoDocumentoDTO.Paging.All && oReqFilterTipoDocumentoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTipoDocumentoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TipoDocumentoDTO> TipoDocumentoDTOList = new List<TipoDocumentoDTO>();

                    switch (oReqFilterTipoDocumentoDTO.FilterCase)
                    {
                        default:
                            {
                                TipoDocumentoDTOList = oTipoDocumentoData.Listar();
                            }
                            break;
                    }

                    oRespListTipoDocumentoDTO.List = TipoDocumentoDTOList;
                    oRespListTipoDocumentoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTipoDocumentoDTO.Success = false;
                    oRespListTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTipoDocumentoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TipoDocumentoGetItem
		//Objetivo: Retorna un registro de tipo TipoDocumentoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTipoDocumentoDTO TipoDocumentoGetItem(ReqFilterTipoDocumentoDTO oReqFilterTipoDocumentoDTO)
		{
			RespItemTipoDocumentoDTO oRespItemTipoDocumentoDTO = new RespItemTipoDocumentoDTO();

            oRespItemTipoDocumentoDTO.Success = false;
            oRespItemTipoDocumentoDTO.Item = null;
            oRespItemTipoDocumentoDTO.User = oReqFilterTipoDocumentoDTO.User;
            oRespItemTipoDocumentoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTipoDocumentoDTO.User))
            {
                oRespItemTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoDocumento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTipoDocumentoDTO.MessageList.Count == 0)
            {
                TipoDocumentoDTO oTipoDocumentoDTO = null;
                try
                {
                    switch (oReqFilterTipoDocumentoDTO.FilterCase)
                    {
                       
                        case filterCaseTipoDocumento.BuscarPorCodigo:
                            {
                                oTipoDocumentoDTO = new TipoDocumentoDTO();
                                oTipoDocumentoDTO = oTipoDocumentoData.BuscarPorCodigoTipoDocumento(oReqFilterTipoDocumentoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTipoDocumentoDTO = new TipoDocumentoDTO();
                            }
                            break;
                    }

                    oRespItemTipoDocumentoDTO.Item = new TipoDocumentoDTO();
                    oRespItemTipoDocumentoDTO.Item = oTipoDocumentoDTO;
                    oRespItemTipoDocumentoDTO.Success = true;
                    oRespItemTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTipoDocumentoDTO.Success = false;
                    oRespItemTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTipoDocumentoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TipoDocumentoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTipoDocumentoDTO ExecuteTransac(ReqTipoDocumentoDTO oReqTipoDocumentoDTO)
		{
			RespTipoDocumentoDTO oRespTipoDocumentoDTO = new RespTipoDocumentoDTO();

            oRespTipoDocumentoDTO.MessageList = new List<Mensaje>();
            oRespTipoDocumentoDTO.User = oReqTipoDocumentoDTO.User;
            
            if (String.IsNullOrEmpty(oReqTipoDocumentoDTO.User))
            {
                oRespTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TipoDocumento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTipoDocumentoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TipoDocumentoDTO item in oReqTipoDocumentoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTipoDocumentoData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oTipoDocumentoData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oTipoDocumentoData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTipoDocumentoDTO.Success = true;
                        oRespTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTipoDocumentoDTO.Success = false;
                        oRespTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTipoDocumentoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
