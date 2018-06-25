using System;
using wpg.builder;
using wpg.domain.journal;
using wpg.domain.notification;
using wpg.domain.payment;
using wpgintegrationtests.xunit;
using Xunit;

namespace wpgintegrationtests.builder
{
    public class XmlNotificationBuilderTest
    {

        [TextFile("data/order-notifications/authorised.txt")]
        [Theory]
        public void authorised_asExpected(string xml)
        {
            // When
            OrderNotification orderNotification = new XmlNotificationBuilder().read(xml);

            // Then
            Assert.NotNull(orderNotification);
            Assert.Equal("Your_order_code", orderNotification.OrderCode);
            Assert.Equal(1, orderNotification.Payments.Count);

            // -- Journal
            Journal journal = orderNotification.Journal;
            Assert.Equal(new DateTime(2020, 1, 1), journal.BookingDate);

            JournalTransaction tx = journal.Transactions[0];
            Assert.Equal("30", tx.BatchId);
            Assert.Equal(JournalTransactionType.IN_PROCESS_AUTHORISED, tx.Type);
            Assert.Equal("EUR", tx.Amount.CurrencyCode);
            Assert.Equal(2L, tx.Amount.Exponent);
            Assert.Equal(2400L, tx.Amount.Value);
            Assert.Equal(DebitCreditIndicator.CREDIT, tx.Amount.DebitCreditIndicator);

            // -- Payment
            Payment payment = orderNotification.Payments[0];
            Assert.Equal(PaymentMethodType.VISA, payment.PaymentMethodType);
            Assert.Equal("VISA_CREDIT-SSL", payment.PaymentMethodMask);
            Assert.Equal("EUR", payment.Amount.CurrencyCode);
            Assert.Equal(2L, payment.Amount.Exponent);
            Assert.Equal(2400L, payment.Amount.Value);
            Assert.Equal(DebitCreditIndicator.CREDIT, tx.Amount.DebitCreditIndicator);
            Assert.Equal("444433******1111", payment.CardDetailsResult.MaskedCardNumber);
            Assert.Equal(1L, payment.CardDetailsResult.ExpiryMonth);
            Assert.Equal(2020L, payment.CardDetailsResult.ExpiryYear);
            Assert.Equal("N/A", payment.CardDetailsResult.IssuerCountryCode);
            Assert.Equal("***", payment.CardDetailsResult.CardHolderName);
            Assert.Equal("622206", payment.PayoutAuthorisationResult.AuthorisationId);
            Assert.Equal("Cardholder authenticated", payment.ThreeDSecureResult.Description);
            Assert.Equal("05", payment.ThreeDSecureResult.ECI);
            Assert.Equal("MAAAAAAAAAAAAAAAAAAAAAAAAAA=", payment.ThreeDSecureResult.CAVV);
            Assert.Equal(LastEvent.AUTHORISED, payment.LastEvent);
            Assert.Equal("E", payment.AvsResult.AvsResultCode);
            Assert.Equal("C", payment.CvcResult.Description);
            Assert.Equal("B", payment.AvvResult.AddressResultCode);
            Assert.Equal("B", payment.AvvResult.PostCodeResultCode);
            Assert.Equal("B", payment.AvvResult.CardHolderNameResultCode);
            Assert.Equal("B", payment.AvvResult.TelephoneResultCode);
            Assert.Equal("B", payment.AvvResult.EmailResultCode);
            Assert.Equal(0, payment.RiskScoreResult.Value);
        }

        [TextFile("data/order-notifications/cancelled.txt")]
        [Theory]
        public void cancelled_asExpected(string xml)
        {
            // When
            XmlNotificationBuilder builder = new XmlNotificationBuilder();
            OrderNotification orderNotification = builder.read(xml);

            // Then
            Assert.NotNull(orderNotification);
            Assert.Equal("ExampleOrder1", orderNotification.OrderCode);

            // -- Payment
            Payment payment = orderNotification.Payments[0];
            Assert.Equal(LastEvent.CANCELLED, payment.LastEvent);
            Assert.Equal("C", payment.CvcResult.Description);
            Assert.Equal("E", payment.AvsResult.AvsResultCode);

            // -- Journal
            Journal journal = orderNotification.Journal;
            Assert.Equal("CANCELLED", journal.Type);
            Assert.Equal("n", journal.Sent);
            Assert.Equal(1, journal.Transactions.Count);
        }

