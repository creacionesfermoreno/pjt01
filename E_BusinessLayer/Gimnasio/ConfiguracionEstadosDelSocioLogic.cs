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
	//Archivo     : ConfiguracionEstadosDelSocioLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/04/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ConfiguracionEstadosDelSocioLogic: IDisposable
	{
		ConfiguracionEstadosDelSocioData oConfiguracionEstadosDelSocioData = null;
		public ConfiguracionEstadosDelSocioLogic()
		{
			oConfiguracionEstadosDelSocioData = new ConfiguracionEstadosDelSocioData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ConfiguracionEstadosDelSocioGetList
		//Objetivo: Retorna una colección de registros de tipo ConfiguracionEstadosDelSocioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListConfiguracionEstadosDelSocioDTO ConfiguracionEstadosDelSocioGetList(ReqFilterConfiguracionEstadosDelSocioDTO oReqFilterConfiguracionEstadosDelSocioDTO)
		{
		
			RespListConfiguracionEstadosDelSocioDTO oRespListConfiguracionEstadosDelSocioDTO = new RespListConfiguracionEstadosDelSocioDTO();
		
			oRespListConfiguracionEstadosDelSocioDTO.List = new List<ConfiguracionEstadosDelSocioDTO>();
			oRespListConfiguracionEstadosDelSocioDTO.User = oReqFilterConfiguracionEstadosDelSocioDTO.User;
			oRespListConfiguracionEstadosDelSocioDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterConfiguracionEstadosDelSocioDTO.User))
            {
                oRespListConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionEstadosDelSocio no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterConfiguracionEstadosDelSocioDTO.Paging == null)
            {
                oRespListConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListConfiguracionEstadosDelSocioDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterConfiguracionEstadosDelSocioDTO.Paging.All && oReqFilterConfiguracionEstadosDelSocioDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterConfiguracionEstadosDelSocioDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ConfiguracionEstadosDelSocioDTO> ConfiguracionEstadosDelSocioDTOList = new List<ConfiguracionEstadosDelSocioDTO>();

                    switch (oReqFilterConfiguracionEstadosDelSocioDTO.FilterCase)
                    {
                        default:
                            {
                                ConfiguracionEstadosDelSocioDTOList = oConfiguracionEstadosDelSocioData.Listar(oReqFilterConfiguracionEstadosDelSocioDTO.Item);
                            }
                            break;
                    }

                    oRespListConfiguracionEstadosDelSocioDTO.List = ConfiguracionEstadosDelSocioDTOList;
                    oRespListConfiguracionEstadosDelSocioDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListConfiguracionEstadosDelSocioDTO.Success = false;
                    oRespListConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListConfiguracionEstadosDelSocioDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ConfiguracionEstadosDelSocioGetItem
		//Objetivo: Retorna un registro de tipo ConfiguracionEstadosDelSocioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemConfiguracionEstadosDelSocioDTO ConfiguracionEstadosDelSocioGetItem(ReqFilterConfiguracionEstadosDelSocioDTO oReqFilterConfiguracionEstadosDelSocioDTO)
		{
			RespItemConfiguracionEstadosDelSocioDTO oRespItemConfiguracionEstadosDelSocioDTO = new RespItemConfiguracionEstadosDelSocioDTO();

            oRespItemConfiguracionEstadosDelSocioDTO.Success = false;
            oRespItemConfiguracionEstadosDelSocioDTO.Item = null;
            oRespItemConfiguracionEstadosDelSocioDTO.User = oReqFilterConfiguracionEstadosDelSocioDTO.User;
            oRespItemConfiguracionEstadosDelSocioDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterConfiguracionEstadosDelSocioDTO.User))
            {
                oRespItemConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionEstadosDelSocio no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemConfiguracionEstadosDelSocioDTO.MessageList.Count == 0)
            {
                ConfiguracionEstadosDelSocioDTO oConfiguracionEstadosDelSocioDTO = null;
                try
                {
                    switch (oReqFilterConfiguracionEstadosDelSocioDTO.FilterCase)
                    {
                       
                        case filterCaseConfiguracionEstadosDelSocio.porCodigo:
                            {
                                oConfiguracionEstadosDelSocioDTO = new ConfiguracionEstadosDelSocioDTO();
                                oConfiguracionEstadosDelSocioDTO = oConfiguracionEstadosDelSocioData.BuscarPorCodigoConfiguracionEstadosDelSocio(oReqFilterConfiguracionEstadosDelSocioDTO.Item);
                            }
                            break;
                        default:
                            {
                                oConfiguracionEstadosDelSocioDTO = new ConfiguracionEstadosDelSocioDTO();
                            }
                            break;
                    }

                    oRespItemConfiguracionEstadosDelSocioDTO.Item = new ConfiguracionEstadosDelSocioDTO();
                    oRespItemConfiguracionEstadosDelSocioDTO.Item = oConfiguracionEstadosDelSocioDTO;
                    oRespItemConfiguracionEstadosDelSocioDTO.Success = true;
                    oRespItemConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemConfiguracionEstadosDelSocioDTO.Success = false;
                    oRespItemConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemConfiguracionEstadosDelSocioDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ConfiguracionEstadosDelSocioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespConfiguracionEstadosDelSocioDTO ExecuteTransac(ReqConfiguracionEstadosDelSocioDTO oReqConfiguracionEstadosDelSocioDTO)
		{
			RespConfiguracionEstadosDelSocioDTO oRespConfiguracionEstadosDelSocioDTO = new RespConfiguracionEstadosDelSocioDTO();

            oRespConfiguracionEstadosDelSocioDTO.MessageList = new List<Mensaje>();
            oRespConfiguracionEstadosDelSocioDTO.User = oReqConfiguracionEstadosDelSocioDTO.User;
            
            if (String.IsNullOrEmpty(oReqConfiguracionEstadosDelSocioDTO.User))
            {
                oRespConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de ConfiguracionEstadosDelSocio no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespConfiguracionEstadosDelSocioDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (ConfiguracionEstadosDelSocioDTO item in oReqConfiguracionEstadosDelSocioDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oConfiguracionEstadosDelSocioData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oConfiguracionEstadosDelSocioData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oConfiguracionEstadosDelSocioData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespConfiguracionEstadosDelSocioDTO.Success = true;
                        oRespConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespConfiguracionEstadosDelSocioDTO.Success = false;
                        oRespConfiguracionEstadosDelSocioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespConfiguracionEstadosDelSocioDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
