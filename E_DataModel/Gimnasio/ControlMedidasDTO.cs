
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class ControlMedidasDTO : AuditoriaDTO
	{
		
		public int CodigoSocio { get; set; }

        
		public int CodigoObjetivo { get; set; }

        
		public int CantidadRegistros { get; set; }

		
		public int CodigoCliente { get; set; }
	
		
		public int CodigoMedida { get; set; }

        
        public string Buscador { get; set; }
        
        public int Estado { get; set; }
        
        public DateTime FechaInicio_Filtro { get; set; }

        
        public DateTime FechaFin_Filtro { get; set; }


        
		public DateTime FechaVencimiento { get; set; }
	
		
		public DateTime TiempoMedicion { get; set; }
	
		
		public string AntecedentesMedicos { get; set; }
	
		
		public string Observacion { get; set; }
	
		
		public string ExpEntrenamiento { get; set; }
	
		
		public int Edad { get; set; }
	
		
		public decimal Estatura { get; set; }
	
		
		public decimal PesoCorporal { get; set; }
	
		
		public decimal PesoGraso { get; set; }
	
		
		public decimal PorcentajeGrasa { get; set; }
	    
		public string PorcentajeAgua { get; set; }
		
		public decimal IMC { get; set; }
	
		
		public decimal Cuello { get; set; }
	
		
		public decimal CirdelMom { get; set; }
	
		
		public decimal CirdelTorax { get; set; }
	
		
		public decimal Cintura { get; set; }
	    
		public decimal GrasaVisceral { get; set; }
		
		public decimal CadA { get; set; }
	
		
		public decimal CadB { get; set; }
	
		
		public decimal MusloSuperior { get; set; }
	
		
		public decimal MusloBajo { get; set; }
	
		
		public decimal Pantorrilla { get; set; }
	
		
		public decimal BrazoNormal { get; set; }
	
		
		public decimal BrazoFlexionado { get; set; }
	
		
		public decimal AntreBrazo { get; set; }
	
		
		public decimal Munieca { get; set; }
        
	    public decimal Gluteos { get; set; }
		
		public string Comentario { get; set; }

        
        public int NumeroRegistros { get; set; }

        
        public int TamanioPagina { get; set; }

        
        public string strTiempoMedicion { get; set; }

        
        public string strFechaIngreso { get; set; }

        
        public string strFechaVencimiento { get; set; }

        
        public string Nombres_Cliente { get; set; }

        
        public string Apellidos_Cliente { get; set; }

        
        public string Genero_Cliente { get; set; }

        
        public string DNI_Cliente { get; set; }

        
        public string TiempoMembresia_Cliente { get; set; }

        
        public int Seguimiento_Cliente { get; set; }

        
        public string Telefono_Cliente { get; set; }

        
        public string Celular_Cliente { get; set; }

        
        public int EdadRango1_Cliente { get; set; }

        
        public int EdadRango2_Cliente { get; set; }

        
        public DateTime FechaInicio_Cliente { get; set; }

        
        public DateTime FechaFin_Cliente { get; set; }

        
        public string EstadoCelular { get; set; }

        
        public string Correo_Cliente { get; set; }

        
        public string Nombres { get; set; }

        
        public string Apellidos { get; set; }

        
        public string Celular { get; set; }
        
        
        public string Correo { get; set; }
        
        
        public Common.Operation Operation { get; set; } 
        
        public string Xml { get; set; }
	}
	
	
	public class ReqControlMedidasDTO : Request
	{
		
        public List<ControlMedidasDTO> List { get; set; }
	}
	
	
    public class ReqFilterControlMedidasDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ControlMedidasDTO Item { get; set; }
        
        public Common.filterCaseControlMedidas FilterCase { get; set; }
    }
	
	
    public class RespControlMedidasDTO : Response
    {
      
    }
	
	
    public class RespItemControlMedidasDTO : Response
    {
        
        public ControlMedidasDTO Item { get; set; } 
    }

    
    public class RespListControlMedidasDTO : Response
    {
        
        public List<ControlMedidasDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	