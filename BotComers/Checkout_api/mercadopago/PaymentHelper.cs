using MercadoPago.DataStructures.Payment;
using MercadoPago.Resources;
using System.Collections.Generic;


namespace BotComers.Checkout_api.mercadopago
{
    public class PaymentHelper
    {
        Payment LastPayment;

        public void Inicio(string AccessToken)
        {
            MercadoPago.SDK.CleanConfiguration();
            MercadoPago.SDK.SetAccessToken(AccessToken);
        }

        public Payment getPaymenData(PaymentParameter oPaymentParameter)
        {
            Inicio(oPaymentParameter.AccessToken);

            //---EJEMPLO ENVIAR AdditionalInfoPayer---
            //var addInfoPayer = new AdditionalInfoPayer
            //{
            //    FirstName = "Rubén",
            //    LastName = "González"
            //};

            //---EJEMPLO ENVIAR items--- 
            //var item = new Item
            //{
            //    Id = "producto123",
            //    Title = "Celular blanco",
            //    Description = "4G, 32 GB",
            //    Quantity = 1,
            //    PictureUrl = "http://www.imagenes.com/celular.jpg",
            //    UnitPrice = 100.4m
            //};

            //List<Item> items = new List<Item>();
            //items.Add(item);

            var addInf = new AdditionalInfo
            {
                Payer = oPaymentParameter.AdditionalInfoPayer,
                Items = oPaymentParameter.items
            };

            Payment payment = new Payment
            {
                TransactionAmount = (float)oPaymentParameter.TransactionAmount,
                Token = oPaymentParameter.Token,
                Description = oPaymentParameter.Description,
                Installments = 1,
                PaymentMethodId = oPaymentParameter.PaymentMethodId,
                Payer = new Payer
                {
                    Email = oPaymentParameter.Email,
                    Identification = new Identification()
                    {
                        Type = oPaymentParameter.docType,
                        Number = oPaymentParameter.docNumber
                    }
                },
                AdditionalInfo = addInf
            };

            return payment;
        }

        public void GuardarPago(PaymentParameter oPaymentParameter)
        {
            Payment payment = getPaymenData(oPaymentParameter);

            payment.Save();
            LastPayment = payment;
        }

    }

    public class PaymentParameter
    {
        public string AccessToken { get; set; }
        public float? TransactionAmount { get; set; }
        public string Token { get; set; }
        public string Description { get; set; }
        public string PaymentMethodId { get; set; }
        public string Email { get; set; }
        public string docType { get; set; }
        public string docNumber { get; set; }
        public AdditionalInfoPayer AdditionalInfoPayer { get; set; }
        public List<Item> items { get; set; }
    }

}