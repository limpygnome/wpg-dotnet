using System;
using wpg.domain.payment.result;
using wpg.domain.tokenisation;

namespace wpg.domain.payment
{
    public class Payment
    {
        public Payment(PaymentMethod paymentMethod, String paymentMethodMask, Amount amount, LastEvent lastEvent, Balance balance,
                   CardDetails cardDetails, PayoutAuthorisationResult payoutAuthorisationResult,
                   ISO8583Result iso8583Result, ThreeDSecureResult threeDSecureResult, AvsResult avsResult,
                   CvcResult cvcResult, AvvResult avvResult, RiskScoreResult riskScoreResult,
                   Token token)
        {
            this.PaymentMethod = paymentMethod;
            this.PaymentMethodMask = paymentMethodMask;
            this.Amount = amount;
            this.LastEvent = lastEvent;
            this.Balance = balance;
            this.CardDetails = cardDetails;
            this.PayoutAuthorisationResult = payoutAuthorisationResult;
            this.ISO8583Result = iso8583Result;
            this.ThreeDSecureResult = threeDSecureResult;
            this.AvsResult = avsResult;
            this.CvcResult = cvcResult;
            this.AvvResult = avvResult;
            this.RiskScoreResult = riskScoreResult;
            this.Token = token;
        }

        public PaymentMethod PaymentMethod { get; private set; }
        public String PaymentMethodMask { get; private set; }
        public Amount Amount { get; private set; }
        public LastEvent LastEvent { get; private set; }
        public Balance Balance { get; private set; }
        public CardDetails CardDetails { get; set; }
        public PayoutAuthorisationResult PayoutAuthorisationResult { get; set; }
        public ISO8583Result ISO8583Result { get; set; }
        public ThreeDSecureResult ThreeDSecureResult { get; set; }
        public AvsResult AvsResult { get; set; }
        public CvcResult CvcResult { get; set; }
        public AvvResult AvvResult { get; set; }
        public RiskScoreResult RiskScoreResult { get; set; }
        public Token Token { get; set; }

    }
}
