
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{
    
    public class PersonalAsistenciaDTO : AuditoriaDTO
    {
        
        public int CodigoDisciplina { get; set; }
        
        public string CodigoProfesional { get; set; }
        
        public string CodigoPersonal { get; set; }
        
        public string NumeroDocumento { get; set; }
        
        public string NombreCompleto { get; set; }
        
        public int CodigoCargo { get; set; }
        
        public string DescripcionCargo { get; set; }
        
        public string CodigoPersonalAsistencia { get; set; }
        
        public string CodigoHorarioClases { get; set; }
        
        public DateTime? FechaHoraIngresoProgramada { get; set; }
        
        public DateTime? FechaHoraIngresoProgramada_TurnoTarde { get; set; }
        
        public int tipoTurno { get; set; }
        
        public string DesTipoTurno { get; set; }

        
        public DateTime? FechaHoraIngreso { get; set; }
        public string FechaHoraIngresoTexto { get; set; }
        
        public DateTime? FechaHoraSalida { get; set; }
        public string FechaHoraSalidaTexto { get; set; }

        public DateTime? FechaHoraRefrigerioSalida { get; set; }
        public string FechaHoraRefrigerioSalidaTexto { get; set; }

        public DateTime? FechaHoraRefrigerioRetorno { get; set; }
        public string FechaHoraRefrigerioRetornoTexto { get; set; }

        public DateTime? FechaHoraIngreso_TurnoTarde { get; set; }
        public string FechaHoraIngreso_TurnoTardeTexto { get; set; }

        public DateTime? FechaHoraSalida_TurnoTarde { get; set; }
        public string FechaHoraSalida_TurnoTardeTexto { get; set; }

        public DateTime? FechaHoraRefrigerioSalida_TurnoTarde { get; set; }
        public string FechaHoraRefrigerioSalida_TurnoTardeTexto { get; set; }

        public DateTime? FechaHoraRefrigerioRetorno_TurnoTarde { get; set; }
        public string FechaHoraRefrigerioRetorno_TurnoTardeTexto { get; set; }


        public int TardanzaMinutos { get; set; }
        
        public int TardanzaMinutos_TurnoTarde { get; set; }
        
        public string TardanzaTexto { get; set; }
        
        public string TardanzaTexto_TurnoTarde { get; set; }
        
        public int TotalTardanzaMinutos { get; set; }
        
        public string TotalTardanzaTexto { get; set; }
        
        public bool Estado { get; set; }
        
        public int TipoPersonal { get; set; }

        
        public ProfesionalFitnessDTO ProfesionalFitness { get; set; }
        
        public PersonalAdministrativoDTO PersonalAdministrativo { get; set; }
        
        public bool Eliminado { get; set; }
        
        public Common.Operation Operation { get; set; }
        
        public DateTime FechaInicio { get; set; }
        
        public DateTime FechaFin { get; set; }
        
        public bool EsMarcacionPorConfig { get; set; }
        
        public int OperacionMarcacion { get; set; }
        
        public string Xml { get; set; }

        public string CodigoHorarioClasesConfiguracion { get; set; }
        public string CodigoHorarioClasesTiempoReal { get; set; }
        public int TipoAsistencia { get; set; }
        public int DiaNumero { get; set; }       
    }

    
    public class PersonalFitnessAsistenciaDTO
    {
        
        public string NumeroDocumento { get; set; }
        
        public string NombreProfesionalFitness { get; set; }
        
        public string Disciplina { get; set; }
        
        public DateTime FechaHoraInicioClase { get; set; }
        
        public int TotalAlumnos { get; set; }
        
        public decimal CostoPorHora { get; set; }
        public decimal CostoPorClase { get; set; }
        
        public DateTime FechaHoraMarcacion { get; set; }

        public DateTime FechaHoraMarcacionIngreso { get; set; }
        public DateTime FechaHoraMarcacionSalida { get; set; }

        public string FechaHoraMarcacionIngreso_texto { get; set; }
        public string FechaHoraMarcacionSalida_texto { get; set; }


        public int Tardanza { get; set; }
        
        public decimal DescuentoPorTardanza { get; set; }
        
        public decimal TotalPago { get; set; }
    }

    
    public class PersonalAdministrativoAsistenciaResumentDTO
    {
        
        public string CodigoPersonal { get; set; }
        
        public string Nombres { get; set; }
        
        public string Apellidos { get; set; }
        
        public string NombreCompleto { get; set; }
        
        public string DNI { get; set; }
        
        public string DescripcionCargo { get; set; }
        
        public int MinutosTrabajados { get; set; }
        
        public int MinutosTardanza { get; set; }
        
        public int MinutosRefrigerio { get; set; }
        
        public int HorasTrabajadas { get; set; }
        
        public string HorasTrabajadasText { get; set; }
        
        public int DiasTrabajados { get; set; }
        
        public decimal Sueldo { get; set; }
        
        public decimal DescuentoPorMinuto { get; set; }
        
        public decimal TotalDescuento { get; set; }
        
        public decimal TotalNeto { get; set; }
        
        public decimal SubTotal { get; set; }
    }
    
    
    public class ReqPersonalAsistenciaDTO : Request
    {
        
        public List<PersonalAsistenciaDTO> List { get; set; }
    }

    
    public class ReqFilterPersonalAsistenciaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PersonalAsistenciaDTO Item { get; set; }
        
        public Common.filterCasePersonalAsistencia FilterCase { get; set; }
    }

    
    public class RespPersonalAsistenciaDTO : Response
    {
        
        public List<PersonalAsistenciaDTO> List { get; set; }
    }

    
    public class RespItemPersonalAsistenciaDTO : Response
    {
        
        public PersonalAsistenciaDTO Item { get; set; }
    }

    
    public class RespListPersonalAsistenciaDTO : Response
    {
        
        public List<PersonalAsistenciaDTO> List { get; set; }
        
        public List<PersonalFitnessAsistenciaDTO> ListProfesores { get; set; }
        
        public List<PersonalAdministrativoAsistenciaResumentDTO> ListPersonalAdministrativoAsistencia { get; set; }
        
        public Common.Paging Paging { get; set; }
    }

}
