
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
	//Archivo     : ProspectosLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 06/09/2016
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class ProspectosLogic: IDisposable
	{
		ProspectosData oProspectosData = null;
		public ProspectosLogic()
		{
			oProspectosData = new ProspectosData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ProspectosGetList
		//Objetivo: Retorna una colección de registros de tipo ProspectosTablaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListProspectosDTO ProspectosGetList(ReqFilterProspectosDTO oReqFilterProspectosDTO)
		{
		
			RespListProspectosDTO oRespListProspectosDTO = new RespListProspectosDTO();
		
			oRespListProspectosDTO.List = new List<ProspectosTablaDTO>();
			oRespListProspectosDTO.User = oReqFilterProspectosDTO.User;
			oRespListProspectosDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterProspectosDTO.User))
            {
                oRespListProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Prospectos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterProspectosDTO.Paging == null)
            {
                oRespListProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListProspectosDTO.MessageList.Count == 0)
            {
                
                try
                {
                   // uint recordCount = 0;
                    
                  

                    List<ProspectosTablaDTO> ProspectosDTOList = new List<ProspectosTablaDTO>();

                    switch (oReqFilterProspectosDTO.FilterCase)
                    {
                      
                        case filterCaseTablaProspectos.uspListarTablaPropectos_Paginacion:
                        {
                            if (!oReqFilterProspectosDTO.Paging.All && oReqFilterProspectosDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterProspectosDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListartablaProspectos_Paginacion"]);
                            }

                            ProspectosDTOList = oProspectosData.uspListarTablaPropectos_Paginacion(oReqFilterProspectosDTO.Item, oReqFilterProspectosDTO.Paging);
                        }
                        break;
                        case filterCaseTablaProspectos.UspListarProspectosSinActividadAgendaComercial:
                            {
                                if (!oReqFilterProspectosDTO.Paging.All && oReqFilterProspectosDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterProspectosDTO.Paging.PageRecords = 120;
                                }

                                ProspectosDTOList = oProspectosData.UspListarProspectosSinActividadAgendaComercial(oReqFilterProspectosDTO.Item, oReqFilterProspectosDTO.Paging);
                            }
                            break;
                        case filterCaseTablaProspectos.uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion:
                            {
                                if (!oReqFilterProspectosDTO.Paging.All && oReqFilterProspectosDTO.Paging.PageRecords == 0)
                                {
                                    oReqFilterProspectosDTO.Paging.PageRecords = 120;
                                }

                                ProspectosDTOList = oProspectosData.uspListarProspectosHistorialEliminadosEnviadosACliente_Paginacion(oReqFilterProspectosDTO.Item, oReqFilterProspectosDTO.Paging);
                            }
                            break;
                        case filterCaseTablaProspectos.uspListarProspectosValidadorExisteDNI:
                            {
                                ProspectosDTOList = oProspectosData.uspListarProspectosValidadorExisteDNI(oReqFilterProspectosDTO.Item);
                            }
                            break;
                        default:
                            {
                               
                            }
                            break;
                    }

                    oRespListProspectosDTO.List = ProspectosDTOList;
                    oRespListProspectosDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListProspectosDTO.Success = false;
                    oRespListProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListProspectosDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	ProspectosGetItem
		//Objetivo: Retorna un registro de tipo ProspectosTablaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemProspectosDTO ProspectosGetItem(ReqFilterProspectosDTO oReqFilterProspectosDTO)
		{
			RespItemProspectosDTO oRespItemProspectosDTO = new RespItemProspectosDTO();

            oRespItemProspectosDTO.Success = false;
            oRespItemProspectosDTO.Item = null;
            oRespItemProspectosDTO.User = oReqFilterProspectosDTO.User;
            oRespItemProspectosDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterProspectosDTO.User))
            {
                oRespItemProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Prospectos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemProspectosDTO.MessageList.Count == 0)
            {
                ProspectosTablaDTO oProspectosDTO = null;
                try
                {
                    switch (oReqFilterProspectosDTO.FilterCase)
                    {

                        case filterCaseTablaProspectos.uspBuscarClientesProspectosPorCodigo:
                            {
                                oProspectosDTO = new ProspectosTablaDTO();
                                oProspectosDTO = oProspectosData.uspBuscarClientesProspectosPorCodigo(oReqFilterProspectosDTO.Item);
                            }
                            break;

                      
                        case filterCaseTablaProspectos.uspListarTablaProspectos_NumeroRegistros:
                            {
                                oProspectosDTO = new ProspectosTablaDTO();
                                oProspectosDTO = oProspectosData.uspListarTablaProspectos_NumeroRegistros(oReqFilterProspectosDTO.Item);
                            }
                            break;
                        case filterCaseTablaProspectos.UspListarProspectosSinActividadAgendaComercial_NumeroRegistros:
                            {
                                oProspectosDTO = new ProspectosTablaDTO();
                                oProspectosDTO = oProspectosData.UspListarProspectosSinActividadAgendaComercial_NumeroRegistros(oReqFilterProspectosDTO.Item);
                            }
                            break;

                        case filterCaseTablaProspectos.uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro:
                            {
                                oProspectosDTO = new ProspectosTablaDTO();
                                oProspectosDTO = oProspectosData.uspListarProspectosHistorialEliminadosEnviadosACliente_NumeroRegistro(oReqFilterProspectosDTO.Item);
                            }
                            break;

                        default:
                            {
                                oProspectosDTO = new ProspectosTablaDTO();
                            }
                            break;
                    }

                    oRespItemProspectosDTO.Item = new ProspectosTablaDTO();
                    oRespItemProspectosDTO.Item = oProspectosDTO;
                    oRespItemProspectosDTO.Success = true;
                    oRespItemProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemProspectosDTO.Success = false;
                    oRespItemProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemProspectosDTO;
		}
	    
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo ProspectosTablaDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespProspectosDTO ExecuteTransac(ReqProspectosDTO oReqProspectosDTO)
		{
			RespProspectosDTO oRespProspectosDTO = new RespProspectosDTO();

            oRespProspectosDTO.MessageList = new List<Mensaje>();
            oRespProspectosDTO.User = oReqProspectosDTO.User;
            
            if (String.IsNullOrEmpty(oReqProspectosDTO.User))
            {
                oRespProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Prospectos no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespProspectosDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    
                    try
                    {
                        
                        int CodigoProspecto = 0;
                        int Codigo = 0;

                        foreach (ProspectosTablaDTO item in oReqProspectosDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:

                                    Codigo = 2;
                                    //registrando prospecto 
                                    CodigoProspecto = oProspectosData.Registrar(item);

                                    //registrando encuesta 1
                                    EncuestaNuevo1DTO EncuestaNuevo1_Dto = new EncuestaNuevo1DTO();
                                    EncuestaNuevo1Data oEncuestaNuevo1Data = new EncuestaNuevo1Data();
                                    EncuestaNuevo1_Dto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    EncuestaNuevo1_Dto.CodigoSede = item.CodigoSede;
                                    EncuestaNuevo1_Dto.CodigoEncuestaNuevo1 = 0;
                                    EncuestaNuevo1_Dto.CodigoProspecto = CodigoProspecto;
                                    EncuestaNuevo1_Dto.CodigoObjetivo = item.CodigoObjetivo;
                                    EncuestaNuevo1_Dto.CodigoComoConocioGym = item.CodigoComoConocioGym;
                                    EncuestaNuevo1_Dto.UsuarioCreacion = item.UsuarioCreacion;
                                    oEncuestaNuevo1Data.Registrar(EncuestaNuevo1_Dto);

                                    //eliminamos EncuestaDatos2 si existe para registrar
                                    EncuestaNuevo1DTO EncuestaNuevo2_Delete = new EncuestaNuevo1DTO();
                                    EncuestaNuevo1Data oEncuestaNuevo2DataDelete = new EncuestaNuevo1Data();
                                    EncuestaNuevo2_Delete.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    EncuestaNuevo2_Delete.CodigoSede = item.CodigoSede;
                                    EncuestaNuevo2_Delete.CodigoProspecto = CodigoProspecto;
                                    oEncuestaNuevo2DataDelete.uspEliminarEncuestaDatos2(EncuestaNuevo2_Delete);

                                    //registrandoEncuestaDatos2
                                    foreach (var item_1 in item.ListaDetalle_E)
                                    {
                                        EncuestaNuevo1DTO EncuestaNuevo2 = new EncuestaNuevo1DTO();
                                        EncuestaNuevo1Data oEncuestaNuevo2Data = new EncuestaNuevo1Data();
                                        EncuestaNuevo2.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        EncuestaNuevo2.CodigoSede = item.CodigoSede;
                                        EncuestaNuevo2.CodigoEncuestaNuevo1 = 0;
                                        EncuestaNuevo2.CodigoOrigenProspecto = item.CodigoOrigen;
                                        EncuestaNuevo2.CodigoProspecto = CodigoProspecto;
                                        EncuestaNuevo2.CodigoInteres = item_1.CodigoInteres;
                                        
                                        EncuestaNuevo2.UsuarioCreacion = item.UsuarioCreacion;
                                        oEncuestaNuevo2Data.uspRegistrarEncuestaDatos2(EncuestaNuevo2);
                                    }


                                    break;

                                case Operation.UpdateProspectoASocio:

                                    Codigo = oProspectosData.uspActualizarProspectoASocio(item);


                                    break;
                                case Operation.UpdateProspectoAInvvitado:
                                    UsuariosIngresosData oUsuariosIngresosDataUpdateProspectoAInvvitado = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOUpdateProspectoAInvvitado = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOUpdateProspectoAInvvitado.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOUpdateProspectoAInvvitado.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOUpdateProspectoAInvvitado.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOUpdateProspectoAInvvitado.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOUpdateProspectoAInvvitado.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOUpdateProspectoAInvvitado.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOUpdateProspectoAInvvitado = oUsuariosIngresosDataUpdateProspectoAInvvitado.uspValidarAccesoSistema(oUsuariosIngresosDTOUpdateProspectoAInvvitado);
                                    if (oUsuariosIngresosDTOUpdateProspectoAInvvitado.CodigoValidacion == 3)
                                    {
                                        Codigo = oProspectosData.uspActualizarProspectoAInvitado(item);
                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }
 
                                    break;
                                case Operation.Delete:
                                    Codigo = 100;
                                    oProspectosData.Eliminar(item);
                                    
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespProspectosDTO.Success = true;
                        oRespProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespProspectosDTO.Success = false;
                        oRespProspectosDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespProspectosDTO;
		}
		


		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
