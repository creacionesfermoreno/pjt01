
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
	//Archivo     : InvitadosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 07/05/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class InvitadosLogic: IDisposable
	{
		InvitadosData oInvitadosData = null;
		public InvitadosLogic()
		{
			oInvitadosData = new InvitadosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	InvitadosGetList
		//Objetivo: Retorna una colección de registros de tipo InvitadosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListInvitadosDTO InvitadosGetList(ReqFilterInvitadosDTO oReqFilterInvitadosDTO)
		{
		
			RespListInvitadosDTO oRespListInvitadosDTO = new RespListInvitadosDTO();
		
			oRespListInvitadosDTO.List = new List<InvitadosDTO>();
			oRespListInvitadosDTO.User = oReqFilterInvitadosDTO.User;
			oRespListInvitadosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterInvitadosDTO.User))
            {
                oRespListInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Invitados no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterInvitadosDTO.Paging == null)
            {
                oRespListInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListInvitadosDTO.MessageList.Count == 0)
            {
                
                try
                {
                    
                    List<InvitadosDTO> InvitadosDTOList = new List<InvitadosDTO>();

                    switch (oReqFilterInvitadosDTO.FilterCase)
                    {
                        case filterCaseInvitados.uspListarTablaInvitados_Paginacion:
                            if (!oReqFilterInvitadosDTO.Paging.All && oReqFilterInvitadosDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterInvitadosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarTablaInvitados_NumeroRegistros"]);
                            }
                            InvitadosDTOList = oInvitadosData.uspListarTablaInvitados_Paginacion(oReqFilterInvitadosDTO.Item, oReqFilterInvitadosDTO.Paging);
                            break;
                   
                        case filterCaseInvitados.uspListarInvitadosBusqueda:
                            InvitadosDTOList = oInvitadosData.uspListarInvitadosBusqueda(oReqFilterInvitadosDTO.Item);
                            break;
                      
                    }

                    oRespListInvitadosDTO.List = InvitadosDTOList;
                    oRespListInvitadosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListInvitadosDTO.Success = false;
                    oRespListInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListInvitadosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	InvitadosGetItem
		//Objetivo: Retorna un registro de tipo InvitadosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemInvitadosDTO InvitadosGetItem(ReqFilterInvitadosDTO oReqFilterInvitadosDTO)
		{
			RespItemInvitadosDTO oRespItemInvitadosDTO = new RespItemInvitadosDTO();

            oRespItemInvitadosDTO.Success = false;
            oRespItemInvitadosDTO.Item = null;
            oRespItemInvitadosDTO.User = oReqFilterInvitadosDTO.User;
            oRespItemInvitadosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterInvitadosDTO.User))
            {
                oRespItemInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Invitados no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemInvitadosDTO.MessageList.Count == 0)
            {
                InvitadosDTO oInvitadosDTO = null;
                try
                {
                    switch (oReqFilterInvitadosDTO.FilterCase)
                    {
                        case filterCaseInvitados.uspBuscarInfoPorCodInvitadoFiltro:
                            {
                                oInvitadosDTO = new InvitadosDTO();
                                oInvitadosDTO = oInvitadosData.uspBuscarInfoPorCodInvitadoFiltro(oReqFilterInvitadosDTO.Item);
                            }
                            break;
                        case filterCaseInvitados.uspBuscarClientesDatosInvitadosPorCodigo:
                            {
                                oInvitadosDTO = new InvitadosDTO();
                                oInvitadosDTO = oInvitadosData.uspBuscarClientesDatosInvitadosPorCodigo(oReqFilterInvitadosDTO.Item);
                            }
                            break;

                        case filterCaseInvitados.uspListarTablaInvitados_NumeroRegistros:
                            {
                                oInvitadosDTO = new InvitadosDTO();
                                oInvitadosDTO = oInvitadosData.uspListarTablaInvitados_NumeroRegistros(oReqFilterInvitadosDTO.Item);
                            }
                            break;


                        default:
                            {
                                oInvitadosDTO = new InvitadosDTO();
                            }
                            break;
                    }

                    oRespItemInvitadosDTO.Item = new InvitadosDTO();
                    oRespItemInvitadosDTO.Item = oInvitadosDTO;
                    oRespItemInvitadosDTO.Success = true;
                    oRespItemInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemInvitadosDTO.Success = false;
                    oRespItemInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemInvitadosDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo InvitadosDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespInvitadosDTO ExecuteTransac(ReqInvitadosDTO oReqInvitadosDTO)
		{
			RespInvitadosDTO oRespInvitadosDTO = new RespInvitadosDTO();

            oRespInvitadosDTO.MessageList = new List<Mensaje>();
            oRespInvitadosDTO.User = oReqInvitadosDTO.User;
            
            if (String.IsNullOrEmpty(oReqInvitadosDTO.User))
            {
                oRespInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Invitados no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespInvitadosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        foreach (InvitadosDTO item in oReqInvitadosDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:

                                    Codigo = 2;
                                    oInvitadosData.Registrar(item);

                                    break;
                              
                                case Operation.UpdateInvitadoSocio:

                                    Codigo = oInvitadosData.uspActualizarInvitadoASocio(item);
                                    break;

                                case Operation.Delete:
                                    UsuariosIngresosData oUsuariosIngresosDataDelete = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTODelete = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTODelete.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTODelete.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTODelete.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTODelete.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTODelete.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTODelete.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTODelete = oUsuariosIngresosDataDelete.uspValidarAccesoSistema(oUsuariosIngresosDTODelete);
                                    if (oUsuariosIngresosDTODelete.CodigoValidacion == 3)
                                    {
                                        Codigo = 999999999;
                                        oInvitadosData.Eliminar(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;
                            }
                        }
                        tx.Complete();
                        oRespInvitadosDTO.Success = true;
                        oRespInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespInvitadosDTO.Success = false;
                        oRespInvitadosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespInvitadosDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
