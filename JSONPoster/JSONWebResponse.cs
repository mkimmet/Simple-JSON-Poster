﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace JSONPoster
{
    public class JSONWebResponse
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public Exception ErrorDetails { get; set; }
    }
}
