
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
	//Archivo     : SubTipoDocumentoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 26/07/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class SubTipoDocumentoLogic: IDisposable
	{
		SubTipoDocumentoData oSubTipoDocumentoData = null;
		public SubTipoDocumentoLogic()
		{
			oSubTipoDocumentoData = new SubTipoDocumentoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SubTipoDocumentoGetList
		//Objetivo: Retorna una colección de registros de tipo SubTipoDocumentoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListSubTipoDocumentoDTO SubTipoDocumentoGetList(ReqFilterSubTipoDocumentoDTO oReqFilterSubTipoDocumentoDTO)
		{
		
			RespListSubTipoDocumentoDTO oRespListSubTipoDocumentoDTO = new RespListSubTipoDocumentoDTO();
		
			oRespListSubTipoDocumentoDTO.List = new List<SubTipoDocumentoDTO>();
			oRespListSubTipoDocumentoDTO.User = oReqFilterSubTipoDocumentoDTO.User;
			oRespListSubTipoDocumentoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterSubTipoDocumentoDTO.User))
            {
                oRespListSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SubTipoDocumento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterSubTipoDocumentoDTO.Paging == null)
            {
                oRespListSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListSubTipoDocumentoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterSubTipoDocumentoDTO.Paging.All && oReqFilterSubTipoDocumentoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterSubTipoDocumentoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<SubTipoDocumentoDTO> SubTipoDocumentoDTOList = new List<SubTipoDocumentoDTO>();

                    switch (oReqFilterSubTipoDocumentoDTO.FilterCase)
                    {
                        case filterCaseSubTipoDocumento.ListarPorTipoDocumento:
                            {
                                SubTipoDocumentoDTOList = oSubTipoDocumentoData.ListarPorTipoDocumento(oReqFilterSubTipoDocumentoDTO.Item);
                            }
                            break;
                        default:
                            {
                                SubTipoDocumentoDTOList = oSubTipoDocumentoData.Listar(oReqFilterSubTipoDocumentoDTO.Item);
                            }
                            break;
                    }

                    oRespListSubTipoDocumentoDTO.List = SubTipoDocumentoDTOList;
                    oRespListSubTipoDocumentoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListSubTipoDocumentoDTO.Success = false;
                    oRespListSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListSubTipoDocumentoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SubTipoDocumentoGetItem
		//Objetivo: Retorna un registro de tipo SubTipoDocumentoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemSubTipoDocumentoDTO SubTipoDocumentoGetItem(ReqFilterSubTipoDocumentoDTO oReqFilterSubTipoDocumentoDTO)
		{
			RespItemSubTipoDocumentoDTO oRespItemSubTipoDocumentoDTO = new RespItemSubTipoDocumentoDTO();

            oRespItemSubTipoDocumentoDTO.Success = false;
            oRespItemSubTipoDocumentoDTO.Item = null;
            oRespItemSubTipoDocumentoDTO.User = oReqFilterSubTipoDocumentoDTO.User;
            oRespItemSubTipoDocumentoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterSubTipoDocumentoDTO.User))
            {
                oRespItemSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SubTipoDocumento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemSubTipoDocumentoDTO.MessageList.Count == 0)
            {
                SubTipoDocumentoDTO oSubTipoDocumentoDTO = null;
                try
                {
                    switch (oReqFilterSubTipoDocumentoDTO.FilterCase)
                    {

                        case filterCaseSubTipoDocumento.BuscarPorCodigo:
                            {
                                oSubTipoDocumentoDTO = new SubTipoDocumentoDTO();
                                oSubTipoDocumentoDTO = oSubTipoDocumentoData.BuscarPorCodigoSubTipoDocumento(oReqFilterSubTipoDocumentoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oSubTipoDocumentoDTO = new SubTipoDocumentoDTO();
                            }
                            break;
                    }

                    oRespItemSubTipoDocumentoDTO.Item = new SubTipoDocumentoDTO();
                    oRespItemSubTipoDocumentoDTO.Item = oSubTipoDocumentoDTO;
                    oRespItemSubTipoDocumentoDTO.Success = true;
                    oRespItemSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemSubTipoDocumentoDTO.Success = false;
                    oRespItemSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemSubTipoDocumentoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo SubTipoDocumentoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespSubTipoDocumentoDTO ExecuteTransac(ReqSubTipoDocumentoDTO oReqSubTipoDocumentoDTO)
		{
			RespSubTipoDocumentoDTO oRespSubTipoDocumentoDTO = new RespSubTipoDocumentoDTO();

            oRespSubTipoDocumentoDTO.MessageList = new List<Mensaje>();
            oRespSubTipoDocumentoDTO.User = oReqSubTipoDocumentoDTO.User;
            
            if (String.IsNullOrEmpty(oReqSubTipoDocumentoDTO.User))
            {
                oRespSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SubTipoDocumento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespSubTipoDocumentoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (SubTipoDocumentoDTO item in oReqSubTipoDocumentoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oSubTipoDocumentoData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oSubTipoDocumentoData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oSubTipoDocumentoData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespSubTipoDocumentoDTO.Success = true;
                        oRespSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespSubTipoDocumentoDTO.Success = false;
                        oRespSubTipoDocumentoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespSubTipoDocumentoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
