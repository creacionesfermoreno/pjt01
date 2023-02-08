using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace E_DataModel.Gimnasio
{
    
    public class SalaDTO : AuditoriaDTO
    {

        
        public int? Codigo { get; set; }
              
        public string Descripcion { get; set; }
        
        public string NroSala { get; set; }
        
        public string Color { get; set; }
        
        public string Estilo { get; set; }
        
        public int CantidadClases { get; set; }
        
        public int Orden { get; set; }
        
        public Common.Operation Operation { get; set; }
        
        public string Xml { get; set; }
    }

    
    public class ReqSalaDTO : Request
    {
        
        public List<SalaDTO> List { get; set; }
    }

    
    public class ReqFilterSalaDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public SalaDTO Item { get; set; }
        
        public Common.filterCaseSala FilterCase { get; set; }
    }

    
    public class RespSalaDTO : Response
    {

    }

    
    public class RespItemSalaDTO : Response
    {
        
        public SalaDTO Item { get; set; }
    }

    
    public class RespListSalaDTO : Response
    {
        
        public List<SalaDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
}
