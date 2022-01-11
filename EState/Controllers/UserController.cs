using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EState.Controllers
{
    public class UserController : Controller
    {
      
        public IActionResult Index()
        {
            return View();
        }
    }
}
