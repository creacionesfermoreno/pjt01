using E_DataModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_DataModel
{
 

    public class FECabeceraDTO : AuditoriaDTO
    {
public Int64 IdComprobanteCabecera { get; set; }
public Int64 IdTipoComprobante { get; set; }
        public string Serie { get; set; }
        public string NumeroComprobante { get; set; }
        public string TipoDocumentoCliente { get; set; }
        public string NumeroDocumentoCliente { get; set; }
        public string DenominacionCliente { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime  FechaVencimiento { get; set; }
        public int Moneda { get; set; }
        public decimal PorcentajeIGV { get; set; }
        public decimal TotalGravada { get; set; }
        public decimal TotalIGV { get; set; }
        public decimal Total { get; set; }
        public string FormaPago { get; set; }
        public List<FEDetalleDTO>  ListaDetalle{ get; set; }
    }


    public class ReqFECabeceraDTO : Request //Peticion de un CRUD
    {
        public List<FECabeceraDTO> List { get; set; }
    }

    public class ReqFilterFECabeceraeDTO : Request //Peticion de un List o Items
    {
        public Common.Paging Paging { get; set; }
        public FECabeceraDTO Item { get; set; }
        public Common.filterCaseFECabecera FilterCase { get; set; }
    }

    public class RespFECabeceraDTO : Response //respuesta de un CRUD
    {

    }

    public class RespItemFECabeceraDTO : Response
    {
        public FECabeceraDTO Item { get; set; }
    } //respuesta de un ITEM

    public class RespListFECabeceraDTO : Response
    {
        public List<FECabeceraDTO> List { get; set; }
        public Common.Paging Paging { get; set; }
    } //respuesta de un LIST
}
