using System;
using wpg.domain.payment;

namespace wpg.@internal.xml.serializer.payment
{
    public class PaymentMethodTypeSerializer
    {

        public static PaymentMethodType? convert(String paymentMethodMask)
        {
            if (paymentMethodMask == null)
            {
                throw new ArgumentNullException(nameof(paymentMethodMask), "Payment method mask is null");
            }

            // Attempt to find payment method
            PaymentMethodType? paymentMethodType = PaymentMethodTypeTranslator.getType(paymentMethodMask);

            // Wipe out child brand if not found (cards only)
            if (paymentMethodType == null)
            {
                if (paymentMethodMask.StartsWith("VISA_") ||
                    paymentMethodMask.StartsWith("ECMC_") ||
                    paymentMethodMask.StartsWith("AMEX_") ||
                    paymentMethodMask.StartsWith("DINERS_") ||
                    paymentMethodMask.StartsWith("CB_") ||
                    paymentMethodMask.StartsWith("CARTEBLEUE_") ||
                    paymentMethodMask.StartsWith("DANKORT_") ||
                    paymentMethodMask.StartsWith("DISCOVER_") ||
                    paymentMethodMask.StartsWith("JCB_") ||
                    paymentMethodMask.StartsWith("TROY_"))
                {
                    String parentBrandMask = paymentMethodMask.Substring(0, paymentMethodMask.IndexOf('_')) + "-SSL";
                    paymentMethodType = PaymentMethodTypeTranslator.getType(parentBrandMask);
                }
            }

            return paymentMethodType;
        }

    }
}
