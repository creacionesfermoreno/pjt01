
using E_BusinessLayer.Gimnasio;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppsfitWebApi.Repository
{
    public class CheckingRepository : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public int GuardarMembresiaCongelamiento(int CodigoUnidadNegocio, int codigo, DateTime fechaInicio, DateTime fechaFin, int FrezenDisponibles, int NroDiasCongelar, DateTime fechaFreziing, DateTime fechaDesFreziing, int CodSede, int CodigoSocio, int NroDias, string NroSolicitud, string Motivo, string User)
        {
            string mensaje = string.Empty;
            int codigoMembresia = 0;
            int EstadoMembresia = 0;

            if (fechaFreziing > DateTime.Now)
            {
                EstadoMembresia = 1; //estado activo y futura congelacion programada y pasara a estado 0
            }
            else
            {
                EstadoMembresia = 0; //estado congelado
            }


            DateTime fechaCongelacionProgramada = DateTime.Now;
            fechaCongelacionProgramada = fechaCongelacionProgramada.AddDays(-1);

            DateTime fechaDesCongelacion;

            fechaCongelacionProgramada = fechaFreziing;

            if (fechaFreziing == null)
            {
                fechaDesCongelacion = fechaDesFreziing;// DateTime.Now.AddDays(NroDiasCongelar);
            }
            else
            {
                fechaDesCongelacion = fechaDesFreziing;// Convert.ToDateTime(fechaFreziing).AddDays(NroDiasCongelar);
            }

            List<ContratoDTO> list = new List<ContratoDTO>();

            list.Add(new ContratoDTO()
            {
                CodigoUnidadNegocio = CodigoUnidadNegocio,
                CodigoMenbresia = codigo,
                FechaInicio = new DateTime(fechaInicio.Year, fechaInicio.Month, fechaInicio.Day),
                FechaFin = new DateTime(fechaFin.Year, fechaFin.Month, fechaFin.Day),
                FrezenDisponibles = FrezenDisponibles,
                FechaCongelacionProgramada = fechaCongelacionProgramada,
                FechaDesCongelacion = fechaDesCongelacion,
                CodigoSede = CodSede,
                Estado = EstadoMembresia,
                CodigoSocio = CodigoSocio,
                NroDias = NroDias,
                NroSolicitud = NroSolicitud,
                Motivo = Motivo,
                UsuarioCreacion = User,
                Operation = Operation.UpdateMembresiaFreezing
            });

            ReqContratoDTO oReq = new ReqContratoDTO()
            {
                List = list,
                User = User
            };

            RespContratoDTO oResp = null;
            using (ContratoLogic oMenbresiasLogic = new ContratoLogic())
            {
                oResp = oMenbresiasLogic.ExecuteTransac(oReq);
            }
            if (oResp.Success)
            {
                mensaje = "";
                codigoMembresia = oResp.MessageList[0].Codigo;
            }

            return codigoMembresia;
        }

    }
}