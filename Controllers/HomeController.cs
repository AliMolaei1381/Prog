using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prog.Models;
using Prog.Models.Context;

namespace Prog.Controllers;

public class HomeController : Controller
{
  private readonly Context_Db db;
  private readonly IWebHostEnvironment env;
  public HomeController(Context_Db _db, IWebHostEnvironment _env)
  {
    db=_db;
    env=_env;
  }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
