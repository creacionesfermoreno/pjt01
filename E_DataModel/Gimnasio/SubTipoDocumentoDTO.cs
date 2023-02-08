
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
	
	public class SubTipoDocumentoDTO : AuditoriaDTO
	{
		
		public int CodigoTipoDocumento { get; set; }

        
        public string DescripcionTipoDocumento { get; set; }

		
		public int Codigo { get; set; }
	
		
		public string Descripcion { get; set; }
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqSubTipoDocumentoDTO : Request
	{
		
        public List<SubTipoDocumentoDTO> List { get; set; }
	}
	
	
    public class ReqFilterSubTipoDocumentoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public SubTipoDocumentoDTO Item { get; set; }
        
        public Common.filterCaseSubTipoDocumento FilterCase { get; set; }
    }
	
	
    public class RespSubTipoDocumentoDTO : Response
    {
      
    }
	
	
    public class RespItemSubTipoDocumentoDTO : Response
    {
        
        public SubTipoDocumentoDTO Item { get; set; } 
    }

    
    public class RespListSubTipoDocumentoDTO : Response
    {
        
        public List<SubTipoDocumentoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	