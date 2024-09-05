using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Models;
using Northwind.UnitOfWork;

namespace Northwind.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Supplier")]
    [Authorize]
    public class SupplierController : Controller
    {
        private  IUnityOfWork _unityOfWork;

        public SupplierController(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_unityOfWork.Supplier.GetById(id));
        }

        [HttpGet]
        [Route("GetPaginatedSupplier/{page:int}/{rows:int}")]
        public IActionResult GetPaginatedCustomer(int page, int rows)
        {
            return Ok(_unityOfWork.Supplier.CustomersPagedList(page, rows));
        }

        [HttpPost]
        public IActionResult Post([FromBody]Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(_unityOfWork.Supplier.Insert(supplier));
        }

        [HttpPut]
        public IActionResult Put([FromBody]Supplier supplier)
        {

            if (ModelState.IsValid && _unityOfWork.Supplier.Update(supplier))
            {
                return Ok(new { Message = "The supplier is Updated" });
            }

            return BadRequest();

        }

        [HttpDelete]
        public IActionResult Delete([FromBody]Supplier supplier)
        {
            if (supplier.Id > 0)
            {
                return Ok(_unityOfWork.Supplier.Delete(supplier));
            }
            return BadRequest();
        }
    }
}