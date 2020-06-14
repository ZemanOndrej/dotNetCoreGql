using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace webapiPgGql.Controllers
{
    [ApiController]
    [Route("/api")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public String Get()
        {
            return "Welcome to the api";
        }
    }
}
