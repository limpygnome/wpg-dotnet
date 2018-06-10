using System;
using wpg.exception;

namespace wpg.@internal.xml.adapter
{
    public class ErrorCodeAdapter
    {

        public static void throwIfPresent(XmlResponse response)
        {
            XmlBuilder builder = response.Builder;
            XmlBuilder errorTag = builder.getElementByName("error");

            if (errorTag != null)
            {
                handleError(response, errorTag);
            }
        }

        private static void handleError(XmlResponse response, XmlBuilder errorTag)
        {
            long code = errorTag.aToLong("code");
            String message = errorTag.cdata();
            throw new WpgErrorResponseException(code, message, response.Response);
        }

    }
}
