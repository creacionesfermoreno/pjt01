using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Configuration;
using E_DataLayer.Gimnasio;
using E_DataModel.Gimnasio;
using E_DataModel.Common;

using E_DataLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;

namespace E_BusinessLayer.Gimnasio
{
	
	public class ContratoLogic: IDisposable
	{
		ContratoData oContratoData = null;
		public ContratoLogic()
		{
			oContratoData = new ContratoData();
		}
		
		public RespListContratoDTO ContratoGetList(ReqFilterContratoDTO oReqFilterContratoDTO)
		{
		    
			RespListContratoDTO oRespListContratoDTO = new RespListContratoDTO();
		    
			oRespListContratoDTO.List = new List<ContratoDTO>();
			oRespListContratoDTO.User = oReqFilterContratoDTO.User;
			oRespListContratoDTO.MessageList = new List<Mensaje>();
		    
			if (String.IsNullOrEmpty(oReqFilterContratoDTO.User))
            {
                oRespListContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Menbresias no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oReqFilterContratoDTO.Paging == null)
            {
                oRespListContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }
			
			if (oRespListContratoDTO.MessageList.Count == 0)
            {
                
                try
                {   
                    List<ContratoDTO> ContratoDTOList = new List<ContratoDTO>();

                    switch (oReqFilterContratoDTO.FilterCase)
                    {
                        
                        case filterCaseContrato.ListarMembresiasSocios:
                            ContratoDTOList = oContratoData.uspListarMembresiasSocios(oReqFilterContratoDTO.Item);
                            
                            break;
                        case filterCaseContrato.appsfit_uspListarMembresiasSocios:
                            ContratoDTOList = oContratoData.appsfit_uspListarMembresiasSocios(oReqFilterContratoDTO.Item);

                            break;
                        case filterCaseContrato.uspListarMembresiasContrato:
                            ContratoDTOList = oContratoData.uspListarMembresiasContrato(oReqFilterContratoDTO.Item);
                            break;

                        case filterCaseContrato.ListarMembresiasTraspasoSocios:
                            ContratoDTOList = oContratoData.uspListarMembresiasTraspasoSocios(oReqFilterContratoDTO.Item);
                            break;

                        case filterCaseContrato.uspListarMembresiasSociosAcuenta_Paginacion:
                            
                            if (!oReqFilterContratoDTO.Paging.All && oReqFilterContratoDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarMembresiasSociosAcuenta_Paginacion"]);
                            }

                            ContratoDTOList = oContratoData.uspListarMembresiasSociosAcuenta_Paginacion(oReqFilterContratoDTO.Item, oReqFilterContratoDTO.Paging);
                            break;
                        case filterCaseContrato.uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion:
                            
                            if (!oReqFilterContratoDTO.Paging.All && oReqFilterContratoDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }

                            ContratoDTOList = oContratoData.uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion(oReqFilterContratoDTO.Item, oReqFilterContratoDTO.Paging);
                            break;

                          case filterCaseContrato.uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion:
                            
                            if (!oReqFilterContratoDTO.Paging.All && oReqFilterContratoDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }

                            ContratoDTOList = oContratoData.uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion(oReqFilterContratoDTO.Item, oReqFilterContratoDTO.Paging);
                            break;
                        case filterCaseContrato.ExportaruspListarMatriculadorAgendaComercial_paginacion:
                            ContratoDTOList = oContratoData.ExportaruspListarMatriculadorAgendaComercial_paginacion(oReqFilterContratoDTO.Item);
                            break;
                        case filterCaseContrato.uspListarMatriculadorAgendaComercial_paginacion:
                            
                            if (!oReqFilterContratoDTO.Paging.All && oReqFilterContratoDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
                            }

                            int outputNumeroRegistros = 0;
                            int outputCantidadNuevos = 0;
                            int outputCantidadRenovaciones = 0;
                            int outputCantidadReinscripciones = 0;

                            decimal outputVentaNuevos = 0;
                            decimal outputVentaRenovaciones = 0;
                            decimal outputVentaReinscripciones = 0;

                            ContratoDTOList = oContratoData.uspListarMatriculadorAgendaComercial_paginacion(oReqFilterContratoDTO.Item,
                                                                                oReqFilterContratoDTO.Paging,
                                                                                ref outputNumeroRegistros,
                                                                                ref outputCantidadNuevos,
                                                                                ref outputCantidadRenovaciones,
                                                                                ref outputCantidadReinscripciones,
                                                                                ref outputVentaNuevos,
                                                                                ref outputVentaRenovaciones,
                                                                                ref outputVentaReinscripciones);
                            oRespListContratoDTO.Paging = new Paging();
                            oRespListContratoDTO.Paging.TotalRecord = Convert.ToUInt32(outputNumeroRegistros);
                            oRespListContratoDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);

                            oRespListContratoDTO.Paging.OutValue_Int1 = outputCantidadNuevos;
                            oRespListContratoDTO.Paging.OutValue_Int2 = outputCantidadRenovaciones;
                            oRespListContratoDTO.Paging.OutValue_Int3 = outputCantidadReinscripciones;

                            oRespListContratoDTO.Paging.OutValue_Deciaml1 = outputVentaNuevos;
                            oRespListContratoDTO.Paging.OutValue_Deciaml2 = outputVentaRenovaciones;
                            oRespListContratoDTO.Paging.OutValue_Deciaml3 = outputVentaReinscripciones;

                            break;
                        case filterCaseContrato.CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo:
                           
                            ContratoDTOList = oContratoData.CentroEntrenamiento_uspListarPlataformaPersonasFit_MembresiasCorreo(oReqFilterContratoDTO.Item);
                            break;
                    
                    }

                    oRespListContratoDTO.List = ContratoDTOList;
                    oRespListContratoDTO.Success = true;

                }
                catch (Exception ex)
                {
                    oRespListContratoDTO.Success = false;
                    oRespListContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }    
            }

