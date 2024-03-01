using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers;

public class RoleController : Controller
{
    private readonly RoleManager<AppRole> _roleManager;
    public RoleController(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public IActionResult Index()
    {
        return View();
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
            // var Role = new AppRole { Id = model.Id , Name = model.Name ,
            // ConcurrencyStamp = model.ConcurrencyStamp  , NormalizedName = model.NormalizedName };

            var result = await _roleManager.CreateAsync(model);

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
    
}