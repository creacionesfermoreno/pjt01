using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{

	public class ContratoFolioDTO : AuditoriaDTO
	{
		public int codigo_Membresia { get; set; }

        
        public string AsesorComercial_Membresia { get; set; }

        
        public string fechaInscripcion_Membresia { get; set; }

        
		public string fechaInicio_Membresia { get; set; }

	    
		public string fechaFin_Membresia { get; set; }
	    
		public decimal costo_Membresia { get; set; }
	    
        public string nroContrato_Membresia { get; set; }
	    
		public int codigo_Socio { get; set; }
	    
		public string nombre_Socio { get; set; }
	    
		public string apellido_Socio { get; set; }
	    
        public string dni_Socio { get; set; }
	    
        public string telefono_Socio { get; set; }
	    
        public string celular_Socio { get; set; }
	    
        public string correo_Socio { get; set; }
	    
        public string fechaNacimiento_Socio { get; set; }
	    
        public string genero_Socio { get; set; }
	    
        public string facebook_Socio { get; set; }
	    
		public int referidoPor_Socio { get; set; }
	    
        public string direccion_Socio { get; set; }
	    
        public string distrito_Socio { get; set; }
	    
        public string ocupacion_Socio { get; set; }
	    
		public int tipo_Socio { get; set; }
	    
		public int codigo_Paquete { get; set; }
	    
        public string nombre_Paquete { get; set; }
	    
		public int valorDias_Paquete { get; set; }
	    
		public int diasFreezing_Paquete { get; set; }
        
        
        public int Edad { get; set; }

        
        public string Clausula { get; set; }
         
        
        public string Apoderado { get; set; }
        
        
        public string Apoderado_DNI { get; set; }

        
        public string observacionTraspaso { get; set; }

        
        public string fechaTraspaso { get; set; }

        
        public string responsableTraspaso { get; set; }

        
        public int Apoderado_Codigo { get; set; }

        
        public string Parentesco_Apoderado { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqContratoFolioDTO : Request
	{
		
        public List<ContratoFolioDTO> List { get; set; }
	}
	
	
    public class ReqFilterContratoFolioDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public ContratoFolioDTO Item { get; set; }
        
        public Common.filterCaseContratoFolioDTO FilterCase { get; set; }
    }
	
	
    public class RespContratoFolioDTO : Response
    {
      
    }
	
	
    public class RespItemContratoFolioDTO : Response
    {
        
        public ContratoFolioDTO Item { get; set; } 
    }

    
    public class RespListContratoFolioDTO : Response
    {
        
        public List<ContratoFolioDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	