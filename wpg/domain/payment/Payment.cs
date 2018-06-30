using System.Collections.Generic;

namespace Worldpay
{
    public class Payment
    {
        public Payment(PaymentMethodType? paymentMethodType, string paymentMethodMask, Amount amount, LastEvent lastEvent, Balance balance,
                   CardDetailsResult cardDetailsResult, PayoutAuthorisationResult payoutAuthorisationResult,
                   ISO8583Result iso8583Result, ThreeDSecureResult threeDSecureResult, AvsResult avsResult,
                   CvcResult cvcResult, AvvResult avvResult, RiskScoreResult riskScoreResult,
                   Token token)
        {
            this.PaymentMethodType = paymentMethodType;
            this.PaymentMethodMask = paymentMethodMask;
            this.Amount = amount;
            this.LastEvent = lastEvent;
            this.Balance = balance;
            this.CardDetailsResult = cardDetailsResult;
            this.PayoutAuthorisationResult = payoutAuthorisationResult;
            this.ISO8583Result = iso8583Result;
            this.ThreeDSecureResult = threeDSecureResult;
            this.AvsResult = avsResult;
            this.CvcResult = cvcResult;
            this.AvvResult = avvResult;
            this.RiskScoreResult = riskScoreResult;
            this.Token = token;
        }

        public PaymentMethodType? PaymentMethodType { get; private set; }
        public string PaymentMethodMask { get; private set; }
        public Amount Amount { get; private set; }
        public LastEvent LastEvent { get; private set; }
        public Balance Balance { get; private set; }
        public CardDetailsResult CardDetailsResult { get; set; }
        public PayoutAuthorisationResult PayoutAuthorisationResult { get; set; }
        public ISO8583Result ISO8583Result { get; set; }
        public ThreeDSecureResult ThreeDSecureResult { get; set; }
        public AvsResult AvsResult { get; set; }
        public CvcResult CvcResult { get; set; }
        public AvvResult AvvResult { get; set; }
        public RiskScoreResult RiskScoreResult { get; set; }
        public Token Token { get; set; }

        public override bool Equals(object obj)
        {
            var payment = obj as Payment;
            return payment != null &&
                   EqualityComparer<PaymentMethodType?>.Default.Equals(PaymentMethodType, payment.PaymentMethodType) &&
                   PaymentMethodMask == payment.PaymentMethodMask &&
                   EqualityComparer<Amount>.Default.Equals(Amount, payment.Amount) &&
                   LastEvent == payment.LastEvent &&
                   EqualityComparer<Balance>.Default.Equals(Balance, payment.Balance) &&
                   EqualityComparer<CardDetailsResult>.Default.Equals(CardDetailsResult, payment.CardDetailsResult) &&
                   EqualityComparer<PayoutAuthorisationResult>.Default.Equals(PayoutAuthorisationResult, payment.PayoutAuthorisationResult) &&
                   EqualityComparer<ISO8583Result>.Default.Equals(ISO8583Result, payment.ISO8583Result) &&
                   EqualityComparer<ThreeDSecureResult>.Default.Equals(ThreeDSecureResult, payment.ThreeDSecureResult) &&
                   EqualityComparer<AvsResult>.Default.Equals(AvsResult, payment.AvsResult) &&
                   EqualityComparer<CvcResult>.Default.Equals(CvcResult, payment.CvcResult) &&
                   EqualityComparer<AvvResult>.Default.Equals(AvvResult, payment.AvvResult) &&
                   EqualityComparer<RiskScoreResult>.Default.Equals(RiskScoreResult, payment.RiskScoreResult) &&
                   EqualityComparer<Token>.Default.Equals(Token, payment.Token);
        }

        public override int GetHashCode()
        {
            var hashCode = 412315353;
            hashCode = hashCode * -1521134295 + EqualityComparer<PaymentMethodType?>.Default.GetHashCode(PaymentMethodType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PaymentMethodMask);
            hashCode = hashCode * -1521134295 + EqualityComparer<Amount>.Default.GetHashCode(Amount);
            hashCode = hashCode * -1521134295 + LastEvent.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Balance>.Default.GetHashCode(Balance);
            hashCode = hashCode * -1521134295 + EqualityComparer<CardDetailsResult>.Default.GetHashCode(CardDetailsResult);
            hashCode = hashCode * -1521134295 + EqualityComparer<PayoutAuthorisationResult>.Default.GetHashCode(PayoutAuthorisationResult);
            hashCode = hashCode * -1521134295 + EqualityComparer<ISO8583Result>.Default.GetHashCode(ISO8583Result);
            hashCode = hashCode * -1521134295 + EqualityComparer<ThreeDSecureResult>.Default.GetHashCode(ThreeDSecureResult);
            hashCode = hashCode * -1521134295 + EqualityComparer<AvsResult>.Default.GetHashCode(AvsResult);
            hashCode = hashCode * -1521134295 + EqualityComparer<CvcResult>.Default.GetHashCode(CvcResult);
            hashCode = hashCode * -1521134295 + EqualityComparer<AvvResult>.Default.GetHashCode(AvvResult);
            hashCode = hashCode * -1521134295 + EqualityComparer<RiskScoreResult>.Default.GetHashCode(RiskScoreResult);
            hashCode = hashCode * -1521134295 + EqualityComparer<Token>.Default.GetHashCode(Token);
            return hashCode;
        }

    }
}
