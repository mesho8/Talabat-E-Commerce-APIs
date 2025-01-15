﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Controllers;
using Talabat.APIs.Errors;
using Talabat.DAL.Data;

namespace Talabat.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }
        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _context.Products.Find(42);
            if(product == null) return NotFound(new ApiResponse(404));
            return Ok();
        }
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _context.Products.Find(42);
            var productToReturn = product.ToString();
            return Ok();
        }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
