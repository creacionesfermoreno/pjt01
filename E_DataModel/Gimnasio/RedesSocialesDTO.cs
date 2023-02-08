
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class RedesSocialesDTO : AuditoriaDTO
	{
	
		
		public string UrlYoutube { get; set; }
	
		
		public string UrlFacebook { get; set; }
	
		
		public string UrlTwitter { get; set; }
	
		
		public string UrlInstagram { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqRedesSocialesDTO : Request
	{
		
        public List<RedesSocialesDTO> List { get; set; }
	}
	
	
    public class ReqFilterRedesSocialesDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public RedesSocialesDTO Item { get; set; }
        
        public Common.filterCaseRedesSociales FilterCase { get; set; }
    }
	
	
    public class RespRedesSocialesDTO : Response
    {
      
    }
	
	
    public class RespItemRedesSocialesDTO : Response
    {
        
        public RedesSocialesDTO Item { get; set; } 
    }

    
    public class RespListRedesSocialesDTO : Response
    {
        
        public List<RedesSocialesDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	