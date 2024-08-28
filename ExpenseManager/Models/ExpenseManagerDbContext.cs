using System;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManager.Models
{
	public class ExpenseManagerDbContext : DbContext
	{
		public DbSet<Expense> Expenses { get; set; }
		public ExpenseManagerDbContext(DbContextOptions<ExpenseManagerDbContext> options)
			: base(options)
		{
		}
	}
}

