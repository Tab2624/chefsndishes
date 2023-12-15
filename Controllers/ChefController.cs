using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chefsndishes.Models;
using Microsoft.EntityFrameworkCore;

namespace chefsndishes.Controllers;


public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;
    private MyContext _context;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Chef> ChefsFromDb = _context.Chefs.Include(c => c.Dishes).OrderByDescending(c => c.CreatedAt).ToList();
        return View("Index", ChefsFromDb);
    }

    [HttpGet("/chefs/new")]
    public IActionResult AddChef()
    {
        return View("NewChef");
    }

    [HttpPost("chefs/create")]
    public IActionResult CreateChef(Chef newChef)
    {
        if (!ModelState.IsValid)
        {
            return View("NewChef");
        }
        _context.Chefs.Add(newChef);
        //! SAVE CHANGES TO THE DB, OR IT WON'T BE PERMANENT!
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
