using System;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace StripeExampleApi.Controllers
{
    [Route("api/v1/create-intent")]
    [ApiController]
    public class CheckoutApiController : Controller
    {
        public CheckoutApiController()
        {
            StripeConfiguration.ApiKey = "sk_test_51NygIWCuxdYH5UkGS43nNG01rHRsxsmZqXCJ51au8JwhzlDg4MRF6ZeJqxvvVzpaZ9ufJUIlCOZ7LSArBhBRVJYe000ffEpJmA";
        }

        [HttpPost]
        public ActionResult Post()
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = 1099,
                Currency = "usd",
                // In the latest version of the API, specifying the `automatic_payment_methods` parameter is optional because Stripe enables its functionality by default.
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            };
            var service = new PaymentIntentService();
            PaymentIntent intent = service.Create(options);
            return Json(new { client_secret = intent.ClientSecret });
        }
    }
}