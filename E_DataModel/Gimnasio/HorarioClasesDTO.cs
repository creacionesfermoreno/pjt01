using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace E_DataModel.Gimnasio
{
 
    public class HorarioClasesDTO : AuditoriaDTO
    {
        
        public string CodigoHorarioClases { get; set; }

        public string CodigoHorarioClasesConfiguracion { get; set; }
      
        
        public string CodigoProfesional { get; set; }
        
        public int CodigoSalaHorario { get; set; }
        
        public int NroSala { get; set; }
        
        public string Disciplina { get; set; }
        
        public int CapacidadPermitida { get; set; }
        
        public string DiaNombre { get; set; }
        
        public int DiaNumero { get; set; }
        //
        //public string NombreProfesionalFitness { get; set; }
        
        public DateTime? FechaHoraInicio { get; set; }
        
        public DateTime? FechaHoraFin { get; set; }
        
        public int Estado { get; set; }
        
        public string DescripcionEstado { get; set; }
        
        public string CodigoPersonalAsistencia { get; set; }
        
        public DateTime? FechaHoraIngresoAsistencia { get; set; }
        
        public DateTime? FechaHoraSalidaAsistencia { get; set; }
        
        public Common.Operation Operation { get; set; }
        
        public string Xml { get; set; }
        
        public ProfesionalFitnessDTO ProfesionalDTO { get; set; }
        
        public SalaHorarioDTO SalaHorarioItem { get; set; }
        
        public List<HorarioClasesDetalleDTO> ListaDetalleHorario { get; set; }
    }

    
    public class ReqHorarioClasesDTO : Request
    {
        
        public List<HorarioClasesDTO> List { get; set; }
    }

    
    public class ReqFilterHorarioClasesDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HorarioClasesDTO Item { get; set; }
        
        public Common.filterCaseHorarioClases FilterCase { get; set; }
    }

    
    public class RespHorarioClasesDTO : Response
    {

    }

    
    public class RespItemHorarioClasesDTO : Response
    {
        
        public HorarioClasesDTO Item { get; set; }
    }

    
    public class RespListHorarioClasesDTO : Response
    {
        
        public List<HorarioClasesDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
}
