using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.IO;

namespace E_BusinessLayer.Gimnasio
{
    public class ConfiguracionLogic : IDisposable
    {   
        ConfiguracionData oConfiguracionData = null;
        public ConfiguracionLogic()
		{
            oConfiguracionData = new ConfiguracionData();
		}

        //-------------------------------------------------------------------
        //Nombre:	ConfiguracionGetList
        //Objetivo: Retorna una colección de registros de tipo ConfiguracionDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespListConfiguracionDTO ConfiguracionGetList(ReqFilterConfiguracionDTO oReqFilterConfiguracionDTO)
        {

            RespListConfiguracionDTO oRespListConfiguracionDTO = new RespListConfiguracionDTO();

            oRespListConfiguracionDTO.List = new List<ConfiguracionDTO>();
            oRespListConfiguracionDTO.User = oReqFilterConfiguracionDTO.User;
            oRespListConfiguracionDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterConfiguracionDTO.User))
            {
                oRespListConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterConfiguracionDTO.Paging == null)
            {
                oRespListConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            

            if (oRespListConfiguracionDTO.MessageList.Count == 0)
            {

                try
                {
                    
                    if (!oReqFilterConfiguracionDTO.Paging.All && oReqFilterConfiguracionDTO.Paging.PageRecords == 0)
                    {
                        oReqFilterConfiguracionDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                    }

                    List<ConfiguracionDTO> ConfiguracionDTOList = new List<ConfiguracionDTO>();

                    switch (oReqFilterConfiguracionDTO.FilterCase)
                    {
                        case filterCaseConfiguracion.uspByteFit_ListarTotalVentasPorEmpresa:
                            ConfiguracionDTOList = oConfiguracionData.uspByteFit_ListarTotalVentasPorEmpresa(oReqFilterConfiguracionDTO.Item);
                        break;
                        case filterCaseConfiguracion.uspByteFitVentasPorUN:
                            ConfiguracionDTOList = oConfiguracionData.uspByteFitVentasPorUN(oReqFilterConfiguracionDTO.Item);
                            break;
                        case filterCaseConfiguracion.ListarSedes:
                            ConfiguracionDTOList = oConfiguracionData.ListarSedes(oReqFilterConfiguracionDTO.Item);
                        break;
                        case filterCaseConfiguracion.uspListarConfiguracion_apfitness_Paginacion:
                            if (!oReqFilterConfiguracionDTO.Paging.All && oReqFilterConfiguracionDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterConfiguracionDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarConfiguracion_apfitness_NumeroRegistros"]);
                            }

                            ConfiguracionDTOList = oConfiguracionData.uspListarConfiguracion_apfitness_Paginacion(oReqFilterConfiguracionDTO.Item,oReqFilterConfiguracionDTO.Paging);
                            break;
                        case filterCaseConfiguracion.uspListarConfiguracion_Cobranzas_Paginacion:
                            if (!oReqFilterConfiguracionDTO.Paging.All && oReqFilterConfiguracionDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterConfiguracionDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarConfiguracion_apfitness_NumeroRegistros"]);
                            }

                            ConfiguracionDTOList = oConfiguracionData.uspListarConfiguracion_Cobranzas_Paginacion(oReqFilterConfiguracionDTO.Item, oReqFilterConfiguracionDTO.Paging);
                            break;
                        case filterCaseConfiguracion.uspListarConfiguracionMatriculas:
                          
                            ConfiguracionDTOList = oConfiguracionData.uspListarConfiguracionMatriculas(oReqFilterConfiguracionDTO.Item);
                            break;
                        case filterCaseConfiguracion.uspListarConfiguracionGastos:
                          
                            ConfiguracionDTOList = oConfiguracionData.uspListarConfiguracionGastos(oReqFilterConfiguracionDTO.Item);
                            break;

                        case filterCaseConfiguracion.uspListaBusquedaClienteContratoAdFitness:
                            ConfiguracionDTOList = oConfiguracionData.uspListaBusquedaClienteContratoAdFitness(oReqFilterConfiguracionDTO.Item);
                            break;
                        case filterCaseConfiguracion.uspListarSedesPorSedesPermisos:
                            {
                                ConfiguracionDTOList = oConfiguracionData.uspListarSedesPorSedesPermisos(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                       case filterCaseConfiguracion.uspListarConfiguracionCuentas:
                            {
                                ConfiguracionDTOList = oConfiguracionData.uspListarConfiguracionCuentas();
                            }
                            break;
                        case filterCaseConfiguracion.uspByteFitMatriculasMensuales:
                            {
                                ConfiguracionDTOList = oConfiguracionData.uspByteFitMatriculasMensuales(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.uspByteFitVentasResumen:
                            {
                                ConfiguracionDTOList = oConfiguracionData.uspByteFitVentasResumen(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.uspByteFitVentasMensuales:
                            {
                                ConfiguracionDTOList = oConfiguracionData.uspByteFitVentasMensuales(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.uspByteFitGastosMensuales:
                            {
                                ConfiguracionDTOList = oConfiguracionData.uspByteFitGastosMensuales(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.uspByteFitClientesNuevosDelMes:
                            {
                                ConfiguracionDTOList = oConfiguracionData.uspByteFitClientesNuevosDelMes(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                    }

                    oRespListConfiguracionDTO.List = ConfiguracionDTOList;
                    oRespListConfiguracionDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListConfiguracionDTO.Success = false;
                    oRespListConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListConfiguracionDTO;

        }

        //-------------------------------------------------------------------
        //Nombre:	ConfiguracionGetItem
        //Objetivo: Retorna un registro de tipo ConfiguracionDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespItemConfiguracionDTO ConfiguracionGetItem(ReqFilterConfiguracionDTO oReqFilterConfiguracionDTO)
        {
            RespItemConfiguracionDTO oRespItemConfiguracionDTO = new RespItemConfiguracionDTO();

            oRespItemConfiguracionDTO.Success = false;
            oRespItemConfiguracionDTO.Item = null;
            oRespItemConfiguracionDTO.User = oReqFilterConfiguracionDTO.User;
            oRespItemConfiguracionDTO.MessageList = new List<Mensaje>();

            if (String.IsNullOrEmpty(oReqFilterConfiguracionDTO.User))
            {
                oRespItemConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemConfiguracionDTO.MessageList.Count == 0)
            {
                ConfiguracionDTO oConfiguracionDTO = null;
                try
                {
                    switch (oReqFilterConfiguracionDTO.FilterCase)
                    {
                        case filterCaseConfiguracion.uspSeguridadObtenerUnidadNegocio:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.uspSeguridadObtenerUnidadNegocio(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.uspSeguridadObtenerUnidadNegocio_SubDominio:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.uspSeguridadObtenerUnidadNegocio_SubDominio(oReqFilterConfiguracionDTO.Item);
                            }
                            break;

                        case filterCaseConfiguracion.uspListarConfiguracion_apfitness_NumeroRegistros:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.uspListarConfiguracion_apfitness_NumeroRegistros(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.uspListarConfiguracion_Cobranzas_NumeroRegistros:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.uspListarConfiguracion_Cobranzas_NumeroRegistros(oReqFilterConfiguracionDTO.Item);
                            }
                            break;

                        case filterCaseConfiguracion.Buscar_RepartirClientes:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.Buscar_RepartirClientes(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                     
                        case filterCaseConfiguracion.BuscarConfiguracionDiasCitasCaida:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.BuscarConfiguracionDiasCitasCaida(oReqFilterConfiguracionDTO.Item);
                            }
                            break;

                        case filterCaseConfiguracion.BuscarConfiguracionTiempoMarcarAsistencia:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.BuscarConfiguracionTiempoMarcarAsistencia(oReqFilterConfiguracionDTO.Item);
                            }
                            break;

                        case filterCaseConfiguracion.BuscarConfiguracionAsistencia:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.BuscarConfiguracionAsistencia(oReqFilterConfiguracionDTO.Item);
                            }
                            break;

                        case filterCaseConfiguracion.BuscarPorCodigo:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.BuscarPorCodigoConfiguracion(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.filter_uspBuscarConfVentaOtrosPorCodigo:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.BuscarConfVentaOtrosPorCodigo(oReqFilterConfiguracionDTO.Item);
                            }
                            break;

                        case filterCaseConfiguracion.filter_uspBuscarConfCorreoBienvenidaPorCodigo:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.BuscarConfCorreoBienvenidaPorCodigo(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.buscarConfiguracionImprimirContrato:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.buscarConfiguracionImprimirContrato(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.uspBuscarConfiguracionControlPagoSoftware:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.uspBuscarConfiguracionControlPagoSoftware(oReqFilterConfiguracionDTO.Item);
                            }
                            break;

                        case filterCaseConfiguracion.uspBuscarConfiguracion_apfitness:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.uspBuscarConfiguracion_apfitness(oReqFilterConfiguracionDTO.Item);
                            }
                            break;
                        case filterCaseConfiguracion.CentroEntrenamiento_uspBuscarEmpresa_imprimirticket:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                                oConfiguracionDTO = oConfiguracionData.CentroEntrenamiento_uspBuscarEmpresa_imprimirticket(oReqFilterConfiguracionDTO.Item);
                            }
                            break;


                        default:
                            {
                                oConfiguracionDTO = new ConfiguracionDTO();
                            }
                            break;
                    }

                    oRespItemConfiguracionDTO.Item = new ConfiguracionDTO();
                    oRespItemConfiguracionDTO.Item = oConfiguracionDTO;
                    oRespItemConfiguracionDTO.Success = true;
                    oRespItemConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemConfiguracionDTO.Success = false;
                    oRespItemConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemConfiguracionDTO;
        }

        //-------------------------------------------------------------------
        //Nombre:	ExecuteTransac
        //Objetivo: Almacena el registro de un objeto de tipo ConfiguracionDTO
        //Valores Prueba:
        //Creacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //Modificacion: innovaperutec@gmail.com - Cristofer Moreno Roman cel: 997935214
        //-------------------------------------------------------------------
        public RespConfiguracionDTO ExecuteTransac(ReqConfiguracionDTO oReqConfiguracionDTO)
        {
            RespConfiguracionDTO oRespConfiguracionDTO = new RespConfiguracionDTO();

            oRespConfiguracionDTO.MessageList = new List<Mensaje>();
            oRespConfiguracionDTO.User = oReqConfiguracionDTO.User;

            if (String.IsNullOrEmpty(oReqConfiguracionDTO.User))
            {
                oRespConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Configuracion no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

           
            if (oRespConfiguracionDTO.MessageList.Count == 0)
            {
                int Estado = 0;
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        foreach (ConfiguracionDTO item in oReqConfiguracionDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.Create:
                                    oConfiguracionData.Registrar(item);
                                    break;
                                case Operation.Update:
                                    oConfiguracionData.Actualizar(item);
                                    break;
                                case Operation.uspActualizarConfiguracionDatosFormatoTicket:
                                    oConfiguracionData.uspActualizarConfiguracionDatosFormatoTicket(item);
                                    break;
                                case Operation.UpdateConfiguracionCitasCaidas:
                                    oConfiguracionData.UpdateConfiguracionCitasCaidas(item);
                                    break;
                                case Operation.UpdateObligarMarcarClaseAsistencia:
                                    oConfiguracionData.UpdateObligarMarcarClaseAsistencia(item);
                                    break;
                                case Operation.UpdateObligatorioIngresoDNI:
                                    oConfiguracionData.UpdateObligatorioIngresoDNI(item);
                                    break;
                                case Operation.UpdatePermitirMuchasAsistenciaPordia:
                                    oConfiguracionData.UpdatePermitirMuchasAsistenciaPordia(item);
                                    break;
                                case Operation.UpdateGenerarCodigoclienteAutomatico:
                                    oConfiguracionData.UpdateGenerarCodigoclienteAutomatico(item);
                                    break;
                                case Operation.UpdateConsultasNumeroDocumentoEntidades:
                                    oConfiguracionData.UpdateConsultasNumeroDocumentoEntidades(item);
                                    break;
                                case Operation.UpdateConfiguracionTiempoMarcarAsistencia:
                                    oConfiguracionData.UpdateConfiguracionTiempoMarcarAsistencia(item);
                                    break;

                                case Operation.UpdateConfiguracionAsistencia:
                                    oConfiguracionData.ActualizarConfiguracionAsistencia(item);
                                    break;
                                case Operation.UpdateConfiguracionFreezing:
                                    oConfiguracionData.ActualizarConfiguracionFreezing(item);
                                    break;
                                case Operation.Delete:
                                    oConfiguracionData.Eliminar(item);
                                    break;
                                case Operation.ActualizarDia_RepartirClientes:
                                    oConfiguracionData.ActualizarDia_RepartirClientes(item);
                                    break;
                                case Operation.UpdateEstadoVentasOtros:
                                    oConfiguracionData.ActualizarVentaOtros(item);
                                    break;
                                case Operation.UpdateCorreoBienvenida:
                                    oConfiguracionData.ActualizarCorreoBienvenida(item);
                                    break;
                                case Operation.Update_ImprimirContrato:
                                    oConfiguracionData.uspActualizarImprimirContrato(item);
                                    break;
                                case Operation.Update_uspActualizarControlPagoSoftware:
                                    oConfiguracionData.uspActualizarControlPagoSoftware(item);
                                    break;
                               case Operation.CreateConfiguracion_adFitness:
                                    oConfiguracionData.uspRegistrarConfiguracion_adFitness(item);
                                    break;
                                case Operation.uspRegistrarConfiguracionPagosMensualidades:
                                    oConfiguracionData.uspRegistrarConfiguracionPagosMensualidades(item);
                                    break;
                                case Operation.uspActualizarConfiguracionPagosMensualidadesRecibos:
                                    oConfiguracionData.uspActualizarConfiguracionPagosMensualidadesRecibos(item);
                                    break;
                                case Operation.uspRegistrarConfiguracionMatriculas:
                                    oConfiguracionData.uspRegistrarConfiguracionMatriculas(item);
                                    break;
                                case Operation.uspRegistrarConfiguracionGastos:
                                    oConfiguracionData.uspRegistrarConfiguracionGastos(item);
                                    break;
                                case Operation.UpdateConfiguracion_adFitness:
                                    oConfiguracionData.uspActualizarConfiguracion_adFitness(item);
                                    break;
                                case Operation.uspActualizarConfiguracionLogo_adFitness:
                                    oConfiguracionData.uspActualizarConfiguracionLogo_adFitness(item);
                                    break;
                                case Operation.uspEliminarCliente_Configuracion_AdFitness:
                                    oConfiguracionData.uspEliminarCliente_Configuracion_AdFitness(item);
                                    break;
                                case Operation.uspRegistrarConfiguracionConsultaDocumentoPersonas_Log:
                                    oConfiguracionData.uspRegistrarConfiguracionConsultaDocumentoPersonas_Log(item);
                                    break;
                                case Operation.uspActualizarConfiguracion_HostEnvioEmail:
                                    oConfiguracionData.uspActualizarConfiguracion_HostEnvioEmail(item);
                                    break;

                                case Operation.UpdateGenerarContratoAutomatico:
                                    oConfiguracionData.UpdateActivarImprimirContrato(item);
                                    break;

                                    
                                case Operation.uspRegistrarPaqueteSedePermiso:

                                    oConfiguracionData.uspEliminarPaqueteSedePermiso(item);
                                    foreach (ConfiguracionDTO nodo in item.Lista)
                                    {
                                        if (nodo.Estado == 1)
                                        {
                                            oConfiguracionData.uspRegistrarPaqueteSedePermiso(nodo);
                                        }
                                    }
                                    break;

                            }
                        }
                        tx.Complete();
                        oRespConfiguracionDTO.Success = true;
                        oRespConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = Estado,
                            Detalle = "Proceso Grabado Correctamente.",
                            Tipo = TipoMensaje.Informacion
                        });

                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespConfiguracionDTO.Success = false;
                        oRespConfiguracionDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespConfiguracionDTO;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public decimal Get_Igv()
        {
            return oConfiguracionData.Get_Igv();
        }

        public int Get_TipoDescuento(int CodigoUnidadNegocio)
        {
            return oConfiguracionData.Get_TipoDescuento(CodigoUnidadNegocio);
        }


        public int uspValidarConfiguracionAdFitness_UnidadNegocio_Sede(int CodigoUnidadNegocio, int CodigoSede,string Dominio)
        {
            int flag = 0;
            flag = oConfiguracionData.uspValidarConfiguracionAdFitness_UnidadNegocio_Sede(CodigoUnidadNegocio, CodigoSede, Dominio);
            return flag;
        }

    }
}
