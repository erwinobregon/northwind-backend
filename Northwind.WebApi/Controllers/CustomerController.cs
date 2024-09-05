using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.UnitOfWork;
using System;

namespace Northwind.Web.Controllers
{
    [Route("api/Customer")]
    [Authorize]
    public class CustomerController: Controller
    {
        private readonly IUnityOfWork _unityOfWork;
        public CustomerController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unityOfWork.Customer.GetById(id));
        }

        [HttpGet]
        [Route("GetPaginatedCustomer/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedCustomer(int page, int rows)
        {
            //throw new Exception("Northwind Error");
            return Ok(_unityOfWork.Customer.CustomersPagedList(page, rows));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(_unityOfWork.Customer.Insert(customer));
        }

        [HttpPut]
        public IActionResult Put([FromBody]Customer customer)
        {

            if (ModelState.IsValid && _unityOfWork.Customer.Update(customer))
            {
                return Ok(new { Message = "The Customer is Updated" });
            }

            return BadRequest();

        }

        [HttpDelete]
        public IActionResult Delete([FromBody]Customer customer)
        {
            if (customer.Id > 0)
            {
                return Ok(_unityOfWork.Customer.Delete(customer));
            }
            return BadRequest();
        }

    }
}
