using Microsoft.EntityFrameworkCore;
using Oasis.Enums;
using Oasis.Models;

namespace Oasis.Context;

public class EntityContext : DbContext
{
	public EntityContext(DbContextOptions options) : base(options)
	{
	}

	public DbSet<Customer> Customers { get; set; }
	public DbSet<BankAccount> BankAccounts { get; set; }
	public DbSet<Transaction> Transactions { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>(entity =>
		{
			entity.HasKey(e => e.CustomerId);
			entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();

			entity.HasMany(c => c.BankAccounts)
					.WithOne(ba => ba.Customer)
					.HasForeignKey(ba => ba.CustomerId);
		});
	}

}