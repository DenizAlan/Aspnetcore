using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IdentityApp.Controllers
{
   
    public class RolesController : Controller
    {
       private readonly RoleManager<AppRole> _roleManager;

       public RolesController(RoleManager<AppRole> roleManager)
       {
            _roleManager=roleManager;
       }

        public IActionResult Index()
       {
        return View(_roleManager.Roles);
       }

       public IActionResult Create()
       {
        return View();
       }

       [HttpPost]

        public async Task<IActionResult> Create(AppRole model)
       {
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(model);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError("",err.Description);
                }
            }

             return View(model);

       }



    }
}