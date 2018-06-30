using System;
using System.Collections.Generic;

namespace Worldpay
{
    public class Journal
    {
        public Journal(string type, string sent, DateTime bookingDate, List<JournalTransaction> transactions, List<JournalReference> references, List<JournalTypeDetail> typeDetails)
        {
            this.Type = type;
            this.Sent = sent;
            this.BookingDate = bookingDate;
            this.Transactions = transactions;
            this.References = references;
            this.TypeDetails = typeDetails;
        }

        public DateTime BookingDate { get; private set; }
        public string Type { get; private set; }
        public string Sent { get; private set; }
        public List<JournalTransaction> Transactions { get; private set; }

        // TODO not well understood, model may need improving
        public List<JournalReference> References { get; private set; }
        public List<JournalTypeDetail> TypeDetails { get; private set; }

        public override bool Equals(object obj)
        {
            var journal = obj as Journal;
            return journal != null &&
                   BookingDate == journal.BookingDate &&
                   Type == journal.Type &&
                   Sent == journal.Sent &&
                   EqualityComparer<List<JournalTransaction>>.Default.Equals(Transactions, journal.Transactions) &&
                   EqualityComparer<List<JournalReference>>.Default.Equals(References, journal.References) &&
                   EqualityComparer<List<JournalTypeDetail>>.Default.Equals(TypeDetails, journal.TypeDetails);
        }

        public override int GetHashCode()
        {
            var hashCode = -1745730648;
            hashCode = hashCode * -1521134295 + BookingDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Sent);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<JournalTransaction>>.Default.GetHashCode(Transactions);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<JournalReference>>.Default.GetHashCode(References);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<JournalTypeDetail>>.Default.GetHashCode(TypeDetails);
            return hashCode;
        }

    }
}
