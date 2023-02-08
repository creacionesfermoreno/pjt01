
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
	//Archivo     : HorarioPaqueteDetalleLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 07/11/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class HorarioPaqueteDetalleLogic: IDisposable
	{
		HorarioPaqueteDetalleData oHorarioPaqueteDetalleData = null;
		public HorarioPaqueteDetalleLogic()
		{
			oHorarioPaqueteDetalleData = new HorarioPaqueteDetalleData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioPaqueteDetalleGetList
		//Objetivo: Retorna una colección de registros de tipo HorarioPaqueteDetalleDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListHorarioPaqueteDetalleDTO HorarioPaqueteDetalleGetList(ReqFilterHorarioPaqueteDetalleDTO oReqFilterHorarioPaqueteDetalleDTO)
		{
		
			RespListHorarioPaqueteDetalleDTO oRespListHorarioPaqueteDetalleDTO = new RespListHorarioPaqueteDetalleDTO();
		
			oRespListHorarioPaqueteDetalleDTO.List = new List<HorarioPaqueteDetalleDTO>();
			oRespListHorarioPaqueteDetalleDTO.User = oReqFilterHorarioPaqueteDetalleDTO.User;
			oRespListHorarioPaqueteDetalleDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterHorarioPaqueteDetalleDTO.User))
            {
                oRespListHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPaqueteDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterHorarioPaqueteDetalleDTO.Paging == null)
            {
                oRespListHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListHorarioPaqueteDetalleDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterHorarioPaqueteDetalleDTO.Paging.All && oReqFilterHorarioPaqueteDetalleDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterHorarioPaqueteDetalleDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<HorarioPaqueteDetalleDTO> HorarioPaqueteDetalleDTOList = new List<HorarioPaqueteDetalleDTO>();

                    switch (oReqFilterHorarioPaqueteDetalleDTO.FilterCase)
                    {

                        case filterCaseHorarioPaqueteDetalle.uspListarHorasCurso:
                            {
                                HorarioPaqueteDetalleDTOList = oHorarioPaqueteDetalleData.uspListarHorasCurso(oReqFilterHorarioPaqueteDetalleDTO.Item);
                            }
                        break;
                        case filterCaseHorarioPaqueteDetalle.Filter_uspListarHoraPaquete:
                            {
                                HorarioPaqueteDetalleDTOList = oHorarioPaqueteDetalleData.uspListarHoraPaquete(oReqFilterHorarioPaqueteDetalleDTO.Item);
                            }
                        break;
              
                    }

                    oRespListHorarioPaqueteDetalleDTO.List = HorarioPaqueteDetalleDTOList;
                    oRespListHorarioPaqueteDetalleDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListHorarioPaqueteDetalleDTO.Success = false;
                    oRespListHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListHorarioPaqueteDetalleDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	HorarioPaqueteDetalleGetItem
		//Objetivo: Retorna un registro de tipo HorarioPaqueteDetalleDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemHorarioPaqueteDetalleDTO HorarioPaqueteDetalleGetItem(ReqFilterHorarioPaqueteDetalleDTO oReqFilterHorarioPaqueteDetalleDTO)
		{
			RespItemHorarioPaqueteDetalleDTO oRespItemHorarioPaqueteDetalleDTO = new RespItemHorarioPaqueteDetalleDTO();

            oRespItemHorarioPaqueteDetalleDTO.Success = false;
            oRespItemHorarioPaqueteDetalleDTO.Item = null;
            oRespItemHorarioPaqueteDetalleDTO.User = oReqFilterHorarioPaqueteDetalleDTO.User;
            oRespItemHorarioPaqueteDetalleDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterHorarioPaqueteDetalleDTO.User))
            {
                oRespItemHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPaqueteDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemHorarioPaqueteDetalleDTO.MessageList.Count == 0)
            {
                HorarioPaqueteDetalleDTO oHorarioPaqueteDetalleDTO = null;
                try
                {
                    switch (oReqFilterHorarioPaqueteDetalleDTO.FilterCase)
                    {
                       
                        default:
                            {
                                oHorarioPaqueteDetalleDTO = new HorarioPaqueteDetalleDTO();
                            }
                            break;
                    }

                    oRespItemHorarioPaqueteDetalleDTO.Item = new HorarioPaqueteDetalleDTO();
                    oRespItemHorarioPaqueteDetalleDTO.Item = oHorarioPaqueteDetalleDTO;
                    oRespItemHorarioPaqueteDetalleDTO.Success = true;
                    oRespItemHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemHorarioPaqueteDetalleDTO.Success = false;
                    oRespItemHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemHorarioPaqueteDetalleDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo HorarioPaqueteDetalleDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespHorarioPaqueteDetalleDTO ExecuteTransac(ReqHorarioPaqueteDetalleDTO oReqHorarioPaqueteDetalleDTO)
		{
			RespHorarioPaqueteDetalleDTO oRespHorarioPaqueteDetalleDTO = new RespHorarioPaqueteDetalleDTO();

            oRespHorarioPaqueteDetalleDTO.MessageList = new List<Mensaje>();
            oRespHorarioPaqueteDetalleDTO.User = oReqHorarioPaqueteDetalleDTO.User;
            
            if (String.IsNullOrEmpty(oReqHorarioPaqueteDetalleDTO.User))
            {
                oRespHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de HorarioPaqueteDetalle no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespHorarioPaqueteDetalleDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (HorarioPaqueteDetalleDTO item in oReqHorarioPaqueteDetalleDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oHorarioPaqueteDetalleData.Registrar(item);
                                    break;                              
                                case Operation.Delete:
                                    oHorarioPaqueteDetalleData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespHorarioPaqueteDetalleDTO.Success = true;
                        oRespHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespHorarioPaqueteDetalleDTO.Success = false;
                        oRespHorarioPaqueteDetalleDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespHorarioPaqueteDetalleDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
