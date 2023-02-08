using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class AlmacenesDTO:AuditoriaDTO
    {
        public int CodigoAlmacen { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Observaciones { get; set; }
    }


    public class ReqAlmacenesDTO : Request
    {
        public List<AlmacenesDTO> List { get; set; }
    }

    public class ReqFilterAlmacenesDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public AlmacenesDTO Item { get; set; }
        public Common.filterCaseAlmacenes FilterCase { get; set; }
    }

    public class RespAlmacenesDTO : Response
    {

    }

    public class RespItemAlmacenesDTO : Response
    {
        public AlmacenesDTO Item { get; set; }
    }

    public class RespListAlmacenesDTO : Response
    {
        public List<AlmacenesDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }
}
