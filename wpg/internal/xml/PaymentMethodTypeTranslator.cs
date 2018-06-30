using System;
using System.Collections.Generic;

namespace Worldpay.@internal.xml
{
    internal class PaymentMethodTypeTranslator
    {
        private static readonly Dictionary<PaymentMethodType, string> pmToMask;
        private static readonly Dictionary<string, PaymentMethodType> maskToPm;

        static PaymentMethodTypeTranslator()
        {
            Dictionary<PaymentMethodType, string> result = new Dictionary<PaymentMethodType, string>();

            result.Add(PaymentMethodType.CARD, "CARD-SSL");
            result.Add(PaymentMethodType.VISA, "VISA-SSL");
            result.Add(PaymentMethodType.MASTERCARD, "ECMC-SSL");
            result.Add(PaymentMethodType.AMEX, "AMEX-SSL");
            result.Add(PaymentMethodType.DINERS, "DINERS-SSL");
            result.Add(PaymentMethodType.CARTE_BLEUE, "CB-SSL");
            result.Add(PaymentMethodType.DANKORT, "DANKORT-SSL");
            result.Add(PaymentMethodType.DISCOVER, "DISCOVER-SSL");
            result.Add(PaymentMethodType.JCB, "JCB-SSL");
            result.Add(PaymentMethodType.AIRPLUS, "AIRPLUS-SSL");
            result.Add(PaymentMethodType.UATP, "UATP-SSL");
            result.Add(PaymentMethodType.LASER, "LASER-SSL");
            result.Add(PaymentMethodType.TROY, "TROY-SSL");

            result.Add(PaymentMethodType.PAYPAL, "PAYPAL-EXPRESS");
            result.Add(PaymentMethodType.ASTROPAY, "ASTROPAY-SSL");
            result.Add(PaymentMethodType.BOLETO, "BOLETO-SSL");
            result.Add(PaymentMethodType.SEPA_DIRECT_DEBIT, "SEPA_DIRECT_DEBIT-SSL");
            result.Add(PaymentMethodType.ACH, "ACH-SSL");
            result.Add(PaymentMethodType.MASTERPASS, "MASTERPASS-SSL");
            result.Add(PaymentMethodType.IDEAL, "IDEAL-SSL");
            result.Add(PaymentMethodType.GIROPAY, "GIROPAY-SSL");
            result.Add(PaymentMethodType.ALIPAY, "ALIPAY-SSL");
            result.Add(PaymentMethodType.CASHU, "CASHU-SSL");
            result.Add(PaymentMethodType.DINEROMAIL_7ELEVEN, "DINEROMAIL_7ELEVEN-SSL");
            result.Add(PaymentMethodType.DINEROMAIL_OXXO, "DINEROMAIL_OXXO-SSL");
            result.Add(PaymentMethodType.DINEROMAIL_ONLINE_BT, "DINEROMAIL_ONLINE_BT-SSL");
            result.Add(PaymentMethodType.DINEROMAIL_SERVIPAG, "DINEROMAIL_SERVIPAG-SSL");

            result.Add(PaymentMethodType.EUTELLER, "EUTELLER-SSL");
            result.Add(PaymentMethodType.MISTERCASH, "MISTERCASH-SSL");
            result.Add(PaymentMethodType.PAYU, "PAYU-SSL");
            result.Add(PaymentMethodType.POLI, "POLI-SSL");
            result.Add(PaymentMethodType.POLINZ, "POLINZ-SSL");
            result.Add(PaymentMethodType.POSTEPAY, "POSTEPAY-SSL");
            result.Add(PaymentMethodType.PRZELEWY, "PRZELEWY-SSL");
            result.Add(PaymentMethodType.QIWI, "QIWI-SSL");
            result.Add(PaymentMethodType.SAFETYPAY, "SAFETYPAY-SSL");
            result.Add(PaymentMethodType.SKRILL, "SKRILL-SSL");
            result.Add(PaymentMethodType.SOFORT, "SOFORT-SSL");
            result.Add(PaymentMethodType.SOFORT_CH, "SOFORT_CH-SSL");
            result.Add(PaymentMethodType.TRUSTPAY_CZ, "TRUSTPAY_CZ-SSL");
            result.Add(PaymentMethodType.TRUSTPAY_EE, "TRUSTPAY_EE-SSL");
            result.Add(PaymentMethodType.TRUSTPAY_SK, "TRUSTPAY_SK-SSL");
            result.Add(PaymentMethodType.WEBMONEY, "WEBMONEY-SSL");
            result.Add(PaymentMethodType.YANDEX_MONEY, "YANDEX_MONEY-SSL");
            result.Add(PaymentMethodType.KLARNA, "KLARNA-SSL");

            pmToMask = result;

            // Flip it
            maskToPm = new Dictionary<string, PaymentMethodType>();
            foreach (var kv in result)
            {
                maskToPm.Add(kv.Value, kv.Key);
            }
        }

        public static string getMask(PaymentMethodType? type)
        {
            string mask = type != null && pmToMask.ContainsKey((PaymentMethodType) type) ? pmToMask[(PaymentMethodType) type] : null;
            return mask;
        }

        public static PaymentMethodType? getType(string paymentMethodMask)
        {
            PaymentMethodType? paymentMethod = maskToPm.ContainsKey(paymentMethodMask) ? (PaymentMethodType?) maskToPm[paymentMethodMask] : null;
            return paymentMethod;
        }

    }
}
