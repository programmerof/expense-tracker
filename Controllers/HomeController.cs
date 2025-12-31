using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using _.Models;
using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace _.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Get all expenses from database
        var expenses = await _context.Expenses.ToListAsync();
        
        // Calculate total amount
        var totalAmount = expenses.Sum(e => e.Amount);
        
        // Get count of entries
        var count = expenses.Count;
        
        // Get recent expenses (last 5)
        var recentExpenses = expenses
            .OrderByDescending(e => e.Id)
            .Take(5)
            .ToList();
        
        // Pass data to view
        ViewBag.TotalAmount = totalAmount.ToString("N2");
        ViewBag.Count = count;
        ViewBag.RecentExpenses = recentExpenses;
        
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