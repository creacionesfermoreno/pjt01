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
	//Archivo     : PerfilMenuLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 11/25/2014
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class PerfilMenuLogic: IDisposable
	{
		PerfilMenuData oPerfilMenuData = null;
		public PerfilMenuLogic()
		{
			oPerfilMenuData = new PerfilMenuData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PerfilMenuGetList
		//Objetivo: Retorna una colección de registros de tipo PerfilMenuDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListPerfilMenuDTO PerfilMenuGetList(ReqFilterPerfilMenuDTO oReqFilterPerfilMenuDTO)
		{
		
			RespListPerfilMenuDTO oRespListPerfilMenuDTO = new RespListPerfilMenuDTO();
		
			oRespListPerfilMenuDTO.List = new List<PerfilMenuDTO>();
			oRespListPerfilMenuDTO.User = oReqFilterPerfilMenuDTO.User;
			oRespListPerfilMenuDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterPerfilMenuDTO.User))
            {
                oRespListPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PerfilMenu no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterPerfilMenuDTO.Paging == null)
            {
                oRespListPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListPerfilMenuDTO.MessageList.Count == 0)
            {
                
                try
                {
                    uint recordCount = 0;
                    
                    if (!oReqFilterPerfilMenuDTO.Paging.All && oReqFilterPerfilMenuDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterPerfilMenuDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<PerfilMenuDTO> PerfilMenuDTOList = new List<PerfilMenuDTO>();

                    switch (oReqFilterPerfilMenuDTO.FilterCase)
                    {
                        case filterCasePerfilMenu.SEGListarPerfilMenuPermisos:
                            {
                              PerfilMenuDTOList = oPerfilMenuData.SEGListarPerfilMenuPermisos(oReqFilterPerfilMenuDTO.Item);
                            }
                            break;
                        default:
                            {
                                PerfilMenuDTOList = oPerfilMenuData.Listar(oReqFilterPerfilMenuDTO.Item);
                            }
                            break;
                    }

                    oRespListPerfilMenuDTO.List = PerfilMenuDTOList;
                    oRespListPerfilMenuDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListPerfilMenuDTO.Success = false;
                    oRespListPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListPerfilMenuDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	PerfilMenuGetItem
		//Objetivo: Retorna un registro de tipo PerfilMenuDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemPerfilMenuDTO PerfilMenuGetItem(ReqFilterPerfilMenuDTO oReqFilterPerfilMenuDTO)
		{
			RespItemPerfilMenuDTO oRespItemPerfilMenuDTO = new RespItemPerfilMenuDTO();

            oRespItemPerfilMenuDTO.Success = false;
            oRespItemPerfilMenuDTO.Item = null;
            oRespItemPerfilMenuDTO.User = oReqFilterPerfilMenuDTO.User;
            oRespItemPerfilMenuDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterPerfilMenuDTO.User))
            {
                oRespItemPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PerfilMenu no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemPerfilMenuDTO.MessageList.Count == 0)
            {
                PerfilMenuDTO oPerfilMenuDTO = null;
                try
                {
                    switch (oReqFilterPerfilMenuDTO.FilterCase)
                    {
                       
                        default:
                            {
                                oPerfilMenuDTO = new PerfilMenuDTO();
                            }
                            break;
                    }

                    oRespItemPerfilMenuDTO.Item = new PerfilMenuDTO();
                    oRespItemPerfilMenuDTO.Item = oPerfilMenuDTO;
                    oRespItemPerfilMenuDTO.Success = true;
                    oRespItemPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemPerfilMenuDTO.Success = false;
                    oRespItemPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemPerfilMenuDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo PerfilMenuDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespPerfilMenuDTO ExecuteTransac(ReqPerfilMenuDTO oReqPerfilMenuDTO)
		{
			RespPerfilMenuDTO oRespPerfilMenuDTO = new RespPerfilMenuDTO();

            oRespPerfilMenuDTO.MessageList = new List<Mensaje>();
            oRespPerfilMenuDTO.User = oReqPerfilMenuDTO.User;
            
            if (String.IsNullOrEmpty(oReqPerfilMenuDTO.User))
            {
                oRespPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de PerfilMenu no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespPerfilMenuDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigo = 0;
                        int CodigoValidacionOperaciones = 999999999;
                        foreach (PerfilMenuDTO item in oReqPerfilMenuDTO.List)
                        {
                            switch (item.Operation)
                            {
                              
                                case Operation.SEGRegistrarPerfilMenuPermisos:

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
                                        oUsuariosIngresosDTOValidacion.DescripcionTabla = "SEG_PerfilMenu";

                                        CodigoValidacionOperaciones = oUsuariosIngresosDataValidacion.uspObtenerValidacionOperaciones(oUsuariosIngresosDTOValidacion);

                                        if (CodigoValidacionOperaciones == 0)
                                        {

                                            codigo = 999999999;
                                            PerfilMenuDTO oPerfilMenuDTO = new PerfilMenuDTO();
                                            oPerfilMenuDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            oPerfilMenuDTO.CodigoPerfil = item.CodigoPerfil;
                                            oPerfilMenuData.SEGEliminarPerfilMenuPermisos(oPerfilMenuDTO);

                                            foreach (var item_0 in item.ListaDetalle_MenuSuperior)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_MenuSuperior = new PerfilMenuDTO();
                                                oPerfilMenuDTO_MenuSuperior.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_MenuSuperior.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_MenuSuperior.CodigoMenu = item_0.CodigoMenu;
                                                oPerfilMenuDTO_MenuSuperior.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_MenuSuperior);
                                            }


                                            foreach (var item_1 in item.ListaDetalle_Clientes)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_Clientes = new PerfilMenuDTO();
                                                oPerfilMenuDTO_Clientes.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_Clientes.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_Clientes.CodigoMenu = item_1.CodigoMenu;
                                                oPerfilMenuDTO_Clientes.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_Clientes);
                                            }

                                            foreach (var item_2 in item.ListaDetalle_Agenda)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_Agenda = new PerfilMenuDTO();
                                                oPerfilMenuDTO_Agenda.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_Agenda.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_Agenda.CodigoMenu = item_2.CodigoMenu;
                                                oPerfilMenuDTO_Agenda.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_Agenda);
                                            }
                                            foreach (var item_3 in item.ListaDetalle_CuadroVentas)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_CuadroVentas = new PerfilMenuDTO();
                                                oPerfilMenuDTO_CuadroVentas.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_CuadroVentas.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_CuadroVentas.CodigoMenu = item_3.CodigoMenu;
                                                oPerfilMenuDTO_CuadroVentas.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_CuadroVentas);
                                            }
                                            foreach (var item_4 in item.ListaDetalle_MetasyBonos)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_MetasYBonos = new PerfilMenuDTO();
                                                oPerfilMenuDTO_MetasYBonos.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_MetasYBonos.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_MetasYBonos.CodigoMenu = item_4.CodigoMenu;
                                                oPerfilMenuDTO_MetasYBonos.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_MetasYBonos);
                                            }
                                            foreach (var item_5 in item.ListaDetalle_Estadisticas)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_Estadisticas = new PerfilMenuDTO();
                                                oPerfilMenuDTO_Estadisticas.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_Estadisticas.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_Estadisticas.CodigoMenu = item_5.CodigoMenu;
                                                oPerfilMenuDTO_Estadisticas.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_Estadisticas);
                                            }
                                            foreach (var item_6 in item.ListaDetalle_Inventario)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_Inventario = new PerfilMenuDTO();
                                                oPerfilMenuDTO_Inventario.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_Inventario.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_Inventario.CodigoMenu = item_6.CodigoMenu;
                                                oPerfilMenuDTO_Inventario.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_Inventario);
                                            }
                                            foreach (var item_7 in item.ListaDetalle_NutricionMedidas)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_NutricionMedidas = new PerfilMenuDTO();
                                                oPerfilMenuDTO_NutricionMedidas.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_NutricionMedidas.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_NutricionMedidas.CodigoMenu = item_7.CodigoMenu;
                                                oPerfilMenuDTO_NutricionMedidas.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_NutricionMedidas);
                                            }
                                            foreach (var item_8 in item.ListaDetalle_Patrimonios)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_Patrimonios = new PerfilMenuDTO();
                                                oPerfilMenuDTO_Patrimonios.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_Patrimonios.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_Patrimonios.CodigoMenu = item_8.CodigoMenu;
                                                oPerfilMenuDTO_Patrimonios.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_Patrimonios);
                                            }
                                            foreach (var item_9 in item.ListaDetalle_PagoProfesores)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_PagoProfesores = new PerfilMenuDTO();
                                                oPerfilMenuDTO_PagoProfesores.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_PagoProfesores.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_PagoProfesores.CodigoMenu = item_9.CodigoMenu;
                                                oPerfilMenuDTO_PagoProfesores.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_PagoProfesores);
                                            }
                                            foreach (var item_10 in item.ListaDetalle_Ajustes)
                                            {
                                                PerfilMenuDTO oPerfilMenuDTO_Ajustes = new PerfilMenuDTO();
                                                oPerfilMenuDTO_Ajustes.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                oPerfilMenuDTO_Ajustes.CodigoPerfil = item.CodigoPerfil;
                                                oPerfilMenuDTO_Ajustes.CodigoMenu = item_10.CodigoMenu;
                                                oPerfilMenuDTO_Ajustes.UsuarioCreacion = item.UsuarioCreacion;
                                                oPerfilMenuData.SEGRegistrarPerfilMenuPermisos(oPerfilMenuDTO_Ajustes);
                                            }


                                        }


                                    }
                                    else
                                    {
                                        codigo = 0;
                                    }
                                    break;

                                case Operation.Update:
                                    //oPerfilMenuData.Actualizar(item);
                                    break;
                                case Operation.Delete:
                                   // oPerfilMenuData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespPerfilMenuDTO.Success = true;
                        oRespPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = codigo,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespPerfilMenuDTO.Success = false;
                        oRespPerfilMenuDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespPerfilMenuDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


    }
}
