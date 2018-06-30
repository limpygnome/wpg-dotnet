using System;
using System.Collections.Generic;

namespace Worldpay
{
    public class BatchError
    {
        public BatchError(string orderCode, int code, string message)
        {
            OrderCode = orderCode;
            Code = code;
            Message = message;
        }

        public string OrderCode { get; private set; }
        public int Code { get; private set; }
        public string Message { get; private set; }

        public override bool Equals(object obj)
        {
            var error = obj as BatchError;
            return error != null &&
                   OrderCode == error.OrderCode &&
                   Code == error.Code &&
                   Message == error.Message;
        }

        public override int GetHashCode()
        {
            var hashCode = 585017486;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderCode);
            hashCode = hashCode * -1521134295 + Code.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            return hashCode;
        }

    }
}
