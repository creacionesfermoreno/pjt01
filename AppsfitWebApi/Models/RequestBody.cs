using AppsfitWebApi.Helpers;
using AppsfitWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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




    public class RequestProductCapturePaypal : RequestBody
    {
        [Required]
        public int CodigoAlmacen { get; set; }

        [Required]
        public string NroIdentificacion { get; set; }

        [Range(0, float.MaxValue)]
        public decimal Total { get; set; }

        [Required]
        public int Estado { get; set; }
        [Required]
        public string CodigoPlantillaFormaPago { get; set; }
        [Required]
        public string OrderId { get; set; }

        public List<ComprobanteDetalleRequestAPI> listaDetalle { get; set; }

    }

    public class RequestProductOrderPaypal
    {

        [Required]
        public string DefaultKeyEmpresa { get; set; }
        [Required]
        public string CodigoPlantillaFormaPago { get; set; }

        public List<ComprobanteDetalleRequestAPI> listaDetalle { get; set; }

    }


    //******************************************** PAYPAL ***********************************************


    //************************************** MPAGO **********************************************************

    public class RequestMPagoPref : RequestBody
    {

        [Required]
        public string CodigoPlantillaFormaPago { get; set; }
        [Required]
        public int CodigoSocio { get; set; }

        [Required]
        public MembresiaAPI membresia { get; set; }

        [Required]
        public ReqPayer payer { get; set; }

    } 
    
    public class RequestMPagoPayment : RequestBody
    {

        [Required]
        public string CodigoPlantillaFormaPago { get; set; }
        [Required]
        public int CodigoSocio { get; set; }

        [Required]
        public string PaymentId { get; set; }

        [Required]
        public string CodeRef { get; set; }

        [Required]
        public MembresiaAPI membresia { get; set; }

        [Required]
        public ReqPayer payer { get; set; }


    }

    public class ReqPayer
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }

        public string Phone { get; set; }
        [Required]
        public string Type_doc { get; set; }
        [Required]
        public string Number_doc { get; set; }
        public string Address { get; set; }
    }

    public class ReqMPRefProduct:RequestBody
    {

        [Required]
        public string CodigoPlantillaFormaPago { get; set; }

        [Required]
        public List<ComprobanteDetalleRequestAPI> listaDetalle { get; set; }

        [Required]
        public ReqPayer payer {get;set; }
    }

    public class ReqProductMP : RequestBody
    {
        [Required]
        public int CodigoAlmacen { get; set; }
        [Required]
        public int Estado { get; set; }
        [Required]
        public string CodigoPlantillaFormaPago { get; set; }
        [Required]
        public string PaymentId { get; set; }
        [Required]
        public string CodeRef { get; set; }
        [Required]
        public List<ComprobanteDetalleRequestAPI> listaDetalle { get; set; }
        [Required]
        public ReqPayer payer { get; set; }

    }

    //************************************** MPAGO **********************************************************

    public class ReqSuscriptionCulqi: RequestBody
    {

        [Required]
        public string DefaultKeyUser { get; set; }     
        [Required]
        public string CodigoPlantillaFormaPago { get; set; }
        [Required]
        public string PlanId { get; set; }
        [Required]
        public int CodigoSocio { get; set; }
        [Required]
        public string NroDoc { get; set; }

        [Required]
        public CardAPI card { get; set; }

        [Required]
        public CustomerCulqi customer { get; set; }
        [Required]
        public MembresiaAPI membresia { get; set; }
        [Required]
        public SaleAPI sale { get; set; }
    }

    public class CustomerCulqi
    {
        [Required]
        public string first_name { get; set; }
        [Required]
        public string last_name { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string address_city { get; set; }
        [Required]
        public string country_code { get; set; }
        [Required]
        public string phone_number { get; set; }
        public string email { get; set; }
    }


    public class SimplePaymentsSuscrption
    {
        [Required]
        public int CodigoUnidadNegocio { get; set; }
        [Required]
        public int CodigoSede { get; set; }
        [Required]
        public int CodigoPaquete { get; set; }
       
    }



}