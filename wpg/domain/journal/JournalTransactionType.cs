using System;
namespace wpg.domain.journal
{
    public enum JournalTransactionType
    {
        AUTHORISED,
        CAPTURED,
        IN_PROCESS,
        FOREX,
        DEPOSIT,
        IN_PROCESS_AUTHORISED,
        IN_PROCESS_CAPTURED,
        IN_PROCESS_SENT_FOR_AUTH,
        IN_PROCESS_REFUND_PENDING,
        REFUND_PENDING,
        PAYOUT,
        SETTLED_BIBIT_NET,
        SETTLED_BIBIT_COMMISSION,
        SETTLED_ACQUIRER_TOTAL
    }
}
