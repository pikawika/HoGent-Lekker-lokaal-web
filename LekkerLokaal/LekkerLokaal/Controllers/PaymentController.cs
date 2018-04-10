using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mollie.Api.Client;
using Mollie.Api.Client.Abstract;
using Mollie.Api.Models.Payment.Request;
using Mollie.Api.Models.Payment.Response;

namespace LekkerLokaal.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentClient _paymentClient;

        public PaymentController()
        {
            this._paymentClient = new PaymentClient("test_yGJ4USbh3BWV5AkGbdh4NG4EG2UdaF");
        }

        [HttpGet]
        public async Task<ActionResult> Pay(string id)
        {
            PaymentResponse payment = await this._paymentClient.GetPaymentAsync(id);
            return this.Redirect(payment.Links.PaymentUrl);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PaymentRequestModel payment = new PaymentRequestModel();
            return this.View(payment);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PaymentRequestModel paymentRequestModel)
        {
            if (this.ModelState.IsValid)
            {
                PaymentRequest paymentRequest = new PaymentRequest();
                paymentRequest.Amount = paymentRequestModel.Amount;
                paymentRequest.Description = paymentRequestModel.Description;
                paymentRequest.RedirectUrl = paymentRequestModel.RedirectUrl;
                await this._paymentClient.CreatePaymentAsync(paymentRequest);

                return this.RedirectToAction("Index");
            }

            return this.View(paymentRequestModel);
        }

    }
}