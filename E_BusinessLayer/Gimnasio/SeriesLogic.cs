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
	//Archivo     : SeriesLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 29/06/2015
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class SeriesLogic: IDisposable
	{
		SeriesData oSeriesData = null;
		public SeriesLogic()
		{
			oSeriesData = new SeriesData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SeriesGetList
		//Objetivo: Retorna una colección de registros de tipo SeriesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListSeriesDTO SeriesGetList(ReqFilterSeriesDTO oReqFilterSeriesDTO)
		{
		
			RespListSeriesDTO oRespListSeriesDTO = new RespListSeriesDTO();
		
			oRespListSeriesDTO.List = new List<SeriesDTO>();
			oRespListSeriesDTO.User = oReqFilterSeriesDTO.User;
			oRespListSeriesDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterSeriesDTO.User))
            {
                oRespListSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Series no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterSeriesDTO.Paging == null)
            {
                oRespListSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListSeriesDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    if (!oReqFilterSeriesDTO.Paging.All && oReqFilterSeriesDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterSeriesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<SeriesDTO> SeriesDTOList = new List<SeriesDTO>();

                    switch (oReqFilterSeriesDTO.FilterCase)
                    {
                        default:
                            {
                                SeriesDTOList = oSeriesData.Listar(oReqFilterSeriesDTO.Item);
                            }
                            break;
                    }

                    oRespListSeriesDTO.List = SeriesDTOList;
                    oRespListSeriesDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListSeriesDTO.Success = false;
                    oRespListSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListSeriesDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SeriesGetItem
		//Objetivo: Retorna un registro de tipo SeriesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemSeriesDTO SeriesGetItem(ReqFilterSeriesDTO oReqFilterSeriesDTO)
		{
			RespItemSeriesDTO oRespItemSeriesDTO = new RespItemSeriesDTO();

            oRespItemSeriesDTO.Success = false;
            oRespItemSeriesDTO.Item = null;
            oRespItemSeriesDTO.User = oReqFilterSeriesDTO.User;
            oRespItemSeriesDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterSeriesDTO.User))
            {
                oRespItemSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Series no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemSeriesDTO.MessageList.Count == 0)
            {
                SeriesDTO oSeriesDTO = null;
                try
                {
                    switch (oReqFilterSeriesDTO.FilterCase)
                    {
                        case filterCaseSeries.BuscarPorCodigo:
                            {
                                oSeriesDTO = new SeriesDTO();
                                oSeriesDTO = oSeriesData.BuscarPorCodigoSeries(oReqFilterSeriesDTO.Item);
                            }
                            break;
                        case filterCaseSeries.BuscarGenerarCorrelativo:
                            {
                                oSeriesDTO = new SeriesDTO();
                                oSeriesDTO = oSeriesData.BuscarGenerarCorrelativo(oReqFilterSeriesDTO.Item);
                            }
                            break;
                        default:
                            {
                                oSeriesDTO = new SeriesDTO();
                            }
                            break;
                    }

                    oRespItemSeriesDTO.Item = new SeriesDTO();
                    oRespItemSeriesDTO.Item = oSeriesDTO;
                    oRespItemSeriesDTO.Success = true;
                    oRespItemSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemSeriesDTO.Success = false;
                    oRespItemSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemSeriesDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo SeriesDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespSeriesDTO ExecuteTransac(ReqSeriesDTO oReqSeriesDTO)
		{
			RespSeriesDTO oRespSeriesDTO = new RespSeriesDTO();

            oRespSeriesDTO.MessageList = new List<Mensaje>();
            oRespSeriesDTO.User = oReqSeriesDTO.User;
            
            if (String.IsNullOrEmpty(oReqSeriesDTO.User))
            {
                oRespSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Series no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespSeriesDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (SeriesDTO item in oReqSeriesDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oSeriesData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oSeriesData.Actualizar(item);
                                    break;
                                case Operation.UpdateCorrelativo:
                                    oSeriesData.ActualizarSerieAumentar(item);
                                    break;
                                case Operation.Delete:
                                    oSeriesData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespSeriesDTO.Success = true;
                        oRespSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespSeriesDTO.Success = false;
                        oRespSeriesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespSeriesDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
