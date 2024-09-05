using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Order")]
    [Authorize]
    public class OrderController: Controller
    {
        private IUnityOfWork _unityOfWork;
        public OrderController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("GetPaginatedOrders/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedOrders(int page, int rows)
        {
            return Ok(_unityOfWork.Order.getPaginatedOrder(page, rows));
        }

    }
}
