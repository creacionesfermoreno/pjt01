using AppsfitWebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppsfitWebApi.Helpers
{
    public class PaypalHelper
    {

        
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
    }
    

}