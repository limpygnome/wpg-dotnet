﻿using System;
using wpg.connection;
using wpg.connection.http;

namespace wpg.@internal.xml
{
    public class XmlResponse
    {
        public XmlResponse(SessionContext sessionContext, HttpResponse response, XmlBuilder builder)
        {
            this.SessionContext = sessionContext;
            this.Response = response;
            this.Builder = builder;
        }

        public SessionContext SessionContext { get; private set; }
        public HttpResponse Response { get; private set; }
        public XmlBuilder Builder { get; private set; }

    }
}
