using System;

namespace Worldpay.@internal.xml.serializer
{
    internal class OrderDetailsSerializer
    {

        public static void decorateMerchantCode(XmlBuildParams buildParams)
        {
            UserPassAuth auth = buildParams.GatewayContext.Auth as UserPassAuth;
            XmlBuilder builder = buildParams.Builder;
            builder.a("merchantCode", auth.MerchantCode);
        }

        public static void decorateAndStartOrder(XmlBuildParams buildParams, OrderDetails orderDetails)
        {
            UserPassAuth auth = buildParams.GatewayContext.Auth as UserPassAuth;
            XmlBuilder builder = buildParams.Builder;

            // add merchant code (handled differently for batch)
            if (!buildParams.IsBatch)
           {
                decorateMerchantCode(buildParams);
                builder.e("submit");
            }

            // start order element
            builder.e("order", true).a("orderCode", orderDetails.OrderCode);

            if (auth.InstallationId != null)
            {
                builder.a("installationId", auth.InstallationId);
            }

            // append description
            builder = builder.e("description").cdata(orderDetails.Description).up();

            // append amount
            Amount amount = orderDetails.Amount;

            if (amount == null)
            {
                throw new ArgumentException("Amount is mandatory for order details");
            }

            AmountSerializer.write(builder, amount);
        }

        public static void decorateFinishOrder(XmlBuildParams buildParams)
        {
            XmlBuilder builder = buildParams.Builder;

            if (buildParams.IsBatch)
            {
                builder.up();
            }
            else
            {
                builder.reset();
            }
        }

    }
}
