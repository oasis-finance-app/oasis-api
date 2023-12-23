using Microsoft.EntityFrameworkCore;
using Oasis.Enums;
using Oasis.Models;

namespace Oasis.Context;

public class EntityContext : DbContext
{
	public EntityContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<BankAccount> BankAccounts { get; set; }
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Transaction> Transactions { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		
	}
}