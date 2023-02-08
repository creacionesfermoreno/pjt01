
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
	//Archivo     : CategoriaSuplementoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 17/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class CategoriaSuplementoLogic: IDisposable
	{
		CategoriaSuplementoData oCategoriaSuplementoData = null;
		public CategoriaSuplementoLogic()
		{
			oCategoriaSuplementoData = new CategoriaSuplementoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	CategoriaSuplementoGetList
		//Objetivo: Retorna una colección de registros de tipo CategoriaSuplementoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListCategoriaSuplementoDTO CategoriaSuplementoGetList(ReqFilterCategoriaSuplementoDTO oReqFilterCategoriaSuplementoDTO)
		{
		
			RespListCategoriaSuplementoDTO oRespListCategoriaSuplementoDTO = new RespListCategoriaSuplementoDTO();
		
			oRespListCategoriaSuplementoDTO.List = new List<CategoriaSuplementoDTO>();
			oRespListCategoriaSuplementoDTO.User = oReqFilterCategoriaSuplementoDTO.User;
			oRespListCategoriaSuplementoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterCategoriaSuplementoDTO.User))
            {
                oRespListCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de CategoriaSuplemento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterCategoriaSuplementoDTO.Paging == null)
            {
                oRespListCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListCategoriaSuplementoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterCategoriaSuplementoDTO.Paging.All && oReqFilterCategoriaSuplementoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterCategoriaSuplementoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<CategoriaSuplementoDTO> CategoriaSuplementoDTOList = new List<CategoriaSuplementoDTO>();

                    switch (oReqFilterCategoriaSuplementoDTO.FilterCase)
                    {
                        default:
                            {
                                CategoriaSuplementoDTOList = oCategoriaSuplementoData.Listar(oReqFilterCategoriaSuplementoDTO.Item);
                            }
                            break;
                    }

                    oRespListCategoriaSuplementoDTO.List = CategoriaSuplementoDTOList;
                    oRespListCategoriaSuplementoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListCategoriaSuplementoDTO.Success = false;
                    oRespListCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListCategoriaSuplementoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	CategoriaSuplementoGetItem
		//Objetivo: Retorna un registro de tipo CategoriaSuplementoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemCategoriaSuplementoDTO CategoriaSuplementoGetItem(ReqFilterCategoriaSuplementoDTO oReqFilterCategoriaSuplementoDTO)
		{
			RespItemCategoriaSuplementoDTO oRespItemCategoriaSuplementoDTO = new RespItemCategoriaSuplementoDTO();

            oRespItemCategoriaSuplementoDTO.Success = false;
            oRespItemCategoriaSuplementoDTO.Item = null;
            oRespItemCategoriaSuplementoDTO.User = oReqFilterCategoriaSuplementoDTO.User;
            oRespItemCategoriaSuplementoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterCategoriaSuplementoDTO.User))
            {
                oRespItemCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de CategoriaSuplemento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemCategoriaSuplementoDTO.MessageList.Count == 0)
            {
                CategoriaSuplementoDTO oCategoriaSuplementoDTO = null;
                try
                {
                    switch (oReqFilterCategoriaSuplementoDTO.FilterCase)
                    {
                       
                        case filterCaseCategoriaSuplemento.porCodigo:
                            {
                                oCategoriaSuplementoDTO = new CategoriaSuplementoDTO();
                       
                            }
                            break;
                        default:
                            {
                                oCategoriaSuplementoDTO = new CategoriaSuplementoDTO();
                            }
                            break;
                    }

                    oRespItemCategoriaSuplementoDTO.Item = new CategoriaSuplementoDTO();
                    oRespItemCategoriaSuplementoDTO.Item = oCategoriaSuplementoDTO;
                    oRespItemCategoriaSuplementoDTO.Success = true;
                    oRespItemCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemCategoriaSuplementoDTO.Success = false;
                    oRespItemCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemCategoriaSuplementoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo CategoriaSuplementoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespCategoriaSuplementoDTO ExecuteTransac(ReqCategoriaSuplementoDTO oReqCategoriaSuplementoDTO)
		{
			RespCategoriaSuplementoDTO oRespCategoriaSuplementoDTO = new RespCategoriaSuplementoDTO();

            oRespCategoriaSuplementoDTO.MessageList = new List<Mensaje>();
            oRespCategoriaSuplementoDTO.User = oReqCategoriaSuplementoDTO.User;
            
            if (String.IsNullOrEmpty(oReqCategoriaSuplementoDTO.User))
            {
                oRespCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de CategoriaSuplemento no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespCategoriaSuplementoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (CategoriaSuplementoDTO item in oReqCategoriaSuplementoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                   // oCategoriaSuplementoData.Registrar(item);
                                    break;
                                case Operation.Update:
                                  //  oCategoriaSuplementoData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                  //  oCategoriaSuplementoData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespCategoriaSuplementoDTO.Success = true;
                        oRespCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespCategoriaSuplementoDTO.Success = false;
                        oRespCategoriaSuplementoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespCategoriaSuplementoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
