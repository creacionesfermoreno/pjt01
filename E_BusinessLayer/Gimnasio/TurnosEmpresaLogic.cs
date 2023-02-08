
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
	//Archivo     : TurnosEmpresaLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 29/04/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class TurnosEmpresaLogic: IDisposable
	{
		TurnosEmpresaData oTurnosEmpresaData = null;
		public TurnosEmpresaLogic()
		{
			oTurnosEmpresaData = new TurnosEmpresaData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TurnosEmpresaGetList
		//Objetivo: Retorna una colección de registros de tipo TurnosEmpresaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListTurnosEmpresaDTO TurnosEmpresaGetList(ReqFilterTurnosEmpresaDTO oReqFilterTurnosEmpresaDTO)
		{
		
			RespListTurnosEmpresaDTO oRespListTurnosEmpresaDTO = new RespListTurnosEmpresaDTO();
		
			oRespListTurnosEmpresaDTO.List = new List<TurnosEmpresaDTO>();
			oRespListTurnosEmpresaDTO.User = oReqFilterTurnosEmpresaDTO.User;
			oRespListTurnosEmpresaDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterTurnosEmpresaDTO.User))
            {
                oRespListTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TurnosEmpresa no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterTurnosEmpresaDTO.Paging == null)
            {
                oRespListTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListTurnosEmpresaDTO.MessageList.Count == 0)
            {
                
                try
                {
                    //uint recordCount = 0;
                    
                    if (!oReqFilterTurnosEmpresaDTO.Paging.All && oReqFilterTurnosEmpresaDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterTurnosEmpresaDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<TurnosEmpresaDTO> TurnosEmpresaDTOList = new List<TurnosEmpresaDTO>();
                    switch (oReqFilterTurnosEmpresaDTO.FilterCase)
                    {
                        default:
                            {
                                TurnosEmpresaDTOList = oTurnosEmpresaData.Listar(oReqFilterTurnosEmpresaDTO.Item);
                            }
                            break;
                    }

                    oRespListTurnosEmpresaDTO.List = TurnosEmpresaDTOList;
                    oRespListTurnosEmpresaDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListTurnosEmpresaDTO.Success = false;
                    oRespListTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListTurnosEmpresaDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	TurnosEmpresaGetItem
		//Objetivo: Retorna un registro de tipo TurnosEmpresaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemTurnosEmpresaDTO TurnosEmpresaGetItem(ReqFilterTurnosEmpresaDTO oReqFilterTurnosEmpresaDTO)
		{
			RespItemTurnosEmpresaDTO oRespItemTurnosEmpresaDTO = new RespItemTurnosEmpresaDTO();

            oRespItemTurnosEmpresaDTO.Success = false;
            oRespItemTurnosEmpresaDTO.Item = null;
            oRespItemTurnosEmpresaDTO.User = oReqFilterTurnosEmpresaDTO.User;
            oRespItemTurnosEmpresaDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterTurnosEmpresaDTO.User))
            {
                oRespItemTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TurnosEmpresa no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemTurnosEmpresaDTO.MessageList.Count == 0)
            {
                TurnosEmpresaDTO oTurnosEmpresaDTO = null;
                try
                {
                    switch (oReqFilterTurnosEmpresaDTO.FilterCase)
                    {
                       
                        case filterCaseTurnosEmpresa.porCodigo:
                            {
                                oTurnosEmpresaDTO = new TurnosEmpresaDTO();
                                oTurnosEmpresaDTO = oTurnosEmpresaData.BuscarPorCodigoTurnosEmpresa(oReqFilterTurnosEmpresaDTO.Item);
                            }
                            break;
                        default:
                            {
                                oTurnosEmpresaDTO = new TurnosEmpresaDTO();
                            }
                            break;
                    }

                    oRespItemTurnosEmpresaDTO.Item = new TurnosEmpresaDTO();
                    oRespItemTurnosEmpresaDTO.Item = oTurnosEmpresaDTO;
                    oRespItemTurnosEmpresaDTO.Success = true;
                    oRespItemTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemTurnosEmpresaDTO.Success = false;
                    oRespItemTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemTurnosEmpresaDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo TurnosEmpresaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespTurnosEmpresaDTO ExecuteTransac(ReqTurnosEmpresaDTO oReqTurnosEmpresaDTO)
		{
			RespTurnosEmpresaDTO oRespTurnosEmpresaDTO = new RespTurnosEmpresaDTO();

            oRespTurnosEmpresaDTO.MessageList = new List<Mensaje>();
            oRespTurnosEmpresaDTO.User = oReqTurnosEmpresaDTO.User;
            
            if (String.IsNullOrEmpty(oReqTurnosEmpresaDTO.User))
            {
                oRespTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de TurnosEmpresa no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespTurnosEmpresaDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (TurnosEmpresaDTO item in oReqTurnosEmpresaDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oTurnosEmpresaData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oTurnosEmpresaData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oTurnosEmpresaData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespTurnosEmpresaDTO.Success = true;
                        oRespTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespTurnosEmpresaDTO.Success = false;
                        oRespTurnosEmpresaDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespTurnosEmpresaDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
