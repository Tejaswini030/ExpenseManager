﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ExpenseManager.Models;

namespace ExpenseManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ExpenseManagerDbContext _context;

    public HomeController(ILogger<HomeController> logger, ExpenseManagerDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Expenses()
    {
        var allExpenses = _context.Expenses.ToList();
        var totalExpenses = allExpenses.Sum(x => x.Value);
        ViewBag.Expenses = totalExpenses;
        return View(allExpenses);
    }

    public IActionResult CreateEditExpense(int? id)
    {
        if (id != null)
        {
            var expensInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
            return View(expensInDb);

        }
        return View();
    }

    public IActionResult DeleteExpense(int id)
    {
        var expensInDb = _context.Expenses.SingleOrDefault(expense => expense.Id == id);
        _context.Expenses.Remove(expensInDb);
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }

    public IActionResult CreateEditExpenseForm(Expense model)
    {
        if (model.Id == 0)
        {
            _context.Expenses.Add(model);

        }
        else
        {
            _context.Expenses.Update(model);
        }
        _context.SaveChanges();
        return RedirectToAction("Expenses");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

