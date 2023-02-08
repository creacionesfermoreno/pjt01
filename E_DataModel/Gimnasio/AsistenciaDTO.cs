using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{	
	public class AsistenciaDTO : AuditoriaDTO
	{
        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }
                
		
		public int CodigoAsistencia { get; set; }
        
        
        public string Correo { get; set; }
        
        public string EstadoCelular { get; set; }
        public string Celular { get; set; }
        
        
        public string Telefono { get; set; }
        
        
        public string Nombres { get; set; }

        
        public string ImagenUrl { get; set; }

        public string DesTipoIngreso { get; set; }
        public string DesTipoPaquete { get; set; }
        public string desTiempoPaquete { get; set; }

        
        public decimal MontoTotal { get; set; }

        
        public decimal Costo { get; set; }

        
        public string Vendedor { get; set; }

        
        public string DiaSemana { get; set; }

        
        public int CodigoSocio { get; set; }

        
        public int totalDias { get; set; }

        
        public int CantDiasTomaFrezzeng { get; set; }

        
        public int DiasEfectivo { get; set; }

        
        public int DiasAsistidos { get; set; }

        
        public int DiasFaltantes { get; set; }

        
        public decimal desPorAsistido { get; set; }

        
        public decimal desPorfaltante { get; set; }

        
        public string desNomEstado { get; set; }

        
        public int CodigoPersona { get; set; }
        
        
        public int DiasAtras { get; set; }
       
        
        public int codigoSocio { get; set; }

        
        public int CantMujeres { get; set; }

        
        public int CantHombres { get; set; }

        
        public int CantTotal { get; set; }

        
        public int TamanioPagina { get; set; }

        //codigoPaquete
        
        public int CodigoMembresia { get; set; }

        
        public int CodigoMembresiaReal { get; set; }

        
        public int CodigoPaquete { get; set; }

        
        public string NombresPersona { get; set; }
        
        
        public string Descripcion { get; set; }
        
        
        public string FechaInicio { get; set; }

        
        public string imagenUrl { get; set; }

        
        public string DNI { get; set; }

		
		public string TipoPersona { get; set; }

        
        public string desSede { get; set; }

        
        public string DesTipoPersona { get; set; }

        
        public DateTime FechaIngreso { get; set; }

        
        public string Genero { get; set; }

        
        public DateTime FechaFinalizo { get; set; }

        
        public DateTime HoraInicioAsistencia { get; set; }

        
        public DateTime HoraFinAsistencia { get; set; }

        
        public string Sexo { get; set; }

        
        public string FechaFin { get; set; }
                
        
        public string Huella { get; set; }

        
        public string strFechaIngreso { get; set; }

        
        public byte[] Foto { get; set; }

        
        public decimal Debe { get; set; }

        
        public int fila { get; set; }

        
        public string colorEstado { get; set; }

        
        public string DesCalificacion { get; set; }

        public string flagVistaImagenAsistioReserva { get; set; }

        public Common.Operation Operation { get; set; } 

        public string CodigoHorarioClasesConfiguracionAsistencias { get; set; }
        
        public string Xml { get; set; }
	}

    public class ReporteAsistencia
    {

        
        public string NombresPersona { get; set; }
        
        public DateTime FechaIngreso { get; set; }
        
        public decimal Debe { get; set; }

       
    }
	
	
	public class ReqAsistenciaDTO : Request
	{
		
        public List<AsistenciaDTO> List { get; set; }
	}
	
	
    public class ReqFilterAsistenciaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public AsistenciaDTO Item { get; set; }
        
        public Common.filterCaseAsistencia FilterCase { get; set; }
    }
	
	
    public class RespAsistenciaDTO : Response
    {
      
    }
	
	
    public class RespItemAsistenciaDTO : Response
    {
        
        public AsistenciaDTO Item { get; set; } 
    }

    
    public class RespListAsistenciaDTO : Response
    {
        
        public List<AsistenciaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	