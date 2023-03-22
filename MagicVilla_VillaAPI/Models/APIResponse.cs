﻿using System.Net;

namespace MagicVilla_VillaAPI.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public bool IsSucccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }  
    }
}
