
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
	//Archivo     : EncuestaNuevo1Logic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 02/08/2017
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class EncuestaNuevo1Logic: IDisposable
	{
		EncuestaNuevo1Data oEncuestaNuevo1Data = null;
		public EncuestaNuevo1Logic()
		{
			oEncuestaNuevo1Data = new EncuestaNuevo1Data();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	EncuestaNuevo1GetList
		//Objetivo: Retorna una colección de registros de tipo EncuestaNuevo1DTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListEncuestaNuevo1DTO EncuestaNuevo1GetList(ReqFilterEncuestaNuevo1DTO oReqFilterEncuestaNuevo1DTO)
		{
		
			RespListEncuestaNuevo1DTO oRespListEncuestaNuevo1DTO = new RespListEncuestaNuevo1DTO();
		
			oRespListEncuestaNuevo1DTO.List = new List<EncuestaNuevo1DTO>();
			oRespListEncuestaNuevo1DTO.User = oReqFilterEncuestaNuevo1DTO.User;
			oRespListEncuestaNuevo1DTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterEncuestaNuevo1DTO.User))
            {
                oRespListEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EncuestaNuevo1 no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterEncuestaNuevo1DTO.Paging == null)
            {
                oRespListEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListEncuestaNuevo1DTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterEncuestaNuevo1DTO.Paging.All && oReqFilterEncuestaNuevo1DTO.Paging.PageRecords == 0)
                    {
                        oReqFilterEncuestaNuevo1DTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<EncuestaNuevo1DTO> EncuestaNuevo1DTOList = new List<EncuestaNuevo1DTO>();

                    switch (oReqFilterEncuestaNuevo1DTO.FilterCase)
                    {
                        case filterCaseEncuestaNuevo1.uspListarEncuestaNuevo2:
                            {
                                EncuestaNuevo1DTOList = oEncuestaNuevo1Data.uspListarEncuestaNuevo2(oReqFilterEncuestaNuevo1DTO.Item);
                            }
                          break;

                         case filterCaseEncuestaNuevo1.uspListarEncuestaEstadisticaObjetivos:
                            {
                                EncuestaNuevo1DTOList = oEncuestaNuevo1Data.uspListarEncuestaEstadisticaObjetivos(oReqFilterEncuestaNuevo1DTO.Item);
                            }
                          break;
                        case filterCaseEncuestaNuevo1.uspListarEstadisticaComoConocioGym:
                            {
                                EncuestaNuevo1DTOList = oEncuestaNuevo1Data.uspListarEncuestaEstadisticaComoConocioGym(oReqFilterEncuestaNuevo1DTO.Item);
                            }
                          break;

                       case filterCaseEncuestaNuevo1.uspListarEstadisticaInteres:
                            {
                                EncuestaNuevo1DTOList = oEncuestaNuevo1Data.uspListarEncuestaEstadisticaInteres(oReqFilterEncuestaNuevo1DTO.Item);
                            }
                          break;

                        default:
                            {
                              //  EncuestaNuevo1DTOList = oEncuestaNuevo1Data.Listar(oReqFilterEncuestaNuevo1DTO.Item, oReqFilterEncuestaNuevo1DTO.Paging, ref recordCount);
                            }
                            break;
                    }

                    oRespListEncuestaNuevo1DTO.List = EncuestaNuevo1DTOList;
                    oRespListEncuestaNuevo1DTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListEncuestaNuevo1DTO.Success = false;
                    oRespListEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListEncuestaNuevo1DTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	EncuestaNuevo1GetItem
		//Objetivo: Retorna un registro de tipo EncuestaNuevo1DTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemEncuestaNuevo1DTO EncuestaNuevo1GetItem(ReqFilterEncuestaNuevo1DTO oReqFilterEncuestaNuevo1DTO)
		{
			RespItemEncuestaNuevo1DTO oRespItemEncuestaNuevo1DTO = new RespItemEncuestaNuevo1DTO();

            oRespItemEncuestaNuevo1DTO.Success = false;
            oRespItemEncuestaNuevo1DTO.Item = null;
            oRespItemEncuestaNuevo1DTO.User = oReqFilterEncuestaNuevo1DTO.User;
            oRespItemEncuestaNuevo1DTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterEncuestaNuevo1DTO.User))
            {
                oRespItemEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EncuestaNuevo1 no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemEncuestaNuevo1DTO.MessageList.Count == 0)
            {
                EncuestaNuevo1DTO oEncuestaNuevo1DTO = null;
                try
                {
                    switch (oReqFilterEncuestaNuevo1DTO.FilterCase)
                    {
                       
                        case filterCaseEncuestaNuevo1.uspBuscarEncuestaNuevo1:
                            {
                                oEncuestaNuevo1DTO = new EncuestaNuevo1DTO();
                                oEncuestaNuevo1DTO = oEncuestaNuevo1Data.uspBuscarEncuestaNuevo1(oReqFilterEncuestaNuevo1DTO.Item);
                            }
                            break;
                        default:
                            {
                                oEncuestaNuevo1DTO = new EncuestaNuevo1DTO();
                            }
                            break;
                    }

                    oRespItemEncuestaNuevo1DTO.Item = new EncuestaNuevo1DTO();
                    oRespItemEncuestaNuevo1DTO.Item = oEncuestaNuevo1DTO;
                    oRespItemEncuestaNuevo1DTO.Success = true;
                    oRespItemEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemEncuestaNuevo1DTO.Success = false;
                    oRespItemEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemEncuestaNuevo1DTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo EncuestaNuevo1DTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespEncuestaNuevo1DTO ExecuteTransac(ReqEncuestaNuevo1DTO oReqEncuestaNuevo1DTO)
		{
			RespEncuestaNuevo1DTO oRespEncuestaNuevo1DTO = new RespEncuestaNuevo1DTO();

            oRespEncuestaNuevo1DTO.MessageList = new List<Mensaje>();
            oRespEncuestaNuevo1DTO.User = oReqEncuestaNuevo1DTO.User;
            
            if (String.IsNullOrEmpty(oReqEncuestaNuevo1DTO.User))
            {
                oRespEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de EncuestaNuevo1 no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespEncuestaNuevo1DTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int Codigo = 0;
                        int CodigoValidacionOperaciones = 999999999;
                        foreach (EncuestaNuevo1DTO item in oReqEncuestaNuevo1DTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    UsuariosIngresosData oUsuariosIngresosDataCreate = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOCreate = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOCreate.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOCreate.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOCreate.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOCreate.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOCreate.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOCreate.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOCreate = oUsuariosIngresosDataCreate.uspValidarAccesoSistema(oUsuariosIngresosDTOCreate);
                                    if (oUsuariosIngresosDTOCreate.CodigoValidacion == 3)
                                    {
                                        UsuariosIngresosData oUsuariosIngresosDataValidacion = new UsuariosIngresosData();
                                        UsuariosIngresosDTO oUsuariosIngresosDTOValidacion = new UsuariosIngresosDTO();
                                        oUsuariosIngresosDTOValidacion.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        oUsuariosIngresosDTOValidacion.CodigoSede = item.CodigoSede;
                                        oUsuariosIngresosDTOValidacion.UsuarioCreacion = item.UsuarioCreacion;
                                        oUsuariosIngresosDTOValidacion.CodigoInicioSesion = item.CodigoInicioSesion;
                                        oUsuariosIngresosDTOValidacion.Operacion = "I";
                                        oUsuariosIngresosDTOValidacion.DescripcionTabla = "EncuestaNuevo1";

                                        CodigoValidacionOperaciones = oUsuariosIngresosDataValidacion.uspObtenerValidacionOperaciones(oUsuariosIngresosDTOValidacion);

                                        if (CodigoValidacionOperaciones == 0)
                                        {

                                            Codigo = 999999999;
                                            //registrandoEncuestaDatos1
                                            oEncuestaNuevo1Data.Registrar(item);
                                            //eliminamos EncuestaDatos2 si existe para registrar
                                            EncuestaNuevo1DTO EncuestaNuevo2_E = new EncuestaNuevo1DTO();
                                            EncuestaNuevo2_E.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            EncuestaNuevo2_E.CodigoSede = item.CodigoSede;
                                            EncuestaNuevo2_E.CodigoProspecto = item.CodigoProspecto;
                                            oEncuestaNuevo1Data.uspEliminarEncuestaDatos2(EncuestaNuevo2_E);
                                            //registrandoEncuestaDatos2
                                            foreach (var item_1 in item.ListaDetalle_E)
                                            {
                                                EncuestaNuevo1DTO EncuestaNuevo1 = new EncuestaNuevo1DTO();
                                                EncuestaNuevo1.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                EncuestaNuevo1.CodigoSede = item.CodigoSede;
                                                EncuestaNuevo1.CodigoEncuestaNuevo1 = 0;
                                                EncuestaNuevo1.CodigoProspecto = item.CodigoProspecto;
                                                EncuestaNuevo1.CodigoInteres = item_1.CodigoInteres;
                                                EncuestaNuevo1.UsuarioCreacion = item.UsuarioCreacion;
                                                oEncuestaNuevo1Data.uspRegistrarEncuestaDatos2(EncuestaNuevo1);
                                            }

                                        }



                                    }
                                    else
                                    {
                                        Codigo = 0;
                                    }

                                    break;
                                case Operation.Update:
                                   // oEncuestaNuevo1Data.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                    //oEncuestaNuevo1Data.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespEncuestaNuevo1DTO.Success = true;
                        oRespEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespEncuestaNuevo1DTO.Success = false;
                        oRespEncuestaNuevo1DTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespEncuestaNuevo1DTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
