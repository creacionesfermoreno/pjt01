
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
	//Archivo     : SalaHorarioLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 22/03/2018
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class SalaHorarioLogic: IDisposable
	{
		SalaHorarioData oSalaHorarioData = null;
		public SalaHorarioLogic()
		{
			oSalaHorarioData = new SalaHorarioData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SalaHorarioGetList
		//Objetivo: Retorna una colección de registros de tipo SalaHorarioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListSalaHorarioDTO SalaHorarioGetList(ReqFilterSalaHorarioDTO oReqFilterSalaHorarioDTO)
		{
		
			RespListSalaHorarioDTO oRespListSalaHorarioDTO = new RespListSalaHorarioDTO();
		
			oRespListSalaHorarioDTO.List = new List<SalaHorarioDTO>();
			oRespListSalaHorarioDTO.User = oReqFilterSalaHorarioDTO.User;
			oRespListSalaHorarioDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterSalaHorarioDTO.User))
            {
                oRespListSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SalaHorario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterSalaHorarioDTO.Paging == null)
            {
                oRespListSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListSalaHorarioDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterSalaHorarioDTO.Paging.All && oReqFilterSalaHorarioDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterSalaHorarioDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<SalaHorarioDTO> SalaHorarioDTOList = new List<SalaHorarioDTO>();

                    switch (oReqFilterSalaHorarioDTO.FilterCase)
                    {
                        default:
                            {
                                SalaHorarioDTOList = oSalaHorarioData.Listar(oReqFilterSalaHorarioDTO.Item);
                            }
                            break;
                    }

                    oRespListSalaHorarioDTO.List = SalaHorarioDTOList;
                    oRespListSalaHorarioDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListSalaHorarioDTO.Success = false;
                    oRespListSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListSalaHorarioDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	SalaHorarioGetItem
		//Objetivo: Retorna un registro de tipo SalaHorarioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemSalaHorarioDTO SalaHorarioGetItem(ReqFilterSalaHorarioDTO oReqFilterSalaHorarioDTO)
		{
			RespItemSalaHorarioDTO oRespItemSalaHorarioDTO = new RespItemSalaHorarioDTO();

            oRespItemSalaHorarioDTO.Success = false;
            oRespItemSalaHorarioDTO.Item = null;
            oRespItemSalaHorarioDTO.User = oReqFilterSalaHorarioDTO.User;
            oRespItemSalaHorarioDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterSalaHorarioDTO.User))
            {
                oRespItemSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SalaHorario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemSalaHorarioDTO.MessageList.Count == 0)
            {
                SalaHorarioDTO oSalaHorarioDTO = null;
                try
                {
                    switch (oReqFilterSalaHorarioDTO.FilterCase)
                    {

                        case filterCaseSalaHorario.PorCodigo:
                            {
                                oSalaHorarioDTO = new SalaHorarioDTO();
                                oSalaHorarioDTO = oSalaHorarioData.BuscarPorCodigoSalaHorario(oReqFilterSalaHorarioDTO.Item);
                            }
                            break;
                        default:
                            {
                                oSalaHorarioDTO = new SalaHorarioDTO();
                            }
                            break;
                    }

                    oRespItemSalaHorarioDTO.Item = new SalaHorarioDTO();
                    oRespItemSalaHorarioDTO.Item = oSalaHorarioDTO;
                    oRespItemSalaHorarioDTO.Success = true;
                    oRespItemSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemSalaHorarioDTO.Success = false;
                    oRespItemSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemSalaHorarioDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo SalaHorarioDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespSalaHorarioDTO ExecuteTransac(ReqSalaHorarioDTO oReqSalaHorarioDTO)
		{
			RespSalaHorarioDTO oRespSalaHorarioDTO = new RespSalaHorarioDTO();

            oRespSalaHorarioDTO.MessageList = new List<Mensaje>();
            oRespSalaHorarioDTO.User = oReqSalaHorarioDTO.User;
            
            if (String.IsNullOrEmpty(oReqSalaHorarioDTO.User))
            {
                oRespSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de SalaHorario no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespSalaHorarioDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (SalaHorarioDTO item in oReqSalaHorarioDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oSalaHorarioData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oSalaHorarioData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    oSalaHorarioData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespSalaHorarioDTO.Success = true;
                        oRespSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespSalaHorarioDTO.Success = false;
                        oRespSalaHorarioDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespSalaHorarioDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
