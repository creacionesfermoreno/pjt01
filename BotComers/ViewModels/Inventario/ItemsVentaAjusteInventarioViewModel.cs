using E_DataModel;
using System;
using System.Collections.Generic;


namespace BotComers.ViewModels.Inventario
{
    public class ItemsVentaAjusteInventarioViewModel
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int CodigoItemsVentaAjusteInventario { get; set; }
        public int CodigoAlmacen { get; set; }
        public string DesAlmacen { get; set; }
        public DateTime FechaAjuste { get; set; }
        public string DesFechaAjuste { get; set; }
        public string Observaciones { get; set; }
        public Decimal TotalAjuste { get; set; }
        public int Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Accion { get; set; }
        public int PageNumber { get; set; }
        public List<ItemsVentaAjusteInventarioDetalleDTO> listaDetalle { get; set; }

        public DateTime? b_FechaAjusteInicio { get; set; }
        public DateTime? b_FechaAjusteFin { get; set; }
    }
}