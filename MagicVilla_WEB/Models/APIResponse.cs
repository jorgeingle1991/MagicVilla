﻿using System.Net;

namespace MagicVilla_WEB.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }  
    }
}
