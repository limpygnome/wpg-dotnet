using System;
using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            var response = obj as PaymentResponse;
            return response != null &&
                   Status == response.Status &&
                   EqualityComparer<Payment>.Default.Equals(Payment, response.Payment) &&
                   EqualityComparer<ThreeDsDetails>.Default.Equals(ThreeDsDetails, response.ThreeDsDetails);
        }

        public override int GetHashCode()
        {
            var hashCode = 122077208;
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Payment>.Default.GetHashCode(Payment);
            hashCode = hashCode * -1521134295 + EqualityComparer<ThreeDsDetails>.Default.GetHashCode(ThreeDsDetails);
            return hashCode;
        }

    }
}
