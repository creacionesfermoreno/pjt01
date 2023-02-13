using AppsfitWebApi.Models;
using E_DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.ViewModels
{
    public class ComprobanteRequestModel
    {
        [StringLength(100), Required]
        public string DefaultKeyEmpresa { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoUnidadNegocio { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoSede { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoAlmacen { get; set; }

        [Range(1, float.MaxValue)]
        public decimal Total { get; set; }

        [Required]
        public int Estado { get; set; }

        [StringLength(15), Required]
        public string NroIdentificacion { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email No es válida")]
        public string Email { get; set; }


        public List<ComprobanteDetalleRequestAPI> listaDetalle { get; set; }
        public CardAPI card { get; set; }
    }

    public class ComprobanteDetalleRequestAPI
    {
        
        public int CodigoAlmacen { get; set; }
        [Range(1, int.MaxValue)]
        public int CodigoItemVenta { get; set; }

        [Range(1, float.MaxValue)]
        public Decimal Precio { get; set; }

        [Range(0, float.MaxValue)]
        public Decimal Descuento { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoTipoImpuesto { get; set; }


        [Required , StringLength(100)]
        public string Descripcion { get; set; }

        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }

        [Range(1, float.MaxValue)]
        public Decimal Total { get; set; }

    }

   



}