using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Models
{
    public class MPagoModel
    {

        public List<ItemMP> items { get; set; }
        public Payer payer { get; set; }
        public BackUrls back_urls { get; set; }
        public string auto_return { get; set; }
        public PaymentMethods payment_methods { get; set; }
        public string notification_url { get; set; }
        public string statement_descriptor { get; set; }
        public string external_reference { get; set; }

    }
    public class Address
    {
        public string street_name { get; set; }
        public int street_number { get; set; }
        public string zip_code { get; set; }
    }

    public class BackUrls
    {
        public string success { get; set; }
        public string failure { get; set; }
        public string pending { get; set; }
    }

    public class ExcludedPaymentMethod
    {
        public string id { get; set; }
    }

    public class ExcludedPaymentType
    {
        public string id { get; set; }
    }

    public class Identification
    {
        public string type { get; set; }
        public string number { get; set; }
    }

    public class ItemMP
    {
        public string id { get; set; }
        public string title { get; set; }
        public string currency_id { get; set; }
        public string description { get; set; }
        public string category_id { get; set; }
        public int quantity { get; set; }
        public decimal unit_price { get; set; }
    }

    public class Payer
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public Phone phone { get; set; }
        public Identification identification { get; set; }
        public Address address { get; set; }
    }

    public class PaymentMethods
    {
        public List<ExcludedPaymentMethod> excluded_payment_methods { get; set; }
        public List<ExcludedPaymentType> excluded_payment_types { get; set; }
        public int installments { get; set; }
    }

    public class Phone
    {
        public string area_code { get; set; }
        public string number { get; set; }
    }



}