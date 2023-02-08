using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace E_DataModel.Gimnasio
{    
    
    public class SalaHorarioDTO : AuditoriaDTO
    {
        public int? CodigoSalaHorario { get; set; }
        
        public string Disciplina { get; set; }
        
        public bool Estado { get; set; }
        
        public string DescripcionEstado { get; set; }
        
        public int? Capacidad { get; set; }
        
        public string Color { get; set; }
        
        public string StyleWidth { get; set; }
        
        public string StyleHeight { get; set; }
        
        public int Orden { get; set; }
        
        public int CodNroSala { get; set; }
        
        public Common.Operation Operation { get; set; }
        
        public string Xml { get; set; }
    }

    
    public class ReqSalaHorarioDTO : Request
    {
        
        public List<SalaHorarioDTO> List { get; set; }
    }

    
    public class ReqFilterSalaHorarioDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public SalaHorarioDTO Item { get; set; }
        
        public Common.filterCaseSalaHorario FilterCase { get; set; }
    }

    
    public class RespSalaHorarioDTO : Response
    {

    }

    
    public class RespItemSalaHorarioDTO : Response
    {
        
        public SalaHorarioDTO Item { get; set; }
    }

    
    public class RespListSalaHorarioDTO : Response
    {
        
        public List<SalaHorarioDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }


}
