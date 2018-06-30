using System.Collections.Generic;

namespace Worldpay
{
    public class JournalReference
    {
        public JournalReference(string type, string reference)
        {
            this.Type = type;
            this.Reference = reference;
        }

        public string Type { get; private set; }
        public string Reference { get; private set; }

        public override bool Equals(object obj)
        {
            var reference = obj as JournalReference;
            return reference != null &&
                   Type == reference.Type &&
                   Reference == reference.Reference;
        }

        public override int GetHashCode()
        {
            var hashCode = 669799855;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Type);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Reference);
            return hashCode;
        }

    }
}
