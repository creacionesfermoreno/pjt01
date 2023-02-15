using AppsfitWebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class RequestBody
    {
        [Required]
        public string DefaultKeyEmpresa { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoUnidadNegocio { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoSede { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email No es válida")]
        public string Email { get; set; }

    }

    public class RequestBodyFilterProduct
    {
        public int CodigoUnidadNegocio { get; set; }
        public int CodigoSede { get; set; }
        public int CodigoMenu { get; set; }
    }

    //******************************************** PAYPAL ***********************************************

    public class RequestOrder : RequestBody
    {
        [Required]
        public string CodigoPlantillaFormaPago { get; set; }
        public MembresiaAPI membresia { get; set; }
    }
    
    public class RequestCapture : RequestBody
    {
        [Required]
        public string OrderId { get; set; }
        public string Runway { get; set; }

        [Required]
        public string CodigoPlantillaFormaPago { get; set; }

        [Required]
        public int CodigoSocio { get; set; }

        public MembresiaAPI membresia { get; set; }
        public SaleAPI sale { get; set; }
    }
    //******************************************** PAYPAL ***********************************************
}