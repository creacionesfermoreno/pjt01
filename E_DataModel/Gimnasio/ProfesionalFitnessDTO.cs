
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
using E_DataModel.CentroEntrenamiento;

namespace E_DataModel.Gimnasio
{

    public class ProfesionalFitnessDTO : AuditoriaDTO
    {
        
        public string CodigoProfesional { get; set; }

        
        public int CodigoTipoProfesional { get; set; }

        
        public string Nombres { get; set; }

        
        public string Apellidos { get; set; }

        
        public string NombreCompleto { get; set; }

        
        public int TipoDocumento { get; set; }

        
        public string DNI { get; set; }

        
        public string Telefono { get; set; }

        
        public string Celular { get; set; }

        
        public string Correo { get; set; }

        
        public DateTime FechaNacimiento { get; set; }

        
        public string ImagenUrl { get; set; }

        
        public string Genero { get; set; }

        
        public int EstadoCivil { get; set; }

        
        public string Facebook { get; set; }

        
        public string Ubicaciones { get; set; }

        
        public string Direccion { get; set; }

        
        public string Distrito { get; set; }

        
        public bool Estado { get; set; }

        
        public decimal CostoPorHora { get; set; }

        
        public decimal DstoPorMinuto { get; set; }

        
        public ProfesionalFitnessPagos ProfesionalFitnessPago { get; set; }

        
        public List<CentroEntrenamiento_Presencial_HorarioClasesConfiguracionDTO> ListaHorarioClases { get; set; }

        
        public Common.Operation Operation { get; set; }

        
        public string Xml { get; set; }

        
        public class ProfesionalFitnessPagos:AuditoriaDTO
        {                     
            
            public string CodigoProfesional { get; set; }
            
            public decimal CostoPorHora { get; set; }
            
            public decimal DstoPorMinuto { get; set; }
            
            public Common.Operation Operacion { get; set; }
        }
    }

    
    public class ProfesionalFitnessAsistenciaClasesDTO : ProfesionalFitnessDTO
    {        
        public string Disciplina { get; set; }
        
        public int TotalAlumnos { get; set; }
        
        public DateTime FechaHoraIngreso { get; set; }
        
        public int Tardanza { get; set; }
    }

    
    public class ReqProfesionalFitnessDTO : Request
    {
        
        public List<ProfesionalFitnessDTO> List { get; set; }
    }

    
    public class ReqFilterProfesionalFitnessDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ProfesionalFitnessDTO Item { get; set; }
        
        public Common.filterCaseProfesionalFitness FilterCase { get; set; }
    }

    
    public class RespProfesionalFitnessDTO : Response
    {

    }

    
    public class RespItemProfesionalFitnessDTO : Response
    {
        
        public ProfesionalFitnessDTO Item { get; set; }
    }

    
    public class RespListProfesionalFitnessDTO : Response
    {
        
        public List<ProfesionalFitnessDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }


}
