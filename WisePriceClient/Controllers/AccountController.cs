using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WisePriceClient.Models;
using WisePriceClient.ViewModels;

namespace WisePriceClient.Controllers
{
  public class AccountController : Controller
  {
    private readonly WisePriceClientContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public IEnumerable<ApplicationUser> Users { get; set; }

    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, WisePriceClientContext db)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public ActionResult Index()
    {
      var users = _userManager.Users;
      foreach (var user in users)
      {
        if (user.UserName == User.Identity.Name)
        {
          return View(user);
        }
      }

      // List<ApplicationUser> users = _db.AspNetUsers.ToList();
      //var thisUser = _db.ApplicationUsers.FirstOrDefault(user => user.UserName == User.Identity.Name);
      // var thisUser = _userManager.Users.FirstOrDefault(user => user.UserName )
      return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register(RegisterViewModel model)
    {
      var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, UserName = model.UserName, Email = model.Email, ZipCode = model.ZipCode };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);

      if (result.Succeeded)
      {
        return RedirectToAction("Login");
      }
      else
      {
        return View();
      }
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, isPersistent : true, lockoutOnFailure : false);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }
  }
}