using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessRepository : IDisposable
    {
        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                Item = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    DiaNumero = request.DiaNumero,
                    FechaCreacion = request.FechaCreacion
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal,
                User = "appsift",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;

            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                Item = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala,
                    DiaNumero = request.DiaNumero,
                    FechaCreacion = request.FechaCreacion
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspListarPresencial_SalaMaquinas_HorarioTemporal_Configuracion,
                User = "appsift",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;

            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                Item = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspListarPresencial_ConfiguracionSalaFitness,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;

            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public int CentroEntrenamiento_uspRegistrarPresencial_ConfiguracionSalaFitness(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> list = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();

            foreach (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO item in request.lista)
            {
                if (item.CodigoSala > 0)
                {
                    list.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                    {
                        CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                        CodigoSede = request.CodigoSede,
                        CodigoSala = item.CodigoSala,
                        CodigoConfiguracionSalaFitness = item.CodigoConfiguracionSalaFitness == null ? string.Empty : request.CodigoConfiguracionSalaFitness,
                        DiaNumero = item.DiaNumero,
                        DiaNombre = item.DiaNombre,
                        HoraInicio = item.HoraInicio,
                        HoraFin = item.HoraFin,
                        Tiempo = item.Tiempo,
                        Minutos = item.Minutos,
                        CapacidadPermitida = item.CapacidadPermitida,
                        //NroHorarios = item.NroHorarios,
                        //AforoxHorario = item.AforoxHorario,
                        UsuarioCreacion = request.UsuarioCreacion,
                        Operation = request.Accion == "N" ? Operation.Create : Operation.Update,
                    });
                }

            }


            ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        //ELIMINAR HORARIO DE LUNES A DOMINGO
        public int CentroEntrenamiento_uspEliminarPresencial_SalaMaquinas_HorarioTemporal(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> list = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();

            list.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoSala = request.CodigoSala,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = Operation.Delete,
            });

            ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        //CAMBIAR AFORO CONFIGURACION
        public int CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> list = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();

            list.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                UsuarioCreacion = request.UsuarioCreacion,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CapacidadPermitida = request.CapacidadPermitida,
                Operation = Operation.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_CambiarAforo,
            });

            ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        //DESACTIVAR CONFIGURACION
        public int CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> list = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();

            list.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                UsuarioCreacion = request.UsuarioCreacion,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                Operation = Operation.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Desactivar,
            });

            ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        //ACTIVAR CONFIGURACION
        public int CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO> list = new List<CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO>();

            list.Add(new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                UsuarioCreacion = request.UsuarioCreacion,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                Operation = Operation.CentroEntrenamiento_uspActualizarPresencial_HorarioClasesConfiguracion_Activar,
            });

            ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;
            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        //BUSCAR CONFIGURACION
        public CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion(CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO request)
        {
            CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO response = null;

            ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
            {
                Item = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_ConfiguracionSalaFitness.CentroEntrenamiento_uspBuscarPresencial_HorarioClasesConfiguracion,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespItemCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO oResp = null;

            using (CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_ConfiguracionSalaFitnessLogic.CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessGetItem(oReq);
            }

            if (oResp.Success)
            {
                response = new CentroEntrenamiento_Presencial_ConfiguracionSalaFitnessDTO();
                response = oResp.Item;
            }

            return response;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}