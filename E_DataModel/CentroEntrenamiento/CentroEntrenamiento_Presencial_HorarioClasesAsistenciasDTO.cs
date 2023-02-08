
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.CentroEntrenamiento
{
    public class CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO:AuditoriaDTO
    {      
       public string  CodigoHorarioClasesConfiguracion { get; set; }
       public string  CodigoHorarioClasesConfiguracionTiempoReal { get; set; }
       public string  CodigoHorarioClasesConfiguracionAsistencias { get; set; }
       public int CodigoDisciplinaSala { get; set; }
       public int  NroCupo { get; set; }
       public string Correo { get; set; }
       public int CodigoSocio { get; set; }
       public int CodigoInvitado { get; set; }
       public int CodigoMembresia { get; set; }
       public int CodigoPaquete { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraFin { get; set; }

        public string HoraInicioTexto { get; set; }
        public string HoraFinTexto { get; set; }
      
        public string Disciplina { get; set; }
        public DateTime  FechaHoraReserva { get; set; }
       public string  UsuarioReservacion { get; set; }
       public bool  flagAsistio { get; set; }
       public DateTime  FechaHoraAsistio { get; set; }
       public bool Estado { get; set; }
        public string flagVistaBotonMarcarAsistencia { get; set; }
        public string flagVistaImagenAsistio { get; set; }
        public string DesflagAsistio { get; set; }
        public int DiaNumero { get; set; }
        public string DiaSemana { get; set; }
        public DateTime FechaReservacion { get; set; }
        public string ImagenUrl { get; set; }
        public string DNI { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string PlanMembresia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public Common.Operation Operation { get; set; }

        public string Accion { get; set; }
    }
    

    public class ReqCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO : Request
    {
        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> List { get; set; }
    }

    public class ReqFilterCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO Item { get; set; }
        public Common.filterCaseCentroEntrenamiento_Presencial_HorarioClasesAsistencias FilterCase { get; set; }
    }

    public class RespCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO : Response
    {

    }

    public class RespItemCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO : Response
    {
        public CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO Item { get; set; }
    }

    public class RespListCentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO : Response
    {
        public List<CentroEntrenamiento_Presencial_HorarioClasesAsistenciasDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
