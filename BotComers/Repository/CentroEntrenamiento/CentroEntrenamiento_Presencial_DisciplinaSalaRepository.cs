using E_BusinessLayer.CentroEntrenamiento;
using E_DataModel.CentroEntrenamiento;
using E_DataModel.Common;
using System;
using System.Collections.Generic;

namespace BotComers.Repository.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_DisciplinaSalaRepository : IDisposable
    {

        public List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> CentroEntrenamiento_uspListarDisciplinaSala_Presencial(CentroEntrenamiento_Presencial_DisciplinaSalaDTO request)
        {
            List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> lista = null;

            ReqFilterCentroEntrenamiento_Presencial_DisciplinaSalaDTO oReq = new ReqFilterCentroEntrenamiento_Presencial_DisciplinaSalaDTO()
            {
                Item = new CentroEntrenamiento_Presencial_DisciplinaSalaDTO()
                {
                    CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                    CodigoSede = request.CodigoSede,
                    CodigoSala = request.CodigoSala
                },
                FilterCase = filterCaseCentroEntrenamiento_Presencial_DisciplinaSala.CentroEntrenamiento_uspListarDisciplinaSala_Presencial,
                User = "Admin",
                Paging = new E_DataModel.Common.Paging()
                {
                    All = false,
                    PageNumber = Convert.ToUInt32(1),
                    PageRecords = 0
                }
            };

            RespListCentroEntrenamiento_Presencial_DisciplinaSalaDTO oResp = null;

            using (CentroEntrenamiento_Presencial_DisciplinaSalaLogic oCentroEntrenamiento_Presencial_DisciplinaSalaLogic = new CentroEntrenamiento_Presencial_DisciplinaSalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_DisciplinaSalaLogic.CentroEntrenamiento_Presencial_DisciplinaSalaGetList(oReq);
            }

            if (oResp.Success)
            {
                lista = new List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO>();
                lista = oResp.List;
            }

            return lista;
        }

        public int CentroEntrenamiento_uspRegistrarDisciplinaSala_Presencial(CentroEntrenamiento_Presencial_DisciplinaSalaDTO request)
        {
            int mensaje = 0;

            List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO> list = new List<CentroEntrenamiento_Presencial_DisciplinaSalaDTO>();

            list.Add(new CentroEntrenamiento_Presencial_DisciplinaSalaDTO()
            {
                CodigoUnidadNegocio = request.CodigoUnidadNegocio,
                CodigoSede = request.CodigoSede,
                CodigoSala = request.CodigoSala,
                CodigoDisciplinaSala = request.CodigoDisciplinaSala,
                Disciplina = request.Disciplina,
                Capacidad = request.Capacidad,
                Color = request.Color,
                UsuarioCreacion = request.UsuarioCreacion,
                Operation = request.Accion == "N" ? Operation.Create : Operation.Update,
            });

            ReqCentroEntrenamiento_Presencial_DisciplinaSalaDTO oReq = new ReqCentroEntrenamiento_Presencial_DisciplinaSalaDTO()
            {
                List = list,
                User = "admin"
            };
            RespCentroEntrenamiento_Presencial_DisciplinaSalaDTO oResp = null;
            using (CentroEntrenamiento_Presencial_DisciplinaSalaLogic oCentroEntrenamiento_Presencial_DisciplinaSalaLogic = new CentroEntrenamiento_Presencial_DisciplinaSalaLogic())
            {
                oResp = oCentroEntrenamiento_Presencial_DisciplinaSalaLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = oResp.MessageList[0].Codigo;
            }

            return mensaje;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}