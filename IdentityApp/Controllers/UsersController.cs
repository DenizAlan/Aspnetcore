using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityApp.Controllers
{
   
    public class UsersController : Controller
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager , RoleManager<AppRole> roleManager)
        {
            _userManager=userManager;
            _roleManager=roleManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        { 
            if(ModelState.IsValid)
            {
                var user=new AppUser {
                            UserName=model.Email,
                            Email=model.Email,
                            FullName=model.FullName
                };

                IdentityResult result=await _userManager.CreateAsync(user , model.Password);

                if(result.Succeeded)
                {
                    return RedirectToAction ("Index");
                }

                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
        
        public async Task<IActionResult> Edit(string id)
        {
            if(id==null)
            {
                return RedirectToAction("Index");
            }

            var user =await _userManager.FindByIdAsync(id);
            if( user !=null)
            {
                //Kullancının seçebilegi roller viewBag aracılıgıyla taşınıyor ders:132
                ViewBag.Roles = await _roleManager.Roles.Select(r=>r.Name).ToListAsync();

              return View( new EditViewModel {
                        Id=user.Id,
                        FullName=user.FullName,
                        Email=user.Email,
                        //daha önce seçtiği roller ders 132
                        SelectedRoles= await _userManager.GetRolesAsync(user)            
                 
                });
            }

             return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit (string id , EditViewModel model)
        {
            if(id != model.Id)
            {
                return RedirectToAction("Index");
            }

            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if(user !=null )
                {
                    user.Email=model.Email;
                    user.FullName=model.FullName;
                    
                    var result= await _userManager.UpdateAsync(user);

                    //mmodele parola eklenmisse eksi parolayı silip yenisini güncelleme işlemi
                    if(result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user , model.Password);
                    }  

                    if(result.Succeeded)
                    {
                         return RedirectToAction("Index");
                    }

                    foreach ( IdentityError err in result.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }

                }
            }

            return View(model);
        }


        [HttpPost]

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
             
             if(user!=null)
             {
                await _userManager.DeleteAsync(user);
             }

             return RedirectToAction("Index");
        }


    }
}