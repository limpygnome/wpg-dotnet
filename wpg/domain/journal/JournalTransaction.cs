using System.Collections.Generic;
using wpg.domain.payment;

namespace wpg.domain.journal
{
    public class JournalTransaction
    {
        public JournalTransaction(string batchId, JournalTransactionType type, Amount amount)
        {
            this.BatchId = batchId;
            this.Type = type;
            this.Amount = amount;
        }

        public string BatchId { get; private set; }
        public JournalTransactionType Type { get; private set; }
        public Amount Amount { get; private set; }

        public override bool Equals(object obj)
        {
            var transaction = obj as JournalTransaction;
            return transaction != null &&
                   BatchId == transaction.BatchId &&
                   EqualityComparer<JournalTransactionType>.Default.Equals(Type, transaction.Type) &&
                   EqualityComparer<Amount>.Default.Equals(Amount, transaction.Amount);
        }

        public override int GetHashCode()
        {
            var hashCode = -2019143404;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(BatchId);
            hashCode = hashCode * -1521134295 + EqualityComparer<JournalTransactionType>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<Amount>.Default.GetHashCode(Amount);
            return hashCode;
        }

    }
}
