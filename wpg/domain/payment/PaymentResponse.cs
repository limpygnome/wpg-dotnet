using System;
using wpg.domain.payment.threeds;
namespace wpg.domain.payment
{
    public class PaymentResponse
    {
        public PaymentResponse(Payment payment)
        {
            this.Payment = payment;
            this.Status = PaymentStatus.PAYMENT_RESULT;
        }

        public PaymentResponse(ThreeDsDetails threeDsDetails)
        {
            this.ThreeDsDetails = threeDsDetails;
            this.Status = PaymentStatus.THREEDS_REQUESTED;
        }

        public PaymentStatus Status { get; set; }
        public Payment Payment { get; set; }
        public ThreeDsDetails ThreeDsDetails { get; set; }

    }
}
