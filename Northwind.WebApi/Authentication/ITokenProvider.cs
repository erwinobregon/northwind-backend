using Microsoft.IdentityModel.Tokens;
using Northwind.Models;
using System;

namespace Northwind.Web.Authentication
{
    public interface ITokenProvider
    {
        string CreateToken(User user, DateTime expiry);
        TokenValidationParameters GetValidationParameters();
    }
}
