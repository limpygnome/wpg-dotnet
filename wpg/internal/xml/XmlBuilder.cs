using System;
using System.Collections.Generic;
using System.Xml.Linq;
using wpg.exception;
namespace wpg.@internal.xml
{
    public class XmlBuilder
    {
        private XmlEndpoint endpoint;
        private XDocument document;
        private XElement current;

        private XmlBuilder(XmlEndpoint endpoint, XDocument document, XElement current)
        {
            this.endpoint = endpoint;
            this.document = document;
            this.current = current;
        }

        public XmlBuilder(XmlEndpoint endpoint)
        {
            this.endpoint = endpoint;
            this.document = new XDocument(new XElement(endpoint.RootElement));

            XDocumentType documentType = new XDocumentType(endpoint.RootElement, endpoint.DocTypePublicId, endpoint.DocTypeSystemId, null);
            this.document.AddFirst(documentType);
            this.current = this.document.Root;
            current.SetAttributeValue("version", endpoint.Version);
        }

        public XmlBuilder a(String key, String value)
        {
            current.SetAttributeValue(key, value);
            return this;
        }

        public XmlBuilder a(String key, int value)
        {
            String text = value.ToString();
            return a(key, text);
        }

        public String a(String key)
        {
            String value = current.Attribute(key).ToString();
            if (value.Length == 0)
            {
                value = null;
            }
            return value;
        }

        public long aToLong(String key)
        {
            String value = a(key);

            try
            {
                
                long longValue = Convert.ToInt64(value);
                return longValue;
            }
            catch (FormatException e)
            {
                throw new WpgRequestException("Failed to read attribute '" + key + "' at " + getPath() + " - value: " + value, e);
            }
        }

        public int? aToIntegerOptional(String key)
        {
            int? result = null;
            String value = a(key);

            if (value != null)
            {
                try
                {
                    result = Int32.Parse(value);
                }
                catch (FormatException e)
                {
                    throw new WpgRequestException("Failed to read attribute '" + key + "' at " + getPath() + " - value: " + value, e);
                }
            }

            return result;
        }

        public int aToInt(String key)
        {
            String value = a(key);
            try
            {
                int intValue = Int32.Parse(value);
                return intValue;
            }
            catch (FormatException e)
            {
                throw new WpgRequestException("Failed to read attribute '" + key + "' at " + getPath() + " - value: " + value, e);
            }
        }

        public XmlBuilder e(String name)
        {
            e(name, false);
            return this;
        }

        public XmlBuilder e(String name, bool addNew)
        {
            // Create if doesn't exist
            XElement element = current.Element(name);

            if (addNew || element == null)
            {
                element = new XElement(name);
                current.Add(element);
            }

            current = element;
            return this;
        }

        public bool isCurrentTag(String tagName)
        {
            return current.Name == tagName;
        }

        public bool hasE(String name)
        {
            return current.Element(name) != null;
        }

        public XmlBuilder cdata(String value)
        {
            current.Add(new XCData(value ?? ""));
            return this;
        }

        public XmlBuilder cdata(int value)
        {
            String text = value.ToString();
            return cdata(text);
        }

        public XmlBuilder cdata(long value)
        {
            String text = value.ToString();
            return cdata(text);
        }

        public String cdata()
        {
            String result = null;
            XNode node = current.FirstNode;

            if (node is XCData)
            {
                XCData nodeCdata = (XCData)node;
                result = nodeCdata.Value;
            }
            return result;
        }

        public String getCdata(String elementName)
        {
            String result = null;

            // Find node
            XElement element = current.Element(elementName);
            if (element != null)
            {
                XNode node = element.FirstNode;

                if (node is XCData)
                {
                    XCData nodeCdata = (XCData)node;
                    result = nodeCdata.Value;
                }
            }

            return result;
        }

        public long? getCdataLong(String elementName)
        {
            String value = getCdata(elementName);
            try
            {
                long longValue = Int64.Parse(value);
                return longValue;
            }
            catch (FormatException e)
            {
                throw new WpgRequestException("Failed to read attribute '" + elementName + "' at " + getPath() + " - value: " + value, e);
            }
        }

        public XmlBuilder up()
        {
            XElement parent = current.Parent;
            if (parent != null)
            {
                current = parent;
            }
            return this;
        }

        public XmlBuilder reset()
        {
            current = document.Root;
            return this;
        }

        public List<XmlBuilder> childTags(String tagName)
        {
            List<XmlBuilder> result = new List<XmlBuilder>();
            foreach (XElement element in current.Elements())
            {
                XmlBuilder clone = new XmlBuilder(endpoint, document, element);
                result.Add(clone);
            }
            return result;
        }

        public bool hasChildNodes()
        {
            return current.HasElements;
        }

        public XmlBuilder getElementByName(String elementName)
        {
            XmlBuilder result = null;
            ICollection<XElement> elements = current.Elements().Descendants(elementName) as ICollection<XElement>;
            IEnumerator<XElement> it = elements.GetEnumerator();
            if (it.MoveNext())
            {
                XElement first = it.Current;
                result = new XmlBuilder(endpoint, document, first);
            }
            return result;
        }

        public override String ToString()
        {
            return document.ToString();
        }

        public static XmlBuilder parse(XmlEndpoint endpoint, String text)
        {
            XDocument document = XDocument.Parse(text);
            XmlBuilder builder = new XmlBuilder(endpoint);

            try
            {
                builder.document = XDocument.Parse(text);
                builder.reset();

                // Check root element is not HTML i.e. webpage
                XElement root = builder.current;
                if ("html" == root.Name)
                {
                    throw new WpgMalformedXmlException(text);
                }
            }
            catch (Exception e)
            {
                throw new WpgMalformedXmlException(text, e);
            }
            return builder;
        }

        public String getPath()
        {
            String path = "";
            XElement parent = current;

            do
            {
                path = "/" + parent.Name + path;
                parent = parent.Parent;
            }
            while (parent != null);
            return path;
        }

    }
}
