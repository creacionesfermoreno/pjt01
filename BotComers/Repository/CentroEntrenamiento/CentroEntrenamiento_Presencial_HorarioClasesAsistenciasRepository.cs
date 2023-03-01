using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_HorarioClasesAsistenciasRepository : IDisposable
    {
        public string CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias_ReservarYMarcarAsistencia(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            string mensaje = string.Empty;

            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal == null ? string.Empty : request.CodigoHorarioClasesConfiguracionTiempoReal,
                NroCupo = 0,
                CodigoSocio = request.CodigoSocio,
                CodigoInvitado = 0,
                CodigoMembresia = request.CodigoMembresia,                                                
                UsuarioCreacion = request.UsuarioCreacion,                
                Operation = Operation.CreateReservarYMarcarAsistencia
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                List = list,
                User = request.UsuarioCreacion
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Detalle;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_UspRegistrarPresencial_HorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal == null ? string.Empty : request.CodigoHorarioClasesConfiguracionTiempoReal,
                CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias == null ? string.Empty : request.CodigoHorarioClasesConfiguracionAsistencias,
                NroCupo = 0,
                CodigoSocio = request.CodigoSocio,
                CodigoInvitado = 0,
                CodigoMembresia = request.CodigoMembresia,
                CodigoPaquete = request.CodigoPaquete,
                FechaReservacion = request.FechaReservacion,
                DiaNumero = request.DiaNumero,
                UsuarioCreacion = request.UsuarioCreacion,
                UsuarioReservacion = request.UsuarioCreacion,
                Operation = request.Accion == "N" ? Operation.Create : Operation.Update,
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                List = list,
                User = request.UsuarioCreacion
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public int CentroEntrenamiento_UspActualizarPresencial_DesactivarHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            int validacion = 0;
            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal == null ? string.Empty : request.CodigoHorarioClasesConfiguracionTiempoReal,
                CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias == null ? string.Empty : request.CodigoHorarioClasesConfiguracionAsistencias,
                CodigoSocio = request.CodigoSocio,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = Operation.Update
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                validacion = oResp.MessageList[0].Codigo;
            }
            return validacion;
        }

        public string CentroEntrenamiento_UspActualizarPresencial_MarcarAsistenciaHorarioClasesAsistencias(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            string validacion = string.Empty;
            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> list = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();

            list.Add(new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                //CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion,
                CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal == null ? string.Empty : request.CodigoHorarioClasesConfiguracionTiempoReal,
                CodigoHorarioClasesConfiguracionAsistencias = request.CodigoHorarioClasesConfiguracionAsistencias == null ? string.Empty : request.CodigoHorarioClasesConfiguracionAsistencias,
                CodigoSocio = request.CodigoSocio,
                CodigoMembresia = request.CodigoMembresia,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = Operation.UpdateMarcarAsistenciaReserva
            });

            ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;
            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                validacion = oResp.MessageList[0].Detalle;
            }
            return validacion;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoHorarioClasesConfiguracionTiempoReal = request.CodigoHorarioClasesConfiguracionTiempoReal
                    
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.CentroEntrenamiento_Presencial_HorarioClasesAsistenciasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking(CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO request)
        {
            List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
            {
                Item = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoHorarioClasesConfiguracion = request.CodigoHorarioClasesConfiguracion
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias.CentroEntrenamiento_uspListarPresencial_HorarioClasesAsistenciasGestion_Cheking,
                User = "appsfit",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO oResp = null;

            using (CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic = new CentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_HorarioClasesAsistenciasLogic.CentroEntrenamiento_Presencial_HorarioClasesAsistenciasGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO>();
                lista = oResp.List;
            }

            return lista;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}