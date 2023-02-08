using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_DataModel
{
    public class FEDetalleDTO : AuditoriaDTO
    {
        public Int64 IdComprobanteDetalle { get; set; }
        public Int64 IdComprobanteCabecera { get; set; }
        public string CodigoProducto { get; set; }
        public string UnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        public decimal IGV { get; set; }
        public decimal Total { get; set; }

        public Common.Operation Operation { get; set; }
    }


    public class ReqFEDetalleDTO : Request //Peticion de un CRUD
    {
        public List<FEDetalleDTO> List { get; set; }
    }

    public class ReqFilterFEDetalleDTO : Request //Peticion de un List o Items
    {
        public Common.Paging Paging { get; set; }
        public FEDetalleDTO Item { get; set; }
        public Common.filterCaseComprobante FilterCase { get; set; }
    }

    public class RespFEDetalleDTO : Response //respuesta de un CRUD
    {

    }

    public class RespItemFEDetalleDTO : Response
    {
        public FEDetalleDTO Item { get; set; }
    } //respuesta de un ITEM

    public class RespListFEDetalleDTO : Response
    {
        public List<FEDetalleDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    } //respuesta de un LIST
}
