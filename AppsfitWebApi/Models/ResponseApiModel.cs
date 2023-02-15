using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class ResponseApiModel
    {




    }
    public class ResponseApi
    {
        public string Message1 { set; get; }
        public string Message2 { set; get; }
        public string Message3 { set; get; }
        public int Status { set; get; }
        public bool Success { set; get; }
        public object Date { set; get; }
        public object Errors { set; get; }
        public int Total { set; get; }
    }

    public class ResponseCulqi
    {
        public string id { get; set; }
        public string merchant_message { get; set; }
        public string user_message { get; set; }
    }


    //**************************** card********************
    public class Metadata
    {
        public string dni { get; set; }
    }

    public class CardAPI
    {
        //ErrorMessage = "Enter number between 0 to 1000"

        [StringLength(16), Required]
        public string card_number { get; set; }

        [StringLength(3), Required]
        public string cvv { get; set; }

        [Range(1, 12)]
        public int expiration_month { get; set; }

        [StringLength(5), Required]
        public string expiration_year { get; set; }
        public string email { get; set; }
        public Metadata metadata { get; set; }


    }



    //************************************* charge **********************************

    public class AntifraudDetails
    {
        public string addres { get; set; }
        public string address_city { get; set; }
        public string country_code { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone_number { get; set; }
    }

    public class MetadataCharge
    {
        public string documentNumber { get; set; }
    }

    public class ChargeAPI
    {
        [Range(1, int.MaxValue)]
        public int amount { get; set; }
        public string currency_code { get; set; }
        public string email { get; set; }
        public string source_id { get; set; }
        public bool capture { get; set; }
        public AntifraudDetails antifraud_details { get; set; }
        public MetadataCharge metadata { get; set; }
    }

    public class MembresiaAPI
    {
        public string DefaultKeyEmpresa { get; set; }
        public int CodigoMembresia { get; set; }

        [Range(1, int.MaxValue), Required]
        public int CodigoPaquete { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2023", "1/1/2999")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2023", "1/1/2999")]
        public DateTime FechaFin { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2023", "1/1/2999")]
        public DateTime FechaFinMembresia { get; set; }

        [Range(1, float.MaxValue)]
        public decimal Costo { get; set; }

        [Range(0, int.MaxValue)]
        public int NroSessiones { get; set; }

        [Range(0, int.MaxValue)]
        public int FrezenDisponibles { get; set; }
        public string Observacion { get; set; }

        [Required]
        public string name { get; set; }
        public string Descripcion { get; set; }
        public int CodigoMebresiaOrigen { get; set; }
        public int TipoIngreso { get; set; }
        public int CodigoSocio { get; set; }


    }


    public class SaleAPI
    {

        [StringLength(100), Required]
        public string RazonSocial_Sr { get; set; }

        [StringLength(15), Required]
        public string RUC_DNI { get; set; }


        public string Direccion { get; set; }
        public string Comentario { get; set; }
        public string NroBoucher { get; set; }

    }



    //*******************************************************************Request****************
    public class RequestCharge
    {
        public MembresiaAPI membresia { get; set; }
        public SaleAPI sale { get; set; }
        public CardAPI card { get; set; }
        public ChargeAPI charge { get; set; }

        [Required]
        public string DefaultKeyEmpresa { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoUnidadNegocio { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoSede { get; set; }

        [Range(1, int.MaxValue)]
        public int CodigoSocio { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email No es válida")]

        public string Email { get; set; }
    }

    public class RequestSimple
    {
        public string DefaultKeyEmpresa { get; set; }
    }
}