
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{
    
    public class SociosFichaSaludMasterDTO : AuditoriaDTO
    {
        
        public int TK_ID { get; set; }
        
        public string TK_Latitude { get; set; }
        
        public string TK_Longitude { get; set; }
        
        public string CodigoMaster { get; set; }
        
        public string Siglas { get; set; }
        
        public string SiglasTitulo { get; set; }
        
        public string Descripcion { get; set; }
        
        public int valor { get; set; }
        
        public int Estado { get; set; }        

    }


    
    public class ReqSociosFichaSaludMasterDTO : Request
    {
        
        public List<SociosFichaSaludMasterDTO> List { get; set; }
    }

    
    public class ReqFilterSociosFichaSaludMasterDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public SociosFichaSaludMasterDTO Item { get; set; }
        
        public Common.filterCaseSociosFichaSaludMaster FilterCase { get; set; }
    }

    
    public class RespSociosFichaSaludMasterDTO : Response
    {

    }

    
    public class RespItemSociosFichaSaludMasterDTO : Response
    {
        
        public ContratoMensajeDTO Item { get; set; }
    }

    
    public class RespListSociosFichaSaludMasterDTO : Response
    {
        
        public List<SociosFichaSaludMasterDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }
}
