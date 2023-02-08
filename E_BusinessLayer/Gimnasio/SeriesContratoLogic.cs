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
	//Archivo     : SeriesContratoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/07/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class SeriesContratoLogic: IDisposable
	{
		SeriesContratoData oSeriesContratoData = null;
		public SeriesContratoLogic()
		{
			oSeriesContratoData = new SeriesContratoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SeriesContratoGetList
		//Objetivo: Retorna una colección de registros de tipo SeriesContratoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListSeriesContratoDTO SeriesContratoGetList(ReqFilterSeriesContratoDTO oReqFilterSeriesContratoDTO)
		{
		
			RespListSeriesContratoDTO oRespListSeriesContratoDTO = new RespListSeriesContratoDTO();
		
			oRespListSeriesContratoDTO.List = new List<SeriesContratoDTO>();
			oRespListSeriesContratoDTO.User = oReqFilterSeriesContratoDTO.User;
			oRespListSeriesContratoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterSeriesContratoDTO.User))
            {
                oRespListSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SeriesContrato no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterSeriesContratoDTO.Paging == null)
            {
                oRespListSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListSeriesContratoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterSeriesContratoDTO.Paging.All && oReqFilterSeriesContratoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterSeriesContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<SeriesContratoDTO> SeriesContratoDTOList = new List<SeriesContratoDTO>();

                    switch (oReqFilterSeriesContratoDTO.FilterCase)
                    {
                        default:
                            {
                                SeriesContratoDTOList = oSeriesContratoData.Listar(oReqFilterSeriesContratoDTO.Item);
                            }
                            break;
                    }

                    oRespListSeriesContratoDTO.List = SeriesContratoDTOList;
                    oRespListSeriesContratoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListSeriesContratoDTO.Success = false;
                    oRespListSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListSeriesContratoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SeriesContratoGetItem
		//Objetivo: Retorna un registro de tipo SeriesContratoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemSeriesContratoDTO SeriesContratoGetItem(ReqFilterSeriesContratoDTO oReqFilterSeriesContratoDTO)
		{
			RespItemSeriesContratoDTO oRespItemSeriesContratoDTO = new RespItemSeriesContratoDTO();

            oRespItemSeriesContratoDTO.Success = false;
            oRespItemSeriesContratoDTO.Item = null;
            oRespItemSeriesContratoDTO.User = oReqFilterSeriesContratoDTO.User;
            oRespItemSeriesContratoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterSeriesContratoDTO.User))
            {
                oRespItemSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SeriesContrato no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemSeriesContratoDTO.MessageList.Count == 0)
            {
                SeriesContratoDTO oSeriesContratoDTO = null;
                try
                {
                    switch (oReqFilterSeriesContratoDTO.FilterCase)
                    {
                       
                        case filterCaseSeriesContrato.BuscarPorCodigo:
                            {
                                oSeriesContratoDTO = new SeriesContratoDTO();
                                oSeriesContratoDTO = oSeriesContratoData.BuscarPorCodigoSeriesContrato(oReqFilterSeriesContratoDTO.Item);
                            }
                            break;
                        case filterCaseSeriesContrato.BuscarGenerarCorrelativo:
                            {
                                oSeriesContratoDTO = new SeriesContratoDTO();
                                oSeriesContratoDTO = oSeriesContratoData.BuscarGenerarCorrelativo(oReqFilterSeriesContratoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oSeriesContratoDTO = new SeriesContratoDTO();
                            }
                            break;
                    }

                    oRespItemSeriesContratoDTO.Item = new SeriesContratoDTO();
                    oRespItemSeriesContratoDTO.Item = oSeriesContratoDTO;
                    oRespItemSeriesContratoDTO.Success = true;
                    oRespItemSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemSeriesContratoDTO.Success = false;
                    oRespItemSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemSeriesContratoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo SeriesContratoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespSeriesContratoDTO ExecuteTransac(ReqSeriesContratoDTO oReqSeriesContratoDTO)
		{
			RespSeriesContratoDTO oRespSeriesContratoDTO = new RespSeriesContratoDTO();

            oRespSeriesContratoDTO.MessageList = new List<Mensaje>();
            oRespSeriesContratoDTO.User = oReqSeriesContratoDTO.User;
            
            if (String.IsNullOrEmpty(oReqSeriesContratoDTO.User))
            {
                oRespSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SeriesContrato no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespSeriesContratoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (SeriesContratoDTO item in oReqSeriesContratoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oSeriesContratoData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oSeriesContratoData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oSeriesContratoData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespSeriesContratoDTO.Success = true;
                        oRespSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespSeriesContratoDTO.Success = false;
                        oRespSeriesContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespSeriesContratoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
