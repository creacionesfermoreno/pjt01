using AppsfitWebApi.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class PaypalApiModel
    {
    }


    public class ResponsePaypay
    {
        public string access_token { get; set; }
        public string error_description { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string message { get; set; }
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
        public string rel { get; set; }
        public string method { get; set; }
    }


    public class OrderDetailPaypal
    {
        public string message { get; set; }
        public string name { get; set; }
        public List<PurchaseUnit> purchase_units { get; set; }
    }
    public class PurchaseUnit
    {
        public string reference_id { get; set; }
        public Amount amount { get; set; }
        public List<ItemPaypal> items { get; set; }
    }

    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }

    }

    public class ItemPaypal
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string description { get; set; }

        [Range(1, int.MaxValue)]
        public int quantity { get; set; }
        public UnitAmount unit_amount { get; set; }
    }

    public class UnitAmount
    {
        [StringLength(3), Required]
        public string currency_code { get; set; }

        [Range(1, float.MaxValue)]
        public decimal value { get; set; }
    }
}