﻿namespace Northwind.Web.Authentication
{
    public class JsonWebToken
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_in { get; set; }
        public string Refesh_Token { get; set; }
    }
}
