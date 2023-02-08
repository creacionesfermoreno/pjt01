
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
	//Archivo     : TiempoMembresiaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 14/01/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TiempoMembresiaLogic: IDisposable
	{
		TiempoMembresiaData oTiempoMembresiaData = null;
		public TiempoMembresiaLogic()
		{
			oTiempoMembresiaData = new TiempoMembresiaData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TiempoMembresiaGetList
		//Objetivo: Retorna una colección de registros de tipo TiempoMembresiaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTiempoMembresiaDTO TiempoMembresiaGetList(ReqFilterTiempoMembresiaDTO oReqFilterTiempoMembresiaDTO)
		{
		
			RespListTiempoMembresiaDTO oRespListTiempoMembresiaDTO = new RespListTiempoMembresiaDTO();
		
			oRespListTiempoMembresiaDTO.List = new List<TiempoMembresiaDTO>();
			oRespListTiempoMembresiaDTO.User = oReqFilterTiempoMembresiaDTO.User;
			oRespListTiempoMembresiaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTiempoMembresiaDTO.User))
            {
                oRespListTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TiempoMembresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTiempoMembresiaDTO.Paging == null)
            {
                oRespListTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTiempoMembresiaDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterTiempoMembresiaDTO.Paging.All && oReqFilterTiempoMembresiaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTiempoMembresiaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TiempoMembresiaDTO> TiempoMembresiaDTOList = new List<TiempoMembresiaDTO>();

                    switch (oReqFilterTiempoMembresiaDTO.FilterCase)
                    {
                        default:
                            {
                                TiempoMembresiaDTOList = oTiempoMembresiaData.Listar(oReqFilterTiempoMembresiaDTO.Item);
                            }
                            break;
                    }

                    oRespListTiempoMembresiaDTO.List = TiempoMembresiaDTOList;
                    oRespListTiempoMembresiaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTiempoMembresiaDTO.Success = false;
                    oRespListTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTiempoMembresiaDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TiempoMembresiaGetItem
		//Objetivo: Retorna un registro de tipo TiempoMembresiaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTiempoMembresiaDTO TiempoMembresiaGetItem(ReqFilterTiempoMembresiaDTO oReqFilterTiempoMembresiaDTO)
		{
			RespItemTiempoMembresiaDTO oRespItemTiempoMembresiaDTO = new RespItemTiempoMembresiaDTO();

            oRespItemTiempoMembresiaDTO.Success = false;
            oRespItemTiempoMembresiaDTO.Item = null;
            oRespItemTiempoMembresiaDTO.User = oReqFilterTiempoMembresiaDTO.User;
            oRespItemTiempoMembresiaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTiempoMembresiaDTO.User))
            {
                oRespItemTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TiempoMembresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTiempoMembresiaDTO.MessageList.Count == 0)
            {
                TiempoMembresiaDTO oTiempoMembresiaDTO = null;
                try
                {
                    switch (oReqFilterTiempoMembresiaDTO.FilterCase)
                    {
                       
                        default:
                            {
                                oTiempoMembresiaDTO = new TiempoMembresiaDTO();
                            }
                            break;
                    }

                    oRespItemTiempoMembresiaDTO.Item = new TiempoMembresiaDTO();
                    oRespItemTiempoMembresiaDTO.Item = oTiempoMembresiaDTO;
                    oRespItemTiempoMembresiaDTO.Success = true;
                    oRespItemTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTiempoMembresiaDTO.Success = false;
                    oRespItemTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTiempoMembresiaDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TiempoMembresiaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTiempoMembresiaDTO ExecuteTransac(ReqTiempoMembresiaDTO oReqTiempoMembresiaDTO)
		{
			RespTiempoMembresiaDTO oRespTiempoMembresiaDTO = new RespTiempoMembresiaDTO();

            oRespTiempoMembresiaDTO.MessageList = new List<Mensaje>();
            oRespTiempoMembresiaDTO.User = oReqTiempoMembresiaDTO.User;
            
            if (String.IsNullOrEmpty(oReqTiempoMembresiaDTO.User))
            {
                oRespTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TiempoMembresia no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTiempoMembresiaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TiempoMembresiaDTO item in oReqTiempoMembresiaDTO.List)
                        {
                            //switch (item.Operation)
                            //{
                              
                            //}
                        }
                        tx.Complete();
                        oRespTiempoMembresiaDTO.Success = true;
                        oRespTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTiempoMembresiaDTO.Success = false;
                        oRespTiempoMembresiaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTiempoMembresiaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
