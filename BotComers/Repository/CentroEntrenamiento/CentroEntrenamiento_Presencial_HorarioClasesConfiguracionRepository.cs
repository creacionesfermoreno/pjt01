using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_HorarioClasesConfiguracionRepository : IDisposable
    {   
        
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> uspListarPresencial_HorarioClasesConfiguracionCalendarioChecking(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendarioChecking,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    DiaNumero = request.DiaNumero,
                    CodigoSocio = request.CodigoSocio,
                    FechaHoraReserva = request.FechaHoraReserva
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionCalendario_SALAMAQUINAS,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    DiaNumero = request.DiaNumero,
                    CodigoSocio = request.CodigoSocio,
                    FechaHoraReserva = request.FechaHoraReserva,
                    HoraInicio = request.HoraInicio,
                    HoraFin = request.HoraFin
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_SALAMAQUINAS,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }
        
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,                 
                    CodigoSocio = request.CodigoSocio
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_Hoy,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    DiaNumero = request.DiaNumero,
                    HoraInicio = request.HoraInicio,
                    HoraFin = request.HoraFin
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinas,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        //LISTA DE CLASES CREADAS EN TIEMPO REAL CLASES GRUPALES Y DE MAQUINAS
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request, int PageNumber)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    FechaHoraReservaInicio_filtro = request.FechaHoraReservaInicio_filtro,
                    FechaHoraReservaFin_filtro = request.FechaHoraReservaFin_filtro,
                    TipoSala = request.TipoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(PageNumber),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    FechaHoraReservaInicio_filtro = request.FechaHoraReservaInicio_filtro,
                    FechaHoraReservaFin_filtro = request.FechaHoraReservaFin_filtro,
                    TipoSala = request.TipoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracion_HISTORIALCLASES_NroRegistros,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return oItem;
        }

        //LISTAR LOS HORARIOS DE LA SALA DE MAQUINAS QUE HAN SIDO RESERVADAS DESDE HOY PARA ADELANTE
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_SALAMAQUINAS_VALIDACIONEXISTE,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }


        //LISTAR USUARIOS FIT

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Buscador_filtro = request.Buscador_filtro
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    Buscador_filtro = request.Buscador_filtro
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_UsuariosFitGestion_NroRegistros,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return oItem;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionPaginaWeb_ConfiguracionSalaMaquinasINACTIVOS,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        private string convertirDiaNumeroaNombreSemana(string dia)
        {
            switch (dia)
            {
                case "1": dia = "Domingo"; break;
                case "2": dia = "Lunes"; break;
                case "3": dia = "Martes"; break;
                case "4": dia = "Miercoles"; break;
                case "5": dia = "Jueves"; break;
                case "6": dia = "Viernes"; break;
                case "7": dia = "Sabado"; break;
                default: dia = ""; break;
            }

            return dia;
        }

        public int CentroEntrenamiento_uspRegistrarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            string[] dias = request.DiaNombre.Split('|');
            foreach (var dia in dias)
            {
                if (dia != String.Empty)
                {
                    list.Add(new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                    {
                        CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                        CodigoSede = request.CodigoSede,
                        CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                        CodigoDisciplinaSala = request.CodigoDisciplinaSala,
                        CodigoProfesional = request.CodigoProfesional == null ? string.Empty : request.CodigoProfesional,
                        CodigoSala = request.CodigoSala,
                        HoraInicio = request.HoraInicio,
                        HoraFin = request.HoraFin,
                        CapacidadPermitida = request.CapacidadPermitida,
                        DiaNumero = Convert.ToInt32(dia),
                        DiaNombre = convertirDiaNumeroaNombreSemana(dia),
                        CostoPorClase = request.CostoPorClase,
                        DescuentoPorminuto = request.DescuentoPorminuto,
                        CompartirLinkSala = request.CompartirLinkSala,
                        LinkSala = request.LinkSala,
                        UsuarioCreacion = request.UsuarioCreacion,
                        Operation = Operation.Create
                    });
                }
               
            }

            ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CodigoDisciplinaSala = request.CodigoDisciplinaSala,
                CodigoProfesional = request.CodigoProfesional == null ? string.Empty : request.CodigoProfesional,
                CodigoSala = request.CodigoSala,
                HoraInicio = request.HoraInicio,
                HoraFin = request.HoraFin,
                CapacidadPermitida = request.CapacidadPermitida,
                DiaNumero = request.DiaNumero,
                DiaNombre = request.DiaNombre,
                CostoPorClase = request.CostoPorClase,
                DescuentoPorminuto = request.DescuentoPorminuto,
                CompartirLinkSala = request.CompartirLinkSala,
                LinkSala = request.LinkSala,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = request.Accion == "N" ? Operation.Create : Operation.Update,
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }


        public int CentroEntrenamiento_uspDesactivarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                Operation = Operation.Delete
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = Operation.CentroEntrenamiento_uspDeshabilitarTodoPresencial_SalaMaquinas_SALAMAQUINASTIEMPOREAL
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    DiaNumero = request.DiaNumero,
                    CodigoSocio = request.CodigoSocio,
                    FechaHoraReserva = request.FechaHoraReserva,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracionReservadoPaginaWeb_SALAMAQUINAS,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return oItem;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspBuscarHorarioClasesConfiguracionPresencial_PorCodigo,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return oItem;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspObtenerFechasReservas_Configuracion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoMembresia = request.CodigoMembresia,
                    CodigoPaquete = request.CodigoPaquete,
                    CodigoSocio = request.CodigoSocio,
                    FechaHoraReserva = request.FechaHoraReserva
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspObtenerFechasReservas_Configuracion,
                User = request.UsuarioCreacion,
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return oItem;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    FechaHoraReserva = request.FechaHoraReserva,
                    Buscador_filtro = request.Buscador_filtro,
                    FechaHoraReservaInicio_filtro = request.FechaHoraReservaInicio_filtro,
                    FechaHoraReservaFin_filtro = request.FechaHoraReservaFin_filtro,
                    Estado = request.Estado

                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 100
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros(CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO request)
        {
            CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oItem = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO();

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    FechaHoraReserva = request.FechaHoraReserva,
                    Buscador_filtro = request.Buscador_filtro,
                    FechaHoraReservaInicio_filtro = request.FechaHoraReservaInicio_filtro,
                    FechaHoraReservaFin_filtro = request.FechaHoraReservaFin_filtro,
                    Estado = request.Estado
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesConfiguracion.CentroEntrenamiento_uspListarPresencial_HorarioClasesConfiguracionGestion_NumeroRegistros,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic = new CentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesConfiguracionLogic.CentroEntrenamiento_Presencial_HorarioClasesConfiguracionGetItem(oReq);
            }

            if (oResp.Success)
            {
                oItem = oResp.Item;
            }
            return oItem;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}