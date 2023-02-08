using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using E_DataModel.Base;

namespace E_DataModel.Gimnasio
{
    
    public class UbicacionesDTO
    {
        
        public int CodigoUbicaciones { get; set; }
        
        public string Ubicaciones { get; set; }
        
        public string Pais { get; set; }
        
        public string Departamento { get; set; }
        
        public string Provincia { get; set; }
        
        public string Distrito { get; set; }
        
        public string Longitud { get; set; }
        
        public string Latitud { get; set; }
        
        public string Buscador { get; set; }

        
        public int Tipo { get; set; }
    }

    
    public class ReqUbicacionesDTO : Request
    {
        
        public List<UbicacionesDTO> List { get; set; }
    }

    
    public class ReqFilterUbicacionesDTO : Request
    {
        
        public Common.Paging Paging { get; set; }
        
        public UbicacionesDTO Item { get; set; }
        
        public Common.filterCaseUbicaciones FilterCase { get; set; }
    }

    
    public class RespUbicacionesDTO : Response
    {

    }

    
    public class RespItemUbicacionesDTO : Response
    {
        
        public UbicacionesDTO Item { get; set; }
    }

    
    public class RespListUbicacionesDTO : Response
    {
        
        public List<UbicacionesDTO> List { get; set; }
        
        public Common.Paging Paging { get; set; }
    }

}
