using System;
using System.Threading.Tasks;

using wpg.connection;
using wpg.connection.auth;
using wpg.domain;
using wpg.domain.payment;
using wpg.request.card;

namespace wpg_examples
{
    class Program
    {

        static void Main(string[] args)
        {
            IAuth auth = new UserPassAuth("xml user", "xml pass", "merchant code");
            GatewayContext gatewayContext = new GatewayContext(GatewayEnvironment.SANDBOX, auth);

            Address address = new Address("test road", "Cambridge", "CB0123", "GB");

            CardPaymentRequest request = new CardPaymentRequest();
            request.OrderDetails = new OrderDetails("test order", new Amount("GBP", 2L, 1234L));
            request.CardDetails = new CardDetails("4444333322221129", 1, 2020, "John Doe");
            request.BillingAddress = address;
            request.ShippingAddress = address;

            Task<PaymentResponse> asyncResponse = request.Send(gatewayContext);
            PaymentResponse response = asyncResponse.Result;

            Console.WriteLine("Hello World!");
        }

    }
}
