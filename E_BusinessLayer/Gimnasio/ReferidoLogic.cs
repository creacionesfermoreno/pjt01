
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
	//Archivo     : ReferidoLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 20/04/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ReferidoLogic: IDisposable
	{
		ReferidoData oReferidoData = null;
		public ReferidoLogic()
		{
			oReferidoData = new ReferidoData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ReferidoGetList
		//Objetivo: Retorna una colección de registros de tipo ReferidoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListReferidoDTO ReferidoGetList(ReqFilterReferidoDTO oReqFilterReferidoDTO)
		{
		
			RespListReferidoDTO oRespListReferidoDTO = new RespListReferidoDTO();
		
			oRespListReferidoDTO.List = new List<ReferidoDTO>();
			oRespListReferidoDTO.User = oReqFilterReferidoDTO.User;
			oRespListReferidoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterReferidoDTO.User))
            {
                oRespListReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Referido no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterReferidoDTO.Paging == null)
            {
                oRespListReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListReferidoDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                   

                    List<ReferidoDTO> ReferidoDTOList = new List<ReferidoDTO>();

                    switch (oReqFilterReferidoDTO.FilterCase)
                    {
                        case filterCaseReferido.uspListarReferido_Paginacion:
                        {
                            if (!oReqFilterReferidoDTO.Paging.All && oReqFilterReferidoDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterReferidoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosReferido_NumeroRegistros"]);
                            }

                            ReferidoDTOList = oReferidoData.uspListarReferido_Paginacion(oReqFilterReferidoDTO.Item, oReqFilterReferidoDTO.Paging);
                        }
                        break;
                    }

                    oRespListReferidoDTO.List = ReferidoDTOList;
                    oRespListReferidoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListReferidoDTO.Success = false;
                    oRespListReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListReferidoDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ReferidoGetItem
		//Objetivo: Retorna un registro de tipo ReferidoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemReferidoDTO ReferidoGetItem(ReqFilterReferidoDTO oReqFilterReferidoDTO)
		{
			RespItemReferidoDTO oRespItemReferidoDTO = new RespItemReferidoDTO();

            oRespItemReferidoDTO.Success = false;
            oRespItemReferidoDTO.Item = null;
            oRespItemReferidoDTO.User = oReqFilterReferidoDTO.User;
            oRespItemReferidoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterReferidoDTO.User))
            {
                oRespItemReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Referido no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemReferidoDTO.MessageList.Count == 0)
            {
                ReferidoDTO oReferidoDTO = null;
                try
                {
                    switch (oReqFilterReferidoDTO.FilterCase)
                    {
                       
                        case filterCaseReferido.uspListarTablaReferido_NumeroRegistros:
                            {
                                oReferidoDTO = new ReferidoDTO();
                                oReferidoDTO = oReferidoData.uspListarTablaReferido_NumeroRegistros(oReqFilterReferidoDTO.Item);
                            }
                            break;

                        case filterCaseReferido.uspBuscarPorCodigo:
                            {
                                oReferidoDTO = new ReferidoDTO();
                                oReferidoDTO = oReferidoData.BuscarPorCodigoReferido(oReqFilterReferidoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oReferidoDTO = new ReferidoDTO();
                            }
                            break;
                    }

                    oRespItemReferidoDTO.Item = new ReferidoDTO();
                    oRespItemReferidoDTO.Item = oReferidoDTO;
                    oRespItemReferidoDTO.Success = true;
                    oRespItemReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemReferidoDTO.Success = false;
                    oRespItemReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemReferidoDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ReferidoDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespReferidoDTO ExecuteTransac(ReqReferidoDTO oReqReferidoDTO)
		{
			RespReferidoDTO oRespReferidoDTO = new RespReferidoDTO();

            oRespReferidoDTO.MessageList = new List<Mensaje>();
            oRespReferidoDTO.User = oReqReferidoDTO.User;
            
            if (String.IsNullOrEmpty(oReqReferidoDTO.User))
            {
                oRespReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Referido no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespReferidoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (ReferidoDTO item in oReqReferidoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    Codigo = 2;
                                    oReferidoData.Registrar(item);
                                    break;
                               
                                case Operation.UpdateReferidoAInvitado:
                                    UsuariosIngresosData oUsuariosIngresosDataUpdateReferidoAInvitado = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOUpdateReferidoAInvitado = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOUpdateReferidoAInvitado.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOUpdateReferidoAInvitado.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOUpdateReferidoAInvitado.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOUpdateReferidoAInvitado.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOUpdateReferidoAInvitado.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOUpdateReferidoAInvitado.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOUpdateReferidoAInvitado = oUsuariosIngresosDataUpdateReferidoAInvitado.uspValidarAccesoSistema(oUsuariosIngresosDTOUpdateReferidoAInvitado);
                                    if (oUsuariosIngresosDTOUpdateReferidoAInvitado.CodigoValidacion == 3)
                                    {
                                        Codigo = oReferidoData.uspActualizarReferidoAInvitado(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;

                                case Operation.UpdateReferidoASocio:
                                   
                                    Codigo = oReferidoData.uspActualizarReferidoASocio(item);

                                    break;
                                case Operation.Delete:
                                 
                                    Codigo = 999999999;
                                    oReferidoData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespReferidoDTO.Success = true;
                        oRespReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespReferidoDTO.Success = false;
                        oRespReferidoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespReferidoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
