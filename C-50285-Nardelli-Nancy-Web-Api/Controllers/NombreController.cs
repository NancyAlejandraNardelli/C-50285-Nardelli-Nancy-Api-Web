using C_50285_Nardelli_Nancy_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace C_50285_Nardelli_Nancy_Web_Api.Controllers
{
    public class NombreController : Controller
    {
        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult GetName()
        {
            var name = new Nombre { name="Nancy Nardelli"};
            return Ok(name);
        }
        
    }
}