            return oRespListContratoDTO;	
           
		}
		
		public RespItemContratoDTO ContratoGetItem(ReqFilterContratoDTO oReqFilterContratoDTO)
		{
			RespItemContratoDTO oRespItemContratoDTO = new RespItemContratoDTO();

            oRespItemContratoDTO.Success = false;
            oRespItemContratoDTO.Item = null;
            oRespItemContratoDTO.User = oReqFilterContratoDTO.User;
            oRespItemContratoDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterContratoDTO.User))
            {
                oRespItemContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Menbresias no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemContratoDTO.MessageList.Count == 0)
            {
                ContratoDTO oContratoDTO = null;
                try
                {
                    switch (oReqFilterContratoDTO.FilterCase)
                    {
                        case filterCaseContrato.porCodigoMembresia:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.VerInformacionMenbresias(oReqFilterContratoDTO.Item);
                                //CARGAR HISTORIAL DE PAGOS
                                PagosContratoData PMData = new PagosContratoData();                                
                                oContratoDTO.ListPagosContrato = PMData.Listar( new PagosContratoDTO() {
                                    CodigoUnidadNegocio = oReqFilterContratoDTO.Item.CodigoUnidadNegocio,
                                    CodigoMembresia = oReqFilterContratoDTO.Item.CodigoMenbresia                                 
                                }).OrderByDescending(x => x.FechaNewPago).ToList();
                                //CARGAR HISTORIAL DE OBSERVACIONES o INSIDENCIAS
                                ContratoMensajeData oContratoMensajeData = new ContratoMensajeData();
                                oContratoDTO.ListContratoMensaje = oContratoMensajeData.ListarMensajesMenbresia( new ContratoMensajeDTO() {
                                    CodigoUnidadNegocio = oReqFilterContratoDTO.Item.CodigoUnidadNegocio,
                                    CodigoMenbresia = oReqFilterContratoDTO.Item.CodigoMenbresia
                                });
                                //CARGAR HISTORIALD DE CUOTAS
                                ContratoCuotaData oContratoCuotaData = new ContratoCuotaData();
                                oContratoDTO.ListContratoCuota = oContratoCuotaData.uspListarClientesMenbresiasCuotas(new ContratoCuotaDTO() {
                                    CodigoUnidadNegocio = oReqFilterContratoDTO.Item.CodigoUnidadNegocio,
                                    CodigoSede = oReqFilterContratoDTO.Item.CodigoSede,
                                    CodigoMembresia = oReqFilterContratoDTO.Item.CodigoMenbresia
                                });
                                //CARGAR HISTORIAL RESERVAS
                                CentroEntrenamiento_Presencial_HorarioClasesAsistenciasData oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasData();
                                oContratoDTO.ListReservas = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasData.CentroEntrenamiento_uspBuscarReservasPresencial_HorarioClasesPorSocio(new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO() {
                                    CodigoUnidadNegocio = oReqFilterContratoDTO.Item.CodigoUnidadNegocio,
                                    CodigoSede = oReqFilterContratoDTO.Item.CodigoSede,
                                    CodigoMembresia = oReqFilterContratoDTO.Item.CodigoMenbresia,
                                    CodigoSocio = oContratoDTO.CodigoSocio
                                });

                            }
                            break;
                        case filterCaseContrato.porCodigo:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.BuscarPorCodigoMenbresias(oReqFilterContratoDTO.Item);
                            }
                            break;

                        case filterCaseContrato.ValidarIngresoDiaPaquete:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.ValidarIngresoDiaPaquete(oReqFilterContratoDTO.Item);
                            }
                            break;

                        case filterCaseContrato.ObtenerTiempoFin:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.ObtenerTiempoVencimiento(oReqFilterContratoDTO.Item);
                            }
                            break;
                        case filterCaseContrato.uspListarMembresiasSociosAcuenta_NumeroRegistro:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.uspListarMembresiasSociosAcuenta_NumeroRegistro(oReqFilterContratoDTO.Item);
                            }
                            break;
                        case filterCaseContrato.uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros(oReqFilterContratoDTO.Item);
                            }
                            break;
                       case filterCaseContrato.uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros(oReqFilterContratoDTO.Item);
                            }
                            break;
                        case filterCaseContrato.uspListarMatriculadorAgendaComercial_NumeroRegistros:
                            {
                                oContratoDTO = new ContratoDTO();
                                oContratoDTO = oContratoData.uspListarMatriculadorAgendaComercial_NumeroRegistros(oReqFilterContratoDTO.Item);
                            }
                            break;
                        default:
                            {
                                oContratoDTO = new ContratoDTO();
                            }
                            break;
                    }

                    oRespItemContratoDTO.Item = new ContratoDTO();
                    oRespItemContratoDTO.Item = oContratoDTO;
                    oRespItemContratoDTO.Success = true;
                    oRespItemContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemContratoDTO.Success = false;
                    oRespItemContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemContratoDTO;
		}
	
		
		public RespContratoDTO ExecuteTransac(ReqContratoDTO oReqContratoDTO)
		{
			RespContratoDTO oRespContratoDTO = new RespContratoDTO();

            oRespContratoDTO.MessageList = new List<Mensaje>();
            oRespContratoDTO.User = oReqContratoDTO.User;
            
            if (String.IsNullOrEmpty(oReqContratoDTO.User))
            {
                oRespContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Menbresias no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespContratoDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int CodigoValidacionOperaciones = 999999999;
                        string MensajeObervacion = "";
                        string MensajeValidacionAcceso = "";
                        int nroIngresos = 0;
                        int CodigoMembresia = 0;
                        int CodigoMembresiaTraspaso = 0;
                        string tipoOperacion = string.Empty;
                        ConfiguracionTraspasoData ctData = new ConfiguracionTraspasoData();
                        ConfiguracionTraspasoDTO ctDto = new ConfiguracionTraspasoDTO();
                        ConfiguracionTraspasoDTO ctNewDto = new ConfiguracionTraspasoDTO();
                        foreach (ContratoDTO item in oReqContratoDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.RegisterContratoApi:
                                    tipoOperacion = "N";
                                    CodigoMembresia = oContratoData.RegistrarContratoApi(item);
                                    break;
                                case Operation.Create:

                                    tipoOperacion = "N";
                                    ctDto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    ctDto.CodigoSede = item.CodigoSede;
                                    ctNewDto = ctData.BuscarPorCodigoConfiguracionTraspaso(ctDto);
                                    int EstadoParaTraspasa;
                                    EstadoParaTraspasa = ctNewDto.Estado;
                                    /*Aqui empiezo con los actualizar los asesores de ventas de detalles de ventas*/
                                    string IndTraspaso;
                                    int CodigoMembresiaActualizar;
                                    IndTraspaso = item.IndTraspaso;
                                    CodigoMembresiaActualizar = item.OrigenMembresiaTraspaso;

                                    if (IndTraspaso == "NM")
                                    {
                                        CodigoMembresia = oContratoData.Registrar(item);
                                    }
                                    else
                                    {
                                        if (EstadoParaTraspasa == 0)
                                        {
                                            CodigoMembresia = oContratoData.Registrar(item);
                                        }
                                    }
                                    if (IndTraspaso == "TM")
                                    {
                                        if (EstadoParaTraspasa == 0)
                                        {
                                            //Listar PagoMembresias por CodigoMembresia
                                            //Foreach a OrigenMembresiaTraspaso a PagoMembresia por CodigoMembresia
                                            PagosContratoData PMData = new PagosContratoData();
                                            PagosContratoDTO itemPM = new PagosContratoDTO();
                                            itemPM.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                            itemPM.CodigoMembresia = CodigoMembresiaActualizar;
                                            List<PagosContratoDTO> ListaPagoMembresia = new List<PagosContratoDTO>();
                                            ListaPagoMembresia = PMData.Listar(itemPM);
                                            foreach (var itemListado in ListaPagoMembresia)
                                            {

                                                VentasDetalleData CDSData = new VentasDetalleData();
                                                VentasDetalleDTO CDSDto = new VentasDetalleDTO();
                                                CDSDto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                CDSDto.CodigoSede = item.CodigoSede;
                                                CDSDto.CodigoProducto = itemListado.CodigoPagoMembresia;
                                                List<VentasDetalleDTO> listaCDS = new List<VentasDetalleDTO>();
                                                listaCDS = CDSData.ListarControlDetalle_PorCodigoPagoMembresia(CDSDto);
                                                foreach (var itemCDS in listaCDS)
                                                {
                                                    CDSDto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                                    CDSDto.CodigoSalidaDetalle = itemCDS.CodigoSalidaDetalle;
                                                    CDSDto.AsesorComercial = item.AsesorComercial;
                                                    CDSData.ActualizarAsesorComercial(CDSDto);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            item.EstadoTraspaso = 1;
                                            CodigoMembresiaTraspaso = oContratoData.RegistrarConfirmarMenbresiasTraspaso(item);

                                        }
                                    }

                                    break;

                                case Operation.Update:

                                    tipoOperacion = "E";
                                    CodigoMembresia = 999999999;
                                    oContratoData.Actualizar(item);

                                    /* Actualizar asesores de ventas de ControlDetalleSalida */
                                    PagosContratoDTO oPMDTO = new PagosContratoDTO();
                                    oPMDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oPMDTO.CodigoMembresia = item.CodigoMenbresia;
                                    PagosContratoData oPMData = new PagosContratoData();
                                    List<PagosContratoDTO> oPMLista = new List<PagosContratoDTO>();
                                    oPMLista = oPMData.Listar(oPMDTO);
                                    foreach (PagosContratoDTO itemPM in oPMLista)
                                    {
                                        VentasDetalleDTO oCDSDto = new VentasDetalleDTO();
                                        oCDSDto.CodigoSalidaDetalle = itemPM.CodigoPagoMembresia;
                                        oCDSDto.AsesorComercial = item.AsesorComercial;
                                        oCDSDto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        VentasDetalleData oCDSData = new VentasDetalleData();
                                        oCDSData.ActualizarAsesorControlDetalleSalida(oCDSDto);
                                    }
                                    
                                    break;
                              
                                case Operation.UpdateEstadoTraspaso:

                                    ConfiguracionTraspasoData ctDataNew = new ConfiguracionTraspasoData();
                                    ConfiguracionTraspasoDTO oCTM = new ConfiguracionTraspasoDTO();
                                    oCTM.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oCTM.CodigoSede = item.CodigoSede;
                                    ConfiguracionTraspasoDTO oCTMnew = new ConfiguracionTraspasoDTO();
                                    oCTMnew = ctDataNew.BuscarPorCodigoConfiguracionTraspaso(oCTM);
                                    int EstadoParaActualizarTraspaso;
                                    EstadoParaActualizarTraspaso = oCTMnew.Estado;

                                    if (EstadoParaActualizarTraspaso == 0)
                                    {
                                        oContratoData.ActualizarEstadosMembresia(item);
                                    }

                                    break;
                                case Operation.UpdateMembresiaNroIngresos:

                                    //UsuariosIngresosData oUsuariosIngresosDataUpdateMembresiaNroIngresos = new UsuariosIngresosData();
                                    //UsuariosIngresosDTO oUsuariosIngresosDTOUpdateMembresiaNroIngresos = new UsuariosIngresosDTO();

                                    //oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoSede = item.CodigoSede;
                                    //oUsuariosIngresosDTOUpdateMembresiaNroIngresos.UsuarioCreacion = item.UsuarioCreacion;
                                    //oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoIngreso = item.TK_ID;
                                    //oUsuariosIngresosDTOUpdateMembresiaNroIngresos.Latitud = item.TK_Latitude;
                                    //oUsuariosIngresosDTOUpdateMembresiaNroIngresos.Longitud = item.TK_Longitude;
                                    //oUsuariosIngresosDTOUpdateMembresiaNroIngresos = oUsuariosIngresosDataUpdateMembresiaNroIngresos.uspValidarAccesoSistema(oUsuariosIngresosDTOUpdateMembresiaNroIngresos);

                                    MensajeValidacionAcceso = "999999999";
                                    MensajeObervacion = oContratoData.ActualizarNroIngreso(item);
                                    nroIngresos = Convert.ToInt32(MensajeObervacion.Split('|')[0]);
                                    //if (oUsuariosIngresosDTOUpdateMembresiaNroIngresos.CodigoValidacion == 3)
                                    //{

                                    //}
                                    //else
                                    //{
                                    //    MensajeValidacionAcceso = "0";
                                    //}
                                    break;
        
                                case Operation.uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo:
                                    oContratoData.uspUpdateEstadoMembresias_Congelacion_Descongelacion_Activo_Inactivo(item);
                                    break;

                                case Operation.UpdateMembresiaFreezing:

                                    tipoOperacion = "Congelar";
                                    CodigoMembresia = 999999999;
                                    //Aqui guardar parte del historial y obtener el codigo de ese
                                    //registro para guardarlo en la tabla de membresias para un control directo 
                                    //con respecto a sus fechas reales (inicialmente)
                                    HistorialCongelamientoDTO oHCDto = new HistorialCongelamientoDTO();
                                    oHCDto.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oHCDto.Codigo = 0;
                                    oHCDto.CodigoMembresia = item.CodigoMenbresia;
                                    oHCDto.CodigoSocio = item.CodigoSocio;
                                    oHCDto.FechaInicio = Convert.ToDateTime(item.FechaCongelacionProgramada);
                                    oHCDto.FechaFin = Convert.ToDateTime(item.FechaDesCongelacion);
                                    oHCDto.NroDias = item.NroDias;
                                    oHCDto.UsuarioCreacion = item.UsuarioCreacion;
                                    oHCDto.FechaCreacion = DateTime.Now;
                                    oHCDto.NroSolicitud = item.NroSolicitud;
                                    oHCDto.Motivo = item.Motivo;
                                    HistorialCongelamientoData oHCData = new HistorialCongelamientoData();
                                    int CodigoHistorialFreezing;
                                    CodigoHistorialFreezing = oHCData.Registrar(oHCDto);
                                    item.UsuarioEdicion = item.UsuarioCreacion;
                                    item.CodigoHistorialMenbresia = CodigoHistorialFreezing;

                                    oContratoData.ActualizarFrezing(item);
                                    //UsuariosIngresosData oUsuariosIngresosDataCongelamiento = new UsuariosIngresosData();
                                    //UsuariosIngresosDTO oUsuariosIngresosDTOCongelamiento = new UsuariosIngresosDTO();

                                    //oUsuariosIngresosDTOCongelamiento.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oUsuariosIngresosDTOCongelamiento.CodigoSede = item.CodigoSede;
                                    //oUsuariosIngresosDTOCongelamiento.UsuarioCreacion = item.UsuarioCreacion;
                                    //oUsuariosIngresosDTOCongelamiento.CodigoIngreso = item.TK_ID;
                                    //oUsuariosIngresosDTOCongelamiento.Latitud = item.TK_Latitude;
                                    //oUsuariosIngresosDTOCongelamiento.Longitud = item.TK_Longitude;

                                    //item.CodigoInicioSesion = item.TK_ID;

                                    //oUsuariosIngresosDTOCongelamiento = oUsuariosIngresosDataCongelamiento.uspValidarAccesoSistema(oUsuariosIngresosDTOCongelamiento);
                                    //if (oUsuariosIngresosDTOCongelamiento.CodigoValidacion == 3)
                                    //{

                                    //    UsuariosIngresosData oUsuariosIngresosDataValidacion = new UsuariosIngresosData();
                                    //    UsuariosIngresosDTO oUsuariosIngresosDTOValidacion = new UsuariosIngresosDTO();
                                    //    oUsuariosIngresosDTOValidacion.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //    oUsuariosIngresosDTOValidacion.CodigoSede = item.CodigoSede;
                                    //    oUsuariosIngresosDTOValidacion.UsuarioCreacion = item.UsuarioCreacion;
                                    //    oUsuariosIngresosDTOValidacion.CodigoInicioSesion = item.CodigoInicioSesion;
                                    //    oUsuariosIngresosDTOValidacion.Operacion = "I";
                                    //    oUsuariosIngresosDTOValidacion.DescripcionTabla = "HistorialCongelamiento";

                                    //    CodigoValidacionOperaciones = oUsuariosIngresosDataValidacion.uspObtenerValidacionOperaciones(oUsuariosIngresosDTOValidacion);

                                    //    if (CodigoValidacionOperaciones == 0)
                                    //    {



                                    //    }


                                    //}
                                    //else
                                    //{
                                    //    CodigoMembresia = 0;
                                    //}

                                    break;
                                case Operation.Delete:

                                        MensajeValidacionAcceso = "999999999";
                                        //Eliminar Pagos
                                        List<PagosContratoDTO> oValidatorPagoMembresia = new List<PagosContratoDTO>();
                                        List<PagosContratoDTO> oValidatorPagoMembresia2 = new List<PagosContratoDTO>();
                                        List<ContratoCuotaDTO> oValidatorPagoCuota = new List<ContratoCuotaDTO>();

                                        PagosContratoData oPagosContratoData = new PagosContratoData();
                                        PagosContratoDTO oPagoMembresiaDTO = new PagosContratoDTO();
                                        oPagoMembresiaDTO.CodigoMembresia = item.CodigoMenbresia;
                                        oPagoMembresiaDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        oPagoMembresiaDTO.CodigoPagoMembresia = 0;

                                        oValidatorPagoMembresia = oPagosContratoData.Listar(oPagoMembresiaDTO);

                                        if (oValidatorPagoMembresia.Count > 0)
                                        {
                                            oValidatorPagoMembresia2 = oValidatorPagoMembresia.Where(x => x.Estado == 1).ToList();
                                        }

                                        //Eliminar Cuotas

                                        //ContratoCuotaData oContratoCuotaData = new ContratoCuotaData();
                                        //ContratoCuotaDTO oContratoCuotaDTO = new ContratoCuotaDTO();
                                        //oContratoCuotaDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                        //oContratoCuotaDTO.CodigoSede = item.CodigoSede;
                                        //oContratoCuotaDTO.CodigoMembresia = item.CodigoMenbresia;
                                        //oContratoCuotaDTO.CodigoCuota = 0;
                                        //oValidatorPagoCuota = oContratoCuotaData.Listar(oContratoCuotaDTO);
                                        //oValidatorPagoCuota.Count == 0 && 
                                        if (oValidatorPagoMembresia2.Count == 0)
                                        {
                                            oContratoData.Eliminar(item);
                                        }
                                        else
                                        {
                                            tx.Dispose();
                                            oRespContratoDTO.Success = false;
                                            oRespContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                            {
                                                Codigo = 100,
                                                // Detalle = "No se puede eliminar la membresia",
                                                Detalle = MensajeObervacion + "/" + MensajeValidacionAcceso,
                                                Tipo = TipoMensaje.Error
                                            });
                                        }

                                    break;
                                case Operation.uspActualizarMenbresiasFechaInicio:
                                    CodigoMembresia = 999999999;
                                    nroIngresos = 999999999;
                                    MensajeValidacionAcceso = "999999999";
                                    oContratoData.uspActualizarMenbresiasFechaInicio(item);
                                    break;
                            }
                        }

                        if (oRespContratoDTO.MessageList.Count == 0)
                        {
                            tx.Complete();
                            oRespContratoDTO.Success = true;

                            if (tipoOperacion == string.Empty)
                            {
                                oRespContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                {
                                    Codigo = nroIngresos,
                                    Detalle = MensajeObervacion + "/" + MensajeValidacionAcceso, //.Split('|')[1],
                                    Tipo = TipoMensaje.Informacion
                                });
                            }
                            else
                            {
                                oRespContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                {
                                    Codigo = CodigoMembresia,
                                    Detalle = "Proceso Grabado Correctamente.",
                                    Tipo = TipoMensaje.Informacion
                                });  
                            }
                               
                        }
                       
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespContratoDTO.Success = false;
                        oRespContratoDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }

                }

            }

            return oRespContratoDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
