using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{	
	public class PerfilMenuDTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }
     

        
		public int CodigoPerfil { get; set; }
	
		
		public int CodigoMenu { get; set; }

        
        public int CodigoMenuSuperior { get; set; }

        
        public string Descripcion { get; set; }

        
        public string Url { get; set; }

        
        public string Tipo { get; set; }

        
        public int Orden { get; set; }


        
        public int Estado { get; set; }

        
        public string deschecked { get; set; }

        
		public bool ControlTotal { get; set; }
	
		
		public bool Escritura { get; set; }
	
		
		public bool Lectura { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_MenuSuperior { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_Clientes { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_Agenda { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_CuadroVentas { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_MetasyBonos { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_Estadisticas { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_Inventario { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_NutricionMedidas { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_Patrimonios { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_PagoProfesores { get; set; }

        
        public List<PerfilMenuDTO> ListaDetalle_Ajustes { get; set; }


    }
	
	
	public class ReqPerfilMenuDTO : Request
	{
		
        public List<PerfilMenuDTO> List { get; set; }
	}
	
	
    public class ReqFilterPerfilMenuDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public PerfilMenuDTO Item { get; set; }
        
        public Common.filterCasePerfilMenu FilterCase { get; set; }
    }
	
	
    public class RespPerfilMenuDTO : Response
    {
      
    }
	
	
    public class RespItemPerfilMenuDTO : Response
    {
        
        public PerfilMenuDTO Item { get; set; } 
    }

    
    public class RespListPerfilMenuDTO : Response
    {
        
        public List<PerfilMenuDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	