
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class IngresoAjustesCajaDTO : AuditoriaDTO
	{
	
		
		public int CodigoIAc { get; set; }
	
		
		public int CodigoAbrirCaja { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
		public decimal Cantidad { get; set; }
	
		
		public DateTime Fecha { get; set; }
	
	
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqIngresoAjustesCajaDTO : Request
	{
		
        public List<IngresoAjustesCajaDTO> List { get; set; }
	}
	
	
    public class ReqFilterIngresoAjustesCajaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public IngresoAjustesCajaDTO Item { get; set; }
        
        public Common.filterCaseIngresoAjustesCaja FilterCase { get; set; }
    }
	
	
    public class RespIngresoAjustesCajaDTO : Response
    {
      
    }
	
	
    public class RespItemIngresoAjustesCajaDTO : Response
    {
        
        public IngresoAjustesCajaDTO Item { get; set; } 
    }

    
    public class RespListIngresoAjustesCajaDTO : Response
    {
        
        public List<IngresoAjustesCajaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	