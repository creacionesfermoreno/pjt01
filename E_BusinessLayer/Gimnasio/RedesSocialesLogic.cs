
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
	//Archivo     : RedesSocialesLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 14/11/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class RedesSocialesLogic: IDisposable
	{
		RedesSocialesData oRedesSocialesData = null;
		public RedesSocialesLogic()
		{
			oRedesSocialesData = new RedesSocialesData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	RedesSocialesGetList
		//Objetivo: Retorna una colección de registros de tipo RedesSocialesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListRedesSocialesDTO RedesSocialesGetList(ReqFilterRedesSocialesDTO oReqFilterRedesSocialesDTO)
		{
		
			RespListRedesSocialesDTO oRespListRedesSocialesDTO = new RespListRedesSocialesDTO();
		
			oRespListRedesSocialesDTO.List = new List<RedesSocialesDTO>();
			oRespListRedesSocialesDTO.User = oReqFilterRedesSocialesDTO.User;
			oRespListRedesSocialesDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterRedesSocialesDTO.User))
            {
                oRespListRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de RedesSociales no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterRedesSocialesDTO.Paging == null)
            {
                oRespListRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListRedesSocialesDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterRedesSocialesDTO.Paging.All && oReqFilterRedesSocialesDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterRedesSocialesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<RedesSocialesDTO> RedesSocialesDTOList = new List<RedesSocialesDTO>();

                    switch (oReqFilterRedesSocialesDTO.FilterCase)
                    {
                        default:
                            {
                               
                            }
                            break;
                    }

                    oRespListRedesSocialesDTO.List = RedesSocialesDTOList;
                    oRespListRedesSocialesDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListRedesSocialesDTO.Success = false;
                    oRespListRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListRedesSocialesDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	RedesSocialesGetItem
		//Objetivo: Retorna un registro de tipo RedesSocialesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemRedesSocialesDTO RedesSocialesGetItem(ReqFilterRedesSocialesDTO oReqFilterRedesSocialesDTO)
		{
			RespItemRedesSocialesDTO oRespItemRedesSocialesDTO = new RespItemRedesSocialesDTO();

            oRespItemRedesSocialesDTO.Success = false;
            oRespItemRedesSocialesDTO.Item = null;
            oRespItemRedesSocialesDTO.User = oReqFilterRedesSocialesDTO.User;
            oRespItemRedesSocialesDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterRedesSocialesDTO.User))
            {
                oRespItemRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de RedesSociales no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemRedesSocialesDTO.MessageList.Count == 0)
            {
                RedesSocialesDTO oRedesSocialesDTO = null;
                try
                {
                    switch (oReqFilterRedesSocialesDTO.FilterCase)
                    {
                       
                        case filterCaseRedesSociales.uspBuscarRedesSocialesPorCodigo:
                            {
                                oRedesSocialesDTO = new RedesSocialesDTO();
                                oRedesSocialesDTO = oRedesSocialesData.BuscarPorCodigoRedesSociales(oReqFilterRedesSocialesDTO.Item);
                            }
                            break;
                        default:
                            {
                                oRedesSocialesDTO = new RedesSocialesDTO();
                            }
                            break;
                    }

                    oRespItemRedesSocialesDTO.Item = new RedesSocialesDTO();
                    oRespItemRedesSocialesDTO.Item = oRedesSocialesDTO;
                    oRespItemRedesSocialesDTO.Success = true;
                    oRespItemRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemRedesSocialesDTO.Success = false;
                    oRespItemRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemRedesSocialesDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo RedesSocialesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespRedesSocialesDTO ExecuteTransac(ReqRedesSocialesDTO oReqRedesSocialesDTO)
		{
			RespRedesSocialesDTO oRespRedesSocialesDTO = new RespRedesSocialesDTO();

            oRespRedesSocialesDTO.MessageList = new List<Mensaje>();
            oRespRedesSocialesDTO.User = oReqRedesSocialesDTO.User;
            
            if (String.IsNullOrEmpty(oReqRedesSocialesDTO.User))
            {
                oRespRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de RedesSociales no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespRedesSocialesDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (RedesSocialesDTO item in oReqRedesSocialesDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oRedesSocialesData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oRedesSocialesData.Actualizar(item);
                                    break;
                              
                            }
                        }
                        tx.Complete();
                        oRespRedesSocialesDTO.Success = true;
                        oRespRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespRedesSocialesDTO.Success = false;
                        oRespRedesSocialesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespRedesSocialesDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
