
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;
		
namespace E_DataModel.Gimnasio
{	
	public class SeriesContratoDTO : AuditoriaDTO
	{
		
		public int CodigoSerie { get; set; }
      
		public int TipoContrato { get; set; }

        
        public string DesTipoContrato { get; set; }

		
		public string NroSerie { get; set; }
	
		
		public string NroCorrelativoActual { get; set; }
	
		
		public string NroCorrelativoFinal { get; set; }
	
		
		public bool Estado { get; set; }

        
        public string DesEstado { get; set; }

        
        public int flag { get; set; }

        
        public int longitudSerie { get; set; }
	
	
		
        public Common.Operation Operation { get; set; } 

        
        public string Xml { get; set; }
	}
	
	
	public class ReqSeriesContratoDTO : Request
	{
		
        public List<SeriesContratoDTO> List { get; set; }
	}
	
	
    public class ReqFilterSeriesContratoDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public SeriesContratoDTO Item { get; set; }
        
        public Common.filterCaseSeriesContrato FilterCase { get; set; }
    }
	
	
    public class RespSeriesContratoDTO : Response
    {
      
    }
	
	
    public class RespItemSeriesContratoDTO : Response
    {
        
        public SeriesContratoDTO Item { get; set; } 
    }

    
    public class RespListSeriesContratoDTO : Response
    {
        
        public List<SeriesContratoDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
	
	
}
	