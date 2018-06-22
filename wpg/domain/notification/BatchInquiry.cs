using System.Collections.Generic;
namespace wpg.domain.notification
{
    public class BatchInquiry
    {

        public BatchInquiry(BatchStatus status, int transactions, List<BatchError> errors)
        {
            this.Status = status;
            this.Transactions = transactions;
            this.Errors = errors;
        }

        public BatchStatus Status { get; private set; }
        public int Transactions { get; private set; }
        public List<BatchError> Errors { get; private set; }

        public override bool Equals(object obj)
        {
            var inquiry = obj as BatchInquiry;
            return inquiry != null &&
                   Status == inquiry.Status &&
                   Transactions == inquiry.Transactions &&
                   EqualityComparer<List<BatchError>>.Default.Equals(Errors, inquiry.Errors);
        }

        public override int GetHashCode()
        {
            var hashCode = -1699673931;
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + Transactions.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<BatchError>>.Default.GetHashCode(Errors);
            return hashCode;
        }

    }
}
