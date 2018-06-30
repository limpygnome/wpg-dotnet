using System;
using System.Collections.Generic;

namespace Worldpay.@internal.xml.serializer
{
    internal class JournalSerializer
    {

        public static Journal read(XmlBuilder builder)
        {
            // Read booking date
            DateTime bookingDate = readBookingDate(builder);

            // Read accountTxs
            List<JournalTransaction> transactions = readJournalTransactions(builder);

            // Read journal references
            List<JournalReference> references = readJournalReferences(builder);

            // Read journal type details
            List<JournalTypeDetail> typeDetails = readJournalDetails(builder);

            // Read attribs
            string type = builder.a("journalType");
            string sent = builder.a("sent");

            // Give back result
            Journal journal = new Journal(type, sent, bookingDate, transactions, references, typeDetails);
            return journal;
        }

        private static DateTime readBookingDate(XmlBuilder builder)
        {
            builder.e("bookingDate").e("date");
            DateTime result = DateSerializer.readDate(builder);
            builder.up().up();
            return result;
        }

        private static List<JournalTransaction> readJournalTransactions(XmlBuilder builder)
        {
            List<XmlBuilder> children = builder.childTags("accountTx");
            List<JournalTransaction> transactions = new List<JournalTransaction>(children.Count);
            foreach (XmlBuilder childBuilder in children)
            {
                JournalTransaction transaction = readJournalTransaction(childBuilder);
                transactions.Add(transaction);
            }
            return transactions;
        }

        private static JournalTransaction readJournalTransaction(XmlBuilder builder)
        {
            String batchId = builder.a("batchId");
            JournalTransactionType type = readJournalTransactionType(builder);
            Amount amount = readJournalTransactionAmount(builder);

            JournalTransaction transaction = new JournalTransaction(batchId, type, amount);
            return transaction;
        }

        private static JournalTransactionType readJournalTransactionType(XmlBuilder builder)
        {
            String rawType = builder.a("accountType");
            JournalTransactionType type = (JournalTransactionType) Enum.Parse(typeof(JournalTransactionType), rawType);
            return type;
        }

        private static Amount readJournalTransactionAmount(XmlBuilder builder)
        {
            builder.e("amount");
            Amount amount = AmountSerializer.read(builder);
            builder.up();
            return amount;
        }

        private static List<JournalReference> readJournalReferences(XmlBuilder builder)
        {
            List<XmlBuilder> children = builder.childTags("journalReference");
            List<JournalReference> references = new List<JournalReference>(children.Count);
            foreach (XmlBuilder childBuilder in children)
            {
                JournalReference reference = readJournalReference(childBuilder);
                references.Add(reference);
            }
            return references;
        }

        private static JournalReference readJournalReference(XmlBuilder builder)
        {
            String type = builder.a("type");
            String reference = builder.a("reference");

            JournalReference result = new JournalReference(type, reference);
            return result;
        }

        private static List<JournalTypeDetail> readJournalDetails(XmlBuilder builder)
        {
            List<XmlBuilder> children = builder.childTags("journalTypeDetail");
            List<JournalTypeDetail> details = new List<JournalTypeDetail>(children.Count);
            foreach (XmlBuilder childBuilder in children)
            {
                JournalTypeDetail detail = readJournalTypeDetail(childBuilder);
                details.Add(detail);
            }
            return details;
        }

        private static JournalTypeDetail readJournalTypeDetail(XmlBuilder builder)
        {
            String id = builder.getCdata("JournalTypeDetailId");
            String description = builder.getCdata("Description");

            List<XmlBuilder> children = builder.childTags("ReversalReason");
            List<String> reversalReasons = new List<string>(children.Count);
            foreach (XmlBuilder childBuilder in children)
            {
                String reversalReason = childBuilder.cdata();
                reversalReasons.Add(reversalReason);
            }

            JournalTypeDetail detail = new JournalTypeDetail(id, description, reversalReasons);
            return detail;
        }

    }
}
