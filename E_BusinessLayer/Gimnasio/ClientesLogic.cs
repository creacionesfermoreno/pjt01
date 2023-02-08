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
	
	public class ClientesLogic: IDisposable
	{
		ClientesData oSociosData = null;
		public ClientesLogic()
		{
			oSociosData = new ClientesData();
		}
				
        public RespListClientesDTO ClientesGetList(ReqFilterClientesDTO oReqFilterClientesDTO)
        {
            RespListClientesDTO oRespListClientesDTO = new RespListClientesDTO();

            oRespListClientesDTO.List = new List<ClientesDTO>();
            oRespListClientesDTO.User = oReqFilterClientesDTO.User;
            oRespListClientesDTO.MessageList = new List<Mensaje>();
            oRespListClientesDTO.Paging = new Paging();

            if (String.IsNullOrEmpty(oReqFilterClientesDTO.User))
            {
                oRespListClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Socios no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oReqFilterClientesDTO.Paging == null)
            {
                oRespListClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "Error en el parámetro de Paginación.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespListClientesDTO.MessageList.Count == 0)
            {

                try
                {
                    uint recordCount = 0;
                   
                    List<ClientesDTO> ClientesDTOList = new List<ClientesDTO>();
                    switch (oReqFilterClientesDTO.FilterCase)
                    {
                        case filterCaseClientes.uspListarSocios_PorVendedor_Paginacion:
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarSocios_PorVendedor_Paginacion_NumeroRegistros"]);
                            }
                            ClientesDTOList = oSociosData.uspListarSocios_PorVendedor_Paginacion(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging);
                            break;
                      
                        case filterCaseClientes.ListarSociosLibresAsesores_Paginacion:
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_ListarSociosLibresAsesores_NumeroRegistros"]);
                            }
                            ClientesDTOList = oSociosData.ListarSociosLibresAsesores_Paginacion(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging);
                            break;
                        case filterCaseClientes.uspListarProspectosPostVenta_Paginacion:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            ClientesDTOList = oSociosData.uspListarProspectosPostVenta_Paginacion(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspListarClientesActivos:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesActivos(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspListarClientesActivos_ExportarExcel:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesActivos_ExportarExcel(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspListarClientesInactivos:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesInactivos(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspListarClientesInactivosSinCita:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesInactivosSinCita(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspListarClientesPorVencer:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesPorVencer(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspListarClientesPorTodos:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_Todos"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesPorTodos(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspVerMasClientesComprometidosPagosCuotas_Paginacion:
                            
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros"]);
                            }
                            ClientesDTOList = oSociosData.uspVerMasClientesComprometidosPagosCuotas_Paginacion(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;

                       case filterCaseClientes.uspNotificacionCumpleaniosSocios_Paginacion:
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarGridCumpleanios_NumeroRegistros"]);
                            }

                            ClientesDTOList = oSociosData.uspNotificacionCumpleaniosSocios_Paginacion(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging);
                            break;
                            
                        case filterCaseClientes.listaTodosClientesPorTipoAgenda:
                            ClientesDTOList = oSociosData.listaTodosClientesPorTipoAgenda(oReqFilterClientesDTO.Item);
                         break;

                        case filterCaseClientes.uspListarClientesHombres_MujeresEstadistica:
                            ClientesDTOList = oSociosData.uspListarClientesHombres_MujeresEstadistica(oReqFilterClientesDTO.Item);
                         break;

                        case filterCaseClientes.uspListarTotalDia_TardeEstadistica:
                            ClientesDTOList = oSociosData.uspListarTotalDia_TardeEstadistica(oReqFilterClientesDTO.Item);
                         break;
                        case filterCaseClientes.uspListarClientesAsistenciaEfectiva_Estadistica:
                            ClientesDTOList = oSociosData.uspListarClientesAsistenciaEfectiva_Estadistica(oReqFilterClientesDTO.Item);
                         break;

                       case filterCaseClientes.uspListarEstadisticaTipoContrato:
                            ClientesDTOList = oSociosData.uspListarEstadisticaTipoContrato(oReqFilterClientesDTO.Item);
                         break;

                       case filterCaseClientes.uspListarEstadisticaTiempoMenbresia:
                            ClientesDTOList = oSociosData.uspListarEstadisticaTiempoMenbresia(oReqFilterClientesDTO.Item);
                         break;
                        case filterCaseClientes.uspListarEstadistica_AsistenciaporRangoEdades:
                            ClientesDTOList = oSociosData.uspListarEstadistica_AsistenciaporRangoEdades(oReqFilterClientesDTO.Item);
                         break;
                        case filterCaseClientes.uspListarEstadistica_AsistenciaporHorarios:
                            ClientesDTOList = oSociosData.uspListarEstadistica_AsistenciaporHorarios(oReqFilterClientesDTO.Item);
                         break;

                        case filterCaseClientes.uspListarEstadistica_AsistenciaporSemana:
                            ClientesDTOList = oSociosData.uspListarEstadistica_AsistenciaporSemana(oReqFilterClientesDTO.Item);
                         break;
                        case filterCaseClientes.uspListarClientesAgendaComercialReinscripcion:
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarClientesAgendaComercialReinscripcion"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesAgendaComercialReinscripcion(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;


                        case filterCaseClientes.uspListarClientesAgendaComercialRenovacion:
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarClientesAgendaComercialRenovacion"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesAgendaComercialRenovacion(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;

                        case filterCaseClientes.uspListarClientesAgendaComercialRenovacionInscritos:
                            if (!oReqFilterClientesDTO.Paging.All && oReqFilterClientesDTO.Paging.PageRecords == 0)
                            {
                                oReqFilterClientesDTO.Paging.PageRecords = Convert.ToUInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarClientesAgendaComercialRenovacionInscritos"]);
                            }
                            ClientesDTOList = oSociosData.uspListarClientesAgendaComercialRenovacionInscritos(oReqFilterClientesDTO.Item, oReqFilterClientesDTO.Paging, ref recordCount);
                            break;
                        case filterCaseClientes.uspEstadisticaDashboar_ListadoporvencerExel:
                        
                            ClientesDTOList = oSociosData.uspEstadisticaDashboar_ListadoporvencerExel(oReqFilterClientesDTO.Item);
                            break;
                        case filterCaseClientes.uspEstadisticaDashboar_ListadoclientesrenovaronExel:
                            ClientesDTOList = oSociosData.uspEstadisticaDashboar_ListadoclientesrenovaronExel(oReqFilterClientesDTO.Item);
                            break;
                        case filterCaseClientes.uspEstadisticaDashboar_ListadoclientesreinscribieronExel:
                            ClientesDTOList = oSociosData.uspEstadisticaDashboar_ListadoclientesreinscribieronExel(oReqFilterClientesDTO.Item);
                            break;
                        case filterCaseClientes.uspEstadisticaDashboar_ListadoclientesnuevosExel:
                            ClientesDTOList = oSociosData.uspEstadisticaDashboar_ListadoclientesnuevosExel(oReqFilterClientesDTO.Item);
                            break;
                        case filterCaseClientes.uspTotalPagosVentasRangoFechas_Appsfit:
                            ClientesDTOList = oSociosData.uspTotalPagosVentasRangoFechas_Appsfit(oReqFilterClientesDTO.Item);
                            break;
                        case filterCaseClientes.uspTotalVentasTurnos_RangoFechas_Appsfit:
                            ClientesDTOList = oSociosData.uspTotalVentasTurnos_RangoFechas_Appsfit(oReqFilterClientesDTO.Item);
                            break;
                        case filterCaseClientes.uspListarVentasTotal:
                            ClientesDTOList = oSociosData.uspListarVentasTotal(oReqFilterClientesDTO.Item);
                            break; 
                        case filterCaseClientes.uspCentroEntrenamiento_uspConsumoTotalPorCliente:
                            ClientesDTOList = oSociosData.CentroEntrenamiento_uspConsumoTotalPorCliente(oReqFilterClientesDTO.Item);
                            break; 
                        case filterCaseClientes.uspCentroEntrenamiento_uspConsumoDetalladoPorCliente:
                            ClientesDTOList = oSociosData.CentroEntrenamiento_uspConsumoDetalladoPorCliente(oReqFilterClientesDTO.Item);
                            break;
                        default:
                                ClientesDTOList = oSociosData.Listar(oReqFilterClientesDTO.Item);
                            break;
                    }
                 

                    oRespListClientesDTO.List = ClientesDTOList;
                    oRespListClientesDTO.Success = true;
                    
                }
                catch (Exception ex)
                {
                    oRespListClientesDTO.Success = false;
                    oRespListClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }
            }

            return oRespListClientesDTO;

        }
        
		public RespItemClientesDTO SociosGetItem(ReqFilterClientesDTO oReqFilterClientesDTO)
		{
			RespItemClientesDTO oRespItemClientesDTO = new RespItemClientesDTO();

            oRespItemClientesDTO.Success = false;
            oRespItemClientesDTO.Item = null;
            oRespItemClientesDTO.User = oReqFilterClientesDTO.User;
            oRespItemClientesDTO.MessageList = new List<Mensaje>();
			
		   if (String.IsNullOrEmpty(oReqFilterClientesDTO.User))
            {
                oRespItemClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Socios no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespItemClientesDTO.MessageList.Count == 0)
            {
                ClientesDTO oClientesDTO = null;
                try
                {
                    switch (oReqFilterClientesDTO.FilterCase)
                    {
                        case filterCaseClientes.TotalPagosVentas:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.TotalPagosVentas(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspTotalPagosSuplementosRangoFechas:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspTotalPagosSuplementosRangoFechas(oReqFilterClientesDTO.Item);
                            }
                            break;

                         case filterCaseClientes.uspTotalPagosRopasRangoFechas:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspTotalPagosRopasRangoFechas(oReqFilterClientesDTO.Item);
                            }
                            break;

                          case filterCaseClientes.TotalPagosVentasCafeteria:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.TotalPagosVentasCafeteria(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.BuscarInfoPorCodSocio:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.BuscarInformacionSociosPorCodigo(oReqFilterClientesDTO.Item);
                            }
                            break;

                        case filterCaseClientes.BuscarInfoPorCodSocioFiltro:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.BuscarInfoPorCodSocioFiltro(oReqFilterClientesDTO.Item);
                            }
                            break;
                      
                        case filterCaseClientes.uspBuscarSociosConFiltro_Contrato:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspBuscarSociosConFiltro_Contrato(oReqFilterClientesDTO.Item);
                            }
                            break;

                        case filterCaseClientes.uspBuscarSociosConFiltrosTransferenciaContrato:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspBuscarSociosConFiltrosTransferenciaContrato(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.porCodigo:
                            {
                                
                            }
                            break;
                    
                        case filterCaseClientes.GetCantidadSociosPorVendedor:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.GetCantidadSociosPorVendedor(oReqFilterClientesDTO.Item);
                            }
                            break;
                     
                        case filterCaseClientes.uspListarClientesActivos_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesActivos_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;

                        case filterCaseClientes.uspListarSociosLibresAsesores_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarSociosLibresAsesores_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;

                        case filterCaseClientes.uspListarSocios_PorVendedor_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarSocios_PorVendedor_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;


                        case filterCaseClientes.uspNotificacionCumpleaniosSocios_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspNotificacionCumpleaniosSocios_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspListarProspectosPostVenta_Paginacion:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarProspectosPostVenta_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspListarClientesInactivos_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesInactivos_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspListarClientesInactivosSinCita_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesInactivosSinCita_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspListarClientesPorVencer_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesPorVencer_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspListarClientesPorTodos_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesPorTodos_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspListarCantidadEstadosClientes:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarCantidadEstadosClientes(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspEstadisticaDashboar:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspEstadisticaDashboar(oReqFilterClientesDTO.Item);
                            }
                            break;
                     
                        case filterCaseClientes.uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspVerMasClientesComprometidosPagosCuotas_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;
                        case filterCaseClientes.uspListarClientesAgendaComercialReinscripcion_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesAgendaComercialReinscripcion_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;

                          case filterCaseClientes.uspListarClientesAgendaComercialRenovacion_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesAgendaComercialRenovacion_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;

                        case filterCaseClientes.uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros:
                            {
                                oClientesDTO = new ClientesDTO();
                                oClientesDTO = oSociosData.uspListarClientesAgendaComercialRenovacionInscritos_NumeroRegistros(oReqFilterClientesDTO.Item);
                            }
                            break;


                        case filterCaseClientes.BuscarCodigoDelPrimerSocio:
                            oClientesDTO = new ClientesDTO();
                            oClientesDTO = oSociosData.uspBuscarSocioConMembresiaActivaPrimerRegistro(oReqFilterClientesDTO.Item);
                            break;
                        case filterCaseClientes.CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo:
                            oClientesDTO = new ClientesDTO();
                            oClientesDTO = oSociosData.CentroEntrenamiento_uspBuscarPlataformaPersonasFit_InformacionSocioPorCorreo(oReqFilterClientesDTO.Item);
                            break;

                        default:
                            {
                                oClientesDTO = new ClientesDTO();
                            }
                            break;
                    }

                    oRespItemClientesDTO.Item = new ClientesDTO();
                    oRespItemClientesDTO.Item = oClientesDTO;
                    oRespItemClientesDTO.Success = true;
                    oRespItemClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = "Correcto.",
                        Tipo = TipoMensaje.Informacion
                    });

                }
                catch (Exception ex)
                {
                    oRespItemClientesDTO.Success = false;
                    oRespItemClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                    {
                        Codigo = 100,
                        Detalle = ex.Message,
                        Tipo = TipoMensaje.Error
                    });
                }

            }
            return oRespItemClientesDTO;
		}

        public int ValidarDni(int CodigoUnidadNegocio,string dni)
        {
            int flag = 0;
            flag = oSociosData.ValidarDNIProspecto(CodigoUnidadNegocio,dni);
            return flag;
        }


        public RespClientesDTO ExecuteTransac(ReqClientesDTO oReqClientesDTO)
		{
			RespClientesDTO oRespClientesDTO = new RespClientesDTO();
           
            oRespClientesDTO.MessageList = new List<Mensaje>();
            oRespClientesDTO.User = oReqClientesDTO.User;
            
            if (String.IsNullOrEmpty(oReqClientesDTO.User))
            {
                oRespClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                {
                    Codigo = 100,
                    Detalle = "La cuenta de Socios no es válida.",
                    Tipo = TipoMensaje.Error
                });
            }

            if (oRespClientesDTO.MessageList.Count == 0)
            {
                
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    try
                    {
                        int codigoSocio = 0;
                      
                        foreach (ClientesDTO item in oReqClientesDTO.List)
                        {
                            switch (item.Operation)
                            {
                                case Operation.ActualizarAsesorComercial_Cliente:

                                    codigoSocio = 999999999;
                                    oSociosData.ActualizarAsesorComercial_Cliente(item);

                                    break;
                                case Operation.Create:

                                    codigoSocio = oSociosData.Registrar(item);

                                    break;
                                case Operation.uspRegistrarSocios_ImportarExcel:

                                    codigoSocio = oSociosData.uspRegistrarSocios_ImportarExcel(item);

                                    //AHORA REGISTRAR MEMBRESIA
                                    ContratoData oContratoData = new ContratoData();
                                    int CodigoMembresia = 0;
                                    ContratoDTO oContratoDTO = new ContratoDTO();
                                    oContratoDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oContratoDTO.CodigoSede = item.CodigoSede;
                                    oContratoDTO.CodigoSocio = codigoSocio;
                                    oContratoDTO.CodigoResponsable = 1;
                                    oContratoDTO.CodigoPaquete = 1;
                                    oContratoDTO.FechaInicio = item.FechaInicio;
                                    oContratoDTO.FechaFin = item.FechaFinal;
                                    oContratoDTO.Costo = item.Costo;
                                    oContratoDTO.NroIngreso = item.NroIngreso;
                                    oContratoDTO.NroIngresoActual = item.NroIngresoActual;
                                    oContratoDTO.NroContrato = item.NroContrato;
                                    oContratoDTO.FrezenDisponibles = 0;
                                    oContratoDTO.MatriculadoPor = 1;
                                    oContratoDTO.Estado = 1;
                                    oContratoDTO.UsuarioCreacion = "appsfit";
                                    oContratoDTO.TipoMembresia = 4;
                                    oContratoDTO.CodigoMebresiaOrigen = 1;
                                    oContratoDTO.ObservacionTraspaso = string.Empty;
                                    oContratoDTO.TipoDescuento = 0;
                                    oContratoDTO.MontoDescuento = 0;
                                    oContratoDTO.Observacion = item.NombreMembresia;
                                    oContratoDTO.AsesorComercial = "appsfit";
                                    oContratoDTO.TipoIngreso = 1;
                                    oContratoDTO.IndTraspaso = string.Empty;
                                    oContratoDTO.TipoContrato = 1;
                                    oContratoDTO.OrigenSociosTraspaso = 0;
                                    oContratoDTO.OrigenMembresiaTraspaso = 0;
                                    oContratoDTO.CodigoProfesor = 0;
                                    oContratoDTO.CodTiempoMenbresia = 1;
                                    oContratoDTO.UsuarioEmisor = "";
                                    oContratoDTO.CodigoInicioSesion = 0;
                                    CodigoMembresia = oContratoData.Registrar(oContratoDTO);

                                    ////AHORA REGISTRAR SU PAGO
                                    //VentasData oVentasData = new VentasData();
                                    //VentasDTO oVentasDTO = new VentasDTO();
                                    //VentasDetalleData oVentasDetalleData = new VentasDetalleData();
                                    //ComprasDetalleData oComprasDetalleData = new ComprasDetalleData();
                                    //PagosContratoData oPagosContratoData = new PagosContratoData();
                                    //VentasDetalleDTO itemDetalle = new VentasDetalleDTO();
                                    //int codigoVenta = 0;
                                    //membresia

                                    //oVentasDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oVentasDTO.CodigoSede = item.CodigoSede;
                                    //oVentasDTO.CodigoSocio = codigoSocio;
                                    //oVentasDTO.RazonSocial_Sr = item.Nombre + " " + item.Apellidos;
                                    //oVentasDTO.RUC_DNI = item.DNI;
                                    //oVentasDTO.Direccion = string.Empty;
                                    //oVentasDTO.FechaVenta = item.FechaInicio;
                                    //oVentasDTO.CodigoTipoComprobante = 1;
                                    //oVentasDTO.CodigoSubTipoDocumento = 1;
                                    //oVentasDTO.NroComprobante = item.NroComprobante;
                                    //oVentasDTO.NroTarjeta = string.Empty;
                                    //oVentasDTO.SubTotal = item.Pago;
                                    //oVentasDTO.IGV = 0;
                                    //oVentasDTO.TotalNeto = item.Pago;
                                    //oVentasDTO.Estado = true;
                                    //oVentasDTO.Comentario = item.NombreMembresia;
                                    //oVentasDTO.TipoMoneda = 1;
                                    //oVentasDTO.FormaPago = item.FormaPago;
                                    //oVentasDTO.tipoCambio = 0;
                                    //oVentasDTO.UsuarioCreacion = "appsfit";
                                    //oVentasDTO.TotalDolares = 0;
                                    //oVentasDTO.CodigoInicioSesion = 0;

                                    //codigoVenta = oVentasData.Registrar(oVentasDTO);

                                    //ControlSalidaFormaPagoData oControlSalidaFormaPagoData = new ControlSalidaFormaPagoData();
                                    //ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTOMenbre = new ControlSalidaFormaPagoDTO();
                                    //oControlSalidaFormaPagoDTOMenbre.Codigo = 0;
                                    //oControlSalidaFormaPagoDTOMenbre.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oControlSalidaFormaPagoDTOMenbre.CodigoSede = item.CodigoSede;
                                    //oControlSalidaFormaPagoDTOMenbre.CodigoIngreso = codigoVenta;
                                    //oControlSalidaFormaPagoDTOMenbre.TipoMoneda = 1;
                                    //oControlSalidaFormaPagoDTOMenbre.Monto = item.Pago;
                                    //oControlSalidaFormaPagoDTOMenbre.TipoCambio = 0;
                                    //oControlSalidaFormaPagoDTOMenbre.FormaPago = 1;
                                    //oControlSalidaFormaPagoDTOMenbre.SubFormaPago = 1;
                                    //oControlSalidaFormaPagoDTOMenbre.NroBoucher = string.Empty;
                                    //oControlSalidaFormaPagoDTOMenbre.UrlBoucher = string.Empty;
                                    //oControlSalidaFormaPagoDTOMenbre.UsuarioCreacion = "appsfit";
                                    //oControlSalidaFormaPagoDTOMenbre.CodigoInicioSesion = 1;
                                    //oControlSalidaFormaPagoData.Registrar(oControlSalidaFormaPagoDTOMenbre);


                                    ////agregar el pago de la membresia
                                    //PagosContratoDTO oPagoMembresiaDTO = new PagosContratoDTO();
                                    //ControlSalidaFormaPagoDTO oControlSalidaFormaPagoDTO = new ControlSalidaFormaPagoDTO();
                                    //oPagoMembresiaDTO.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //oPagoMembresiaDTO.CodigoSede = item.CodigoSede;
                                    //oPagoMembresiaDTO.CodigoMembresia = CodigoMembresia;
                                    //oPagoMembresiaDTO.Monto = item.Pago;

                                    //oPagoMembresiaDTO.NroComprobante = "bol-";
                                    //oPagoMembresiaDTO.FechaPago = item.FechaInicio;
                                    //oPagoMembresiaDTO.FormaPago = 1;
                                    //oPagoMembresiaDTO.nroTarjeta = string.Empty;
                                    //oPagoMembresiaDTO.UsuarioCreacion = "appsfit";
                                    //oPagoMembresiaDTO.CodigoInicioSesion = 0;

                                    //int codigoPagoMem = oPagosContratoData.Registrar(oPagoMembresiaDTO);

                                    ////registrar detalle
                                    //itemDetalle.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    //itemDetalle.CodigoSede = item.CodigoSede;
                                    //itemDetalle.CodigoSalida = codigoVenta;
                                    //itemDetalle.CodigoSalidaDetalle = 0;
                                    //itemDetalle.Cantidad = 1;
                                    //itemDetalle.PrecioUnitario = item.Pago;
                                    //itemDetalle.Descripcion = item.NombreMembresia;
                                    //itemDetalle.Importe = item.Pago;
                                    //itemDetalle.Tipo = 2;
                                    //itemDetalle.CodigoProducto = codigoPagoMem;
                                    //itemDetalle.UsuarioCreacion = "appsfit";
                                    //itemDetalle.AsesorComercial = "appsfit";
                                    //itemDetalle.FechaCreacion = item.FechaInicio;
                                    //itemDetalle.CodigoInicioSesion = 0;

                                    //oVentasDetalleData.Registrar(itemDetalle);


                                    break;
                                case Operation.Update:
                                  
                                    break;
                              
                                case Operation.RegistrarMigracion:
                                    codigoSocio = 999999999;
                                    oSociosData.RegistrarMigracionSocios(item);
                                    
                                    break;

                                case Operation.uspEnviarSocioANuevo:
                                    UsuariosIngresosData oUsuariosIngresosDatauspEnviarSocioANuevo = new UsuariosIngresosData();
                                    UsuariosIngresosDTO oUsuariosIngresosDTOuspEnviarSocioANuevo = new UsuariosIngresosDTO();

                                    oUsuariosIngresosDTOuspEnviarSocioANuevo.CodigoUnidadNegocio = item.CodigoUnidadNegocio;
                                    oUsuariosIngresosDTOuspEnviarSocioANuevo.CodigoSede = item.CodigoSede;
                                    oUsuariosIngresosDTOuspEnviarSocioANuevo.UsuarioCreacion = item.UsuarioCreacion;
                                    oUsuariosIngresosDTOuspEnviarSocioANuevo.CodigoIngreso = item.TK_ID;
                                    oUsuariosIngresosDTOuspEnviarSocioANuevo.Latitud = item.TK_Latitude;
                                    oUsuariosIngresosDTOuspEnviarSocioANuevo.Longitud = item.TK_Longitude;

                                    item.CodigoInicioSesion = item.TK_ID;

                                    oUsuariosIngresosDTOuspEnviarSocioANuevo = oUsuariosIngresosDatauspEnviarSocioANuevo.uspValidarAccesoSistema(oUsuariosIngresosDTOuspEnviarSocioANuevo);
                                    if (oUsuariosIngresosDTOuspEnviarSocioANuevo.CodigoValidacion == 3)
                                    {
                                        codigoSocio = 999999999;
                                        oSociosData.uspEnviarSocioANuevo(item);
                                    }
                                    else
                                    {
                                        codigoSocio = 0;
                                    }

                                    break;

                                case Operation.UpdateFoto:
                                    oSociosData.ActualizarFoto(item);
                                    break;
                                case Operation.UpdateFotoCarnetVacunacion:
                                    oSociosData.ActualizarFotoCarnetVacunacion(item);
                                    break;

                                case Operation.Delete:

                                    codigoSocio = 999999999;
                                    int cantidad = oSociosData.Eliminar(item);

                                    if (cantidad > 0)
                                    {
                                        tx.Dispose();
                                        oRespClientesDTO.Success = false;
                                        oRespClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                                        {
                                            Codigo = 16,
                                            Detalle = "No ha podido eliminar porque el cliente tiene membresias.",
                                            Tipo = TipoMensaje.Informacion
                                        });
                                    }

                                    break;


                            }
                        }

                        if (oRespClientesDTO.MessageList.Count == 0)
                        {
                            tx.Complete();
                            oRespClientesDTO.Success = true;
                            oRespClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                            {
                                Codigo = codigoSocio,
                                Detalle = "Proceso Grabado Correctamente.",
                                Tipo = TipoMensaje.Informacion
                            });
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        tx.Dispose();
                        oRespClientesDTO.Success = false;
                        oRespClientesDTO.MessageList.Add(new E_DataModel.Common.Mensaje()
                        {
                            Codigo = 100,
                            Detalle = ex.Message,
                            Tipo = TipoMensaje.Error
                        });
                    }
                }

            }

            return oRespClientesDTO;
		}
		
		public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

	}
}
