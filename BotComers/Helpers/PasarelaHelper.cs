using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BotComers.Helpers
{
    public class PasarelaHelper
    {



        //********************************** PAYPAL *****************************

        public object OrderHelper(List<ItemPaypal> items)
        {
            decimal total = 0;
            foreach (ItemPaypal itemPaypal in items)
            {
                total += itemPaypal.unit_amount.value;
            }

            return new
            {
                intent = "CAPTURE",
                purchase_units = new List<object>()
                {
                     new
                {
                    items = items,
                    amount = new
                    {
                        currency_code = "USD",
                        value = total,
                        breakdown = new
                        {
                            item_total = new
                            {
                                currency_code = "USD",
                                value = total
                            }
                        }
                    }
                },
                },
                application_context = new
                {
                    return_url = "https://example.com/return",
                    cancel_url = "https://example.com/cancel"
                }
            };
        }


        //********************************** END PAYPAL *****************************


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
    public class Amount
    {
        public string currency_code { get; set; }
        public string value { get; set; }

    }
    public class UnitAmount
    {
        [StringLength(3), Required]
        public string currency_code { get; set; }

        [Range(1, float.MaxValue)]
        public decimal value { get; set; }
    }

}