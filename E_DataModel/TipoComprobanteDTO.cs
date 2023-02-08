using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class TipoComprobanteDTO : AuditoriaDTO
    {     
        public int CodigoTipoComprobante { get; set; }
        public int CodigoTipoDocumentoEmpresa { get; set; }
        public string Serie { get; set; }
        public string Nombre { get; set; }
    }



    public class ReqTipoComprobanteDTO : Request
    {
        public List<TipoComprobanteDTO> List { get; set; }
    }

    public class ReqFilterTipoComprobanteDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public TipoComprobanteDTO Item { get; set; }
        public Common.filterCaseTipoComprobante FilterCase { get; set; }
    }

    public class RespTipoComprobanteDTO : Response
    {

    }

    public class RespItemTipoComprobanteDTO : Response
    {
        public TipoComprobanteDTO Item { get; set; }
    }

    public class RespListTipoComprobanteDTO : Response
    {
        public List<TipoComprobanteDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
