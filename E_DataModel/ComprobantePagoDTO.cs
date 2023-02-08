using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_DataModel.Base;

namespace E_DataModel
{
    public class ComprobantePagoDTO:AuditoriaDTO
    {      
        public int CodigoComprobantePago { get; set; }
        public int CodigoComprobante     { get; set; }
        public int CodigoComprobanteDetalle { get; set; }
        public int CodigoCuentaBancaria  { get; set; }
        public int CodigoMetodoPago      { get; set; }
        public Decimal TipoMoneda            { get; set; }
        public Decimal Monto                 { get; set; }
        public string Nota                  { get; set; }
        public int Estado { get; set; }
        public string Accion { get; set; }
        public Common.Operation Operation { get; set; }
        public Decimal Total { get; set; }
        public DateTime request_FechaInicio { get; set; }
        public DateTime request_Fin { get; set; }
        public string request_Vendedor { get; set; }
        public string request_Counter { get; set; }
        public int request_Tipo { get; set; }
        public int request_Turno { get; set; }
    }

    public class ReqComprobantePagoDTO : Request
    {
        public List<ComprobantePagoDTO> List { get; set; }
    }

    public class ReqFilterComprobantePagoDTO : Request
    {
        public Common.Paging Paging { get; set; }
        public ComprobantePagoDTO Item { get; set; }
        public Common.filterCaseComprobantePago FilterCase { get; set; }
    }

    public class RespComprobantePagoDTO : Response
    {

    }

    public class RespItemComprobantePagoDTO : Response
    {
        public ComprobantePagoDTO Item { get; set; }
    }

    public class RespListComprobantePagoDTO : Response
    {
        public List<ComprobantePagoDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    }

}
