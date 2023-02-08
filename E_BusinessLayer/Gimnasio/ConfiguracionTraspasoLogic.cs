
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
	//Archivo     : ConfiguracionTraspasoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/09/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ConfiguracionTraspasoLogic: IDisposable
	{
		ConfiguracionTraspasoData oConfiguracionTraspasoData = null;
		public ConfiguracionTraspasoLogic()
		{
			oConfiguracionTraspasoData = new ConfiguracionTraspasoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ConfiguracionTraspasoGetList
		//Objetivo: Retorna una colección de registros de tipo ConfiguracionTraspasoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListConfiguracionTraspasoDTO ConfiguracionTraspasoGetList(ReqFilterConfiguracionTraspasoDTO oReqFilterConfiguracionTraspasoDTO)
		{
		
			RespListConfiguracionTraspasoDTO oRespListConfiguracionTraspasoDTO = new RespListConfiguracionTraspasoDTO();
		
			oRespListConfiguracionTraspasoDTO.List = new List<ConfiguracionTraspasoDTO>();
			oRespListConfiguracionTraspasoDTO.User = oReqFilterConfiguracionTraspasoDTO.User;
			oRespListConfiguracionTraspasoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterConfiguracionTraspasoDTO.User))
            {
                oRespListConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionTraspaso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterConfiguracionTraspasoDTO.Paging == null)
            {
                oRespListConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListConfiguracionTraspasoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterConfiguracionTraspasoDTO.Paging.All && oReqFilterConfiguracionTraspasoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterConfiguracionTraspasoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ConfiguracionTraspasoDTO> ConfiguracionTraspasoDTOList = new List<ConfiguracionTraspasoDTO>();

                    switch (oReqFilterConfiguracionTraspasoDTO.FilterCase)
                    {
                        default:
                            {
                                //ConfiguracionTraspasoDTOList = oConfiguracionTraspasoData.Listar(oReqFilterConfiguracionTraspasoDTO.Item, oReqFilterConfiguracionTraspasoDTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListConfiguracionTraspasoDTO.List = ConfiguracionTraspasoDTOList;
                    oRespListConfiguracionTraspasoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListConfiguracionTraspasoDTO.Success = false;
                    oRespListConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListConfiguracionTraspasoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ConfiguracionTraspasoGetItem
		//Objetivo: Retorna un registro de tipo ConfiguracionTraspasoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemConfiguracionTraspasoDTO ConfiguracionTraspasoGetItem(ReqFilterConfiguracionTraspasoDTO oReqFilterConfiguracionTraspasoDTO)
		{
			RespItemConfiguracionTraspasoDTO oRespItemConfiguracionTraspasoDTO = new RespItemConfiguracionTraspasoDTO();

            oRespItemConfiguracionTraspasoDTO.Success = false;
            oRespItemConfiguracionTraspasoDTO.Item = null;
            oRespItemConfiguracionTraspasoDTO.User = oReqFilterConfiguracionTraspasoDTO.User;
            oRespItemConfiguracionTraspasoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterConfiguracionTraspasoDTO.User))
            {
                oRespItemConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionTraspaso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemConfiguracionTraspasoDTO.MessageList.Count == 0)
            {
                ConfiguracionTraspasoDTO oConfiguracionTraspasoDTO = null;
                try
                {
                    switch (oReqFilterConfiguracionTraspasoDTO.FilterCase)
                    {
                        case filterCaseConfiguracionTraspaso.uspBuscar_Configuracion_Traspaso_Duplicado:
                            {
                                oConfiguracionTraspasoDTO = new ConfiguracionTraspasoDTO();
                                oConfiguracionTraspasoDTO = oConfiguracionTraspasoData.uspBuscar_Configuracion_Traspaso_Duplicado(oReqFilterConfiguracionTraspasoDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracionTraspaso.uspBuscar_Configuracion_Congelamiento:
                            {
                                oConfiguracionTraspasoDTO = new ConfiguracionTraspasoDTO();
                                oConfiguracionTraspasoDTO = oConfiguracionTraspasoData.uspBuscar_Configuracion_Congelamiento(oReqFilterConfiguracionTraspasoDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracionTraspaso.porCodigo:
                            {
                                oConfiguracionTraspasoDTO = new ConfiguracionTraspasoDTO();
                                oConfiguracionTraspasoDTO = oConfiguracionTraspasoData.BuscarPorCodigoConfiguracionTraspaso(oReqFilterConfiguracionTraspasoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oConfiguracionTraspasoDTO = new ConfiguracionTraspasoDTO();
                            }
                            break;
                    }

                    oRespItemConfiguracionTraspasoDTO.Item = new ConfiguracionTraspasoDTO();
                    oRespItemConfiguracionTraspasoDTO.Item = oConfiguracionTraspasoDTO;
                    oRespItemConfiguracionTraspasoDTO.Success = true;
                    oRespItemConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemConfiguracionTraspasoDTO.Success = false;
                    oRespItemConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemConfiguracionTraspasoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ConfiguracionTraspasoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespConfiguracionTraspasoDTO ExecuteTransac(ReqConfiguracionTraspasoDTO oReqConfiguracionTraspasoDTO)
		{
			RespConfiguracionTraspasoDTO oRespConfiguracionTraspasoDTO = new RespConfiguracionTraspasoDTO();

            oRespConfiguracionTraspasoDTO.MessageList = new List<Mensaje>();
            oRespConfiguracionTraspasoDTO.User = oReqConfiguracionTraspasoDTO.User;
            
            if (String.IsNullOrEmpty(oReqConfiguracionTraspasoDTO.User))
            {
                oRespConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionTraspaso no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespConfiguracionTraspasoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (ConfiguracionTraspasoDTO item in oReqConfiguracionTraspasoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.uspActualizarConfiguracion_Traspaso_Duplicado:
                                    oConfiguracionTraspasoData.uspActualizarConfiguracion_Traspaso_Duplicado(item);
                                    break;
                                case Operation.Update:
                                    oConfiguracionTraspasoData.Actualizar(item);
                                    break;
                                case Operation.uspActualizarConfiguracion_Congelamiento:
                                    oConfiguracionTraspasoData.uspActualizarConfiguracion_Congelamiento(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespConfiguracionTraspasoDTO.Success = true;
                        oRespConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespConfiguracionTraspasoDTO.Success = false;
                        oRespConfiguracionTraspasoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespConfiguracionTraspasoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
