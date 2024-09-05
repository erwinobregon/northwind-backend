using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.UnitOfWork;
using Northwind.Web.Authentication;
using System;

namespace Northwind.Web.Controllers
{

    [Route("api/token")]
    public class TokenController:Controller
    {
        private ITokenProvider _tokenProvider;
        private IUnityOfWork _unitofWork;

        public TokenController(ITokenProvider tokenProvider, IUnityOfWork unitofWork)
        {
            _tokenProvider = tokenProvider;
            _unitofWork = unitofWork;
            
        }
        [HttpPost]
        public JsonWebToken Post([FromBody]User userLogin)
        {
            var user = _unitofWork.User.ValidateUser(userLogin.Email, userLogin.Password);
            if (user==null)
            {
                throw new UnauthorizedAccessException();
            }

            var token = new JsonWebToken
            {
                Access_Token = _tokenProvider.CreateToken(user, DateTime.UtcNow.AddHours(8)),
                Expires_in = 480 // minutes; 
            };
            return token;
        }
    }
}
