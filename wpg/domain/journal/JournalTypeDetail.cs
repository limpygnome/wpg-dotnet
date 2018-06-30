using System.Collections.Generic;

namespace Worldpay
{
    public class JournalTypeDetail
    {
        public JournalTypeDetail(string id, string description, List<string> reversalReasons)
        {
            this.Id = id;
            this.Description = description;
            this.ReversalReasons = reversalReasons;
        }

        public string Id { get; private set; }
        public string Description { get; private set; }
        public List<string> ReversalReasons { get; private set; }

        public override bool Equals(object obj)
        {
            var detail = obj as JournalTypeDetail;
            return detail != null &&
                   Id == detail.Id &&
                   Description == detail.Description &&
                   EqualityComparer<List<string>>.Default.Equals(ReversalReasons, detail.ReversalReasons);
        }

        public override int GetHashCode()
        {
            var hashCode = 1936524713;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Description);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<string>>.Default.GetHashCode(ReversalReasons);
            return hashCode;
        }

    }
}
