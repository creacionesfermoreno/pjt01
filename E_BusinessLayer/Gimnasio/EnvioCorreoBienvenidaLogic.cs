
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
	//Archivo     : EnvioCorreoBienvenidaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/12/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class EnvioCorreoBienvenidaLogic: IDisposable
	{
		EnvioCorreoBienvenidaData oEnvioCorreoBienvenidaData = null;
		public EnvioCorreoBienvenidaLogic()
		{
			oEnvioCorreoBienvenidaData = new EnvioCorreoBienvenidaData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	EnvioCorreoBienvenidaGetList
		//Objetivo: Retorna una colección de registros de tipo EnvioCorreoBienvenidaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListEnvioCorreoBienvenidaDTO EnvioCorreoBienvenidaGetList(ReqFilterEnvioCorreoBienvenidaDTO oReqFilterEnvioCorreoBienvenidaDTO)
		{
		
			RespListEnvioCorreoBienvenidaDTO oRespListEnvioCorreoBienvenidaDTO = new RespListEnvioCorreoBienvenidaDTO();
		
			oRespListEnvioCorreoBienvenidaDTO.List = new List<EnvioCorreoBienvenidaDTO>();
			oRespListEnvioCorreoBienvenidaDTO.User = oReqFilterEnvioCorreoBienvenidaDTO.User;
			oRespListEnvioCorreoBienvenidaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterEnvioCorreoBienvenidaDTO.User))
            {
                oRespListEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EnvioCorreoBienvenida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterEnvioCorreoBienvenidaDTO.Paging == null)
            {
                oRespListEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListEnvioCorreoBienvenidaDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterEnvioCorreoBienvenidaDTO.Paging.All && oReqFilterEnvioCorreoBienvenidaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterEnvioCorreoBienvenidaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<EnvioCorreoBienvenidaDTO> EnvioCorreoBienvenidaDTOList = new List<EnvioCorreoBienvenidaDTO>();

                    switch (oReqFilterEnvioCorreoBienvenidaDTO.FilterCase)
                    {
                        default:
                            {
                                
                            }
                            break;
                    }

                    oRespListEnvioCorreoBienvenidaDTO.List = EnvioCorreoBienvenidaDTOList;
                    oRespListEnvioCorreoBienvenidaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListEnvioCorreoBienvenidaDTO.Success = false;
                    oRespListEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListEnvioCorreoBienvenidaDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	EnvioCorreoBienvenidaGetItem
		//Objetivo: Retorna un registro de tipo EnvioCorreoBienvenidaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemEnvioCorreoBienvenidaDTO EnvioCorreoBienvenidaGetItem(ReqFilterEnvioCorreoBienvenidaDTO oReqFilterEnvioCorreoBienvenidaDTO)
		{
			RespItemEnvioCorreoBienvenidaDTO oRespItemEnvioCorreoBienvenidaDTO = new RespItemEnvioCorreoBienvenidaDTO();

            oRespItemEnvioCorreoBienvenidaDTO.Success = false;
            oRespItemEnvioCorreoBienvenidaDTO.Item = null;
            oRespItemEnvioCorreoBienvenidaDTO.User = oReqFilterEnvioCorreoBienvenidaDTO.User;
            oRespItemEnvioCorreoBienvenidaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterEnvioCorreoBienvenidaDTO.User))
            {
                oRespItemEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EnvioCorreoBienvenida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemEnvioCorreoBienvenidaDTO.MessageList.Count == 0)
            {
                EnvioCorreoBienvenidaDTO oEnvioCorreoBienvenidaDTO = null;
                try
                {
                    switch (oReqFilterEnvioCorreoBienvenidaDTO.FilterCase)
                    {
                        case filterCaseEnvioCorreoBienvenida.uspBuscarEnvioCorreoBienvenidaPorCodigo:
                            {
                                oEnvioCorreoBienvenidaDTO = new EnvioCorreoBienvenidaDTO();
                                oEnvioCorreoBienvenidaDTO = oEnvioCorreoBienvenidaData.BuscarPorCodigoEnvioCorreoBienvenida(oReqFilterEnvioCorreoBienvenidaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oEnvioCorreoBienvenidaDTO = new EnvioCorreoBienvenidaDTO();
                            }
                            break;
                    }

                    oRespItemEnvioCorreoBienvenidaDTO.Item = new EnvioCorreoBienvenidaDTO();
                    oRespItemEnvioCorreoBienvenidaDTO.Item = oEnvioCorreoBienvenidaDTO;
                    oRespItemEnvioCorreoBienvenidaDTO.Success = true;
                    oRespItemEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemEnvioCorreoBienvenidaDTO.Success = false;
                    oRespItemEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemEnvioCorreoBienvenidaDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo EnvioCorreoBienvenidaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespEnvioCorreoBienvenidaDTO ExecuteTransac(ReqEnvioCorreoBienvenidaDTO oReqEnvioCorreoBienvenidaDTO)
		{
			RespEnvioCorreoBienvenidaDTO oRespEnvioCorreoBienvenidaDTO = new RespEnvioCorreoBienvenidaDTO();

            oRespEnvioCorreoBienvenidaDTO.MessageList = new List<Mensaje>();
            oRespEnvioCorreoBienvenidaDTO.User = oReqEnvioCorreoBienvenidaDTO.User;
            
            if (String.IsNullOrEmpty(oReqEnvioCorreoBienvenidaDTO.User))
            {
                oRespEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EnvioCorreoBienvenida no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespEnvioCorreoBienvenidaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (EnvioCorreoBienvenidaDTO item in oReqEnvioCorreoBienvenidaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oEnvioCorreoBienvenidaData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oEnvioCorreoBienvenidaData.Actualizar(item);
                                    break;
                              
                            }
                        }
                        tx.Complete();
                        oRespEnvioCorreoBienvenidaDTO.Success = true;
                        oRespEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespEnvioCorreoBienvenidaDTO.Success = false;
                        oRespEnvioCorreoBienvenidaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespEnvioCorreoBienvenidaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
