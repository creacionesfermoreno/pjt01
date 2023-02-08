using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{

	public class UsuarioDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }

        
        public int Codigo { get; set; }

		
		public int CodigoUsuario { get; set; }

        
		public int ValidacionExisteCita { get; set; }

        
		public int ValidarExisteVendedorActivo { get; set; }

        
        public int ValidacionUsuario { get; set; }

        
		public int flag { get; set; }

        
        public int CodigoTurno { get; set; }

        
         public DateTime Fecha { get; set; }

        public string filtro { get; set; }

        
        public string Usuario { get; set; }

        
        public int CodigoSocio { get; set; }

        
        public int CodigoTipoAgenda { get; set; }

        
        public string Vendedor { get; set; }

        
        public string Main { get; set; }

        
        public string Contrasenia { get; set; }

        
        public int CodigoPerfil { get; set; }

        
        public string DesPerfil { get; set; }
        
        
        public string Descripcion { get; set; }

        
        public string NombreCompleto { get; set; }
        
        
        public string DescripcionPerfil  { get; set; }
        
        
        public string NombreSede { get; set; }
        
        
        public string NombrePerfil { get; set; }
	
		
		public string Password { get; set; }


        
        public string PasswordNuevo { get; set; }


        
        public string PasswordAntiguo { get; set; }
	
		
        public string imagenUrl { get; set; }
	
		
		public string Mail { get; set; }
	
		
		public string Telefono { get; set; }

        
        public string Nombres { get; set; }

        
        public string Apellidos { get; set; }

        
        public string DNI { get; set; }

        
		public bool Estado { get; set; }

        
        public int EstadoComprobador { get; set; }

        
        public string DesEstado { get; set; }

        
        public char indMigracion { get; set; }

        
        public string CodigoUsuarioConfiguracion { get; set; }

        
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public string HuelaStr { get; set; }
	}
	
	
	public class ReqUsuarioDTO : Request
	{
		
        public List<UsuarioDTO> List { get; set; }
	}
	
	
    public class ReqFilterUsuarioDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public UsuarioDTO Item { get; set; }
        
        public Common.filterCaseUsuario FilterCase { get; set; }
    }
	
	
    public class RespUsuarioDTO : Response
    {
      
    }
	
	
    public class RespItemUsuarioDTO : Response
    {
        
        public UsuarioDTO Item { get; set; } 
    }

    
    public class RespListUsuarioDTO : Response
    {
        
        public List<UsuarioDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	