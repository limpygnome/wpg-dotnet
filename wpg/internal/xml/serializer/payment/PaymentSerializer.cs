using System;
using Worldpay.@internal.xml.serializer.payment.result;
using Worldpay.@internal.xml.serializer.payment.tokenisation;

namespace Worldpay.@internal.xml.serializer.payment
{
    internal class PaymentSerializer
    {

        public static Payment read(XmlBuilder builder)
        {
            // payment method (can be null when unknown)
            string paymentMethodMask = builder.getCdata("paymentMethod");
            PaymentMethodType? paymentMethodType = PaymentMethodTypeSerializer.convert(paymentMethodMask);

            // amount
            builder.e("amount");
            Amount amount = AmountSerializer.read(builder);
            builder.up();

            // last event
            string lastEventRaw = builder.getCdata("lastEvent");
            LastEvent lastEvent = (LastEvent) Enum.Parse(typeof(LastEvent), lastEventRaw, true);

            // read results
            CardDetailsResult cardDetails = CardDetailsResultSerializer.read(builder);
            PayoutAuthorisationResult payoutAuthorisationResult = PayoutAuthorisationResultSerializer.read(builder);
            ISO8583Result iso8583Result = ISO8583ResultSerializer.read(builder);
            ThreeDSecureResult threeDSecureResult = ThreeDSecureResultSerializer.read(builder);
            AvsResult avsResult = AvsResultSerializer.read(builder);
            CvcResult cvcResult = CvcResultSerializer.read(builder);
            AvvResult avvResult = AvvResultSerializer.read(builder);
            Balance balance = BalanceSerializer.read(builder);
            RiskScoreResult riskScoreResult = RiskScoreResultSerializer.read(builder);

            // -- token details are not in paymentDetails tag, thus go up a level
            builder.up();
            Token token = TokenSerializer.read(builder);

            // wrap it all up
            Payment payment = new Payment(
                    paymentMethodType, paymentMethodMask, amount, lastEvent, balance, cardDetails, payoutAuthorisationResult,
                    iso8583Result, threeDSecureResult, avsResult, cvcResult, avvResult, riskScoreResult, token
            );
            return payment;
        }

    }
}
