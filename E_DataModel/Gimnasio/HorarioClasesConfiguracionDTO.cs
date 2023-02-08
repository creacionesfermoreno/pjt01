using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace E_DataModel.Gimnasio
{

    public class HorarioClasesConfiguracionDTO : AuditoriaDTO
    {
        
        public string CodigoHorarioClasesConfiguracion { get; set; }
        
        public string Disciplina { get; set; }
        
        public string CodigoProfesional { get; set; }
        
        public int CodigoSalaHorario { get; set; }
        
        public int CapacidadPermitida { get; set; }
        
        public decimal CostoPorClase { get; set; }
        
        public decimal DescuentoPorMinuto { get; set; }
        
        public int CodigoSala { get; set; }
        
        public string DiaNombre { get; set; }
        
        public int DiaNumero { get; set; }
        
        public DateTime HoraInicio { get; set; }
        
        public DateTime HoraFin { get; set; }
        
        public string HoraInicioTexto
        {
            get
            {
                return HoraInicio.ToString("HH:mm:ss");
            }
        }
        
        public string HoraFinTexto
        {
            get
            {
                return HoraFin.ToString("HH:mm:ss");
            }
        }

        
        public string NombreProfesionalFitness { get; set; }
        
        public string PhotoProfesionalFitness { get; set; }
        
        public string DNIProfesionalFitness { get; set; }
        
        public bool Estado { get; set; }
        
        public string DescripcionEstado { get; set; }
        
        public ProfesionalFitnessDTO ProfesionalFitness { get; set; }
        
        public string Color { get; set; }

        
        public string Celular { get; set; }

        
        public string Correo { get; set; }

        
        public string Direccion { get; set; }

        
        public DateTime FechaNacimiento { get; set; }

        
        public string DesSala { get; set; }

        
        public string DesDia { get; set; }

        
        public Common.Operation Operation { get; set; }

        
        public string Xml { get; set; }
    }

    
    public class ReqHorarioClasesConfiguracionDTO : Request
    {
        
        public List<HorarioClasesConfiguracionDTO> List { get; set; }
    }

    
    public class ReqFilterHorarioClasesConfiguracionDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HorarioClasesConfiguracionDTO Item { get; set; }
        
        public Common.filterCaseHorarioClasesConfiguracion FilterCase { get; set; }
    }

    
    public class RespHorarioClasesConfiguracionDTO : Response
    {

    }

    
    public class RespItemHorarioClasesConfiguracionDTO : Response
    {
        
        public HorarioClasesConfiguracionDTO Item { get; set; }
    }

    
    public class RespListHorarioClasesConfiguracionDTO : Response
    {
        
        public List<HorarioClasesConfiguracionDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
}
