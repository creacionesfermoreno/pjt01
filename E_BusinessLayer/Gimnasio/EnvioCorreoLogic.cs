
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
	//Archivo     : EnvioCorreoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 27/07/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class EnvioCorreoLogic: IDisposable
	{
		EnvioCorreoData oEnvioCorreoData = null;
		public EnvioCorreoLogic()
		{
			oEnvioCorreoData = new EnvioCorreoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	EnvioCorreoGetList
		//Objetivo: Retorna una colección de registros de tipo EnvioCorreoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListEnvioCorreoDTO EnvioCorreoGetList(ReqFilterEnvioCorreoDTO oReqFilterEnvioCorreoDTO)
		{
		
			RespListEnvioCorreoDTO oRespListEnvioCorreoDTO = new RespListEnvioCorreoDTO();
		
			oRespListEnvioCorreoDTO.List = new List<EnvioCorreoDTO>();
			oRespListEnvioCorreoDTO.User = oReqFilterEnvioCorreoDTO.User;
			oRespListEnvioCorreoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterEnvioCorreoDTO.User))
            {
                oRespListEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EnvioCorreo no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterEnvioCorreoDTO.Paging == null)
            {
                oRespListEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListEnvioCorreoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterEnvioCorreoDTO.Paging.All && oReqFilterEnvioCorreoDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterEnvioCorreoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<EnvioCorreoDTO> EnvioCorreoDTOList = new List<EnvioCorreoDTO>();


                    oRespListEnvioCorreoDTO.List = EnvioCorreoDTOList;
                    oRespListEnvioCorreoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListEnvioCorreoDTO.Success = false;
                    oRespListEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListEnvioCorreoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	EnvioCorreoGetItem
		//Objetivo: Retorna un registro de tipo EnvioCorreoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemEnvioCorreoDTO EnvioCorreoGetItem(ReqFilterEnvioCorreoDTO oReqFilterEnvioCorreoDTO)
		{
			RespItemEnvioCorreoDTO oRespItemEnvioCorreoDTO = new RespItemEnvioCorreoDTO();

            oRespItemEnvioCorreoDTO.Success = false;
            oRespItemEnvioCorreoDTO.Item = null;
            oRespItemEnvioCorreoDTO.User = oReqFilterEnvioCorreoDTO.User;
            oRespItemEnvioCorreoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterEnvioCorreoDTO.User))
            {
                oRespItemEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EnvioCorreo no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemEnvioCorreoDTO.MessageList.Count == 0)
            {
                EnvioCorreoDTO oEnvioCorreoDTO = null;
                try
                {
                    switch (oReqFilterEnvioCorreoDTO.FilterCase)
                    {
                       
                        case filterCaseEnvioCorreo.porCodigo:
                            {
                                oEnvioCorreoDTO = new EnvioCorreoDTO();
                                oEnvioCorreoDTO = oEnvioCorreoData.BuscarPorCodigoEnvioCorreo(oReqFilterEnvioCorreoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oEnvioCorreoDTO = new EnvioCorreoDTO();
                            }
                            break;
                    }

                    oRespItemEnvioCorreoDTO.Item = new EnvioCorreoDTO();
                    oRespItemEnvioCorreoDTO.Item = oEnvioCorreoDTO;
                    oRespItemEnvioCorreoDTO.Success = true;
                    oRespItemEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemEnvioCorreoDTO.Success = false;
                    oRespItemEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemEnvioCorreoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo EnvioCorreoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespEnvioCorreoDTO ExecuteTransac(ReqEnvioCorreoDTO oReqEnvioCorreoDTO)
		{
			RespEnvioCorreoDTO oRespEnvioCorreoDTO = new RespEnvioCorreoDTO();

            oRespEnvioCorreoDTO.MessageList = new List<Mensaje>();
            oRespEnvioCorreoDTO.User = oReqEnvioCorreoDTO.User;
            
            if (String.IsNullOrEmpty(oReqEnvioCorreoDTO.User))
            {
                oRespEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EnvioCorreo no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespEnvioCorreoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (EnvioCorreoDTO item in oReqEnvioCorreoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oEnvioCorreoData.Registrar(item);
                                    break;
                             
                            }
                        }
                        tx.Complete();
                        oRespEnvioCorreoDTO.Success = true;
                        oRespEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespEnvioCorreoDTO.Success = false;
                        oRespEnvioCorreoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespEnvioCorreoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
