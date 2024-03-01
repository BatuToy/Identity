using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

public class UserController : Controller 
{
    
    private readonly UserManager<AppUser> _userManager ;
    
    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
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
           var user = new AppUser { UserName = model.Email , FullName = model.FullName , Email = model.Email };
        
            var result = await _userManager.CreateAsync(user , model.Password);

            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach(var err in result.Errors)
            {
                ModelState.AddModelError("" , err.Description);
            } 
        }
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if(user != null)
        {
            await _userManager.DeleteAsync(user);
        }
        return RedirectToAction("Index");
    }
   public async Task<IActionResult> Edit(string Id)
    {
        if(Id == null)
        {
            return RedirectToAction("Index" , "Home");
        }
        
        var user = await _userManager.FindByIdAsync(Id);

        if(user != null)
        {
            return View( new EditViewModel {
                Id = user.Id ,
                FullName = user.FullName ,
                Email = user.Email
            });
        }

        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(string Id , EditViewModel model)
    {
        if(Id != model.Id)
        {
            return RedirectToAction("Index");
        }

        if(ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if(user != null)
            {
                user.FullName = model.FullName;
                user.Email = model.Email;
                
                var result = await  _userManager.UpdateAsync(user);

                if(result.Succeeded && !string.IsNullOrEmpty(model.Password))
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user , model.Password);
                }

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
        }
        return View(model);
    }
}