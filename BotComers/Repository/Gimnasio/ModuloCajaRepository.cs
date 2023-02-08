using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI.WebControls;


namespace BotComers.Repository.Gimnasio
{
    public class ModuloCajaRepository : IDisposable
    {
        public List<UsuarioDTO> ListarCounters(int CodigoUnidadNegocio, int CodSede)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.Where(x => x.CodigoPerfil == 4).ToList();
            }

            return lista;
        }

        public List<UsuarioDTO> ListarAsesoresComerciales(int CodigoUnidadNegocio, int CodSede)
        {
            List<UsuarioDTO> lista = null;
            UsuarioDTO oUsuarioDTO = new UsuarioDTO();
            oUsuarioDTO.CodigoSede = CodSede;
            oUsuarioDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            ReqFilterUsuarioDTO oReq = new ReqFilterUsuarioDTO()
            {
                User = "ADMIN",
                Item = oUsuarioDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListUsuarioDTO oResp = null;

            using (UsuarioLogic oUsuarioLogic = new UsuarioLogic())
            {
                oResp = oUsuarioLogic.UsuarioGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.Where(x => x.CodigoPerfil == 5 || x.CodigoPerfil == 7).ToList();
            }

            return lista;
        }

        public List<TurnosEmpresaDTO> ListarTurnos(int CodigoUnidadNegocio, int CodSede)
        {
            List<TurnosEmpresaDTO> lista = null;
            TurnosEmpresaDTO oTurnosEmpresaDTO = new TurnosEmpresaDTO();
            oTurnosEmpresaDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oTurnosEmpresaDTO.CodigoSede = CodSede;
            ReqFilterTurnosEmpresaDTO oReq = new ReqFilterTurnosEmpresaDTO()
            {
                User = "ADMIN",
                Item = oTurnosEmpresaDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListTurnosEmpresaDTO oResp = null;

            using (TurnosEmpresaLogic oTurnosEmpresaLogic = new TurnosEmpresaLogic())
            {
                oResp = oTurnosEmpresaLogic.TurnosEmpresaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.ToList();
            }

            return lista;
        }

        public List<TipoIngresoDTO> ListarTipoIngreso(int CodigoSede)
        {
            List<TipoIngresoDTO> lista = null;
            TipoIngresoDTO oTipoIngresoDTO = new TipoIngresoDTO();
            oTipoIngresoDTO.CodigoSede = CodigoSede;
            ReqFilterTipoIngresoDTO oReq = new ReqFilterTipoIngresoDTO()
            {
                User = "Admin",
                Item = oTipoIngresoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListTipoIngresoDTO oResp = null;

            using (TipoIngresoLogic oTipoIngresoLogic = new TipoIngresoLogic())
            {
                oResp = oTipoIngresoLogic.TipoIngresoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }

            return lista;

        }

        public List<TiempoMembresiaDTO> ListaTiempoMembresia(string nombre)
        {
            nombre = nombre == "undefined" ? string.Empty : nombre;

            List<TiempoMembresiaDTO> lista = null;

            ReqFilterTiempoMembresiaDTO oReq = new ReqFilterTiempoMembresiaDTO()
            {
                Item = new TiempoMembresiaDTO() { Descripcion = nombre },
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageRecords = 0
                }
            };
            RespListTiempoMembresiaDTO oResp = null;

            using (TiempoMembresiaLogic oTiempoMembresiaLogic = new TiempoMembresiaLogic())
            {
                oResp = oTiempoMembresiaLogic.TiempoMembresiaGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;

            }
            return lista;
        }

        public List<ClientesDTO> uspTotalPagosVentasRangoFechas_Appsfit(int CodigoUnidadNegocio, int CodSede, string Fecha, string FechaFin, string Vendedor, int Turno,
                                                 int CodTiempoPaquete, string AsesorComercial, string TipoIngreso, int Tipo)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Fecha = new DateTime(Convert.ToInt32(Fecha.Split('/')[2]), Convert.ToInt32(Fecha.Split('/')[0]), Convert.ToInt32(Fecha.Split('/')[1]), 0, 0, 0, 0, 0);
            oClientesDTO.FechaFinStr = new DateTime(Convert.ToInt32(FechaFin.Split('/')[2]), Convert.ToInt32(FechaFin.Split('/')[0]), Convert.ToInt32(FechaFin.Split('/')[1]), 23, 0, 0, 0, 0);
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.Turno = Turno;
            oClientesDTO.CodTiempoPaquete = CodTiempoPaquete;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.TipoIngreso = TipoIngreso;
            oClientesDTO.Tipo = Tipo;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspTotalPagosVentasRangoFechas_Appsfit,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }

            return lista;

        }

        public List<ClientesDTO> CentroEntrenamiento_uspConsumoTotalPorCliente(int CodigoUnidadNegocio, int CodSede, int CodigoSocio, string DNI)
        {           
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;          
            oClientesDTO.CodigoSocio = CodigoSocio;
            oClientesDTO.DNI = DNI;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspCentroEntrenamiento_uspConsumoTotalPorCliente,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }

            return lista;

        }

        public List<ClientesDTO> CentroEntrenamiento_uspConsumoDetalladoPorCliente(int CodigoUnidadNegocio, int CodSede, int CodigoSocio, string DNI)
        {
            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.CodigoSocio = CodigoSocio;
            oClientesDTO.DNI = DNI;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspCentroEntrenamiento_uspConsumoDetalladoPorCliente,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }

            return lista;

        }


        public List<ClientesDTO> uspTotalVentasTurnos_RangoFechas_Appsfit(int CodigoUnidadNegocio, int CodSede, string Fecha, string FechaFin, string Vendedor,
                                             int CodTiempoPaquete, string AsesorComercial, string TipoIngreso)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Fecha = new DateTime(Convert.ToInt32(Fecha.Split('/')[2]), Convert.ToInt32(Fecha.Split('/')[0]), Convert.ToInt32(Fecha.Split('/')[1]), 0, 0, 0, 0, 0);
            oClientesDTO.FechaFinStr = new DateTime(Convert.ToInt32(FechaFin.Split('/')[2]), Convert.ToInt32(FechaFin.Split('/')[0]), Convert.ToInt32(FechaFin.Split('/')[1]), 23, 0, 0, 0, 0);
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.CodTiempoPaquete = CodTiempoPaquete;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.TipoIngreso = TipoIngreso;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspTotalVentasTurnos_RangoFechas_Appsfit,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }

            return lista;

        }


        public ClientesDTO TotalPagosVentas(int CodigoUnidadNegocio, int CodSede, string Fecha, string FechaFin, string Vendedor, int Turno,
                                                 int CodTiempoPaquete, string AsesorComercial, string TipoIngreso, int Tipo)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Fecha = new DateTime(Convert.ToInt32(Fecha.Split('/')[2]), Convert.ToInt32(Fecha.Split('/')[0]), Convert.ToInt32(Fecha.Split('/')[1]), 0, 0, 0, 0, 0);
            oClientesDTO.FechaFinStr = new DateTime(Convert.ToInt32(FechaFin.Split('/')[2]), Convert.ToInt32(FechaFin.Split('/')[0]), Convert.ToInt32(FechaFin.Split('/')[1]), 23, 0, 0, 0, 0);
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.Turno = Turno;
            oClientesDTO.CodTiempoPaquete = CodTiempoPaquete;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.TipoIngreso = TipoIngreso;
            oClientesDTO.Tipo = Tipo;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.TotalPagosVentas,
                Item = oClientesDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return oClientesDTO;
        }



        public ClientesDTO TotalPagosVentasCafeteria(int CodigoUnidadNegocio, int CodSede, string Fecha, string FechaFin, string Vendedor, int Turno,
                                                int CodTiempoPaquete, string AsesorComercial, string TipoIngreso, int Tipo)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Fecha = new DateTime(Convert.ToInt32(Fecha.Split('/')[2]), Convert.ToInt32(Fecha.Split('/')[0]), Convert.ToInt32(Fecha.Split('/')[1]), 0, 0, 0, 0, 0);
            oClientesDTO.FechaFinStr = new DateTime(Convert.ToInt32(FechaFin.Split('/')[2]), Convert.ToInt32(FechaFin.Split('/')[0]), Convert.ToInt32(FechaFin.Split('/')[1]), 23, 0, 0, 0, 0);
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.Turno = Turno;
            oClientesDTO.CodTiempoPaquete = CodTiempoPaquete;
            oClientesDTO.AsesorComercial = AsesorComercial;
            oClientesDTO.TipoIngreso = TipoIngreso;
            oClientesDTO.Tipo = Tipo;

            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.TotalPagosVentasCafeteria,
                Item = oClientesDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return oClientesDTO;
        }

        public RespListVentasDetalleDTO uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion(VentasDetalleDTO oItem,int PageNumber)
        {
            if (oItem.AsesorComercial == "Vendedores")
            {
                oItem.AsesorComercial = "";
            }
            if (oItem.AsesorComercial == "Vendedores")
            {
                oItem.AsesorComercial = "";
            }
            
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = oItem.CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = oItem.CodigoSede;
            oVentasDetalleDTO.FechaInicio = oItem.FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = oItem.FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);            
            oVentasDetalleDTO.Tipo = oItem.Tipo; //2 = membresia, 7 = nutricion, 8 = personalizado
            oVentasDetalleDTO.Turno = oItem.Turno;            
            oVentasDetalleDTO.TipoIngresoMembresia = oItem.TipoIngresoMembresia;            
            oVentasDetalleDTO.Counter = oItem.Counter;
            oVentasDetalleDTO.AsesorComercial = oItem.AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = oItem.CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasMembresiasRangoFechasPrecioCero_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            return oResp;

            //oResp.List.Count
            //oResp.Paging.TotalRecord;

            //if (oResp.Success)
            //{
            //    lista = oResp.List;
            //}

            //return lista;

        }


        public List<VentasDetalleDTO> uspReporteVentasMembresiasRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {

            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 2;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial == "Vendedores" ? "" : AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;



        }

        public VentasDetalleDTO uspReporteVentasMembresiasRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }

            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio;
            oVentasDetalleDTO.FechaFin = FechaFin;
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 2;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public List<ContratoDTO> uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int CodigoSede, int TipoIngresoMembresia, int CodigoTiempoPaquete, int PageNumber)
        {
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }

            List<ContratoDTO> lista = null;
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            oContratoDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oContratoDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oContratoDTO.AsesorComercial = AsesorComercial;
            oContratoDTO.TipoIngreso = TipoIngresoMembresia;
            oContratoDTO.CodTiempoMenbresia = CodigoTiempoPaquete;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasDeudasPorDiaRangoFechas_Paginacion,
                User = "Admin",
                Item = oContratoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public ContratoDTO uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int CodigoSede, int TipoIngresoMembresia, int CodigoTiempoPaquete)
        {
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            oContratoDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oContratoDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oContratoDTO.AsesorComercial = AsesorComercial;
            oContratoDTO.TipoIngreso = TipoIngresoMembresia;
            oContratoDTO.CodTiempoMenbresia = CodigoTiempoPaquete;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarMembresiasDeudasPorDiaRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oContratoDTO

            };

            RespItemContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }

            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
                oContratoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oContratoDTO;
        }

        public List<VentasDetalleDTO> uspReporteVentasProductosRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 1;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasProductosRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;


        }

        public VentasDetalleDTO uspReporteVentasProductosRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.FechaInicio = FechaInicio;
            oVentasDetalleDTO.FechaFin = FechaFin;
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 1;
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasProductosRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public List<VentasDetalleDTO> uspReporteVentasCafeteriaRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 1;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasProductosRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;


        }

        public VentasDetalleDTO uspReporteVentasCafeteriaRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.FechaInicio = FechaInicio;
            oVentasDetalleDTO.FechaFin = FechaFin;
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 1;
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasProductosRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public List<VentasDetalleDTO> uspReporteVentasServiciosRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 4;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasServiciosRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;


        }

        public VentasDetalleDTO uspReporteVentasServiciosRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio;
            oVentasDetalleDTO.FechaFin = FechaFin;
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 4;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasServiciosRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public List<VentasDetalleDTO> uspReporteVentasLibresRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 3;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial == "Vendedores" ? "" : AsesorComercial; ;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public VentasDetalleDTO uspReporteVentasLibresRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }

            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio;
            oVentasDetalleDTO.FechaFin = FechaFin;
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 3;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public List<GastosDTO> uspReporteEgresoRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int TipoEgreso, int TipoDocumento, int PageNumber)
        {
            List<GastosDTO> lista = null;
            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = CodigoSede;
            oGastosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oGastosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oGastosDTO.Responsable = Vendedor;
            oGastosDTO.TipoEgreso = TipoEgreso;
            oGastosDTO.Turno = Turno;
            oGastosDTO.TipoMoneda = TipoDocumento;// TipoDocumento
            oGastosDTO.Tipo = 1;//gastos administrativos

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.uspReporteEgresoRangoFechas_Paginacion,
                User = "Admin",
                Item = oGastosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListGastosDTO oResp = null;

            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.GastosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }


        public List<GastosDTO> uspReporteEgresoRangoFechas_Exportar(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int TipoEgreso, int TipoDocumento, int PageNumber)
        {
            List<GastosDTO> lista = null;
            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = CodigoSede;
            oGastosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oGastosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oGastosDTO.Responsable = Vendedor;
            oGastosDTO.TipoEgreso = TipoEgreso;
            oGastosDTO.Turno = Turno;
            oGastosDTO.TipoMoneda = TipoDocumento;// TipoDocumento
            oGastosDTO.Tipo = 1;//gastos administrativos

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.uspReporteEgresoRangoFechas_Paginacion,
                User = "Admin",
                Item = oGastosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 10000
                }
            };

            RespListGastosDTO oResp = null;

            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.GastosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }


        public GastosDTO uspReporteEgresoRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int TipoEgreso, int TipoDocumento)
        {

            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }

            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = CodigoSede;
            oGastosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oGastosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oGastosDTO.Responsable = Vendedor;
            oGastosDTO.TipoEgreso = TipoEgreso;
            oGastosDTO.Turno = Turno;
            oGastosDTO.TipoMoneda = TipoDocumento;// TipoDocumento
            oGastosDTO.Tipo = 1;//gastos administrativos

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.uspReporteEgresoRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oGastosDTO
            };

            RespItemGastosDTO oResp = null;

            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.GastosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oGastosDTO = oResp.Item;
                oGastosDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oGastosDTO;
        }


        public List<GastosDTO> uspReporteEgresoRangoFechas_GastosCaja_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int TipoEgreso, int TipoDocumento, int PageNumber)
        {
            List<GastosDTO> lista = null;
            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = CodigoSede;
            oGastosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oGastosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oGastosDTO.Responsable = Vendedor;
            oGastosDTO.TipoEgreso = TipoEgreso;
            oGastosDTO.Turno = Turno;
            oGastosDTO.TipoMoneda = TipoDocumento;// TipoDocumento
            oGastosDTO.Tipo = 2; //caja gastos

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.uspReporteEgresoRangoFechas_Paginacion,
                User = "Admin",
                Item = oGastosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListGastosDTO oResp = null;

            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.GastosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;

        }

        public GastosDTO uspReporteEgresoRangoFechas_GastosCaja_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int TipoEgreso, int TipoDocumento)
        {

            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }

            GastosDTO oGastosDTO = new GastosDTO();
            oGastosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oGastosDTO.CodigoSede = CodigoSede;
            oGastosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oGastosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oGastosDTO.Responsable = Vendedor;
            oGastosDTO.TipoEgreso = TipoEgreso;
            oGastosDTO.Turno = Turno;
            oGastosDTO.TipoMoneda = TipoDocumento;// TipoDocumento
            oGastosDTO.Tipo = 2; //caja gastos

            ReqFilterGastosDTO oReq = new ReqFilterGastosDTO()
            {
                FilterCase = filterCaseGastos.uspReporteEgresoRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oGastosDTO
            };

            RespItemGastosDTO oResp = null;

            using (GastosLogic oEgresosLogic = new GastosLogic())
            {
                oResp = oEgresosLogic.GastosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oGastosDTO = oResp.Item;
                oGastosDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oGastosDTO;
        }


        public List<PerfilMenuDTO> ListarPerfilMenu(int CodigoUnidadNegocio, string Perfil, string User)
        {

            List<PerfilMenuDTO> oListPerfilMenuDTO = null;
            UsuarioDTO item = new UsuarioDTO();
            PerfilMenuDTO oPerfilMenuDTO = new PerfilMenuDTO();
            oPerfilMenuDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPerfilMenuDTO.CodigoPerfil = Convert.ToInt32(Perfil);
            ReqFilterPerfilMenuDTO oReq = new ReqFilterPerfilMenuDTO()
            {
                Item = oPerfilMenuDTO,
                User = User,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListPerfilMenuDTO oResp = null;

            using (PerfilMenuLogic oPerfilMenuLogic = new PerfilMenuLogic())
            {
                oResp = oPerfilMenuLogic.PerfilMenuGetList(oReq);
            }

            if (oResp.Success)
            {
                oListPerfilMenuDTO = oResp.List;
            }

            return oListPerfilMenuDTO;
        }

        public List<VentasDetalleDTO> uspReporteVentasNutricionRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 7;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasNutricionRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public VentasDetalleDTO uspReporteVentasNutricionRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio;
            oVentasDetalleDTO.FechaFin = FechaFin;
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 7;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasNutricionRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasNutricionRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public List<VentasDetalleDTO> uspReporteVentasPersonalizadoRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 8;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasPersonalizadoRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;
            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public VentasDetalleDTO uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int CodigoSede, int Turno, int FormaPago, string TipoIngresoMembresia, int TipoCliente, string Counter, string AsesorComercial, int CodigoTiempoPaquete)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio;
            oVentasDetalleDTO.FechaFin = FechaFin;
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = 8;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.TipoIngresoMembresia = TipoIngresoMembresia;
            oVentasDetalleDTO.TipoCliente = TipoCliente;
            oVentasDetalleDTO.Counter = Counter;
            oVentasDetalleDTO.AsesorComercial = AsesorComercial;
            oVentasDetalleDTO.CodigoTiempoPaquete = CodigoTiempoPaquete;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasPersonalizadoRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasPersonalizadoRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public ContratoDTO uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int CodigoSede, int TipoIngresoMembresia, int CodigoTiempoPaquete, int Tipo)
        {

            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            oContratoDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oContratoDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oContratoDTO.AsesorComercial = AsesorComercial;
            oContratoDTO.TipoIngreso = TipoIngresoMembresia;
            oContratoDTO.CodTiempoMenbresia = CodigoTiempoPaquete;
            oContratoDTO.Tipo = Tipo;

            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarDeudasTotalesPorTipoDiaRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oContratoDTO

            };

            RespItemContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetItem(oReq);
            }

            if (oResp.Success)
            {
                oContratoDTO = oResp.Item;
                oContratoDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRangoFechas_Paginacion"]);
            }

            return oContratoDTO;
        }

        public List<ContratoDTO> uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion(int CodigoUnidadNegocio, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int CodigoSede, int TipoIngresoMembresia, int CodigoTiempoPaquete, int Tipo, int PageNumber)
        {
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<ContratoDTO> lista = null;
            ContratoDTO oContratoDTO = new ContratoDTO();
            oContratoDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oContratoDTO.CodigoSede = CodigoSede;
            oContratoDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oContratoDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oContratoDTO.AsesorComercial = AsesorComercial;
            oContratoDTO.TipoIngreso = TipoIngresoMembresia;
            oContratoDTO.CodTiempoMenbresia = CodigoTiempoPaquete;
            oContratoDTO.Tipo = Tipo;
            ReqFilterContratoDTO oReq = new ReqFilterContratoDTO()
            {
                FilterCase = filterCaseContrato.uspListarDeudasTotalesPorTipoDiaRangoFechas_Paginacion,
                User = "Admin",
                Item = oContratoDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListContratoDTO oResp = null;

            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ContratoGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public List<VentasDetalleDTO> uspReporteVentasSuplementosTotalesRangoFechas_Paginacion(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter, int PageNumber)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }

            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = Tipo;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.Counter = Counter;
            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasSuplementosTotalesRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public VentasDetalleDTO uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = Tipo;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.Counter = Counter;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasSuplementosTotalesRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasSuplementosTotalesRangoFechas_Paginacion"]);
            }

            return oVentasDetalleDTO;

        }

        public ClientesDTO uspTotalPagosSuplementosRangoFechas(int CodigoUnidadNegocio, int CodSede, string Fecha, string FechaFin, string Vendedor, int Tipo, int Turno, string Counter)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Fecha = new DateTime(Convert.ToInt32(Fecha.Split('/')[2]), Convert.ToInt32(Fecha.Split('/')[0]), Convert.ToInt32(Fecha.Split('/')[1]), 0, 0, 0, 0, 0);
            oClientesDTO.FechaFinStr = new DateTime(Convert.ToInt32(FechaFin.Split('/')[2]), Convert.ToInt32(FechaFin.Split('/')[0]), Convert.ToInt32(FechaFin.Split('/')[1]), 23, 0, 0, 0, 0);
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.Turno = Turno;
            oClientesDTO.Tipo = Tipo;
            oClientesDTO.Counter = Counter;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspTotalPagosSuplementosRangoFechas,
                Item = oClientesDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return oClientesDTO;
        }

        public List<PagosSuplementosDTO> uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int PageNumber)
        {
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<PagosSuplementosDTO> lista = null;
            PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();
            oPagosSuplementosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPagosSuplementosDTO.CodigoSede = CodigoSede;
            oPagosSuplementosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oPagosSuplementosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oPagosSuplementosDTO.UsuarioCreacion = AsesorComercial;

            ReqFilterPagosSuplementosDTO oReq = new ReqFilterPagosSuplementosDTO()
            {
                FilterCase = filterCasePagosSuplementos.uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion,
                User = "Admin",
                Item = oPagosSuplementosDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListPagosSuplementosDTO oResp = null;

            using (PagosSuplementosLogic oPagosSuplementosLogic = new PagosSuplementosLogic())
            {
                oResp = oPagosSuplementosLogic.PagosSuplementosGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }

            return lista;
        }

        public PagosSuplementosDTO uspListarDeudasSuplementosTotalesDiaRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial)
        {
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            PagosSuplementosDTO oPagosSuplementosDTO = new PagosSuplementosDTO();
            oPagosSuplementosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPagosSuplementosDTO.CodigoSede = CodigoSede;
            oPagosSuplementosDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oPagosSuplementosDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oPagosSuplementosDTO.UsuarioCreacion = AsesorComercial;

            ReqFilterPagosSuplementosDTO oReq = new ReqFilterPagosSuplementosDTO()
            {
                FilterCase = filterCasePagosSuplementos.uspListarDeudasSuplementosTotalesDiaRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oPagosSuplementosDTO

            };

            RespItemPagosSuplementosDTO oResp = null;

            using (PagosSuplementosLogic oPagosSuplementosLogic = new PagosSuplementosLogic())
            {
                oResp = oPagosSuplementosLogic.PagosSuplementosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oPagosSuplementosDTO = oResp.Item;
                oPagosSuplementosDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDeudasSuplementosTotalesDiaRangoFechas_Paginacion"]);
            }

            return oPagosSuplementosDTO;
        }

        public UsuariosIngresosDTO uspValidarAccesoSistema(int CodigoUnidadNegocio, int CodigoSede, string Usuario, string tk, string latitud, string longitud)
        {
            UsuariosIngresosDTO oUsuariosIngresosDTO = new UsuariosIngresosDTO();
            oUsuariosIngresosDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oUsuariosIngresosDTO.CodigoSede = CodigoSede;
            oUsuariosIngresosDTO.UsuarioCreacion = Usuario;
            oUsuariosIngresosDTO.CodigoIngreso = Convert.ToInt32(tk.Split('|')[1]);
            oUsuariosIngresosDTO.Latitud = latitud;
            oUsuariosIngresosDTO.Longitud = longitud;

            ReqFilterUsuariosIngresosDTO oReq = new ReqFilterUsuariosIngresosDTO()
            {
                FilterCase = filterCaseUsuariosIngresos.uspValidarAccesoSistema,
                Item = oUsuariosIngresosDTO,
                User = "Admin"
            };
            RespItemUsuariosIngresosDTO oResp = null;
            using (UsuariosIngresosLogic oUsuariosIngresosLogic = new UsuariosIngresosLogic())
            {
                oResp = oUsuariosIngresosLogic.UsuariosIngresosGetItem(oReq);
            }
            if (oResp.Success)
            {
                oUsuariosIngresosDTO = oResp.Item;
            }
            return oUsuariosIngresosDTO;
        }

        public List<VentasDetalleDTO> uspReporteVentasRopasTotalesRangoFechas_Paginacion(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter, int PageNumber)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }
            List<VentasDetalleDTO> lista = null;
            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = Tipo;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.Counter = Counter;
            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasRopasTotalesRangoFechas_Paginacion,
                User = "Admin",
                Item = oVentasDetalleDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public VentasDetalleDTO uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string Vendedor, int Tipo, int Turno, int FormaPago, string Counter)
        {
            if (Counter == "Counter")
            {
                Counter = "";
            }
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }

            VentasDetalleDTO oVentasDetalleDTO = new VentasDetalleDTO();
            oVentasDetalleDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oVentasDetalleDTO.CodigoSede = CodigoSede;
            oVentasDetalleDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oVentasDetalleDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oVentasDetalleDTO.Vendedor = Vendedor;
            oVentasDetalleDTO.Tipo = Tipo;
            oVentasDetalleDTO.Turno = Turno;
            oVentasDetalleDTO.FormaPago = FormaPago;
            oVentasDetalleDTO.Counter = Counter;

            ReqFilterVentasDetalleDTO oReq = new ReqFilterVentasDetalleDTO()
            {
                FilterCase = filterCaseVentasDetalle.uspReporteVentasRopasTotalesRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oVentasDetalleDTO,
            };

            RespItemVentasDetalleDTO oResp = null;

            using (VentasDetalleLogic oControlDetalleSalidaLogic = new VentasDetalleLogic())
            {
                oResp = oControlDetalleSalidaLogic.VentasDetalleGetItem(oReq);
            }

            if (oResp.Success)
            {
                oVentasDetalleDTO = oResp.Item;
                oVentasDetalleDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspReporteVentasRopasTotalesRangoFechas_Paginacion"]);
            }
            return oVentasDetalleDTO;
        }

        public ClientesDTO uspTotalPagosRopasRangoFechas(int CodigoUnidadNegocio, int CodSede, string Fecha, string FechaFin, string Vendedor, int Tipo, int Turno, string Counter)
        {
            if (Vendedor == "Vendedores")
            {
                Vendedor = "";
            }

            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Fecha = new DateTime(Convert.ToInt32(Fecha.Split('/')[2]), Convert.ToInt32(Fecha.Split('/')[0]), Convert.ToInt32(Fecha.Split('/')[1]), 0, 0, 0, 0, 0);
            oClientesDTO.FechaFinStr = new DateTime(Convert.ToInt32(FechaFin.Split('/')[2]), Convert.ToInt32(FechaFin.Split('/')[0]), Convert.ToInt32(FechaFin.Split('/')[1]), 23, 0, 0, 0, 0);
            oClientesDTO.Vendedor = Vendedor;
            oClientesDTO.Turno = Turno;
            oClientesDTO.Tipo = Tipo;
            oClientesDTO.Counter = Counter;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspTotalPagosRopasRangoFechas,
                Item = oClientesDTO,
                Paging = new Paging() { All = true, PageRecords = 0 },
                User = "Admin"
            };

            RespItemClientesDTO oResp = null;
            using (ClientesLogic oSociosLogic = new ClientesLogic())
            {
                oResp = oSociosLogic.SociosGetItem(oReq);
            }

            if (oResp.Success)
            {
                oClientesDTO = oResp.Item;
            }

            return oClientesDTO;
        }

        public List<PagosRopasDTO> uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial, int PageNumber)
        {
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            List<PagosRopasDTO> lista = null;
            PagosRopasDTO oPagosRopasDTO = new PagosRopasDTO();
            oPagosRopasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPagosRopasDTO.CodigoSede = CodigoSede;
            oPagosRopasDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oPagosRopasDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oPagosRopasDTO.UsuarioCreacion = AsesorComercial;
            ReqFilterPagosRopasDTO oReq = new ReqFilterPagosRopasDTO()
            {
                FilterCase = filterCasePagosRopas.uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion,
                User = "Admin",
                Item = oPagosRopasDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListPagosRopasDTO oResp = null;
            using (PagosRopasLogic oPagosRopasLogic = new PagosRopasLogic())
            {
                oResp = oPagosRopasLogic.PagosRopasGetList(oReq);
            }
            if (oResp.Success)
            {
                lista = oResp.List;
            }
            return lista;
        }

        public PagosRopasDTO uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros(int CodigoUnidadNegocio, int CodigoSede, DateTime FechaInicio, DateTime FechaFin, string AsesorComercial)
        {
            if (AsesorComercial == "Vendedores")
            {
                AsesorComercial = "";
            }
            PagosRopasDTO oPagosRopasDTO = new PagosRopasDTO();
            oPagosRopasDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oPagosRopasDTO.CodigoSede = CodigoSede;
            oPagosRopasDTO.FechaInicio = FechaInicio.AddMinutes(0).AddSeconds(0).AddMinutes(0).AddMilliseconds(0);
            oPagosRopasDTO.FechaFin = FechaFin.AddMinutes(23).AddSeconds(50).AddMinutes(50).AddMilliseconds(0);
            oPagosRopasDTO.UsuarioCreacion = AsesorComercial;
            ReqFilterPagosRopasDTO oReq = new ReqFilterPagosRopasDTO()
            {
                FilterCase = filterCasePagosRopas.uspListarDeudasRopasTotalesDiaRangoFechas_NumeroRegistros,
                User = "Admin",
                Item = oPagosRopasDTO
            };
            RespItemPagosRopasDTO oResp = null;
            using (PagosRopasLogic oPagosRopasLogic = new PagosRopasLogic())
            {
                oResp = oPagosRopasLogic.PagosRopasGetItem(oReq);
            }
            if (oResp.Success)
            {
                oPagosRopasDTO = oResp.Item;
                oPagosRopasDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarDeudasRopasTotalesDiaRangoFechas_Paginacion"]);
            }
            return oPagosRopasDTO;
        }

        public List<CajaAperturaCierreDTO> uspListarAperturaCaja_Paginacion(int CodigoUnidadNegocio, int CodigoSede, string UsuarioCreacion, DateTime FechaCreacion, DateTime Fecha, int PageNumber)
        {
            List<CajaAperturaCierreDTO> lista = null;
            CajaAperturaCierreDTO oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
            oCajaAperturaCierreDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oCajaAperturaCierreDTO.CodigoSede = CodigoSede;
            oCajaAperturaCierreDTO.UsuarioCreacion = UsuarioCreacion;
            oCajaAperturaCierreDTO.FechaCreacion = FechaCreacion;
            oCajaAperturaCierreDTO.Fecha = Fecha;

            ReqFilterCajaAperturaCierreDTO oReq = new ReqFilterCajaAperturaCierreDTO()
            {
                FilterCase = filterCaseCajaAperturaCierre.uspListarAperturaCaja_Paginacion,
                User = "ADMIN",
                Item = oCajaAperturaCierreDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };
            RespListCajaAperturaCierreDTO oResp = null;
            using (CajaAperturaCierreLogic oCajaAperturaCierreLogic = new CajaAperturaCierreLogic())
            {
                oResp = oCajaAperturaCierreLogic.CajaAperturaCierreGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List.ToList();
            }
            return lista;
        }

        public CajaAperturaCierreDTO uspListarAperturaCaja_NumeroRegistros(int CodigoUnidadNegocio, int CodigoSede, string UsuarioCreacion, DateTime FechaCreacion, DateTime Fecha)
        {
            CajaAperturaCierreDTO oCajaAperturaCierreDTO = new CajaAperturaCierreDTO();
            oCajaAperturaCierreDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oCajaAperturaCierreDTO.CodigoSede = CodigoSede;
            oCajaAperturaCierreDTO.UsuarioCreacion = UsuarioCreacion;
            oCajaAperturaCierreDTO.FechaCreacion = FechaCreacion;
            oCajaAperturaCierreDTO.Fecha = Fecha;

            ReqFilterCajaAperturaCierreDTO oReq = new ReqFilterCajaAperturaCierreDTO()
            {
                FilterCase = filterCaseCajaAperturaCierre.uspListarAperturaCaja_NumeroRegistros,
                User = "Admin",
                Item = oCajaAperturaCierreDTO
            };
            RespItemCajaAperturaCierreDTO oResp = null;
            using (CajaAperturaCierreLogic oCajaAperturaCierreLogic = new CajaAperturaCierreLogic())
            {
                oResp = oCajaAperturaCierreLogic.CajaAperturaCierreGetItem(oReq);
            }
            if (oResp.Success)
            {
                oCajaAperturaCierreDTO = oResp.Item;
                oCajaAperturaCierreDTO.TamanioPagina = Convert.ToInt32(ConfigurationManager.AppSettings["RecordNumForPage_uspListarAperturaCaja_Paginacion"]);
            }
            return oCajaAperturaCierreDTO;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #region Angel Rojas

        public List<ClientesDTO> uspListarVentasTotal(int CodigoUnidadNegocio, int CodSede, DateTime fecha_inicio, DateTime fecha_fin)
        {

            List<ClientesDTO> lista = null;
            ClientesDTO oClientesDTO = new ClientesDTO();
            oClientesDTO.CodigoUnidadNegocio = CodigoUnidadNegocio;
            oClientesDTO.CodigoSede = CodSede;
            oClientesDTO.Fecha = fecha_inicio;
            oClientesDTO.FechaFinStr = fecha_fin;
            ReqFilterClientesDTO oReq = new ReqFilterClientesDTO()
            {
                FilterCase = filterCaseClientes.uspListarVentasTotal,
                User = "appsfit",
                Item = oClientesDTO,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = true,
                    PageNumber = 0,
                    PageRecords = 0
                }
            };

            RespListClientesDTO oResp = null;

            using (ClientesLogic oClientesLogic = new ClientesLogic())
            {
                oResp = oClientesLogic.ClientesGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = oResp.List;

            }

            return lista;

        }

        #endregion

    }
}