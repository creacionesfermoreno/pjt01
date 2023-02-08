
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{	
	public class UsuariosIngresosDTO : AuditoriaDTO
	{
	
		public int CodigoIngreso { get; set; }
	
		
		public int CodigoUsuario { get; set; }
	
		
		public string NombreCompleto { get; set; }

        
        public string Operacion { get; set; }

        
        public string DescripcionTabla { get; set; }

        
        public string Descripcion { get; set; }

        
        public int CodigoValidacion { get; set; }

        
        public string Mensaje { get; set; }

        
		public string Password { get; set; }
	
		
		public string Latitud { get; set; }
	
		
		public string Longitud { get; set; }
	
		
		public DateTime FechaHora { get; set; }
	
		
		public string IPPublica { get; set; }
	
		
		public string IPPrivada { get; set; }
	
		
		public string NombrePC { get; set; }
	
		
		public int Estado { get; set; }

        public int flag { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqUsuariosIngresosDTO : Request
	{
		
        public List<UsuariosIngresosDTO> List { get; set; }
	}
	
	
    public class ReqFilterUsuariosIngresosDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public UsuariosIngresosDTO Item { get; set; }
        
        public Common.filterCaseUsuariosIngresos FilterCase { get; set; }
    }
	
	
    public class RespUsuariosIngresosDTO : Response
    {
      
    }
	
	
    public class RespItemUsuariosIngresosDTO : Response
    {
        
        public UsuariosIngresosDTO Item { get; set; } 
    }

    
    public class RespListUsuariosIngresosDTO : Response
    {
        
        public List<UsuariosIngresosDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	