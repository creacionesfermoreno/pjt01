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
	//Archivo     : MetasLogic.cs
	//Proyecto    : (NOMBRE DEL PROYECTO)
	//Creacion    : innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
	//Fecha       : 07/04/2015
	//Descripcion : Clase para capa de negocio
	//-------------------------------------------------------------------
	public class MetasLogic: IDisposable
	{
		MetasData oMetasData = null;
		public MetasLogic()
		{
			oMetasData = new MetasData();
		}
		
		//-------------------------------------------------------------------
		//Nombre:	MetasGetList
		//Objetivo: Retorna una colección de registros de tipo MetasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespListMetasDTO MetasGetList(ReqFilterMetasDTO oReqFilterMetasDTO)
		{
		
			RespListMetasDTO oRespListMetasDTO = new RespListMetasDTO();
		
			oRespListMetasDTO.List = new List<MetasDTO>();
			oRespListMetasDTO.User = oReqFilterMetasDTO.User;
			oRespListMetasDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterMetasDTO.User))
            {
                oRespListMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Metas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterMetasDTO.Paging == null)
            {
                oRespListMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListMetasDTO.MessageList.Count == 0)
            {
                
                try
                {
                   
                    List<MetasDTO> MetasDTOList = new List<MetasDTO>();
                    
                    switch (oReqFilterMetasDTO.FilterCase)
                    {
                        case filterCaseMetas.uspListarHistorialMetas:
                            
                            if (!oReqFilterMetasDTO.Paging.All && oReqFilterMetasDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterMetasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarHistorialMetas"]);
                            }
                            MetasDTOList = oMetasData.uspListarHistorialMetas(oReqFilterMetasDTO.Item, oReqFilterMetasDTO.Paging);
                            break;
                        case filterCaseMetas.uspListarMetasDetalle_VentasAvance:
                            MetasDTOList = oMetasData.uspListarMetasDetalle_VentasAvance(oReqFilterMetasDTO.Item);
                            break;
                        case filterCaseMetas.ListarporVendedores:
                            MetasDTOList = oMetasData.ListarVendedores(oReqFilterMetasDTO.Item);
                            break;
                        case filterCaseMetas.uspListarMetasDetalle_EstadisticaVenta:
                            MetasDTOList = oMetasData.uspListarMetasDetalle_EstadisticaVenta(oReqFilterMetasDTO.Item);
                            break;
                        case filterCaseMetas.uspListarMetasDetalle_CuadroComisiones:
                            MetasDTOList = oMetasData.uspListarMetasDetalle_CuadroComisiones(oReqFilterMetasDTO.Item);
                            break;
                        case filterCaseMetas.uspListarVerificadorCodigosComerciales:
                            MetasDTOList = oMetasData.uspListarVerificadorCodigosComerciales(oReqFilterMetasDTO.Item);
                            break;
                        case filterCaseMetas.uspListarVerificadorInformacionSociosComerciales_paginacion:
                            
                            if (!oReqFilterMetasDTO.Paging.All && oReqFilterMetasDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterMetasDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarVerificadorInformacionSociosComerciales_paginacion"]);
                            }
                            MetasDTOList = oMetasData.uspListarVerificadorInformacionSociosComerciales_paginacion(oReqFilterMetasDTO.Item, oReqFilterMetasDTO.Paging);

                        break;
                        case filterCaseMetas.uspListarProductividad_AreaComercial:
                            MetasDTOList = oMetasData.uspListarProductividad_AreaComercial(oReqFilterMetasDTO.Item);
                            break;
                        case filterCaseMetas.uspListarEfectivadadCitasVendedores:
                            MetasDTOList = oMetasData.uspListarEfectivadadCitasVendedores(oReqFilterMetasDTO.Item);
                            break; 
                        case filterCaseMetas.uspListarMetasDetalle_ConversionLeads_Totales:
                            MetasDTOList = oMetasData.uspListarMetasDetalle_ConversionLeads_Totales(oReqFilterMetasDTO.Item);
                            break;
                        case filterCaseMetas.uspListarMetricas_ConversionLeads_Totales:
                            MetasDTOList = oMetasData.uspListarMetricas_ConversionLeads_Totales(oReqFilterMetasDTO.Item);
                            break;
                    }

                    oRespListMetasDTO.List = MetasDTOList;
                    oRespListMetasDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListMetasDTO.Success = false;
                    oRespListMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListMetasDTO;	
           
		}
		
		//-------------------------------------------------------------------
		//Nombre:	MetasGetItem
		//Objetivo: Retorna un registro de tipo MetasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespItemMetasDTO MetasGetItem(ReqFilterMetasDTO oReqFilterMetasDTO)
		{
			RespItemMetasDTO oRespItemMetasDTO = new RespItemMetasDTO();

            oRespItemMetasDTO.Success = false;
            oRespItemMetasDTO.Item = null;
            oRespItemMetasDTO.User = oReqFilterMetasDTO.User;
            oRespItemMetasDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterMetasDTO.User))
            {
                oRespItemMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Metas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemMetasDTO.MessageList.Count == 0)
            {
                MetasDTO oMetasDTO = null;
                try
                {
                    switch (oReqFilterMetasDTO.FilterCase)
                    {
                        case filterCaseMetas.uspBuscarMetaVendedorPorCodigo:
                            {
                                oMetasDTO = new MetasDTO();
                                oMetasDTO = oMetasData.uspBuscarMetaVendedorPorCodigo(oReqFilterMetasDTO.Item);
                            }
                            break;
                       case filterCaseMetas.uspBuscarMetaVendedorPorMesActual:
                            {
                                oMetasDTO = new MetasDTO();
                                oMetasDTO = oMetasData.uspBuscarMetaVendedorPorMesActual(oReqFilterMetasDTO.Item);
                            }
                            break;
                        case filterCaseMetas.uspListarVerificadorInformacionSociosComerciales_NumeroRegistros:
                            {
                                oMetasDTO = new MetasDTO();
                                oMetasDTO = oMetasData.uspListarVerificadorInformacionSociosComerciales_NumeroRegistros(oReqFilterMetasDTO.Item);
                            }
                            break;
                        default:
                            {
                                oMetasDTO = new MetasDTO();
                            }
                            break;
                    }

                    oRespItemMetasDTO.Item = new MetasDTO();
                    oRespItemMetasDTO.Item = oMetasDTO;
                    oRespItemMetasDTO.Success = true;
                    oRespItemMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemMetasDTO.Success = false;
                    oRespItemMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemMetasDTO;
		}
	
		//-------------------------------------------------------------------
		//Nombre:	ExecuteTransac
		//Objetivo: Almacena el registro de un objeto de tipo MetasDTO
		//Valores Prueba:
		//Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
		//-------------------------------------------------------------------
		public RespMetasDTO ExecuteTransac(ReqMetasDTO oReqMetasDTO)
		{
			RespMetasDTO oRespMetasDTO = new RespMetasDTO();

            oRespMetasDTO.MessageList = new List<Mensaje>();
            oRespMetasDTO.User = oReqMetasDTO.User;
            
            if (String.IsNullOrEmpty(oReqMetasDTO.User))
            {
                oRespMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Metas no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespMetasDTO.MessageList.Count == 0)
            {
               // int campo = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {

                        int CodigoMeta = 0;
                        foreach (MetasDTO item in oReqMetasDTO.List)
                        {
                            switch (item.Operation)
                            {                              
                                case Operation.Create:
                                      MetasDetalleData oMetasDetalleData = new MetasDetalleData();
                                      CodigoMeta = oMetasData.Registrar(item);

                                      foreach (MetasDetalleDTO oMetasDetalleDTO in item.ListaDetalle)
                                      {
                                          oMetasDetalleDTO.CodigoEntidadNegocio = item.CodigoEntidadNegocio;
                                          oMetasDetalleDTO.CodigoSede = item.CodigoSede;
                                          oMetasDetalleDTO.CodigoMeta = CodigoMeta;
                                          oMetasDetalleDTO.FechaInicio = item.FechaInicio;
                                          oMetasDetalleDTO.FechaFin = item.FechaFin;
                                          oMetasDetalleDTO.UsuarioCreacion = item.UsuarioCreacion;

                                          oMetasDetalleData.Registrar(oMetasDetalleDTO);
                                      }

                                    break;
                                    case Operation.uspRegistrarMetaInicioMes:
                                      MetasDetalleData oMetasDetalleDataInicioMes = new MetasDetalleData();
                                      CodigoMeta = oMetasData.uspRegistrarMetaInicioMes(item);

                                      foreach (MetasDetalleDTO oMetasDetalleDTO in item.ListaDetalle)
                                      {
                                          oMetasDetalleDTO.CodigoEntidadNegocio = item.CodigoEntidadNegocio;
                                          oMetasDetalleDTO.CodigoSede = item.CodigoSede;
                                          oMetasDetalleDTO.CodigoMeta = CodigoMeta;
                                          oMetasDetalleDTO.FechaInicio = item.FechaInicio;
                                          oMetasDetalleDTO.FechaFin = item.FechaFin;
                                          oMetasDetalleDTO.UsuarioCreacion = item.UsuarioCreacion;
                                          oMetasDetalleDataInicioMes.Registrar(oMetasDetalleDTO);
                                      }

                                      //reparticion de renovaciones
                                    MetasDTO oMetasDetalleRepartirClientes = new MetasDTO();
                                    oMetasDetalleRepartirClientes.CodigoSede = item.CodigoSede;
                                    oMetasDetalleRepartirClientes.CodigoEntidadNegocio = item.CodigoEntidadNegocio;
                                    MetasData oMetasDetalleDataInicioMesRepartirClientesData = new MetasData();
                                    oMetasDetalleDataInicioMesRepartirClientesData.uspActualizarEstado_RepartirClientes(oMetasDetalleRepartirClientes);

                                    break;
                                case Operation.uspAsignarClienteInactivosSinCitaAVendedores:

                                    if (item.flagRepartirInactivos)
                                    {
                                        //reparticion de inactivos
                                        oMetasData.uspAsignarClienteInactivosSinCitaAVendedores(item, item.flagRepartirEquitativamenteSegunMeta);
                                    }

                                    if (item.flagRepartirRenovaciones)
                                    {
                                        //reparticion de renovaciones
                                        MetasDTO OMetasDTO = new MetasDTO();
                                        OMetasDTO.CodigoSede = item.CodigoSede;
                                        OMetasDTO.CodigoEntidadNegocio = item.CodigoEntidadNegocio;
                                        MetasData OMetasData_Repartir_renovaciones = new MetasData();
                                        OMetasData_Repartir_renovaciones.uspActualizarEstado_RepartirClientes(OMetasDTO);
                                    }

                                    if (item.flagRepartirProspectosSinCitaVendedoresInactivos)
                                    {
                                        ///ESTA SP BUSCA LOS PROSPECTOS SIN ACTIVIDAD DE LOS VENDEDORES INACTIVOS Y LOS REPARTE A LOS VENDEDORES ACTIVOS
                                        oMetasData.uspBuscarProspectosSinCitaVendedoresInactivosYRepartirVendedoresActivos(item);
                                    }

                                    if (item.flagRepartirProspectosSinActividadVendedoresActivos)
                                    {
                                        ///ESTA SP BUSCA LOS PROSPECTOS SIN ACTIVIDAD DE LOS VENDEDORES ACTIVOS Y LOS REPARTE A LOS VENDEDORES ACTIVOS
                                        oMetasData.uspBuscarProspectosSinCitaVendedoresActivosYRepartir(item);
                                    }
                                    break;
                                case Operation.Update:
                                    
                                    MetasDetalleData oMetasDetalleData2 = new MetasDetalleData();

                                    oMetasData.Actualizar(item);
                                    foreach (MetasDetalleDTO oMetasDetalleDTO in item.ListaDetalle)
                                    {
                                        oMetasDetalleDTO.UsuarioEdicion = item.UsuarioCreacion;
                                        oMetasDetalleData2.Actualizar(oMetasDetalleDTO);   
                                    }
                                    break;
                                case Operation.Delete:
                                    oMetasData.Eliminar(item);
                                    break;
                            }
                        }
                        tx.Complete();
                        oRespMetasDTO.Success = true;
                        oRespMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = CodigoMeta,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespMetasDTO.Success = false;
                        oRespMetasDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespMetasDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        public int uspValidarPrimerDiaMesConfiguracionMetas(int CodigoUnidadNegocio, int CodigoSede)
        {
            int flag = 0;
            flag = oMetasData.uspValidarPrimerDiaMesConfiguracionMetas(CodigoUnidadNegocio, CodigoSede);
            return flag;
        }


    }
}
