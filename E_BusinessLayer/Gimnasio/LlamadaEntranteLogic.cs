
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
	//Archivo     : LlamadaEntranteLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 20/04/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class LlamadaEntranteLogic: IDisposable
	{
		LlamadaEntranteData oLlamadaEntranteData = null;
		public LlamadaEntranteLogic()
		{
			oLlamadaEntranteData = new LlamadaEntranteData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	LlamadaEntranteGetList
		//Objetivo: Retorna una colección de registros de tipo LlamadaEntranteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListLlamadaEntranteDTO LlamadaEntranteGetList(ReqFilterLlamadaEntranteDTO oReqFilterLlamadaEntranteDTO)
		{
		
			RespListLlamadaEntranteDTO oRespListLlamadaEntranteDTO = new RespListLlamadaEntranteDTO();
		
			oRespListLlamadaEntranteDTO.List = new List<LlamadaEntranteDTO>();
			oRespListLlamadaEntranteDTO.User = oReqFilterLlamadaEntranteDTO.User;
			oRespListLlamadaEntranteDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterLlamadaEntranteDTO.User))
            {
                oRespListLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de LlamadaEntrante no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterLlamadaEntranteDTO.Paging == null)
            {
                oRespListLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListLlamadaEntranteDTO.MessageList.Count == 0)
            {
                
                try
                {
                 
                    List<LlamadaEntranteDTO> LlamadaEntranteDTOList = new List<LlamadaEntranteDTO>();

                    switch (oReqFilterLlamadaEntranteDTO.FilterCase)
                    {
                        case filterCaseLlamadaEntrante.uspListarTablaLlamadaEntrante_Paginacion:
                            {
                                if (!oReqFilterLlamadaEntranteDTO.Paging.All && oReqFilterLlamadaEntranteDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterLlamadaEntranteDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosLlamadaE_NumeroRegistros"]);
                                }

                                LlamadaEntranteDTOList = oLlamadaEntranteData.uspListarTablaLlamadaEntrante_Paginacion(oReqFilterLlamadaEntranteDTO.Item, oReqFilterLlamadaEntranteDTO.Paging);
                            }
                            break;
                        case filterCaseLlamadaEntrante.uspListarTablaWeb_Paginacion:
                            {
                                if (!oReqFilterLlamadaEntranteDTO.Paging.All && oReqFilterLlamadaEntranteDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterLlamadaEntranteDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosLlamadaE_NumeroRegistros"]);
                                }

                                LlamadaEntranteDTOList = oLlamadaEntranteData.uspListarTablaWeb_Paginacion(oReqFilterLlamadaEntranteDTO.Item, oReqFilterLlamadaEntranteDTO.Paging);
                            }
                            break;
                    }

                    oRespListLlamadaEntranteDTO.List = LlamadaEntranteDTOList;
                    oRespListLlamadaEntranteDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListLlamadaEntranteDTO.Success = false;
                    oRespListLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListLlamadaEntranteDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	LlamadaEntranteGetItem
		//Objetivo: Retorna un registro de tipo LlamadaEntranteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemLlamadaEntranteDTO LlamadaEntranteGetItem(ReqFilterLlamadaEntranteDTO oReqFilterLlamadaEntranteDTO)
		{
			RespItemLlamadaEntranteDTO oRespItemLlamadaEntranteDTO = new RespItemLlamadaEntranteDTO();

            oRespItemLlamadaEntranteDTO.Success = false;
            oRespItemLlamadaEntranteDTO.Item = null;
            oRespItemLlamadaEntranteDTO.User = oReqFilterLlamadaEntranteDTO.User;
            oRespItemLlamadaEntranteDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterLlamadaEntranteDTO.User))
            {
                oRespItemLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de LlamadaEntrante no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemLlamadaEntranteDTO.MessageList.Count == 0)
            {
                LlamadaEntranteDTO oLlamadaEntranteDTO = null;
                try
                {
                    switch (oReqFilterLlamadaEntranteDTO.FilterCase)
                    {
                     
                        case filterCaseLlamadaEntrante.uspListarTablaLlamadaEntrante_NumeroRegistros:
                            {
                                oLlamadaEntranteDTO = new LlamadaEntranteDTO();
                                oLlamadaEntranteDTO = oLlamadaEntranteData.uspListarTablaLlamadaEntrante_NumeroRegistros(oReqFilterLlamadaEntranteDTO.Item);
                            }
                            break;
                        case filterCaseLlamadaEntrante.uspListarTablaWeb_NumeroRegistros:
                            {
                                oLlamadaEntranteDTO = new LlamadaEntranteDTO();
                                oLlamadaEntranteDTO = oLlamadaEntranteData.uspListarTablaWeb_NumeroRegistros(oReqFilterLlamadaEntranteDTO.Item);
                            }
                            break;

                        case filterCaseLlamadaEntrante.uspBuscarPorCodigo:
                            {
                                oLlamadaEntranteDTO = new LlamadaEntranteDTO();
                                oLlamadaEntranteDTO = oLlamadaEntranteData.BuscarPorCodigoLlamadaEntrante(oReqFilterLlamadaEntranteDTO.Item);
                            }
                            break;
                        case filterCaseLlamadaEntrante.uspBuscarPropectoWebPorCodigo:
                            {
                                oLlamadaEntranteDTO = new LlamadaEntranteDTO();
                                oLlamadaEntranteDTO = oLlamadaEntranteData.uspBuscarPropectoWebPorCodigo(oReqFilterLlamadaEntranteDTO.Item);
                            }
                            break;
                        default:
                            {
                                oLlamadaEntranteDTO = new LlamadaEntranteDTO();
                            }
                            break;
                    }

                    oRespItemLlamadaEntranteDTO.Item = new LlamadaEntranteDTO();
                    oRespItemLlamadaEntranteDTO.Item = oLlamadaEntranteDTO;
                    oRespItemLlamadaEntranteDTO.Success = true;
                    oRespItemLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemLlamadaEntranteDTO.Success = false;
                    oRespItemLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemLlamadaEntranteDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo LlamadaEntranteDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespLlamadaEntranteDTO ExecuteTransac(ReqLlamadaEntranteDTO oReqLlamadaEntranteDTO)
		{
			RespLlamadaEntranteDTO oRespLlamadaEntranteDTO = new RespLlamadaEntranteDTO();

            oRespLlamadaEntranteDTO.MessageList = new List<Mensaje>();
            oRespLlamadaEntranteDTO.User = oReqLlamadaEntranteDTO.User;
            
            if (String.IsNullOrEmpty(oReqLlamadaEntranteDTO.User))
            {
                oRespLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de LlamadaEntrante no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespLlamadaEntranteDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (LlamadaEntranteDTO item in oReqLlamadaEntranteDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    Codigo = 2;
                                    oLlamadaEntranteData.Registrar(item);
                                    break;
                                case Operation.uspRegistrarProspectoWeb:
                                    Codigo = 999999999;
                                    oLlamadaEntranteData.uspRegistrarProspectoWeb(item);
                                    break;
                                case Operation.UpdateLlamadaEASocio:

                                    Codigo = oLlamadaEntranteData.uspActualizarLlamadaEASocio(item);

                                    break;
                                case Operation.uspActualizarProspectoWebASocio:

                                    Codigo = oLlamadaEntranteData.uspActualizarProspectoWebASocio(item);

                                    break;
                                case Operation.UpdateLlamadaEAInvitado:

                                    Codigo = oLlamadaEntranteData.uspActualizarLlamadaEAInvitado(item);

                                    break;

                                case Operation.Delete:
                                    Codigo = 999999999;
                                    oLlamadaEntranteData.Eliminar(item);
                                    break;
                                case Operation.uspEliminarProspectoWeb:
                                    Codigo = 999999999;
                                    oLlamadaEntranteData.uspEliminarProspectoWeb(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespLlamadaEntranteDTO.Success = true;
                        oRespLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespLlamadaEntranteDTO.Success = false;
                        oRespLlamadaEntranteDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespLlamadaEntranteDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
