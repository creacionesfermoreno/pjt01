using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class HorariosModel
    {
        public string CodigoHorarioClasesConfiguracion { get; set; }
        public string CodigoHorarioClasesTiempoReal { get; set; }
        public int CapacidadPermitida { get; set; }
        public int CantidadAsistencias { get; set; }
        public int CantidadPlazas { get; set; }
        public string EstadoReserva { get; set; }
        public string ColorReserva { get; set; }
        public int DiaNumero { get; set; }
        public string Disciplina { get; set; }
        public string DesSala { get; set; }

        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string HoraInicioTexto { get; set; }
        public string HoraFinTexto { get; set; }
        public string FechaInicioTexto { get; set; }
        public string FechaFinTexto { get; set; }        
        public string CodigoHorarioClasesConfiguracionAsistencias { get; set; }

        public int CodigoSocio { get; set; }
        public int CodigoMembresia { get; set; }
        public int CodigoPaquete { get; set; }
        public int validacionCancelarCita { get; set; }

        public bool CompartirLinkSala { get; set; }
        public string LinkSala { get; set; }
        public string NombreProfesionalFitness { get; set; }

        public int EventoBoton { get; set; }
        public string MensajeBoton { get; set; }

    }
}