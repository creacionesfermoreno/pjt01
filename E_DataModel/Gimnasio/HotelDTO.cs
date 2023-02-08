
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class HotelDTO : AuditoriaDTO
	{
	
		public int CodigoHotel { get; set; }	
		
		public string Numero { get; set; }	
		
		public string Descripcion { get; set; }	
		
		public string Vigencia { get; set; }      
		
        public Common.Operation Operation { get; set; } 
	}
	
	
	public class ReqHotelDTO : Request
	{
		
        public List<HotelDTO> List { get; set; }
	}
	
	
    public class ReqFilterHotelDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public HotelDTO Item { get; set; }
        //
        //public Common.filterCaseHotel FilterCase { get; set; }
    }
	
	
    public class RespHotelDTO : Response
    {
      
    }
	
	
    public class RespItemHotelDTO : Response
    {
        
        public HotelDTO Item { get; set; } 
    }

    
    public class RespListHotelDTO : Response
    {
        
        public List<HotelDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	