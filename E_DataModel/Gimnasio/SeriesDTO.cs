
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{
		
	public class SeriesDTO : AuditoriaDTO
	{       
        
        public int CodigoSerie { get; set; }
		
		public int TipoDocumento { get; set; }

        
        public string DesSubTipoDocumento { get; set; }
        
        
        public int SubTipoDocumento { get; set; }

        
        public string DesTipoDocumento { get; set; }

		
		public string NroSerie { get; set; }
	
		
		public string NroCorrelativoActual { get; set; }
	
		
		public string NroCorrelativoFinal { get; set; }
	    
		
		public bool Estado { get; set; }
        public bool chkGenerarSerie { get; set; }
        public bool chkGenerarComprobante { get; set; }

        
        public string DesEstado { get; set; }

        
        public int flag { get; set; }

        
        public int subFlag { get; set; }

        
        public int longitudSerie { get; set; }

		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqSeriesDTO : Request
	{
		
        public List<SeriesDTO> List { get; set; }
	}
	
	
    public class ReqFilterSeriesDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public SeriesDTO Item { get; set; }
        
        public Common.filterCaseSeries FilterCase { get; set; }
    }
	
	
    public class RespSeriesDTO : Response
    {
      
    }
	
	
    public class RespItemSeriesDTO : Response
    {
        
        public SeriesDTO Item { get; set; } 
    }

    
    public class RespListSeriesDTO : Response
    {
        
        public List<SeriesDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	