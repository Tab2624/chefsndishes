using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using chefsndishes.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace chefsndishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    private MyContext _context;

    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }


    // Dish Dashboard
    [HttpGet("/dishes")]
    public IActionResult AllDishes()
    {
        List<Dish> DishesFromDb = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
        return View("AllDishes", DishesFromDb);
    }

    // Render new dish form
    [HttpGet("dishes/new")]
    public IActionResult NewDish()
    {   
        ViewBag.ChefsFromDb = _context.Chefs.ToList(); 
        return View("NewDish");
    }

    // Add dish to dashboard
    [HttpPost("dishes/create")]
    public IActionResult CreateDish(Dish newDish)
    {
        if (!ModelState.IsValid)
        {
            return View("NewDish");
        }
        _context.Dishes.Add(newDish);
        //! SAVE CHANGES TO THE DB, OR IT WON'T BE PERMANENT!
        _context.SaveChanges();
        return RedirectToAction("Index", "Chef");
    }

    // View one
    [HttpGet("dishes/{dishId}")]
    public IActionResult ViewPost(int dishId)
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(d => d.DishId == dishId);
        if (OneDish == null)
        {
            return RedirectToAction("Index");
        }
        return View("ViewDish", OneDish);
    }

    // Delete by ID
    [HttpPost("dishes/{dishId}/delete")]
    public RedirectToActionResult DeleteDish(int dishId)
    {
        Dish? DishToDelete = _context.Dishes.SingleOrDefault(d => d.DishId == dishId);
        if (DishToDelete != null)
        {
            _context.Remove(DishToDelete);

            // Save Changes
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    // Edit a post
    [HttpGet("dishes/{dishId}/edit")]
    public IActionResult EditPost(int dishId)
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(e => e.DishId == dishId);
        if (OneDish == null)
        {
            return RedirectToAction("Index");
        }
        return View("EditDish", OneDish);
    }

    // Update the new changes
    [HttpPost("dishes/{dishId}/update")]
    public IActionResult UpdatePost(int dishId, Dish editedDish)
    {
        Dish? OldDish = _context.Dishes.FirstOrDefault(e => e.DishId == dishId);
        if (ModelState.IsValid)
        {
            OldDish.DishName = editedDish.DishName;
            OldDish.Calories = editedDish.Calories;
            OldDish.Tastiness = editedDish.Tastiness;

            OldDish.UpdatedAt = DateTime.Now;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("EditDish", editedDish);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
