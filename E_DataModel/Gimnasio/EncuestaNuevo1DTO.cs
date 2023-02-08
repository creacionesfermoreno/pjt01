
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class EncuestaNuevo1DTO : AuditoriaDTO
	{

        
        public int TK_ID { get; set; }

        
        public string TK_Latitude { get; set; }

        
        public string TK_Longitude { get; set; }
	
		
		public int CodigoEncuestaNuevo1 { get; set; }

        
        public string DescripcioObjetivo { get; set; }

        
        public string DescripcioComoConocioGym{ get; set; }

        
        public string DescripcionInteres { get; set; }

        
        public Decimal porTotalInteres { get; set; }

        
        public Decimal porTotalComoConocioGym { get; set; }

        
        public Decimal porTotalObjetivo { get; set; }

        
        public DateTime fehaInicio { get; set; }

        
        public DateTime fehaFin { get; set; }

        
		public int CodigoEncuestaNuevo2 { get; set; }

        public int CodigoOrigenProspecto { get; set; }
        public int CodigoProspecto { get; set; }
	
		
		public int CodigoObjetivo { get; set; }
	
		
		public int CodigoComoConocioGym { get; set; }


		
		public int CodigoInteres { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }

        
        public List<EncuestaNuevo1DTO> ListaDetalle_E { get; set; }
    }
	
	
	public class ReqEncuestaNuevo1DTO : Request
	{
		
        public List<EncuestaNuevo1DTO> List { get; set; }
	}
	
	
    public class ReqFilterEncuestaNuevo1DTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public EncuestaNuevo1DTO Item { get; set; }
        
        public Common.filterCaseEncuestaNuevo1 FilterCase { get; set; }
    }
	
	
    public class RespEncuestaNuevo1DTO : Response
    {
      
    }
	
	
    public class RespItemEncuestaNuevo1DTO : Response
    {
        
        public EncuestaNuevo1DTO Item { get; set; } 
    }

    
    public class RespListEncuestaNuevo1DTO : Response
    {
        
        public List<EncuestaNuevo1DTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	