using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;
namespace wpgintegrationtests.xunit
{
    public class TextFileAttribute : DataAttribute
    {
        private string path;
        public TextFileAttribute(string path)
        {
            this.path = path;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            yield return new object[] { System.IO.File.ReadAllText(path) };
        }
    }
}
