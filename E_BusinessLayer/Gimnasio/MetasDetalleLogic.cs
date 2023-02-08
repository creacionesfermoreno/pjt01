
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
	//Archivo     : MetasDetalleLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 13/05/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class MetasDetalleLogic: IDisposable
	{
		MetasDetalleData oMetasDetalleData = null;
		public MetasDetalleLogic()
		{
			oMetasDetalleData = new MetasDetalleData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	MetasDetalleGetList
		//Objetivo: Retorna una colección de registros de tipo MetasDetalleDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListMetasDetalleDTO MetasDetalleGetList(ReqFilterMetasDetalleDTO oReqFilterMetasDetalleDTO)
		{
		
			RespListMetasDetalleDTO oRespListMetasDetalleDTO = new RespListMetasDetalleDTO();
		
			oRespListMetasDetalleDTO.List = new List<MetasDetalleDTO>();
			oRespListMetasDetalleDTO.User = oReqFilterMetasDetalleDTO.User;
			oRespListMetasDetalleDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterMetasDetalleDTO.User))
            {
                oRespListMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MetasDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterMetasDetalleDTO.Paging == null)
            {
                oRespListMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListMetasDetalleDTO.MessageList.Count == 0)
            {
                
                try
                {
                   
                    
                    if (!oReqFilterMetasDetalleDTO.Paging.All && oReqFilterMetasDetalleDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterMetasDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<MetasDetalleDTO> MetasDetalleDTOList = new List<MetasDetalleDTO>();
                    switch (oReqFilterMetasDetalleDTO.FilterCase)
                    {
                        case filterCaseMetasDetalle.uspListarMetasDetalle:
                            MetasDetalleDTOList = oMetasDetalleData.uspListarMetasDetalle(oReqFilterMetasDetalleDTO.Item);
                            break;
                        default:
                            break;
                    }
                    switch (oReqFilterMetasDetalleDTO.FilterCase)
                    {
                        default:
                            {
                               // MetasDetalleDTOList = oMetasDetalleData.Listar(oReqFilterMetasDetalleDTO.Item, oReqFilterMetasDetalleDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListMetasDetalleDTO.List = MetasDetalleDTOList;
                    oRespListMetasDetalleDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListMetasDetalleDTO.Success = false;
                    oRespListMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListMetasDetalleDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	MetasDetalleGetItem
		//Objetivo: Retorna un registro de tipo MetasDetalleDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemMetasDetalleDTO MetasDetalleGetItem(ReqFilterMetasDetalleDTO oReqFilterMetasDetalleDTO)
		{
			RespItemMetasDetalleDTO oRespItemMetasDetalleDTO = new RespItemMetasDetalleDTO();

            oRespItemMetasDetalleDTO.Success = false;
            oRespItemMetasDetalleDTO.Item = null;
            oRespItemMetasDetalleDTO.User = oReqFilterMetasDetalleDTO.User;
            oRespItemMetasDetalleDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterMetasDetalleDTO.User))
            {
                oRespItemMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MetasDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemMetasDetalleDTO.MessageList.Count == 0)
            {
                MetasDetalleDTO oMetasDetalleDTO = null;
                try
                {
                    switch (oReqFilterMetasDetalleDTO.FilterCase)
                    {
                       
                        //case filterCaseMetasDetalle.porCodigo:
                        //    {
                        //        oMetasDetalleDTO = new MetasDetalleDTO();
                        //        oMetasDetalleDTO = oMetasDetalleData.BuscarPorCodigoMetasDetalle(oReqFilterMetasDetalleDTO.Item);
                        //    }
                        //    break;
                        default:
                            {
                                oMetasDetalleDTO = new MetasDetalleDTO();
                            }
                            break;
                    }

                    oRespItemMetasDetalleDTO.Item = new MetasDetalleDTO();
                    oRespItemMetasDetalleDTO.Item = oMetasDetalleDTO;
                    oRespItemMetasDetalleDTO.Success = true;
                    oRespItemMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemMetasDetalleDTO.Success = false;
                    oRespItemMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemMetasDetalleDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo MetasDetalleDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespMetasDetalleDTO ExecuteTransac(ReqMetasDetalleDTO oReqMetasDetalleDTO)
		{
			RespMetasDetalleDTO oRespMetasDetalleDTO = new RespMetasDetalleDTO();

            oRespMetasDetalleDTO.MessageList = new List<Mensaje>();
            oRespMetasDetalleDTO.User = oReqMetasDetalleDTO.User;
            
            if (String.IsNullOrEmpty(oReqMetasDetalleDTO.User))
            {
                oRespMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de MetasDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespMetasDetalleDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (MetasDetalleDTO item in oReqMetasDetalleDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oMetasDetalleData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oMetasDetalleData.Actualizar(item);
                                    break;                              
                            }
                        }
                        tx.Complete();
                        oRespMetasDetalleDTO.Success = true;
                        oRespMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespMetasDetalleDTO.Success = false;
                        oRespMetasDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespMetasDetalleDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
