
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
	//Archivo     : ConfiguracionGastoAperturaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 17/03/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ConfiguracionGastoAperturaLogic: IDisposable
	{
		ConfiguracionGastoAperturaData oConfiguracionGastoAperturaData = null;
		public ConfiguracionGastoAperturaLogic()
		{
			oConfiguracionGastoAperturaData = new ConfiguracionGastoAperturaData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ConfiguracionGastoAperturaGetList
		//Objetivo: Retorna una colección de registros de tipo ConfiguracionGastoAperturaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListConfiguracionGastoAperturaDTO ConfiguracionGastoAperturaGetList(ReqFilterConfiguracionGastoAperturaDTO oReqFilterConfiguracionGastoAperturaDTO)
		{
		
			RespListConfiguracionGastoAperturaDTO oRespListConfiguracionGastoAperturaDTO = new RespListConfiguracionGastoAperturaDTO();
		
			oRespListConfiguracionGastoAperturaDTO.List = new List<ConfiguracionGastoAperturaDTO>();
			oRespListConfiguracionGastoAperturaDTO.User = oReqFilterConfiguracionGastoAperturaDTO.User;
			oRespListConfiguracionGastoAperturaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterConfiguracionGastoAperturaDTO.User))
            {
                oRespListConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionGastoApertura no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterConfiguracionGastoAperturaDTO.Paging == null)
            {
                oRespListConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListConfiguracionGastoAperturaDTO.MessageList.Count == 0)
            {
                
                try
                {
                   // uint recordCount = 0;
                    
                    if (!oReqFilterConfiguracionGastoAperturaDTO.Paging.All && oReqFilterConfiguracionGastoAperturaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterConfiguracionGastoAperturaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ConfiguracionGastoAperturaDTO> ConfiguracionGastoAperturaDTOList = new List<ConfiguracionGastoAperturaDTO>();

                  
                    oRespListConfiguracionGastoAperturaDTO.List = ConfiguracionGastoAperturaDTOList;
                    oRespListConfiguracionGastoAperturaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListConfiguracionGastoAperturaDTO.Success = false;
                    oRespListConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListConfiguracionGastoAperturaDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ConfiguracionGastoAperturaGetItem
		//Objetivo: Retorna un registro de tipo ConfiguracionGastoAperturaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemConfiguracionGastoAperturaDTO ConfiguracionGastoAperturaGetItem(ReqFilterConfiguracionGastoAperturaDTO oReqFilterConfiguracionGastoAperturaDTO)
		{
			RespItemConfiguracionGastoAperturaDTO oRespItemConfiguracionGastoAperturaDTO = new RespItemConfiguracionGastoAperturaDTO();

            oRespItemConfiguracionGastoAperturaDTO.Success = false;
            oRespItemConfiguracionGastoAperturaDTO.Item = null;
            oRespItemConfiguracionGastoAperturaDTO.User = oReqFilterConfiguracionGastoAperturaDTO.User;
            oRespItemConfiguracionGastoAperturaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterConfiguracionGastoAperturaDTO.User))
            {
                oRespItemConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionGastoApertura no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemConfiguracionGastoAperturaDTO.MessageList.Count == 0)
            {
                ConfiguracionGastoAperturaDTO oConfiguracionGastoAperturaDTO = null;
                try
                {
                    switch (oReqFilterConfiguracionGastoAperturaDTO.FilterCase)
                    {
                       
                        case filterCaseConfiguracionGastoApertura.uspBuscarPorCodigo:
                            {
                                oConfiguracionGastoAperturaDTO = new ConfiguracionGastoAperturaDTO();
                                oConfiguracionGastoAperturaDTO = oConfiguracionGastoAperturaData.BuscarPorCodigoConfiguracionCaja(oReqFilterConfiguracionGastoAperturaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oConfiguracionGastoAperturaDTO = new ConfiguracionGastoAperturaDTO();
                            }
                            break;
                    }

                    oRespItemConfiguracionGastoAperturaDTO.Item = new ConfiguracionGastoAperturaDTO();
                    oRespItemConfiguracionGastoAperturaDTO.Item = oConfiguracionGastoAperturaDTO;
                    oRespItemConfiguracionGastoAperturaDTO.Success = true;
                    oRespItemConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemConfiguracionGastoAperturaDTO.Success = false;
                    oRespItemConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemConfiguracionGastoAperturaDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ConfiguracionGastoAperturaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespConfiguracionGastoAperturaDTO ExecuteTransac(ReqConfiguracionGastoAperturaDTO oReqConfiguracionGastoAperturaDTO)
		{
			RespConfiguracionGastoAperturaDTO oRespConfiguracionGastoAperturaDTO = new RespConfiguracionGastoAperturaDTO();

            oRespConfiguracionGastoAperturaDTO.MessageList = new List<Mensaje>();
            oRespConfiguracionGastoAperturaDTO.User = oReqConfiguracionGastoAperturaDTO.User;
            
            if (String.IsNullOrEmpty(oReqConfiguracionGastoAperturaDTO.User))
            {
                oRespConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionGastoApertura no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespConfiguracionGastoAperturaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (ConfiguracionGastoAperturaDTO item in oReqConfiguracionGastoAperturaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oConfiguracionGastoAperturaData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oConfiguracionGastoAperturaData.Actualizar(item);
                                    break;
                                case Operation.UpdateConfiguracionCaja:
                                    oConfiguracionGastoAperturaData.UpdateConfiguracionCaja(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespConfiguracionGastoAperturaDTO.Success = true;
                        oRespConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespConfiguracionGastoAperturaDTO.Success = false;
                        oRespConfiguracionGastoAperturaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespConfiguracionGastoAperturaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