        [TextFile("data/order-notifications/captured.txt")]
        [Theory]
        public void captured_asExpected(string xml)
        {
            // When
            XmlNotificationBuilder builder = new XmlNotificationBuilder();
            OrderNotification orderNotification = builder.read(xml);

            // Then
            Assert.NotNull(orderNotification);
            Assert.Equal("ExampleOrder1", orderNotification.OrderCode);

            // -- Payment
            Payment payment = orderNotification.Payments[0];
            Assert.Equal(LastEvent.CAPTURED, payment.LastEvent);

            // -- Journal
            Journal journal = orderNotification.Journal;
            Assert.Equal("CAPTURED", journal.Type);
            Assert.Equal("n", journal.Sent);
            Assert.Equal(2, journal.Transactions.Count);

            JournalTransaction tx1 = journal.Transactions[0];
            Assert.Equal("29", tx1.BatchId);
            Assert.Equal(JournalTransactionType.IN_PROCESS_CAPTURED, tx1.Type);
            Assert.Equal(DebitCreditIndicator.CREDIT, tx1.Amount.DebitCreditIndicator);
            Assert.Equal(1000L, tx1.Amount.Value);
            Assert.Equal(2L, tx1.Amount.Exponent);
            Assert.Equal("EUR", tx1.Amount.CurrencyCode);

            JournalTransaction tx2 = journal.Transactions[1];
            Assert.Equal("30", tx2.BatchId);
            Assert.Equal(JournalTransactionType.IN_PROCESS_AUTHORISED, tx2.Type);
            Assert.Equal(DebitCreditIndicator.DEBIT, tx2.Amount.DebitCreditIndicator);
            Assert.Equal(1000L, tx2.Amount.Value);
            Assert.Equal(2L, tx2.Amount.Exponent);
            Assert.Equal("EUR", tx2.Amount.CurrencyCode);

            JournalReference reference = journal.References[0];
            Assert.Equal("capture", reference.Type);
            Assert.Equal("YourReference", reference.Reference);
        }

        [TextFile("data/order-notifications/refund.txt")]
        [Theory]
        public void refund_asExpected(string xml)
        {
            // When
            XmlNotificationBuilder builder = new XmlNotificationBuilder();
            OrderNotification orderNotification = builder.read(xml);

            // Then
            Assert.NotNull(orderNotification);
            Assert.Equal("ExampleOrder1", orderNotification.OrderCode);

            // -- Payment
            Payment payment = orderNotification.Payments[0];
            Assert.Equal(LastEvent.SENT_FOR_REFUND, payment.LastEvent);

            // -- Journal
            Journal journal = orderNotification.Journal;
            Assert.Equal("SENT_FOR_REFUND", journal.Type);
        }

        [TextFile("data/order-notifications/refused.txt")]
        [Theory]
        public void refused_asExpected(string xml)
        {
            // When
            XmlNotificationBuilder builder = new XmlNotificationBuilder();
            OrderNotification orderNotification = builder.read(xml);

            // Then
            Assert.NotNull(orderNotification);
            Assert.Equal("ExampleOrder1", orderNotification.OrderCode);

            // -- Payment
            Payment payment = orderNotification.Payments[0];
            Assert.Equal(LastEvent.REFUSED, payment.LastEvent);
            Assert.Equal(256, payment.RiskScoreResult.Value);

            // -- Journal
            Journal journal = orderNotification.Journal;
            Assert.Equal("REFUSED", journal.Type);
        }

    }
}
