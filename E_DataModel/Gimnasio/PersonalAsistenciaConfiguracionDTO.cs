
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{
      
    public class PersonalAsistenciaConfiguracionDTO : AuditoriaDTO
    {        
        public string CodigoPersonal { get; set; }
        public int CodigoCargo { get; set; }
        public string CodigoPersonalAsistenciaConfiguracion { get; set; }
                
        public DateTime? HoraIngreso_Lunes_Turno1     { get; set; } 
        public DateTime? HoraSalida_Lunes_Turno1      { get; set; } 
        public DateTime? HoraIngreso_Martes_Turno1    { get; set; } 
        public DateTime? HoraSalida_Martes_Turno1     { get; set; } 
        public DateTime? HoraIngreso_Miercoles_Turno1 { get; set; } 
        public DateTime? HoraSalida_Miercoles_Turno1  { get; set; } 
        public DateTime? HoraIngreso_Jueves_Turno1    { get; set; } 
        public DateTime? HoraSalida_Jueves_Turno1     { get; set; } 
        public DateTime? HoraIngreso_Viernes_Turno1   { get; set; } 
        public DateTime? HoraSalida_Viernes_Turno1    { get; set; } 
        public DateTime? HoraIngreso_Sabado_Turno1    { get; set; } 
        public DateTime? HoraSalida_Sabado_Turno1     { get; set; } 
        public DateTime? HoraIngreso_Domingo_Turno1   { get; set; } 
        public DateTime?  HoraSalida_Domingo_Turno1 { get; set; }   
	 
        public DateTime? HoraIngreso_Lunes_Turno2    { get; set; }
        public DateTime? HoraSalida_Lunes_Turno2     { get; set; }
        public DateTime? HoraIngreso_Martes_Turno2   { get; set; }
        public DateTime? HoraSalida_Martes_Turno2    { get; set; }
        public DateTime? HoraIngreso_Miercoles_Turno2 { get; set; }
        public DateTime? HoraSalida_Miercoles_Turno2 { get; set; }
        public DateTime? HoraIngreso_Jueves_Turno2   { get; set; }
        public DateTime? HoraSalida_Jueves_Turno2    { get; set; }
        public DateTime? HoraIngreso_Viernes_Turno2  { get; set; }
        public DateTime? HoraSalida_Viernes_Turno2   { get; set; }
        public DateTime? HoraIngreso_Sabado_Turno2   { get; set; }
        public DateTime? HoraSalida_Sabado_Turno2    { get; set; }
        public DateTime? HoraIngreso_Domingo_Turno2  { get; set; }
        public DateTime? HoraSalida_Domingo_Turno2 { get; set; }

        public decimal Sueldo { get; set; }
        public int MinutosRefrigerio { get; set; }
        public int MinutosTolerancia { get; set; }
        public decimal DescuentoXMinuto { get; set; }
        public bool Estado { get; set; }
        public Common.Operation Operation { get; set; }
        
        public string Xml { get; set; }
    }

    
    public class ReqPersonalAsistenciaConfiguracionDTO : Request
    {
        
        public List<PersonalAsistenciaConfiguracionDTO> List { get; set; }
    }

    
    public class ReqFilterPersonalAsistenciaConfiguracionDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PersonalAsistenciaConfiguracionDTO Item { get; set; }
        
        public Common.filterCasePersonalAsistenciaConfiguracion FilterCase { get; set; }
    }

    
    public class RespPersonalAsistenciaConfiguracionDTO : Response
    {

    }

    
    public class RespItemPersonalAsistenciaConfiguracionDTO : Response
    {
        
        public PersonalAsistenciaConfiguracionDTO Item { get; set; }
    }

    
    public class RespListPersonalAsistenciaConfiguracionDTO : Response
    {
        
        public List<PersonalAsistenciaConfiguracionDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }


}
